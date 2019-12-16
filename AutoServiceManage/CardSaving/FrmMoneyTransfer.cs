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
using AutoServiceManage.Presenter;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmMoneyTransfer : Form
    {
        #region 变量
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        string _inHosOfficeID = string.Empty;
        DataSet InHosData = new DataSet();//住院病人信息
        bool IsCanInput = false;
        #endregion

        #region 构造函数,load,窗体事件
        public FrmMoneyTransfer()
        {
            InitializeComponent();
        }
        
        private void FrmMoneyTransfer_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            if (!BindPage())
            {
                this.Close();
            }
        }

        private void FrmMoneyTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
              
        #endregion

        #region 清屏
        private void lblclear_Click(object sender, EventArgs e)
        {
            if (!IsCanInput)
                return;
            lblhm.Text = "请输入";
            ucTime1.Sec = 60;
            this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
        }
        #endregion

        #region 退格
        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (!IsCanInput)
                return;
            if (lblhm.Text.IndexOf("号") < 0)
            {
                string hmValue = lblhm.Text.Substring(0, lblhm.Text.Length - 1);
                lblhm.Text = hmValue;
            }
            if (lblhm.Text.Length == 0)
            {

                lblhm.Text = "请输入";
                this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;                
            }
            ucTime1.Sec = 60;
        }
        #endregion

        #region 确认
        private void lblOK_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();
            SkyComm.GetCardBalance();
            this.lblMzYe.Text = SkyComm.cardBlance.ToString();
            if (!checkData())
            {
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }
            FrmYesNoAlert frmAlert = new FrmYesNoAlert();
            frmAlert.Title = "提示";
            frmAlert.Msg = "是否确认将门诊预交金转至住院预交金，转账金额：" + Convert.ToDecimal(this.lblhm.Text.Trim ()).ToString("0.00##") + "元";
            if (frmAlert.ShowDialog() == DialogResult.OK)
            {
                string advanceID = Postprepay();

                if (string.IsNullOrEmpty(advanceID))
                {
                    this.ucTime1.Sec = 60;
                    this.ucTime1.timer1.Start();
                    return;
                }

                SkyComm.ShowMessageInfo("转账成功！转账金额：" + Convert.ToDecimal(this.lblhm.Text.Trim()).ToString("0.00##") + "元。住院预交金余额：" + this.lblZyYe.Text + "元");
                AdvanceRecordFacade theAdvanceRecordFacade = new AdvanceRecordFacade();
                string receiptID = theAdvanceRecordFacade.GetReceiptIDByAdvanceID(advanceID);

                PrintInfo("住院预交金充值凭证", receiptID, this.lblhm.Text, advanceID);
            }
            else
            {
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private bool checkData()
        {
            string inputNum = this.lblhm.Text;
            decimal transferMoney = 0;
            if (inputNum.Trim() == "请输入")
            {
                SkyComm.ShowMessageInfo("请输入转账金额！");
                return false;
            }
            try
            {
                transferMoney=Convert.ToDecimal(inputNum);
                if (transferMoney > Convert.ToDecimal(this.lblMzYe.Text.Trim()))
                {
                    SkyComm.ShowMessageInfo("转入金额不能大于门诊预交金余额！");
                    return false;
                }
                if (transferMoney == 0)
                {
                    SkyComm.ShowMessageInfo("转入金额不能等于零！");
                    return false;
                }
                return true;
            }
            catch
            {
                SkyComm.ShowMessageInfo("请输入正确格式的数字！");
                return false;
            }
 
        }
        #endregion

        #region 输入数字0-9
        private void lbl0_Click(object sender, EventArgs e)
        {
            if (!IsCanInput)
                return;
            Label lbl = (Label) sender;
            string num = lbl.Name.Substring(lbl.Name.Length - 1, 1);
            this.lblhm.ForeColor = System.Drawing.Color.Red;
            InputText(num);
            ucTime1.Sec = 60;
        }
        private void lblPoint_Click(object sender, EventArgs e)
        {
            if (!IsCanInput)
                return;
            if (this.lblhm.Text.Length != 0)
            {
                if (!this.lblhm.Text.Contains("."))
                {
                    InputText(".");
                }
            }
            ucTime1.Sec = 60;
        }
        private void InputText(string Num)
        {
            if (lblhm.Text.IndexOf("请") >= 0)
            {
                lblhm.Text = Num;
            }
            else
            {
                lblhm.Text = lblhm.Text + Num;
            }
        }


        private void label6_Click(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            IsCanInput = true;
            this.lblhm.Text = "请输入";
            this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
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

        #region 住院信息查询，转账金额处理
        private bool BindPage()
        {
            try
            {
                if (SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["Is_FEECHARGING_CARD"].ToString() != "0")
                {
                    SkyComm.ShowMessageInfo("该卡是非储值卡，不能进行交款!");
                    return false;
                }

                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();

                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];

                    if (drInHos["INHOSSTATE"].ToString() == "1")
                    {
                        SkyComm.ShowMessageInfo("您的住院费用已经结算,不能进行预交款操作");
                        return false;
                    }

                    this.lblPatient.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                    this.lblSex.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                    this.lblAge.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                    this.lblIDCard.Text="身份证号："+ SkyComm.ConvertIdCard(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["IDENTITYCARD"].ToString());
                    _inHosID = drInHos["INHOSID"].ToString();
                    _inHosOfficeID = drInHos["INHOSOFFICEID"].ToString();
                    this.lblInHosID.Text = "住院号："+_inHosID;
                    this.lblZyYe.Text = Convert.ToDecimal(drInHos["BALANCEMONEY"].ToString()).ToString("0.00##");
                    SkyComm.GetCardBalance();
                    this.lblMzYe.Text = SkyComm.cardBlance.ToString();
                    this.lblhm.Text = SkyComm.cardBlance.ToString();
                    this.lblhm.ForeColor = System.Drawing.Color.Red;
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

        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money,string advanceID)
        {
            try
            {
                MoneyTransferPresenter moneyTransferPresenter = new MoneyTransferPresenter();

                

                if (InHosData.Tables[0].Rows.Count > 0)
                {
                    moneyTransferPresenter.addDatas(InHosData, advanceID,"门诊预交金转住院预交金");

                    InHosData.WriteXml(Application.StartupPath + @"\\ReportXml\\" + ReportName + SkyComm.DiagnoseID + "_" + strTRANSACTION_ID + ".xml");
                    if (!File.Exists(Application.StartupPath + @"\\Reports\\" + ReportName + ".frx"))
                    {
                        SkynetMessage.MsgInfo(ReportName + ".frx报表文件不存在，无法打印.");
                        return;
                    }
                    decimal old_YE = Convert.ToDecimal(InHosData.Tables[0].Rows[0]["BALANCEMONEY"].ToString());
                    decimal reMoney = Convert.ToDecimal(Money);

                    //Common_XH theCamera_XH = new Common_XH();
                    //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                    PrintManager print = new PrintManager();
                    print.InitReport(ReportName);
                    print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                    print.AddParam("收据号", strTRANSACTION_ID);
                    print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                    print.AddParam("预交金余额", (old_YE + reMoney).ToString("0.00##"));
                    print.AddParam("充值金额", Convert.ToDecimal(Money).ToString ("0.00##"));
                    print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                    print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                    print.AddData(InHosData.Tables[0], "report");
                    //PrintManager.CanDesign = true;
                    //print.PreView();
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

        #region 处理住院预交金业务
        private string Postprepay()
        {
            AdvanceRecordData AdvData = new AdvanceRecordData();
            bool IfForegift = false;//不确定是否以后会在转账时扣住院押金
            string AdvanceID = "";
            ChargeTypeFacade chargeTypeFacade = new ChargeTypeFacade();
            DataRow dr = AdvData.Tables[0].NewRow();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_ADVANCEID] = "新增";
            dr[AdvanceRecordData.H_ADVANCE_RECORD_BUSINESSBANK] = "";//开户银行
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CANCELMARK] = 0;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CHEQUEID] = "";//支票号
            dr[AdvanceRecordData.H_ADVANCE_RECORD_CURRENTINHOSMARK] = GetInHosInfo(_inHosID).Tables[0].Rows[0]["CURRENTINHOSMARK"].ToString();
            dr[AdvanceRecordData.H_ADVANCE_RECORD_INHOSID] = _inHosID;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OFFICEID] = _inHosOfficeID;//this.txtZyks.Text;
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATEDATE] = new CommonFacade().GetServerDateTime().ToString("yyyy-MM-dd HH:mm:ss.ffff");
            dr[AdvanceRecordData.H_ADVANCE_RECORD_OPERATORID] = SysOperatorInfo.OperatorID;

            //支付类型ID及支付类型名称
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMODEID] = SkyComm.getvalue("住院预交金充值方式_门诊预交金").ToString();//case21248 2015-5-4 yuanxiaoxia ID字段来源于界面加载
            dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMODE] = "门诊预交金";
            decimal ForegiftMoney = 0;
            if (IfForegift == true)
            {
                dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMONEY] = DecimalRound.Round(Convert.ToDecimal(this.lblhm.Text) - ForegiftMoney, 2);
            }
            else
            {
                dr[AdvanceRecordData.H_ADVANCE_RECORD_PAYMONEY] = DecimalRound.Round(Convert.ToDecimal(this.lblhm.Text), 2);
            }

            dr[AdvanceRecordData.H_ADVANCE_RECORD_RECEIPTID] = string.Empty;
            AdvData.Tables[0].Rows.Add(dr);

            try
            {

                AdvanceRecordFacade fac = new AdvanceRecordFacade();
                AdvanceID = fac.insertEntity(AdvData.GetChanges(), IfForegift);
                SkyComm.GetCardBalance();
                this.lblMzYe.Text = SkyComm.cardBlance.ToString();
                this.lblZyYe.Text = Convert.ToString(Convert.ToDecimal(this.lblhm.Text) + Convert.ToDecimal(lblZyYe.Text));
            }
            catch (Exception err)
            {
                SkyComm.ShowMessageInfo(err.Message);
                AdvData.Tables[0].Clear();
                AdvData.Clear();
                return "";
            }
            return AdvanceID;
        }

        /// <summary>
        /// 获取在院信息
        /// </summary>
        /// <param name="InHosNo"></param>
        /// <returns></returns>
        private DataSet GetInHosInfo(string InHosNo)
        {
            InHosRecordFacade fac = new InHosRecordFacade();
            DataSet InHosData = fac.FindInHosInfo(InHosNo);
            return InHosData;
        }
        #endregion

    }
}
