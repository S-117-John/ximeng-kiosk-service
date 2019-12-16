using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Skynet.LoggingService;
using Skynet.Framework.Common;
using BusinessFacade.BankHisExchange;
using EntityData.BankHisExchange;
using AutoServiceSDK.POSInterface.POS003;
using AutoServiceSDK.POSInterface.POS003.Dialogs;
using SystemFramework.Voice;

namespace AutoServiceSDK.POSInterface.POS003
{
    public class SingleePosClass : POSBase
    {
        //定义密码框
        private FrmPasswordBox frmPwd = new FrmPasswordBox();

        #region 初始化
        public SingleePosClass()
            : base()
        {
            Init();
        }
        #endregion

        #region 交易方法
        /// <summary>
        /// 交易方法
        /// </summary>
        /// <param name="TranType">交易类型，1为消费，2消费确认，-2取消消费,5签到，6结算</param>
        /// <param name="htParams">交易参数，HIS流水号，病人ID,金额</param>
        /// <returns></returns>
        public override int Trans(string TranType, Hashtable htParams)
        {
            base.Trans(TranType, htParams);

            switch (TranType)
            {
                case POSTransType.TRANS_BEGIN_PAY: //消费
                    try
                    {
                        BeginPay(htParams);
                    }
                    catch (Exception ex )
                    {

                        throw ex;
                    }              
                    break;

                case POSTransType.TRANS_END_PAY: //消费确认
                    EndPay(htParams);
                    break;

                case POSTransType.TRANS_REVOKE: //撤销消费
                    Revoke(htParams);
                    break;

                case POSTransType.TRANS_SETTLE: //结算
                    Settle(htParams);
                    break;

                case POSTransType.TRANS_SIGN_IN: //签到
                    Settle(htParams); //兼容老代码
                    SignIn(htParams);
                    break;
            }

            return 0;
        }
        #endregion

        #region 环境初始化
        /// <summary>
        /// 环境初始化
        /// </summary>
        private void Init()
        {
            try
            {
                ;
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("环境初始化失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 签到交易
        private void SignIn(Hashtable htParams)
        {
            try
            {
                ;
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("银行签到失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 消费交易
        private void BeginPay(Hashtable htParams)
        {
            String strErrorMsg = string.Empty;
            try
            {
                #region 组织请求入参
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("TransCode", "01");
                dic.Add("TransMoney", CheckMoney(htParams, "MONEY"));
                dic.Add("TransNo", PadRight("", 6));
                dic.Add("MachineID", PadRight("", 10));
                dic.Add("OperatorID", PadRight(SysOperatorInfo.OperatorID, 10));
                dic.Add("ReferenceNo", PadRight("", 15));
                dic.Add("AuthNo", PadRight("", 6));
                dic.Add("OldTransDate", PadRight("", 8));
                dic.Add("CardType", "H");
                dic.Add("IndexNo", PadRight(htParams["POSNO"].ToString(), 76));
                dic.Add("Track2", PadRight("", 37));
                dic.Add("Track3", PadRight("", 104));
                dic.Add("OldTransCode", PadRight("", 2));
                dic.Add("OldTerminalID", PadRight("", 15));
                dic.Add("OldAuthNo", PadRight("", 15));
                dic.Add("PayAccountID", PadRight("", 3));
                dic.Add("StoreID", PadRight("", 20));
                dic.Add("ReceiptID", PadRight("", 30));
                dic.Add("OrderID", PadRight("", 15));
                dic.Add("OldBatchNo", PadRight("", 6));
                dic.Add("OldTransTime", PadRight("", 6));
                dic.Add("OldTransMoney", PadRight("", 12));
                dic.Add("ServerIP", PadRight("", 15));
                dic.Add("ListenPort", PadRight("", 6));
                string strInput = dic.GetValueString();
                #endregion

                #region 平台接口调用
                LogService.GlobalInfoMessage("消费交易调用入参：" + strInput);
                StringBuilder sb = new StringBuilder(1000);
                //int iResult = SingleeMethods.CardTransCBK(strInput, sb, new SingleeMethods.EnterPasswordCallBack(EnterPasswordHandler), frmPwd.Handle);
                int iResult = SingleeMethods.CardTransDllWin(strInput, sb);
                string strOutput = sb.ToString();
                LogService.GlobalInfoMessage("消费交易调用出参：" + strOutput);
                #endregion

                #region 解析请求出参
              
                byte[] bytes = System.Text.Encoding.Default.GetBytes(strOutput);
                string retCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 0, 6));
                string retMsg = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 6, 40));
             
                #endregion

                #region 交易结果处理
                if (retCode == "000000")
                {
                    string transNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 46, 6));
                    string authNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 52, 6));
                    string batchNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 58, 6));
                    string bankCardID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 64, 19));
                    string expDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 83, 4));
                    string bankNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 87, 2));
                    string referenceNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 89, 12));
                    string terminalNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 101, 15));
                    string merchantID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 116, 15));
                    string transMoney = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 131, 12));
                    string indexNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 143, 16));
                    string customField = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 159, 74));
                    string sendCardBankCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 234, 7));
                    string bankDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 241, 8));
                    string bankTime = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 249, 6));

                    #region 交易成功
                    LogService.GlobalInfoMessage("消费交易调用成功。" + retMsg);

                    #region 广播出参
                    htParams.Add("REFERENCENO", referenceNo);
                    htParams.Add("BANKCARDID", bankCardID);
                    htParams.Add("BANKSEQNO", transNo);
                    #endregion


                    #region 把交易数据写入数据库
                    string seqNo = htParams["SEQNO"].ToString();
                    string diagnoseID = htParams["DIAGNOSEID"].ToString();
                    string cardID = htParams["CARDID"].ToString();

                    //string strDateTime = DateTime.Now.ToString();
                    //if (!string.IsNullOrEmpty(bankDate.Trim()))
                    //{
                    //    strDateTime=bankDate.Substring(0, 4) + "-" + bankDate.Substring(4, 2) + "-" + bankDate.Substring(6, 2) + " " + bankTime.Substring(0, 2) + ":" + bankTime.Substring(2, 2) + ":" + bankTime.Substring(4, 2);

                    //}
                    //DateTime payDateTime = Convert.ToDateTime(strDateTime);
                    try
                    {
                        DateTime dtn;
                        string strDate = string.Empty;
                        if (!string.IsNullOrEmpty(bankDate.Trim()))
                        {
                            strDate = bankDate.Substring(0, 4) + "-" + bankDate.Substring(4, 2) + "-" + bankDate.Substring(6, 2) + " " + bankTime.Substring(0, 2) + ":" + bankTime.Substring(2, 2);

                        }
                        DateTime payDateTime = DateTime.TryParse(strDate, out dtn) ? dtn : DateTime.Now;

                        decimal payMoney = decimal.Parse(transMoney.TrimStart('0'));

                        TBankhisexchangeTransData dataPayment = new TBankhisexchangeTransData()
                        {
                            InterfaceName="锡盟新利银医接口",
                            Hisseqno = seqNo,
                            Hisstate = "0",
                            Bankseqno = transNo,
                            Bankstate = "1",
                            DIAGNOSEID = diagnoseID,
                            Hisid = cardID,
                            Usetype = "消费",
                            DataSources = "自助",
                            BusinessType = "POS扣款",
                            Operatorid = SysOperatorInfo.OperatorID,
                            Operatetime = payDateTime,
                            TerminalNo = terminalNo,
                            Trfamt = payMoney.ToString("0.##"),
                            ITEM1 = bankCardID,
                            ITEM2 = batchNo,
                            ITEM3 = string.Empty,
                            Ohisseqno = referenceNo,
                            MerchantID = merchantID,
                        };

                        TBankhisexchangeTransFacade facBankTrans = new TBankhisexchangeTransFacade();
                        facBankTrans.Insert(dataPayment);
                        LogService.GlobalInfoMessage("消费交易数据保存完成。");
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("消费交易数据保存失败");
                        #region 打印交易凭证
                        //PrintInfo(htParams);
                        SkynetMessage.MsgInfo("预交金充值失败，需要撤销银行交易，请按POS机提示操作，点击确定后，请重新插入银行卡");
                        Revoke(htParams);                     
                        #endregion
                        //throw new Exception(string.Format("消费交易数据保存失败！已调用撤销方法！,错误:{0}",ex.Message));
                        throw new Exception(string.Format("银行交易失败，充值金额已退回卡，请重新充值！"));
                    }

                    #endregion
                    #endregion
                }
                #region 交易失败
                else if (retCode == "C41003")
                {
                    strErrorMsg = "取消交易,请重新操作!";
                }
                else if (retCode == "C85006")
                {
                    strErrorMsg = "密码错误,请重新操作!";
                }
                else if (retCode == "C40006")
                {
                    strErrorMsg = "读卡错误，请检查卡片是否插错!";
                }
                else if (retCode == "C99999")
                {
                    strErrorMsg = "银行卡余额不足，交易失败!";
                }
                else
                {
                    strErrorMsg = string.Format("消费交易调用失败:{0}", retMsg);
                }
                if (!string.IsNullOrEmpty(strErrorMsg))
                {
                    LogService.GlobalInfoMessage(strErrorMsg);
                    //SkynetMessage.MsgInfo(strErrorMsg);

                    //Voice voice = new Voice();
                    //voice.PlayText(strErrorMsg);
                    //voice.EndJtts();
                    throw new Exception(strErrorMsg);
                }
                #endregion
                #endregion
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage(e.Message);
                //SkynetMessage.MsgInfo(e.Message);
                throw new Exception(string.Format("{0}", e.Message));
            }
        }
        #endregion

        #region 撤销交易
        private void Revoke(Hashtable htParams)
        {
            try
            {
                if (!htParams.ContainsKey("BANKSEQNO"))
                {
                    LogService.GlobalInfoMessage("撤销交易入参列表没有POS流水号，不需要进行撤销。");
                    return;
                }

                #region 组织请求入参
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("TransCode", "02");
                dic.Add("TransMoney", CheckMoney(htParams, "MONEY"));
                dic.Add("TransNo", PadRight(htParams["BANKSEQNO"].ToString(), 6));
                dic.Add("MachineID", PadRight("", 10));
                dic.Add("OperatorID", PadRight(SysOperatorInfo.OperatorID, 10));
                dic.Add("ReferenceNo", PadRight("", 15));
                dic.Add("AuthNo", PadRight("", 6));
                dic.Add("OldTransDate", PadRight("", 8));
                dic.Add("CardType", "H");
                dic.Add("IndexNo", PadRight(htParams["POSNO"].ToString(), 76));
                dic.Add("Track2", PadRight("", 37));
                dic.Add("Track3", PadRight("", 104));
                dic.Add("OldTransCode", PadRight("", 2));
                dic.Add("OldTerminalID", PadRight("", 15));
                dic.Add("OldAuthNo", PadRight("", 15));
                dic.Add("PayAccountID", PadRight("", 3));
                dic.Add("StoreID", PadRight("", 20));
                dic.Add("ReceiptID", PadRight("", 30));
                dic.Add("OrderID", PadRight("", 15));
                dic.Add("OldBatchNo", PadRight("", 6));
                dic.Add("OldTransTime", PadRight("", 6));
                dic.Add("OldTransMoney", PadRight("", 12));
                dic.Add("ServerIP", PadRight("", 15));
                dic.Add("ListenPort", PadRight("", 6));
                string strInput = dic.GetValueString();
                #endregion

                #region 平台接口调用
                LogService.GlobalInfoMessage("撤销交易调用入参：" + strInput);
                StringBuilder sb = new StringBuilder(1000);
                //int iResult = SingleeMethods.CardTransCBK(strInput, sb, null, IntPtr.Zero);
                int iResult = SingleeMethods.CardTransDllWin(strInput, sb);
                
                string strOutput = sb.ToString();
                LogService.GlobalInfoMessage("撤销交易调用出参：" + strOutput);
                #endregion

                #region 解析请求出参
                byte[] bytes = System.Text.Encoding.Default.GetBytes(strOutput);
                string retCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 0, 6));
                string retMsg = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 6, 40));
            
                #endregion

                #region 交易结果处理
                if (retCode == "000000")
                {
                    string transNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 46, 6));
                    string authNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 52, 6));
                    string batchNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 58, 6));
                    string bankCardID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 64, 19));
                    string expDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 83, 4));
                    string bankNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 87, 2));
                    string referenceNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 89, 12));
                    string terminalNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 101, 15));
                    string merchantID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 116, 15));
                    string transMoney = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 131, 12));
                    string indexNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 143, 16));
                    string customField = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 159, 74));
                    string sendCardBankCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 234, 7));
                    string bankDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 241, 8));
                    string bankTime = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 249, 6));

                    #region 交易成功
                    LogService.GlobalInfoMessage("撤销交易调用成功。");

                    #region 把交易数据写入数据库
                    string seqNo = htParams["SEQNO"].ToString();
                    string diagnoseID = htParams["DIAGNOSEID"].ToString();
                    string cardID = htParams["CARDID"].ToString();
                    //string strDateTime = bankDate.Substring(0, 4) + "-" + bankDate.Substring(4, 2) + "-" + bankDate.Substring(6, 2) + " " + bankTime.Substring(0, 2) + ":" + bankTime.Substring(2, 2) + ":" + bankTime.Substring(4, 2);
                    //DateTime payDateTime = Convert.ToDateTime(strDateTime);

                    DateTime dtn;
                    string strDate = string.Empty;
                    if (!string.IsNullOrEmpty(bankDate.Trim()))
                    {
                        strDate = bankDate.Substring(0, 4) + "-" + bankDate.Substring(4, 2) + "-" + bankDate.Substring(6, 2) + " " + bankTime.Substring(0, 2) + ":" + bankTime.Substring(2, 2);

                    }
                    DateTime payDateTime = DateTime.TryParse(strDate, out dtn) ? dtn : DateTime.Now;

                    decimal payMoney = decimal.Parse(transMoney.TrimStart('0'));

                    TBankhisexchangeTransFacade facBankTrans = new TBankhisexchangeTransFacade();
                    DataSet dsPayment = facBankTrans.Select(string.Format("HISSEQNO = '{0}'", seqNo));
                    if (dsPayment.Tables[0].Rows.Count > 0)
                    {
                        TBankhisexchangeTransData dataPayment = new TBankhisexchangeTransData()
                        {
                            InterfaceName = "锡盟新利银医接口",
                            Hisseqno = seqNo + "C",
                            Hisstate = "1",
                            Bankseqno = transNo,
                            Bankstate = "1",
                            DIAGNOSEID = diagnoseID,
                            Hisid = cardID,
                            Usetype = "冲正",
                            DataSources = "自助",
                            BusinessType = "POS冲正",
                            Operatorid = SysOperatorInfo.OperatorID,
                            Operatetime = payDateTime,
                            TerminalNo = terminalNo,
                            Trfamt = "-" + payMoney.ToString("0.##"),
                            ITEM1 = bankCardID,
                            ITEM2 = batchNo,
                            ITEM3 = string.Empty,
                            Ohisseqno = referenceNo,
                            MerchantID = merchantID,
                        };

                        facBankTrans.Insert(dataPayment);
                    }
                    LogService.GlobalInfoMessage("撤销交易数据保存完成。");
                    #endregion
                    #endregion
                }
                else
                {
                    #region 交易失败
                    LogService.GlobalInfoMessage("撤销交易调用失败。" + retMsg);
                    throw new Exception(retMsg);
                    #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("银行撤销失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 退货交易
        private void BeginRefund(Hashtable htParams)
        {
            try
            {
                #region 组织请求入参
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("TransCode", "09");
                dic.Add("TransMoney", CheckMoney(htParams, "MONEY"));
                dic.Add("TransNo", PadRight("", 6));
                dic.Add("MachineID", PadRight("", 10));
                dic.Add("OperatorID", PadRight(SysOperatorInfo.OperatorID, 10));
                dic.Add("ReferenceNo", PadRight(htParams["REFERENCENO"].ToString(), 15));
                dic.Add("AuthNo", PadRight("", 6));
                dic.Add("OldTransDate", Convert.ToDateTime(htParams["OPERATETIME"]).ToString("YYYYMMDD"));
                dic.Add("CardType", "H");
                dic.Add("IndexNo", PadRight(htParams["POSNO"].ToString(), 76));
                dic.Add("Track2", PadRight("", 37));
                dic.Add("Track3", PadRight("", 104));
                dic.Add("OldTransCode", PadRight("", 2));
                dic.Add("OldTerminalID", PadRight("", 15));
                dic.Add("OldAuthNo", PadRight("", 15));
                dic.Add("PayAccountID", PadRight("", 3));
                dic.Add("StoreID", PadRight("", 20));
                dic.Add("ReceiptID", PadRight("", 30));
                dic.Add("OrderID", PadRight("", 15));
                dic.Add("OldBatchNo", PadRight("", 6));
                dic.Add("OldTransTime", PadRight("", 6));
                dic.Add("OldTransMoney", PadRight("", 12));
                dic.Add("ServerIP", PadRight("", 15));
                dic.Add("ListenPort", PadRight("", 6));
                string strInput = dic.GetValueString();
                #endregion

                #region 平台接口调用
                LogService.GlobalInfoMessage("退货交易调用入参：" + strInput);
                StringBuilder sb = new StringBuilder(1000);
                //int iResult = SingleeMethods.CardTransCBK(strInput, sb, null, IntPtr.Zero);
                int iResult = SingleeMethods.CardTransDllWin(strInput, sb);
                string strOutput = sb.ToString();
                LogService.GlobalInfoMessage("退货交易调用出参：" + strOutput);
                #endregion

                #region 解析请求出参
                byte[] bytes = System.Text.Encoding.Default.GetBytes(strOutput);
                string retCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 0, 6));
                string retMsg = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 6, 40));
           
                #endregion

                #region 交易结果处理
                if (retCode == "000000")
                {
                    string transNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 46, 6));
                    string authNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 52, 6));
                    string batchNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 58, 6));
                    string bankCardID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 64, 19));
                    string expDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 83, 4));
                    string bankNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 87, 2));
                    string referenceNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 89, 12));
                    string terminalNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 101, 15));
                    string merchantID = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 116, 15));
                    string transMoney = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 131, 12));
                    string indexNo = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 143, 16));
                    string customField = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 159, 74));
                    string sendCardBankCode = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 234, 7));
                    string bankDate = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 241, 8));
                    string bankTime = System.Text.Encoding.Default.GetString(SingleeMethods.SubByte(bytes, 249, 6));

                    #region 交易成功
                    LogService.GlobalInfoMessage("退货交易调用成功。" + retMsg);

                    #region 把交易数据写入数据库
                    string seqNo = htParams["SEQNO"].ToString();
                    string diagnoseID = htParams["DIAGNOSEID"].ToString();
                    string cardID = htParams["CARDID"].ToString();

                    DateTime dtn;
                    string strDate = string.Empty;
                    if (!string.IsNullOrEmpty(bankDate.Trim()))
                    {
                        strDate = bankDate.Substring(0, 4) + "-" + bankDate.Substring(4, 2) + "-" + bankDate.Substring(6, 2) + " " + bankTime.Substring(0, 2) + ":" + bankTime.Substring(2, 2);

                    }
                    DateTime payDateTime = DateTime.TryParse(strDate, out dtn) ? dtn : DateTime.Now;
                    decimal payMoney = decimal.Parse(transMoney.TrimStart('0'));

                    TBankhisexchangeTransData dataPayment = new TBankhisexchangeTransData()
                    {
                        Hisseqno = seqNo + "R",
                        Hisstate = "0",
                        Bankseqno = transNo,
                        Bankstate = "1",
                        DIAGNOSEID = diagnoseID,
                        Hisid = cardID,
                        Usetype = "退款",
                        DataSources = "自助",
                        BusinessType = "POS退款",
                        Operatorid = SysOperatorInfo.OperatorID,
                        Operatetime = payDateTime,
                        TerminalNo = terminalNo,
                        Trfamt = "-" + payMoney.ToString("0.##"),
                        ITEM1 = bankCardID,
                        ITEM2 = batchNo,
                        ITEM3 = string.Empty,
                        Ohisseqno = referenceNo,
                        MerchantID = merchantID,
                    };

                    TBankhisexchangeTransFacade facBankTrans = new TBankhisexchangeTransFacade();
                    facBankTrans.Insert(dataPayment);
                    LogService.GlobalInfoMessage("退款交易数据保存完成。");
                    #endregion
                    #endregion
                }
                else
                {
                    #region 交易失败
                    LogService.GlobalInfoMessage("退货交易调用失败。" + retMsg);
                    throw new Exception(retMsg);
                    #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("银行退款失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 结算交易
        private void Settle(Hashtable htParams)
        {
            try
            {
                ;
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("银行结算失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 消费确认
        private void EndPay(Hashtable htParams)
        {
            try
            {
                TBankhisexchangeTransFacade facBankTrans = new TBankhisexchangeTransFacade();
                string seqNo = htParams["SEQNO"].ToString();
                LogService.GlobalInfoMessage(seqNo);
                facBankTrans.UpdateState(seqNo, "HISSTATE", "1");
                LogService.GlobalInfoMessage("消费交易确认成功。");
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("消费交易确认失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 退货确认
        private void EndRefund(Hashtable htParams)
        {
            try
            {
                TBankhisexchangeTransFacade facBankTrans = new TBankhisexchangeTransFacade();
                string seqNo = htParams["SEQNO"].ToString() + "R";
                LogService.GlobalInfoMessage(seqNo);
                facBankTrans.UpdateState(seqNo, "HISSTATE", "1");
                LogService.GlobalInfoMessage("退货交易确认成功。");
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("退货交易确认失败。" + e.Message);
                throw;
            }
        }
        #endregion

        #region 金额处理
        /// <summary>
        /// 获取与HIS交互的数字字符串，如12.28,返回000000001228
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string CheckMoney(Hashtable ht, string key)
        {
            if (ht[key] == null)
                throw new Exception(key + "信息不能为空");
            decimal decMoney = 0;
            decimal decOldMoney = 0;
            if (decimal.TryParse(ht[key].ToString(), out decOldMoney) == false)
                throw new Exception(string.Format("{0}传入POS接口的{0}" + ht[key] + "不正确,必须是数字!", key));

            decMoney = DecimalRound.Round(decOldMoney, 2);
            if (decMoney != decOldMoney)
                throw new Exception(string.Format("{0}传入POS接口的{0}" + ht[key] + "不正确,必须是2位小数!", key));
            var strMoney = DecimalRound.Round(decMoney * 100, 0).ToString(System.Globalization.CultureInfo.InvariantCulture);
            LogService.GlobalInfoMessage("银行卡交易转换后的金额" + strMoney);
            return strMoney.PadLeft(12, '0');
        }

        #endregion

        #region 字段补齐
        private string PadRight(string str, int length)
        {
            return str.PadRight(length, ' ');
        }
        #endregion

        #region 检索配置文件
        private string GetValue(string key)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "system.config";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            //得到顶层节点列表 
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlElement element in topM)
            {
                if (element.Name.ToLower() == "appsettings")
                {
                    //得到该节点的子节点 
                    XmlNodeList nodelist = element.ChildNodes;
                    if (nodelist.Count > 0)
                    {
                        try
                        {
                            for (int i = 0; i < nodelist.Count; i++)
                            {
                                try
                                {
                                    XmlElement el = (XmlElement)nodelist[i];
                                    if (el.Attributes["key"].Value == key)
                                    {
                                        return el.Attributes["value"].Value;
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        #region 打印报表
        /// <summary>
        /// 防止单边交易：银行成功、HIS失败
        /// </summary>
        public void PrintInfo(Hashtable htParams)
        {
            try
            {
                using (TiuWeb.ReportBase.PrintManager print = new TiuWeb.ReportBase.PrintManager())
                {

                    string fileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Reports\交易凭证.frx";
                    print.LoadFromFile(fileName);
                    print.AddParam("医院名称", SysOperatorInfo.CustomerName);
                    print.AddParam("收据号", htParams["BANKSEQNO"].ToString());
                    print.AddParam("银行参考号", htParams["REFERENCENO"].ToString());
                    print.AddParam("充值金额", htParams["MONEY"].ToString());
                    print.AddParam("操作员", SysOperatorInfo.OperatorCode);
                    print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
                    print.Print();
                }
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("打印交易凭证失败。" + e.Message);
            }
        }
        #endregion

        #region 处理密码输入
        private void EnterPasswordHandler(IntPtr pConf, char chKey)
        {
            SkynetMessage.MsgInfo(chKey.ToString());
            frmPwd = new FrmPasswordBox();
            frmPwd.AcceptPwdInput(pConf, chKey);
        }
        #endregion
    }
}
