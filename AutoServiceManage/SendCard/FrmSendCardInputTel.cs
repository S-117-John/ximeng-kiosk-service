using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.CardSaving;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SdkData;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using CardInterface;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using AutoServiceManage.Common;
using AutoServiceSDK.SdkService;
using TiuWeb.ReportBase;
using BusinessFacade.His.Register;

namespace AutoServiceManage.SendCard
{
    public partial class FrmSendCardInputTel : Form
    {
        #region 变量
        private string _telphone;

        public string TelePhone 
        {
            get { return _telphone; }
            set { TelePhone = value; }
        }
        public IDCardInfo IdInfo { get; set; }
        private CardAuthorizationData eLCardAuthorizationData = new CardAuthorizationData();
        private ISendCardInterFace SendCard = null;
        #endregion

        #region 窗体加载，关闭
        public FrmSendCardInputTel(IDCardInfo idinfo)
        {
            InitializeComponent();
            IdInfo = idinfo;
            this.lblxm.Text = idinfo.Name;
            this.lblsex.Text = idinfo.Sex;
            this.lblmz.Text = idinfo.People;
            this.lblzfzh.Text = idinfo.Number;
            this.lblcsrq.Text = idinfo.Birthday;
            this.lbldz.Text = idinfo.Address;
            picImage.ImageLocation = idinfo.ImagePath;
        }
        private void FrmSendCardInputTel_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            if (AutoHostConfig.SendCardType == "XUHUI")
            {
                SendCard = new AutoServiceSDK.SdkService.SendCard_XH();
            }
            else if (AutoHostConfig.SendCardType == "XUHUI_PH") //省医院
            {
                SendCard = new AutoServiceSDK.SdkService.SendCard_Ph(); //射频卡
            }
            else if (AutoHostConfig.SendCardType == "XUHUIM1") //省医院
            {
                SendCard = new AutoServiceSDK.SdkService.SendCard_Ph(); //射频卡
            }
            else if (AutoHostConfig.SendCardType == "XUHUI_XM")
            {
                SendCard = new AutoServiceSDK.SdkService.SendCardNew_XH();//射频卡
            }

            string projectType = SkyComm.getvalue("项目版本标识");
            if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
            {
                this.label12.Text = "2．	请仔细核对身份信息，本卡系就诊专用卡。";
            }
        }

        private void FrmSendCardInputTel_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
            try
            {
                Common_XH theCamera_XH = new Common_XH();
                theCamera_XH.DoorLightClose(LightTypeenum.出卡槽);
                theCamera_XH.DoorLightClose(LightTypeenum.发卡器);
                theCamera_XH.DoorLightClose(LightTypeenum.凭条);
            }
            catch (Exception exception)
            {

            }
        }
        #endregion

        #region 清屏
        private void lblclear_Click(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            lblErr.Text = string.Empty;
            lblErr.Visible = false;
            lbltel.Text = "请输入手机号";
            lbltel.ForeColor = System.Drawing.Color.Gray;
        }
        #endregion

        #region 退格
        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (lbltel.Text.IndexOf("号") < 0 && lbltel.Text.Length > 0)
            {
                string hmValue = lbltel.Text.Substring(0, lbltel.Text.Length - 1);
                lbltel.Text = hmValue;
            }
            if (lbltel.Text.Length == 0)
            {
                lbltel.Text = "请输入手机号";
                lbltel.ForeColor = System.Drawing.Color.Gray;
            }
            lblErr.Text = string.Empty;
            lblErr.Visible = false;
            ucTime1.Sec = 60;
        }
        #endregion

        #region 确认
        private void lblOK_Click(object sender, EventArgs e)
        {
           
            if (lbltel.Text.Length != 11)
            {
                lblErr.Text = "手机号长度必须是11位!";
                lblErr.Visible = true;
                return;
            }

            if (SendCard.CheckCard() == "1")
            {
                SkyComm.ShowMessageInfo("该自助机没有卡，请在其他自助机上继续发卡！");
                return;
            }
            this.lblOK.Enabled = false;
            this.btnReturn.Enabled = false;
            this.btnExit.Enabled = false;
            panel2.Enabled = false;
            _telphone = this.lbltel.Text.Trim();
            string CardNo = string.Empty;
            
            string strDiagnoseid = string.Empty;
            CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();

            using (WaitDialogForm form = new WaitDialogForm("正在发卡中，请稍候...", "正在组织发卡数据,请稍候......", new Size(240, 60)))
            {
                #region 办理就诊卡
                if (eLCardAuthorizationData != null)
                {
                    eLCardAuthorizationData.Tables[0].Rows.Clear();
                }
                
                

                SetCardInfo(IdInfo, _telphone);                
                eLCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.insertEntity(eLCardAuthorizationData);
                strDiagnoseid = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_DIAGNOSEID].ToString();
                CardNo = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();
                //写卡，如果失败则重试
                
                bool isSuccess = false;
                form.Caption = "正在写卡中，请稍候...";
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        //第一次写卡
                        if (SendCard.WriteCard(CardNo) == false)
                        {
                            //第一次写卡失败，再进行第二次写卡
                            if (SendCard.WriteCard(CardNo) == false)
                            {
                                //第二次写卡失败以后将卡回收
                                SendCard.ReturnCard();

                            }
                            else
                            {
                                isSuccess = true;
                                break;
                            }
                        }
                        else
                        {
                            isSuccess = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("发卡失败：" + ex.Message);
                    SkyComm.ShowMessageInfo("发卡失败：" + ex.Message);
                    isSuccess = false;
                    this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    this.lblOK.Enabled = true;
                    panel2.Enabled = true;
                }

                //写卡失败
                if (isSuccess == false)
                {
                    //撤消已发卡的信息
                    eCardAuthorizationFacade.deleteEntityAndCardSaving(eLCardAuthorizationData);
                    eLCardAuthorizationData = new CardAuthorizationData();
                    strDiagnoseid = string.Empty;
                    SkynetMessage.MsgInfo("写卡失败，请在其他自助机上重试！");
                    this.lblOK.Enabled = true;
                    this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    panel2.Enabled = true;

                    //第二次写卡失败以后将卡回收
                    SendCard.ReturnCard();
                    return;
                }
                #endregion
                form.Close();
            }
            #region 充值
            try
            {
                SkyComm.eCardAuthorizationData = eLCardAuthorizationData;
                SkyComm.DiagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                SkyComm.cardInfoStruct.CardNo = CardNo;
                SkyComm.cardBlance = 0;
                CardSavingFacade cf = new CardSavingFacade();
                SkyComm.cardallmoney = 0;

                int Savingsucceed = 0;
                decimal RechargeMoney = 0;
                string strModeType = "现金";
                try
                {
                    //充值，弹出界面选择是现金充值还是银行卡预存                       
                    if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
                    {
                        FrmCardSavingMain frm = new FrmCardSavingMain();
                        frm.CallType = 2;
                        frm.ShowDialog();
                        Savingsucceed = frm.Savingsucceed;
                        RechargeMoney = frm.RechargeMoney;
                        strModeType = frm.MODETYPE;
                        frm.Dispose();
                    }
                    else
                    {
                        //拍照
                        switch (AutoHostConfig.ReadCardType)
                        {
                            case "XUHUI":
                                AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                                camera.TakeCamera(SkyComm.cardInfoStruct.CardNo, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString(), AutoHostConfig.Machineno);
                                break;
                            default:
                                break;
                        }

                        FrmCardSavingCash frm = new FrmCardSavingCash();
                        frm.CallType = 2;
                        frm.ShowDialog();                                
                        Savingsucceed = frm.Savingsucceed;
                        RechargeMoney = frm.RechargeMoney;
                        frm.Dispose();
                    }

                    Log.Info("发卡充值界面结束后","Savingsucceed的值", Savingsucceed+"");

                    if (Savingsucceed == 1)
                    {
                        eLCardAuthorizationData.Tables[0].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_MODETYPE] = strModeType;
                        if (!eLCardAuthorizationData.Tables[0].Columns.Contains("OPERATORNAME"))
                        {
                            eLCardAuthorizationData.Tables[0].Columns.Add("OPERATORNAME");

                            eLCardAuthorizationData.Tables[0].Rows[0]["OPERATORNAME"] = SysOperatorInfo.OperatorName;
                        }
                        eCardAuthorizationFacade.updateEntity(eLCardAuthorizationData);
                        eLCardAuthorizationData.Tables[0].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = RechargeMoney; 
                       
                        //打印发卡凭证
                        PrintSendCardReport(eLCardAuthorizationData);
                    }
                }
                catch (Exception ex2)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("发卡充值失败：" + ex2.Message);
                }
                finally
                {

                }

                if (Savingsucceed == 1)
                {
                    SendCard.OutputCard();
                    try
                    {
                        Common_XH theCamera_XH = new Common_XH();
                        theCamera_XH.DoorLightOpen(LightTypeenum.出卡槽, LightOpenTypeenum.闪烁);
                        theCamera_XH.DoorLightOpen(LightTypeenum.发卡器, LightOpenTypeenum.闪烁);
                        theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
                    }
                    catch (Exception exception)
                    {
                       
                    }
                    SkyComm.ShowMessageInfo("发卡成功，请取走您的就诊卡!");
                }
                else
                {
                    //回收卡信息并且注册卡
                    //撤消已发卡的信息
                    Skynet.LoggingService.LogService.GlobalInfoMessage("充值失败，撤消发卡信息");
                    eCardAuthorizationFacade.deleteEntityAndCardSaving(eLCardAuthorizationData);
                    eLCardAuthorizationData = new CardAuthorizationData();
                    strDiagnoseid = string.Empty;
                    this.lblOK.Enabled = true;
                    this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    panel2.Enabled = true;

                    //发卡失败以后将卡进行回收
                    SendCard.ReturnCard();
                    return;
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("发卡失败：" + ex.Message + ",系统回收卡机中的卡");

                //发卡失败以后将卡进行回收
                SendCard.ReturnCard();

                SkynetMessage.MsgInfo("发卡失败：" + ex.Message);
                return;
            }
            finally
            {
                this.lblOK.Enabled = true;
                this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
                panel2.Enabled = true;

                SkyComm.cardInfoStruct = new CardInformationStruct();
                SkyComm.eCardAuthorizationData.Tables[0].Clear();
                SkyComm.DiagnoseID = string.Empty;
                SkyComm.cardBlance = 0;
                SkyComm.cardallmoney = 0;
            }
            #endregion

            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请在就诊卡出口取走您的就诊卡!");
            voice.EndJtts();


            DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        public CardAuthorizationData SetCardInfo(IDCardInfo userInfo, string TelePhone)
        {
            PinWbCode pw = new PinWbCode();

            eLCardAuthorizationData.Tables[0].Rows.Clear();
            DataRow dr = this.eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].NewRow();
            //卡号
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ACCOUNT_ID] = "0000000000";

            string strDiagnoseID = GetDiagnoseID(userInfo.Number, userInfo.Name);
            //用户诊疗号
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DIAGNOSEID] = strDiagnoseID;

            // 卡类型
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TYPEID] = SkyComm.dsCardType.Tables[0].Rows[0]["TYPE_NO"];
            //卡类型名称
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TYPE_NAME] = SkyComm.dsCardType.Tables[0].Rows[0]["TYPE_NAME"];
            //随机码
            ValidateCode vc = new ValidateCode();
            string RoundCode = vc.GenValidateCode();
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_RANDOMCODE] = RoundCode;

            Skynet.LoggingService.LogService.GlobalInfoMessage("发卡保存数据设置押金之前1");
            //押金、工本费
            if (!string.IsNullOrEmpty(SkyComm.getvalue("发卡工本费")) && SkyComm.getvalue("发卡工本费") != "0")
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DEPOSIT] = Convert.ToDecimal(SkyComm.getvalue("发卡工本费"));
            }
            else
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DEPOSIT] = SkyComm.dsCardType.Tables[0].Rows[0]["FEES"];
            }
            Skynet.LoggingService.LogService.GlobalInfoMessage("发卡保存数据设置押金之后1");
            //证件类型
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYNAME] = "身份证";
            //证件号码
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYCARD] = userInfo.Number;
            //姓名
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PATIENTNAME] = userInfo.Name.Replace("\n", "").Replace("\r", "");
            //拼音码
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NAMEPY] = pw.GetQuanPin(userInfo.Name);
            //性别
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SEX] = userInfo.Sex;
            //出生年月
            try
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_BIRTHDAY] = Convert.ToDateTime(userInfo.Birthday);
            }
            catch (Exception e)
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_BIRTHDAY] = DateTime.Now;
                Log.Info("发卡转换出生年月","", e.Message);
            }
            //户口地址
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_HOMEPLACE] = userInfo.Address;
            //费别
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SICKTYPE] = SkyComm.FeeTypeID;//费用类别
            //身份
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_EMPLOYMENT] = "";

            //职业
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_EMPLOYMENT] = "其他";
                    
            //家庭电话
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TELEPHONE] = TelePhone;
            //联系地址
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_REGISTEREDADDRESS] = userInfo.Address;
            //邮政编码
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_POSTALCODE] = "";
            //合同单位
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_WORKUNIT] = "";
            //单位电话t
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_UNITTELEPHONE] = "";
            //单位地址
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ADDRESS] = "";
            //单位邮编
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_UNITPOSTCODE] = "";
            //国籍NATIONALITY
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NATIONALITY] = "中国";
            //民族
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NATION] = userInfo.People;
            //保密等级
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SECRECYCLASS] = "内部级";
            //卡状态
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE] = 0;
            //照片
            //ImageConverter con = new ImageConverter();
            //年龄
            #region 设置年龄

            try
            {
                int age = DateTime.Today.Year - Convert.ToDateTime(userInfo.Birthday).Year;
                if (age > 0)
                {
                    dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGE] = age.ToString();
                    //年龄单位
                    dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGEUNIT] = "岁";
                }
                else
                {
                    int month = DateTime.Today.Month - Convert.ToDateTime(userInfo.Birthday).Month;
                    if (month > 0)
                    {
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGE] = month.ToString();
                        //年龄单位
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGEUNIT] = "月";
                    }
                    else
                    {
                        int day1 = Convert.ToDateTime(userInfo.Birthday).Day;
                        int day = DateTime.Today.Day - Convert.ToDateTime(userInfo.Birthday).Day;
                        if (day >= 0)
                        {
                            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGE] = day.ToString();
                            //年龄单位
                            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGEUNIT] = "天";

                        }
                    }
                }
            }
            catch (Exception e)
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGE] = "0";
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGEUNIT] = "岁";
                Log.Info("发卡设置年龄", "", e.Message);
            }
            #endregion
            //dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PERSONPHOTO] = userInfo.ImagePath;

            //发卡时间
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PROVIDECARDDATE] = DateTime.Now;
            //卡有效年限
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CARDVALIDDATE] = DateTime.Now.AddYears(1);
            //发卡方式
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_MODEID] = 0;
            //密码修改时间
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_LASTPASSWORDCHANGEDDATE] = DateTime.Now;
            //最后修改时间
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_LASTLOGINDATE] = DateTime.Now;
            //1-不设置密码，0-设置密码)

            //string PWD = cbxCardType.SelectedRow["IS_SET_PWD"].ToString();//1-不设置密码，0-设置密码)
            //if (PWD != "1")
            //{
            //    //密码
            //    dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PASSWORD] = this.strPassword;
            //}
            //else
            //{
            string PWD = SkyComm.getvalue("发卡默认密码").ToString().Trim();

            if (string.IsNullOrEmpty(PWD))
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PASSWORD] = "NOPWD";
            }
            else
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PASSWORD] = PWD;
            }

            //}
            //操作员
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATOR] = SysOperatorInfo.OperatorID;

            int INTERNAL_AUDITING = SkyComm.dsCardType.Tables[0].Rows[0]["INTERNAL_AUDITING"].ToString() == "1" ? -1 : 1;//1-不需要审核，0--需要审核
            // 是否需要内部审核状态(Internal_Auditing)0-	是（需要审核）、1-否（不需要审核）
            // (Auditing_State)0 已审核、1未审核  
            if (INTERNAL_AUDITING == 1)
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AUDITING_STATE] = 1;//未审核状态
            }
            else//1-不需要审核
            {
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AUDITING_STATE] = -1;//-1 悬空
            }
            dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ISGUARDIAN] = 0;

            this.eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows.Add(dr);
            return eLCardAuthorizationData;
        }

        private string GetDiagnoseID(string IDentityCard,string PatientName)
        {
            #region 获取诊疗号
            string diagnoseId = string.Empty;
            PatientInfoFacade pfacade = new PatientInfoFacade();
            DataSet dsPaitent = pfacade.FindByIdentity(IDentityCard);
            if (dsPaitent.Tables.Count > 0 & dsPaitent.Tables[0].Rows.Count > 0)
            {
                if (dsPaitent.Tables[0].Rows.Count == 1)
                {
                    if (dsPaitent.Tables[0].Rows[0]["PATIENTNAME"].ToString() == PatientName)
                    {
                        string ConditionStr = " AND A.IDENTITYCARD='" + IDentityCard + "' AND A.PATIENTNAME='" + PatientName + "'";
                        DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                        if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] dr0 = dsState.Tables[0].Select("CIRCUIT_STATE=0");
                            if (dr0.Length > 0)
                            {
                                throw new Exception("-1|该病人已发卡，不能重复发卡！");
                            }
                            else
                            {
                                dr0 = dsState.Tables[0].Select("CIRCUIT_STATE=1");
                                if (dr0.Length > 0)
                                {
                                    throw new Exception( "-1|该病人已发卡，且卡已挂失，请补发卡！！");
                                }
                            }
                        }
                        else
                        {
                            diagnoseId = dsPaitent.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in dsPaitent.Tables[0].Rows)
                    {
                        if (item["PATIENTNAME"].ToString() == PatientName)
                        {
                            string ConditionStr = " AND A.IDENTITYCARD='" + IDentityCard + "' AND A.PATIENTNAME='" + PatientName + "'";
                            DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                            if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)
                            {
                                DataRow[] dr0 = dsState.Tables[0].Select("CIRCUIT_STATE=0", "DIAGNOSEID DESC");
                                if (dr0.Length > 0)
                                {
                                    throw new Exception( "-1|该病人已发卡，不能重复发卡！");
                                }
                                else
                                {
                                    dr0 = dsState.Tables[0].Select("CIRCUIT_STATE=1", "DIAGNOSEID DESC");
                                    if (dr0.Length > 0)
                                    {
                                        throw new Exception( "-1|该病人已发卡，且卡已挂失，请补发卡！");
                                    }
                                    else
                                    {
                                        dr0 = dsState.Tables[0].Select("CIRCUIT_STATE=2", "DIAGNOSEID DESC");
                                        if (dr0.Length > 0)
                                        {
                                            diagnoseId = dr0[0]["DIAGNOSEID"].ToString();
                                            break;
                                        }
                                        else
                                        {
                                            diagnoseId = item["DIAGNOSEID"].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                diagnoseId = item["DIAGNOSEID"].ToString();
                                break;
                            }
                        }
                    }
                }
            }

            return diagnoseId;
            #endregion
        }

        private void PrintSendCardReport(DataSet eLCardAuthorizationData)
        {
            eLCardAuthorizationData.WriteXml(Application.StartupPath + @"\\ReportXml\\自助发卡凭证" + eLCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString() + ".xml");
            string path = Application.StartupPath + @"\\Reports\\自助发卡凭证.frx";

            if (System.IO.File.Exists(path) == false)
            {
                SkynetMessage.MsgInfo("自助发卡凭证不存在,请联系管理员!");
                return;
            }
            Skynet.LoggingService.LogService.GlobalInfoMessage("打印发卡证证");

            decimal decYJ = 0;
            string stryj = SkyComm.getvalue("发卡工本费");
            if (string.IsNullOrEmpty(stryj) || stryj == "0")
            {
                decYJ = Convert.ToDecimal(SkyComm.dsCardType.Tables[0].Rows[0]["FEES"].ToString());
            }
            else
            {
                decYJ = Convert.ToDecimal(stryj);
            }

            //Common_XH theCamera_XH = new Common_XH();
            //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
            PrintManager print = new PrintManager();
            print.InitReport("自助发卡凭证");
            print.AddParam("医院名称", SysOperatorInfo.CustomerName);
            print.AddParam("姓名", lblxm.Text);
            print.AddParam("卡押金", decYJ);
            print.AddParam("操作员", SysOperatorInfo.OperatorCode);
            print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
            print.AddData(eLCardAuthorizationData.Tables[0], "report");
            PrintManager.CanDesign = true;
            print.Print();
            print.Dispose();
            Thread.Sleep(100);
        }

        #endregion

        #region 输入数字0-9
        private void lbl0_Click(object sender, EventArgs e)
        {
            lblErr.Visible = false;
            if (lbltel.Text.Length >= 11)
            {
                lblErr.Text = "手机号长度不能超过11位!";
                lblErr.Visible = true;
                return;
            }

            Label lbl = (Label)sender;
            string num = lbl.Name.Substring(lbl.Name.Length - 1, 1);
            this.lbltel.ForeColor = System.Drawing.Color.Red;
            InputText(num);
            ucTime1.Sec = 60;
        }

        private void InputText(string Num)
        {
            if (lbltel.Text.IndexOf("号") > 0)
            {
                lbltel.Text = Num;
            }
            else
            {
                lbltel.Text = lbltel.Text + Num;
            }
            System.Console.Beep(1234, 150);
        }

        #endregion

        #region 返回,退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        #endregion

        #region 异步方法
        AnsyCall _call;
        List<string> NotEnableArray = new List<string>();
        protected void AnsyWorker(Action<UpdataUIAction> action, AnsyStyle style)
        {
            if (this.AnsyIsBusy)
                return;
            if (_call == null)
            {
                _call = new AnsyCall(this);
                _call.WorkCompletedAction = () =>
                {
                    NotEnableArray.Clear();
                };
            }
            //使工具栏 置灰
            _call.AnsyWorker(action, style);
        }
        protected void AnsyWorker(Action<UpdataUIAction> action)
        {
            AnsyWorker(action, AnsyStyle.LoadingPanel);
        }

        protected bool AnsyIsBusy
        {
            get
            {
                if (_call == null)
                    return false;
                else
                    return _call.IsBusy;
            }
        }
        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请输入您的手机号!");
            voice.EndJtts();
        }
        #region 按键效果

        private void lbl7_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BorderStyle = BorderStyle.Fixed3D;
        }

        private void lbl7_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BorderStyle = BorderStyle.None;
        }
        #endregion
    }
}
