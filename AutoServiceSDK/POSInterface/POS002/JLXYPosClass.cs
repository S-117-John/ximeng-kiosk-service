using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using BusinessFacade.BankHisExchange;
using BusinessFacade.His.Common;
using EntityData.BankHisExchange;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using SystemFramework.NewCommon;

namespace AutoServiceSDK.POSInterface.POS002
{
    /// <summary>
    ///  	北京嘉利兴业POS接口   自助机交行
    /// </summary>
    public class JLXYPosClass : POSBase
    {
        #region 初始化
        public JLXYPosClass()
            : base()
        {

        }
        #endregion

        #region API引用

        [DllImport("\\BJJLXY_POS\\ecrcomm.dll")]
        private static extern int ECRCOMM(string file,StringBuilder request, StringBuilder response);

        private int Pos_ECRCOMM(StringBuilder request, StringBuilder response)
        {
            string strpath = "BJJLXY_POS\\";
            //StringBuilder sbpath = new StringBuilder(strpath);
            LogService.GlobalInfoMessage("调用bankall方法传入的参数：" + request.ToString());

            int intResult = ECRCOMM(strpath, request, response);
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
                PosInOutputData thePosInOutputData = new PosInOutputData();

                DateTime dtCurrenTime = new CommonFacade().GetServerDateTime();
                thePosInOutputData.TransType = "01";

                 //获取交易金额，根据是12位的字符串，没有小数点到小数点后两位
                string strMoney = CheckMoney(htParams, "MONEY");
                thePosInOutputData.TransAmount = strMoney;
                thePosInOutputData.TransDate = dtCurrenTime.ToString("yyyyMMdd");
                thePosInOutputData.TransTime = dtCurrenTime.ToString("HHmmss");
                string Seqno = Check(htParams, "SEQNO");
                thePosInOutputData.HISLSH = Seqno;

                string strDiagnoseID = Check(htParams, "DIAGNOSEID");
                string CardID = Check(htParams, "CARDID");

                #endregion

                #region 调用POS接口
                StringBuilder sbInput = new StringBuilder(thePosInOutputData.GetInput());
                StringBuilder sbOutPut = new StringBuilder(256);
                PosInOutputData theOutputData = null;

                try
                {
                    //调用银行交易的第一次调用
                    int intResult = Pos_ECRCOMM(sbInput, sbOutPut);
                    if (intResult != 1)
                    {
                        if (intResult == -37 || intResult == -38 || intResult == -44)
                        {
                            thePosInOutputData.TransType = "03";
                            sbInput = new StringBuilder(thePosInOutputData.GetInput());

                            //如果第二次调用
                            intResult = Pos_ECRCOMM(sbInput, sbOutPut);

                            if (intResult != 1)
                            {
                                if (intResult == -37 || intResult == -38 || intResult == -44)
                                {
                                    //第三次调用
                                    intResult = Pos_ECRCOMM(sbInput, sbOutPut);
                                    if (intResult != 1)
                                    {
                                        throw new Exception("调用银行POS机接口失败，返回错误代码：" + intResult);
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            throw new Exception("调用银行POS机接口失败，返回错误代码：" + intResult);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用银行交易方法ECRCOMM失败：" + ex.Message);
                    throw ex;
                }

                try
                {
                    theOutputData = new PosInOutputData(sbOutPut);
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("调用银行交易方法成功，解析银行返回字符串失败：" + ex.Message);
                    throw ex;
                }
                #endregion

                #region 保存POS结算结果

                try
                {
                    
                    TBankhisexchangeTransData data = new TBankhisexchangeTransData();

                    //银行行号+批次号+授权号
                    data.Remark = theOutputData.CardType + "|" + theOutputData.BatchNo + "|" + theOutputData.ApprovalCode;

                    //银行卡号
                    data.ITEM1 = theOutputData.CardNumber;

                    //凭证号
                    data.Bankseqno = theOutputData.ReceiptNo;                

                    //金额
                    data.Trfamt = Check(htParams, "MONEY");

                    //商户号
                    data.Buscd = theOutputData.MerchantID;

                    //终端号
                    data.TerminalNo = theOutputData.TerminalID;

                    //批次号
                    data.ITEM2 = theOutputData.BatchNo;

                    //交易日期
                    data.Operatetime = theOutputData.YHJYSJ;
                    data.Operatorid = SysOperatorInfo.OperatorID;

                    //交易参考号
                    data.Ohisseqno = theOutputData.RefNo;

                    data.Usetype = "充值";
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
                    htParams.Add("CARDNO", theOutputData.CardNumber);
                    htParams.Add("BANKSEQNO", theOutputData.RefNo + "|" + theOutputData.ReceiptNo);
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("保存POS直联记录表T_BANKHISEXCHANGE_TRANS失败：" + ex.Message);
                    DeleteBankTran(thePosInOutputData,theOutputData);
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


                    PosInOutputData thePosInOutputData = new PosInOutputData();

                    DateTime dtCurrenTime = new CommonFacade().GetServerDateTime();
                    thePosInOutputData.TransType = "02";

                    //获取交易金额，根据是12位的字符串，没有小数点到小数点后两位

                    decimal decMoney = 0;
                    decimal.TryParse(list[0].Trfamt, out decMoney);
                 
                    string strMoney = GetMoneyString(list[0].Trfamt);
                    thePosInOutputData.TransAmount = strMoney;
                    thePosInOutputData.TransDate = dtCurrenTime.ToString("yyyyMMdd");
                    thePosInOutputData.TransTime = dtCurrenTime.ToString("HHmmss");
                    Seqno =list[0].Hisseqno + "R";
                    thePosInOutputData.HISLSH = Seqno;
                    thePosInOutputData.ReceiptNo = list[0].Bankseqno;
                    
                    #endregion

                    #region 调用POS接口
                    StringBuilder sbInput = new StringBuilder(thePosInOutputData.GetInput());
                    StringBuilder sbOutPut = new StringBuilder(150);
                    PosInOutputData theOutputData = null;

                    try
                    {
                        //调用银行交易
                        int intResult = Pos_ECRCOMM(sbInput, sbOutPut);
                        if (intResult !=1)
                        {
                            throw new Exception("调用银行POS机接口失败，返回错误代码："+intResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用银行交易方法ECRCOMM失败：" + ex.Message);
                        throw ex;
                    }

                    try
                    {
                        theOutputData = new PosInOutputData(sbOutPut);
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用银行交易方法成功，解析银行返回字符串失败：" + ex.Message);
                        throw ex;
                    }
                    #endregion

                    #region 保存POS结算结果

                    try
                    {

                        TBankhisexchangeTransData data = new TBankhisexchangeTransData();

                        //银行行号+批次号+授权号
                        data.Remark = theOutputData.CardType + "|" + theOutputData.BatchNo + "|" + theOutputData.ApprovalCode;

                        //银行卡号
                        data.ITEM1 = theOutputData.CardNumber;

                        //凭证号
                        data.Bankseqno = theOutputData.ReceiptNo;

                        //金额
                        data.Trfamt = (decMoney * (-1)).ToString();

                        //商户号
                        data.Buscd = theOutputData.MerchantID;

                        //终端号
                        data.TerminalNo = theOutputData.TerminalID;

                        //批次号
                        data.ITEM2 = theOutputData.BatchNo;

                        //交易日期
                        data.Operatetime = theOutputData.YHJYSJ;
                        data.Operatorid = SysOperatorInfo.OperatorID;

                        //交易参考号
                        data.Ohisseqno = theOutputData.RefNo;

                        data.Usetype = "充值冲正";
                        data.Hisid = CardID;
                        data.DIAGNOSEID = strDiagnoseID;
                        data.Hisseqno = Seqno;

                        data.DataSources = "自助";
                        data.BusinessType = "POS冲正";
                        data.Bankstate = "1";
                        data.Hisstate = "1";

                        facade.Insert(data);
                        LogService.GlobalInfoMessage("调用银行支付交易成功，写入数据库成功！");
                        //htParams.Add("CARDNO", theOutputData.CardNumber);
                        //htParams.Add("BANKSEQNO", theOutputData.RefNo + "|" + theOutputData.ReceiptNo);
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("保存POS直联记录表T_BANKHISEXCHANGE_TRANS失败：" + ex.Message);
                        //DeleteBankTran(thePosInOutputData, theOutputData);
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
                
            }
            #endregion

            #region 结算
            if (TranType == "6")
            {
               
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

        public string GetMoneyString(string strMoney1)
        {
            decimal decMoney = 0;
            decimal decOldMoney = 0;
            if (decimal.TryParse(strMoney1, out decOldMoney) == false)
            {
                throw new Exception("传入数字" + strMoney1 + "不正确,必须是数字!");
            }

            decMoney = DecimalRound.Round(decOldMoney, 2);
            if (decMoney != decOldMoney)
            {
                throw new Exception("传入POS接口的数字" +strMoney1 + "不正确,必须是2位小数!");
            }
            string strMoney = DecimalRound.Round(decMoney * 100, 0).ToString();
            return strMoney.PadLeft(12, '0');

        }


        private void DeleteBankTran(PosInOutputData thePosInputData,PosInOutputData theOutPutData)
        {
            LogService.GlobalInfoMessage("HIS处理失败，请用银行的撤消交易开始!");

            DateTime dtCurrenTime = new CommonFacade().GetServerDateTime();
            PosInOutputData theCancelInput = new PosInOutputData();
            theCancelInput.TransType = "02";
            theCancelInput.TransAmount = thePosInputData.TransAmount;
            theCancelInput.TransDate = dtCurrenTime.ToString("yyyyMMdd");
            theCancelInput.TransTime = dtCurrenTime.ToString("HHmmss");
            theCancelInput.HISLSH = thePosInputData.HISLSH.Trim() + "R";
            theCancelInput.ReceiptNo = theOutPutData.ReceiptNo;
            theCancelInput.RefNo = theOutPutData.RefNo;

            StringBuilder sbInput = new StringBuilder(theCancelInput.GetInput());           

            try
            {
                StringBuilder sbOutPut = new StringBuilder(150);
                //调用银行交易
                int intResult = Pos_ECRCOMM(sbInput, sbOutPut);
                if (intResult != 1)
                {
                    throw new Exception("调用银行POS机接口失败，返回错误代码：" + intResult);
                }

            }
            catch (Exception ex)
            {
                LogService.GlobalInfoMessage("调用银行交易方法ECRCOMM失败：" + ex.Message);
                throw ex;
            }
        }

        public class PosInOutputData
        {
            public PosInOutputData() 
            {
                ResultCode = string.Empty;
                TransType = string.Empty;
                ReceiptNo = string.Empty;
                CardNumber = string.Empty;
                ExpirationDate = string.Empty;
                TransAmount = string.Empty;

                TransDate = string.Empty;
                TransTime = string.Empty;
                ApprovalCode = string.Empty;
                CardType = string.Empty;
                TerminalID = string.Empty;
                MerchantID = string.Empty;
                RefNo = string.Empty;
                BatchNo = string.Empty;
                HISLSH = string.Empty;        
            }

            public PosInOutputData(StringBuilder outputPara)
            {
                byte[] BOutPutpara = Encoding.Default.GetBytes(outputPara.ToString());

                ResultCode = Encoding.Default.GetString(BOutPutpara, 71, 2);
                if (ResultCode != "00")
                {
                    throw new Exception("返回代码：" + ResultCode + ",错误信息：" + GetMsg(ResultCode));
                }               

                TransType = Encoding.Default.GetString(BOutPutpara, 0, 2);
                ReceiptNo = Encoding.Default.GetString(BOutPutpara, 2, 6);
                CardNumber = Encoding.Default.GetString(BOutPutpara, 8, 19);
                ExpirationDate = Encoding.Default.GetString(BOutPutpara, 27, 4);
                TransAmount = Encoding.Default.GetString(BOutPutpara, 31, 12);

                TransDate = Encoding.Default.GetString(BOutPutpara, 43, 8);
                TransTime = Encoding.Default.GetString(BOutPutpara, 51, 6);
                ApprovalCode = Encoding.Default.GetString(BOutPutpara, 57, 6);
                CardType = Encoding.Default.GetString(BOutPutpara, 63, 8);
               

                TerminalID = Encoding.Default.GetString(BOutPutpara, 73, 8);
                MerchantID = Encoding.Default.GetString(BOutPutpara, 81, 15);
                RefNo = Encoding.Default.GetString(BOutPutpara, 96, 12);
                BatchNo = Encoding.Default.GetString(BOutPutpara, 108, 6);
                HISLSH = Encoding.Default.GetString(BOutPutpara, 114, 30);

                DateTime OperatorTime = DateTime.Now;
                DateTime.TryParse(TransDate.Substring(0, 4) + "-" + TransDate.Substring(4, 2) + "-" + TransDate.Substring(6,2) + " " +
                    TransTime.Substring(0, 2) + ":" + TransTime.Substring(2, 2) + ":" + TransTime.Substring(4), out OperatorTime);

                YHJYSJ = OperatorTime;
            }

            /// <summary>
            /// //0交易类别2
            /// </summary>
            internal string TransType { get; set; }

            /// <summary>
            ///1流水号6
            /// </summary>
            internal string ReceiptNo { get; set; }

            /// <summary>
            ///2银行卡卡号(左靠右补空白)19
            /// </summary>
            internal string CardNumber { get; set; }

            /// <summary>
            ///3银行卡卡有效期4
            /// </summary>
            internal string ExpirationDate { get; set; }

            /// <summary>
            ///4交易金额单位为分，无小数点，长度12位，左补0   12
            /// </summary>
            internal string TransAmount { get; set; }

            /// <summary>
            /// 5交易日期(YYYYMMDD) 8
            /// </summary>
            internal string TransDate { get; set; }

            /// <summary>
            /// 6交易时间(hhmmss) 6
            /// </summary>
            internal string TransTime { get; set; }

            /// <summary>
            /// 7授权码(左靠右补空白) 6
            /// </summary>
            internal string ApprovalCode { get; set; }

            /// <summary>
            /// 8发卡行代码(左靠右补空白)8
            /// </summary>
            internal string CardType { get; set; }

            /// <summary>
            /// 9响应码2
            /// </summary>
            internal string ResultCode { get; set; }

            /// <summary>
            /// 10POS号8
            /// </summary>
            internal string TerminalID { get; set; }

            /// <summary>
            /// 11商户号15
            /// </summary>
            internal string MerchantID { get; set; }

            /// <summary>
            /// 12银行交易序号12
            /// </summary>
            internal string RefNo { get; set; }

            /// <summary>
            /// 13批次号6
            /// </summary>
            internal string BatchNo { get; set; }

            /// <summary>
            /// 14业务单据号（就诊号、一卡通号、住院号等）30
            /// </summary>
            internal string HISLSH { get; set; }

            /// <summary>
            /// 银行业务操作时间
            /// </summary>
            internal DateTime YHJYSJ { get; set; }

            internal string GetMsg(string Code)
            {
                switch (Code)
                {
                    case "01":
                        return "请持卡人与发卡银行联系";
                    case "03":
                        return "无效商户!";
                    case "04":
                        return "此卡被没收!";
                    case "05":                    
                        return "持卡人认证失败!";
                    case "10":
                        return "部分批准金额!";
                    case "11":
                        return "成功,VIP客户!";
                    case "12":
                        return "无效交易!";
                    case "13":
                        return "无效金额!";
                    case "14":
                        return "无效卡号!";
                    case "15":
                        return "此卡无对应发卡方!";                 
                    case "21":
                        return "该卡未初始化或睡眠卡!";
                    case "22":
                        return "操作有误,或超出交易允许天数!"; 
                    case "25":
                        return "没有原始交易,请联系发卡方!";

                    case "30":
                        return "请重试!";                  
                    case "34":
                        return "作弊卡,吞卡!";                  
                    case "38":
                        return "密码错误次数超限,请与发卡方联系!";
                    case "40":
                        return "发卡方不支持的交易类型!";

                    case "41":
                        return "挂失卡,请没收(POS)!";                   
                    case "43":
                        return "被窃卡,请没收!";
                    case "45":
                        return "请使用芯片!";
                    case "51":
                        return "可用余额不足!";                 
                    case "54":
                        return "该卡已过期，请联系发卡行!";
                    case "55":
                        return "密码错，请重试!";                  
                    case "57":
                        return "不允许此卡交易!";
                    case "58":
                        return "发卡方不许该卡在本终端进行此交易!";
                    case "59":
                        return "卡片校验错!";                  
                    case "61":
                        return "交易金额超限!";
                    case "62":
                        return "受限制的卡!";                 
                    case "64":
                        return "交易金额与原交易不匹配!";
                    case "65":
                        return "超出消费次数限制!";
                    case "68":
                        return "交易超时,请重试!";
                    case "75":
                        return "密码错误次数超限!";

                    case "77":
                        return "请操作员重新签到,再作交易!";
                    case "90":
                        return "系统日切,请稍后重试!";
                    case "91":
                        return "发卡方状态不正常,请稍后重试!";
                    case "92":
                        return "发卡方线路异常,请稍后重试!";
                    case "94":
                        return "拒绝,重复交易,请稍后重试!";

                    case "96":
                        return "拒绝,交换中心异常,请稍后重试!";
                    case "97":
                        return "终端未登记!";
                    case "98":
                        return "发卡方超时!";
                    case "99":
                        return "PIN格式错,请重新签到!";
                    case "A0":
                        return "MAC校验错,请重新签到!";

                    case "A1":
                        return "转账货币不一致!";
                    case "A2":
                        return "交易成功,请向资金到账行确认!";
                    case "A3":
                        return "资金到账行账号不正确!";
                    case "A4":
                        return "交易成功,请向资金到账行确认!";
                    case "A5":
                        return "交易成功,请向资金到账行确认!";
                    case "A6":
                        return "交易成功,请向资金到账行确认!";
                    case "A7":
                        return "安全处理失败!";

                    case "P1":
                        return "交易取消!";
                    case "P2":
                        return "无原始交易!";
                    case "P3":
                        return "无交易!";
                    case "P4":
                        return "联接失败!";
                    case "P5":
                        return "数据发送失败!";
                    case "P6":
                        return "数据接收失败!";
                    case "XX":
                        return "交易失败!";                  
                    default:
                        return Code;
                }

            }
            /// <summary>
            /// 获取输入参数
            /// </summary>
            /// <returns></returns>
            public string GetInput()
            {
                string input = "";

                //43
                input += TransType;
                input += ReceiptNo.PadLeft(6);
                input += CardNumber.PadRight(19);
                input += "".PadLeft(4);
                input += TransAmount.PadLeft(12, '0');

                //30
                input += TransDate;
                input += TransTime;
                input += "".PadLeft(6);
                input += "".PadLeft(8);
                input += "".PadLeft(2);

                //71  
                input += "".PadLeft(8);
                input += "".PadLeft(15);
                input += "".PadLeft(12);
                input += "".PadLeft(6);
                input += HISLSH.PadLeft(30);
                return input;
            }
        }


        private string GetErrmsg(string errcode)
        {
            string strMsg = string.Empty;
            switch (errcode)
            {
                case "-5":
                    strMsg = "设置数据缓冲块错误";
                    break;
                case "":

                case "-35":
                    strMsg = "初始化串口错误";
                    break;
                case "-36":
                    strMsg = "读取ecr.dat配置文件错";
                    break;
                case "-37":
                    strMsg = "得不到ACK响应并且超过发送次数";
                    break;
                case "-38":
                    strMsg = "读取pos数据错误";
                    break;
                case "-44":
                    strMsg = "接收pos数据超时";
                    break;
                default:
                    strMsg = errcode;
                    break;
            }
            return strMsg;
        }
        #endregion
    }
}
