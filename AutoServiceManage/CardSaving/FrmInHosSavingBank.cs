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
using AutoServiceManage.Presenter;
using EntityData.His.Inpatient;
using BusinessFacade.His.Inpatient;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmInHosSavingBank : Form
    {
        #region 变量

        CardAuthorizationFacade eCardAuthorizationFacade = null;

        CardSavingFacade eCardSavingFacade = null;

        CardAuthorizationData eCardAuthorizationData = null;
        public decimal RechargeMoney = 0;  //充值金额
        public int Savingsucceed = 0;

        public DataSet inHosData { get; set; }
        #endregion

        #region 构造函数，LOAD
        public FrmInHosSavingBank()
        {
            InitializeComponent();
        }

        private void FrmCardSavingBank_Load(object sender, EventArgs e)
        {
            try
            {
                if (BindPage() && CheckInHosSaving())
                {

                    if (!backgroundWorker1.IsBusy)
                        backgroundWorker1.RunWorkerAsync();
                    ucTime1.Sec = 60;

                    ucTime1.timer1.Start();
                }
                else
                {
                    SkyComm.CloseWin(this);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行卡预存LOAD_1");
                    eCardAuthorizationData = new CardAuthorizationData();

                    if (eCardAuthorizationFacade == null)
                    {
                        eCardAuthorizationFacade = new CardAuthorizationFacade();
                    }
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行卡预存LOAD_2");
                    eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行卡预存LOAD_3");
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
                SkyComm.ShowMessageInfo("患者信息绑定失败，银行卡预存功能暂不能使用。请与医院工作人员联系!");
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用银行卡预存LOAD失败：" + ex.Message);
                return false;
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
            frm.CallType = 0;
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
            if (AutoHostConfig.ReadCardType == "XUHUI" || AutoHostConfig.ReadCardType == "XUHUIM1")
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
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "此操作没有找到所需要的患者信息！");
                frm.ShowDialog();
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

            #region 住院预交金充值
            AdvanceRecordData AdvData = new AdvanceRecordData();
            DataRow dr = AdvData.Tables[0].NewRow();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_ADVANCEID] = "新增";
            dr[AdvanceRecordData.H_ADVANCE_RECORD_BUSINESSBANK] = HisSeqNo;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CANCELMARK] = 0;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CHEQUEID] = "";
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CURRENTINHOSMARK] = inHosData.Tables[0].Rows[0]["CURRENTINHOSMARK"].ToString(); ;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_INHOSID] = inHosData.Tables[0].Rows[0]["INHOSID"].ToString();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OFFICEID] = inHosData.Tables[0].Rows[0]["INHOSOFFICEID"].ToString();//this.txtZyks.Text;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATEDATE] = new CommonFacade().GetServerDateTime();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATORID] = SysOperatorInfo.OperatorID;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMODEID] = SkyComm.getvalue("住院预交金充值方式_银行卡").ToString();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMONEY] = RechargeMoney;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_RECEIPTID] = string.Empty;
            AdvData.Tables[0].Rows.Add(dr);
            AdvanceRecordFacade theAdvanceRecordFacade = new AdvanceRecordFacade();
            #endregion
            string advanceid = string.Empty;
            Hashtable BankTranht = new Hashtable();
            BankTranht.Add("SEQNO", HisSeqNo);
            BankTranht.Add("MONEY", money);
            BankTranht.Add("OPERATORID", SysOperatorInfo.OperatorCode);
            BankTranht.Add("POSNO", AutoHostConfig.PosNo);
            BankTranht.Add("DIAGNOSEID", SkyComm.DiagnoseID);
            BankTranht.Add("CARDID", SkyComm.cardInfoStruct.CardNo);

            if (AutoHostConfig.PosInterfaceType.Equals("锡盟新利"))//锡盟新利 弹出错误提示
            {
                try
                {
                    Posfac.Trans("1", BankTranht);
                }
                catch (Exception ex)
                {

                    SkyComm.ShowMessageInfo(ex.Message.ToString());
                    return;      
              
                }
            }
            else
            {
                Posfac.Trans("1", BankTranht);
            }
        

            if (BankTranht["CARDNO"] != null)
            {
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CHECKLOT] = BankTranht["CARDNO"].ToString();
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BANKCARDNO] = BankTranht["CARDNO"].ToString();
                eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BANKTRANSNO] = BankTranht["BANKSEQNO"].ToString();

            }

            try
            {
                advanceid = theAdvanceRecordFacade.insertEntity(AdvData, false);
                Posfac.Trans("2", BankTranht);
                Savingsucceed = 1;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("POS接口调用成功，HIS保存失败：" + ex.Message + "\r\n开始调用撤消方法");
                SkyComm.ShowMessageInfo("预交金充值失败，需要撤销银行交易，请按POS机提示操作，点击确定后，请重新插入银行卡！");
                Posfac.Trans("-2", BankTranht);
                Skynet.LoggingService.LogService.GlobalInfoMessage("POS接口调用成功，HIS失败，调用银行撤消完成！");
                SkyComm.ShowMessageInfo("银行交易失败，充值金额已退回卡，请重新充值！");
                //SkynetMessage.MsgInfo("HIS充值失败:"+ex.Message);
                return;
            }
            //SkyComm.GetCardBalance();

            if (Savingsucceed == 1)
            {
                string receiptID = theAdvanceRecordFacade.GetReceiptIDByAdvanceID(advanceid);
                //打印充值凭证
                PrintInfo("住院预交金充值凭证", receiptID, money.ToString(), advanceid, HisSeqNo);

               



                decimal old_YE = Convert.ToDecimal(inHosData.Tables[0].Rows[0]["BALANCEMONEY"].ToString());
                SkyComm.ShowMessageInfo("您成功充值" + money + "元！卡中余额" + (old_YE + money).ToString("0.00##") + "元！");
                  string _isPrint = SkyComm.getvalue("锡盟预交款打印凭证");//Case 31629 锡盟预交款打印凭证
               _isPrint = string.IsNullOrEmpty(_isPrint) ? "0" : "1";

               if (_isPrint.Equals("0"))
               {
                   try
                   {
                       MoneyTransferPresenter moneyTransferPresenter = new MoneyTransferPresenter();

                       DataSet dataSet = moneyTransferPresenter.getBankInfo(SkyComm.DiagnoseID, HisSeqNo);
                       dataSet.WriteXml(Application.StartupPath + @"\\ReportXml\\" + "银行pos凭证" + SkyComm.DiagnoseID + ".xml");
                       if (!File.Exists(Application.StartupPath + @"\\Reports\\" + "银行pos凭证" + ".frx"))
                       {
                           SkynetMessage.MsgInfo("银行pos凭证" + ".frx报表文件不存在，无法打印.");
                           return;
                       }
                       //Common_XH theCamera_XH = new Common_XH();
                       //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                       PrintManager print = new PrintManager();
                       print.InitReport("银行pos凭证");
                       print.AddData(dataSet.Tables[0], "pos");
                       print.Print();
                       print.Dispose();
                   }
                   catch (Exception e)
                   {

                   }
               }
            
            }
            SkyComm.CloseWin(this);
        }

        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money,string advanceID,string HisSeqNo)
        {
            try
            {
                MoneyTransferPresenter moneyTransferPresenter = new MoneyTransferPresenter();

//                DataSet dataSet = moneyTransferPresenter.getBankInfo(SkyComm.DiagnoseID);

                if (inHosData.Tables[0].Rows.Count > 0)
                {
                    
                    moneyTransferPresenter.addDatas(inHosData, advanceID,"银行卡");
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
                    print.AddParam("预交金余额", (old_YE+reMoney).ToString ("0.00##"));
                    print.AddParam("充值金额", Money);
                    print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                    print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);

                    if (AutoHostConfig.PosInterfaceType.Equals("锡盟新利"))//锡盟新利增加输出参数
                    {
                        //锡盟自助机打印增加哦交易参考号
                        DataSet dataSet = moneyTransferPresenter.getBankInfo(SkyComm.DiagnoseID, HisSeqNo);
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            print.AddParam("交易参考号", (dataSet.Tables[0]).Rows[0]["OHISSEQNO"].ToString());
                        }
                    }
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
        #region 检查当前自助机及病人是否可以进行住院预存
        private bool CheckInHosSaving()
        {
            try
            {
                string sql = "SELECT ID,STARTNUMBER,CURRNUMBER,ENDNUMBER,SURPLUSNUMBER FROM T_OPERATOR_INVOICE WHERE INVOICETYPE=:INVOICETYPE  AND OPERATORID=:OPERATORID AND SURPLUSNUMBER>'0'  AND CURRNUMBER<=ENDNUMBER  ORDER BY ID";
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

        #endregion    
    }
}
