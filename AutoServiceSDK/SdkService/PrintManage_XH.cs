using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;

namespace AutoServiceSDK.SdkService
{
    public class PrintManage_XH : IPrintManage
    {
        // <summary>
        /// 检查打印机状态是否正常
        /// </summary>
        /// <returns>可状态返回空，不能使用返回原因</returns>
        public string CheckPrintStatus()
        {
            try
            {
                string strinput = "<invoke name=\"RECEIPTSTATUS\"><arguments></arguments></invoke>";
                StringBuilder sbinput = new StringBuilder(strinput);
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                LogService.GlobalInfoMessage("调用检测打印机方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(sbinput.ToString());
                System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='ERROR']");
                if (xnd != null)
                {
                    string ErroerText = xnd.InnerText;
                    if (ErroerText == "NOPAPER")
                    {
                        return "打印机缺纸，请在其他自助机或窗口办理业务！";
                    }
                    else if (ErroerText == "DEVERROR")
                    {
                        return "打印机硬件故障，请在其他自助机或窗口办理业务！";
                    }
                    return "";
                }
                return "";
            }
            catch (Exception ex)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo(ex.Message);
            }
            return "";
        }
    }
}
