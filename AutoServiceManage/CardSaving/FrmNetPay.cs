using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceManage.Ipresenter;
using AutoServiceManage.Presenter;
using BusinessFacade.His.ClinicDoctor;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmNetPay : Form
    {
        #region 变量
        public int Savingsucceed = 0;

        public string inHosMoney;//住院余额

        private NetPayPresenterImpl mNetPayPresenter;

        private string payMethod;//支付方式：微信，支付宝

        private decimal payMoney;//支付金额

        private string payType;//支付类型

        public string mSerialNo;//流水号
        public string bankNo;
        private string serviceType;

        private bool ex = false;

        public FrmNetPay()
        {
            InitializeComponent();
            init();
        }

        public FrmNetPay(string payMethod, string serviceType,string payMoney)
        {
            InitializeComponent();
            init(payMethod, serviceType, payMoney);
        }



        public decimal PayMoney
        {
            get
            {
                return payMoney;
            }

            set
            {
                payMoney = value;
            }
        }

        public string PayType
        {
            get
            {
                return payType;
            }

            set
            {
                payType = value;
            }
        }

        public string ServiceType
        {
            get
            {
                return serviceType;
            }

            set
            {
                serviceType = value;
            }
        }

        public string PayMethod
        {
            get
            {
                return payMethod;
            }

            set
            {
                payMethod = value;
            }
        }

        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Hashtable mHashtable = new Hashtable();

            string typeId = "4";

            switch (serviceType)
            {
                case "预约取号":
                    typeId = "1";
                    break;
                case "当日挂号":
                    typeId = "2";
                    break;
                case "诊间支付":
                    typeId = "3";
                    break;
                case "门诊预交金充值":
                    typeId = "4";
                    break;
                case "住院预交金充值":
                case "5":
                    typeId = "5";
                    break;
            }

            mHashtable.Add("serviceType", typeId);//传交易类型对应数值（1、预约取号2、当日挂号3、诊间支付4、门诊预交金充值5、住院预交金充值）           
            mHashtable.Add("payFee", payMoney.ToString());//支付金额，保留两位小数（99.99）
            mHashtable.Add("payMethod", this.payMethod);//传支付方式对应数字（1、微信支付2、支付宝支付）
            mHashtable.Add("mchNum", SkyComm.getvalue("网络支付商户ID"));//商户ID SkyComm.getvalue("网络支付商户ID")AutoHostConfig.Machineno

            mHashtable.Add("使用范围","门诊");
            mHashtable.Add("接口类型", "支付接口");
//            mHashtable.Add("测试", "111");


            string mUrl = mNetPayPresenter.getUrl(mHashtable);//获取二维码

            e.Result = mUrl;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //取消等待窗体
            
            //显示二维码
            this.label4.Enabled = true;
            if (e.Error != null)
            {
                Log.Info(GetType().ToString(), "请求二维码", e.Error.Message + e.Error.StackTrace);
                //请求二维码失败
                SkyComm.ShowMessageInfo("获取二维码失败，请返回重新操作或更换自助机！");

                this.loading.Visible = false;

                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                string mResult = (string) e.Result;

                this.loading.Visible = false;

                qrCodeImgControl1.Visible = true;

                qrCodeImgControl1.Text = mResult;
                timer2.Interval = 300000;
                timer2.Start();
                this.backgroundWorker2.RunWorkerAsync();//保存基础交易信息
            }

           
        }
        public string hisNo = string.Empty;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //每5秒查询交易结果
            bool mResult = false;

            try
            {
                
                   mResult = mNetPayPresenter.getPayResult();
                        hisNo = mNetPayPresenter.HisSerialNo;
            }
            catch (Exception exception)
            {
                this.timer1.Stop();
                this.timer2.Stop();
                SkyComm.ShowMessageInfo(exception.Message);
                this.DialogResult = DialogResult.No;               
            }
            try
            {

                if (mResult)
                {
                    this.timer1.Stop();
                    this.timer2.Stop();

                    string mPaymethod = "";
                    if (this.payMethod.Equals("1"))
                    {
                        mPaymethod = "线上微信";
                    }
                    else
                    {
                        mPaymethod = "线上支付宝";
                    }

                    if (this.payType.Equals("住院充值"))
                    {
                        mNetPayPresenter.updateHisState();
                        Hashtable hashtable = new Hashtable();
                        hashtable.Add("充值金额", this.payMoney);
                        hashtable.Add("住院预交款余额", this.inHosMoney);
//                        mNetPayPresenter.printInfo(hashtable,"住院");
                        this.DialogResult = DialogResult.OK;
                        return;
                    }



                    if (this.payType.Equals("缴费"))
                    {
                        this.DialogResult = DialogResult.OK;
                        return;
                    }








                    bool saveResult = mNetPayPresenter.saveCard(payType, payMoney, mPaymethod);

                    if (saveResult)
                    {
                        //更新his状态
                        mNetPayPresenter.updateHisState();

                        Hashtable hashtable = new Hashtable();
                        hashtable.Add("充值金额",this.payMoney);
//                        mNetPayPresenter.printInfo(hashtable,"门诊");

                       
                       

                        this.DialogResult = DialogResult.OK;
                    }                   
                }

            }
            catch (Exception exception)
            {
                this.timer1.Stop();
                this.timer2.Stop();
                //调用撤销接口
                Hashtable mHashtable = new Hashtable();              
                mHashtable.Add("refundAmount", payMoney.ToString());//支付金额，保留两位小数（99.99）              
                mHashtable.Add("mchNum", AutoHostConfig.Machineno);//商户ID SkyComm.getvalue("网络支付商户ID")AutoHostConfig.Machineno
                mHashtable.Add("使用范围", "门诊");
                mHashtable.Add("接口类型", "支付接口");
                mNetPayPresenter.refundOrder(mHashtable);
                //撤销信息
                SkyComm.ShowMessageInfo(exception.Message);
                this.DialogResult = DialogResult.No;
            }
        }

        private void FrmNetPay_Load(object sender, EventArgs e)
        {
            //正在生成二维码
            initUI();

            timer3.Interval = 1000;

            timer3.Start();

            this.label4.Enabled = false;

            this.backgroundWorker1.RunWorkerAsync();//生成二维码
        }

        private void initUI()
        {
            this.txtMoney.Text = this.PayMoney.ToString();
            
            this.txtTradeType.Text = this.payMethod.Equals("1")?"微信":"支付宝";
            this.label7.Text = this.payMethod.Equals("1") ? "注意：请使用微信进行扫码" : "注意：请使用支付宝进行扫码";
            Log.Info(GetType().ToString(),"支付金额："+ this.payMoney);
            Log.Info(GetType().ToString(), "支付方式（payMethod）：" + this.payMethod);
            Log.Info(GetType().ToString(), "支付类型（payType）：" + this.payType);//serviceType
            Log.Info(GetType().ToString(), "serviceType：" + this.serviceType);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //存储基本交易数据,第一次
            string mPayMethod = "";
            if (this.payMethod.Equals("1"))
            {
                mPayMethod = "微信";
            }
            else
            {
                mPayMethod = "支付宝";
            }
            mNetPayPresenter.saveTradeInfo(this.PayMoney, this.payType, mPayMethod);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                SkyComm.ShowMessageInfo("系统出现未知异常，如您已支付，请拿微信支付订单在收费窗口完成充值或退费！");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.timer1.Start();
                
            }           
        }

        private void init()
        {
            mNetPayPresenter = new NetPayPresenterImpl();
        }

        private void init(string payMethod, string serviceType, string payMoney)
        {
            mNetPayPresenter = new NetPayPresenterImpl(payMethod, serviceType, payMoney);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            SkyComm.ShowMessageInfo("支付超时，如果您已经支付成功，请在窗口进行退款！");

            mNetPayPresenter.revokedTrade("-1");//（-1：支付超时，-2订单撤销或关闭）
            this.timer2.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 点击返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label4_Click(object sender, EventArgs e)
        {
            FrmYesNoAlert mFrmInfoAlert = new FrmYesNoAlert();

            mFrmInfoAlert.Msg = "如果您已经扫码，请在此界面等待支付结果！是否离开？";
            mFrmInfoAlert.sec = 10;
            mFrmInfoAlert.timer1.Start();

            if (mFrmInfoAlert.ShowDialog() == DialogResult.OK)
            {
                mNetPayPresenter.revokedTrade("-2"); //（-1：支付超时，-2订单撤销或关闭）
                timer2.Stop();
                this.DialogResult = DialogResult.Cancel;
            }
            

//            if (!ex)
//            {
//                SkyComm.ShowMessageInfo("如果您已经扫码，请在此界面等待支付结果！如您确认离开，请再此点击返回按钮");
//
//                ex = true;
//
//                return;
//            }
//
//            if (ex)
//            {
//                mNetPayPresenter.revokedTrade("-2");//（-1：支付超时，-2订单撤销或关闭）
//                timer2.Stop();
//                this.DialogResult = DialogResult.Cancel;
//            }
        }

        private void FrmNetPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer3.Stop();
        }

        private int time = 300;
        private void timer3_Tick(object sender, EventArgs e)
        {
            time = time - 1;
            this.label6.Text = "操作时间：" + time;

        }
    }
}
