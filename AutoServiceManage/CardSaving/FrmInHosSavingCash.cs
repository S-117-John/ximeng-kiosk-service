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
using AutoServiceManage.Presenter;
using EntityData.His.Inpatient;
using BusinessFacade.His.Inpatient;
using Skynet.LoggingService;
using BusinessFacade.His.Register;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmInHosSavingCash : Form
    {
        #region 变量
        CardAuthorizationFacade eCardAuthorizationFacade = null;

        CardSavingFacade eCardSavingFacade = null;

        CardAuthorizationData eCardAuthorizationData = null;

        public decimal RechargeMoney = 0;  //充值金额
        public int Savingsucceed = 0;       

        private IMoneyService MoneyServer;

        private int sec = 90;

        public DataSet inHosData { get; set; }

        #endregion

        #region 窗体构造函数,LOAD,关闭事件

        public FrmInHosSavingCash()
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
            string projectType = SkyComm.getvalue("项目版本标识");
            if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
            {
                SkyComm.ShowMessageInfo("提示：系统可接受50元、100元的纸币。");
            }

            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            try
            {
                if (!BindPage() || !CheckInHosSaving())
                {
                    SkyComm.CloseWin(this);
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用现金预存LOAD失败：" + ex.Message);
                throw ex;
            }
        }

        private void FrmCardSavingCash_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
            timer1.Stop();
            timer2.Stop();
        }

        private bool BindPage()
        {
            try
            {
                if (inHosData == null)
                {
                    InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
                    inHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                }
                if (inHosData != null && inHosData.Tables.Count != 0 && inHosData.Tables[0].Rows.Count != 0)
                {
                    if (inHosData.Tables[0].Rows[0]["INHOSSTATE"].ToString() == "1")
                    {
                        SkyComm.ShowMessageInfo("您已结算，不能继续预交款操作！");
                        return false;
                    }
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用现金预存LOAD_1");
                    eCardAuthorizationData = new CardAuthorizationData();

                    if (eCardAuthorizationFacade == null)
                    {
                        eCardAuthorizationFacade = new CardAuthorizationFacade();
                    }
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用现金预存LOAD_2");
                    eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用现金预存LOAD_3");
                    string patientName = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_PATIENTNAME].ToString();
                    string sex = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_SEX].ToString();
                    string identity = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYCARD].ToString();

                    lblPatientInfo.Text = patientName + " " + sex + " 身份证：" + SkyComm.ConvertIdCard(identity);
                    lblye.Text = Convert.ToDecimal(inHosData.Tables[0].Rows[0]["BALANCEMONEY"].ToString()).ToString("0.00##");
                    return true;
                }
                else
                {
                    SkyComm.ShowMessageInfo("未找到您的住院信息!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("患者信息绑定失败，现金预存功能暂不能使用。请与医院工作人员联系!");
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用现金预存LOAD失败：" + ex.Message);
                return false;
            }
        }

        #endregion               

        #region 开始现金预存
        private void btnStart_Click(object sender, EventArgs e)
        {
           
            backgroundWorker1.RunWorkerAsync();
            if (MoneyServer.OpenPort(SkyComm.cardInfoStruct.CardNo) == true)
            {
                //拍照
                switch (AutoHostConfig.ReadCardType)
                {
                    case "XUHUI":
                    case "XUHUIM1":
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
            bool errorFlag = false;//错误标识
            WaitDialogForm form = new WaitDialogForm("正在存钞中，请稍候...", "正在组织数据,请稍候......", new Size(240, 60));
            try
            {
                MoneyServer.NotAllowCashin();

                //拍照,纸币器
                switch (AutoHostConfig.ReadCardType)
                {
                    case "XUHUI":
                    case "XUHUIM1":
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

                ucTime1.timer1.Stop();

                #region 预交金充值
                AdvanceRecordData AdvData = new AdvanceRecordData();
                string strlsh = DateTime.Now.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + GenValidateCode();
                DataRow dr = AdvData.Tables[0].NewRow();
                dr[AdvanceRecordData.H_ADVANCE_RECORD_ADVANCEID] = "新增";
                dr[AdvanceRecordData.H_ADVANCE_RECORD_BUSINESSBANK] = strlsh;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_CANCELMARK] = 0;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_CHEQUEID] = "";
                dr[AdvanceRecordData.H_ADVANCE_RECORD_CURRENTINHOSMARK] = inHosData.Tables[0].Rows[0]["CURRENTINHOSMARK"].ToString(); ;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_INHOSID] = inHosData.Tables[0].Rows[0]["INHOSID"].ToString();
                dr[AdvanceRecordData.H_ADVANCE_RECORD_OFFICEID] = inHosData.Tables[0].Rows[0]["INHOSOFFICEID"].ToString();//this.txtZyks.Text;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATEDATE] = new CommonFacade().GetServerDateTime();
                dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATORID] = SysOperatorInfo.OperatorID;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMODEID] = SkyComm.getvalue("住院预交金充值方式_现金").ToString();
                dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMONEY] = RechargeMoney;
                dr[AdvanceRecordData.H_ADVANCE_RECORD_RECEIPTID] = string.Empty;
                AdvData.Tables[0].Rows.Add(dr);
                AdvanceRecordFacade theAdvanceRecordFacade = new AdvanceRecordFacade();
                string advanceid = string.Empty;
                try
                {
                    advanceid = theAdvanceRecordFacade.insertEntity(AdvData, false);
                    if (advanceid == "")
                    {
                        SkyComm.ShowMessageInfo("住院预交金充值失败,请与医院相关人员联系!");
                        errorFlag = true;
                    }
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("收预交款失败:" + ex.Message);
                    if (ex.Message.IndexOf("您已经没有可用发票") >= 0)
                    {
                        SkyComm.ShowMessageInfo("住院预交金充值失败,请与医院相关人员联系!");
                        errorFlag = true;
                    }
                }
                #endregion
                Skynet.LoggingService.LogService.GlobalInfoMessage("充值成功");
                if (advanceid != "")
                {
                    string receiptID = theAdvanceRecordFacade.GetReceiptIDByAdvanceID(advanceid);
                    
                    //打印充值凭证
                    PrintInfo("住院预交金充值凭证", receiptID, RechargeMoney.ToString(),advanceid);
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("存钞失败：" + ex.Message);
                errorFlag = true;
                Skynet.LoggingService.LogService.GlobalInfoMessage("存钞失败：" + ex.Message);
            }
            finally
            {
                MoneyServer.ClosePort();
                form.Close();
                form.Dispose();

                btnEnd.Enabled = true;
            }
            if (!errorFlag)
            {
                decimal old_YE = Convert.ToDecimal(inHosData.Tables[0].Rows[0]["BALANCEMONEY"].ToString());
                SkyComm.ShowMessageInfo("您成功充值" + RechargeMoney + "元！住院预交金余额" + (RechargeMoney + old_YE).ToString("0.00##") + "元！");
            }
            else
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("住院预交金现金充值操作失败：患者诊疗号【" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString() + "】,患者姓名【" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString() + "】,现金充值金额【" + RechargeMoney + "】元");
            }
            SkyComm.CloseWin(this);
        }
        public string GenValidateCode()
        {
            Random random = new Random();
            int j = random.Next(1000, 9999);
            return j.ToString();

        }
        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money,string advanceid)
        {
            try
            {
                MoneyTransferPresenter moneyTransferPresenter = new MoneyTransferPresenter();

                

                //CardSavingFacade cardSavingFacade = new CardSavingFacade();
                //DataSet cardSavingData = cardSavingFacade.FindByPrimaryKey(strTRANSACTION_ID);
                if (inHosData.Tables[0].Rows.Count > 0)
                {
                    moneyTransferPresenter.addDatas(inHosData, advanceid, "现金");
                    inHosData.WriteXml(Application.StartupPath + @"\\ReportXml\\" + ReportName + SkyComm.DiagnoseID + "_" + strTRANSACTION_ID + ".xml");
                    if (!File.Exists(Application.StartupPath + @"\\Reports\\" + ReportName + ".frx"))
                    {
                        SkynetMessage.MsgInfo(ReportName + ".frx报表文件不存在，无法打印.");
                        return;
                    }
                    decimal old_YE = Convert.ToDecimal(inHosData.Tables[0].Rows[0]["BALANCEMONEY"].ToString());
                    decimal reMoney = Convert.ToDecimal(Money);

                    //Common_XH theCamera_XH = new Common_XH();
                    //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                    PrintManager print = new PrintManager();
                    print.InitReport(ReportName);
                    print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                    print.AddParam("收据号", strTRANSACTION_ID);
                    print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                    print.AddParam("预交金余额", (old_YE + reMoney).ToString("0.00##"));
                    print.AddParam("充值金额", Money);
                    print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                    print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                    print.AddData(inHosData.Tables[0], "report");

                    //PrintManager.CanDesign = true;

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
                case "XUHUIM1":
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
                Thread.Sleep(2000);
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
                case "XUHUIM1":
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
                Thread.Sleep(2000);
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
        #region 检查当前自助机及病人是否可以进行住院预存
        private bool CheckInHosSaving()
        {
            try
            {
                string sql = "SELECT ID,STARTNUMBER,CURRNUMBER,ENDNUMBER,SURPLUSNUMBER FROM T_OPERATOR_INVOICE WHERE INVOICETYPE=:INVOICETYPE  AND OPERATORID=:OPERATORID AND SURPLUSNUMBER>'0'  AND CURRNUMBER<=ENDNUMBER   ORDER BY ID";
                Hashtable para = new Hashtable();
                para.Add(":INVOICETYPE", "预交款发票");
                para.Add(":OPERATORID", SysOperatorInfo.OperatorID);
                QuerySolutionFacade facadem = new QuerySolutionFacade();
                DataSet dsAdvance = facadem.ExeQuery(sql, para);
                if (dsAdvance == null || dsAdvance.Tables.Count == 0 || dsAdvance.Tables[0].Rows.Count == 0)
                {
                    SkyComm.ShowMessageInfo("该自助机无可用发票，请到其他自助机进行预存操作!");
                    return false;
                }
                Int64 currnumber = Convert.ToInt64(dsAdvance.Tables[0].Rows[0]["CURRNUMBER"]);
                if (currnumber > Convert.ToInt64(dsAdvance.Tables[0].Rows[0]["ENDNUMBER"]))
                {
                    SkyComm.ShowMessageInfo("该自助机已无可用发票，请到其他自助机进行预存操作!");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("检测是否可进行住院预存方法调用失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("该自助机暂无法进行住院预存操作，请到其他自助机进行住院预存!");
                return false;
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
