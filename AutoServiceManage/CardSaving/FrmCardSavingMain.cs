using BusinessFacade.His.Clinic;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmCardSavingMain : Form
    {
        public FrmCardSavingMain()
        {
            InitializeComponent();
        }
       
        private void FrmCardSavingMain_Load(object sender, EventArgs e)
        {
           
            string mechinNo = string.Empty;
            try
            {
                QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();
                DetailAccountFacade detailAccountFacade = new DetailAccountFacade();
                #region 获取ip
                string ipAddress = null;
                try
                {
                    string hostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                    for (int i = 0; i < ipEntry.AddressList.Length; i++)
                    {
                        if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ipEntry.AddressList[i].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                #endregion
                #region 获取机器码
                string sql1 = "select MACHINENO from T_AUTOSERVICEMACHINE_INFO where IPADDRESS = @IPADDRESS";
                Hashtable hashtable1 = new Hashtable();
                hashtable1.Add("@IPADDRESS", ipAddress);
                DataSet dataSet = querySolutionFacade.ExeQuery(sql1, hashtable1);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    mechinNo = dataSet.Tables[0].Rows[0]["MACHINENO"].ToString();
                }
                #endregion



                #region 记录结算时间
               


                DataSet data = new DataSet();

                Hashtable hashtable = new Hashtable();

                string mSql = "select * from SETTLEMENT_RECORD where MECHINNO = @MECHINNO ";

                hashtable.Add("@MECHINNO", mechinNo);


                data = querySolutionFacade.ExeQuery(mSql, hashtable);
                if(data.Tables[0].Rows.Count > 0)
                {
                    if (data.Tables[0].Rows[0]["SETTLEMENT_TIME"].ToString().Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        this.lblCashStored.Enabled = false;
                    }
                    else
                    {
                        this.lblCashStored.Enabled = true;
                    }


                }
                else
                {
                    this.lblCashStored.Enabled = true;
                }
                #endregion
            }
            catch (Exception)
            {

                this.lblCashStored.Enabled = true;
            }

            //if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用现金")))
            //{
            //    lblCashStored.Visible = false;
            //    btnWxPay.Location = new Point(142, 332);
            //}

            if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用银行卡")))
            {
                lblBankCardStored.Visible = false;
            }

            if (!string.IsNullOrEmpty(SkyComm.getvalue("禁用微信")))
            {
                btnWxPay.Visible = false;
            }

            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
        }

        public int Savingsucceed = 0;
        public decimal RechargeMoney = 0;  //充值金额

        /// <summary>
        /// 调用类型：0：主界面，1：取号，缴费,2:办卡
        /// </summary>
        public int CallType { get; set; }

        public string serviceType { get; set; }

        //充值方式：现金,银行卡
        public string MODETYPE { get; set; }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void label1_Click(object sender, EventArgs e)
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

            FrmCardSavingBank frm = new FrmCardSavingBank();
            frm.CallType = CallType;
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            Savingsucceed = frm.Savingsucceed;
            RechargeMoney = frm.RechargeMoney;
            MODETYPE = "线上银行卡";
            frm.Dispose();
            
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
                    AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                    camera.TakeCamera(SkyComm.cardInfoStruct.CardNo, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString(), AutoHostConfig.Machineno);
                    break;
                default:
                    break;                    
            }

            FrmCardSavingCash frm = new FrmCardSavingCash();
            frm.CallType = CallType;            
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            Savingsucceed = frm.Savingsucceed;
            RechargeMoney = frm.RechargeMoney;
            MODETYPE = "现金";
            frm.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmCardSavingMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        /// <summary>
        /// 点击微信支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWxPay_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();

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
                string mMoney = mChooseMoneyFrm.inputMoney.ToString()+".00";
                decimal mChooseMoney = Math.Round(Convert.ToDecimal(mMoney),2);//所选金额

                if (!string.IsNullOrEmpty(SkyComm.getvalue("微信交易测试")))
                {
                    mChooseMoney = Math.Round(Convert.ToDecimal("0.01"), 2);//所选金额
                }

                FrmNetPay payFrm = new FrmNetPay();

                payFrm.PayMoney = mChooseMoney;

                payFrm.ServiceType = serviceType;

                payFrm.PayMethod = "1";

                switch (CallType)
                {
                    case 0:
                        payFrm.PayType = "充值";
                        break;
                    case 1:
                        payFrm.PayType = "缴费";
                        break;
                    case 2:
                        payFrm.PayType = "办卡";
                        break;
                    default:
                        payFrm.PayType = "";
                        break;
                }

                DialogResult mDialogResult = payFrm.ShowDialog();

                if (mDialogResult == DialogResult.OK)
                {
                    Savingsucceed = 1;
                    RechargeMoney = mChooseMoney;
//                    SkyComm.ShowMessageInfo(payFrm.PayType+"成功！");
                    FrmRechargeSuccessful frmRecharge = new FrmRechargeSuccessful();
                    frmRecharge.RechargeMoney = mChooseMoney;
                    frmRecharge.ShowDialog(this);
                    frmRecharge.Dispose();
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

        private void btnApliPay_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();

            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();

                int intResult = SkyComm.ReadCard("支付宝预存");

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

                payFrm.ServiceType = serviceType;

                payFrm.PayMethod = "2";//支付宝

                switch (CallType)
                {
                    case 0:
                        payFrm.PayType = "充值";
                        break;
                    case 1:
                        payFrm.PayType = "缴费";
                        break;
                    case 2:
                        payFrm.PayType = "办卡";
                        break;
                    default:
                        payFrm.PayType = "";
                        break;
                }

                DialogResult mDialogResult = payFrm.ShowDialog();

                if (mDialogResult == DialogResult.OK)
                {
                    Savingsucceed = 1;
                    RechargeMoney = mChooseMoney;
                    //                    SkyComm.ShowMessageInfo(payFrm.PayType + "成功！");

                    FrmRechargeSuccessful frmRecharge = new FrmRechargeSuccessful();
                    frmRecharge.RechargeMoney = mChooseMoney;
                    frmRecharge.ShowDialog(this);
                    frmRecharge.Dispose();
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
