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
using BusinessFacade.His.Common;
using BusinessFacade.His.Inpatient;
using EntityData.His.Inpatient;
using EntityData.His.Common;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmInhosCostListPrint : Form
    {

        #region 自定义

        private string _inhosID = string.Empty;
        private int _inhosTime = 0;
        //DataSet[] dsAllDetail = null;
        //DataSet[] dsAllSum = null;
        List<DataSet> dsAllSum = new List<DataSet>();
        List<DataSet> dsAllDetail = new List<DataSet>();
        #endregion

        #region 构造函数及LOAD
        public FrmInhosCostListPrint()
        {
            InitializeComponent();
        }

        private void FrmInhosCostListPrint_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            if (BindPage())
            {
                this.lblInHosDate.Text = dsAllSum[0].Tables[0].Rows[0]["INHOSDATE"].ToString();
                this.lblOutHosDate.Text = dsAllSum[0].Tables[0].Rows[0]["LEAVEHOSDATE"].ToString();
                this.lblName.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                this.lblInHosNO.Text = dsAllSum[0].Tables[0].Rows[0]["INHOSID"].ToString();
                this.lblBedNo.Text = dsAllSum[0].Tables[0].Rows[0]["BEDID"].ToString();
                this.lblYJK.Text = dsAllSum[0].Tables[0].Rows[0]["ADVANCE"].ToString().Trim() == "" ? "0.00" : Convert.ToDecimal(dsAllSum[0].Tables[0].Rows[0]["ADVANCE"].ToString().Trim()).ToString("0.####");
                decimal totalCost = 0;
                for (int i = 0; i < dsAllSum.Count; i++)
                {
                    if (dsAllSum[i] != null && dsAllSum[i].Tables.Count > 0 && dsAllSum[i].Tables[0].Rows.Count > 0)
                    {
                        totalCost += dsAllSum[i].Tables[0].Rows[0]["TOTALMONEY"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(dsAllSum[i].Tables[0].Rows[0]["TOTALMONEY"].ToString().Trim());
                    }
                }
                this.lblTotalCost.Text = totalCost.ToString ("0.####");
            }
            else
            {
                this.Close();
            }

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

        private bool BindPage()
        {
            try
            {
                LeaveHosRecordFacade lhFacade = new LeaveHosRecordFacade();
                LeaveHosRecordFacade leaveHosRecordFacade = new LeaveHosRecordFacade();

                DataSet dsLeaveHos = lhFacade.QuaryLeaveHosInfoLastByDiagnoseID(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString());
                //DataSet dsLeaveHos = lhFacade.QuaryLeaveHosInfoLastByDiagnoseID("0000643764");

                if (dsLeaveHos == null || dsLeaveHos.Tables[0].Rows.Count == 0)
                {
                    SkyComm.ShowMessageInfo("未找到您的出院记录信息，点击关闭后返回!");
                    return false;
                }

                string inHosID = dsLeaveHos.Tables[0].Rows[0]["INHOSID"].ToString();
                int inhostime = Convert.ToInt32(dsLeaveHos.Tables[0].Rows[0]["CURRENTINHOSMARK"].ToString());
                _inhosID = inHosID;
                _inhosTime = inhostime;

                string startDate = "2010-01-01 00:00:00";
                string endDate = new CommonFacade().GetServerDateTime().ToString("yyyy-MM-dd 23:59:59");
                LeaveHosRecordData childInhosData = (LeaveHosRecordData)leaveHosRecordFacade.FindChildInfoByMotherID(_inhosID, _inhosTime);
                List<string> allInHosID = new List<string>();
                allInHosID.Add(inHosID);
                foreach (DataRow drInhos in childInhosData.Tables[0].Rows)
                {
                    allInHosID.Add(drInhos["INHOSID"].ToString().Trim());
                }
                foreach (string tempID in allInHosID)
                {
                    DataSet dsDetail = new DataSet();
                    DataSet dsSum = new DataSet();
                    inHosID = tempID;
                    #region 查询列表
                    Hashtable inParamListDetail = new Hashtable(); //输入参数名及参数值
                    Hashtable inParamListSum = new Hashtable(); //输入参数名及参数值
                    Hashtable outParamList = new Hashtable(); //输出参数名及参数类型
                    ArrayList keys = new ArrayList(); //输入参数名
                    ArrayList keysSum = new ArrayList(); //输入参数名

                    inParamListDetail.Add("InPatientID", inHosID);
                    inParamListDetail.Add("InHosOffice", 0);
                    inParamListSum.Add("InPatientID", inHosID);
                    inParamListSum.Add("InHosOffice", 0);

                    inParamListDetail.Add("StartTime", startDate);
                    inParamListDetail.Add("EndTime", endDate);
                    inParamListSum.Add("StartTime", startDate);
                    inParamListSum.Add("EndTime", endDate);

                    inParamListDetail.Add("StatisticExpenseDetail", 0); //统计明细
                    inParamListDetail.Add("StatisticLeechdomDetail", 0); //统计明细
                    inParamListDetail.Add("TotalRollExpense", 0); //统计明细
                    inParamListDetail.Add("StatisticRefund", 0); //统计明细
                    inParamListDetail.Add("TotalExpense", 0); //统计明细
                    inParamListDetail.Add("ShowZeroExpense", 1);

                    inParamListDetail.Add("InHosTime", inhostime);
                    inParamListSum.Add("InHosTime", inhostime);
                    //加入所有的参数
                    keys.Add("InPatientID");
                    keys.Add("InHosOffice");
                    keys.Add("StartTime");
                    keys.Add("EndTime");
                    keys.Add("StatisticRefund");
                    keys.Add("StatisticExpenseDetail");
                    keys.Add("StatisticLeechdomDetail");
                    keys.Add("TotalRollExpense");
                    keys.Add("TotalExpense");
                    keys.Add("ShowZeroExpense");
                    keys.Add("InHosTime");

                    keysSum.Add("InPatientID");
                    keysSum.Add("InHosOffice");
                    keysSum.Add("StartTime");
                    keysSum.Add("EndTime");
                    keysSum.Add("InHosTime");


                    ReportSolutionFacade reportSolutionFacade = new ReportSolutionFacade();

                    //程序实现存储过程  明细和汇总
                    BusinessFacade.His.ReportDesign.LeavePatientSpendDetailFacade reportLeavePatientSpendDetailFacade = new BusinessFacade.His.ReportDesign.LeavePatientSpendDetailFacade();
                    BusinessFacade.His.ReportDesign.LeavePatientSpendSumFacade reportLeavePatientSpendSumFacade = new BusinessFacade.His.ReportDesign.LeavePatientSpendSumFacade();

                    try
                    {
                        //this.AnsyWorker(ui =>
                        //{
                        //ui.UpdateTitle("正在打印，请稍等...");

                        //ui.SynUpdateUI(() =>
                        //{

                        dsSum = reportSolutionFacade.ExecStoredProc("PH_LeavePatientSpendSum", keysSum, inParamListSum, outParamList);
                        dsDetail = reportSolutionFacade.ExecStoredProc("PH_LeavePatientSpendDetail", keys, inParamListDetail, outParamList);

                        DataTable dtMaster = new DataTable("MASTER");
                        dtMaster.Columns.Add("住院号", typeof(string));
                        dtMaster.Columns.Add("住院次数", typeof(System.Int32));
                        dtMaster.Columns.Add("费用类别", typeof(string));
                        dtMaster.Columns.Add("预交款", typeof(System.Decimal));
                        dtMaster.Columns.Add("总花费", typeof(System.Decimal));
                        dtMaster.Columns.Add("现金退款", typeof(System.Decimal));//pzj 2009-05-19 added
                        dtMaster.Columns.Add("姓名", typeof(string));
                        dtMaster.Columns.Add("入院日期", typeof(string));
                        dtMaster.Columns.Add("出院日期", typeof(string));
                        dtMaster.Columns.Add("住院科室", typeof(string));
                        dtMaster.Columns.Add("科床号", typeof(string));
                        dtMaster.Columns.Add("入科日期", typeof(string));

                        InHosInvoiceFacade invoiceFacade = new InHosInvoiceFacade();
                        DataSet dsInvoice = invoiceFacade.FindAll();
                        foreach (DataRow dr in dsInvoice.Tables[0].Rows)
                        {
                            dtMaster.Columns.Add(dr["INVOICEITEM"].ToString(), typeof(System.Decimal));
                        }
                        foreach (DataRow drMaster in dsSum.Tables[0].Rows)
                        {
                            DataRow[] drs = dtMaster.Select("住院号='" + drMaster["INHOSID"].ToString() + "' and 住院次数 = " + drMaster["CURRENTINHOSMARK"].ToString());//yuanxiaoxia 21166 明细按照住院次数分开打印
                            if (drs.Length == 0)
                            {
                                DataRow drNew = dtMaster.NewRow();
                                drNew["住院号"] = drMaster["INHOSID"];
                                drNew["住院次数"] = drMaster["CURRENTINHOSMARK"];
                                drNew["费用类别"] = drMaster["FEETYPE"];
                                drNew["预交款"] = drMaster["ADVANCE"];
                                drNew["总花费"] = drMaster["TOTALMONEY"];
                                drNew["现金退款"] = drMaster["CASHREFUND"];//pzj 2009-05-19 added
                                drNew["姓名"] = drMaster["PATIENTNAME"];
                                drNew["入院日期"] = drMaster["InHosDate"];
                                drNew["出院日期"] = drMaster["leaveHosDate"];
                                drNew["住院科室"] = drMaster["InHosOffice"];
                                drNew["科床号"] = drMaster["BedID"];
                                int inhostime1 = Convert.ToInt32(drMaster["CURRENTINHOSMARK"].ToString());//yuanxiaoxia 2015-4-30 case21166 新增参数inhostime
                                LeaveHosRecordData leaveHosRecordData1 = (LeaveHosRecordData)new LeaveHosRecordFacade().FindLeaveHosInfoByInHosIDAndCurrentInhosMark(drMaster["INHOSID"].ToString(), inhostime1);
                                if (leaveHosRecordData1.Tables.Count > 0 && leaveHosRecordData1.Tables[0].Rows.Count > 0 && leaveHosRecordData1.Tables[0].Rows[0]["ENTEROFFICEDATE"].ToString().Trim() != string.Empty)
                                {
                                    drNew["入科日期"] = Convert.ToDateTime(leaveHosRecordData1.Tables[0].Rows[leaveHosRecordData1.Tables[0].Rows.Count - 1]["ENTEROFFICEDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                                }

                                drNew[drMaster["INVOICEITEM"].ToString()] = drMaster["MONEY"];
                                dtMaster.Rows.Add(drNew);
                            }
                            else
                            {
                                DataRow drUpdate = drs[0];
                                drUpdate.BeginEdit();
                                drUpdate[drMaster["INVOICEITEM"].ToString()] = drMaster["MONEY"];
                                drUpdate.EndEdit();

                            }
                        }
                        dsDetail.Tables.Add(dtMaster);
                        dsDetail.AcceptChanges();

                        foreach (DataRow dr in dsDetail.Tables[0].Rows)
                        {
                            if (dsDetail.Tables[1].Select("住院号 = '" + dr["INHOSID"].ToString() + "'").Length <= 0)
                            {
                                SkynetMessage.MsgInfo(dr["INHOSID"].ToString());
                            }
                        }

                        DataColumn[] parentColumns = new DataColumn[2];
                        DataColumn[] childColumns = new DataColumn[2];
                        parentColumns[0] = dsDetail.Tables[1].Columns["住院号"];
                        parentColumns[1] = dsDetail.Tables[1].Columns["住院次数"];//yuanxiaoxia 21166 明细按照住院次数分开打印
                        childColumns[0] = dsDetail.Tables[0].Columns["INHOSID"];
                        childColumns[1] = dsDetail.Tables[0].Columns["CURRENTINHOSMARK"];
                        DataRelation relation = new DataRelation("MastDetail", parentColumns, childColumns);
                        dsDetail.Relations.Add(relation);
                        //    });
                        //});

                    }
                    catch (Exception err)
                    {
                        SkyComm.ShowMessageInfo("数据生成失败！" + err.Message);
                        return false;
                    }

                    SUniteChargeFacade sUniteChargeFacade = new SUniteChargeFacade();
                    DataSet sUnitCharge = new DataSet();
                    DataSet medicareClassData = new MedicareClassFacade().FindAll();
                    DataRow[] medicareClassRows;
                    foreach (DataRow dr in dsDetail.Tables[0].Rows)
                    {
                        if (dr["NMEDICARETYPE"].ToString().Trim() != string.Empty)
                        {
                            medicareClassRows = medicareClassData.Tables[0].Select("MEDICAREID='" + dr["NMEDICARETYPE"] + "'");
                            if (medicareClassRows.Length > 0)
                                dr["NMEDICARETYPE"] = medicareClassRows[0]["MEDICARECLASS"];
                            else
                                dr["NMEDICARETYPE"] = "农合药";
                        }
                    }
                    #endregion
                    dsAllSum.Add(dsSum.Copy());
                    dsAllDetail.Add(dsDetail.Copy());
                }
                DataTable dtMain = null;
                foreach (DataSet dsTemp in dsAllDetail)
                {
                    if (dtMain == null)
                        dtMain = dsTemp.Tables[0].Copy();
                    else
                    {
                        object[] obj = new object[dtMain.Columns.Count];
                        for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                        {
                            dsTemp.Tables[0].Rows[i].ItemArray.CopyTo(obj, 0);
                            dtMain.Rows.Add(obj);
                        }
                    }
                }
                if (dtMain == null || dsAllSum[0].Tables.Count == 0)
                {
                    SkyComm.ShowMessageInfo("未找到您的住院费用清单信息！");
                    return false;
                }
                if (dtMain.Rows.Count == 0)
                {
                    SkyComm.ShowMessageInfo("未找到您的住院费用清单信息！");
                    return false;
                }

                gdcMain.DataSource = dtMain;
                return true;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("住院费用清单查询异常：" + ex.Message);
                SkyComm.ShowMessageInfo("未找到您的住院费用清单信息！");
                return false;
            }
        }


        private void lblInhosCostListPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.ucTime1.timer1.Stop();
                //if (!checkPrintCount())
                //{
                //    SkyComm.ShowMessageInfo("此住院费用清单已被打印过，不可再次打印!");
                //    ucTime1.Sec = 60;
                //    ucTime1.timer1.Start();
                //    return;
                //}

                if (false == System.IO.File.Exists(Application.StartupPath + @"\\Reports\\住院费用清单.frx"))
                {
                    SkyComm.ShowMessageInfo("系统没有找到报表文件“住院费用清单.frx”!");
                    ucTime1.Sec = 60;
                    ucTime1.timer1.Start();
                    return;
                }
                this.AnsyWorker(ui =>
                {
                    ui.UpdateTitle("正在打印，请稍等...");

                    ui.SynUpdateUI(() =>
                    {
                        foreach (DataSet dsDetail1 in dsAllDetail)
                        {
                            DataSet dsDetail = dsDetail1.Copy();
                            if (dsDetail.Tables.Count == 0 || dsDetail.Tables[0].Rows.Count == 0)
                                continue;
                            dsDetail.WriteXml(Application.StartupPath + @"\\ReportXml\\住院费用清单.xml");
                            dsDetail.Tables[1].Columns.Add("结算日期", typeof(string));
                            dsDetail.Tables[1].Columns.Add("住院天数", typeof(string));
                            dsDetail.Tables[1].Columns.Add("性别", typeof(string));
                            dsDetail.Tables[1].Columns.Add("年龄", typeof(string));
                            dsDetail.Tables[1].Columns.Add("年龄单位", typeof(string));
                            foreach (DataRow dr in dsDetail.Tables[1].Rows)
                            {
                                if (dr["住院号"].ToString().Contains("d"))
                                    continue;
                                DataSet dsAccount = new AccountBalanceFacade().FindAccountBalanceInfo(dr["住院号"].ToString(), Convert.ToInt32(dr["住院次数"]));
                                dr["结算日期"] = Convert.ToDateTime(dsAccount.Tables[0].Rows[0]["LEAVEHOSDATE"]).ToString("yyyy-MM-dd HH:mm:ss");
                                dr["住院天数"] = dsAccount.Tables[0].Rows[0]["INHOSDAYS"];

                                DataSet dsInhosrecord = new LeaveHosRecordFacade().FindByInHosID(dr["住院号"].ToString(), Convert.ToInt32(dr["住院次数"]));
                                dr["性别"] = dsInhosrecord.Tables[0].Rows[0]["SEX"];
                                dr["年龄"] = dsInhosrecord.Tables[0].Rows[0]["AGE"];
                                dr["年龄单位"] = dsInhosrecord.Tables[0].Rows[0]["AGEUNIT"];
                            }
                            PrintManager print = new PrintManager();
                            print.InitReport("住院费用清单");
                            print.AddData(dsDetail.Tables[1], "report1");
                            print.AddData(dsDetail.Tables[0], "report2");

                            print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                            print.AddParam("DATE", new CommonFacade().GetServerDateTime().ToString());
                            print.AddParam("操作员", SysOperatorInfo.OperatorID);
                            print.AddParam("操作员代码", SysOperatorInfo.OperatorCode);
                            PrintManager.CanDesign = true;
                            //print.PreView();
                            print.Print();
                            print.Dispose();
                        }
                        insertPrintCount();
                        Thread.Sleep(100);
                        ucTime1.Sec = 60;
                        ucTime1.timer1.Start();
                        
                    });

                });
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("住院费用清单打印异常：" + ex.Message);
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
            }
            finally
            {
                //ucTime1.Sec = 60;
                //ucTime1.timer1.Start();
            }
        }

        /// <summary>
        /// 查询该信息是否已被打印过
        /// </summary>
        /// <param name="inhosID"></param>
        /// <param name="inhostimes"></param>
        /// <returns></returns>
        private bool checkPrintCount()
        {
            TPrintRecordFacade trFacade = new TPrintRecordFacade();
            List<TPrintRecordData> entity = trFacade.Get("PRINTBUSINESSID='" + _inhosID + "(" + _inhosTime + ")' AND BUSINESSTYPE='住院'");
            if (entity.Count > 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 插入打印次数记录表
        /// </summary>
        /// <param name="inhosID"></param>
        /// <param name="inhostimes"></param>
        private void insertPrintCount()
        {
            try
            {
                TPrintRecordFacade trFacade = new TPrintRecordFacade();
                TPrintRecordData entity = new TPrintRecordData();
                entity.Printbusinessid = _inhosID + "(" + _inhosTime + ")";
                entity.Businesstype = "住院";
                entity.Printcount = 1;
                trFacade.Insert(entity);
            }
            catch { };
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
