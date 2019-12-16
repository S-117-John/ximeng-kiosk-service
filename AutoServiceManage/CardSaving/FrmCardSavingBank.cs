using AutoServiceManage.Inquire;
using AutoServiceManage.Common;
using AutoServiceManage.Inc;
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
using TiuWeb.ReportBase;
using System.Threading;
using AutoServiceSDK.POSInterface;
using System.Collections;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmCardSavingBank : Form
    {
        #region 变量
        /// <summary>
        /// 调用类型：0：主界面，1：取号，缴费,2:办卡
        /// </summary>
        public int CallType { get; set; }

        CardAuthorizationFacade eCardAuthorizationFacade = null;

        CardSavingFacade eCardSavingFacade = null;

        CardAuthorizationData eCardAuthorizationData = null;
        public decimal RechargeMoney = 0;  //充值金额
        public int Savingsucceed = 0;
        #endregion

        #region 构造函数，LOAD
        public FrmCardSavingBank()
        {
            InitializeComponent();
        }

        private void FrmCardSavingBank_Load(object sender, EventArgs e)
        {
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
                if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
                ucTime1.Sec = 60;

                ucTime1.timer1.Start();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmCardSavingBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //请插入您的银行卡，
        }

        #endregion

        #region 返回，退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
                
        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        #endregion

        #region 银行卡预存
        private void lblBankStored_Click(object sender, EventArgs e)
        {
            lblBankStored.Enabled = false;
            ucTime1.timer1.Stop();
                                   
            FrmMoneyInput frm = new FrmMoneyInput();
            frm.CallType = CallType;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RechargeMoney = DecimalRound.Round(frm.inputMoney, 2);
                frm.Dispose();                
            }
            else
            {                
                ucTime1.timer1.Start();
                lblBankStored.Enabled = true;
                return;
            }

            //调用POS机推秆程序打开
            AutoServiceSDK.SdkService.Common_XH theCommon_XH = null;
            if (AutoHostConfig.ReadCardType == "XUHUI")
            {
                theCommon_XH = new AutoServiceSDK.SdkService.Common_XH();
                theCommon_XH.PosDoor(0);
            }

            try
            {
                Saving(RechargeMoney);

            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行卡充值失败：" + ex.Message);
            }
            finally
            {
                if (AutoHostConfig.ReadCardType == "XUHUI")
                {
                    theCommon_XH.PosDoor(1);
                }
                lblBankStored.Enabled = true;
            }


        }
        
        private void Saving(decimal money)
        {
            if (eCardSavingFacade == null)
            {
                eCardSavingFacade = new CardSavingFacade();
            }

            if (SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows.Count <= 0)
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

            Skynet.LoggingService.LogService.GlobalInfoMessage("发卡保存数据设置押金之前");
            ////Add money
            if (CallType == 2)
            {                
                Decimal deposit = Convert.ToDecimal(SkyComm.getvalue("发卡工本费"));
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = money - Convert.ToDecimal(SkyComm.dsCardType.Tables[0].Rows[0]["FEES"]) - deposit;
            }
            else
            {
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = money;  //此处需调接口
            }
            Skynet.LoggingService.LogService.GlobalInfoMessage("发卡保存数据设置押金之后");
            //业务类型
            eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BUSSNESSTYPE] = "充值";
            //支付方式
            eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_MODETYPE] = SkyComm.AddMoneyPosMode;
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
            DataSet cardSavingData = new DataSet();
            POSBase Posfac = IPOSFactory.CreateIPOS(AutoHostConfig.PosInterfaceType);
            if (Posfac == null)
            {
                SkyComm.ShowMessageInfo("银联POS接口配置不正确，请与管理员联系！");
                return;
            }
            ValidateCode vc = new ValidateCode();
            string HisSeqNo = string.Empty;

            HisSeqNo = DateTime.Now.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + vc.GenValidateCode(4);
            Hashtable BankTranht = new Hashtable();
            BankTranht.Add("SEQNO", HisSeqNo);
            BankTranht.Add("MONEY", money);
            BankTranht.Add("OPERATORID", SysOperatorInfo.OperatorCode);
            BankTranht.Add("POSNO", AutoHostConfig.PosNo);
            BankTranht.Add("DIAGNOSEID", SkyComm.DiagnoseID);
            BankTranht.Add("CARDID", SkyComm.cardInfoStruct.CardNo);
            Posfac.Trans("1", BankTranht);
            if (BankTranht["CARDNO"] != null)
            {
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CHECKLOT] = BankTranht["CARDNO"].ToString();
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BANKCARDNO] = BankTranht["CARDNO"].ToString();
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BANKTRANSNO] = BankTranht["BANKSEQNO"].ToString();
                
            }

            try
            {
                //if (SkynetMessage.MsgInfo("是否要充值成功？", true) == false)
                //{
                //    throw new Exception("操作操作测试放弃充值！");
                //}                
                cardSavingData = eCardSavingFacade.insertEntity(eCardAuthorizationData);       
                Posfac.Trans("2", BankTranht);
                Savingsucceed = 1;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("POS接口调用成功，HIS保存失败："+ex.Message+"\r\n开始调用撤消方法");
                SkynetMessage.MsgInfo("HIS充值失败，需要撤消银行交易，请按POS机提示操作！");
                Posfac.Trans("-2", BankTranht);
                Skynet.LoggingService.LogService.GlobalInfoMessage("POS接口调用成功，HIS失败，调用银行撤消完成！");
                //SkynetMessage.MsgInfo("HIS充值失败:"+ex.Message);
                return;
            }
            SkyComm.GetCardBalance();

            if (CallType != 2)
            {
                //打印充值凭证
                PrintInfo("自助充值凭证", cardSavingData.Tables[0].Rows[0]["TRANSACTION_ID"].ToString(), money.ToString());

                if (CallType == 0)
                {
                    FrmRechargeSuccessful frmRecharge = new FrmRechargeSuccessful();
                    frmRecharge.RechargeMoney = money;
                    frmRecharge.ShowDialog(this);
                    frmRecharge.Dispose();
                    SkyComm.CloseWin(this);
                }
                else
                {
                    SkyComm.ShowMessageInfo("您成功充值" + money + "元！卡中余额" + SkyComm.cardBlance.ToString() + "");
                    SkyComm.CloseWin(this);
                }
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
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


        #endregion


        #region 未缴费查询
        private void lblNoPaymentCharge_Click(object sender, EventArgs e)
        {
            ucTime1.timer1.Stop();
            FrmNoPayment frm = new FrmNoPayment();
            if (frm.ShowDialog() == DialogResult.Cancel)
            {
                ucTime1.timer1.Start();
            }
        }

        #endregion        
    }
}
