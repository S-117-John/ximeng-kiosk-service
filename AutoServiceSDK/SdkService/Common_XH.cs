using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AutoServiceSDK.SDK;
using Skynet.LoggingService;
using BusinessFacade.His.Register;
using System.Data;
using EntityData.His.Register;

namespace AutoServiceSDK.SdkService
{
    public class Common_XH
    {
        #region 摄像头
        //连续拍照开始进钞口
        public void TakeCameraStart(string CardID, string pName, string macineno)
        {
            AutoserviceLimtconfigFacade autolr = new AutoserviceLimtconfigFacade();
            DataSet ds = autolr.GetBylimitConfig(macineno);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["LIMTCONFIGDETAIL"].ToString() == "自助机摄像头调用模式")
                {
                    string status = ds.Tables[0].Rows[i]["DEFAULTVALUE"].ToString();
                    StringBuilder sbinput = null;
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp连续拍照方法TakeCameraStart开始！");
                    if (status == "正常")
                    {
                        sbinput = new StringBuilder("<invoke name=\"TAKEPHOTOSTART\"><arguments>" +
                     "<string id=\"CARDNO\">" + CardID + "</string>" +
                 "<string id=\"NAME\">" + pName + "</string>" +
                 "<string id=\"USECAMERAINDEX\">0</string></arguments></invoke>");
                    }
                    else
                    {

                        sbinput = new StringBuilder("<invoke name=\"TAKEPHOTOSTART\"><arguments>" +
                        "<string id=\"CARDNO\">" + CardID + "</string>" +
                    "<string id=\"NAME\">" + pName + "</string>" +
                    "<string id=\"USECAMERAINDEX\">1</string></arguments></invoke>");
                    }


                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp连续拍照开始输入参数：" + sbinput.ToString());

                    string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp连续拍照开始方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    if (sbinput.ToString().IndexOf("SUCCESS") <= 0)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp连续拍照方法拍照设备故障！");
                    }

                }

            }
        }
        /// <summary>
        /// 顶部摄像头
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="pName"></param>
        public void TakeCamera(string CardID, string pName, string macineno)
        {
            AutoserviceLimtconfigFacade autolr = new AutoserviceLimtconfigFacade();
            DataSet ds = autolr.GetBylimitConfig(macineno);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["LIMTCONFIGDETAIL"].ToString() == "自助机摄像头调用模式")
                {
                    StringBuilder sbinput = null;
                    string status = ds.Tables[0].Rows[i]["DEFAULTVALUE"].ToString();
                    if (status == "正常")
                    {
                        sbinput = new StringBuilder("<invoke name=\"TAKEPHOTO\"><arguments>" +
                            "<string id=\"CARDNO\">" + CardID + "</string>" +
                        "<string id=\"NAME\">" + pName + "</string>" +
                        "<string id=\"USECAMERAINDEX\">1</string></arguments></invoke>");
                    }
                    else
                    {
                        sbinput = new StringBuilder("<invoke name=\"TAKEPHOTO\"><arguments>" +
                                "<string id=\"CARDNO\">" + CardID + "</string>" +
                            "<string id=\"NAME\">" + pName + "</string>" +
                            "<string id=\"USECAMERAINDEX\">0</string></arguments></invoke>");
                    }

                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp拍照方法输入参数：" + sbinput);
                    string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp拍照方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    if (sbinput.ToString().IndexOf("SUCCESS") < 0)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp拍照方法拍照设备故障！");
                    }


                }
            }



        }


        //连续拍照结束
        public void TakeCameraEnd()
        {
            StringBuilder sbinput = new StringBuilder("<invoke name=\"TAKEPHOTOEND\"><arguments><string id=\"USECAMERAINDEX\">0</string></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp连续拍照结束方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (sbinput.ToString().IndexOf("SUCCESS") < 0)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("拍照设备故障！");
            }

        }

        #endregion

        #region 门禁灯

        public void DoorLightOpen(LightTypeenum lightType, LightOpenTypeenum OpenType)
        {
            LogService.GlobalInfoMessage("调用DoorLightOpen_1");
            string FunName = "DOORLIGHTFLUSH";
            string Index = "-1";

            if (string.IsNullOrEmpty(getvalue("省医院门禁灯")))
            {
                switch (lightType)
                {
                    case LightTypeenum.读卡器:
                        Index = "3";
                        break;
                    case LightTypeenum.纸币器:
                        Index = "5";
                        break;
                    case LightTypeenum.发卡器:
                        Index = "4";
                        break;
                    case LightTypeenum.银联卡:
                        Index = "7";
                        break;
                    case LightTypeenum.条码扫描:
                        Index = "1";
                        break;
                    case LightTypeenum.凭条:
                        Index = "2";
                        break;
                    case LightTypeenum.报告打印机:
                        Index = "6";
                        break;
                    case LightTypeenum.报警器:
                        Index = "8";
                        break;
                }
            }
            else
            {
                LogService.GlobalInfoMessage("调用省医院门禁灯××××××××××××××××××××××××××××××××××");
                LogService.GlobalInfoMessage("传入lightType：" + lightType);
                switch (lightType)
                {
                    case LightTypeenum.凭条:
                        Index = "1";
                        break;
                    case LightTypeenum.读卡器:
                        Index = "2";
                        break;
                    case LightTypeenum.发卡器:
                        Index = "3";
                        break;
                    case LightTypeenum.出卡槽:
                        Index = "4";
                        break;
                    case LightTypeenum.纸币器:
                        Index = "5";
                        break;
                    case LightTypeenum.银联卡:
                        Index = "6";
                        break;
                    case LightTypeenum.病历本出口:
                        Index = "7";
                        break;
                    case LightTypeenum.条码扫描:
                        Index = "8";
                        break;
                    case LightTypeenum.身份证:
                        Index = "9";
                        break;
                    case LightTypeenum.化验单:
                        Index = "10";
                        break;
                    case LightTypeenum.报警器:
                        Index = "11";
                        break;
                }
            }


            LogService.GlobalInfoMessage("调用DoorLightOpen_2");
            if (OpenType == LightOpenTypeenum.打开)
            {
                FunName = "DOORLIGHTOPEN";
            }
            LogService.GlobalInfoMessage("调用DoorLightOpen_3");

            string strInput = "<invoke name=\"" + FunName + "\"><arguments><string id=\"INDEX\">" + Index + "</string></arguments></invoke>";
            LogService.GlobalInfoMessage("调用XmlTcp门禁灯打开方法输入参数" + strInput);

            StringBuilder sbinput = new StringBuilder(strInput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用XmlTcp门禁灯打开方法返回参数：" + sbinput.ToString());

            LogService.GlobalInfoMessage("调用DoorLightOpen_4");

        }


        public static string getvalue(string appkey)
        {
            string filename = Application.StartupPath + "\\system.config";
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
                                    if (el.Attributes["key"].Value == appkey)
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
            return "";
        }


        public void DoorLightClose(LightTypeenum lightType)
        {
            string FunName = "DOORLIGHTCLOSE";
            string Index = "-1";
            if (string.IsNullOrEmpty(getvalue("省医院门禁灯")))
            {
                switch (lightType)
                {
                    case LightTypeenum.读卡器:
                        Index = "3";
                        break;
                    case LightTypeenum.纸币器:
                        Index = "5";
                        break;
                    case LightTypeenum.发卡器:
                        Index = "4";
                        break;
                    case LightTypeenum.银联卡:
                        Index = "7";
                        break;
                    case LightTypeenum.条码扫描:
                        Index = "1";
                        break;
                    case LightTypeenum.凭条:
                        Index = "2";
                        break;
                    case LightTypeenum.报告打印机:
                        Index = "6";
                        break;
                    case LightTypeenum.报警器:
                        Index = "8";
                        break;
                }
            }
            else
            {
                switch (lightType)
                {
                    case LightTypeenum.凭条:
                        Index = "1";
                        break;
                    case LightTypeenum.读卡器:
                        Index = "2";
                        break;
                    case LightTypeenum.发卡器:
                        Index = "3";
                        break;
                    case LightTypeenum.出卡槽:
                        Index = "4";
                        break;
                    case LightTypeenum.纸币器:
                        Index = "5";
                        break;
                    case LightTypeenum.银联卡:
                        Index = "6";
                        break;
                    case LightTypeenum.病历本出口:
                        Index = "7";
                        break;
                    case LightTypeenum.条码扫描:
                        Index = "8";
                        break;
                    case LightTypeenum.身份证:
                        Index = "9";
                        break;
                    case LightTypeenum.化验单:
                        Index = "10";
                        break;
                    case LightTypeenum.报警器:
                        Index = "11";
                        break;
                }
            }


            StringBuilder sbinput = new StringBuilder("<invoke name=\"" + FunName + "\"><arguments><string id=\"INDEX\">" + Index + "</string></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp门禁灯关闭方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

        }

        #endregion

        #region 门禁锁
        /// <summary>
        /// 门禁锁打开关闭方法
        /// </summary>
        /// <param name="intindex">1：报告单，2：钞箱，3：发卡机，凭条</param>
        /// <param name="OpenType">0：关闭，1：打开</param>
        public void Doorlock(int intindex, int OpenType)
        {
            string funName = "LOCKOPEN";
            if (OpenType == 0)
            {
                funName = "LOCKCLOSE";
            }
            string strinput = "<invoke name=\"" + funName + "\"><arguments><string id=\"INDEX\">" + intindex + "</string></arguments></invoke>";
            StringBuilder sbinput = new StringBuilder(strinput);
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp门禁开关" + funName + "方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
        }

        #endregion

        #region POS推秆

        /// <summary>
        /// POS推秆程序
        /// </summary>
        /// <param name="OpenType">0:打开，1关闭</param>
        public void PosDoor(int OpenType)
        {
            if (OpenType == 0)
            {
                StringBuilder sbinput = new StringBuilder("<invoke name=\"DOORTGOPEN\"><arguments></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp打开推杆方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

            }
            else
            {
                StringBuilder sbinput = new StringBuilder("<invoke name=\"DOORTGCLOSE\"><arguments></arguments></invoke>");
                string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp关闭推杆方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            }
        }

        #endregion
    }

    public enum LightTypeenum
    {
        纸币器,
        读卡器,
        发卡器,
        银联卡,
        条码扫描,
        凭条,
        报告打印机,
        报警器,
        病历本出口,
        身份证,
        化验单,
        出卡槽
    }

    public enum LightOpenTypeenum
    {
        打开,
        闪烁
    }

}