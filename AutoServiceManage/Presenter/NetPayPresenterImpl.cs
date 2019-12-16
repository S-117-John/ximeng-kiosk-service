using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Common;
using AutoServiceManage.Imodel;
using AutoServiceManage.Ipresenter;
using AutoServiceManage.Model;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using BusinessFacade.His.ErpPad;
using BusinessFacade.His.Inpatient;
using EntityData.His.CardClubManager;
using EntityData.His.Inpatient;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using TiuWeb.ReportBase;

namespace AutoServiceManage.Presenter
{
    public class NetPayPresenterImpl : InetPayPresenter
    {
        #region 变量

        private string payMethod;

        private InetPayModel mModel;

        private string serviceType;

        private string bankSerialNo;

        private string hisSerialNo;

        private string payMoney;

        public string HisSerialNo
        {
            get { return hisSerialNo; }

            set { hisSerialNo = value; }
        }

        public string BankSerialNo
        {
            get { return bankSerialNo; }

            set { bankSerialNo = value; }
        }
        
        public NetPayPresenterImpl()
        {
            this.hisSerialNo = getSerialNo();
            mModel = new NetPayModelImpl();
        }
        public NetPayPresenterImpl(string payMethod, string serviceType, string payMoney)
        {
            this.hisSerialNo = getSerialNo();
            this.serviceType = serviceType;
            this.payMethod = payMethod;
            this.payMoney = payMoney;
            mModel = new NetPayModelImpl();
        }

        #endregion

        public string getUrl(Hashtable hashtable)
        {
            LogService.GlobalInfoMessage("geturl有参方法");
            hashtable.Add("hisTradeId", hisSerialNo); //his流水
            return mModel.getUrl(hashtable);
        }

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <returns></returns>
        public string getUrl()
        {
            Hashtable mHashtable = new Hashtable();
            mHashtable.Add("hisTradeId", hisSerialNo); //his流水
            mHashtable.Add("serviceType", serviceType);//传交易类型对应数值（1、预约取号2、当日挂号3、诊间支付4、门诊预交金充值5、住院预交金充值）           
            mHashtable.Add("payFee", payMoney);//支付金额，保留两位小数（99.99）
            mHashtable.Add("payMethod", this.payMethod);//传支付方式对应数字（1、微信支付2、支付宝支付）
            mHashtable.Add("mchNum", AutoHostConfig.Machineno);//商户ID SkyComm.getvalue("网络支付商户ID")AutoHostConfig.Machineno
            mHashtable.Add("使用范围", "门诊");
            mHashtable.Add("接口类型", "支付接口");
            return mModel.getUrl(mHashtable);
        }

        /// <summary>
        /// 获取交易结果
        /// </summary>
        /// <returns></returns>
        public bool getPayResult()
        {
            try
            {
                DataSet mPayResultDataSet = mModel.getPayResult(this.hisSerialNo);

                if (mPayResultDataSet == null || mPayResultDataSet.Tables[0].Rows.Count == 0)
                {
                    return false;
                }

                string mBankSerialNo = mPayResultDataSet.Tables[0].Rows[0]["BANKSEQNO"].ToString();



                if (string.IsNullOrEmpty(mBankSerialNo))
                {
                    return false;
                }

                string bankState = mPayResultDataSet.Tables[0].Rows[0]["BANKSTATE"].ToString();

                if (bankState.Equals("0"))
                {
                    return false;
                }
                this.bankSerialNo = mBankSerialNo;
                Log.Info(GetType().ToString(), "银行流水号：" + mBankSerialNo);
                Log.Info(GetType().ToString(), "银行状态：" + bankState);
                if (bankState.Equals("-1"))
                {
                    throw new Exception("微信交易结果失败：" + mPayResultDataSet.Tables[0].Rows[0]["ITEM1"].ToString());
                }



                return true;
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(), "查询交易结果异常信息" + e.Message);
                throw e;
            }
        }

        public string getSerialNo()
        {
            ValidateCode vc = new ValidateCode();
            string HisSeqNo = string.Empty;

            HisSeqNo = DateTime.Now.ToString("yyMMddHHmmss") + SysOperatorInfo.OperatorID + vc.GenValidateCode(4);

            Log.Info(GetType().ToString(), SkyComm.cardInfoStruct.CardNo + "生成的流水号", HisSeqNo);

            return HisSeqNo;
        }

        public void saveTradeInfo(decimal tradeMoney, string type,string payMethod)
        {
            try
            {
                Hashtable mHashtable = new Hashtable();
                mHashtable.Add("@HISSEQNO", this.hisSerialNo); //医院流水号
                mHashtable.Add("@BANKSTATE", "0"); //银行状态
                mHashtable.Add("@HISSTATE", "0"); //医院状态
                mHashtable.Add("@BUSCD", "自助机"); //业务类别
                mHashtable.Add("@HISID", SkyComm.cardInfoStruct.CardNo); //卡号
                mHashtable.Add("@TRFAMT", tradeMoney.ToString()); //交易金额
                mHashtable.Add("@USETYPE", type); //用途
                mHashtable.Add("@OPERATORID", SysOperatorInfo.OperatorID); //操作员id
                mHashtable.Add("@OPERATETIME", DateTime.Now); //操作时间
                mHashtable.Add("@DIAGNOSEID", SkyComm.DiagnoseID); //诊疗好

                if (payMethod.Contains("微信"))
                {
                    payMethod = "微信";
                }
                else
                {
                    payMethod = "支付宝";
                }

                mHashtable.Add("@BUSINESSTYPE", payMethod); //交易类型
                mHashtable.Add("@DATASOURCES", "自助机"); //数据来源


                Log.Info(GetType().ToString(), "his流水号:" + hisSerialNo);

                Log.Info(GetType().ToString(), "saveTradeInfo传入的交易金额:" + tradeMoney);

                mModel.saveTradeInfo(mHashtable);
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(), "微信交易返回二维码后保存基础交易信息异常:" + e.Message + e.Source + e.StackTrace);


                throw e;
            }
        }

        public bool saveCard(string type, decimal money, string payMethod)
        {
            try
            {
                mModel.svaeCard(type, money, payMethod, this.hisSerialNo);
                return true;
            }
            catch (Exception e)
            {

                Log.Info(GetType().ToString(), "保存卡充值异常信息：" + e.Message + e.Source + e.StackTrace);



                throw e;
            }
        }

        public void updateHisState()
        {
            try
            {
                mModel.updateHisState(hisSerialNo);
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(), "更新his状态异常信息：" + e.Message + e.Source + e.StackTrace);
                throw e;
            }
        }

        public void revokedTrade(string errorId)
        {
            mModel.revokedTrade(hisSerialNo, errorId);
        }

        public bool refundOrder(Hashtable hashtable)
        {
            hashtable.Add("platfromTradeId", this.bankSerialNo); //支付金额，保留两位小数（99.99）

            List<string> mList = mModel.refundTrade(hashtable);

            if (mList[0].Equals("-1"))
            {
                throw new Exception("退费失败！，请在窗口进行退费！");
            }

            if (mList[0].Equals("0"))
            {
                mModel.updateRefundOrderResult(mList, this.hisSerialNo);

            }

            return true;
        }

        public void printInfo(Hashtable hashtable, string type)
        {
            try
            {

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("凭证名称", typeof(string));
                dataTable.Columns.Add("医院名称", typeof(string));
                dataTable.Columns.Add("收据号", typeof(string));
                dataTable.Columns.Add("姓名", typeof(string));
                dataTable.Columns.Add("卡余额", typeof(string));
                dataTable.Columns.Add("充值金额", typeof(string));
                dataTable.Columns.Add("操作员", typeof(string));
                dataTable.Columns.Add("操作员姓名", typeof(string));
                dataTable.Columns.Add("支付订单号", typeof(string));
                dataTable.Columns.Add("住院预交款余额", typeof(string));


                DataRow dataRow = dataTable.NewRow();

                dataRow["凭证名称"] = "网络支付凭证";
                dataRow["医院名称"] = SysOperatorInfo.CustomerName;
                dataRow["收据号"] = this.hisSerialNo;
                dataRow["姓名"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                if (type.Equals("住院"))
                {
                    dataRow["卡余额"] = SkyComm.cardBlance;
                    decimal mInhosMoney = Convert.ToDecimal(hashtable["住院预交款余额"]) + Convert.ToDecimal(hashtable["充值金额"].ToString());
                    dataRow["住院预交款余额"] = mInhosMoney.ToString();
                }
                else
                {
                    decimal m = Convert.ToDecimal(SkyComm.cardBlance) + Convert.ToDecimal(hashtable["充值金额"].ToString());

                    dataRow["卡余额"] = m.ToString();
                }
                
                dataRow["充值金额"] = hashtable["充值金额"].ToString();
                dataRow["操作员"] = SysOperatorInfo.OperatorCode;
                dataRow["操作员姓名"] = SysOperatorInfo.OperatorName;
                dataRow["支付订单号"] = this.bankSerialNo;

                dataTable.Rows.Add(dataRow);
                dataTable.TableName = "cardSavingTable";
                dataTable.WriteXml(Application.StartupPath + @"\\ReportXml\\" + "网络支付凭证" + SkyComm.DiagnoseID +
                                   "_" + this.hisSerialNo + ".xml");
                if (!File.Exists(Application.StartupPath + @"\\Reports\\" + "网络支付凭证" + ".frx"))
                {
//                    SkynetMessage.MsgInfo(hashtable["凭证名称"] + ".frx报表文件不存在，无法打印.");
                    return;
                }
                PrintManager print = new PrintManager();
                print.InitReport("网络支付凭证");
                
                print.AddData(dataTable, "cardSavingTable");

                PrintManager.CanDesign = true;

                print.Print();
                print.Dispose();
                Thread.Sleep(100);
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

        public string getUrlByWebservice(Hashtable hashtable)
        {
//            NetPayService.HISServicesSoapClient mNetPayService = new NetPayService.HISServicesSoapClient();
//            string url = mNetPayService.getNetPayUrl(hashtable);
            return "";
        }

        public List<string> getRequestBaseData()
        {
            EInterfaceinfoFacade interfacefacade = new EInterfaceinfoFacade();

//            string userArea = hashtable["使用范围"].ToString();
//            string type = hashtable["接口类型"].ToString();
            DataSet dsInterface = interfacefacade.dsInterfaceInfo("门诊", "支付接口","云平台支付接口");

            LogService.GlobalInfoMessage("调用基础请求数据"+ dsInterface.Tables[0].Rows.Count);


            string token = string.Empty;
            string hosId = "";
            string strUrl = string.Empty;
            if (dsInterface.Tables[0].Rows.Count > 0)
            {
                LogService.GlobalInfoMessage("调用云平台。获取接口相关信息：" +
                                             dsInterface.Tables[0].Rows[0]["IDESCRIPTION"].ToString() + "|" +
                                             dsInterface.Tables[0].Rows[0]["IPARAMFORMAT"].ToString() + "|" +
                                             dsInterface.Tables[0].Rows[0]["IURL"].ToString());
                token = dsInterface.Tables[0].Rows[0]["IDESCRIPTION"].ToString();
                hosId = dsInterface.Tables[0].Rows[0]["IPARAMFORMAT"].ToString();
                strUrl = dsInterface.Tables[0].Rows[0]["IURL"].ToString();
            }

            List<string> mList = new List<string>();



            try
            {
                mList.Add(token.Split('|')[0]);
                LogService.GlobalInfoMessage("token：" + token.Split('|')[0]);
            }
            catch (Exception e)
            {
                mList.Add(token);
            }
            mList.Add(hosId);
            LogService.GlobalInfoMessage("hosId：" + hosId);
            mList.Add(strUrl);
            LogService.GlobalInfoMessage("strUrl：" + strUrl);
            return mList;
        }

        public string getUrl1(Hashtable hashtable)
        {
            throw new Exception();
        }
    }
}
