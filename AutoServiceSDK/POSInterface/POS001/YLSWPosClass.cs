using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using BusinessFacade.BankHisExchange;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using EntityData.BankHisExchange;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using SystemFramework.NewCommon;

namespace AutoServiceSDK.POSInterface.POS001
{
    public class YLSWPosClass : POSBase
    {
        #region 初始化
        public YLSWPosClass() : base()
        {

        }
        #endregion

        #region API引用

        [DllImport("c:\\gmc\\posinf.dll")]
        private static extern int bankall(StringBuilder request, StringBuilder response);

        private int Pos_BankAll(StringBuilder request, StringBuilder response)
        {
            LogService.GlobalInfoMessage("调用bankall方法传入的参数：" + request.ToString());
            int intResult = bankall(request, response);
            LogService.GlobalInfoMessage("调用bankall方法返回值" + intResult + "，输出参数：" + response.ToString());
            return intResult;
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

            #region 消费
            if (TranType == "1")
            {
                #region 组织参数
                //POS机号使用自助机的编号
                string strInput = Check(htParams, "POSNO").Trim().PadRight(8, ' ').Substring(0,8);

                //POS机操作员
                strInput += SysOperatorInfo.OperatorCode.Trim().PadRight(8, ' ');

                //交易类型
                strInput += "00";

                //获取交易金额，根据是12位的字符串，没有小数点到小数点后两位
                string strMoney = CheckMoney(htParams, "MONEY");
                strInput += strMoney;

                //原交易日期
                strInput += "".PadRight(8, ' ');

                //原交易参考号
                strInput += "".PadRight(12, ' ');

                //原凭证号
                strInput += "".PadRight(6, ' ');

                //LRC校验
                Random rm = new Random();
                int intrm = rm.Next(100, 999);
                strInput += intrm.ToString();

                string strDiagnoseID = Check(htParams, "DIAGNOSEID");
                string Seqno = Check(htParams, "SEQNO");
                string CardID = Check(htParams, "CARDID");

                #endregion 

                #region 调用POS接口
                StringBuilder sbInput = new StringBuilder(strInput);
                StringBuilder sbOutPut = new StringBuilder(150);
                OutputData theOutputData = null;

                try
                {
                    //调用银行交易
                    Pos_BankAll(sbInput, sbOutPut);

                    theOutputData = new OutputData(sbOutPut);
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用银行交易方法bankall失败：" + ex.Message);
                    throw ex;
                }
                #endregion

                #region 保存POS结算结果

                try
                {
                    DateTime ServerDate = new CommonFacade().GetServerDateTime();                    
                    TBankhisexchangeTransData data = new TBankhisexchangeTransData();

                    //银行行号+批次号+授权号
                    data.Remark = theOutputData.BankCode + "|" + theOutputData.BatchNo+"|"+theOutputData.LicenseNo;
                    
                    //银行卡号
                    data.ITEM1 = theOutputData.CardNo;

                    //凭证号
                    data.Bankseqno = theOutputData.VoucherNo;
                    
                    //金额
                    data.Trfamt = theOutputData.TranMoney.ToString();

                    //商户号
                    data.Buscd = theOutputData.MchId;
                    
                    //终端号
                    data.TerminalNo = theOutputData.TerminalNo;

                    //批次号
                    data.ITEM2 = theOutputData.BatchNo;

                    //交易日期
                    data.Operatetime = theOutputData.YHJYSJ;
                    data.Operatorid = SysOperatorInfo.OperatorID;
                                        
                    //交易参考号
                    data.Ohisseqno = theOutputData.RefNo;
                                       
                    data.Usetype = "充值" ;
                    data.Hisid = CardID;
                    data.DIAGNOSEID = strDiagnoseID;
                    data.Hisseqno = Seqno;
                                   
                    data.DataSources = "自助";
                    data.BusinessType = "POS充值";
                    data.Bankstate = "0";
                    data.Hisstate = "0";

                    TBankhisexchangeTransFacade facade = new TBankhisexchangeTransFacade();
                    facade.Insert(data);
                    LogService.GlobalInfoMessage("调用银行支付交易成功，写入数据库成功！");
                    htParams.Add("CARDNO", theOutputData.CardNo);
                    htParams.Add("BANKSEQNO", theOutputData.RefNo + "|" + theOutputData.VoucherNo);
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("保存POS直联记录表T_BANKHISEXCHANGE_TRANS失败：" + ex.Message);
                    DeleteBankTran(htParams, theOutputData.VoucherNo);
                    throw ex;
                }
                #endregion
            }

            #endregion

            #region 消费确认
            if (TranType == "2")
            {
                //更新交易中间层的记录标识T_BANKHISEXCHANGE_RECORD
                TBankhisexchangeTransFacade facade = new TBankhisexchangeTransFacade();
                string Seqno = Check(htParams, "SEQNO");
                facade.UpdateState(Seqno, "HISSTATE", "1");
            }
            #endregion

            #region 消费撤消
            if (TranType == "-2")
            {
                //根据交易流水号查询HIS中已经存在的交易记录
                string Seqno = Check(htParams, "SEQNO");
                string strDiagnoseID = Check(htParams, "DIAGNOSEID");
                string CardID = Check(htParams, "CARDID");

                TBankhisexchangeTransFacade facade = new TBankhisexchangeTransFacade();
                EntityList<TBankhisexchangeTransData> list = facade.Get("HISSEQNO = '" + Seqno + "'");
                if (list.Count > 0)
                {
                    #region 如果已经存在，则调用撤消方法

                    #region 组织相关参数
                    LogService.GlobalInfoMessage("HIS处理失败，请用银行的撤消交易开始!");
                    //POS机号使用自助机的编号
                    string strInput = Check(htParams, "POSNO").Trim().PadRight(8, ' ').Substring(0, 8);

                    //POS机操作员
                    strInput += SysOperatorInfo.OperatorCode.Trim().PadRight(8, ' ');

                    //交易类型
                    strInput += "01";

                    //获取交易金额，根据是12位的字符串，没有小数点到小数点后两位            
                    string strMoney = CheckMoney(htParams, "MONEY");
                    strInput += strMoney;

                    //原交易日期
                    strInput += "".PadRight(8, ' ');

                    //原交易参考号
                    strInput += "".PadRight(12, ' ');

                    //原凭证号
                    strInput += list[0].Bankseqno;

                    //LRC校验
                    Random rm = new Random();
                    int intrm = rm.Next(100, 999);
                    strInput += intrm.ToString();

                    #endregion

                    #region 调用POS接口
                    StringBuilder sbInput = new StringBuilder(strInput);
                    StringBuilder sbOutPut = new StringBuilder(150);
                    OutputData theOutputData = null;
                    
                    try
                    {
                        //调用银行交易
                        Pos_BankAll(sbInput, sbOutPut);

                        theOutputData = new OutputData(sbOutPut);
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用银行交易方法bankall失败：" + ex.Message);
                        throw ex;
                    }
                    #endregion

                    #region 保存POS结算结果
                    try
                    {
                        facade.UpdateState(Seqno, "HISSTATE", "-1");

                        DateTime ServerDate = new CommonFacade().GetServerDateTime();                       
                        TBankhisexchangeTransData data = new TBankhisexchangeTransData();

                        //银行行号+批次号+授权号
                        data.Remark = theOutputData.BankCode + "|" + theOutputData.BatchNo + "|" + theOutputData.LicenseNo;

                        //银行卡号
                        data.ITEM1 = theOutputData.CardNo;

                        //凭证号
                        data.Bankseqno = theOutputData.VoucherNo;

                        //金额
                        data.Trfamt = (theOutputData.TranMoney * -1).ToString();

                        //商户号
                        data.Buscd = theOutputData.MchId;

                        //终端号
                        data.TerminalNo = theOutputData.TerminalNo;

                        //批次号
                        data.ITEM2 = theOutputData.BatchNo;

                        //交易日期
                        data.Operatetime = theOutputData.YHJYSJ;
                        data.Operatorid = SysOperatorInfo.OperatorID;

                        //交易参考号
                        data.Ohisseqno = theOutputData.RefNo;

                        data.Usetype = "冲正";
                        data.Hisid = CardID;
                        data.DIAGNOSEID = strDiagnoseID;

                        data.Hisseqno = Seqno+"R";

                        data.DataSources = "自助";
                        data.BusinessType = "POS充值";
                        data.Bankstate = "0";
                        data.Hisstate = "-2";

                        facade.Insert(data);
                        LogService.GlobalInfoMessage("调用银行支付交易成功，写入数据库成功！");

                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("保存POS直联记录表T_BANKHISEXCHANGE_TRANS失败：" + ex.Message);
                        throw ex;
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    LogService.GlobalInfoMessage("HIS结算失败，调用银行冲正查询数据未找到，条件：" + "HISSEQNO = '" + Seqno + "'");
                }
            }
            #endregion

            #region 签到
            if (TranType == "5")
            {
                string PosNo = Check(htParams, "POSNO").Trim();
                //判断当天是否已经进行签到，如果签到不再进行签到
                DateTime ServerDate = new CommonFacade().GetServerDateTime();
                string Currdate = ServerDate.ToString("yyyyMMdd");
                HealthCardAuthkeyFacade theAuthKeyfacade = new HealthCardAuthkeyFacade();
                HealthCardAuthkeyData theEntitydata = theAuthKeyfacade.GetByPrimaryKey(PosNo, Currdate);
                if (theEntitydata != null)
                {
                    return 0;
                }
                try
                {
                    Trans("6", htParams);
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用 POS结算业务失败：" + ex.Message);
                }
                #region 组织参数
                //POS机号使用自助机的编号
                string strInput = Check(htParams, "POSNO").Trim().PadRight(8, ' ').Substring(0, 8);

                //POS机操作员
                strInput += SysOperatorInfo.OperatorCode.Trim().PadRight(8, ' ');

                //交易类型
                strInput += "05";

                //获取交易金额               
                strInput += "000000000000";

                //原交易日期
                strInput += "".PadRight(8, ' ');

                //原交易参考号
                strInput += "".PadRight(12, ' ');

                //原凭证号
                strInput += "".PadRight(6, ' ');

                //LRC校验
                Random rm = new Random();
                int intrm = rm.Next(100, 999);
                strInput += intrm.ToString();

                #endregion

                #region 调用POS接口
                StringBuilder sbInput = new StringBuilder(strInput);
                StringBuilder sbOutPut = new StringBuilder(150);
                OutputData theOutputData = null;

                try
                {
                    //调用银行交易
                    Pos_BankAll(sbInput, sbOutPut);

                    theOutputData = new OutputData(sbOutPut);

                    theEntitydata = new HealthCardAuthkeyData();
                    theEntitydata.Orgcode = PosNo;
                    theEntitydata.Username = Currdate;
                    theEntitydata.Authkey = theOutputData.MchId + "|" + theOutputData.TranTime;
                    theAuthKeyfacade.Insert(theEntitydata);

                    LogService.GlobalInfoMessage("调用银行交易方法bankall签到交易成功！");
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用银行交易方法bankall签到失败：" + ex.Message);
                    throw ex;
                }
                #endregion
            }
            #endregion
            
            #region 结算
            if (TranType == "6")
            {
                #region 组织参数
                //POS机号使用自助机的编号
                string strInput = Check(htParams, "POSNO").Trim().PadRight(8, ' ').Substring(0, 8);

                //POS机操作员
                strInput += SysOperatorInfo.OperatorCode.Trim().PadRight(8, ' ');

                //交易类型
                strInput += "06";

                //获取交易金额               
                strInput += "000000000000";

                //原交易日期
                strInput += "".PadRight(8, ' ');

                //原交易参考号
                strInput += "".PadRight(12, ' ');

                //原凭证号
                strInput += "".PadRight(6, ' ');

                //LRC校验
                Random rm = new Random();
                int intrm = rm.Next(100, 999);
                strInput += intrm.ToString();

                #endregion

                #region 调用POS接口
                StringBuilder sbInput = new StringBuilder(strInput);
                StringBuilder sbOutPut = new StringBuilder(150);
                OutputData theOutputData = null;

                try
                {
                    //调用银行交易
                    Pos_BankAll(sbInput, sbOutPut);

                    theOutputData = new OutputData(sbOutPut);
                    LogService.GlobalInfoMessage("调用银行交易方法bankall结算交易成功！");
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用银行交易方法bankall结算失败：" + ex.Message);
                    throw ex;
                }
                #endregion
            }
            #endregion

            return 0;
        }


        /// <summary>
        /// 获取与HIS交互的数据
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string Check(Hashtable ht, string Key)
        {
            if (ht[Key] == null)
                throw new Exception(string.Format("{0}信息不能为空", Key));
            else
                return ht[Key].ToString();
        }

        /// <summary>
        /// 获取与HIS交互的数字字符串，如12.28,返回000000001228
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string CheckMoney(Hashtable ht, string Key)
        {
            if (ht[Key] == null)
                throw new Exception(string.Format("{0}信息不能为空", Key));
            else
            {
                decimal decMoney = 0;
                decimal decOldMoney = 0;
                if (decimal.TryParse(ht[Key].ToString(), out decOldMoney) == false)
                {
                    throw new Exception(string.Format("{0}传入POS接口的{0}" + ht[Key].ToString() + "不正确,必须是数字!", Key));
                }

                decMoney = DecimalRound.Round(decOldMoney, 2);
                if (decMoney != decOldMoney)
                {
                    throw new Exception(string.Format("{0}传入POS接口的{0}" + ht[Key].ToString() + "不正确,必须是2位小数!", Key));
                }
                string strMoney = DecimalRound.Round(decMoney * 100, 0).ToString();
                return strMoney.PadLeft(12, '0');
            }
        }


        private void DeleteBankTran(Hashtable htParams, string Bankseqno)
        {
            LogService.GlobalInfoMessage("HIS处理失败，请用银行的撤消交易开始!");
            //POS机号使用自助机的编号
            string strInput = Check(htParams, "POSNO").Trim().PadRight(8, ' ').Substring(0, 8);

            //POS机操作员
            strInput += SysOperatorInfo.OperatorCode.Trim().PadRight(8, ' ');

            //交易类型
            strInput += "01";

            //获取交易金额，根据是12位的字符串，没有小数点到小数点后两位            
            string strMoney = CheckMoney(htParams, "MONEY");
            strInput += strMoney;

            //原交易日期
            strInput += "".PadRight(8, ' ');

            //原交易参考号
            strInput += "".PadRight(12, ' ');

            //原凭证号
            strInput += Bankseqno;

            //LRC校验
            Random rm = new Random();
            int intrm = rm.Next(100, 999);
            strInput += intrm.ToString();
            string strDiagnoseID = Check(htParams, "DIAGNOSEID");

            StringBuilder sbInput = new StringBuilder(strInput);
            StringBuilder sbOutPut = new StringBuilder(150);
            OutputData theOutputData = null;

            try
            {
                //调用银行交易
                Pos_BankAll(sbInput, sbOutPut);

                theOutputData = new OutputData(sbOutPut);
            }
            catch (Exception ex)
            {
                LogService.GlobalInfoMessage("调用银行交易方法bankall失败：" + ex.Message);
                throw ex;
            }
        }

        public class OutputData
        {
            public OutputData(StringBuilder outputPara)
            {
                byte[] BOutPutpara = Encoding.Default.GetBytes(outputPara.ToString());

                //影响码2
                //string strOutPutpara = outputPara.ToString();

                ResultCode = Encoding.Default.GetString(BOutPutpara, 0, 2);
                if (ResultCode != "00")
                {
                    throw new Exception("返回代码：" + ResultCode + ",错误信息：" + Encoding.Default.GetString(BOutPutpara, 44, 40));
                }
                LogService.GlobalInfoMessage("返回码:" + ResultCode);
                //行号4
                BankCode = Encoding.Default.GetString(BOutPutpara, 2, 4);
                LogService.GlobalInfoMessage("行号:" + BankCode);
                //卡号20
                CardNo = Encoding.Default.GetString(BOutPutpara, 6, 20);
                LogService.GlobalInfoMessage("卡号:" + CardNo);

                //凭证号6
                VoucherNo = Encoding.Default.GetString(BOutPutpara, 26, 6);
                LogService.GlobalInfoMessage("凭证号:" + VoucherNo);
                //交易金额12
                decimal decMoney = 0;
                decimal.TryParse(Encoding.Default.GetString(BOutPutpara, 32, 10) + "." + Encoding.Default.GetString(BOutPutpara, 42, 2), out decMoney);
                TranMoney = decMoney;
                LogService.GlobalInfoMessage("交易金额:" + Encoding.Default.GetString(BOutPutpara, 32, 12));
                // 错误说明40:中文解释
                ResultMsg = Encoding.Default.GetString(BOutPutpara, 44, 40);
                LogService.GlobalInfoMessage("错误说明:" + ResultMsg);

                //商户号15
                MchId = Encoding.Default.GetString(BOutPutpara, 84, 15);
                LogService.GlobalInfoMessage("商户号:" + MchId);

                //终端号8
                TerminalNo = Encoding.Default.GetString(BOutPutpara, 99, 8);
                LogService.GlobalInfoMessage("终端号:" + TerminalNo);

                //批次号6
                BatchNo = Encoding.Default.GetString(BOutPutpara, 107, 6);
                LogService.GlobalInfoMessage("批次号:" + BatchNo);

                //交易日期4
                TranDate = Encoding.Default.GetString(BOutPutpara, 113, 4);
                LogService.GlobalInfoMessage("交易日期:" + TranDate);

                //交易时间6
                TranTime = Encoding.Default.GetString(BOutPutpara, 117, 6);
                LogService.GlobalInfoMessage("交易时间:" + TranTime);

                //交易参考号12
                RefNo = Encoding.Default.GetString(BOutPutpara, 123, 12);
                LogService.GlobalInfoMessage("交易参考号:" + RefNo);

                //授权号6
                LicenseNo = Encoding.Default.GetString(BOutPutpara, 135, 6);
                LogService.GlobalInfoMessage("授权号:" + LicenseNo);

                //清算日期4
                BalanceDate = Encoding.Default.GetString(BOutPutpara, 141, 4);
                LogService.GlobalInfoMessage("清算日期:" + BalanceDate);

                //LRC 3
                LRC = Encoding.Default.GetString(BOutPutpara, 145, 3);
                LogService.GlobalInfoMessage("LRC:" + LRC);

                DateTime OperatorTime = DateTime.Now;
                DateTime.TryParse(OperatorTime.Year + "-" + TranDate.Substring(0, 2) + "-" + TranDate.Substring(2) + " " +
                    TranTime.Substring(0, 2) + ":" + TranTime.Substring(2, 2) + ":" + TranTime.Substring(4), out OperatorTime);

                YHJYSJ = OperatorTime;
            }


            /// <summary>
            /// 返回码2:00 表示成功，其它表示失败
            /// </summary>
            internal string ResultCode { get; set; }

            /// <summary>
            /// 银行行号4 :发卡行代码_
            /// </summary>
            internal string BankCode { get; set; }

            /// <summary>
            /// 卡号20:（屏蔽部分，保留前6后4）
            /// </summary>
            internal string CardNo { get; set; }

            /// <summary>
            /// 凭证号6
            /// </summary>
            internal string VoucherNo { get; set; }

            /// <summary>
            /// 交易金额12
            /// </summary>
            internal decimal TranMoney { get; set; }

            /// <summary>
            /// 错误说明40:中文解释
            /// </summary>
            internal string ResultMsg { get; set; }

            /// <summary>
            /// 商户号15
            /// </summary>
            internal string MchId { get; set; }

            /// <summary>
            /// 终端号8
            /// </summary>
            internal string TerminalNo { get; set; }

            /// <summary>
            /// 批次号6
            /// </summary>
            internal string BatchNo { get; set; }

            /// <summary>
            /// 交易日期4
            /// </summary>
            internal string TranDate { get; set; }

            /// <summary>
            /// 交易时间6
            /// </summary>
            internal string TranTime { get; set; }

            /// <summary>
            /// 交易参考号12
            /// </summary>
            internal string RefNo { get; set; }

            /// <summary>
            /// 授权号6
            /// </summary>
            internal string LicenseNo { get; set; }


            /// <summary>
            /// 清算日期4
            /// </summary>
            internal string BalanceDate { get; set; }

            /// <summary>
            /// LRC 3
            /// </summary>
            internal string LRC { get; set; }

            /// <summary>
            /// 银行业务操作时间
            /// </summary>
            internal DateTime YHJYSJ { get; set; }

        }

        #endregion

    }
}
