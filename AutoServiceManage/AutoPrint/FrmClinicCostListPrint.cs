using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessFacade.His.Clinic;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmClinicCostListPrint : Form
    {
        #region 构造函数及LOAD
        public FrmClinicCostListPrint()
        {
            InitializeComponent();
        }

        private void FrmClinicCostListPrint_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();

            this.lblxm.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
            this.lblxb.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
            this.lblnl.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString();        
        }
        #endregion

        #region 返回，退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        #endregion

        private void lblClinicEmrPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.ucTime1.timer1.Stop();
                if (false == System.IO.File.Exists(Application.StartupPath + @"\\Reports\\门诊费用清单.frx"))
                {
                    SkyComm.ShowMessageInfo("系统没有找到报表文件“门诊费用清单.frx”!");
                    return;
                }

                this.AnsyWorker(ui =>
                {
                    ui.UpdateTitle("正在准备数据，请稍等...");

                    ui.SynUpdateUI(() =>
                    {

                        #region 查询列表
                        string[] Condition = {"", "", "", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString (),"", "", "" };
                        DetailAccountFacade facade = new DetailAccountFacade();
                        DateTime dtStart = Convert.ToDateTime("2016-01-01");
                        DateTime dtEnd = Convert.ToDateTime("2016-10-11");
                        DataSet ds = facade.QueryPatientPayList(Condition, dtStart,dtEnd);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                row["MONEY"] = decimal.Round(decimal.Parse(row["MONEY"].ToString()), 2);
                            }
                        }
                        #endregion
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            SkyComm.ShowMessageInfo("所选时间段内未找到门诊费用信息！");
                            return;
                        }

                        ui.UpdateTitle("正在打印，请稍等...");
                        ds.WriteXml(Application.StartupPath + @"\\ReportXml\\门诊费用清单.xml");
                        PrintManager print = new PrintManager();
                        print.InitReport("门诊费用清单");
                        print.AddData(ds.Tables[0], "report");

                        Hashtable MedType = new Hashtable();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (dr["MEDICARETYPE"].ToString().Trim() != string.Empty)
                            {
                                if (MedType.ContainsKey("医保" + dr["MEDICARETYPE"].ToString()) == false)
                                {
                                    MedType.Add("医保" + dr["MEDICARETYPE"].ToString(), Convert.ToDecimal(dr["MONEY"]));
                                }
                                else
                                {
                                    MedType["医保" + dr["MEDICARETYPE"].ToString()] = Convert.ToDecimal(MedType["医保" + dr["MEDICARETYPE"].ToString()]) + Convert.ToDecimal(dr["MONEY"]);
                                }
                            }

                            if (dr["NMEDICARETYPE"].ToString().Trim() != string.Empty)
                            {
                                if (MedType.ContainsKey("农保" + dr["NMEDICARETYPE"].ToString()) == false)
                                {
                                    MedType.Add("农保" + dr["NMEDICARETYPE"].ToString(), Convert.ToDecimal(dr["MONEY"]));
                                }
                                else
                                {
                                    MedType["农保" + dr["NMEDICARETYPE"].ToString()] = Convert.ToDecimal(MedType["农保" + dr["NMEDICARETYPE"].ToString()]) + Convert.ToDecimal(dr["MONEY"]);
                                }
                            }
                        }

                        foreach (DictionaryEntry de in MedType)
                        {
                            print.AddParam(de.Key.ToString(), de.Value.ToString());
                        }

                        PrintManager.CanDesign = true;
                        //print.PreView();
                        print.Print();
                        print.Dispose();
                        Thread.Sleep(100);
                    });
                });
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("门诊费用清单打印异常：" + ex.Message);
            }
            finally
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
            }
        }

        #region 异步方法
        AnsyCall _call;
        List<string> NotEnableArray = new List<string>();
        protected void AnsyWorker(Action<UpdataUIAction> action, AnsyStyle style)
        {
            if (this.AnsyIsBusy)
                return;
            if (_call == null)
            {
                _call = new AnsyCall(this);
                _call.WorkCompletedAction = () =>
                {
                    NotEnableArray.Clear();
                };
            }
            //使工具栏 置灰
            _call.AnsyWorker(action, style);
        }
        protected void AnsyWorker(Action<UpdataUIAction> action)
        {
            AnsyWorker(action, AnsyStyle.LoadingPanel);
        }

        protected bool AnsyIsBusy
        {
            get
            {
                if (_call == null)
                    return false;
                else
                    return _call.IsBusy;
            }
        }
        #endregion

    }
}
