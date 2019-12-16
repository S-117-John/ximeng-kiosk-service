using BusinessFacade.His.Inpatient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Presenter;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using EntityData.His.CardClubManager;
using EntityData.His.Inpatient;
using Skynet.Framework.Common;
using TiuWeb.ReportBase;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.Register;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmInhosAdvance : Form
    {
        #region 自定义
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        DataSet InHosData = new DataSet();//住院病人信息
        #endregion

        public FrmInhosAdvance()
        {
            InitializeComponent();
        }

        private void FrmInhosAdvance_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();

            if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用现金")))
            {
                lblCashStored.Visible = false;
                label3.Location = new Point(138, 335);
            }

            if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用银行卡")))
            {
                lblBankCardStored.Visible = false;
            }

            if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用微信")))
            {
                label3.Visible = false;
            }

            if (!BindPage())
            {
                this.Close();
            }
        }

        private bool BindPage()
        {
            try
            {
                
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();

                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID= SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];
                    if (drInHos["INHOSSTATE"].ToString() == "1")
                    {
                        SkyComm.ShowMessageInfo("您已结算，不能继续预交款操作！");
                        return false;
                    }
                    this.lblInHosDate.Text =Convert.ToDateTime(drInHos["ENTERHOSDATE"].ToString()).ToString ("yyyy-MM-dd");
                    this.lblEnterOfficeDate.Text = drInHos["ENTEROFFICEDATE"].ToString().Trim() != "" ? Convert.ToDateTime(drInHos["ENTEROFFICEDATE"].ToString().Trim()).ToString("yyyy-MM-dd") : "";
                    this.lblName.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                    _inHosID = drInHos["INHOSID"].ToString();
                    this.lblInHosNO.Text = _inHosID;
                    this.lblBedNo.Text = drInHos["OFFICEBEDID"].ToString();
                    this.lblYJK.Text = Convert.ToDecimal(drInHos["ADVANCEMONEY"].ToString()).ToString("0.00##");
                    this.lblTotalCost.Text= Convert.ToDecimal(drInHos["TOTALCONSUMEMONEY"].ToString()).ToString("0.00##");
                    this.lblYE.Text= Convert.ToDecimal(drInHos["BALANCEMONEY"].ToString()).ToString("0.00##");
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

        private void lblCashStored_Click(object sender, EventArgs e)
        {
           
            this.ucTime1.timer1.Stop();

            //现金预存前先刷卡
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();
                int intResult = SkyComm.ReadCard("现金预存");
                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            //拍照
            switch (AutoHostConfig.ReadCardType)
            {
                case "XUHUI":
                case "XUHUIM1":
                    AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                    camera.TakeCamera(SkyComm.cardInfoStruct.CardNo, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString(), AutoHostConfig.Machineno);
                    break;
                default:
                    break;
            }

            FrmInHosSavingCash frm = new FrmInHosSavingCash();
            frm.inHosData =InHosData;
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();
        }

        private void lblBankCardStored_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();
            //银行卡预存前先刷卡
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();
                int intResult = SkyComm.ReadCard("银行卡预存");
                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            FrmInHosSavingBank frm = new FrmInHosSavingBank();
            frm.inHosData = InHosData;
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        /// <summary>
        /// 微信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();

            InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
            DataSet inHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
            if (inHosData != null && inHosData.Tables.Count != 0 && inHosData.Tables[0].Rows.Count != 0)
            {
                if (inHosData.Tables[0].Rows[0]["INHOSSTATE"].ToString() == "1")
                {
                    SkyComm.ShowMessageInfo("您已结算，不能继续预交款操作！");
                    return;
                }
            }
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();

                int intResult = SkyComm.ReadCard("微信预存");

                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            //            FrmChooseMoney mChooseMoneyFrm = new FrmChooseMoney();
            FrmMoneyInput mChooseMoneyFrm = new FrmMoneyInput();
            if (mChooseMoneyFrm.ShowDialog() == DialogResult.OK)//确认了金额
            {
                //                decimal mChooseMoney = mChooseMoneyFrm.ChooseMoney;//所选金额
                string mMoney = mChooseMoneyFrm.inputMoney.ToString() + ".00";
                decimal mChooseMoney = Math.Round(Convert.ToDecimal(mMoney), 2);//所选金额

                if (!string.IsNullOrEmpty(SkyComm.getvalue("微信交易测试")))
                {
                    mChooseMoney = Math.Round(Convert.ToDecimal("0.01"), 2);//所选金额
                }
                FrmNetPay payFrm = new FrmNetPay();

                payFrm.PayMoney = mChooseMoney;

                payFrm.ServiceType = "5";

                payFrm.PayMethod = "1";

                payFrm.PayType = "住院充值";
                payFrm.inHosMoney = lblYE.Text;
                DialogResult mDialogResult = payFrm.ShowDialog();

                if (mDialogResult == DialogResult.OK)
                {
                    //his预存

                    inHosSave(inHosData, mChooseMoney, payFrm.mSerialNo, payFrm.bankNo);
                    ;

                   

                    SkyComm.ShowMessageInfo(payFrm.PayType + "成功！");
                }
                else
                {
                    this.ucTime1.timer1.Start();//计时器动 
                }



            }
            else
            {
                this.ucTime1.timer1.Start();//计时器动
            }


        }


        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money, string advanceid,DataSet inHosData)
        {
            try
            {
                MoneyTransferPresenter moneyTransferPresenter = new MoneyTransferPresenter();



                //CardSavingFacade cardSavingFacade = new CardSavingFacade();
                //DataSet cardSavingData = cardSavingFacade.FindByPrimaryKey(strTRANSACTION_ID);
                if (inHosData.Tables[0].Rows.Count > 0)
                {
//                    moneyTransferPresenter.addDatas(inHosData, advanceid, "现金");
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

        public void inHosSave(DataSet inHosData, decimal RechargeMoney, string hisNo, string bankNo)
        {


            AdvanceRecordData AdvData = new AdvanceRecordData();
            DataRow dr = AdvData.Tables[0].NewRow();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_ADVANCEID] = "新增";
            dr[AdvanceRecordData.H_ADVANCE_RECORD_BUSINESSBANK] = hisNo;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CANCELMARK] = 0;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CHEQUEID] = "";
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CURRENTINHOSMARK] =
                inHosData.Tables[0].Rows[0]["CURRENTINHOSMARK"].ToString();
            ;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_INHOSID] = inHosData.Tables[0].Rows[0]["INHOSID"].ToString();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OFFICEID] = inHosData.Tables[0].Rows[0]["INHOSOFFICEID"].ToString();
            //this.txtZyks.Text;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATEDATE] = new CommonFacade().GetServerDateTime();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATORID] = SysOperatorInfo.OperatorID;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMODEID] = SkyComm.getvalue("住院预交金充值方式_微信").ToString();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMONEY] = RechargeMoney;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_RECEIPTID] = string.Empty;
            AdvData.Tables[0].Rows.Add(dr);
            AdvanceRecordFacade theAdvanceRecordFacade = new AdvanceRecordFacade();

            string advanceid = string.Empty;
            Hashtable BankTranht = new Hashtable();
            BankTranht.Add("SEQNO", hisNo);
            BankTranht.Add("MONEY", RechargeMoney);
            BankTranht.Add("OPERATORID", SysOperatorInfo.OperatorCode);
            BankTranht.Add("POSNO", AutoHostConfig.PosNo);
            BankTranht.Add("DIAGNOSEID", SkyComm.DiagnoseID);
            BankTranht.Add("CARDID", SkyComm.cardInfoStruct.CardNo);
            BankTranht.Add("BANKSEQNO", bankNo);
            CardAuthorizationData eCardAuthorizationData = new CardAuthorizationData();
            CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();
            eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
//            eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][
//                CardAuthorizationData.T_CARD_AUTHORIZATION_BANKTRANSNO] = BankTranht["BANKSEQNO"].ToString();

            advanceid = theAdvanceRecordFacade.insertEntity(AdvData, false);
            string receiptID = theAdvanceRecordFacade.GetReceiptIDByAdvanceID(advanceid);
            PrintInfo("住院预交金充值凭证", receiptID, RechargeMoney.ToString(), advanceid, inHosData);

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();

            InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
            DataSet inHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
            if (inHosData != null && inHosData.Tables.Count != 0 && inHosData.Tables[0].Rows.Count != 0)
            {
                if (inHosData.Tables[0].Rows[0]["INHOSSTATE"].ToString() == "1")
                {
                    SkyComm.ShowMessageInfo("您已结算，不能继续预交款操作！");
                    return;
                }
            }
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();

                int intResult = SkyComm.ReadCard("微信预存");

                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            //            FrmChooseMoney mChooseMoneyFrm = new FrmChooseMoney();
            FrmMoneyInput mChooseMoneyFrm = new FrmMoneyInput();
            if (mChooseMoneyFrm.ShowDialog() == DialogResult.OK)//确认了金额
            {
                //                decimal mChooseMoney = mChooseMoneyFrm.ChooseMoney;//所选金额
                string mMoney = mChooseMoneyFrm.inputMoney.ToString() + ".00";
                decimal mChooseMoney = Math.Round(Convert.ToDecimal(mMoney), 2);//所选金额

                if (!string.IsNullOrEmpty(SkyComm.getvalue("微信交易测试")))
                {
                    mChooseMoney = Math.Round(Convert.ToDecimal("0.01"), 2);//所选金额
                }
                FrmNetPay payFrm = new FrmNetPay();

                payFrm.PayMoney = mChooseMoney;

                payFrm.ServiceType = "5";

                payFrm.PayMethod = "2";

                
                payFrm.PayType = "住院充值";
                payFrm.inHosMoney = lblYE.Text;
                DialogResult mDialogResult = payFrm.ShowDialog();

                if (mDialogResult == DialogResult.OK)
                {
                    //his预存

                    inHosSave(inHosData, mChooseMoney, payFrm.mSerialNo, payFrm.bankNo);
                    ;



                    SkyComm.ShowMessageInfo(payFrm.PayType + "成功！");
                }
                else
                {
                    this.ucTime1.timer1.Start();//计时器动 
                }



            }
            else
            {
                this.ucTime1.timer1.Start();//计时器动
            }
        }
    }
}
