using AutoServiceManage.CardSaving;
using AutoServiceManage.Common;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SdkData;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Clinic;
using BusinessFacade.His.Common;
using CardInterface;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;

namespace AutoServiceManage.Electronics
{
    public partial class FormElectronicsInput : Form
    {
        #region 变量
        private QuerySolutionFacade query = new QuerySolutionFacade();

        private string _telphone;
        private bool isHadCard = false;//签约电子卡前检测患者是否有院内卡
        private string cardNub = string.Empty;//电子卡签约获取患者既往院内卡号

        private string cbxCertificateNO = string.Empty;//身份证号
        private string patientName = string.Empty;//患者姓名
        private string strDiagnoseid = string.Empty;//诊疗号
        private string gender = string.Empty;//性别
        private string phoneNo = string.Empty;//电话
        private string birthday = string.Empty;//出生日期
        private string address = string.Empty;//地址
        private string nation = string.Empty;//民族

        private CardInformationStruct _cardInfoStruct = new CardInformationStruct();
        private CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();

        private string virtualCardId = string.Empty;
        public string TelePhone
        {
            get { return _telphone; }
            set { TelePhone = value; }
        }
        public IDCardInfo IdInfo { get; set; }
        private CardAuthorizationData eLCardAuthorizationData = new CardAuthorizationData();
        private ISendCardInterFace SendCard = null;
        #endregion
        public FormElectronicsInput(IDCardInfo idinfo)
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

        private void FormElectronicsInput_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();



            string projectType = SkyComm.getvalue("项目版本标识");
            if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
            {
                this.label12.Text = "2．	请仔细核对身份信息，本卡系就诊专用卡。";
            }
        }

        private void FormElectronicsInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();

        }

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


            this.lblOK.Enabled = false;
            this.btnReturn.Enabled = false;
            this.btnExit.Enabled = false;
            panel2.Enabled = false;
            _telphone = this.lbltel.Text.Trim();
            string CardNo = string.Empty;

            CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();


            try
            {
                isHadCard = false;
                cardNub = string.Empty;
                string cardType, cardTypeId;
                HealthCardInfoStruct Entity = new HealthCardInfoStruct();
                if (!cardRegulation(out cardType, out cardTypeId))//电子卡注册签约条件判断
                    return;

                #region 卡管平台电子卡注册

                HealthCardBase _healthcardBase = HealthCardBase.CreateEhealthCardInstance(this);

                HealthCardInfoStruct patientInfo = new HealthCardInfoStruct();
                patientInfo.XM = IdInfo.Name;
                patientInfo.XB = IdInfo.Sex;
                patientInfo.LXDH = _telphone;
                patientInfo.CSRQ = IdInfo.Birthday;
                patientInfo.SFZH = IdInfo.Number;
                patientInfo.DZ = IdInfo.Address;
                patientInfo.MZ = IdInfo.People;

                Entity = _healthcardBase.registerEHC(patientInfo);



                #endregion

                #region  HIS业务机制处理
                PatientInfoFacade pfacade = new PatientInfoFacade();
                string ConditionStr = cardType == "0" ? " AND A.IDENTITYCARD='" + IdInfo.Number + "' " : " AND A.GUARDIANIDNUMBER='" + IdInfo.Number + "' ";
                ConditionStr += " AND A.PATIENTNAME='" + IdInfo.Name + "'  ORDER BY  A.DIAGNOSEID DESC ";
                DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                if (dsState != null && dsState.Tables[0].Rows.Count > 0)
                {
                    strDiagnoseid = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                }
                virtualCardId = Entity.ehealthCardId;


                if (string.IsNullOrEmpty(strDiagnoseid))
                {
                    BindHis(virtualCardId);
                    eCardAuthorizationFacade.updateViceCardIdByCardId(IdInfo.Number, virtualCardId);//根据卡号修改电子健康卡号
                }
                else
                {
                    string sql = "select * from t_card_authorization where DIAGNOSEID = @DIAGNOSEID and CIRCUIT_STATE = 0 and CARDID = @CARDID and TYPEID = 3";
                    Hashtable ht = new Hashtable();
                    ht.Add("@DIAGNOSEID", strDiagnoseid);
                    ht.Add("@CARDID", IdInfo.Number);
                    DataSet dataSet = query.ExeQuery(sql, ht);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        bool flag = true;
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            if (!string.IsNullOrEmpty(dataRow["VICECARDID"].ToString()))
                            {
                                flag = false;
                                string sql1 = "update t_card_authorization set VICECARDID = @VICECARDID where ACCOUNT_ID = @ACCOUNT_ID";
                                Hashtable hashtable = new Hashtable();
                                hashtable.Add("@ACCOUNT_ID", dataRow["ACCOUNT_ID"].ToString());
                                hashtable.Add("@VICECARDID", virtualCardId);
                                query.ExeQuery(sql1, hashtable);
                            }
                        }

                        if (flag)
                        {
                            BindHis(virtualCardId);
                            eCardAuthorizationFacade.updateViceCardIdByCardId(IdInfo.Number, virtualCardId);//根据卡号修改电子健康卡号
                        }


                    }
                    else
                    {
                        BindHis(virtualCardId);
                        eCardAuthorizationFacade.updateViceCardIdByCardId(IdInfo.Number, virtualCardId);//根据卡号修改电子健康卡号
                    }
                }




                if (eLCardAuthorizationData != null)
                {
                    cardNub = eLCardAuthorizationData.Tables[0].Rows.Count > 0 ? eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString() : "";
                }
                

                if (strDiagnoseid != "")
                {

                    //eCardAuthorizationFacade.updateViceCardIdByCardId(cardNub, virtualCardId);//根据卡号修改电子健康卡号
                   
                }
                printVirtualCard(Entity);//打印二维码
                #endregion
            }
            catch (Exception ex)
            {
                SkynetMessage.MsgInfo(ex.Message);
            }







            DialogResult = System.Windows.Forms.DialogResult.OK;
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



        /// <summary>
        /// 电子卡注册签约条件判断
        /// </summary>
        /// <returns></returns>
        private bool cardRegulation(out string cardType, out string cardTypeId)
        {
            getElectronicCardType(out cardType, out cardTypeId);




            DateTime GetServerDateTime = new CommonFacade().GetServerDateTime();


            if (!CheckLicenseConfi()) { return false; }//his内部发卡条件判断
            if (cardType == "2")
                return true;
            PatientInfoFacade pfacade = new PatientInfoFacade();
            string electronicCardType = SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue;
            if (electronicCardType == "0")
            {
                #region  验证该患者是否已有院内卡
                string ConditionStr = cardType == "0" ? " AND A.IDENTITYCARD='" + IdInfo.Number + "' " : " AND A.GUARDIANIDNUMBER='" + IdInfo.Number + "' ";
                ConditionStr += " AND A.PATIENTNAME='" + IdInfo.Name + "' AND B.CIRCUIT_STATE IN (0,1)  ORDER BY  A.DIAGNOSEID DESC ";
                DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)//已办理院内卡
                {
                    if (dsState.Tables[0].Rows[0]["CARDTYPENAME"].ToString() != "身份证" && cardType == "0")
                    {
                        SkynetMessage.MsgInfo("患者[" + IdInfo.Name + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡,请先注销");
                        return false;
                    }
                    else
                    {
                        if (dsState.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")//院内卡已挂失
                        {
                            SkynetMessage.MsgInfo("患者[" + IdInfo.Name + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请先补发卡！");
                            return false;
                        }
                        cardNub = dsState.Tables[0].Rows[0]["CARDID"].ToString();
                        strDiagnoseid = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        //isHadCard = true;
                    }
                }
                #endregion
            }
            else
            {
                #region 验证患者是否有用身份证签约的院内卡

                DataSet dsState = pfacade.FindIsCardTypeIdentity(IdInfo.Number);
                if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)//已办理院内卡
                {
                    if (dsState.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")//院内卡已挂失
                    {
                        SkynetMessage.MsgInfo("患者[" + IdInfo.Name + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请先补发卡！");
                        return false;
                    }
                    cardNub = dsState.Tables[0].Rows[0]["CARDID"].ToString();
                    strDiagnoseid = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    //isHadCard = true;
                }
                #endregion
            }
            return true;
        }


        /// <summary>
        /// 获取电子卡卡类型和卡类型ID
        /// </summary>
        /// <returns>cardType:0电子卡  1：儿童卡 2：电子就诊卡</returns>
        private void getElectronicCardType(out string cardType, out string cardTypeId)
        {
            cardType = "0";
            cardTypeId = GetElectronicHealthCardConfig("CardType");
        }



        /// <summary>
        /// 获取电子健康卡配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string GetElectronicHealthCardConfig(string config)
        {

            string applicationDocumentPath = Path.Combine(Application.StartupPath, "ResidentsHealthCard.xml");

            if (File.Exists(applicationDocumentPath))
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(applicationDocumentPath);

                XmlNode xmlNode = xmlDoc.SelectSingleNode("healthCardConfigs/ElectronicHealthCardConfig/" + config);

                if (xmlNode != null)
                {
                    string CardType = xmlNode.InnerText;

                    return CardType;
                }

            }
            return "err";
        }


        private bool CheckLicenseConfi()
        {

            return true;
        }


        /// <summary>
        /// 通过电子卡号办理HIs院内卡
        /// </summary>
        /// <param name="isHandleCard">是否持有院内卡患者</param>
        /// <param name="ehealthCardId">电子卡号</param>
        private void BindHis(string ehealthCardId)
        {
            if (!string.IsNullOrEmpty(ehealthCardId))//返回电子卡号
            {
                #region  发卡
                string cardType, cardTypeId;
                getElectronicCardType(out cardType, out cardTypeId);



                DateTime GetServerDateTime = new CommonFacade().GetServerDateTime();  //douyaming 2014-9-1

                DataRow dr = this.eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].NewRow();

                TLicenseconfigFacade facade = new TLicenseconfigFacade();
                string where = " where MODULE='健康卡发卡'";
                DataSet dataset = facade.GetAllInfo(where);
                if (dataset != null && dataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dataset.Tables[0].Rows)
                    {
                        string field = item["FIELDNAME"].ToString();
                        int iswirte = Convert.ToInt32(item["ISWRITE"].ToString());
                        #region BIRTHDAY 出生日期
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_BIRTHDAY] = IdInfo.Birthday;
                        #endregion
                        #region IDENTITYNAME 证件类型
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYNAME] = "身份证";
                        //证件号码
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_IDENTITYCARD] = IdInfo.Number;
                        #endregion

                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NATION] = IdInfo.People;
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NATIONALITY] = "";
                        #region EMPLOYMENT 职业
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_EMPLOYMENT] = "";
                        #endregion
                        #region TELEPHONE 联系电话
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TELEPHONE] = _telphone;
                        #endregion
                        #region ADDRESS 联系地址
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ADDRESS] = IdInfo.Address;
                        #endregion
                        #region POSTALCODE 邮政编码
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_POSTALCODE] = "";
                        #endregion
                        #region BrAddress 出生地
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_HOMEPLACE] = "";//省
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_COUNTYSANJAK] = "";//市
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CSD_COUNTY] = "";//县
                        #endregion
                        #region UNITPOSTCODE 单位电话
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_UNITTELEPHONE] = "";
                        #endregion
                        #region REGISTEREDADDRESS 户口地址
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_REGISTEREDADDRESS] = "";
                        #endregion#region UNITPOSTCODE 邮政编码
                        #region UNITPOSTCODE 邮政编码
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_UNITPOSTCODE] = "";
                        #endregion#region UNITPOSTCODE 邮政编码
                        #region WORKUNIT 工作单位
                        dr[CardAuthorizationData.T_CARD_AUTHORIZATION_WORKUNIT] = "";
                        #endregion#region UNITPOSTCODE 邮政编码
                    }
                }




                DateTime GetDateTime = new CommonFacade().GetServerDateTime();
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ACCOUNT_ID] = "0000000000";
                //卡号
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID] = IdInfo.Number;
                //电子卡号绑定
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_VICECARDID] = ehealthCardId;
                //用户诊疗号
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DIAGNOSEID] = strDiagnoseid;
                // 卡类型
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TYPEID] = cardTypeId;
                //卡类型名称
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_TYPE_NAME] = "电子卡";
                //随机码
                ValidateCode vc = new ValidateCode();
                string RoundCode = vc.GenValidateCode();
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_RANDOMCODE] = RoundCode;

                //押金、工本费
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DEPOSIT] = 0;
                //姓名
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PATIENTNAME] = IdInfo.Name;
                //婚姻
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_MARRIAGESTATUS] = "";

                //拼音码
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_NAMEPY] = "";
                //性别
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SEX] = IdInfo.Sex;
                //出生年月
                //dr[CardAuthorizationData.T_CARD_AUTHORIZATION_BIRTHDAY] = this.dtBorn.Text; //由上面配置决定是否输入
                //费别
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SICKTYPE] = "";

                //保密等级
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_SECRECYCLASS] = "";
                //卡状态
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE] = 0;
                //照片
                //ImageConverter con = new ImageConverter();
                //年龄
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGE] = 0;
                //年龄单位
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AGEUNIT] = "";
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PERSONPHOTO] = new byte[10];

                //[工本费]支付方式
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_BALANCEMODE] = "";//不用支付
                                                                                //发卡时间
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PROVIDECARDDATE] = GetDateTime;

                #region 卡有效年限            

                //卡有效年限
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_CARDVALIDDATE] = new CommonFacade().GetServerDateTime().AddYears(1);
                #endregion

                //发卡方式
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_MODEID] = 0;
                //密码修改时间
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_LASTPASSWORDCHANGEDDATE] = GetDateTime;
                //最后修改时间
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_LASTLOGINDATE] = GetDateTime;

                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_PASSWORD] = "NOPWD";
                //操作员
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_OPERATOR] = SysOperatorInfo.OperatorID;

                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_AUDITING_STATE] = -1;//-1 悬空

                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_FEEOTHER] = "";

                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_ISGUARDIAN] = 0;
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_GUARDIANIDNUMBER] = IdInfo.Number;
                //门诊住院类型区分
                dr[CardAuthorizationData.T_CARD_AUTHORIZATION_DHTYPE] = "0";
                this.eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows.Add(dr);

                eLCardAuthorizationData.Tables[0].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CUSTOMNO] = 0;//暂时这样写

                decimal decMoney = 0;

                decimal decFees = 0;

                Hashtable BankTranht = new Hashtable();



                //string sql = "select * from TRADITIONAL_DECOCTION where ID=@DIAGNOSEID order by SOAKING_TIME desc";
                //Hashtable ht = new Hashtable();
                //ht.Add("@DIAGNOSEID", diagnoseID);
                //query.ExeQuery(sql, ht);







                eLCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.insertEntity(eLCardAuthorizationData);

                this.strDiagnoseid = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_DIAGNOSEID].ToString();
                _cardInfoStruct.CardNo = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();

                string cardID = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();

                #endregion
            }
        }



        /// <summary>
        ///chenqiang 2018.04.20 add by Case:31026   打印电子健康卡二维码
        /// </summary>
        /// <param name="imgData">电子健康卡二维码base64数据</param>
        private void printVirtualCard(HealthCardInfoStruct Entity)

        {
            try
            {
                string path = Application.StartupPath + @"\\Reports\\电子卡二维码.frx";
                PrintManager print = new PrintManager();
                if (System.IO.File.Exists(path) == true)
                {
                    using (PrintManager report = new PrintManager())
                    {
                        DataTable dtQRCode = new DataTable("reportQR");
                        if (Entity.imgData != "" && Entity.imgData != null)
                        {
                            byte[] bytesImg = Convert.FromBase64String(Entity.imgData);//字符串转换为字节
                            dtQRCode.Columns.Add("QRCode", typeof(byte[]));
                            DataRow drQrCode = dtQRCode.NewRow();
                            drQrCode["QRCode"] = bytesImg;
                            dtQRCode.Rows.Add(drQrCode);
                        }
                        else
                        {
                            dtQRCode.Columns.Add("QRCode", typeof(string));
                            DataRow drQrCode = dtQRCode.NewRow();
                            drQrCode["QRCode"] = Entity.QRCode;
                            dtQRCode.Rows.Add(drQrCode);
                        }
                        string cardType = string.Empty;
                        if (Entity.QRCode.IndexOf("EH") == 0)
                        {
                            cardType = "1";
                        }
                        else if (Entity.QRCode.IndexOf("EM") == 0)
                        {
                            cardType = "2";
                        }
                        else
                        {
                            cardType = "0";
                        }
                        print.InitReport("电子卡二维码");
                        print.AddParam("二维码", Entity.QRCode);
                        print.AddDataSet(dtQRCode, "电子卡二维码");
                        print.AddVariable("姓名", IdInfo.Name);
                        print.AddVariable("性别", IdInfo.Sex);
                        print.AddVariable("年龄", "");
                        print.AddVariable("出生日期", IdInfo.Birthday);
                        string idNumber = IdInfo.Number;
                        print.AddVariable("身份证号", idNumber);
                        print.AddVariable("卡类型", cardType);
                        PrintManager.CanDesign = true;
                        //print.PreView();
                        print.Print();
                        print.Dispose();
                    }
                }
                else
                {
                    SkynetMessage.MsgInfo("系统没有找到报表文件“电子卡二维码.frx”!");
                }
            }
            catch (Exception lex)
            {
                if (lex.Message.IndexOf("灾难性") > 0)
                {
                    SkynetMessage.MsgInfo(lex.Message + ": 打印机连接失败,请检查!");
                }
                else
                {
                    SkynetMessage.MsgInfo(lex.Message);
                }
            }
        }

    }
}
