using AutoServiceManage.Inquire;
using AutoServiceManage.Common;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SdkService;
using TiuWeb.ReportBase;
using System.Threading;
using System.Collections;
using BusinessFacade.His.Register;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmCardSavingCash : Form
    {
        #region 变量
        CardAuthorizationFacade eCardAuthorizationFacade = null;

        CardSavingFacade eCardSavingFacade = null;

        CardAuthorizationData eCardAuthorizationData = null;

        public decimal RechargeMoney = 0;  //充值金额
        public int Savingsucceed = 0;       
        /// <summary>
        /// 调用类型：0：主界面，1：取号，缴费,2:办卡
        /// </summary>
        public int CallType { get; set; }

        private IMoneyService MoneyServer;

        private int sec = 90;

        #endregion

        #region 窗体构造函数,LOAD,关闭事件

        public FrmCardSavingCash()
        {
            InitializeComponent();

            switch (AutoHostConfig.CashBoxType)
            { 
                case "XUHUIMEI":
                    MoneyServer = new CashCodeMoney_XH();
                    break;

                default :
                    SkynetMessage.MsgInfo("纸币器的配置不正确，请与管理员联系！");
                    //MoneyServer = new CashMoneyTest();
                    break;
            }
            btnEnd.Enabled = false;
            lblTime.Visible = false;
        }

        private void FrmCardSavingCash_Load(object sender, EventArgs e)
        {            
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            try
            {
                eCardAuthorizationData = new CardAuthorizationData();

                if (eCardAuthorizationFacade == null)
                {
                    eCardAuthorizationFacade = new CardAuthorizationFacade();
                }

                eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);

                string patientName = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_PATIENTNAME].ToString();
                string sex = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_SEX].ToString();
                string identity = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYCARD].ToString();

                lblPatientInfo.Text = patientName + " " + sex + " 身份证：" + SkyComm.ConvertIdCard(identity);

                GetMoeny();
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }

        /// <summary>
        /// 获取卡余额
        /// </summary>
        private void GetMoeny()
        {
            //获取诊疗号
            string diagnoseId = SkyComm.DiagnoseID;
            //预交金 
            if (eCardSavingFacade == null)
            {
                eCardSavingFacade = new CardSavingFacade();
            }

            string yje = string.Empty;

            if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
            {
                yje = eCardSavingFacade.FindBalanceMoneyByDiagnoseID(diagnoseId).ToString("0.00");
            }
            else
            {
                yje = eCardSavingFacade.FindBalanceMoneyByDiagnoseID_New(diagnoseId, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString()).ToString("0.00");
            }
            //门诊未结
            //string wj = eCardAuthorizationFacade.FindClinicNoCheckoutMoney(diagnoseId).ToString("0.00");

            //门诊未缴费
            #region 查询未缴费的金额
            QuerySolutionFacade query = new QuerySolutionFacade();
            Hashtable ht = new Hashtable();
            string wj = "0.00";
            string strsql = " SELECT COALESCE(SUM(TOTALMONEY),0) TOTALMONEY FROM (" +
                " select SUM(C.TOTALMONEY) TOTALMONEY" +
                " from CLINICPHYSICIANRECIPE C" +
                " where C.DIAGNOSEID=:DIAGNOSEID AND RECIPESTATE = 0 and OPERATETIME >= :YPDATE " +
                " AND C.RECIPETYPE IN ('药品费','中草药','医材')" +
                " union all " +
                " SELECT SUM(D_SUMMARY_INFO.UNITPRICE * CLINICPHYSICIANRECIPE.AMOUNT * S_MEDORD_DETAIL.AMOUNT) AS TOTALMONEY" +
                " FROM CLINICPHYSICIANRECIPE,D_SUMMARY_INFO,S_MEDORD_DETAIL,S_MEDORD_MAIN " +
                " WHERE CLINICPHYSICIANRECIPE.RECIPECONTENT = S_MEDORD_DETAIL.MEDORDID AND S_MEDORD_DETAIL.ITEMID = D_SUMMARY_INFO.ITEMID AND" +
                "  CLINICPHYSICIANRECIPE.DIAGNOSEID=:DIAGNOSEID AND RECIPESTATE = 0 AND" +
                "  CLINICPHYSICIANRECIPE.RECIPETYPE <> '附加'  AND CLINICPHYSICIANRECIPE.RECIPECONTENT = S_MEDORD_MAIN.MEDORDID " +
                "  AND OPERATETIME >=:JZFDATE AND (ISCHANGEPRICE<> 1 OR ISCHANGEPRICE IS NULL)" +
                " union all " +
                " SELECT SUM(CLINICPHYSICIANRECIPE.UNITPRICE * CLINICPHYSICIANRECIPE.AMOUNT) AS TOTALMONEY" +
                " FROM CLINICPHYSICIANRECIPE " +
                " WHERE CLINICPHYSICIANRECIPE.DIAGNOSEID=:DIAGNOSEID AND RECIPESTATE = 0 AND" +
                "  CLINICPHYSICIANRECIPE.RECIPETYPE <> '附加' " +
                "  AND OPERATETIME >=:JZFDATE AND ISCHANGEPRICE = 1" +
                " union all  " +
                " select SUM(CLINICPHYSICIANRECIPE.UNITPRICE * CLINICPHYSICIANRECIPE.AMOUNT) TOTALMONEY" +
                " from CLINICPHYSICIANRECIPE  " +
                " where  RECIPESTATE = 0  AND CLINICPHYSICIANRECIPE.RECIPETYPE = '附加' " +
                "  AND CLINICPHYSICIANRECIPE.DIAGNOSEID=:DIAGNOSEID and OPERATETIME >=:JZFDATE ) AA ";

            string ypDate = SystemInfo.SystemConfigs["药品处方有效期"].DefaultValue;
            string jzfDate = SystemInfo.SystemConfigs["检治费处方有效期"].DefaultValue;

            CommonFacade commonFacade = new CommonFacade();
            DateTime dtCurrent = commonFacade.GetServerDateTime();

            ht.Add(":DIAGNOSEID", diagnoseId);
            ht.Add(":YPDATE", dtCurrent.Date.AddDays(Convert.ToInt32(ypDate) * -1));
            ht.Add(":JZFDATE", dtCurrent.Date.AddDays(Convert.ToInt32(jzfDate) * -1));

            try
            {
                lblNoPaymentCharge.Visible = false;
                decimal decNoCharge = 0;
                DataSet ds = query.ExeQuery(strsql, ht);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    decNoCharge = DecimalRound.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTALMONEY"]), 2);
                }
                wj = decNoCharge.ToString();
                lblwjf.Text = decNoCharge.ToString();

                if (decNoCharge != 0)
                {
                    lblNoPaymentCharge.Visible = true;
                }
                else
                {
                    lblNoPaymentCharge.Visible = false;
                }
            }
            catch (Exception ex)
            {
                SkynetMessage.MsgInfo("获取未交费信息出错：" + ex.Message);
            }

            #endregion

            lblwjf.Text = wj + "元";
            //余额
            lblye.Text = SkyComm.cardBlance.ToString();
        }

        private void FrmCardSavingCash_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
            timer1.Stop();
            timer2.Stop();
            if (ucTime1.Sec == 0)
            {
                Savingsucceed = 0;
            }
            try
            {
                Common_XH theCamera_XH = new Common_XH();
                theCamera_XH.DoorLightClose(LightTypeenum.纸币器);
                
            }
            catch (Exception exception)
            {

            }
        }

        #endregion               

        #region 开始现金预存
        private void btnStart_Click(object sender, EventArgs e)
        {
           
            backgroundWorker1.RunWorkerAsync();
            try
            {
                Common_XH theCamera_XH = new Common_XH();
                theCamera_XH.DoorLightOpen(LightTypeenum.纸币器, LightOpenTypeenum.闪烁);
                
            }
            catch (Exception exception)
            {

            }

            if (MoneyServer.OpenPort(SkyComm.cardInfoStruct.CardNo) == true)
            {
                //拍照
                switch (AutoHostConfig.ReadCardType)
                {
                    case "XUHUI":
                        AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                        camera.TakeCameraStart(SkyComm.cardInfoStruct.CardNo, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString(), AutoHostConfig.Machineno);

                        camera.DoorLightOpen(LightTypeenum.纸币器, LightOpenTypeenum.闪烁);
                        break;
                    default:
                        break;
                }

                ucTime1.timer1.Stop();
                btnStart.Enabled = false;
                lblTime.Visible = true;
                lblNoPaymentCharge.Enabled = false;
                timer1.Start();
                timer2.Start();
                this.timer3.Start();
                this.btnClose.Enabled = false;
                this.btnExit.Enabled = false;            
            }
            else
            {
                SkynetMessage.MsgInfo("钞箱设置没有初始化失败，请在窗口充值或者银行卡充值！");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {    
            //获取存钞明细
            if (MoneyServer == null)
            {
                timer1.Stop();
                return;
            }
            int InMoney = MoneyServer.GetInMoney(AutoHostConfig.Machineno, SysOperatorInfo.OperatorID);
            if (InMoney > 0)
            {
                //投入数量
                int intOld = Convert.ToInt32(lblAmount.Text) + 1;
                lblAmount.Text = intOld.ToString();

                //投入金额
                RechargeMoney += Convert.ToDecimal(InMoney);
                RechargeMoney = DecimalRound.Round(RechargeMoney, 2);
                lblMoney.Text = RechargeMoney.ToString();
                sec = 90;
            }

            //获取状态的
            int intCheckStatus = MoneyServer.GetStatus();
            //Skynet.LoggingService.LogService.GlobalInfoMessage("返回纸币器状态：" + intCheckStatus.ToString());
            if (intCheckStatus == 1)
            {
                btnEnd.Enabled = false;
                btnClose.Enabled = false;
                btnExit.Enabled = false;
            }
            else
            {
                if (RechargeMoney > 0)
                {
                    btnClose.Enabled = false;
                    btnExit.Enabled = false;
                    btnEnd.Enabled = true;
                }
                else
                {
                    btnEnd.Enabled = false;
                    if (this.timer3.Enabled == false)
                    {
                        btnClose.Enabled = true;
                        btnExit.Enabled = true;
                    }
                }
            }          
        }

        #endregion

        #region 结束现金预存

        private void lblCashStored_Click(object sender, EventArgs e)
        {
            btnEnd.Enabled = false;
            WaitDialogForm form = new WaitDialogForm("正在存钞中，请稍候...", "正在组织数据,请稍候......", new Size(240, 60));
            try
            {
                MoneyServer.NotAllowCashin();

                //拍照,纸币器
                switch (AutoHostConfig.ReadCardType)
                {
                    case "XUHUI":
                        AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                        camera.TakeCameraEnd();
                        camera.DoorLightClose(LightTypeenum.纸币器);
                        break;
                    default:
                        break;
                }

                timer2.Stop();
                timer1.Stop();

                #region 休眠3秒以后重新获取纸币金额
                Thread.Sleep(5000);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后休眠2秒重新获取存钞明细");
                int InMoney = MoneyServer.GetInMoney(AutoHostConfig.Machineno, SysOperatorInfo.OperatorID);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后重新获取存钞明细金额：" + InMoney);
                if (InMoney > 0)
                {
                    //投入数量
                    int intOld = Convert.ToInt32(lblAmount.Text) + 1;
                    lblAmount.Text = intOld.ToString();

                    //投入金额
                    RechargeMoney += Convert.ToDecimal(InMoney);
                    RechargeMoney = DecimalRound.Round(RechargeMoney, 2);
                    lblMoney.Text = RechargeMoney.ToString();
                }
                #endregion

                if (RechargeMoney == 0)
                {                    
                    btnClose_Click(null, null);
                    return;
                }        
                
                lblNoPaymentCharge.Enabled = true;
                ucTime1.timer1.Stop();
                if (eCardSavingFacade == null)
                {
                    eCardSavingFacade = new CardSavingFacade();
                }
                if (eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows.Count <= 0)
                {
                    MyAlert frm = new MyAlert(AlertTypeenum.信息, "此操作没有找到所要充值的卡信息！");
                    frm.ShowDialog();
                    return;
                }
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0].BeginEdit();
                //卡号
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID] = SkyComm.cardInfoStruct.CardNo;
                //充值时间
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATETIME] = new CommonFacade().GetServerDateTime();
                ////操作员
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATOR] = SysOperatorInfo.OperatorID;
                ////充值类型
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_SAVINGMODE] = 1;
                ////Add money

                Skynet.LoggingService.LogService.GlobalInfoMessage("现金发卡保存数据设置押金之前");
                if (CallType == 2)
                {
                   Decimal deposit= Convert.ToDecimal(SkyComm.getvalue("发卡工本费"));
                   eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = RechargeMoney - Convert.ToDecimal(SkyComm.dsCardType.Tables[0].Rows[0]["FEES"]) - deposit;
                }
                else
                {
                    eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = RechargeMoney;
                }
                Skynet.LoggingService.LogService.GlobalInfoMessage("现金发卡保存数据设置押金之后");
                //业务类型
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BUSSNESSTYPE] = "充值";
                //支付方式
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_MODETYPE] = SkyComm.AddMoneyCashMode;
                //单位
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_UNIT] = "";
                //支票号
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CHECKLOT] = "";

                //充值数据来源
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEYSOURCE] = AutoHostConfig.BankName;
            
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0].EndEdit();

                ReckonAccountTimeFacade reckonAccountsTimeFacade = new ReckonAccountTimeFacade();
                DateTime accountTime = reckonAccountsTimeFacade.GetEndTime(SysOperatorInfo.OperatorID, "门诊");
                if (Convert.ToDateTime(eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATETIME]) < accountTime)
                {
                    SkyComm.ShowMessageInfo("该时间段已经结帐，不能办理预交金业务，请重试！");
                    return;
                }

                DataSet cardSavingData = eCardSavingFacade.insertEntity(eCardAuthorizationData);                
                SkyComm.GetCardBalance();
                Skynet.LoggingService.LogService.GlobalInfoMessage("充值成功，调用类型：" + CallType);
                Savingsucceed = 1;
                if (CallType != 2)
                {
                    //打印充值凭证
                    PrintInfo("自助充值凭证", cardSavingData.Tables[0].Rows[0]["TRANSACTION_ID"].ToString(), RechargeMoney.ToString());
                }               
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("存钞失败：" + ex.Message);
                Skynet.LoggingService.LogService.GlobalInfoMessage("存钞失败：" + ex.Message);
            }
            finally
            {
                MoneyServer.ClosePort();
                form.Close();
                form.Dispose();
                
                btnEnd.Enabled = true;
            }

            Skynet.LoggingService.LogService.GlobalInfoMessage("充值成功，调用类型：" + CallType);
            if (CallType == 0)
            {
                FrmRechargeSuccessful frmRecharge = new FrmRechargeSuccessful();
                frmRecharge.RechargeMoney = RechargeMoney;
                frmRecharge.ShowDialog(this);
                frmRecharge.Dispose();
                SkyComm.CloseWin(this);
            }
            else if (CallType == 2)
            {
                SkyComm.CloseWin(this);
            }
            else
            {
                SkyComm.ShowMessageInfo("您成功充值" + RechargeMoney + "元！卡中余额" + SkyComm.cardBlance.ToString() + "");
                SkyComm.CloseWin(this);
            }
             //GetMoeny();
        }

        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money)
        {
            try
            {
                CardSavingFacade cardSavingFacade = new CardSavingFacade();
                DataSet cardSavingData = cardSavingFacade.FindByPrimaryKey(strTRANSACTION_ID);
                if (cardSavingData.Tables[0].Rows.Count > 0)
                {
                    cardSavingData.WriteXml(Application.StartupPath + @"\\ReportXml\\" + ReportName + SkyComm.DiagnoseID + "_" + strTRANSACTION_ID + ".xml");
                    if (!File.Exists(Application.StartupPath + @"\\Reports\\" + ReportName + ".frx"))
                    {
                        SkynetMessage.MsgInfo(ReportName + ".frx报表文件不存在，无法打印.");
                        return;
                    }
                    //Common_XH theCamera_XH = new Common_XH();
                    //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                    PrintManager print = new PrintManager();
                    print.InitReport(ReportName);
                    print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                    print.AddParam("收据号", strTRANSACTION_ID);
                    print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                    print.AddParam("卡余额", SkyComm.cardBlance);
                    print.AddParam("充值金额", Money);
                    print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                    print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                    print.AddData(cardSavingData.Tables[0], "report");

                    PrintManager.CanDesign = true;

                    print.Print();
                    print.Dispose();
                    Thread.Sleep(100);
                }
            }
            catch (Exception lex)
            {
                if (lex.Message.IndexOf("灾难性") > 0)
                {
                    SkynetMessage.MsgInfo(lex.Message + ": 打印机连接失败,请检查!");
                }
                else
                {
                    SkynetMessage.MsgInfo(lex.Message);
                }
            }
        }

        #endregion               

        #region 未缴费查询

        private void lblNoPaymentCharge_Click(object sender, EventArgs e)
        {
            ucTime1.timer1.Stop();
            timer2.Stop();            
            timer1.Stop();
            FrmNoPayment frm = new FrmNoPayment();
            if (frm.ShowDialog() == DialogResult.Cancel)
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
            }
            timer2.Start();
            
            timer1.Start();
        }

        #endregion

        #region 返回，退出
        private void btnClose_Click(object sender, EventArgs e)
        {
            MoneyServer.NotAllowCashin();            
            
            timer2.Stop();                        
            timer1.Stop();

            //wangchao 点击返回或退出之后，将按钮置为不可用状态，防止反复点击。
            btnClose.Enabled = false;
            btnExit.Enabled = false;

            //拍照,纸币器
            switch (AutoHostConfig.ReadCardType)
            {
                case "XUHUI":
                    AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                    camera.TakeCameraEnd();
                    camera.DoorLightClose(LightTypeenum.纸币器);
                    break;
                default:
                    break;
            }

            if (btnStart.Enabled == false)
            {
                
                #region 休眠3秒以后重新获取纸币金额
                Thread.Sleep(3000);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后休眠2秒重新获取存钞明细,开始获取状态");
                int intCheckStatus = MoneyServer.GetStatus();
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后休眠2秒重新获取存钞明细,状态：" + intCheckStatus);
                int InMoney = MoneyServer.GetInMoney(AutoHostConfig.Machineno, SysOperatorInfo.OperatorID);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后重新获取存钞明细金额：" + InMoney);
                if (InMoney > 0)
                {
                    //投入数量
                    int intOld = Convert.ToInt32(lblAmount.Text) + 1;
                    lblAmount.Text = intOld.ToString();

                    //投入金额
                    RechargeMoney += Convert.ToDecimal(InMoney);
                    RechargeMoney = DecimalRound.Round(RechargeMoney, 2);
                    lblMoney.Text = RechargeMoney.ToString();
                    MoneyServer.AllowCashin();
                    btnClose.Enabled = false;
                    btnExit.Enabled = false;
                    btnEnd.Enabled = true;
                    timer1.Start();
                    timer2.Start();  
                    return;
                }
                #endregion
            }
            MoneyServer.ClosePort();
            if (RechargeMoney == 0)
            {
                ucTime1.timer1.Stop();
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MoneyServer.NotAllowCashin();
            timer2.Stop();           
            timer1.Stop();

            //wangchao 点击返回或退出之后，将按钮置为不可用状态，防止反复点击。
            btnClose.Enabled = false;
            btnExit.Enabled = false;

            //拍照,纸币器
            switch (AutoHostConfig.ReadCardType)
            {
                case "XUHUI":
                    AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                    camera.TakeCameraEnd();
                    camera.DoorLightClose(LightTypeenum.纸币器);
                    break;
                default:
                    break;
            }

            if (btnStart.Enabled == false)
            {
                #region 休眠3秒以后重新获取纸币金额
                Thread.Sleep(3000);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后休眠2秒重新获取存钞明细,开始获取状态");
                int intCheckStatus = MoneyServer.GetStatus();
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后休眠2秒重新获取存钞明细,状态：" + intCheckStatus);
                int InMoney = MoneyServer.GetInMoney(AutoHostConfig.Machineno, SysOperatorInfo.OperatorID);
                Skynet.LoggingService.LogService.GlobalInfoMessage("在结束预存之后重新获取存钞明细金额：" + InMoney);
                if (InMoney > 0)
                {
                    //投入数量
                    int intOld = Convert.ToInt32(lblAmount.Text) + 1;
                    lblAmount.Text = intOld.ToString();

                    //投入金额
                    RechargeMoney += Convert.ToDecimal(InMoney);
                    RechargeMoney = DecimalRound.Round(RechargeMoney, 2);
                    lblMoney.Text = RechargeMoney.ToString();
                    MoneyServer.AllowCashin();

                    btnClose.Enabled = false;
                    btnExit.Enabled = false;
                    btnEnd.Enabled = true;
                    timer1.Start();
                    timer2.Start();       

                    return;
                }
                MoneyServer.ClosePort();
                #endregion
            }
            
            if (RechargeMoney == 0)
            {
                SkyComm.CloseWin(this);
            }
        }

        #endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
            sec = sec - 1;
            lblTime.Text = sec.ToString();
            if (sec == 3)
            {
                //btnEnd.Enabled = false;
                MoneyServer.NotAllowCashin();
            }

            if (sec == 0)
            {
                lblCashStored_Click(null, null);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请放入单张纸币!");
            voice.EndJtts();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.btnClose.Enabled = true;
            this.btnExit.Enabled = true;
            this.timer3.Enabled = false;
        }
    }
}
