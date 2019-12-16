using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Skynet.Framework.Common;
using System.Data;
using BusinessFacade.His.Common;
using System.Collections;
using System.Net;
using BusinessFacade.His.Register;
using EntityData.His.Register;
using CardInterface;
using EntityData.His.CardClubManager;
using AutoServiceManage.Common;
using BusinessFacade.His.CardClubManager;
using AutoServiceManage.InCard;
using Skynet.LoggingService;
using System.Threading;

namespace AutoServiceManage
{
    public class SkyComm
    {
        #region 初始化，系统配置
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

        public static bool Init()
        {
            BusinessFacade.His.Common.CommonFacade cf = new BusinessFacade.His.Common.CommonFacade();
            cf.RemotingLogin();
            int zone = cf.GetServerDateZone();
            LogService.GlobalDebugMessage("时区:" + zone);
            Skynet.Framework.Common.TimeSync.SyncTime(cf.GetServerDateTime(), zone);

            UpdateSystemConfig();

            //return true;
            //初始化硬件
            switch (AutoHostConfig.ReadCardType)
            {
                case "XUHUI":
                    //Thread.Sleep(200);
                    StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDALLOWCARDIN\"><arguments></arguments></invoke>");
                    string strResult = AutoServiceSDK.SDK.XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp允许进卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    break;
                default:
                    break;
            }

            //卡类型
            CardTypesFacade cardTypesFacade = new CardTypesFacade();
            dsCardType = cardTypesFacade.FindByTypeName(SkyComm.getvalue("发卡卡类型"));
            if (dsCardType == null || dsCardType.Tables[0].Rows.Count == 0)
            {
                SkynetMessage.MsgInfo("发卡机的卡类型维护不正确，请与管理员联系!");
            }

            //费用类别
            ChargeKindFacade cktype = new ChargeKindFacade();
            FeeType = SkyComm.getvalue("发卡费用类别");
            DataSet dscktype = cktype.FindChargeKindByName(SkyComm.getvalue("发卡费用类别"));
            if (dscktype.Tables[0].Rows.Count < 0)
            {
                SkynetMessage.MsgInfo("发卡机的卡类型维护不正确，请与管理员联系!");
            }
            else
            {
                FeeTypeID = dscktype.Tables[0].Rows[0]["CHARGEKINDID"].ToString();
            }

            //获取银行卡预存，现金预存的配置
            AddMoneyCashMode = SkyComm.getvalue("现金预存充值方式");
            AddMoneyPosMode = SkyComm.getvalue("银行预存充值方式");
            return login();
        }

        private static bool login()
        {
            string HostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostByName(HostName);
            string IpAddress = hostEntry.AddressList[0].ToString();

            //通过计算机名和IP地址查询自助机信息表的相关配置
            AutoServiceMachineInfoFacade AutoServiceFac = new AutoServiceMachineInfoFacade();
            AutoServiceMachineInfoData AutoServiceData = AutoServiceFac.GetByIPAddressHostName(IpAddress, HostName);
            if (AutoServiceData == null)
            {
                SkynetMessage.MsgInfo("该自助机IP地址和计算名没有登记，请与管理员联系！");
                return false;
            }
            if (AutoServiceData.Stopflag == 1)
            {
                SkynetMessage.MsgInfo("该自助机已被停用，不能正常使用");
                return false;
            }
            AutoHostConfig.SendCardType = AutoServiceData.Sendcardtype;
            AutoHostConfig.SendCardPort = AutoServiceData.Sendcardport;
            AutoHostConfig.ReadCardType = AutoServiceData.Readcardtype;

            LogService.GlobalInfoMessage("读卡机类型："+AutoServiceData.Readcardtype);

            AutoHostConfig.ReadCardPort = AutoServiceData.Readcardport;

            AutoHostConfig.CashBoxType = AutoServiceData.Cashboxtype;
            AutoHostConfig.CashBoxPort = AutoServiceData.Cashboxport;
            AutoHostConfig.IDCardType = AutoServiceData.Idcardtype;
            AutoHostConfig.IDCardPort = AutoServiceData.Idcardport;
            AutoHostConfig.MachineType = AutoServiceData.Machinetype;
            AutoHostConfig.Machineno = AutoServiceData.Machineno;
            AutoHostConfig.PosInterfaceType = AutoServiceData.PosInterfaceType;
            AutoHostConfig.PosNo = AutoServiceData.PosNo;
            AutoHostConfig.BankName = AutoServiceData.BankName;
            AutoHostConfig.ClosePassword = AutoServiceData.Closepassword;
            AutoHostConfig.OpenDoorPassword = AutoServiceData.Opendoorpassword;


            //登陆HIS操作系统
            OperatorFacade operatorFacade = new OperatorFacade();
            DataSet opds = operatorFacade.GetLoginOperatorInfo(AutoServiceData.Operatorcodeno);
            if (opds.Tables[0].Rows.Count == 0)
            {
                SkynetMessage.MsgInfo("自助机对应的用户在系统中找不到！");
                return false;
            }

            //根据查询的结果给AutoHostConfig中变量赋值，保存操作员的相关信息，以及设备的相关信息
            DataRow OperatorRow = opds.Tables[0].Rows[0];
            SysOperatorInfo.OperatorID = OperatorRow["OPERATORID"].ToString();
            SysOperatorInfo.OperatorCode = AutoServiceData.Operatorcodeno;
            SysOperatorInfo.OperatorName = OperatorRow["OPERATORNAME"].ToString();
            SysOperatorInfo.OperatorWorkkind = OperatorRow["WORKKIND"].ToString();
            SysOperatorInfo.UserID = OperatorRow["USERID"].ToString();

            SysOperatorInfo.OperatorOfficeID = OperatorRow["OFFICEID"].ToString(); 
            DataSet dsofficeArea = new OfficeFacade().FindHospitalAreaInfoByOfficeID(SysOperatorInfo.OperatorOfficeID);
            if (dsofficeArea.Tables.Count > 0 && dsofficeArea.Tables[0].Rows.Count > 0)
            {
                SysOperatorInfo.OperatorAreaid = dsofficeArea.Tables[0].Rows[0]["AREAID"].ToString();
                SysOperatorInfo.OperatorAreaname = dsofficeArea.Tables[0].Rows[0]["AREANAME"].ToString();
            }
            else
            {
                SysOperatorInfo.OperatorAreaid = string.Empty;
                SysOperatorInfo.OperatorAreaname = string.Empty;
            }

            OfficeAttachFacade officeAttachFacade = new OfficeAttachFacade();
            SysOperatorInfo.CustomerName = officeAttachFacade.GetCustomName();

            return true;

        }

        private static void UpdateSystemConfig()
        {
            SystemConfigFacade sf = new SystemConfigFacade();
            DataSet sysConfigData = sf.FindAll();
            ArrayList sysConfigCollection = new ArrayList();
            Skynet.Framework.Common.SystemConfig sysConfig = null;
            foreach (DataRow row in sysConfigData.Tables[0].Rows)
            {
                sysConfig = new Skynet.Framework.Common.SystemConfig();
                //配置编号
                sysConfig.ConfigureNo = row["CONFIGURENO"].ToString().Trim();
                //设置类型
                sysConfig.SetType = row["SETTYPE"].ToString().Trim();
                //设置内容
                sysConfig.SetContent = row["SETCONTENT"].ToString().Trim();
                //设置值
                sysConfig.DefaultValue = row["DEFAULTVALUE"].ToString().Trim();
                //说明
                sysConfig.Detail = row["DETAIL"].ToString().Trim();
                //可选参数
                sysConfig.OptionalPara = row["OPTIONALPARA"].ToString().Trim();
                sysConfigCollection.Add(sysConfig);
            }
            SystemInfo.SystemConfigs = new SystemConfigCollection();
            SystemInfo.SystemConfigs.Init(sysConfigCollection);
        }

        #endregion


        public static bool CloseCash = false;

        #region 变量
        public static bool SCANCARD = false;
        /// <summary>
        /// 病人基本信息
        /// </summary>
        //public static DataSet patientInfoData = new DataSet();

        /// <summary>
        /// 读卡信息
        /// </summary>
        public static CardInformationStruct cardInfoStruct = new CardInformationStruct();

        public static string DiagnoseID = string.Empty;

        //public static DataSet cardData = new DataSet();

        /// <summary>
        /// 健康卡信息
        /// </summary>
        public static CardAuthorizationData eCardAuthorizationData = new CardAuthorizationData();

        /// <summary>
        /// 卡余额
        /// </summary>
        public static Decimal cardBlance = 0;

        /// <summary>
        ///上次结算之后余额(不包括未结的费用)
        /// </summary>
        public static Decimal cardallmoney = 0;//

        /// <summary>
        /// 重新获取卡余额（在充值，取号，缴费以后都需要调用）
        /// </summary>
        public static void GetCardBalance()
        {
            CardAuthorizationFacade theCardAuthorizationFacade = new CardAuthorizationFacade();
            //SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance(SkyComm.DiagnoseID), 2);
            if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
            {
                SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance(SkyComm.DiagnoseID), 2);
            }
            else
            {
                SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance_New(SkyComm.DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString()), 2);
            }
        }

        public static DataSet dsCardType = null;
        public static string FeeType = null;
        public static string FeeTypeID = null;

        public static bool PosEnabled = false;

        /// <summary>
        /// 现金预存结算方式
        /// </summary>
        public static string AddMoneyCashMode = "现金";

        /// <summary>
        /// 银行预存结算方式
        /// </summary>
        public static string AddMoneyPosMode = "银行卡";

        #endregion

        #region 常用方法
        /// <summary>
        /// 弹出提示信息框
        /// </summary>
        /// <param name="Info"></param>
        public static void ShowMessageInfo(string Info)
        {
            MyAlert frm2 = new MyAlert(AlertTypeenum.信息, Info);
            frm2.ShowDialog();
            frm2.Dispose();
        }

        /// <summary>
        /// 退发钮事件
        /// </summary>
        /// <param name="frmtest"></param>
        public static void CloseWin(Form frmExitForm)
        {
            if (frmExitForm != null)
            {
                if (frmExitForm.Name.IndexOf("FrmMain") < 0)
                {
                    frmExitForm.Close();
                    CloseWin(frmExitForm.Owner as Form);
                }
            }
        }

        /// <summary>
        /// 充值返回类型：0：关闭，1：取号，2：缴费
        /// </summary>
        public static int CardSavingType { get; set; }

        #endregion

        #region 读卡

        public static int ReadCard(string ReadType)
        {
            #region wangchenyang 2018/5/29 case 31245（自助机增加设备扫描手机）,自助机的改造
            string _isDzCard = SystemInfo.SystemConfigs["是否启用电子卡签约"].DefaultValue;
            if (_isDzCard.Equals("1"))
            {
                FrmChooseCardType frmChooseCardType = new FrmChooseCardType();
                try
                {
                    if (frmChooseCardType.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(frmChooseCardType.strMsg))
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }                                         
                    }

                    if (frmChooseCardType.cardType != FrmChooseCardType.CardType.Entitycard)
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("电子卡二维码生成失败：" + ex.Message);
                }
                finally {
                    frmChooseCardType.Dispose();
                }
          
            }
            #endregion

            #region  chenqiang 2018.09.20 add by Case:31784
            if (SkyComm.getvalue("读卡是否提醒选择卡类型") == "1")//0:院内就诊卡   1：身份证
            {
                if (SkyComm.cardInfoStruct.CardNo != "" && SkyComm.cardInfoStruct.CardNo != null)
                    return 1;
                FrmReadType FrmReadType = new FrmReadType();
                try
                {
                    if (FrmReadType.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(FrmReadType.strMsg))
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }  
                    }
                 if (FrmReadType.cardType != FrmReadType.CardType.Entitycard)
                        return 0;
                }
                catch (Exception ex)
                {
                    LogService.GlobalInfoMessage("身份证件调用失败：" + ex.Message);
                    FrmReadType.Dispose();
                }               
                finally {
                    FrmReadType.Dispose();
                }
            }
            #endregion
          
            string CardID = string.Empty;


            switch (AutoHostConfig.ReadCardType)
            {
                case "302H":
                    break;
                case "XUHUI":
                    FrmReadCardXH frm1 = new FrmReadCardXH();
                    frm1.CardType = "1";
                    try
                    {
                        if (frm1.CheckCard() != "1")
                        {
                            if (frm1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            {
                                return 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用读卡器错误：" + ex.Message);
                    }
                    finally
                    {
                        frm1.Dispose();
                    }
                    break;
                case "XUHUIM1":
                    FrmReadCardXH frm2 = new FrmReadCardXH();

                    LogService.GlobalInfoMessage("进入了xuhuim1方法");

                    frm2.CardType = "2";
                    try

                    {
                        if (frm2.CheckCard() != "1")
                        {
                            if (frm2.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            {
                                return 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用读卡器错误：" + ex.Message);
                    }
                    finally
                    {
                        frm2.Dispose();
                    }
                    break;

                case "XUHUIM_PH"://省医院
                    FrmReadCardXH frm3 = new FrmReadCardXH();

                    LogService.GlobalInfoMessage("进入了cpu读卡方法");

                    frm3.CardType = "3";
                    try
                    {
                        if (frm3.CheckCard() != "1")
                        {
                            if (frm3.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            {
                                return 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogService.GlobalInfoMessage("调用读卡器错误：" + ex.Message);
                    }
                    finally
                    {
                        frm3.Dispose();
                    }
                    break;

                default:
                    FrmReadCardTest frm = new FrmReadCardTest();
                    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return 0;
                    }
                    break;
            }
            return 1;
        }

        public static int ExitCard()
        {
            switch (AutoHostConfig.ReadCardType)
            {
                case "302H":
                    break;
                case "XUHUI":
                case "XUHUIM1":
                case "XUHUIM_PH":
                    //Thread.Sleep(200);
                    //StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDNOTALLOWCARDIN\"><arguments></arguments></invoke>");
                    //string strResult = AutoServiceSDK.SDK.XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    //LogService.GlobalInfoMessage("调用禁止进卡号卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                    Thread.Sleep(200);
                    StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDOUTCARD\"><arguments></arguments></invoke>");
                    string strResult = AutoServiceSDK.SDK.XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    LogService.GlobalInfoMessage("调用退卡卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    break;
                default:
                    break;
            }
            return 1;
        }


        #endregion

        /// <summary>
        /// 隐藏部分身份证号码
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static string ConvertIdCard(string idCard)
        {
            try
            {
                if (string.IsNullOrEmpty(idCard)) return "";

                string mReplace = idCard.Substring(6, 8);

                return idCard.Replace(mReplace, "********");
            }
            catch (Exception e)
            {
                LogService.GlobalInfoMessage("身份证号隐藏异常：" + e.ToString());
                return idCard;
            }
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

        //倒计时结束后清除缓存
        public static void ClearCookie()
        {
            SkyComm.cardInfoStruct = new CardInformationStruct();
            SkyComm.eCardAuthorizationData.Tables[0].Clear();
            SkyComm.DiagnoseID = string.Empty;
            SkyComm.cardBlance = 0;
            SkyComm.cardallmoney = 0;
        }

    }
}
