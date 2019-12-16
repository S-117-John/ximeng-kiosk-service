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
using BusinessFacade.His.ReportDesign;
using Skynet.LoggingService;

namespace AutoServiceManage.Inquire
{
    public partial class FrmCostRecord_InHos : Form
    {

        #region 自定义
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        string _inHosOfficeID = string.Empty;
        DateTime _queryTime = new DateTime();
        DataSet InHosData = new DataSet();//住院病人信息
        #endregion

        #region 构造函数及LOAD
        public FrmCostRecord_InHos()
        {
            InitializeComponent();
        }

        private void FrmCostRecord_InHos_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            if (GetPatientInformation())
            {
                _queryTime = new CommonFacade().GetServerDateTime();
                string dtQuery = _queryTime.ToString("yyyy-MM-dd");
                BindPage(dtQuery);
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

        private bool GetPatientInformation()
        {
            try
            {
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];

                    _inHosID = drInHos["INHOSID"].ToString();
                    return true;
                }
                else
                {
                    SkyComm.ShowMessageInfo("未找到您的住院信息，点击关闭后返回!");
                    return false;
                }

            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("未找到您的住院信息，点击关闭后返回!");

                return false;
            }
        }
        private bool BindPage(string queryDate)
        {
            try
            {
                this.lblDate.Text = queryDate;
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
                DataSet InHosData = new DataSet();

                string inHosid = _inHosID;
                //string inHosid = "0000003243";
                string startDate = queryDate;
                string endDate = queryDate;

                DateTime startTime = Convert.ToDateTime(startDate + " 00:00:00");
                DateTime endTime = Convert.ToDateTime(endDate + " 23:59:59");

                Hashtable inParamListDetail = new Hashtable(); //输入参数名及参数值
                Hashtable outParamList = new Hashtable(); //输出参数名及参数类型
                ArrayList keys = new ArrayList(); //输入参数名
                inParamListDetail.Add("InPatientID", inHosid);
                inParamListDetail.Add("InHosOffice", 0);
                inParamListDetail.Add("StartTime", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
                inParamListDetail.Add("EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                inParamListDetail.Add("StatisticRefund", 0); //统计明细
                inParamListDetail.Add("StatisticExpenseDetail", 0); //统计明细
                inParamListDetail.Add("StatisticLeechdomDetail", 0); //统计明细
                string ShowMx = "0";
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("住院病人费用明细显示明细"))
                {
                    ShowMx = System.Configuration.ConfigurationManager.AppSettings["住院病人费用明细显示明细"];
                }

                if (ShowMx == "1")
                {
                    inParamListDetail.Add("TotalRollExpense", 1); //统计明细
                    inParamListDetail.Add("TotalExpense", 1); //统计明细
                }
                else
                {
                    inParamListDetail.Add("TotalRollExpense", 0); //统计明细
                    inParamListDetail.Add("TotalExpense", 0); //统计明细
                }
                inParamListDetail.Add("ShowZeroExpense", 1);

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
                DataSet dsDetail;

                InpatientSpendDetailFacade detailFacade = new InpatientSpendDetailFacade();
                dsDetail = detailFacade.GetInpatientSpendDetailReport(
                     inParamListDetail["InPatientID"].ToString(),
                     inParamListDetail["InHosOffice"].ToString(),
                     DateTime.Parse(inParamListDetail["StartTime"].ToString()),
                     DateTime.Parse(inParamListDetail["EndTime"].ToString()),
                     int.Parse(inParamListDetail["StatisticRefund"].ToString()),
                     int.Parse(inParamListDetail["StatisticExpenseDetail"].ToString()),
                     int.Parse(inParamListDetail["StatisticLeechdomDetail"].ToString()),
                     int.Parse(inParamListDetail["TotalRollExpense"].ToString()),
                     int.Parse(inParamListDetail["TotalExpense"].ToString()),
                     int.Parse(inParamListDetail["ShowZeroExpense"].ToString()));
                if (dsDetail != null && dsDetail.Tables.Count != 0 && dsDetail.Tables[0].Rows.Count != 0)
                {
                    gdcMain.DataSource = dsDetail.Tables[0];
                }
                else
                {
                    gdcMain.DataSource = null;
                }
                
                return true;
             
            }
            catch (Exception ex)
            {
                LogService.GlobalErrorMessage("获取住院患者费用汇总及明细信息失败，原因：" + ex.Message);

                SkyComm.ShowMessageInfo("无法获取住院患者费用明细信息！");
                return false;
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

        #region 翻页事件
        private void lblLastDay_Click(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            _queryTime = _queryTime.AddDays(-1);
            BindPage(_queryTime.ToString("yyyy-MM-dd"));
        }

        private void lblNextDay_Click(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            _queryTime = _queryTime.AddDays(1);
            BindPage(_queryTime.ToString("yyyy-MM-dd"));
        }
        #endregion
    }
}
