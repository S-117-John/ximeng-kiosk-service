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
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Clinic;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using EntityData.His.CardClubManager;
using EntityData.His.Clinic;
using EntityData.His.ClinicDoctor;
using EntityData.His.Common;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using SystemFramework.NewCommon;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;
using RMReportEngine;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.Charge
{
    public partial class FrmReserveInfo : Form
    {
        #region 自定义变量
        DataTable dtReserveDateList = null; //可预约时段列表 

        public bool isupdatereserve = false;//是否修改预约
        public DateTime reserveOldTime { get; set; }//上次预约时间，用作判断是否跟上一次预约

        public string streserveid = "", stregisterid = "";
        private CLINICMtReserveExitFacade Crexit;//预约中间表BF层
        public string QueueID;
        // 日期控件事件两次执行判断
        bool isSecendClick = false;

        // 医技预约组
        public string GroupID { get; set; }

        // 医技预约组名
        public string GroupName { get; set; }

        public string DiagnoseID { get; set; }

        public string OfficeID { get; set; }

        public string exOfficeName { get; set; }//执行科室

        //public string OpeartionOffice { get; set; }//开单科室

        //预约数据
        public DataTable dtRev { get; set; }

        // 总花费
        public string CostMoney { get; set; }

        public string queueNO { get; set; }//排队号

        public string reserveDate { get; set; }//预约时间


        public string reserveDateNew { get; set; }//预约时间

        private DataSet dsRecipeMedord;

        string allOfficeID = string.Empty;//医技报到时调用PACS接口科室ID

        TMedgroupDetailFacade medGroupDetailFacade;
        private DetailAccountData detailAccountData;
        private DetailAccountFacade detailAccountFacade;
        private EcipeMedicineData ecipeMedicineData;
        private ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade;

        static int userClickRowIndex = 0;//用户选择预约时段的数据//btnReserve_Click内调用外部方法索引会发生变化，所以加此变量
        #endregion

        #region 构造函数
        public FrmReserveInfo()
        {
            InitializeComponent();
        }

        private void FrmReserveInfo_Load(object sender, EventArgs e)
        {
            dsRecipeMedord = new DataSet();
            medGroupDetailFacade = new TMedgroupDetailFacade();
            detailAccountData = new DetailAccountData();
            detailAccountFacade = new DetailAccountFacade();
            ecipeMedicineData = new EcipeMedicineData();
            clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            this.lblGroup.Text = GroupName;
            //wangchao modify 2016-07-29 增加医技预约调用PACS接口科室限制
            if (SystemInfo.SystemConfigs["医技分时段预约调用PACS接口科室限制"].DefaultValue.Trim() != "0")
            {
                allOfficeID = SystemInfo.SystemConfigs["医技分时段预约调用PACS接口科室限制"].Detail.ToString().Trim();
            }
            this.monthCalendar1.SelectDate(new CommonFacade().GetServerDateTime());
            DateTime dtSelect = this.monthCalendar1.SelectedDates[0];
            this.lblDate.Text = dtSelect.Month + "月" + dtSelect.Day + "日 " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dtSelect.DayOfWeek);
            GetDataForReserveDate();
            isSecendClick = false;
        }
        #endregion

        #region 查询预约信息
        private bool SelectItem(string diagnoseid, string OfficeID)
        {
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByExeofficeID(diagnoseid, OfficeID);

            #region MyRegion
            dtRev.Columns.Add("处方号");
            dtRev.Columns.Add("处方内容");
            dtRev.Columns.Add("注意事项");
            dtRev.Columns.Add("预约号");
            dtRev.Columns.Add("诊疗号");
            dtRev.Columns.Add("挂号号");
            dtRev.Columns.Add("科室ID");
            dtRev.Columns.Add("诊室名称");
            dtRev.Columns.Add("执行科室地址");
            dtRev.Columns.Add("医嘱用语备注");
            dtRev.Columns.Add("开单科室");
            dtRev.Columns.Add("检查医师");
            dtRev.Columns.Add("检查医师ID");
            dtRev.Columns.Add("预约日期");
            dtRev.Columns.Add("预约结束日期");
            dtRev.Columns.Add("报到时间");
            dtRev.Columns.Add("完成时间");
            dtRev.Columns.Add("挂起时间");
            dtRev.Columns.Add("操作员ID");
            dtRev.Columns.Add("预约状态");
            dtRev.Columns.Add("检查状态");
            dtRev.Columns.Add("检查目的");  //douyaming 2014-8-4 
            dtRev.Columns.Add("收费状态");  //douyaming 2016-05-14

            #endregion

            dtRev.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                #region 增加医技预约次数限制 wangchao 2017.03.23 case:27593
                //try
                //{
                //    if (SystemInfo.SystemConfigs["医技预约限制次数"].DefaultValue == "1")
                //    {
                //       // string _execOffice = ds.Tables[0].Rows[i]["OFFICEID"].ToString();
                //        string _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                //        string _content = ds.Tables[0].Rows[i]["RECIPECONTENT"].ToString();
                //        CLINICMtReserveFacade _reserveFacade = new CLINICMtReserveFacade();
                //        DataSet dsCheck = _reserveFacade.checkRecipeHasReserveRecord(_diagnoseID, OfficeID, _content);
                //        if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
                //        {
                //            SkyComm.ShowMessageInfo("检查项目【" + ds.Tables[0].Rows[i]["CHARGEITEM"].ToString() + "】在日期【" + Convert.ToDateTime(dsCheck.Tables[0].Rows[0]["RESERVESTARTTIME"].ToString()).ToString("yyyy年MM月dd日") + "】已存在预约信息，不能重复预约！");
                //            return false;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Skynet.LoggingService.LogService.GlobalInfoMessage("医技预约次数限制模块发生异常,原因：" + ex.Message);
                //    return false;
                //}
                #endregion

                DataRow newRow = dtRev.NewRow();
                newRow["处方号"] = ds.Tables[0].Rows[i]["CLINICRECIPEID"].ToString();
                newRow["处方内容"] = ds.Tables[0].Rows[i]["CHARGEITEM"].ToString();
                newRow["注意事项"] = "";
                newRow["预约号"] = "1";
                newRow["诊疗号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                newRow["挂号号"] = ds.Tables[0].Rows[i]["REGISTERID"].ToString();
                newRow["科室ID"] = ds.Tables[0].Rows[i]["EXECOFFICEID"].ToString(); ;// this.cmbExamineName.SelectedValue.ToString(); 
                //newRow["诊室名称"] = this.cmbExamineName.Text; 
                newRow["诊室名称"] = ds.Tables[0].Rows[i]["EXECOFFICE"].ToString();//new
                newRow["开单科室"] = ds.Tables[0].Rows[i]["OPERATOROFFICE"].ToString();
                newRow["执行科室地址"] = ds.Tables[0].Rows[i]["OFFICEADDRESS"].ToString();
                newRow["医嘱用语备注"] = ds.Tables[0].Rows[i]["REMARK"].ToString();
                newRow["检查医师ID"] = "";// this.cmbUserName.SelectedValue;
                newRow["检查医师"] = "";// this.cmbUserName.Text;
                newRow["预约日期"] = "";
                newRow["预约结束日期"] = null;
                newRow["报到时间"] = null;
                newRow["完成时间"] = null;
                newRow["挂起时间"] = null;
                newRow["操作员ID"] = SysOperatorInfo.OperatorID;
                newRow["预约状态"] = 1; //预约
                newRow["检查状态"] = 1; //未检查
                newRow["检查目的"] = "";// this.gridView1.GetDataRow(i)["APPLYREMARK"].ToString(); 
                try
                {
                    var revState = ds.Tables[0].Rows[i]["RECIPESTATE"];
                    newRow["收费状态"] = ds.Tables[0].Rows[i]["RECIPESTATE"] == null ? "" : ds.Tables[0].Rows[i]["RECIPESTATE"].ToString() == "0" ? "未收费" : "已收费";
                }
                catch (Exception)
                {
                    newRow["收费状态"] = "";
                }
                dtRev.Rows.Add(newRow);

            }
            return true;
        }
        #endregion

        #region 选择、关闭事件

        private void lblOK_Click(object sender, EventArgs e)
        {
            string reserveItem = string.Empty;
            decimal ye = 0;
            decimal sumMoney = 0;
            DataSet dsCopy = new DataSet();
            DataSet dsPrint = new DataSet();
            if (isupdatereserve)
            {
                //添加预约修改  smj   2018-08-30
                #region 预约修改
                try
                {
                    #region 预约判断
                    DateTime dtNow = new CommonFacade().GetServerDateTime();//获取服务器时间
                    DataRow drSelectRevData = this.gridViewDate.GetDataRow(gridViewDate.FocusedRowHandle);
                    #region 当天预约患者修改预约当天其他时段，排队号不变，否则重新产生排队号
                    bool isOnlyAlterDate = false;
                    //if (dateTimePicker1.DateTime.Date == dtNow.Date)
                    //    isOnlyAlterDate = true;
                    #endregion
                    if (drSelectRevData == null)
                    {
                        SkyComm.ShowMessageInfo("未选择预约时段，请重新选择！");
                        return;
                    }
                    if (GroupID.Length > 0)
                    {
                        reserveDate = this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["PERIOD"].ToString();
                        userClickRowIndex = gridViewDate.FocusedRowHandle;
                        clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
                        //修改真正的预约日期
                        DateTime dateTimeStart = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["STARTTIME"].ToString());
                        DateTime dateTimeEnd = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["ENDTIME"].ToString());
                        if (reserveOldTime >= dateTimeStart && reserveOldTime < dateTimeEnd) //用户操作所选时间与上次预约时间相同
                        {
                            SkynetMessage.MsgInfo("所选时间与上次预约时间相同,不作修改！");
                            return;
                        }
                        if (DateTime.Compare(dateTimeEnd, dtNow) < 0)
                        {
                            SkynetMessage.MsgInfo("预约时间不能小于当前时间!");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                    #endregion
                    #region  预约
                    DateTime realtime = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["STARTTIME"].ToString()).AddSeconds(1);
                    //返回当天该诊室的排队最大号
                    int iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                    if (iMaxQueueNo == -2)
                    {
                        SkyComm.ShowMessageInfo("获取最大号失败！");
                        return;
                    }
                    else if (iMaxQueueNo == -1)
                    {
                        if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                            SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                        else
                            SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                        return;
                    }
                    bool isConfirm = false;

                    //如果预约时间为当天，则预约并报到
                    bool isReserveAndCheckIn = false;
                    if (this.monthCalendar1.SelectedDates[0].Date == dtNow.Date)
                        isReserveAndCheckIn = true;
                    bool isPrintReport = false;
                    try
                    {
                        //if (SelectItem(DiagnoseID, OfficeID))
                        //{
                            #region MyRegion

                            using (FrmReserveConfirm frm = new FrmReserveConfirm())
                            {
                                frm.ExOfficeName = exOfficeName;

                                foreach (DataRow dr in dtRev.Rows)
                                {
                                    if (string.IsNullOrEmpty(reserveItem))
                                        reserveItem = dr["处方内容"].ToString();
                                    else
                                        reserveItem += "/" + dr["处方内容"].ToString();
                                    string remark = SkyComm.getvalue(dr["处方内容"].ToString());
                                    if (remark != null && remark != "")
                                        reserveItem += "(" + remark + ")";
                                    dr["注意事项"] = remark;
                                }
                                frm.reserveItem = reserveItem;
                                try
                                {
                                    string[] dateTemp = reserveDate.Split(' ')[1].Split('-');
                                    string timeString1 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[0]).ToString("HH:mm");
                                    string timeString2 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[1]).ToString("HH:mm");
                                    reserveDateNew = reserveDate.Split(' ')[0].ToString() + " " + timeString1 + "-" + timeString2;
                                    frm.reserveDate = reserveDateNew;
                                }
                                catch
                                {
                                    frm.reserveDate = reserveDate;
                                }
                               // frm.queueNO = QueueID;
                                frm.CostMoney = CostMoney;
                                frm.reserveGroup = GroupName;
                                frm.ExOfficeID = OfficeID;
                                frm.OfficeName = dtRev.Rows[0]["开单科室"].ToString();
                                frm.OfficeAddress = dtRev.Rows[0]["执行科室地址"].ToString();
                                if (frm.ShowDialog(this) == DialogResult.OK)
                                {
                                    isConfirm = true;
                                }
                                else
                                {
                                    isConfirm = false;
                                }
                            }
                            if (isConfirm)
                            {
                                #region isConfirm=true
                                this.AnsyWorker(ui =>
                                {
                                    ui.UpdateTitle("正在处理中，请稍等...");

                                    ui.SynUpdateUI(() =>
                                    {

                                        //返回当天该诊室的排队最大号
                                        iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                                        if (iMaxQueueNo == -2)
                                        {
                                            SkyComm.ShowMessageInfo("获取最大号失败！");
                                            return;
                                        }
                                        else if (iMaxQueueNo == -1)
                                        {
                                            if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                                                SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                                            else
                                                SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                                            return;
                                        }
                                        try
                                        {
                                            #region 扣费相关
                                            DataSet dsTemp = dsRecipeMedord.Clone();
                                            dsPrint = dsRecipeMedord.Clone();
                                            dsRecipeMedord.Clear();
                                            IEnumerable<string> _ClinicRecipedIDs = dtRev.AsEnumerable().Where(a => a.Field<string>("收费状态") == "未收费").Select(a => a.Field<string>("处方号")).Distinct();
                                            foreach (string ClinicRecipedID in _ClinicRecipedIDs)
                                            {
                                                DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByClinicRecipeId(ClinicRecipedID);
                                                if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return;
                                                DataRow[] drs = ds.Tables[0].Select(" RECIPETYPE <>'医材' and RECIPETYPE <> '附加' ");
                                                if (drs.Length < 1)
                                                {
                                                    SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                    return;
                                                }
                                                if (dsRecipeMedord.Tables.Count == 0)
                                                    dsRecipeMedord.Tables.Add(drs.CopyToDataTable());
                                                else
                                                    dsRecipeMedord.Tables[0].Merge(drs.CopyToDataTable());

                                                if (dsTemp.Tables.Count == 0)
                                                {
                                                    dsTemp = dsRecipeMedord.Clone();
                                                    dsPrint = dsRecipeMedord.Clone();
                                                }
                                                ArrayList checkClinic = new ArrayList();

                                                if (dsRecipeMedord.Tables.Count > 0 && dsRecipeMedord.Tables[0].Rows.Count > 0)
                                                {
                                                    int i = 0;
                                                    foreach (DataRow drMedord in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + ClinicRecipedID + "'"))
                                                    {

                                                        if (FindRecipeState(drMedord["CLINICRECIPEID"].ToString(), drMedord["RECIPELISTNUM"].ToString()) == false)
                                                        {
                                                            SkyComm.ShowMessageInfo("处方" + drMedord["CLINICRECIPEID"].ToString() + "[" + drMedord["CHARGEITEM"].ToString() + "]已计费，不能再计费！");
                                                            return;
                                                        }

                                                        if (checkClinic.IndexOf(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString()) < 0)
                                                        {
                                                            foreach (DataRow dr in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + drMedord["CLINICRECIPEID"].ToString() + "' and RECIPECONTENT = '" + drMedord["RECIPECONTENT"].ToString() + "'"))
                                                            {
                                                                dsTemp.Tables[0].ImportRow(dr);
                                                            }

                                                            checkClinic.Add(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString());
                                                        }

                                                        dsPrint.Tables[0].ImportRow(drMedord);
                                                    }
                                                }
                                                else
                                                {
                                                    SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                    return;
                                                }
                                            }
                                            if (dsTemp.Tables.Count > 0)
                                            {
                                                if (dsTemp.Tables[0].Rows.Count > 0)
                                                {
                                                    #region 调用扣费方法

                                                    #region 扣费
                                                    detailAccountData.Clear();
                                                    ecipeMedicineData.Clear();
                                                    AddRecipeCharge(dsTemp);

                                                    //验证西北妇幼高值耗材
                                                    if (!CheckHValueMaterial(detailAccountData))
                                                    {
                                                        return;
                                                    }

                                                    CardAuthorizationFacade cardAuthorizationFacade = new CardAuthorizationFacade();
                                                    string CardId = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                                                    CardAuthorizationData eCardAuthorizationData = (CardAuthorizationData)cardAuthorizationFacade.SelectPatientAndCardInfoByCardID(CardId);
                                                    string strIsBankCard = "0";
                                                    if (eCardAuthorizationData.Tables[0].Columns.Contains("ISBANKCARD"))
                                                        strIsBankCard = eCardAuthorizationData.Tables[0].Rows[0]["ISBANKCARD"].ToString();
                                                    //decimal ye = 0;
                                                    if (strIsBankCard != "1")
                                                    {
                                                        detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);
                                                        if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
                                                        {
                                                            ye = cardAuthorizationFacade.FindCardBalance(DiagnoseID);
                                                        }
                                                        else
                                                        {
                                                            ye = cardAuthorizationFacade.FindCardBalance_New(DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        #region 银医直联卡调用扣费方法

                                                        //string strCardTypeName = eCardAuthorizationData.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                                                        //string strBankInterFacename = eCardAuthorizationData.Tables[0].Rows[0]["BANKINTEFACENAME"].ToString();
                                                        //Skynet.LoggingService.LogService.GlobalInfoMessage("卡号:" + CardId + ",卡类型：" + strCardTypeName + "，是否银医直联：" + strIsBankCard + ",接口名称" + strBankInterFacename);

                                                        //CommonFacade commonFacade = new CommonFacade();
                                                        //DateTime dateTime = commonFacade.GetServerDateTime();
                                                        //ValidateCode vc = new ValidateCode();
                                                        //string RoundCode = vc.GenValidateCode(4);
                                                        //string strlsh = dateTime.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + RoundCode;
                                                        //foreach (DataRow Row in detailAccountData.Tables[0].Rows)
                                                        //{

                                                        //    Row.BeginEdit();
                                                        //    Row[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = strCardTypeName;//结算方式
                                                        //    Row["ISBANKCARD"] = 1;
                                                        //    Row["BANKTRANSNO"] = strlsh;
                                                        //    Row.EndEdit();
                                                        //}

                                                        //decimal balanMoney = Convert.ToDecimal(detailAccountData.Tables[0].Compute("SUM(SELFMONEY)", ""));
                                                        //Exchange exchange = new Exchange();
                                                        //exchange.Init(strBankInterFacename);
                                                        //try
                                                        //{
                                                        //    Hashtable ht1 = new Hashtable();
                                                        //    ht1.Add("SEQNO", strlsh);
                                                        //    ht1.Add("PATIENTNAME", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                                                        //    ht1.Add("CARDID", CardId);
                                                        //    ht1.Add("CUSTID", CardId);
                                                        //    ht1.Add("MONEY", balanMoney);
                                                        //    ht1.Add("TRANTYPE", "医技");

                                                        //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法输入参数CARDID：" + CardId + ",MONEY：" + balanMoney.ToString());
                                                        //    Hashtable htqdRst = exchange.Trans(BussinessCode.ZZ_ZZ_C2H, ht1);
                                                        //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法返回代码：" + htqdRst["RCODE"].ToString() + ",返回消息：" + htqdRst["RINFO"].ToString());

                                                        //    if (htqdRst["RCODE"].ToString() != "0000")
                                                        //    {
                                                        //        Skynet.LoggingService.LogService.GlobalInfoMessage("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                        //        throw new Exception("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                        //    }
                                                        //}
                                                        //catch (Exception ex)
                                                        //{
                                                        //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费失败：" + ex.Message);
                                                        //    throw new Exception("调用银行扣费错误：" + ex.Message);
                                                        //}

                                                        //try
                                                        //{
                                                        //    detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);
                                                        //    exchange.Confirm(strlsh);

                                                        //}
                                                        //catch (Exception ex)
                                                        //{
                                                        //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费成功，HIS保存失败：" + ex.Message);
                                                        //    exchange.Cancel(strlsh);
                                                        //    throw ex;
                                                        //}
                                                        #endregion
                                                    }
                                                    #endregion

                                                    #region 重组 打印报表时候需要显示医嘱用语而不是明细
                                                    DataTable dtCopy = dsRecipeMedord.Tables[0].Copy();
                                                    DataTable dtMerge = new DataTable();
                                                    //dtMerge.Columns.Add("MEDORDNAME", typeof(string)); //修改为下面这个字段
                                                    dtMerge.Columns.Add("CHARGEITEM", typeof(string));
                                                    dtMerge.Columns.Add("TOTALMONEY", typeof(decimal));
                                                    dtMerge.Columns.Add("AMOUNT", typeof(decimal));
                                                    var query = from t in dtCopy.AsEnumerable()
                                                                group t by new { t1 = t.Field<string>("MEDORDNAME"), t2 = t.Field<string>("CLINICRECIPEID") } into m
                                                                select new
                                                                {
                                                                    MEDORDNAME = m.Key.t1,
                                                                    AMOUNT = 1,
                                                                    TOTALMONEY = m.Sum(n => n.Field<decimal>("TOTALMONEY"))
                                                                };
                                                    var queryNew = from t in query
                                                                   group t by new { t1 = t.MEDORDNAME } into m
                                                                   select new
                                                                   {
                                                                       MEDORDNAME = m.Key.t1,
                                                                       AMOUNT = m.Count(),
                                                                       TOTALMONEY = m.Sum(n => n.TOTALMONEY)
                                                                   };
                                                    queryNew.ToList().ForEach(q =>
                                                    {
                                                        dtMerge.Rows.Add(q.MEDORDNAME, q.TOTALMONEY, q.AMOUNT);
                                                    });

                                                    dsCopy.Tables.Add(dtMerge);
                                                    #endregion

                                                    if (dsCopy.Tables[0].Rows.Count > 0)
                                                    {
                                                        sumMoney = ReturnTotalMoney(dsCopy);
                                                    }

                                                    //打印医技扣费凭证
                                                    isPrintReport = true;
                                                    #endregion
                                                }
                                            }
                                            #endregion

                                        }
                                        catch (Exception ex)
                                        {
                                            SkyComm.ShowMessageInfo("扣费失败！原因：" + ex.Message);
                                            return;
                                        }

                                        CLINICMtReserveData resEntity = new CLINICMtReserveData();
                                        resEntity.Reserveid = streserveid;
                                        #region resEntity赋值
                                        resEntity.ResverveType = "门诊";
                                        resEntity.Diagnoseid = DiagnoseID;
                                        resEntity.Registerid = stregisterid;
                                        resEntity.Officeid = OfficeID; //this.lookUpEditGroupInfo.EditValue.ToString();
                                        resEntity.Examinename = GroupName; //this.lookUpEditGroupInfo.Text; //2016年7月2日 16:53:07
                                        resEntity.Checkdoctorid = "0";
                                        resEntity.Reservestarttime = realtime;// Convert.ToDateTime(dtRev.Rows[0]["预约日期"]);//时间段预约
                                        if (Convert.IsDBNull(dtRev.Rows[0]["预约结束日期"]))
                                        {
                                            //预约结束时间(修改为实际的预约操作时间)
                                            resEntity.Reserveendtime = dtNow;
                                        }
                                        else
                                        {
                                            resEntity.Reserveendtime = Convert.ToDateTime(dtRev.Rows[0]["预约结束日期"]);
                                        }
                                        if (Convert.IsDBNull(dtRev.Rows[0]["报到时间"]))
                                        {
                                            resEntity.Checkintimes = null;
                                        }
                                        else
                                        {
                                            resEntity.Checkintimes = Convert.ToDateTime(dtRev.Rows[0]["报到时间"]);
                                        }
                                        if (Convert.IsDBNull(dtRev.Rows[0]["完成时间"]))
                                        {
                                            resEntity.Completiontime = null;
                                        }
                                        else
                                        {
                                            resEntity.Completiontime = Convert.ToDateTime(dtRev.Rows[0]["完成时间"]);
                                        }
                                        if (Convert.IsDBNull(dtRev.Rows[0]["挂起时间"]))
                                        {
                                            dtRev.Rows[0]["挂起时间"] = null;
                                        }
                                        else
                                        {
                                            resEntity.Suspendtime = Convert.ToDateTime(dtRev.Rows[0]["挂起时间"]);
                                        }
                                        resEntity.Operatorid = dtRev.Rows[0]["操作员ID"].ToString();
                                        resEntity.Resstatus = Convert.ToInt32(dtRev.Rows[0]["预约状态"].ToString());
                                        if (isReserveAndCheckIn)
                                        {
                                            resEntity.Resstatus = 2;//预约并报到
                                            resEntity.Checkdoctorid = "0";//按组报到，无法知道检查医师
                                            resEntity.Checkintimes = dtNow;
                                        }
                                        resEntity.Checkstatus = Convert.ToInt32(dtRev.Rows[0]["检查状态"].ToString());
                                        #endregion
                                        string strReseverID = "0";   //预约号
                                        int strQueue = 0;  //排队号
                                        #region 添加中间表改造修改预约流程 2017-08-29  smj
                                        //中间表插入数据CLINICMtReserveExitData
                                        CLINICMtQueueFacade queueFacade = new CLINICMtQueueFacade();
                                        CLINICMtReserveFacade resFacade = new CLINICMtReserveFacade();


                                        CLINICMtQueueData queue = new CLINICMtQueueData();
                                        queue = queueFacade.GetByReserveID(streserveid);
                                        CLINICMtReserveExitData Cresexit = new CLINICMtReserveExitData();
                                        CLINICMtReserveExitFacade Cresexitfacade = new CLINICMtReserveExitFacade();
                                        Cresexit.Exitid = System.Guid.NewGuid().ToString();
                                        Cresexit.Exitgroupdtailid = queue.Queuegroupdtailid.ToString();
                                        Cresexit.Releasenum = queue.Queueno.ToString();
                                        Cresexit.Releasetime = dtNow;
                                        Cresexit.Resstatus = 0;
                                        Cresexit.Remark = "";
                                        Cresexit.Exitnooff = queue.Exitnooff.ToString();

                                        int resexitid = Cresexitfacade.Insert(Cresexit);
                                        //根据预约号修改预约表
                                        int n0 = resFacade.Update(resEntity);

                                        #region 更新医技预约组排班明细表的预约数据
                                        //更新医技预约组排班明细表的预约数据
                                        bool isOnLineMin = false;
                                        if (queue.Exitnooff == "0")
                                        {
                                            isOnLineMin = true;
                                        }
                                        int sd = medGroupDetailFacade.updateReserveNoMiddleMin(Convert.ToInt32(queue.Queuegroupdtailid.ToString()), isOnLineMin);
                                        #endregion
                                        Crexit = new CLINICMtReserveExitFacade();
                                        CLINICMtQueueData queueEntity = new CLINICMtQueueData();
                                        DataSet dsExit = Crexit.FindReserveExitInfo(drSelectRevData["MEDGROUPRDETAILID"].ToString());
                                        bool isOnLine = this.monthCalendar1.SelectedDates[0].Date > dtNow.Date ? false : true;
                                        if (dsExit.Tables[0].Rows.Count <= 0)//判断中间层是否有数据
                                        {
                                            #region 排队表实体---中间表无数据

                                            int isOnlyAlterDateQueryNo = 0;//
                                            if (isOnlyAlterDate)
                                            {
                                                #region MyRegion
                                                try
                                                {
                                                    queueEntity = queueFacade.GetByReserveID(streserveid);
                                                    isOnlyAlterDateQueryNo = queueEntity.Queueno;//获取之前该患者的排队号；
                                                }
                                                catch
                                                {
                                                    isOnlyAlterDateQueryNo = 0;
                                                }
                                                #endregion
                                            }
                                            queueFacade.MyDelete(streserveid);//删除该预约号之前排队的数据

                                            queueEntity.Queueid = "1";
                                            queueEntity.Reserveid = streserveid;
                                            queueEntity.Priority = 1;
                                            queueEntity.QueueDate = realtime;
                                            strQueue = iMaxQueueNo + 1;
                                            queueEntity.Queueno = strQueue;
                                            if (isOnlyAlterDate)
                                                queueEntity.Queueno = isOnlyAlterDateQueryNo == 0 ? strQueue : isOnlyAlterDateQueryNo;

                                            queueEntity.Queuegroupdtailid = drSelectRevData["MEDGROUPRDETAILID"].ToString();
                                            if (isOnLine)
                                            {
                                                queueEntity.Exitnooff = "0";
                                            }
                                            else
                                            {
                                                queueEntity.Exitnooff = "1";
                                            }


                                            #endregion
                                            //更新医技预约组排班明细表的最大号
                                            if (!isOnlyAlterDate)
                                            {
                                                int updateRtn = medGroupDetailFacade.updateMaxReserveNo((int)drSelectRevData["MEDGROUPRDETAILID"], isOnLine);
                                                if (updateRtn < 1)
                                                {
                                                    SkynetMessage.MsgError("更新医技预约组排班明细表的最大号失败！");
                                                    return;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            #region 排队表实体--中间表有数据

                                            int isOnlyAlterDateQueryNo = 0;//
                                            if (isOnlyAlterDate)
                                            {
                                                #region MyRegion
                                                try
                                                {
                                                    queueEntity = queueFacade.GetByReserveID(streserveid);
                                                    isOnlyAlterDateQueryNo = queueEntity.Queueno;//获取之前该患者的排队号；
                                                }
                                                catch
                                                {
                                                    isOnlyAlterDateQueryNo = 0;
                                                }
                                                #endregion
                                            }
                                            queueFacade.MyDelete(streserveid);//删除该预约号之前排队的数据

                                            queueEntity.Queueid = "1";
                                            queueEntity.Reserveid = streserveid;
                                            queueEntity.Priority = 1;
                                            queueEntity.QueueDate = realtime;
                                            strQueue = Convert.ToInt32(dsExit.Tables[0].Rows[0]["RELEASENUM"].ToString());
                                            queueEntity.Queueno = strQueue;
                                            if (isOnlyAlterDate)
                                                queueEntity.Queueno = isOnlyAlterDateQueryNo == 0 ? strQueue : isOnlyAlterDateQueryNo;
                                            queueEntity.Queuegroupdtailid = drSelectRevData["MEDGROUPRDETAILID"].ToString();
                                            string isoff = dsExit.Tables[0].Rows[0]["EXITNOOFF"].ToString();
                                            queueEntity.Exitnooff = isoff;

                                            #endregion
                                            //更新医技预约组排班明细表的最大号
                                            if (!isOnlyAlterDate)
                                            {
                                                bool isofline = false;
                                                if (isoff == "0")
                                                {
                                                    isofline = true;//在线
                                                }
                                                int updateRtn = medGroupDetailFacade.updateMaxReserveNoMiddle((int)drSelectRevData["MEDGROUPRDETAILID"], isofline);

                                                if (updateRtn < 1)
                                                {
                                                    SkynetMessage.MsgError("更新医技预约组排班明细表的最大号失败！");
                                                    return;
                                                }
                                            }
                                            int deleteid = Crexit.Delete(dsExit.Tables[0].Rows[0]["EXITID"].ToString());//删除中间表数据
                                        }
                                        QueueID = queueEntity.Queueno.ToString();
                                        int nn = queueFacade.Insert(queueEntity);//重新插入排队数据
                                        #endregion

                                        //预约处方表实体泛型集合
                                        EntityList<CLINICReseverRecipeData> resRecipeLst = new EntityList<CLINICReseverRecipeData>();
                                        //存放处方号
                                        List<string> myLst = new List<string>();
                                        myLst.Clear();
                                        for (int i = 0; i < dtRev.Rows.Count; i++)
                                        {//douyaming 2013-9-2 CASE:15238
                                            if (!myLst.Contains(dtRev.Rows[i]["处方号"].ToString()))
                                            {
                                                myLst.Add(dtRev.Rows[i]["处方号"].ToString());
                                            }
                                            dtRev.Rows[i]["收费状态"] = "已收费";
                                        }
                                        //去掉处方号相同的记录
                                        DataTable dtTemp = dtRev.Copy();
                                        var dataTemp = dtTemp.AsEnumerable();
                                        var result = (from n in dataTemp
                                                      select n).Distinct(new MyComparer2());
                                        dtTemp = result.CopyToDataTable();
                                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                                        {
                                            CLINICReseverRecipeData entity = new CLINICReseverRecipeData();
                                            entity.Clinicrecipeid = dtTemp.Rows[i]["处方号"].ToString();
                                            entity.Reserveid = strReseverID;
                                            resRecipeLst.Add(entity);
                                            // myLst.Add(entity.Clinicrecipeid);
                                        }
                                        //预约并报到
                                        if (isReserveAndCheckIn)
                                        {
                                            //从处方表移除已预约(报到)的处方
                                            DataRow[] drAll = this.dtRev.Select("1=1");
                                            if (drAll.Length > 0)
                                            {
                                                bool exFlag = false;
                                                string exOfficeID = resEntity.Officeid;
                                                foreach (string singleOfficeID in allOfficeID.Split('|'))
                                                {
                                                    if (singleOfficeID == exOfficeID)
                                                    {
                                                        exFlag = true;
                                                        break;
                                                    }
                                                }
                                                if (exFlag)
                                                {
                                                    SendExamApp(resEntity, drAll);
                                                }
                                            }
                                        }

                                        PrintAll(ye, sumMoney, dsCopy, reserveItem);
                                        DialogResult = DialogResult.OK;
                                    });
                                });
                                #endregion
                            }
                            #endregion
                       // }

                    }
                    catch (Exception ex)
                    {
                        SkyComm.ShowMessageInfo(ex.Message);
                        return;
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    SkyComm.ShowMessageInfo("预约修改失败：" + ex.Message);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("预约修改失败原因：" + ex.Message);
                }
                #endregion
            }
            else
            {
                //预约  smj   2018-08-30
                DataRow drSelectRevData = this.gridViewDate.GetDataRow(gridViewDate.FocusedRowHandle);
                Crexit = new CLINICMtReserveExitFacade();
                CLINICMtQueueData queueEntity = new CLINICMtQueueData();
                DataSet dsExit = Crexit.FindReserveExitInfo(drSelectRevData["MEDGROUPRDETAILID"].ToString());
                if (dsExit.Tables[0].Rows.Count <= 0)//判断中间层是否有数据
                {
                    //中间表无数据
                    #region  预约
                    if (drSelectRevData == null)
                    {
                        SkyComm.ShowMessageInfo("未选择预约时段，请重新选择！");
                        return;
                    }
                    reserveDate = this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["PERIOD"].ToString();
                    userClickRowIndex = gridViewDate.FocusedRowHandle;
                    clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
                    //修改真正的预约日期
                    DateTime realtime = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["STARTTIME"].ToString()).AddSeconds(1);
                    DataTable dttemp = dtRev;
                    bool isPrintReport = false;
                    try
                    {
                        #region MyRegion
                        if (this.dtRev.Rows.Count <= 0) return;
                        DateTime dtNow = new CommonFacade().GetServerDateTime();
                        if (GroupID.Length > 0)
                        {
                            DateTime revDate = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["ENDTIME"].ToString());

                            if (DateTime.Compare(revDate, dtNow) < 0)
                            {
                                SkyComm.ShowMessageInfo("预约时间不能小于当前时间!");
                                return;
                            }
                        }
                        else return;
                        //返回当天该诊室的排队最大号
                        int iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                        if (iMaxQueueNo == -2)
                        {
                            SkyComm.ShowMessageInfo("获取最大号失败！");
                            return;
                        }
                        else if (iMaxQueueNo == -1)
                        {
                            if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                                SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                            else
                                SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                            return;
                        }
                        bool isConfirm = false;

                        //如果预约时间为当天，则预约并报到
                        bool isReserveAndCheckIn = false;
                        if (this.monthCalendar1.SelectedDates[0].Date == dtNow.Date)
                            isReserveAndCheckIn = true;

                        using (FrmReserveConfirm frm = new FrmReserveConfirm())
                        {
                            frm.ExOfficeName = exOfficeName;

                            foreach (DataRow dr in dtRev.Rows)
                            {
                                if (string.IsNullOrEmpty(reserveItem))
                                    reserveItem = dr["处方内容"].ToString();
                                else
                                    reserveItem += "/" + dr["处方内容"].ToString();
                                string remark = SkyComm.getvalue(dr["处方内容"].ToString());
                                if (remark != null && remark != "")
                                    reserveItem += "(" + remark + ")";
                                dr["注意事项"] = remark;
                            }
                            frm.reserveItem = reserveItem;
                            try
                            {
                                string[] dateTemp = reserveDate.Split(' ')[1].Split('-');
                                string timeString1 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[0]).ToString("HH:mm");
                                string timeString2 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[1]).ToString("HH:mm");
                                reserveDateNew = reserveDate.Split(' ')[0].ToString() + " " + timeString1 + "-" + timeString2;
                                frm.reserveDate = reserveDateNew;
                            }
                            catch
                            {
                                frm.reserveDate = reserveDate;
                            }
                            //frm.queueNO = Convert.ToString(iMaxQueueNo + 1);
                            frm.CostMoney = CostMoney;
                            frm.reserveGroup = GroupName;
                            frm.ExOfficeID = OfficeID;
                            frm.OfficeName = dtRev.Rows[0]["开单科室"].ToString();
                            frm.OfficeAddress = dtRev.Rows[0]["执行科室地址"].ToString();
                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                isConfirm = true;
                            }
                            else
                            {
                                isConfirm = false;
                            }
                        }
                        if (isConfirm)
                        {
                            this.AnsyWorker(ui =>
                            {
                                ui.UpdateTitle("正在处理中，请稍等...");

                                ui.SynUpdateUI(() =>
                                {

                                    //返回当天该诊室的排队最大号
                                    iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                                    if (iMaxQueueNo == -2)
                                    {
                                        SkyComm.ShowMessageInfo("获取最大号失败！");
                                        return;
                                    }
                                    else if (iMaxQueueNo == -1)
                                    {
                                        if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                                            SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                                        else
                                            SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                                        return;
                                    }
                                    try
                                    {
                                        DataSet dsTemp = dsRecipeMedord.Clone();
                                        dsPrint = dsRecipeMedord.Clone();
                                        dsRecipeMedord.Clear();
                                        IEnumerable<string> _ClinicRecipedIDs = dtRev.AsEnumerable().Where(a => a.Field<string>("收费状态") == "未收费").Select(a => a.Field<string>("处方号")).Distinct();
                                        foreach (string ClinicRecipedID in _ClinicRecipedIDs)
                                        {
                                            DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByClinicRecipeId(ClinicRecipedID);
                                            if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return;
                                            DataRow[] drs = ds.Tables[0].Select(" RECIPETYPE <>'医材' and RECIPETYPE <> '附加' ");
                                            if (drs.Length < 1)
                                            {
                                                SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                return;
                                            }
                                            if (dsRecipeMedord.Tables.Count == 0)
                                                dsRecipeMedord.Tables.Add(drs.CopyToDataTable());
                                            else
                                                dsRecipeMedord.Tables[0].Merge(drs.CopyToDataTable());

                                            if (dsTemp.Tables.Count == 0)
                                            {
                                                dsTemp = dsRecipeMedord.Clone();
                                                dsPrint = dsRecipeMedord.Clone();
                                            }
                                            ArrayList checkClinic = new ArrayList();

                                            if (dsRecipeMedord.Tables.Count > 0 && dsRecipeMedord.Tables[0].Rows.Count > 0)
                                            {
                                                int i = 0;
                                                foreach (DataRow drMedord in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + ClinicRecipedID + "'"))
                                                {

                                                    if (FindRecipeState(drMedord["CLINICRECIPEID"].ToString(), drMedord["RECIPELISTNUM"].ToString()) == false)
                                                    {
                                                        SkyComm.ShowMessageInfo("处方" + drMedord["CLINICRECIPEID"].ToString() + "[" + drMedord["CHARGEITEM"].ToString() + "]已计费，不能再计费！");
                                                        return;
                                                    }

                                                    if (checkClinic.IndexOf(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString()) < 0)
                                                    {
                                                        foreach (DataRow dr in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + drMedord["CLINICRECIPEID"].ToString() + "' and RECIPECONTENT = '" + drMedord["RECIPECONTENT"].ToString() + "'"))
                                                        {
                                                            dsTemp.Tables[0].ImportRow(dr);
                                                        }

                                                        checkClinic.Add(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString());
                                                    }

                                                    dsPrint.Tables[0].ImportRow(drMedord);
                                                }
                                            }
                                            else
                                            {
                                                SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                return;
                                            }
                                        }
                                        if (dsTemp.Tables.Count > 0)
                                        {
                                            if (dsTemp.Tables[0].Rows.Count > 0)
                                            {
                                                #region 调用扣费方法

                                                #region 扣费
                                                detailAccountData.Clear();
                                                ecipeMedicineData.Clear();
                                                AddRecipeCharge(dsTemp);

                                                //验证西北妇幼高值耗材
                                                if (!CheckHValueMaterial(detailAccountData))
                                                {
                                                    return;
                                                }

                                                CardAuthorizationFacade cardAuthorizationFacade = new CardAuthorizationFacade();
                                                string CardId = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                                                CardAuthorizationData eCardAuthorizationData = (CardAuthorizationData)cardAuthorizationFacade.SelectPatientAndCardInfoByCardID(CardId);
                                                string strIsBankCard = "0";
                                                if (eCardAuthorizationData.Tables[0].Columns.Contains("ISBANKCARD"))
                                                    strIsBankCard = eCardAuthorizationData.Tables[0].Rows[0]["ISBANKCARD"].ToString();
                                                //decimal ye = 0;
                                                if (strIsBankCard != "1")
                                                {
                                                    detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);
                                                    if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
                                                    {
                                                        ye = cardAuthorizationFacade.FindCardBalance(DiagnoseID);
                                                    }
                                                    else
                                                    {
                                                        ye = cardAuthorizationFacade.FindCardBalance_New(DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    #region 银医直联卡调用扣费方法

                                                    //string strCardTypeName = eCardAuthorizationData.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                                                    //string strBankInterFacename = eCardAuthorizationData.Tables[0].Rows[0]["BANKINTEFACENAME"].ToString();
                                                    //Skynet.LoggingService.LogService.GlobalInfoMessage("卡号:" + CardId + ",卡类型：" + strCardTypeName + "，是否银医直联：" + strIsBankCard + ",接口名称" + strBankInterFacename);

                                                    //CommonFacade commonFacade = new CommonFacade();
                                                    //DateTime dateTime = commonFacade.GetServerDateTime();
                                                    //ValidateCode vc = new ValidateCode();
                                                    //string RoundCode = vc.GenValidateCode(4);
                                                    //string strlsh = dateTime.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + RoundCode;
                                                    //foreach (DataRow Row in detailAccountData.Tables[0].Rows)
                                                    //{

                                                    //    Row.BeginEdit();
                                                    //    Row[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = strCardTypeName;//结算方式
                                                    //    Row["ISBANKCARD"] = 1;
                                                    //    Row["BANKTRANSNO"] = strlsh;
                                                    //    Row.EndEdit();
                                                    //}

                                                    //decimal balanMoney = Convert.ToDecimal(detailAccountData.Tables[0].Compute("SUM(SELFMONEY)", ""));
                                                    //Exchange exchange = new Exchange();
                                                    //exchange.Init(strBankInterFacename);
                                                    //try
                                                    //{
                                                    //    Hashtable ht1 = new Hashtable();
                                                    //    ht1.Add("SEQNO", strlsh);
                                                    //    ht1.Add("PATIENTNAME", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                                                    //    ht1.Add("CARDID", CardId);
                                                    //    ht1.Add("CUSTID", CardId);
                                                    //    ht1.Add("MONEY", balanMoney);
                                                    //    ht1.Add("TRANTYPE", "医技");

                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法输入参数CARDID：" + CardId + ",MONEY：" + balanMoney.ToString());
                                                    //    Hashtable htqdRst = exchange.Trans(BussinessCode.ZZ_ZZ_C2H, ht1);
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法返回代码：" + htqdRst["RCODE"].ToString() + ",返回消息：" + htqdRst["RINFO"].ToString());

                                                    //    if (htqdRst["RCODE"].ToString() != "0000")
                                                    //    {
                                                    //        Skynet.LoggingService.LogService.GlobalInfoMessage("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                    //        throw new Exception("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                    //    }
                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费失败：" + ex.Message);
                                                    //    throw new Exception("调用银行扣费错误：" + ex.Message);
                                                    //}

                                                    //try
                                                    //{
                                                    //    detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);
                                                    //    exchange.Confirm(strlsh);

                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费成功，HIS保存失败：" + ex.Message);
                                                    //    exchange.Cancel(strlsh);
                                                    //    throw ex;
                                                    //}
                                                    #endregion
                                                }
                                                #endregion

                                                #region 重组 打印报表时候需要显示医嘱用语而不是明细
                                                DataTable dtCopy = dsRecipeMedord.Tables[0].Copy();
                                                DataTable dtMerge = new DataTable();
                                                //dtMerge.Columns.Add("MEDORDNAME", typeof(string)); //修改为下面这个字段
                                                dtMerge.Columns.Add("CHARGEITEM", typeof(string));
                                                dtMerge.Columns.Add("TOTALMONEY", typeof(decimal));
                                                dtMerge.Columns.Add("AMOUNT", typeof(decimal));
                                                var query = from t in dtCopy.AsEnumerable()
                                                            group t by new { t1 = t.Field<string>("MEDORDNAME"), t2 = t.Field<string>("CLINICRECIPEID") } into m
                                                            select new
                                                            {
                                                                MEDORDNAME = m.Key.t1,
                                                                AMOUNT = 1,
                                                                TOTALMONEY = m.Sum(n => n.Field<decimal>("TOTALMONEY"))
                                                            };
                                                var queryNew = from t in query
                                                               group t by new { t1 = t.MEDORDNAME } into m
                                                               select new
                                                               {
                                                                   MEDORDNAME = m.Key.t1,
                                                                   AMOUNT = m.Count(),
                                                                   TOTALMONEY = m.Sum(n => n.TOTALMONEY)
                                                               };
                                                queryNew.ToList().ForEach(q =>
                                                {
                                                    dtMerge.Rows.Add(q.MEDORDNAME, q.TOTALMONEY, q.AMOUNT);
                                                });

                                                dsCopy.Tables.Add(dtMerge);
                                                #endregion

                                                if (dsCopy.Tables[0].Rows.Count > 0)
                                                {
                                                    sumMoney = ReturnTotalMoney(dsCopy);
                                                }

                                                //打印医技扣费凭证
                                                isPrintReport = true;
                                                #endregion
                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        SkyComm.ShowMessageInfo("扣费失败！原因：" + ex.Message);
                                        return;
                                    }

                                    CLINICMtReserveData resEntity = new CLINICMtReserveData();
                                    resEntity.Reserveid = dtRev.Rows[0]["预约号"].ToString();
                                    #region resEntity赋值
                                    resEntity.ResverveType = "门诊";
                                    resEntity.Diagnoseid = DiagnoseID;
                                    resEntity.Registerid = dtRev.Rows[0]["挂号号"].ToString();
                                    resEntity.Officeid = OfficeID; //this.lookUpEditGroupInfo.EditValue.ToString();
                                    resEntity.Examinename = GroupName; //this.lookUpEditGroupInfo.Text; //2016年7月2日 16:53:07
                                    resEntity.Checkdoctorid = "0";
                                    resEntity.Reservestarttime = realtime;// Convert.ToDateTime(dtRev.Rows[0]["预约日期"]);//时间段预约
                                    if (Convert.IsDBNull(dtRev.Rows[0]["预约结束日期"]))
                                    {
                                        //预约结束时间(修改为实际的预约操作时间)
                                        resEntity.Reserveendtime = dtNow;
                                    }
                                    else
                                    {
                                        resEntity.Reserveendtime = Convert.ToDateTime(dtRev.Rows[0]["预约结束日期"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["报到时间"]))
                                    {
                                        resEntity.Checkintimes = null;
                                    }
                                    else
                                    {
                                        resEntity.Checkintimes = Convert.ToDateTime(dtRev.Rows[0]["报到时间"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["完成时间"]))
                                    {
                                        resEntity.Completiontime = null;
                                    }
                                    else
                                    {
                                        resEntity.Completiontime = Convert.ToDateTime(dtRev.Rows[0]["完成时间"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["挂起时间"]))
                                    {
                                        dtRev.Rows[0]["挂起时间"] = null;
                                    }
                                    else
                                    {
                                        resEntity.Suspendtime = Convert.ToDateTime(dtRev.Rows[0]["挂起时间"]);
                                    }
                                    resEntity.Operatorid = dtRev.Rows[0]["操作员ID"].ToString();
                                    resEntity.Resstatus = Convert.ToInt32(dtRev.Rows[0]["预约状态"].ToString());
                                    if (isReserveAndCheckIn)
                                    {
                                        resEntity.Resstatus = 2;//预约并报到
                                        resEntity.Checkdoctorid = "0";//按组报到，无法知道检查医师
                                        resEntity.Checkintimes = dtNow;
                                    }
                                    resEntity.Checkstatus = Convert.ToInt32(dtRev.Rows[0]["检查状态"].ToString());
                                    #endregion

                                    string strReseverID = "0";   //预约号
                                    CLINICMtReserveFacade resFacade = new CLINICMtReserveFacade();
                                    //插入医技预约表
                                    strReseverID = resFacade.MyInsert(resEntity);
                                    resEntity.Reserveid = strReseverID;

                                    //预约处方表实体泛型集合
                                    EntityList<CLINICReseverRecipeData> resRecipeLst = new EntityList<CLINICReseverRecipeData>();
                                    //存放处方号
                                    List<string> myLst = new List<string>();
                                    myLst.Clear();
                                    for (int i = 0; i < dtRev.Rows.Count; i++)
                                    {//douyaming 2013-9-2 CASE:15238
                                        if (!myLst.Contains(dtRev.Rows[i]["处方号"].ToString()))
                                        {
                                            myLst.Add(dtRev.Rows[i]["处方号"].ToString());
                                        }
                                        dtRev.Rows[i]["收费状态"] = "已收费";
                                    }
                                    //去掉处方号相同的记录
                                    DataTable dtTemp = dtRev.Copy();
                                    var dataTemp = dtTemp.AsEnumerable();
                                    var result = (from n in dataTemp
                                                  select n).Distinct(new MyComparer2());
                                    dtTemp = result.CopyToDataTable();
                                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                                    {
                                        CLINICReseverRecipeData entity = new CLINICReseverRecipeData();
                                        entity.Clinicrecipeid = dtTemp.Rows[i]["处方号"].ToString();
                                        entity.Reserveid = strReseverID;
                                        resRecipeLst.Add(entity);
                                        // myLst.Add(entity.Clinicrecipeid);
                                    }

                                    CLINICReseverRecipeFacade resRecipeFacade = new CLINICReseverRecipeFacade();
                                    //插入预约处方表
                                    resRecipeFacade.Insert(resRecipeLst, null);

                                    //更新医技预约组排班明细表的最大号
                                    bool isOnLine = this.monthCalendar1.SelectedDates[0].Date > dtNow.Date ? false : true;
                                    iMaxQueueNo = medGroupDetailFacade.updateMaxReserveNoNew((int)drSelectRevData["MEDGROUPRDETAILID"], isOnLine);

                                    //排队表实体
                                
                                    queueEntity.QueueDate = realtime;
                                    queueEntity.Queueid = "1";
                                    queueEntity.Reserveid = strReseverID;
                                    queueEntity.Priority = 1;
                                    int strQueue = iMaxQueueNo;  //排队号
                                    queueEntity.Queueno = strQueue;
                                    queueEntity.Queueno = strQueue;
                                    queueEntity.Queuegroupdtailid = drSelectRevData["MEDGROUPRDETAILID"].ToString();
                                    if (isOnLine)
                                    {
                                        queueEntity.Exitnooff = "0";
                                    }
                                    else
                                    {
                                        queueEntity.Exitnooff = "1";
                                    }
                                    CLINICMtQueueFacade queueFacade = new CLINICMtQueueFacade();
                                    QueueID = strQueue.ToString();
                                    queueFacade.Insert(queueEntity);
                                    queueNO = strQueue.ToString();//回传排队号
                                    //更新处方表预约状态字段
                                    ClinicPhysicianRecipeFacade recipeFacade = new ClinicPhysicianRecipeFacade();
                                    for (int i = 0; i < myLst.Count; i++)
                                    {
                                        int n = recipeFacade.UpdateByClinicRecipeID(myLst[i], OfficeID, false, "门诊", dtRev.Rows[0]["挂号号"].ToString());
                                    }



                                    //预约并报到
                                    if (isReserveAndCheckIn)
                                    {
                                        //从处方表移除已预约(报到)的处方
                                        DataRow[] drAll = this.dtRev.Select("1=1");
                                        if (drAll.Length > 0)
                                        {
                                            bool exFlag = false;
                                            string exOfficeID = resEntity.Officeid;
                                            foreach (string singleOfficeID in allOfficeID.Split('|'))
                                            {
                                                if (singleOfficeID == exOfficeID)
                                                {
                                                    exFlag = true;
                                                    break;
                                                }
                                            }
                                            if (exFlag)
                                            {
                                                SendExamApp(resEntity, drAll);
                                            }
                                            //SendExamApp(resEntity, drAll);
                                        }
                                    }
                                    //wangchao modify 移除单个打印，增加全部打印
                                    //if (isPrintReport)
                                    //{
                                    //    PrintReport(ye, sumMoney, dsCopy);
                                    //}
                                    //PrintPDPZ(reserveItem);
                                    PrintAll(ye, sumMoney, dsCopy, reserveItem);


                                    DialogResult = DialogResult.OK;
                        #endregion


                                });
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        SkyComm.ShowMessageInfo(ex.Message);
                        return;
                    }

                    #endregion
                }
                else
                {
                    //中间表有数据
                    #region  预约
                    string queueID = dsExit.Tables[0].Rows[0]["RELEASENUM"].ToString();//释放号表排队号

                    if (drSelectRevData == null)
                    {
                        SkyComm.ShowMessageInfo("未选择预约时段，请重新选择！");
                        return;
                    }
                    reserveDate = this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["PERIOD"].ToString();
                    userClickRowIndex = gridViewDate.FocusedRowHandle;
                    clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
                    //修改真正的预约日期
                    DateTime realtime = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["STARTTIME"].ToString()).AddSeconds(1);
                    DataTable dttemp = dtRev;
                    bool isPrintReport = false;
                    try
                    {
                        #region MyRegion
                        #region 预约判断
                        if (this.dtRev.Rows.Count <= 0) return;
                        DateTime dtNow = new CommonFacade().GetServerDateTime();
                        if (GroupID.Length > 0)
                        {
                            DateTime revDate = DateTime.Parse(this.monthCalendar1.SelectedDates[0].ToShortDateString() + " " + drSelectRevData["ENDTIME"].ToString());

                            if (DateTime.Compare(revDate, dtNow) < 0)
                            {
                                SkyComm.ShowMessageInfo("预约时间不能小于当前时间!");
                                return;
                            }
                        }
                        else return;
                        //返回当天该诊室的排队最大号
                        int iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                        if (iMaxQueueNo == -2)
                        {
                            SkyComm.ShowMessageInfo("获取最大号失败！");
                            return;
                        }
                        else if (iMaxQueueNo == -1)
                        {
                            if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                                SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                            else
                                SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                            return;
                        }
                        #endregion
                        bool isConfirm = false;

                        //如果预约时间为当天，则预约并报到
                        bool isReserveAndCheckIn = false;
                        if (this.monthCalendar1.SelectedDates[0].Date == dtNow.Date)
                            isReserveAndCheckIn = true;

                        using (FrmReserveConfirm frm = new FrmReserveConfirm())
                        {
                            #region  FrmReserveConfirm传值判断
                            frm.ExOfficeName = exOfficeName;

                            foreach (DataRow dr in dtRev.Rows)
                            {
                                if (string.IsNullOrEmpty(reserveItem))
                                    reserveItem = dr["处方内容"].ToString();
                                else
                                    reserveItem += "/" + dr["处方内容"].ToString();
                                string remark = SkyComm.getvalue(dr["处方内容"].ToString());
                                if (remark != null && remark != "")
                                    reserveItem += "(" + remark + ")";
                                dr["注意事项"] = remark;
                            }
                            frm.reserveItem = reserveItem;
                            try
                            {
                                string[] dateTemp = reserveDate.Split(' ')[1].Split('-');
                                string timeString1 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[0]).ToString("HH:mm");
                                string timeString2 = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dateTemp[1]).ToString("HH:mm");
                                reserveDateNew = reserveDate.Split(' ')[0].ToString() + " " + timeString1 + "-" + timeString2;
                                frm.reserveDate = reserveDateNew;
                            }
                            catch
                            {
                                frm.reserveDate = reserveDate;
                            }
                            //frm.queueNO = Convert.ToString(iMaxQueueNo + 1);
                            frm.CostMoney = CostMoney;
                            frm.reserveGroup = GroupName;
                            frm.ExOfficeID = OfficeID;
                            frm.OfficeName = dtRev.Rows[0]["开单科室"].ToString();
                            frm.OfficeAddress = dtRev.Rows[0]["执行科室地址"].ToString();
                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                isConfirm = true;
                            }
                            else
                            {
                                isConfirm = false;
                            }
                            #endregion
                        }
                        if (isConfirm)
                        {
                            #region
                            this.AnsyWorker(ui =>
                            {
                                ui.UpdateTitle("正在处理中，请稍等...");

                                ui.SynUpdateUI(() =>
                                {
                                    #region 扣费判断相关
                                    //返回当天该诊室的排队最大号
                                    iMaxQueueNo = GetReserveMaxNo((int)drSelectRevData["MEDGROUPRDETAILID"], this.monthCalendar1.SelectedDates[0].Date > dtNow.Date);
                                    if (iMaxQueueNo == -2)
                                    {
                                        SkyComm.ShowMessageInfo("获取最大号失败！");
                                        return;
                                    }
                                    else if (iMaxQueueNo == -1)
                                    {
                                        if (this.monthCalendar1.SelectedDates[0].Date > dtNow.Date)
                                            SkyComm.ShowMessageInfo("当前时间【提前可预约】人数已满！");
                                        else
                                            SkyComm.ShowMessageInfo("当前时间预约人数已满！");
                                        return;
                                    }
                                    try
                                    {
                                        #region 扣费相关
                                        DataSet dsTemp = dsRecipeMedord.Clone();
                                        dsPrint = dsRecipeMedord.Clone();
                                        dsRecipeMedord.Clear();
                                        IEnumerable<string> _ClinicRecipedIDs = dtRev.AsEnumerable().Where(a => a.Field<string>("收费状态") == "未收费").Select(a => a.Field<string>("处方号")).Distinct();
                                        foreach (string ClinicRecipedID in _ClinicRecipedIDs)
                                        {
                                            DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByClinicRecipeId(ClinicRecipedID);
                                            if (ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return;
                                            DataRow[] drs = ds.Tables[0].Select(" RECIPETYPE <>'医材' and RECIPETYPE <> '附加' ");
                                            if (drs.Length < 1)
                                            {
                                                SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                return;
                                            }
                                            if (dsRecipeMedord.Tables.Count == 0)
                                                dsRecipeMedord.Tables.Add(drs.CopyToDataTable());
                                            else
                                                dsRecipeMedord.Tables[0].Merge(drs.CopyToDataTable());

                                            if (dsTemp.Tables.Count == 0)
                                            {
                                                dsTemp = dsRecipeMedord.Clone();
                                                dsPrint = dsRecipeMedord.Clone();
                                            }
                                            ArrayList checkClinic = new ArrayList();

                                            if (dsRecipeMedord.Tables.Count > 0 && dsRecipeMedord.Tables[0].Rows.Count > 0)
                                            {
                                                int i = 0;
                                                foreach (DataRow drMedord in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + ClinicRecipedID + "'"))
                                                {

                                                    if (FindRecipeState(drMedord["CLINICRECIPEID"].ToString(), drMedord["RECIPELISTNUM"].ToString()) == false)
                                                    {
                                                        SkyComm.ShowMessageInfo("处方" + drMedord["CLINICRECIPEID"].ToString() + "[" + drMedord["CHARGEITEM"].ToString() + "]已计费，不能再计费！");
                                                        return;
                                                    }

                                                    if (checkClinic.IndexOf(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString()) < 0)
                                                    {
                                                        foreach (DataRow dr in dsRecipeMedord.Tables[0].Select("CLINICRECIPEID = '" + drMedord["CLINICRECIPEID"].ToString() + "' and RECIPECONTENT = '" + drMedord["RECIPECONTENT"].ToString() + "'"))
                                                        {
                                                            dsTemp.Tables[0].ImportRow(dr);
                                                        }

                                                        checkClinic.Add(drMedord["CLINICRECIPEID"].ToString() + drMedord["RECIPECONTENT"].ToString());
                                                    }

                                                    dsPrint.Tables[0].ImportRow(drMedord);
                                                }
                                            }
                                            else
                                            {
                                                SkyComm.ShowMessageInfo("没有未收费的信息，请确认！");
                                                return;
                                            }
                                        }
                                        if (dsTemp.Tables.Count > 0)
                                        {
                                            if (dsTemp.Tables[0].Rows.Count > 0)
                                            {
                                                #region 调用扣费方法

                                                #region 扣费
                                                detailAccountData.Clear();
                                                ecipeMedicineData.Clear();
                                                AddRecipeCharge(dsTemp);

                                                //验证西北妇幼高值耗材
                                                if (!CheckHValueMaterial(detailAccountData))
                                                {
                                                    return;
                                                }

                                                CardAuthorizationFacade cardAuthorizationFacade = new CardAuthorizationFacade();
                                                string CardId = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                                                CardAuthorizationData eCardAuthorizationData = (CardAuthorizationData)cardAuthorizationFacade.SelectPatientAndCardInfoByCardID(CardId);
                                                string strIsBankCard = "0";
                                                if (eCardAuthorizationData.Tables[0].Columns.Contains("ISBANKCARD"))
                                                    strIsBankCard = eCardAuthorizationData.Tables[0].Rows[0]["ISBANKCARD"].ToString();
                                                //decimal ye = 0;
                                                if (strIsBankCard != "1")
                                                {
                                                    detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);

                                                    if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
                                                    {
                                                        ye = cardAuthorizationFacade.FindCardBalance(DiagnoseID);
                                                    }
                                                    else
                                                    {
                                                        ye = cardAuthorizationFacade.FindCardBalance_New(DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    #region 银医直联卡调用扣费方法

                                                    //string strCardTypeName = eCardAuthorizationData.Tables[0].Rows[0]["TYPE_NAME"].ToString();
                                                    //string strBankInterFacename = eCardAuthorizationData.Tables[0].Rows[0]["BANKINTEFACENAME"].ToString();
                                                    //Skynet.LoggingService.LogService.GlobalInfoMessage("卡号:" + CardId + ",卡类型：" + strCardTypeName + "，是否银医直联：" + strIsBankCard + ",接口名称" + strBankInterFacename);

                                                    //CommonFacade commonFacade = new CommonFacade();
                                                    //DateTime dateTime = commonFacade.GetServerDateTime();
                                                    //ValidateCode vc = new ValidateCode();
                                                    //string RoundCode = vc.GenValidateCode(4);
                                                    //string strlsh = dateTime.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + RoundCode;
                                                    //foreach (DataRow Row in detailAccountData.Tables[0].Rows)
                                                    //{

                                                    //    Row.BeginEdit();
                                                    //    Row[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = strCardTypeName;//结算方式
                                                    //    Row["ISBANKCARD"] = 1;
                                                    //    Row["BANKTRANSNO"] = strlsh;
                                                    //    Row.EndEdit();
                                                    //}

                                                    //decimal balanMoney = Convert.ToDecimal(detailAccountData.Tables[0].Compute("SUM(SELFMONEY)", ""));
                                                    //Exchange exchange = new Exchange();
                                                    //exchange.Init(strBankInterFacename);
                                                    //try
                                                    //{
                                                    //    Hashtable ht1 = new Hashtable();
                                                    //    ht1.Add("SEQNO", strlsh);
                                                    //    ht1.Add("PATIENTNAME", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                                                    //    ht1.Add("CARDID", CardId);
                                                    //    ht1.Add("CUSTID", CardId);
                                                    //    ht1.Add("MONEY", balanMoney);
                                                    //    ht1.Add("TRANTYPE", "医技");

                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法输入参数CARDID：" + CardId + ",MONEY：" + balanMoney.ToString());
                                                    //    Hashtable htqdRst = exchange.Trans(BussinessCode.ZZ_ZZ_C2H, ht1);
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("ZZ_ZZ_C2H方法返回代码：" + htqdRst["RCODE"].ToString() + ",返回消息：" + htqdRst["RINFO"].ToString());

                                                    //    if (htqdRst["RCODE"].ToString() != "0000")
                                                    //    {
                                                    //        Skynet.LoggingService.LogService.GlobalInfoMessage("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                    //        throw new Exception("银医扣费失败：" + htqdRst["RINFO"].ToString());
                                                    //    }
                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费失败：" + ex.Message);
                                                    //    throw new Exception("调用银行扣费错误：" + ex.Message);
                                                    //}

                                                    //try
                                                    //{
                                                    //    detailAccountData = (DetailAccountData)detailAccountFacade.insertEntityNoInvoice(detailAccountData, ref ecipeMedicineData);
                                                    //    exchange.Confirm(strlsh);

                                                    //}
                                                    //catch (Exception ex)
                                                    //{
                                                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行扣费成功，HIS保存失败：" + ex.Message);
                                                    //    exchange.Cancel(strlsh);
                                                    //    throw ex;
                                                    //}
                                                    #endregion
                                                }
                                                #endregion

                                                #region 重组 打印报表时候需要显示医嘱用语而不是明细
                                                DataTable dtCopy = dsRecipeMedord.Tables[0].Copy();
                                                DataTable dtMerge = new DataTable();
                                                //dtMerge.Columns.Add("MEDORDNAME", typeof(string)); //修改为下面这个字段
                                                dtMerge.Columns.Add("CHARGEITEM", typeof(string));
                                                dtMerge.Columns.Add("TOTALMONEY", typeof(decimal));
                                                dtMerge.Columns.Add("AMOUNT", typeof(decimal));
                                                var query = from t in dtCopy.AsEnumerable()
                                                            group t by new { t1 = t.Field<string>("MEDORDNAME"), t2 = t.Field<string>("CLINICRECIPEID") } into m
                                                            select new
                                                            {
                                                                MEDORDNAME = m.Key.t1,
                                                                AMOUNT = 1,
                                                                TOTALMONEY = m.Sum(n => n.Field<decimal>("TOTALMONEY"))
                                                            };
                                                var queryNew = from t in query
                                                               group t by new { t1 = t.MEDORDNAME } into m
                                                               select new
                                                               {
                                                                   MEDORDNAME = m.Key.t1,
                                                                   AMOUNT = m.Count(),
                                                                   TOTALMONEY = m.Sum(n => n.TOTALMONEY)
                                                               };
                                                queryNew.ToList().ForEach(q =>
                                                {
                                                    dtMerge.Rows.Add(q.MEDORDNAME, q.TOTALMONEY, q.AMOUNT);
                                                });

                                                dsCopy.Tables.Add(dtMerge);
                                                #endregion

                                                if (dsCopy.Tables[0].Rows.Count > 0)
                                                {
                                                    sumMoney = ReturnTotalMoney(dsCopy);
                                                }

                                                //打印医技扣费凭证
                                                isPrintReport = true;
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        SkyComm.ShowMessageInfo("扣费失败！原因：" + ex.Message);
                                        return;
                                    }
                                    #endregion
                                    CLINICMtReserveData resEntity = new CLINICMtReserveData();
                                    resEntity.Reserveid = dtRev.Rows[0]["预约号"].ToString();
                                    #region resEntity赋值
                                    resEntity.ResverveType = "门诊";
                                    resEntity.Diagnoseid = DiagnoseID;
                                    resEntity.Registerid = dtRev.Rows[0]["挂号号"].ToString();
                                    resEntity.Officeid = OfficeID; //this.lookUpEditGroupInfo.EditValue.ToString();
                                    resEntity.Examinename = GroupName; //this.lookUpEditGroupInfo.Text; //2016年7月2日 16:53:07
                                    resEntity.Checkdoctorid = "0";
                                    resEntity.Reservestarttime = realtime;// Convert.ToDateTime(dtRev.Rows[0]["预约日期"]);//时间段预约
                                    if (Convert.IsDBNull(dtRev.Rows[0]["预约结束日期"]))
                                    {
                                        //预约结束时间(修改为实际的预约操作时间)
                                        resEntity.Reserveendtime = dtNow;
                                    }
                                    else
                                    {
                                        resEntity.Reserveendtime = Convert.ToDateTime(dtRev.Rows[0]["预约结束日期"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["报到时间"]))
                                    {
                                        resEntity.Checkintimes = null;
                                    }
                                    else
                                    {
                                        resEntity.Checkintimes = Convert.ToDateTime(dtRev.Rows[0]["报到时间"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["完成时间"]))
                                    {
                                        resEntity.Completiontime = null;
                                    }
                                    else
                                    {
                                        resEntity.Completiontime = Convert.ToDateTime(dtRev.Rows[0]["完成时间"]);
                                    }
                                    if (Convert.IsDBNull(dtRev.Rows[0]["挂起时间"]))
                                    {
                                        dtRev.Rows[0]["挂起时间"] = null;
                                    }
                                    else
                                    {
                                        resEntity.Suspendtime = Convert.ToDateTime(dtRev.Rows[0]["挂起时间"]);
                                    }
                                    resEntity.Operatorid = dtRev.Rows[0]["操作员ID"].ToString();
                                    resEntity.Resstatus = Convert.ToInt32(dtRev.Rows[0]["预约状态"].ToString());
                                    if (isReserveAndCheckIn)
                                    {
                                        resEntity.Resstatus = 2;//预约并报到
                                        resEntity.Checkdoctorid = "0";//按组报到，无法知道检查医师
                                        resEntity.Checkintimes = dtNow;
                                    }
                                    resEntity.Checkstatus = Convert.ToInt32(dtRev.Rows[0]["检查状态"].ToString());
                                    #endregion

                                    string strReseverID = "0";   //预约号
                                    CLINICMtReserveFacade resFacade = new CLINICMtReserveFacade();
                                    //插入医技预约表
                                    strReseverID = resFacade.MyInsert(resEntity);
                                    resEntity.Reserveid = strReseverID;

                                    //预约处方表实体泛型集合
                                    EntityList<CLINICReseverRecipeData> resRecipeLst = new EntityList<CLINICReseverRecipeData>();
                                    //存放处方号
                                    List<string> myLst = new List<string>();
                                    myLst.Clear();
                                    for (int i = 0; i < dtRev.Rows.Count; i++)
                                    {//douyaming 2013-9-2 CASE:15238
                                        if (!myLst.Contains(dtRev.Rows[i]["处方号"].ToString()))
                                        {
                                            myLst.Add(dtRev.Rows[i]["处方号"].ToString());
                                        }
                                        dtRev.Rows[i]["收费状态"] = "已收费";
                                    }
                                    //去掉处方号相同的记录
                                    DataTable dtTemp = dtRev.Copy();
                                    var dataTemp = dtTemp.AsEnumerable();
                                    var result = (from n in dataTemp
                                                  select n).Distinct(new MyComparer2());
                                    dtTemp = result.CopyToDataTable();
                                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                                    {
                                        CLINICReseverRecipeData entity = new CLINICReseverRecipeData();
                                        entity.Clinicrecipeid = dtTemp.Rows[i]["处方号"].ToString();
                                        entity.Reserveid = strReseverID;
                                        resRecipeLst.Add(entity);
                                        // myLst.Add(entity.Clinicrecipeid);
                                    }

                                    CLINICReseverRecipeFacade resRecipeFacade = new CLINICReseverRecipeFacade();
                                    //插入预约处方表
                                    resRecipeFacade.Insert(resRecipeLst, null);

                                    //更新医技预约组排班明细表的最大号
                                    bool isOnLine = this.monthCalendar1.SelectedDates[0].Date > dtNow.Date ? false : true;
                                   // iMaxQueueNo = medGroupDetailFacade.updateMaxReserveNoNew((int)drSelectRevData["MEDGROUPRDETAILID"], isOnLine);

                                    //排队表实体
                                    queueEntity.QueueDate = realtime;
                                    queueEntity.Queueid = "1";
                                    queueEntity.Reserveid = strReseverID;
                                    queueEntity.Priority = 1;
                                    int strQueue = Convert.ToInt32(queueID);  //排队号
                                    queueEntity.Queueno = strQueue;
                                    queueEntity.Queuegroupdtailid = drSelectRevData["MEDGROUPRDETAILID"].ToString();
                                    if (isOnLine)
                                    {
                                        queueEntity.Exitnooff = "0";
                                    }
                                    else
                                    {
                                        queueEntity.Exitnooff = "1";
                                    }
                                    CLINICMtQueueFacade queueFacade = new CLINICMtQueueFacade();
                                    QueueID = strQueue.ToString();
                                    queueFacade.Insert(queueEntity);
                                    queueNO = strQueue.ToString();//回传排队号
                                    //更新处方表预约状态字段
                                    ClinicPhysicianRecipeFacade recipeFacade = new ClinicPhysicianRecipeFacade();
                                    for (int i = 0; i < myLst.Count; i++)
                                    {
                                        int n = recipeFacade.UpdateByClinicRecipeID(myLst[i], OfficeID, false, "门诊", dtRev.Rows[0]["挂号号"].ToString());
                                    }

                                    //更新医技预约组排班明细表的最大号

                                    int updateRtn = medGroupDetailFacade.updateMaxReserveNoMiddle((int)drSelectRevData["MEDGROUPRDETAILID"], isOnLine);
                                    if (updateRtn < 1)
                                    {
                                        SkynetMessage.MsgError("更新医技预约组排班明细表的最大号失败！");
                                        return;
                                    }


                                    //预约并报到
                                    if (isReserveAndCheckIn)
                                    {
                                        //从处方表移除已预约(报到)的处方
                                        DataRow[] drAll = this.dtRev.Select("1=1");
                                        if (drAll.Length > 0)
                                        {
                                            bool exFlag = false;
                                            string exOfficeID = resEntity.Officeid;
                                            foreach (string singleOfficeID in allOfficeID.Split('|'))
                                            {
                                                if (singleOfficeID == exOfficeID)
                                                {
                                                    exFlag = true;
                                                    break;
                                                }
                                            }
                                            if (exFlag)
                                            {
                                                SendExamApp(resEntity, drAll);
                                            }
                                            //SendExamApp(resEntity, drAll);
                                        }
                                    }
                                   
                                    PrintAll(ye, sumMoney, dsCopy, reserveItem);


                                    DialogResult = DialogResult.OK;
                      


                                });
                            });
                            #endregion
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        SkyComm.ShowMessageInfo(ex.Message);
                        return;
                    }

                    #endregion
                    int deleteid = Crexit.Delete(dsExit.Tables[0].Rows[0]["EXITID"].ToString());//删除中间表数据
                }


            }

        }
        /// <summary>
        /// 获取当前时间当前组的最大预约号(同时判断是否可预约)
        /// return -1预约已满,-2未查询到数据,大于0为最大预约号
        /// </summary>
        /// <param name="medGroupDetailID">医技预约组排班明细表主键</param>
        /// <param name="isCurrent">false在线预约,true离线预约</param>
        /// <returns>-1预约已满,-2未查询到数据,大于0为最大预约号</returns>
        private int GetReserveMaxNo(int medGroupDetailID, bool isCurrent)
        {
            TMedgroupDetailData data = new TMedgroupDetailData();
            medGroupDetailFacade = new TMedgroupDetailFacade();
            data = medGroupDetailFacade.GetByPrimaryKey(medGroupDetailID);
            if (data == null) return -2;
            if (!isCurrent)//在线预约
            {
                //在线预约限数+离线预约限数-在线已预约数-离线已预约数
                if (data.Beclinicresenum + data.Bespokereresenum - data.Beclinicmakemin - data.Bespokeremain < 1)
                    return -1;
            }
            else//离线预约
            {
                //离线预约限数-离线已预约数
                if (data.Bespokereresenum - data.Bespokeremain < 1)
                    return -1;
            }
            if (data.Reservemaxnum.HasValue)
                return (int)data.Reservemaxnum;
            else return 0;
        }
        /// <summary>
        /// 判断该处方是否可以进行计费确认
        /// </summary>
        /// <param name="ClinicRecipeid"></param>
        /// <returns>如果已经计费返回false,可以进行计费返回true</returns>
        private bool FindRecipeState(string ClinicRecipeid, string recipelistnum)
        {
            ClinicPhysicianRecipeFacade theClinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet ds = theClinicPhysicianRecipeFacade.FindClinicRecipeByClinicRecipeid(ClinicRecipeid, recipelistnum);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 调用PACS医技报到接口 wangchao 2016-07-02 add
        /// </summary>
        /// <param name="ReserveID"></param>
        /// <returns></returns>
        private void SendExamApp(CLINICMtReserveData resEntity, DataRow[] arrdr)
        {
            try
            {
                //if (string.IsNullOrEmpty(arrdr[0].Table.TableName))
                //    arrdr[0].Table.TableName = "temp1";
                //arrdr[0].Table.WriteXml("ReportXml\\Temp1.xml");
                ClinicPhysicianRecipeFacade facade = new ClinicPhysicianRecipeFacade();
                string strXml = string.Empty;
                if (arrdr.Length > 0)
                {
                    DataSet ds = facade.FindAlreadyChechWithAKey(resEntity.ResverveType, resEntity.Diagnoseid, resEntity.Reserveid);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("上传失败，预约号：" + resEntity.Reserveid);
                        //SkynetMessage.MsgInfo("没有找到可报到的有效检查申请！");
                        return;
                    }
                    //if (string.IsNullOrEmpty(ds.Tables[0].TableName))
                    //    ds.Tables[0].TableName = "temp2";
                    //ds.WriteXml("ReportXml\\Temp2.xml");

                    IEnumerable<string> _APPLYDOCNOs = ds.Tables[0].AsEnumerable().Select(a => a.Field<string>("APPLYDOCNO")).Distinct();
                    foreach (string strApplyNo in _APPLYDOCNOs)
                    {
                        #region MyRegion
                        DataRow[] ArrRow = ds.Tables[0].Select("APPLYDOCNO = '" + strApplyNo + "'");
                        if (ArrRow.Length == 0)
                        {
                            Skynet.LoggingService.LogService.GlobalInfoMessage("上传失败，申请号：" + strApplyNo);
                            continue;
                        }
                        //构造数据****
                        string PatientName = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();//病人姓名
                        string PatientAge = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString();///病人年龄
                        string AgeUnit = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();//年龄单位
                        string PatientSex = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString(); //病人性别

                        strXml = "<Send>";
                        //HIS系统主键,申请单号,病人主索引,开单时间,ANY
                        strXml += "<IHISORDER_IID>" + ArrRow[0]["APPLYDOCNO"].ToString() + "</IHISORDER_IID>";
                        strXml += "<CORDER_INDEX>" + ArrRow[0]["APPLYDOCNO"].ToString() + "</CORDER_INDEX>";
                        strXml += "<CPATWL_KEY>" + resEntity.Diagnoseid + "</CPATWL_KEY>";
                        ////开方时间?
                        strXml += "<CTRIGGER_DTTM>" + Convert.ToDateTime(ArrRow[0]["BILLTIME"]).ToString("yyyy-MM-dd") + "</CTRIGGER_DTTM>";
                        strXml += "<CREPLICA_DTTM>ANY</CREPLICA_DTTM>";

                        //病人号,门诊号,住院号,医保编号,姓名
                        strXml += "<CPATIENT_ID>" + resEntity.Diagnoseid + "</CPATIENT_ID>";
                        strXml += "<COUTPATIENT_ID>" + resEntity.Registerid + "</COUTPATIENT_ID>";

                        if (resEntity.ResverveType == "门诊")
                        {
                            strXml += "<CINPATIENT_ID></CINPATIENT_ID>";
                        }
                        else
                        {
                            strXml += "<CINPATIENT_ID>" + arrdr[0]["挂号号"].ToString() + "</CINPATIENT_ID>";
                        }
                        strXml += "<CHPATIENT_ID></CHPATIENT_ID>";
                        strXml += "<CNAME>" + PatientName + "</CNAME>";

                        //性别,年龄，年龄单位，出生日期，民族
                        strXml += "<CSEX>" + PatientSex + "</CSEX>";
                        strXml += "<CAGE>" + PatientAge + "</CAGE>";
                        strXml += "<CAGEDW>" + AgeUnit + "</CAGEDW>";

                        DateTime dtBirthday = new DateTime();
                        if (DateTime.TryParse(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString(), out dtBirthday) == true)
                            strXml += "<DDATE_OF_BIRTH>" + dtBirthday.ToString("yyyy-MM-dd") + "</DDATE_OF_BIRTH>";
                        else
                            strXml += "<DDATE_OF_BIRTH></DDATE_OF_BIRTH>";
                        strXml += "<CNATION></CNATION>";

                        //地址，联系电话，病人类型，开单医生工号，开单医生姓名
                        strXml += "<CMAILING_ADDRESS></CMAILING_ADDRESS>";
                        strXml += "<CPHONE_NUMBER_HOME></CPHONE_NUMBER_HOME>";
                        strXml += "<CFSOURCE>" + resEntity.ResverveType + "</CFSOURCE>";
                        strXml += "<CKDYQ_ID>" + ArrRow[0]["DOCTORID"].ToString() + "</CKDYQ_ID>";//申请医生id
                        strXml += "<CKDYQ_NAME>" + ArrRow[0]["DOCTORNAME"].ToString() + "</CKDYQ_NAME>";//申请医生

                        ////病区名称,开单科室编码.开单科室名称,床号,临床描述
                        strXml += "<CWARD_NAME>" + ArrRow[0]["REGISTEROFFICE"].ToString() + "</CWARD_NAME>";
                        strXml += "<CDEPT_CODE>" + ArrRow[0]["REGISTEROFFICEID"].ToString() + "</CDEPT_CODE>";
                        strXml += "<CREQ_DEPT>" + ArrRow[0]["REGISTEROFFICE"].ToString() + "</CREQ_DEPT>";
                        strXml += "<CBED_NO></CBED_NO>";

                        string MedordName = string.Empty;
                        decimal totalMoney = 0;
                        string strjcmd = string.Empty;
                        foreach (DataRow drInfo in ArrRow)
                        {
                            if (string.IsNullOrEmpty(MedordName))
                            {
                                MedordName = drInfo["MEDORDNAME"].ToString();
                            }
                            else
                            {
                                MedordName += "," + drInfo["MEDORDNAME"].ToString();
                            }
                            if (string.IsNullOrEmpty(strjcmd))
                            {
                                strjcmd = drInfo["APPLYREMARK"].ToString();
                            }
                            else
                            {
                                strjcmd += "," + drInfo["APPLYREMARK"].ToString();
                            }

                            totalMoney += Convert.ToDecimal(drInfo["TOTALMONEY"]);
                        }

                        ClinicBriefemrFacade theClinicBriefemrFacade = new ClinicBriefemrFacade();
                        ClinicBriefemrData theClinicBriefemrData = theClinicBriefemrFacade.GetByDiagnoseIdOrRegisterID(resEntity.Diagnoseid, resEntity.Registerid, ArrRow[0]["REGISTEROFFICEID"].ToString());

                        if (theClinicBriefemrData != null)
                        {
                            //strXml += "<CCLIN_SYNP>" + theClinicBriefemrData.DiagResult + "</CCLIN_SYNP>";

                            //////临床诊断,检查描述,检查备注,
                            //strXml += "<CCLIN_DIAG>" + theClinicBriefemrData.DiagResult + "</CCLIN_DIAG>";
                            //strXml += "<CPHYS_SIGN></CPHYS_SIGN>";
                            //strXml += "<CRELEVANT_DIAG>" + strjcmd + "</CRELEVANT_DIAG>";

                            strXml += "<CCLIN_SYNP>" + theClinicBriefemrData.CaseinChief + "</CCLIN_SYNP>";

                            ////临床诊断,检查描述,检查备注,
                            strXml += "<CCLIN_DIAG>" + theClinicBriefemrData.DiagResult + "</CCLIN_DIAG>";
                            strXml += "<CPHYS_SIGN>" + theClinicBriefemrData.Physical + "</CPHYS_SIGN>";
                            strXml += "<CRELEVANT_DIAG>" + theClinicBriefemrData.EMPLOYMENT + "</CRELEVANT_DIAG>";

                        }
                        else
                        {
                            strXml += "<CCLIN_SYNP></CCLIN_SYNP>";
                            ////临床诊断,检查描述,检查备注,
                            strXml += "<CCLIN_DIAG>" + strjcmd + "</CCLIN_DIAG>";
                            strXml += "<CPHYS_SIGN></CPHYS_SIGN>";
                            strXml += "<CRELEVANT_DIAG>" + strjcmd + "</CRELEVANT_DIAG>";
                        }

                        //检查类型,检查分组编码,
                        strXml += "<CEXAM_CLASS>" + ArrRow[0]["MACHINECLASSNAME"].ToString() + "</CEXAM_CLASS>";


                        strXml += "<CEXAM_GROUP_NO>" + GroupID + "</CEXAM_GROUP_NO>";

                        ////检查分组名称,检查房间ID,检查房间,检查项目编码,检查项目
                        strXml += "<CEXAM_GROUP>" + GroupName + "</CEXAM_GROUP>";
                        strXml += "<CEXAM_ROOM_ID></CEXAM_ROOM_ID>";
                        strXml += "<CEXAM_ROOM></CEXAM_ROOM>";

                        strXml += "<CEXAM_ITEM_NO>" + MedordName + "</CEXAM_ITEM_NO>";
                        strXml += "<CEXAM_ITEM>" + MedordName + "</CEXAM_ITEM>";

                        ////检查预约时间,检查排队号,检查费用,收费类型

                        strXml += "<CEXAM_SCHE_DATE>" + resEntity.Reserveendtime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "</CEXAM_SCHE_DATE>";

                        strXml += "<CEXAM_SCHE_NUM>" + ArrRow[0]["QUEUENO"].ToString() + "</CEXAM_SCHE_NUM>";
                        strXml += "<CCOSTS>" + totalMoney + "</CCOSTS>";
                        strXml += "<CCOSTS_FLAG></CCOSTS_FLAG>";
                        strXml += "</Send>";
                        #endregion
                        try
                        {
                            Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到接口入参：" + strXml);
                            if (string.IsNullOrEmpty(strXml))
                            {
                                Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到接口错误：未能获取表单中数据！");
                            }
                            else
                            {
                                string returnString = ServiceTool.SendExamApp(strXml);
                                Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到接口返回值：" + returnString);
                                if (returnString != "0")
                                {
                                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到接口错误：" + returnString);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到接口错误：" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用医技报到PACS接口失败。原因：" + ex.Message + "|");
            }
        }

        private void AddRecipeCharge(DataSet recipeInfo)
        {
            decimal money = 0;
            string OrderID = "";
            DataRow selrow = null;
            money = 0;
            decimal packAmount = 0;

            foreach (DataRow rowRecipe in recipeInfo.Tables[0].Rows)
            {
                selrow = rowRecipe;
                money = Convert.ToDecimal(selrow["TOTALMONEY"]);

                if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
                {
                    AddNewRecipeClinic(selrow, out OrderID);
                    foreach (DataRow row in detailAccountData.Tables[0].Rows)
                    {
                        if (rowRecipe["RECIPETYPE"].ToString() != "附加")
                        {
                            if (row["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                                row["SUMMARY"].ToString() == selrow["SUMMARY"].ToString() &&
                                row["ITEMID"].ToString() == selrow["ITEMID"].ToString() &&
                                row["DETAILACCOUNTID"].ToString().Trim() == OrderID &&
                                row["UNITECODE"].ToString().Trim() == selrow["RECIPECONTENT"].ToString().Trim()) //在组合收费表的CODENO对应收费项目的ITEMID
                            {
                                row.BeginEdit();
                                row["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]) + Convert.ToDecimal(selrow["AMOUNT"]);
                                //							row["UNITPRICE"] =selrow["UNITPRICE"];
                                row["MONEY"] = Convert.ToDecimal(row["MONEY"]) + money;
                                row.EndEdit();
                            }
                        }
                        else
                        {
                            if (row["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                                    row["SUMMARY"].ToString() == selrow["SUMMARY"].ToString() &&
                                    row["ITEMID"].ToString() == selrow["ITEMID"].ToString() &&
                                    row["DETAILACCOUNTID"].ToString().Trim() == OrderID) //在附加费时
                            {
                                row.BeginEdit();
                                row["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]) + Convert.ToDecimal(selrow["AMOUNT"]);
                                //							row["UNITPRICE"] =selrow["UNITPRICE"];
                                row["MONEY"] = Convert.ToDecimal(row["MONEY"]) + money;
                                row.EndEdit();
                            }
                        }
                    }
                }
                else //药品费，医材费
                {
                    packAmount = Convert.ToDecimal(selrow["AMOUNT"]);

                    packAmount = packAmount * Convert.ToDecimal(selrow["DOSECOUNT"]);

                    AddNewRecipeClinic(selrow, out OrderID);

                    string summary = string.Empty;
                    string itemid = string.Empty;
                    foreach (DataRow rowYpmx in ecipeMedicineData.Tables[0].Rows)
                    {
                        switch (selrow["SUMMARY"].ToString())
                        {
                            case "西药":
                                summary = "西药费";
                                break;
                            case "中成药":
                                summary = "中成药";
                                break;
                            case "中草药":
                                summary = "中草药";
                                break;
                            case "医材":
                                summary = "医材费";
                                break;
                        }

                        if (rowYpmx["STOREROOMID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                            rowYpmx["SUMMARY"].ToString() == summary &&
                            rowYpmx["LEECHDOMNO"].ToString() == selrow["ITEMID"].ToString() &&
                            rowYpmx["DETAILACCOUNTID"].ToString() == OrderID)
                        {
                            rowYpmx.BeginEdit();
                            rowYpmx["AMOUNT"] = Convert.ToDecimal(rowYpmx["AMOUNT"]) + packAmount;
                            rowYpmx["OLDAMOUNT"] = Convert.ToDecimal(rowYpmx["OLDAMOUNT"]) + packAmount;
                            rowYpmx["UNITAMOUNT"] = Convert.ToDecimal(rowYpmx["UNITAMOUNT"]) + (Convert.ToDecimal(selrow["AMOUNT"]) * Convert.ToDecimal(selrow["DOSECOUNT"]));
                            rowYpmx["OLDUNITAMOUNT"] = Convert.ToDecimal(rowYpmx["OLDUNITAMOUNT"]) + (Convert.ToDecimal(selrow["AMOUNT"]) * Convert.ToDecimal(selrow["DOSECOUNT"]));
                            rowYpmx["TOTALMONEY"] = Convert.ToDecimal(rowYpmx["TOTALMONEY"]) + money;
                            rowYpmx.EndEdit();
                        }
                    }

                    summary = string.Empty;
                    itemid = string.Empty;
                    foreach (DataRow rowSfxm in detailAccountData.Tables[0].Rows)
                    {
                        switch (selrow["SUMMARY"].ToString())
                        {
                            case "西药":
                                summary = "西药费";
                                itemid = "01";
                                break;
                            case "中成药":
                                summary = "中成药";
                                itemid = "02";
                                break;
                            case "中草药":
                                summary = "中草药";
                                itemid = "03";
                                break;
                            case "医材":
                                summary = "医材费";
                                itemid = "04";
                                break;
                        }
                        if (rowSfxm["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                            rowSfxm["SUMMARY"].ToString() == summary &&
                            rowSfxm["ITEMID"].ToString() == itemid &&
                            rowSfxm["DETAILACCOUNTID"].ToString() == OrderID &&
                            rowSfxm["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim()
                        )
                        {
                            rowSfxm.BeginEdit();
                            rowSfxm["AMOUNT"] = 0;
                            rowSfxm["UNITPRICE"] = 0;
                            rowSfxm["MONEY"] = Convert.ToDecimal(rowSfxm["MONEY"]) + money;
                            rowSfxm.EndEdit();
                        }
                    }
                }

            }

            money = 0;

            decimal dMoney = 0;
            int iConfigClinicMoney = Convert.ToInt32(SystemInfo.SystemConfigs["门诊收费时分币处理方式"].DefaultValue);
            foreach (DataRow dr in detailAccountData.Tables[0].Rows)
            {

                dr.BeginEdit();
                dr[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = "预交金";
                dr["ISBANKCARD"] = 0;
                dMoney = Convert.ToDecimal(dr[DetailAccountData.D_DETAIL_ACCOUNT_MONEY]);
                switch (iConfigClinicMoney)
                {
                    //0.不处理;1.四舍五入;2.见分进位;
                    case 0:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney, 2);
                        break;
                    case 1:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney, 1);
                        break;
                    case 2:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney + Convert.ToDecimal(0.04), 1);
                        break;
                }

                dr[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = DecimalRound.Round(dMoney, 2);

                dr["PITCHON"] = false;
                dr["ORDERID"] = dr["DETAILACCOUNTID"];
                dr["OLDORDERID"] = dr["DETAILACCOUNTID"];
                dr.EndEdit();
            }

            //处理行序号
            foreach (DataRow datarow in ecipeMedicineData.Tables[0].Rows)
            {
                datarow.BeginEdit();
                datarow["ORDERID"] = datarow["DETAILACCOUNTID"];
                datarow["OLDORDERID"] = datarow["DETAILACCOUNTID"];
                datarow.EndEdit();
            }
        }
        private void AddNewRecipeClinic(DataRow selrow, out string ID)
        {
            //bool isHaving = false;
            bool havingSfmx = false;
            bool havingYpmx = false;
            string OrderID = Convert.ToString(detailAccountData.Tables[0].Rows.Count + 1);//selrow["CLINICRECIPEID"].ToString().Trim();
            string Summary = "";
            string ItemID = "";
            string execOfficeName;
            string execOfficeID;

            execOfficeName = selrow["EXECOFFICE"].ToString().Trim();
            execOfficeID = selrow["EXECOFFICEID"].ToString().Trim();

            if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
            {
                ItemID = selrow["ITEMID"].ToString().Trim();
                Summary = selrow["SUMMARY"].ToString().Trim();
            }
            else
            {
                switch (selrow["SUMMARY"].ToString())
                {
                    case "西药":
                        ItemID = "01";
                        Summary = "西药费";
                        break;
                    case "中成药":
                        ItemID = "02";
                        Summary = "中成药";
                        break;
                    case "中草药":
                        ItemID = "03";
                        Summary = "中草药";
                        break;
                    case "医材费":
                        ItemID = "04";
                        Summary = "医材费";
                        break;
                }
            }

            //药品费,医材费
            //STOREROOM,LEECHDOMNO
            foreach (DataRow rowYpmx in ecipeMedicineData.Tables[0].Rows)
            {
                if (rowYpmx["STOREROOMID"].ToString().Trim() == execOfficeID &&
                    rowYpmx["SUMMARY"].ToString().Trim() == Summary &&
                    rowYpmx["LEECHDOMNO"].ToString().Trim() == selrow["ITEMID"].ToString().Trim() &&
                    rowYpmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                {
                    havingYpmx = false;
                }
            }

            foreach (DataRow rowSfmx in detailAccountData.Tables[0].Rows)
            {
                if (selrow["RECIPETYPE"].ToString() != "附加")
                {
                    if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                        rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                        rowSfmx["ITEMID"].ToString().Trim() == ItemID &&
                        rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim() &&
                        rowSfmx["UNITECODE"].ToString().Trim() == selrow["RECIPECONTENT"].ToString().Trim())
                    {
                        havingSfmx = true;
                    }
                }
                else
                {
                    if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                        rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                        rowSfmx["ITEMID"].ToString().Trim() == ItemID &&
                        rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                    {
                        havingSfmx = true;
                    }
                }
            }

            foreach (DataRow rowSfmx in detailAccountData.Tables[0].Rows)
            {
                if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                    rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                    rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                {
                    OrderID = rowSfmx["ORDERID"].ToString().Trim();
                    break;
                }
            }


            //住院明细帐业务实体(检治费)
            if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
            {
                if (havingSfmx == false)
                {
                    AddNewRecipeSfxmDetailAccountData(OrderID, selrow);
                }
            }
            else //住院处方明细业务实体(药品费，医材费)
            {
                if (havingSfmx == false)
                {
                    AddNewRecipeYpDetailAccountData(OrderID, selrow);
                }
                if (havingYpmx == false)
                {
                    AddNewRecipeEcipeMedicineData(OrderID, selrow);
                }
            }

            ID = OrderID;
        }
        private void AddNewRecipeSfxmDetailAccountData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = detailAccountData.Tables[0].Rows.Count + 1;
            DataRow rowSfxm = detailAccountData.Tables[0].NewRow();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OLDORDER] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ORDERID] = OrderID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DETAILACCOUNTID] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DIAGNOSEID] = DiagnoseID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CANCELMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = SelUniteRow["ITEMID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = SelUniteRow["CHARGEITEM"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = SelUniteRow["SUMMARY"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_PITCHON] = false;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DOCTORID] = SelUniteRow["DOCTORID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ESCAPECHARGEMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CODENO] = SelUniteRow["CODENO"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNIT] = SelUniteRow["UNIT"];

            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_EXECOFFICEID] = SelUniteRow["EXECOFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATORID] = SysOperatorInfo.OperatorID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITPRICE] = SelUniteRow["UNITPRICE"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEDATE] = new CommonFacade().GetServerDateTime();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MEDICARETYPE] = SelUniteRow["MEDICARETYPE"];
            //自付金额SELFMONEY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = 0;
            //BALANCEMODE结算方式
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = 0;
            //现金支付额CASHDEFRAY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DISCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTBALANCE] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OVERTYPE] = 0;//重打标识,0=正常;1=重打
            //OPERATEORDERID操作流水号
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEORDERID] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_AMOUNT] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MONEY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTERID] = SelUniteRow["REGISTERID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CLINICRECIPEID] = SelUniteRow["CLINICRECIPEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_INVOICEITEM] = SelUniteRow["INVOICEITEM"];

            if (SelUniteRow["SICKTYPE"].ToString().Trim() != string.Empty)
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = SelUniteRow["SICKTYPE"].ToString().Trim();
            }
            else
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = "0001";
            }

            if (SelUniteRow["RECIPETYPE"].ToString() != "附加")
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITECODE] = SelUniteRow["RECIPECONTENT"];
            }
            detailAccountData.Tables[0].Rows.Add(rowSfxm);
        }

        private void AddNewRecipeYpDetailAccountData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = detailAccountData.Tables[0].Rows.Count + 1;
            DataRow rowSfxm = detailAccountData.Tables[0].NewRow();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OLDORDER] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ORDERID] = OrderID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DETAILACCOUNTID] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DIAGNOSEID] = DiagnoseID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CANCELMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_PITCHON] = false;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DOCTORID] = SelUniteRow["DOCTORID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ESCAPECHARGEMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_EXECOFFICEID] = SelUniteRow["EXECOFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATORID] = SysOperatorInfo.OperatorID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITPRICE] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEDATE] = new CommonFacade().GetServerDateTime();
            //自付金额SELFMONEY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = 0;
            //BALANCEMODE结算方式
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = 0;
            //现金支付额CASHDEFRAY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DISCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTBALANCE] = 0;
            // rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITECODE] = SelUniteRow["RECIPECONTENT"];   //douayming unitecode
            //OPERATEORDERID操作流水号
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEORDERID] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OVERTYPE] = 0;//重打标识,0=正常;1=重打
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_AMOUNT] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MONEY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTERID] = SelUniteRow["REGISTERID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CLINICRECIPEID] = SelUniteRow["CLINICRECIPEID"];

            if (SelUniteRow["SICKTYPE"].ToString().Trim() != string.Empty)
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = SelUniteRow["SICKTYPE"].ToString().Trim();
            }
            else
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = "0001";
            }


            switch (SelUniteRow["SUMMARY"].ToString().Trim())
            {
                case "西药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "西药费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "西药费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "01";
                    break;
                case "中成药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "中成药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "中成药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "02";
                    break;
                case "中草药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "中草药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "中草药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "03";
                    break;
                case "医材":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "医材";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "医材费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "04";
                    break;
            }

            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_INVOICEITEM] = SelUniteRow["INVOICEITEM"];
            detailAccountData.Tables[0].Rows.Add(rowSfxm);
        }
        private decimal ReturnTotalMoney(DataSet dsCopy)
        {
            try
            {
                decimal sumMoney = 0;
                foreach (DataRow drMedord in dsCopy.Tables[0].Rows)
                {
                    sumMoney += Convert.ToDecimal(drMedord["TOTALMONEY"]);
                }

                switch (SystemInfo.SystemConfigs["门诊收费时分币处理方式"].DefaultValue)
                {
                    //0.不处理;1.四舍五入;2.见分进位;
                    case "0":
                        sumMoney = DecimalRound.Round(sumMoney, 2);
                        break;
                    case "1":
                        sumMoney = DecimalRound.Round(sumMoney, 1);
                        break;
                    case "2":
                        sumMoney = DecimalRound.Round(sumMoney + Convert.ToDecimal(0.04), 1);
                        break;
                }
                return sumMoney;
            }
            catch
            {
                return 0;
            }
        }
        private void AddNewRecipeEcipeMedicineData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = ecipeMedicineData.Tables[0].Rows.Count + 1;
            DataRow rowYp = ecipeMedicineData.Tables[0].NewRow();
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDORDERID] = OrderID;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ORDERID] = OrderID;
            // rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SERIALNO] = ecipeMedicineData.Tables[0].Select("DetailAccountID = '" + DetailAccountID + "'").Length + 1;
            //serialNo = ecipeMedicineData.Tables[0].Select("DetailAccountID = '" + DetailAccountID + "'").Length + 1;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SERIALNO] = SelUniteRow["RECIPELISTNUM"];
            // serialNo = Convert.ToInt32(SelUniteRow["RECIPELISTNUM"]);

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DETAILACCOUNTID] = DetailAccountID;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_CANCELMARK] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_PITCHON] = false;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOCTORID] = SelUniteRow["DOCTORID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_STOREROOMID] = SelUniteRow["EXECOFFICEID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_STOREROOM] = SelUniteRow["EXECOFFICE"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OPERATORID] = SysOperatorInfo.OperatorID;

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_LEECHDOMNO] = SelUniteRow["ITEMID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_LEECHDOMNAME] = SelUniteRow["CHARGEITEM"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SPECS] = SelUniteRow["SPECS"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_MEDICARETYPE] = SelUniteRow["MEDICARETYPE"];

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNIT] = SelUniteRow["UNIT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNITPRICE] = SelUniteRow["UNITPRICE"];

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDUNITPRICE] = SelUniteRow["UNITPRICE"];
            //划价号（处方号）
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ECIPENUM] = "";
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_AMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_TOTALMONEY] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOSAGE] = SelUniteRow["DOSECOUNT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SENDLEECHDOMMARK] = 0;//药房发药标示0=未发；1=已发
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNITAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDUNITAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OUTPATIENTUNIT] = SelUniteRow["OUTPATIENTUNIT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_CHANGERATIO] = SelUniteRow["CHANGERATIO"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ECIPENUM] = SelUniteRow["CLINICRECIPEID"];//将处方号保存到药品的划价号中，为了在药房方便打印处方
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOSE] = 0;//输入剂量
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_TOPRETAILPRICE] = SelUniteRow["TOPRETAILPRICE"];//最高限价

            switch (SelUniteRow["SUMMARY"].ToString().Trim())
            {
                case "西药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "西药费";
                    break;
                case "中成药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "中成药";
                    break;
                case "中草药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "中草药";
                    break;
                case "医材":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "医材费";
                    break;
            }
            ecipeMedicineData.Tables[0].Rows.Add(rowYp);
        }
        private void PrintReport(decimal ye, decimal sumMoney, DataSet dsPrint)
        {
            try
            {
                DataSet dsXML = new DataSet();
                DataTable dtXML = this.CreateDataTable("扣费").Clone();
                DataRow dr = dtXML.NewRow();
                dr["医院名称"] = SysOperatorInfo.CustomerName;
                dr["姓名"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                dr["性别"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                dr["出生日期"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString();
                dr["年龄"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                dr["卡余额"] = SkyComm.cardBlance;
                dr["卡号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                dr["本次扣费金额"] = sumMoney.ToString();
                dr["诊疗号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                dr["操作员"] = SysOperatorInfo.OperatorCode;
                dr["操作员姓名"] = SysOperatorInfo.OperatorName;
                dtXML.Rows.Add(dr);
                dsXML.Tables.Add(dtXML.Copy());
                dsXML.Tables[0].TableName = "参数";
                dsXML.Tables.Add(dsPrint.Tables[0].Copy());
                dsXML.Tables[1].TableName = "数据集";


                dsXML.WriteXml(Application.StartupPath + @"\\ReportXml\\自助医技预约扣费凭证" + this.dtRev.Rows[0]["诊疗号"].ToString() + ".xml");
                string path = Application.StartupPath + @"\\Reports\\自助医技预约扣费凭证.frx";

                if (System.IO.File.Exists(path) == false)
                {
                    SkyComm.ShowMessageInfo("自助医技预约扣费凭证不存在,无法打印收费凭证!");
                    return;
                }
                //Common_XH theCamera_XH = new Common_XH();
                //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                PrintManager print = new PrintManager();
                print.InitReport("自助医技预约扣费凭证");
                print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                print.AddParam("性别", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString());
                print.AddParam("出生日期", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString());
                print.AddParam("年龄", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString());
                print.AddParam("卡余额", SkyComm.cardBlance);
                print.AddParam("卡号", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString());
                print.AddParam("本次扣费金额", sumMoney.ToString());
                print.AddParam("重打", "");
                print.AddParam("诊疗号", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString());
                print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                print.AddData(dsPrint.Tables[0], "report");
                PrintManager.CanDesign = true;
                //print.PreView();
                print.Print();
                print.Dispose();
                Thread.Sleep(100);
                LogService.GlobalInfoMessage("自助医技预约扣费凭证");
            }
            catch (Exception lex)
            {
                if (lex.Message.IndexOf("灾难性") > 0)
                {
                    SkyComm.ShowMessageInfo(lex.Message + ": 打印机连接失败,请检查!");
                }
                else
                {
                    SkyComm.ShowMessageInfo(lex.Message);
                }
            }
        }
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="strQueue"></param>
        /// <param name="Reservestarttime"></param>
        /// <param name="officeID"></param>
        private void PrintPDPZ(string reserveItem)
        {
            try
            {

                DataSet dsXML = new DataSet();
                DataTable dtXML = this.CreateDataTable("预约").Clone();
                DataRow dr = dtXML.NewRow();
                dr["类别"] = "门诊";
                dr["类别名称"] = "挂号号";
                dr["类别值"] = dtRev.Rows[0]["挂号号"].ToString();
                dr["医院名称"] = SysOperatorInfo.CustomerName;
                dr["姓名"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                dr["性别"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                dr["年龄"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + " " + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                dr["检查项目"] = reserveItem;
                dr["科室"] = exOfficeName;
                dr["诊室"] = GroupName;
                dr["排队号"] = queueNO;
                dr["预约时间"] = reserveDate;
                dtXML.Rows.Add(dr);
                dsXML.Tables.Add(dtXML.Copy());
                dsXML.Tables[0].TableName = "参数";
                dsXML.Tables.Add(this.dtRev.Copy());
                dsXML.Tables[1].TableName = "数据集";

                dsXML.WriteXml(Application.StartupPath + @"\\ReportXml\\自助医技预约凭证" + this.dtRev.Rows[0]["诊疗号"].ToString() + ".xml");

                string path = Application.StartupPath + @"\\Reports\\自助医技预约凭证.frx";

                if (System.IO.File.Exists(path) == false)
                {
                    SkyComm.ShowMessageInfo("自助医技预约凭证不存在,无法打印预约凭证!");
                    return;
                }
                //Common_XH theCamera_XH = new Common_XH();
                //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                PrintManager print = new PrintManager();
                print.InitReport("自助医技预约凭证");
                print.AddParam("类别", "门诊");
                print.AddParam("类别名称", "挂号号");
                print.AddParam("类别值", dtRev.Rows[0]["挂号号"].ToString());
                print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                print.AddParam("性别", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString());
                print.AddParam("年龄", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + " " + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString());
                print.AddParam("检查项目", reserveItem);
                print.AddParam("检查目的", "");
                print.AddParam("科室", exOfficeName);
                print.AddParam("医师", "");
                print.AddParam("诊室", GroupName);//组名
                print.AddParam("排队号", queueNO);
                print.AddParam("预约时间", reserveDateNew);
                print.AddParam("重打", "");
                print.AddData(this.dtRev, "report");
                PrintManager.CanDesign = true;
                //print.PreView();
                print.Print();
                print.Dispose();
                Thread.Sleep(100);
                LogService.GlobalInfoMessage("自助医技预约凭证打印");
            }
            catch (Exception lex)
            {
                if (lex.Message.IndexOf("灾难性") > 0)
                {
                    SkyComm.ShowMessageInfo(lex.Message + ": 打印机连接失败,请检查!");
                }
                else
                {
                    SkyComm.ShowMessageInfo(lex.Message);
                }
            }
        }
        private void PrintAll(decimal ye, decimal sumMoney, DataSet dsPrint, string reserveItem)
        {
            try
            {
                DataSet dsXML = new DataSet();
                DataTable dtXML = this.CreateDataTable("全部").Clone();
                DataRow dr = dtXML.NewRow();

                if (dsPrint.Tables.Count == 0)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("CHARGEITEM", typeof(string));
                    dtTemp.Columns.Add("TOTALMONEY", typeof(decimal));
                    dtTemp.Columns.Add("AMOUNT", typeof(decimal));
                    dsPrint.Tables.Add(dtTemp.Copy());
                }

                dr["医院名称"] = SysOperatorInfo.CustomerName;
                dr["姓名"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                dr["性别"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                dr["年龄"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + " " + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                dr["出生日期"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString();
                dr["卡余额"] = SkyComm.cardBlance;
                dr["卡号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                dr["本次扣费金额"] = sumMoney.ToString();
                dr["诊疗号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                dr["操作员"] = SysOperatorInfo.OperatorCode;
                dr["操作员姓名"] = SysOperatorInfo.OperatorName;
                dr["类别"] = "门诊";
                dr["类别名称"] = "挂号号";
                dr["类别值"] = dtRev.Rows[0]["挂号号"].ToString();
                dr["检查项目"] = reserveItem;
                dr["科室"] = exOfficeName;
                dr["诊室"] = GroupName;
                dr["排队号"] = QueueID;
                dr["预约时间"] = reserveDate;
                dtXML.Rows.Add(dr);
                dsXML.Tables.Add(dtXML.Copy());
                dsXML.Tables[0].TableName = "参数";
                dsXML.Tables.Add(dsPrint.Tables[0].Copy());
                dsXML.Tables[1].TableName = "reportCost";
                dsXML.Tables.Add(this.dtRev.Copy());
                dsXML.Tables[2].TableName = "reportReserve";


                dsXML.WriteXml(Application.StartupPath + @"\\ReportXml\\预约并扣费凭证" + this.dtRev.Rows[0]["诊疗号"].ToString() + ".xml");
                string path = Application.StartupPath + @"\\Reports\\预约并扣费凭证.frx";

                if (System.IO.File.Exists(path) == false)
                {
                    SkyComm.ShowMessageInfo("预约并扣费凭证不存在,无法打印凭证!");
                    return;
                }
                //Common_XH theCamera_XH = new Common_XH();
                //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                PrintManager print = new PrintManager();
                print.InitReport("预约并扣费凭证");

                string alter = string.Empty;                
                if (this.isupdatereserve)
                {
                    alter = "改签";
                }
                print.AddParam("是否改签", alter);
                print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                print.AddParam("性别", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString());
                print.AddParam("出生日期", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString());
                print.AddParam("年龄", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString());
                print.AddParam("卡余额", SkyComm.cardBlance);
                print.AddParam("卡号", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString());
                print.AddParam("本次扣费金额", sumMoney.ToString());
                print.AddParam("诊疗号", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString());
                print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                print.AddParam("类别", "门诊");
                print.AddParam("类别名称", "挂号号");
                print.AddParam("类别值", dtRev.Rows[0]["挂号号"].ToString());
                print.AddParam("检查项目", reserveItem);
                print.AddParam("科室", exOfficeName);
                print.AddParam("诊室", GroupName);//组名
                print.AddParam("排队号", QueueID);
                print.AddParam("预约时间", reserveDateNew);
                print.AddData(dsPrint.Tables[0], "reportCost");
                print.AddData(this.dtRev, "reportReserve");
                PrintManager.CanDesign = true;
                //print.PreView();
                print.Print();

                print.Dispose();
                Thread.Sleep(100);
                //LogService.GlobalInfoMessage("预约并扣费凭证");
            }
            catch (Exception lex)
            {
                if (lex.Message.IndexOf("灾难性") > 0)
                {
                    SkyComm.ShowMessageInfo(lex.Message + ": 打印机连接失败,请检查!");
                }
                else
                {
                    SkyComm.ShowMessageInfo(lex.Message);
                }
            }
        }
        private DataTable CreateDataTable(string type)
        {
            if (type == "扣费")
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("医院名称");
                dtTemp.Columns.Add("姓名");
                dtTemp.Columns.Add("性别");
                dtTemp.Columns.Add("出生日期");
                dtTemp.Columns.Add("年龄");
                dtTemp.Columns.Add("卡余额");
                dtTemp.Columns.Add("卡号");
                dtTemp.Columns.Add("本次扣费金额");
                dtTemp.Columns.Add("诊疗号");
                dtTemp.Columns.Add("操作员");
                dtTemp.Columns.Add("操作员姓名");
                return dtTemp;
            }
            else if (type == "全部")
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("医院名称");
                dtTemp.Columns.Add("诊疗号");
                dtTemp.Columns.Add("姓名");
                dtTemp.Columns.Add("性别");
                dtTemp.Columns.Add("年龄");
                dtTemp.Columns.Add("出生日期");
                dtTemp.Columns.Add("卡余额");
                dtTemp.Columns.Add("卡号");
                dtTemp.Columns.Add("本次扣费金额");
                dtTemp.Columns.Add("类别");
                dtTemp.Columns.Add("类别名称");
                dtTemp.Columns.Add("类别值");
                dtTemp.Columns.Add("检查项目");
                dtTemp.Columns.Add("科室");
                dtTemp.Columns.Add("诊室");
                dtTemp.Columns.Add("排队号");
                dtTemp.Columns.Add("预约时间");
                dtTemp.Columns.Add("操作员");
                dtTemp.Columns.Add("操作员姓名");
                return dtTemp;
            }
            else
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("类别");
                dtTemp.Columns.Add("类别名称");
                dtTemp.Columns.Add("类别值");
                dtTemp.Columns.Add("医院名称");
                dtTemp.Columns.Add("姓名");
                dtTemp.Columns.Add("性别");
                dtTemp.Columns.Add("年龄");
                dtTemp.Columns.Add("检查项目");
                dtTemp.Columns.Add("科室");
                dtTemp.Columns.Add("诊室");
                dtTemp.Columns.Add("排队号");
                dtTemp.Columns.Add("预约时间");
                return dtTemp;
            }
        }
        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 排班时段绑定
        /// <summary>
        /// 根据组号、时间获取预约(报到)时间选择列面
        /// </summary>
        private bool GetDataForReserveDate()
        {
            try
            {
                this.lblWarn.Visible = false;
                TMedgroupRecordFacade medGroupRecordFacade = new TMedgroupRecordFacade();
                TMedgroupDetailFacade medGroupDetailFacade = new TMedgroupDetailFacade();
                DateTime dtNow = new CommonFacade().GetServerDateTime();
                DateTime date = this.monthCalendar1.SelectedDates[0];

                if (GroupID.Length < 1)
                {
                    this.gridControlDate.DataSource = null;
                    return false;
                }
                //查询 医技预约组排班表
                DataTable dtRecord = medGroupRecordFacade.FindByGroupIDAndDate(GroupID, date.ToShortDateString(), date.ToShortDateString());
                if (dtRecord != null && dtRecord.Rows.Count > 0)
                {
                    dtRecord = dtRecord.Select(" FLAG <>'是' ").CopyToDataTable();//过滤停用的数据
                }
                else
                {
                    this.lblWarn.Visible = true;
                    //SkyComm.ShowMessageInfo(string.Format("未查询到{0}的医技预约组排班记录（请核对是否维护或者已经停用）！", date.ToShortDateString()));
                    this.gridControlDate.DataSource = null;
                    return false;
                }
                if (dtRecord.Rows.Count != 1)
                {
                    this.lblWarn.Visible = true;
                    //SkyComm.ShowMessageInfo(string.Format("{0}的医技预约组排班记录存在多条数据，维护有误,请重新维护！", date.ToShortDateString()));
                    this.gridControlDate.DataSource = null;
                    return false;
                }
                //查询 医技预约组排班明细表
                DataTable dtDetail = new DataTable();
                dtDetail = medGroupDetailFacade.FindByMedGroupRecordIDNew(dtRecord.Rows[0]["MEDGROUPRECORDID"].ToString());
                if (dtDetail == null || dtDetail.Rows.Count < 1)
                {
                    this.lblWarn.Visible = true;
                    //SkyComm.ShowMessageInfo(string.Format("未查询到{0}的医技预约组排班明细记录！", date.ToShortDateString()));
                    this.gridControlDate.DataSource = null;
                    return false;
                }
                //构建预约时段 显示的数据
                if (dtReserveDateList == null)
                {
                    dtReserveDateList = new DataTable();
                    dtReserveDateList.Columns.Add("PERIOD", typeof(string));
                    dtReserveDateList.Columns.Add("reserveEnableNo", typeof(int));//可预约
                    dtReserveDateList.Columns.Add("reservedNo", typeof(int));//已预约
                    dtReserveDateList.Columns.Add("reserveEnableNew", typeof(string));
                    dtReserveDateList.Columns.Add("MEDGROUPRDETAILID", typeof(int));//动态获取最大号用
                    dtReserveDateList.Columns.Add("STARTTIME", typeof(string));
                    dtReserveDateList.Columns.Add("ENDTIME", typeof(string));
                }
                dtReserveDateList.Clear();
                //dtReserveDateList.Rows.Add(new string[] { "8:00-9:00", "22", "2", "0" }); 
                // this.gridControlDate.DataSource = dtReserveDateList;
                int reserveEnableNo = 0; //可预约数
                int reservedNo = 0; //已预约数
                int reserveTotal = 0;//预约总数
                foreach (DataRow dr in dtDetail.Rows)
                {
                    if (date.Date > dtNow.Date) //离线预约
                    {
                        reserveTotal = (int)dr["BESPOKERERESENUM"];//离线预约限数
                        reservedNo = (int)dr["BESPOKEREMAIN"];//离线已预约数
                        reserveEnableNo = reserveTotal - reservedNo;//离线预约限数-离线已预约数 
                    }
                    else//在线预约
                    {
                        reserveTotal = (int)dr["BECLINICRESENUM"] + (int)dr["BESPOKERERESENUM"];//在线预约限数+离线预约限数
                        reservedNo = (int)dr["BECLINICMAKEMIN"] + (int)dr["BESPOKEREMAIN"];//在线已预约数+离线已预约数
                        reserveEnableNo = reserveTotal - reservedNo;
                    }
                    dtReserveDateList.Rows.Add(new string[]{
                        dr["STARTTIME"].ToString()+"-"+dr["ENDTIME"].ToString(),
                        reserveEnableNo.ToString(),
                        reservedNo.ToString(),
                        "余号:"+reserveEnableNo.ToString (),
                        dr["MEDGROUPRDETAILID"].ToString(),
                        dr["STARTTIME"].ToString(),
                        dr["ENDTIME"].ToString()
                        });
                }

                dtReserveDateList.AcceptChanges();
                this.gridControlDate.DataSource = dtReserveDateList;
                if (dtReserveDateList.Rows.Count == 0)
                {
                    this.lblWarn.Visible = true;
                }
                gridViewDate.Focus();
                return true;
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo(ex.Message);
                this.gridControlDate.DataSource = null;
                return false;
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

        #region 控件重绘

        private void backGroundPanelTrend1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gbSD_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.DodgerBlue, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.DodgerBlue, e.Graphics.MeasureString(gbSD.Text, gbSD.Font).Width + 8, 7, gbSD.Width - 2, 7);
            e.Graphics.DrawLine(Pens.DodgerBlue, 1, 7, 1, gbSD.Height - 2);
            e.Graphics.DrawLine(Pens.DodgerBlue, 1, gbSD.Height - 2, gbSD.Width - 2, gbSD.Height - 2);
            e.Graphics.DrawLine(Pens.DodgerBlue, gbSD.Width - 2, 7, gbSD.Width - 2, gbSD.Height - 2);
        }
        #endregion


        #region 新日期控件事件
        private void monthCalendar1_DaySelected(object sender, Pabo.Calendar.DaySelectedEventArgs e)
        {

            DateTime dtNow = new CommonFacade().GetServerDateTime();
            if (this.monthCalendar1.SelectedDates[0].Date < dtNow.Date || this.monthCalendar1.SelectedDates[0].Date > dtNow.AddDays(60).Date)
            {
                this.lblDate.Text = "请选择预约日期";
                this.gridControlDate.DataSource = null;
                this.lblWarn.Visible = true;
                return;
            }
            DateTime dtSelect = this.monthCalendar1.SelectedDates[0];
            this.lblDate.Text = dtSelect.Month + "月" + dtSelect.Day + "日 " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dtSelect.DayOfWeek);
            if (!GetDataForReserveDate()) return;
        }

        private void monthCalendar1_DayRender(object sender, Pabo.Calendar.DayRenderEventArgs e)
        {
            DateTime dtNow = new CommonFacade().GetServerDateTime();
            if (e.Date < dtNow.Date || e.Date > dtNow.AddDays(60).Date)
            {
                Pabo.Calendar.DateItem di = new Pabo.Calendar.DateItem();
                di.Date = e.Date;
                di.Enabled = false;
                this.monthCalendar1.Dates.Add(di);
            }
        }

        private void monthCalendar1_DayClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {
            if (this.monthCalendar1.SelectedDates.Count == 0)
            {
                this.lblDate.Text = "请选择预约日期";
                this.gridControlDate.DataSource = null;
                this.lblWarn.Visible = true;
                return;
            }
        }

        #endregion

        #region 高值耗材验证

        /// <summary>
        /// 高值耗材验证
        /// </summary>
        /// <param name="AccountDetailData">收费项目明细信息</param>
        /// <returns></returns>
        /// ZHOUHU ADD CASE:31590 2018101601  yyl UPDATE　CASE:33584 2019092701
        public bool CheckHValueMaterial(DetailAccountData detailAccountData)
        {
            bool result = true;
            int barCodeCount = 0;
            try
            {
                string IHMaterial = SystemInfo.SystemConfigs["门诊是否启用高值耗材接口"].DefaultValue;
                if ("1" == IHMaterial)
                {
                    string strWhere = string.Empty;
                    for (int i = 0; i < detailAccountData.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere += "ITEMID IN (";
                        }
                        strWhere += "'" + detailAccountData.Tables[0].Rows[i]["ITEMID"].ToString() + "',";
                        if (i == detailAccountData.Tables[0].Rows.Count - 1)
                        {
                            strWhere = strWhere.Remove(strWhere.Length - 1);
                            strWhere += ")";
                        }
                    }
                    SummaryInfoFacade summaryInfoFacade = new SummaryInfoFacade();
                    DataSet ds = summaryInfoFacade.Select(strWhere);
                    barCodeCount = ds.Tables[0].Select(" CODENO LIKE 'HC%' ").Length;

                    if (barCodeCount > 0)
                    {
                        result = false;
                        SkyComm.ShowMessageInfo("缴费信息中有高值耗材，请在门诊窗口进行缴费！");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

    }
}
