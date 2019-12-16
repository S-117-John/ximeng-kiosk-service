using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceManage.Imodel;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using DevExpress.XtraGrid.Blending;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using Skynet.AreaDataExchangeInterfaces.NetPayClouldInterface;
using TiuWeb.ReportBase;
using Skynet.LoggingService;
using BusinessFacade.His.ErpPad;
using System.Web.Script.Serialization;

namespace AutoServiceManage.Model
{
    public class NetPayModelImpl : InetPayModel
    {
        /// <summary>
        /// 保存基础交易信息
        /// </summary>
        public void saveTradeInfo(Hashtable hashtable)
        {
            string mSql = "INSERT INTO T_BANKHISEXCHANGE_TRANS (HISSEQNO,BANKSTATE,HISSTATE,BUSCD,HISID,TRFAMT,USETYPE,OPERATORID,OPERATETIME,DIAGNOSEID,BUSINESSTYPE,DATASOURCES,OHISSEQNO,BANKSEQNO) VALUES (@HISSEQNO ,@BANKSTATE,@HISSTATE,@BUSCD,@HISID,@TRFAMT,@USETYPE,@OPERATORID,@OPERATETIME,@DIAGNOSEID,@BUSINESSTYPE,@DATASOURCES,'','')";

            string start = DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00";

            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            int i = querySolutionFacade.ExeNonQuery(mSql, hashtable);

            if (i <= 0)
            {
                throw new Exception("保存基础信息失败！");
            }
        }

        public DataSet getPayResult(string hisSerialNo)
        {
            DataSet data = new DataSet();

            Hashtable hashtable = new Hashtable();

            hashtable.Add("@HISSEQNO", hisSerialNo);

            string mSql = "SELECT * FROM T_BANKHISEXCHANGE_TRANS where HISSEQNO = @HISSEQNO";// and BANKSTATE = '1'

            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            data = querySolutionFacade.ExeQuery(mSql, hashtable);

            return data;
        }

        public void svaeCard(string type, decimal money, string payMethod, string hisSerialNo)
        {
            CardSavingFacade mCardSavingFacade = new CardSavingFacade();
            if (SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows.Count <= 0)
            {
                throw new Exception("没有找到要充值的卡信息，请拿微信交易单号去窗口办理充值！");
            }

            CardAuthorizationData mCardAuthorizationData = new CardAuthorizationData();

            CardAuthorizationFacade mCardAuthorizationFacade = new CardAuthorizationFacade();

            mCardAuthorizationData = (CardAuthorizationData)mCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);

            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0].BeginEdit();
            //卡号
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID] = SkyComm.cardInfoStruct.CardNo;
            //充值时间
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATETIME] = new CommonFacade().GetServerDateTime();
            ////操作员
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATOR] = SysOperatorInfo.OperatorID;
            ////充值类型
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_SAVINGMODE] = 1;

            Skynet.LoggingService.LogService.GlobalInfoMessage("发卡保存数据设置押金之前");
            ////Add money
            if (type.Equals("办卡"))
            {
                Decimal deposit = Convert.ToDecimal(SkyComm.getvalue("发卡工本费"));
                mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = money - Convert.ToDecimal(SkyComm.dsCardType.Tables[0].Rows[0]["FEES"]) - deposit;
            }
            else
            {
                mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = money;  //此处需调接口
            }
            
            //业务类型
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BUSSNESSTYPE] = "充值";
            //支付方式
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_MODETYPE] = payMethod;
            //单位
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_UNIT] = "";
            //支票号
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CHECKLOT] = "";

            //银行流水号--T_CARD_SAVING.BANKTRANSNO统一保存T_BANKHISEXCHANGE_TRANS的主键HISSEQNO
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_BANKTRANSNO] = hisSerialNo;

            //充值数据来源
            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEYSOURCE] = "自助机";

            mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0].EndEdit();

            ReckonAccountTimeFacade reckonAccountsTimeFacade = new ReckonAccountTimeFacade();
            DateTime accountTime = reckonAccountsTimeFacade.GetEndTime(SysOperatorInfo.OperatorID, "门诊");
            if (Convert.ToDateTime(mCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATETIME]) < accountTime)
            {
                throw new Exception("该时间段已经结帐，不能办理预交金业务，请拿微信交易单号去窗口办理充值！");
               
            }

            DataSet cardSavingData = new DataSet();

            cardSavingData = mCardSavingFacade.insertEntity(mCardAuthorizationData);

            if (cardSavingData == null || cardSavingData.Tables[0].Rows.Count == 0)
            {
                throw new Exception("卡充值失败，请拿微信交易单号去窗口办理充值！");
            }
            PrintInfo("自助充值凭证", cardSavingData.Tables[0].Rows[0]["TRANSACTION_ID"].ToString(), money.ToString());

        }

        private void PrintInfo(string ReportName, string strTRANSACTION_ID, string Money)
        {
            try
            {
                CardAuthorizationData eCardAuthorizationData = new CardAuthorizationData();
                CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();
                eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
                string identity = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYCARD].ToString();
                CardSavingFacade cardSavingFacade = new CardSavingFacade();
                DataSet cardSavingData = cardSavingFacade.FindByPrimaryKey(strTRANSACTION_ID);

                cardSavingData.Tables[0].Columns.Add("身份证", typeof(string));

                if (cardSavingData.Tables[0].Rows.Count > 0)
                {
                    cardSavingData.Tables[0].Rows[0]["身份证"] = identity;
                    cardSavingData.WriteXml(Application.StartupPath + @"\\ReportXml\\" + ReportName + SkyComm.DiagnoseID + "_" + strTRANSACTION_ID + ".xml");
                    if (!File.Exists(Application.StartupPath + @"\\Reports\\" + ReportName + ".frx"))
                    {
                        SkynetMessage.MsgInfo(ReportName + ".frx报表文件不存在，无法打印.");
                        return;
                    }
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
//                if (lex.Message.IndexOf("灾难性") > 0)
//                {
//                    SkynetMessage.MsgInfo(lex.Message + ": 打印机连接失败,请检查!");
//                }
//                else
//                {
//                    SkynetMessage.MsgInfo(lex.Message);
//                }
            }
        }

        public void updateHisState(string hisSerialNo)
        {
            string mSql = "UPDATE T_BANKHISEXCHANGE_TRANS set HISSTATE = '1' where HISSEQNO = @HISSEQNO";

            
            Hashtable hashtable = new Hashtable();

            hashtable.Add("@HISSEQNO", hisSerialNo);
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            int i = querySolutionFacade.ExeNonQuery(mSql, hashtable);

            if (i <= 0)
            {
                throw new Exception("更新医院状态失败！");
            }
        }

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        public string getUrl(Hashtable hashtable)
        {
            try
            {
                
                

                NetPayClouldInterface mNetPayClouldInterface = new NetPayClouldInterface();
                //                List<string> mList = mNetPayClouldInterface.getPayJson(hashtable);

                LogService.GlobalInfoMessage("调用银医geturl有参方法");
                foreach (DictionaryEntry de in hashtable)
                {
                    
                    Console.WriteLine("调用银医geturl有参方法入参"+string.Format("{0}-{1}", de.Key, de.Value));
                }

                //string url = mNetPayClouldInterface.getPayUrl(hashtable);

                string url = getPayUrl(hashtable);

                Log.Info(GetType().ToString(),"二维码："+ url);
               
                return url;
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(), "请求二维码异常：" + e.Message+e.Source+e.StackTrace);
                throw e;
            }

            
        }

        public void revokedTrade(string hisSerialNo,string errorId)
        {
            try
            {
                string mSql = "UPDATE T_BANKHISEXCHANGE_TRANS set REMARK = @REMARK where HISSEQNO = @HISSEQNO";


                Hashtable hashtable = new Hashtable();

                hashtable.Add("@HISSEQNO", hisSerialNo);
                hashtable.Add("@REMARK", errorId);
                QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

                int i = querySolutionFacade.ExeNonQuery(mSql, hashtable);

               
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(),"等待支付结果超时，更新交易表异常："+e.Message);
                
            }
        }

        public List<string> refundTrade(Hashtable hashtable)
        {


            NetPayClouldInterface mNetPayClouldInterface = new NetPayClouldInterface();

            return mNetPayClouldInterface.revokedOrder(hashtable);
        }

        public void updateRefundOrderResult(List<string> list,string hisSerialNo)
        {
            
            try
            {

                string mSql = "UPDATE T_BANKHISEXCHANGE_TRANS set OHISSEQNO = @OHISSEQNO, BANKSTATE = '0', HISSTATE = '0',REFUNDMONEY = @REFUNDMONEY,OPERATETIME = @OPERATETIME where a.HISSEQNO = @HISSEQNO";


                Hashtable hashtable = new Hashtable();

                hashtable.Add("@OHISSEQNO", list[1]);
                hashtable.Add("@REFUNDMONEY", list[2]);
                hashtable.Add("@OPERATETIME", list[3]);
                hashtable.Add("@HISSEQNO", hisSerialNo);
                QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

                int i = querySolutionFacade.ExeNonQuery(mSql, hashtable);

                
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(),"保存"+SkyComm.cardInfoStruct.CardNo+"的退费记录",e.Message);
            }
        }



        public List<string> getRequestBaseData(Hashtable hashtable)
        {
            EInterfaceinfoFacade interfacefacade = new EInterfaceinfoFacade();

            string userArea = hashtable["使用范围"].ToString();
            string type = hashtable["接口类型"].ToString();
            LogService.GlobalInfoMessage("进入撤销方法,userArea=" + userArea + ",type=" + type);

            string sql = "select * from E_INTERFACEINFO where ISTATUS=0 and IUSEAREA=2 and ITYPE='支付接口' and INAME='云平台支付接口'";
            Hashtable h1 = new Hashtable();
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();
            
            //DataSet dsInterface = interfacefacade.dsInterfaceInfo(userArea, type, "云平台支付接口");

            DataSet dsInterface = querySolutionFacade.ExecCustomQuery(sql);


            LogService.GlobalInfoMessage("调用云平台。获取接口相关信息count=" + dsInterface.Tables[0].Rows.Count);
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


        public string getPayUrl(Hashtable hashtable)
        {
            List<string> mList = getRequestBaseData(hashtable);

            RequestUrlJson mRequestUrlJson = new RequestUrlJson();

            mRequestUrlJson.token = mList[0];

            mRequestUrlJson.hosId = mList[1];

            mRequestUrlJson.serviceType = hashtable["serviceType"].ToString();

            mRequestUrlJson.hisTradeId = hashtable["hisTradeId"].ToString();

            mRequestUrlJson.payFee = hashtable["payFee"].ToString();

            mRequestUrlJson.payMethod = hashtable["payMethod"].ToString();

            mRequestUrlJson.mchNum = hashtable["mchNum"].ToString();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string mRequestInfo = "requestJson=" + serializer.Serialize(mRequestUrlJson);
            LogService.GlobalInfoMessage("请求二维码信息：" + mRequestInfo);
            HttpService mHttpService = new HttpService();

            string mRequestUrl = mList[2] + "services/pay/order/apply";
            LogService.GlobalInfoMessage("最终请求二维码信息：" + mRequestUrl);
            string mJsonResult = mHttpService.PostRequest(mRequestUrl, mRequestInfo, 60000, Encoding.UTF8, Encoding.UTF8);
            LogService.GlobalInfoMessage("请求二维码结果：" + mJsonResult);
            Root mRoot = serializer.Deserialize<Root>(mJsonResult);
            ResponseUrlJson mResponseUrlJson = serializer.Deserialize<ResponseUrlJson>(mJsonResult);

            if (mRoot.code.ToString().Equals("0"))
            {
                if (string.IsNullOrEmpty(mRoot.responseData.codeUrl))
                {
                    throw new Exception("请求二维码失败！");
                }

                return mRoot.responseData.codeUrl;

            }
            else
            {
                throw new Exception("请求二维码失败！");
            }


            return mRoot.responseData.codeUrl;



        }


    }
}
