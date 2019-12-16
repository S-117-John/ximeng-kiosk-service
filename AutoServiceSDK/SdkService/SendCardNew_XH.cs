using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;


namespace AutoServiceSDK.SdkService
{
    public class SendCardNew_XH : ISendCardInterFace
    {
        public bool WriteCard(string CardNo)
        {
            if (!MoveCard_Write())
            {
                return false;
            }
            //开始写卡
            Thread.Sleep(1000);//移动卡后沉睡1秒
            string cardNo1 = string.Empty;
            string cardNo2 = string.Empty;
            //LogService.GlobalInfoMessage("传入卡号：" +CardNo);
            if (CardNo.Length > 16)
            {
                cardNo1 = StringToHexString(CardNo.Substring(0, 16), ASCIIEncoding.ASCII);
                cardNo2 = StringToHexString(CardNo.Substring(16), ASCIIEncoding.ASCII);
            }
            else
            {
                cardNo1 = StringToHexString(CardNo, ASCIIEncoding.ASCII);
            }
            //LogService.GlobalInfoMessage("cardno1：" + cardNo1);


            string strinput = "<invoke name=\"CARDSENDERWRITERFCARD\"><arguments><string id=\"CARDNO\">" + cardNo1 + "</string><string id=\"BLOCKADR\">8</string><string id=\"NADR\">2</string><string id=\"PASSWORD\">FFFFFFFFFFFF</string></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            LogService.GlobalInfoMessage("调用发卡机_写卡方输入参数(第一次）：" + strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_写卡方法返回（第一次）：" + strResult + ",输出参数：" + sbinput.ToString());
            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                return false;
            }
            if (CardNo.Length > 16)
            {
                string strinput2 = "<invoke name=\"CARDSENDERWRITERFCARD\"><arguments><string id=\"CARDNO\">" + cardNo2 + "</string><string id=\"BLOCKADR\">8</string><string id=\"NADR\">2</string><string id=\"PASSWORD\">FFFFFFFFFFFF</string></arguments></invoke>";
                StringBuilder sbinput2 = new StringBuilder(strinput2);
                LogService.GlobalInfoMessage("调用发卡机_写卡方输入参数(第二次）：" + strinput2);
                string strResult2 = XuHuiInterface_DLL.XmlTcp(sbinput2, 0);
                LogService.GlobalInfoMessage("调用发卡机_写卡方法返回（第二次）：" + strResult2 + ",输出参数：" + sbinput2.ToString());
                if (sbinput.ToString().IndexOf("SUCCESS") < 0)
                {
                    return false;
                }
            }
            string ReturnCardNo = string.Empty;

            strinput = "<invoke name=\"CARDSENDERREADRFCARD\"><arguments><string id=\"BLOCKADR\">8</string><string id=\"NADR\">2</string><string id=\"PASSWORD\">FFFFFFFFFFFF</string></arguments></invoke>";
            sbinput = new StringBuilder(strinput);
            strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法返回(第一次)：" + strResult + ",输出参数：" + sbinput.ToString());
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());

            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
            if (xnd == null)
                return false;

            ReturnCardNo = HexStringToString(xnd.InnerText, ASCIIEncoding.ASCII).ToString().Trim().Substring(0, 15);
            if (CardNo.Length > 16)
            {
                string strinput2 = "<invoke name=\"CARDSENDERREADRFCARD\"><arguments><string id=\"BLOCKADR\">8</string><string id=\"NADR\">2</string><string id=\"PASSWORD\">FFFFFFFFFFFF</string></arguments></invoke>";
                StringBuilder sbinput2 = new StringBuilder(strinput2);
                string strResult2 = XuHuiInterface_DLL.XmlTcp(sbinput2, 0);
                LogService.GlobalInfoMessage("调用发卡机_读卡方法返回(第二次)：" + strResult2 + ",输出参数：" + sbinput2.ToString());
                System.Xml.XmlDocument doc2 = new System.Xml.XmlDocument();
                doc2.LoadXml(sbinput2.ToString());
                System.Xml.XmlNode xnd2 = doc2.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
                if (xnd2 == null)
                    return false;
                ReturnCardNo = ReturnCardNo + HexStringToString(xnd2.InnerText, ASCIIEncoding.ASCII).ToString().Trim().Substring(0, CardNo.Length - 16);
                ReturnCardNo = ReturnCardNo.Replace(" ", "");
            }

            if (!string.IsNullOrEmpty(ReturnCardNo))
            {
                LogService.GlobalInfoMessage("调用发卡机_读卡方法写入卡号：" + ReturnCardNo + ",实际卡号：" + CardNo);
                if (ReturnCardNo == CardNo)
                {
                    return true;
                }
            }
            return false;
        }
        private bool MoveCard_Write()
        {
            //将卡移动到射频位
            string strinput = "<invoke name=\"CARDSENDERMOVECARDTORF\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法输入参数：" + strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                return false;
            }
            return true;
        }
        private bool MoveCard_Read()
        {
            //将卡移动到射频位
            string strinput = "<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法输入参数：" + strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                return false;
            }
            return true;
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
            if (!MoveCard_Read())
            {
                return string.Empty;
            }
            string strinput = "<invoke name=\"CARDSENDERREADRFCARD\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
            if (xnd != null)
            {
                if (!string.IsNullOrEmpty(xnd.InnerText) && xnd.InnerText.IndexOf('0') >= 0)
                {
                    return xnd.InnerText;
                }
            }
            return string.Empty;
        }

        public string CheckCard()
        {
            //            string strinput = "<invoke name=\"CARDSENDERGETSTATUS\"><arguments></arguments></invoke>";
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

        /// <summary>
        /// 新检测有卡
        /// </summary>
        /// <returns></returns>
        public bool hasCard()
        {
            string strinput = "<invoke name=\"CARDSENDERGETSTATUS\"><arguments></arguments></invoke>";
            //            string strinput = "<invoke name=\"CARDSENDERREADCARD\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_读卡方法,为了检验是否有卡返回：" + strResult + ",输出参数：" + sbinput.ToString());

            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                return false;
            }
            return true;
        }

        /// <16进制转字符>
        /// Hex to Text
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }
        /// <字符转16进制>
        /// Text to Hex
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }
    }
}
