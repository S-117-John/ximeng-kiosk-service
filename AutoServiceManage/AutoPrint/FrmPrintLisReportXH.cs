using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Properties;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using BusinessFacade.His.Inpatient;
using System.IO;
using TiuWeb.ReportBase;
using EntityData.His.Inpatient;
using AutoServiceManage.Common;
using System.Collections;
using EntityData.His.Common;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmPrintLisReportXH : Form
    {
        #region 变量
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        string _inHosOfficeID = string.Empty;
        DataSet InHosData = new DataSet();//住院病人信息
        bool IsCanInput = false;
        #endregion

        #region 构造函数,load,窗体事件
        public FrmPrintLisReportXH()
        {
            InitializeComponent();
        }
        
        private void FrmPrintLisReportXH_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            if (!BindPage())
            {
                this.Close();
            }
        }

        private void FrmPrintLisReportXH_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
              
        #endregion

        #region 返回,退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        #endregion

        #region 住院信息查询
        private bool BindPage()
        {
            try
            {
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();

                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                this.lblPatient.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                this.lblSex.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                this.lblAge.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                this.lblIDCard.Text = "身份证号：" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["IDENTITYCARD"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];
                    _inHosID = drInHos["INHOSID"].ToString();
                    _inHosOfficeID = drInHos["INHOSOFFICEID"].ToString();
                    this.lblInHosID.Text = "住院号："+_inHosID;
                }
                DataTable dtReport = GetListData();
                if (dtReport != null && dtReport.Rows.Count != 0)
                {
                    this.gdcMain.DataSource = dtReport;
                    return true;
                }
                else
                {
                    SkyComm.ShowMessageInfo("未找到您的检验报告单信息!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("获取患者信息失败!");
                return false;
            }
        }
        #endregion

        #region 数据查询及更新
        /// <summary>
        /// 查询检验中间表，查询患者检验报告信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetListData()
        {
            try
            {
                DataTable dtAllReport = this.CreateDataTable();
                #region 查询杏和LIS数据
                StringBuilder sb = new StringBuilder();
                sb.Append("select distinct OUTPATIENT_ID,REQUISITION_ID,TEST_ORDER_NAME,SAMPLING_TIME from HIS_INSPECTION_SAMPLE left outer join T_PRINT_RECORD tp on HIS_INSPECTION_SAMPLE.REQUISITION_ID=tp.PRINTBUSINESSID where PATIENT_TYPE='2'and OUTPATIENT_ID=@OUTPATIENT_ID AND ( SAMPLING_TIME IS NOT NULL OR SAMPLING_TIME<>'') AND LENGTH(SAMPLING_TIME)>6 AND TP.PRINTCOUNT IS NULL");

                Hashtable ht1 = new Hashtable();
                ht1.Add("@OUTPATIENT_ID", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString());
                QuerySolutionFacade facade1 = new QuerySolutionFacade();
                DataSet ds1 = facade1.ExeQuery(sb.ToString(), ht1);

                if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string ora_startDate = new CommonFacade().GetServerDateTime().AddMonths(-3).ToString("yyyy-MM-dd 00:00:00");

                        if (ds1.Tables[0].Rows[i]["SAMPLING_TIME"].ToString().Trim() != "")
                        {
                            DateTime samplingTime = Convert.ToDateTime(ds1.Tables[0].Rows[i]["SAMPLING_TIME"].ToString().Trim().Replace("@", ":"));
                            ds1.Tables[0].Rows[i]["SAMPLING_TIME"] = samplingTime.ToString("yyyy-MM-dd HH:mm:ss");

                            if (samplingTime <= Convert.ToDateTime(ora_startDate))
                                continue;
                        }
                        else
                        {
                            continue;
                        }
                        object[] obj = new object[dtAllReport.Columns.Count];
                        ds1.Tables[0].Rows[i].ItemArray.CopyTo(obj, 0);
                        dtAllReport.Rows.Add(obj);
                    }
                }
                #endregion

                return dtAllReport;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("查询检验结果报表失败，原因：" + ex.Message);
                return null;
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("OUTPATIENT_ID");
            dtTemp.Columns.Add("REQUISITION_ID");
            dtTemp.Columns.Add("TEST_ORDER_NAME");
            dtTemp.Columns.Add("SAMPLING_TIME");
            return dtTemp;
        }

        #endregion

        #region 检验单打印次数检测及更新
        /// <summary>
        /// 查询该信息是否已被打印过
        /// </summary>
        /// <returns></returns>
        private bool checkPrintCount(string bussID)
        {
            TPrintRecordFacade trFacade = new TPrintRecordFacade();
            List<TPrintRecordData> entity = trFacade.Get("PRINTBUSINESSID='" + bussID +"' AND BUSINESSTYPE='检验报告'");
            if (entity.Count > 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 插入打印次数记录表
        /// </summary>
        private void insertPrintCount()
        {
            try
            {
                TPrintRecordFacade trFacade = new TPrintRecordFacade();
                DataTable dtInsert = (DataTable)this.gdcMain.DataSource;
                foreach (DataRow drInsert in dtInsert.Rows)
                {
                    TPrintRecordData entity = new TPrintRecordData();
                    entity.Printbusinessid = drInsert["REQUISITION_ID"].ToString ();
                    entity.Businesstype = "检验报告";
                    entity.Printcount = 1;
                    trFacade.Insert(entity);
                }
            }
            catch { };
        }
        #endregion

        #region 确认打印
        private void lblOK_Click(object sender, EventArgs e)
        {
            try
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Stop();

                this.lblOK.Enabled = false;
                this.btnReturn.Enabled = false;
                this.btnExit.Enabled = false;

                #region 打印方法
                this.AnsyWorker(ui =>
                {
                    ui.UpdateTitle("正在打印，请稍等...");

                    ui.SynUpdateUI(() =>
                    {
                        DataTable dtPrint = (DataTable)this.gdcMain.DataSource;
                        string printParams = string.Empty;
                        foreach (DataRow drPrint in dtPrint.Rows)
                        {
                            printParams = string.IsNullOrEmpty(printParams) ? drPrint["REQUISITION_ID"].ToString().Replace(" ","").TrimEnd() : printParams + "^" + drPrint["REQUISITION_ID"].ToString().Replace(" ", "").TrimEnd();
                        }
                        printParams = printParams + ",,,,,,,,";
                        string[] argsList = new string[1];
                        argsList[0] = printParams;
                        string path = System.IO.Directory.GetCurrentDirectory() + "\\LisPrint\\";
                        string fileName = "XHLis42.exe";
                        StartProcess(argsList, path, fileName);

                        this.insertPrintCount();//更新打印记录
                        SkyComm.ShowMessageInfo("检验报告单打印成功!");
                        this.Close();
                    });

                });
                #endregion
            }
            catch (Exception ex)
            {
                ucTime1.timer1.Start();
                this.lblOK.Enabled = true;
                this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
                Skynet.LoggingService.LogService.GlobalErrorMessage("检验报告单打印失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("检验报告单打印失败!");
            }
            finally
            {
                //ucTime1.timer1.Start();
                //this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                //this.btnExit.Enabled = true;

            }
        }

        private void StartProcess(string[] argsList, string path, string fileName)
        {
            //声明一个程序信息类
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名
            Info.FileName = fileName;
            //设置外部程序的启动参数（命令行参数）为test.txt
            Info.Arguments = argsList[0];
            //设置外部程序工作目录为  C:\
            Info.WorkingDirectory = path;
            try
            {
                //
                //启动外部程序
                //
                System.Diagnostics.Process.Start(Info).WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                //throw new Exception("系统找不到指定的程序文件。\r"+e.Message,e);
                SkyComm.ShowMessageInfo("检验报告单打印失败!");
            }
        }
        #endregion

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
