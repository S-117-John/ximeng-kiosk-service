using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;
using EntityData.His.Register;
using BusinessFacade.His.Register;

namespace AutoServiceSDK.SdkService
{
    /// <summary>
    /// 旭辉自助机钞箱接口
    /// </summary>
    public class CashCodeMoney_XH: IMoneyService
    {      
        #region IMoneyService 成员

        /// <summary>
        /// 设备初始化
        /// </summary>
        public bool OpenPort(string CardNo)
        {
            try
            {
                //打开纸币器
                StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTOROPENPORT\"><arguments><string id=\"ID\">" + CardNo + "</string></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp打开纸币器方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                if (sbinput.ToString().IndexOf("SUCCESS") <= 0)
                {
                    return false;
                }

                //设置纸币器进币金额
                sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORSETCASHINMONEY\"><arguments><string id=\"AVAILABLE\">5,10,20,50,100</string></arguments></invoke>");
                strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp设置进币金额方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                if (sbinput.ToString().IndexOf("SUCCESS") <= 0)
                {
                    //关闭纸币器
                    sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORCLOSEPORT\"><arguments></arguments></invoke>");
                    strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp关闭纸币器方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    return false;
                }

                //允许进钞
                sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORALLOWCASHIN\"><arguments></arguments></invoke>");
                strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp允许纸币入币方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                if (sbinput.ToString().IndexOf("SUCCESS") <= 0)
                {
                    ClosePort();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        /// <summary>
        /// 发送接收纸币指令，需要连续不断发送此指令
        /// </summary>
        /// <returns>读取的纸币金额</returns>
        public int GetInMoney(string Machineno, string OperatorID)
        {                                   
            int moneycount = 0;

            StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORSTACKMONEYDETAIL\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp获取存钞明细方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='STACKMONEY']");
            if (xnd != null)
            {
                if (!string.IsNullOrEmpty(xnd.InnerText))
                {
                    //防止快速存钞后，方法返回结果出现多张信息，以|分割后，再循环操作
                    string[] mSaveResults = xnd.InnerText.Split('|');

                    LogService.GlobalInfoMessage("存钞函数返回结果" + xnd.InnerText);

                    foreach (var variable in mSaveResults)
                    {
                        //string[] arrstr = xnd.InnerText.Split(',');
                        string[] arrstr = variable.Split(',');
                        moneycount = Convert.ToInt32(arrstr[0]);
                        if (arrstr.Length >= 2)
                        {
                            LogService.GlobalInfoMessage("卡号：" + arrstr[1] + "存入明细金额：" + moneycount + ",存入时间：" + arrstr[2]);

                            AutoInMoneyRecordData theAutoInMoneyRecordData = new AutoInMoneyRecordData();
                            theAutoInMoneyRecordData.Cardno = arrstr[1];
                            theAutoInMoneyRecordData.Inmoney = moneycount;
                            theAutoInMoneyRecordData.Machineno = Machineno;
                            theAutoInMoneyRecordData.Operatorcodeno = OperatorID;
                            theAutoInMoneyRecordData.Operatortime = Convert.ToDateTime(arrstr[2]);
                            AutoInMoneyRecordFacade theAutoInMoneyRecordFacade = new AutoInMoneyRecordFacade();
                            theAutoInMoneyRecordFacade.Insert(theAutoInMoneyRecordData);

                        }
                        else
                        {
                            LogService.GlobalInfoMessage("存入金额：" + moneycount);
                        }
                    }
                }
            }
            return moneycount;
        }

        /// <summary>
        /// 停止接收纸币
        /// </summary>
        public bool ClosePort()
        {
            //StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORNOTALLOWCASHIN\"><arguments></arguments></invoke>");
            //string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            //Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp禁止纸币入币方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

            StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORCLOSEPORT\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp关闭纸币器方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            
            return true;            
        }

        public bool NotAllowCashin()
        {
            StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORNOTALLOWCASHIN\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp禁止纸币入币方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            return true;
        }

        public bool AllowCashin()
        {
            StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORALLOWCASHIN\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp允许纸币入币方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            return true;
        }


        public int GetStatus()
        {
            StringBuilder sbinput = new StringBuilder("<invoke name=\"BILLACCEPTORGETSTATUS\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp获取纸币状态方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            string output = sbinput.ToString();

            //output.ToUpper().IndexOf("ACCEPTING") >= 0 || 
            if (output.ToUpper().IndexOf("ESCROW") >= 0 || output.ToUpper().IndexOf("STACKING") >= 0)
            {
                return 1;
            }
            return 0;
        }

        #endregion
    }

    public class CashMoneyTest : IMoneyService
    {
        #region IMoneyService 成员

        /// <summary>
        /// 设备初始化
        /// </summary>
        public bool OpenPort(string CardNo)
        {
            try
            {                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 发送接收纸币指令，需要连续不断发送此指令
        /// </summary>
        /// <returns>读取的纸币金额</returns>
        public int GetInMoney(string Machineno, string OperatorID)
        {
            int intSecnd = DateTime.Now.Second;
            if (DateTime.Now.Second % 5 == 0)
            {
                return intSecnd;
            }
            else
            {
                return 0;
            }
                      
            
        }
        public bool NotAllowCashin()
        {
            return true;
        }

        public bool AllowCashin()
        {
            return true;
        }
        /// <summary>
        /// 停止接收纸币
        /// </summary>
        public bool ClosePort()
        {
            
            return true;
        }

        public int GetStatus()
        {
            return 0;
        }
        #endregion
    }
}
