using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AutoServiceSDK.SdkData;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;

namespace AutoServiceManage.Common
{
    public class CardCpu
    {
        public static string getCardNo()
        {
            try
            {
                // 卡片移到射频位
                StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

//                StringBuilder mIn = new StringBuilder("<invoke name=\"READCARDREADSXSRFCARD\"><arguments></arguments></invoke>");//
                StringBuilder mIn = new StringBuilder("<invoke name=\"READCARDREADJZK\"><arguments></arguments></invoke>", 1024);
                Log.Info("CardCpu", "读cpu卡传入参数", mIn.ToString());
                LogService.GlobalInfoMessage("进入读陕西省就诊卡");
                LogService.GlobalInfoMessage("读cpu卡传入参数：" + mIn);
                string strResult1 = XuHuiInterface_DLL.XmlTcp(mIn, 0);
                LogService.GlobalInfoMessage("读cpu卡输出参数：" + mIn);
                if (mIn.ToString().Contains("DEVEEROR"))
                {
                    SkyComm.ShowMessageInfo("读卡失败：DEVEEROR！");

                    return "";
                }

                Log.Info("CardCpu", "读cpu卡返回", "调用读卡方法返回：" + strResult1 + ",输出参数：" + mIn);

                XmlDocument mDoc = new XmlDocument();
                mDoc.LoadXml(mIn.ToString());
                XmlNode mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='RECEIVEDATA']");
                string mCardNO = mXmlNode.InnerText;
                Log.Info("CardCpu", "原卡号", mCardNO);
                mCardNO = SkyComm.HexStringToString(mCardNO, ASCIIEncoding.ASCII);
                string a = mCardNO.Replace("\0", "").Trim();
                Log.Info("CardCpu", "读cpu卡转换后mCardNO", a);

                return a;
            }
            catch (Exception e)
            {

                return "";
            }
        }


        public static bool writeCard(string id)
        {

            try
            {
                // 卡片移到射频位
                StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());


                string cardNo = Encode(id);

                StringBuilder mIn = new StringBuilder("<invoke name=\"WRITEAJZKDATA\"><arguments><string id=\" SENDDATA\">"+ cardNo + "</string></arguments></invoke>", 1024);
                Log.Info("CardCpu", "写cpu卡传入参数", mIn.ToString());

                string strResult1 = XuHuiInterface_DLL.XmlTcp(mIn, 0);

                if (mIn.ToString().Contains("DEVEEROR"))
                {
                    SkyComm.ShowMessageInfo("写卡失败：DEVEEROR！");
                    return false;
                }

                Log.Info("CardCpu", "写cpu卡返回", "调用写卡方法返回：" + strResult1 + ",输出参数：" + mIn);

                XmlDocument mDoc = new XmlDocument();
                mDoc.LoadXml(mIn.ToString());
                XmlNode mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='RECEIVEDATA']");
                string mCardNO = mXmlNode.InnerText;
                Log.Info("CardCpu", "写入卡号", id);
                mCardNO = SkyComm.HexStringToString(mCardNO, ASCIIEncoding.ASCII);
                string a = mCardNO.Replace("\0", "").Trim();
                Log.Info("CardCpu", "写cpu卡转换后mCardNO", mCardNO);
                if (mCardNO.Equals(id))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public static string Encode(string strEncode)
        {
            string strReturn = "";//  存储转换后的编码
            foreach (short shortx in strEncode.ToCharArray())
            {
                strReturn += shortx.ToString("X4");
            }
            return strReturn;
        }

        public static IDCardInfo readIdCard()
        {
            try
            {
                IDCardInfo mCardInfo = new IDCardInfo();
                StringBuilder mIn = new StringBuilder("<invoke name=\"SHENFENZHENG\"><arguments></arguments></invoke>", 1024);
                Log.Info("省医院读取身份证", "读取身份证传入参数", mIn.ToString());
                string strResult1 = XuHuiInterface_DLL.XmlTcp(mIn, 0);

                if (mIn.ToString().Contains("DEVEEROR"))
                {
                    SkyComm.ShowMessageInfo("写卡失败：DEVEEROR！");
                    return null;
                }

                Log.Info("省医院读取身份证", "读取身份返回结果", strResult1);

                if (string.IsNullOrEmpty(mIn.ToString ()))
                    return null;
                if (!mIn.ToString().Contains("IDNAME"))
                    return null;

                Log.Info("省医院读取身份证", "读取身份返回参数", mIn.ToString());

                XmlDocument mDoc = new XmlDocument();
                mDoc.LoadXml(mIn.ToString());
                XmlNode mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='IDNAME']");
                mCardInfo.Name = mXmlNode.InnerText;
                Log.Info("省医院读取身份证", "姓名", mCardInfo.Name);
                mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='SEX']");
                mCardInfo.Sex = mXmlNode.InnerText;
                Log.Info("省医院读取身份证", "性别", mCardInfo.Name);
                mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='BORN']");
                try
                {
                    mCardInfo.Birthday = GetAge(mXmlNode.InnerText);
                }
                catch (Exception e)
                {
                    mCardInfo.Birthday = mXmlNode.InnerText;
                }
                Log.Info("省医院读取身份证", "出生", mCardInfo.Birthday);
                mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='ADDRESS']");
                mCardInfo.Address = mXmlNode.InnerText;
                Log.Info("省医院读取身份证", "地址", mCardInfo.Address);
                mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='IDCARDNO']");
                mCardInfo.Number = mXmlNode.InnerText;
                Log.Info("省医院读取身份证", "身份证号", mCardInfo.Number);

                if (mIn.ToString().Contains("NATION"))
                {
                    mXmlNode = mDoc.SelectSingleNode("/return/arguments/string[@id='NATION']");
                    mCardInfo.People = mXmlNode.InnerText;
                }

                mCardInfo.ImagePath = Application.StartupPath + "\\_Temp.bmp";// SkyComm.getvalue("省医院身份证照片路径");

                return mCardInfo;
            }
            catch (Exception ex)
            {
                Log.Info("省医院读取身份证错误", "读取身份证错误详情", ex.ToString());
                return null;
            }
        }

        public static string GetAge(string birthday)
        {

            string year = birthday.Substring(0, 4);
            string month = birthday.Substring(4, 2);
            string day = birthday.Substring(6, 2);
            return year + "-" + month + "-" + day;


        }
    }
}
