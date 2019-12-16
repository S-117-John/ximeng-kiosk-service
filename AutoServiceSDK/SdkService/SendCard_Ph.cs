using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;

namespace AutoServiceSDK.SdkService
{
    public class SendCard_Ph : ISendCardInterFace
    {
        public bool WriteCard(string CardNo)
        {
            if (!MoveCard_Write())
            {
                return false;
            }
            //开始写卡
            Thread.Sleep(1000);//移动卡后沉睡1秒

            return writeCard(CardNo);

        }
        private bool MoveCard_Write()
        {
            //将卡移动到射频位
            string strinput = "<invoke name=\"CARDSENDERMOVECARDTORF\"><arguments></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法输入参数：" + strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用发卡机_移卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            //if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            //{
            //    return false;
            //}
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

        public static bool writeCard(string id)
        {

            try
            {
                LogService.GlobalInfoMessage("CPU卡写卡前传入的卡号：" + id);

                //                // 卡片移到射频位
                //                StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                //                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                //                LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());


                string cardNo = StringToHexString(id, ASCIIEncoding.ASCII)+ "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

                LogService.GlobalInfoMessage("CPU卡写卡前输入的16进制卡号：" + cardNo);

                StringBuilder mIn = new StringBuilder("<invoke name=\"CARDSENDERWRITEJZK\"><arguments><string id=\"SENDDATA\">" + cardNo + "</string></arguments></invoke>", 1024);
                //StringBuilder mIn = new StringBuilder("<invoke name=\"CARDSENDEWRITESXSRFCARD\"><arguments>");
                //mIn.Append("<string id=\"CARDNO\">"+ id + "</string>");
                //mIn.Append("<string id=\"CARDTYPE\"></string>");
                //mIn.Append("<string id=\"CARDFIRSTDATE\"></string>");
                //mIn.Append("<string id=\"CARDVALID\"></string>");
                //mIn.Append("<string id=\"IDNUMBER\"></string>");
                //mIn.Append("<string id=\"CREDTYPE\"></string>");
                //mIn.Append("<string id=\"CREDNO\"></string>");
                //mIn.Append("<string id=\"NAME\"></string>");
                //mIn.Append("<string id=\"BIRTHDATE\"></string>");
                //mIn.Append("<string id=\"NATION\"></string>");
                //mIn.Append("<string id=\"SEX\"></string>");
                //mIn.Append("<string id=\"PHONE\"></string>");
                //mIn.Append("<string id=\"ADDRESS\"></string>");
                //mIn.Append("</arguments>");
                //mIn.Append("</invoke>");

                LogService.GlobalInfoMessage("CPU卡写卡传入的参数" + mIn);
                string strResult1 = XuHuiInterface_DLL.XmlTcp(mIn, 0);

                LogService.GlobalInfoMessage("CPU卡写卡：" + strResult1 + ",输出参数：" + mIn);

                if (mIn.ToString().Contains("DEVERROR"))
                {
                   
                    return false;
                }

              

              
                string mCardNO = cardNo;
                
                mCardNO = HexStringToString(cardNo, ASCIIEncoding.ASCII);
                string a = mCardNO.Replace("\0", "").Trim();
                LogService.GlobalInfoMessage("CPU卡写卡后的卡号："+ a);
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public static string getCardNo()
        {
            try
            {
                // 卡片移到射频位
                StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                //                StringBuilder mIn = new StringBuilder("<invoke name=\"READCARDREADSXSRFCARD\"><arguments></arguments></invoke>");//
                StringBuilder mIn = new StringBuilder("<invoke name=\"READJZKDATA\"><arguments></arguments></invoke>", 1024);
                

                string strResult1 = XuHuiInterface_DLL.XmlTcp(mIn, 0);

                if (mIn.ToString().Contains("DEVEEROR"))
                {
                    
                    return "";
                }

                

                XmlDocument mDoc = new XmlDocument();
                mDoc.LoadXml(mIn.ToString());
                XmlNode mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='RECEIVEDATA']");
                string mCardNO = mXmlNode.InnerText;
               
                mCardNO = HexStringToString(mCardNO, ASCIIEncoding.ASCII);
                string a = mCardNO.Replace("\0", "").Trim();
                

                return a;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
