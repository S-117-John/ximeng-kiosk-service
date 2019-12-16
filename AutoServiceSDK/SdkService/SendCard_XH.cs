using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;

namespace AutoServiceSDK.SdkService
{
    public class SendCard_XH : ISendCardInterFace
    {
        public bool WriteCard(string CardNo)
        {
            //开始写卡
            string strinput = "<invoke name=\"CARDSENDERWRITECARD\"><arguments><string id=\"CARDNO\">" + CardNo + "</string></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            LogService.GlobalInfoMessage("调用发卡机_写卡方输入参数：" + strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_写卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                return false;
            }

            strinput = "<invoke name=\"CARDSENDERREADCARD\"><arguments></arguments></invoke>";
            sbinput = new StringBuilder(strinput);
            strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
            if (xnd != null)
            {
                LogService.GlobalInfoMessage("调用发卡机_读卡方法写入卡号：" + xnd.InnerText + ",实际卡号：" + CardNo);
                if (!string.IsNullOrEmpty(xnd.InnerText) && xnd.InnerText == CardNo)
                {
                    return true;
                }
            }

            return false;
        }

        public void OutputCard()
        {
            try
            {
                string strinput = "<invoke name=\"CARDSENDEROUTCARD\"><arguments></arguments></invoke>";
                StringBuilder sbinput = new StringBuilder(strinput);
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用发卡机_吐卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            }
            catch (Exception ex)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo(ex.Message);
            }
        }

        public void ReturnCard()
        {
            try
            {
                string strinput = "<invoke name=\"CARDSENDERRECYCLE\"><arguments></arguments></invoke>";
                StringBuilder sbinput = new StringBuilder(strinput);
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用发卡机_回收卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            }
            catch (Exception ex)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo(ex.Message);
            }
        }

        public string ReadCard()
        {
            string strinput = "<invoke name=\"CARDSENDERREADCARD\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
            if (xnd != null)
            {
                if (!string.IsNullOrEmpty(xnd.InnerText) && xnd.InnerText.IndexOf('0')>=0)
                {
                    return xnd.InnerText;
                }
            }
            return string.Empty;
        }

        public string CheckCard()
        {
            string strinput = "<invoke name=\"CARDSENDERREADCARD\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法,为了检验是否有卡返回：" + strResult + ",输出参数：" + sbinput.ToString());
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
            if (xnd != null)
            {
                if (!string.IsNullOrEmpty(xnd.InnerText) && (xnd.InnerText.IndexOf("4,98") >= 0 || xnd.InnerText.IndexOf("FEEDER EMPTY") >= 0))
                {
                    return "1";
                }
            }
            return "0";
        }
    }

}
