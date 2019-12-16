using AutoServiceManage.Common;
using BusinessFacade.His.Common;
using BusinessFacade.His.Disjoin;
using BusinessFacade.His.Register;
using EntityData.His.Common;
using EntityData.His.Register;
using Skynet.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TiuWeb.ReportBase;
using System.Threading;
using System.Collections;
using AutoServiceSDK.SdkService;
using Skynet.AreaDataExchangeInterfaces.RegisterClouldInterface;

namespace AutoServiceManage.BespeakRegister
{
    public partial class FrmBespeakConfirm : Form
    {
        public FrmBespeakConfirm()
        {
            InitializeComponent();
        }

        private BespeakRegisterFacade bespeakFac;

        public BespeakRegisterData BespeakDataset = new BespeakRegisterData();

        public RegisterInfoData registerInfoData = new RegisterInfoData();

        public RegisterInfoFacade registerFacade = new RegisterInfoFacade();

        private PatientInfoData patientData;

        private PatientInfoFacade patientFacade;

        private ArranageRecordFacade arranageRecordFacade = new ArranageRecordFacade();

        private string REGISTERCLASS = string.Empty;
        private decimal Gbf = 0;
        private decimal Zj = 0;
        private string Ghf = string.Empty;
        private decimal jcf = 0;  //科室检查费
        private bool isDepartCheckCharge = true;
        public string arrangeSource = string.Empty;//HIS排班数据来源
        public string arranageDetailSource = string.Empty;//分时排班数据来源
        private void FrmBespeakConfirm_Load(object sender, EventArgs e)
        {
            bespeakFac = new BespeakRegisterFacade();

            patientData = new PatientInfoData();

            patientFacade = new PatientInfoFacade();

            if (BespeakDataset != null && BespeakDataset.Tables.Count > 0 && BespeakDataset.Tables[0].Rows.Count > 0)
            {
                patientData = (PatientInfoData)patientFacade.FindAllInfoByDiagnoseId(SkyComm.DiagnoseID);
                if (patientData.Tables.Count > 0 && patientData.Tables[0].Rows.Count > 0)
                {
                    this.lblPatientName.Text = "患者姓名：" + patientData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                    this.lblSex.Text = "性别：" + patientData.Tables[0].Rows[0]["SEX"].ToString();
                }
                else
                {
                    this.lblPatientName.Text = "患者姓名：";
                    this.lblSex.Text = "性别：";
                }
                this.lblOffice.Text = "科室：" + BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICENAME"].ToString();
                this.lblDoctorName.Text = "医师姓名：" + BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORNAME"].ToString();
                this.lblRole.Text = "职称：" + BespeakDataset.Tables[0].Rows[0]["ROLE"].ToString();
                this.lblWorkType.Text = "班次：" + BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString();
                if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
                {
                    this.lblOrder.Text = "排队号：" + BespeakDataset.Tables[0].Rows[0]["QUEUEID"].ToString();
                }
                else
                {
                    this.lblOrder.Visible = false;
                }
                this.lblOfficeDddress.Text = "科室位置：" + BespeakDataset.Tables[0].Rows[0]["OFFICEADDRESS"].ToString();

                if (Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date == DateTime.Now.Date)
                {
                    this.lblDate.Text = "挂号时间：" + BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString();
                    label1.Text = "确认挂号信息";

                    lblghfy.Visible = true;

                    decimal Doctorzj = 0;
                    DataSet dsRecord = arranageRecordFacade.FindArranageByArranageRecordID(BespeakDataset.Tables[0].Rows[0]["ARRANAGERECORDID"].ToString());
                    if (dsRecord.Tables.Count > 0 && dsRecord.Tables[0].Rows.Count > 0)
                    {
                        REGISTERCLASS = dsRecord.Tables[0].Rows[0]["REGISTERCLASS"].ToString();//挂号方式
                        Gbf = Convert.ToDecimal(dsRecord.Tables[0].Rows[0]["MEDICALRECORDFEE"].ToString());//工本费
                        Ghf = dsRecord.Tables[0].Rows[0]["REGISTERFEE"].ToString();//挂号费
                        Doctorzj = DecimalRound.Round(Convert.ToDecimal(dsRecord.Tables[0].Rows[0]["DOCTOREXAMINEMONEY"].ToString()), 2);
                    }

                    RegisterTypeFacade RegTypeFacade = new RegisterTypeFacade();
                    DataSet RegTypeSet = RegTypeFacade.FindAll();
                    DataRow regTypeRow = RegTypeSet.Tables[0].AsEnumerable().FirstOrDefault(o => o.Field<string>("REGISTERCLASS") == REGISTERCLASS);

                    if (regTypeRow["ISDEPARTCHECKCHARGES"].ToString() == "0")
                        isDepartCheckCharge = false;

                    if (null != regTypeRow && !string.IsNullOrEmpty(regTypeRow["EXAMINEMONEY"].ToString()))
                    {
                        Zj = DecimalRound.Round(Convert.ToDecimal(regTypeRow["EXAMINEMONEY"]), 2) + Doctorzj;
                    }
                    else
                    {
                        Zj = Doctorzj;
                    }

                    PlusFeeFacade plusFeeFacade = new PlusFeeFacade();
                    DataSet dsPlusFee = plusFeeFacade.FindAll(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString());
                    if (dsPlusFee.Tables[0].Rows.Count > 0 && isDepartCheckCharge == true)
                    {
                        jcf = DecimalRound.Round(Convert.ToDecimal(dsPlusFee.Tables[0].Rows[0]["PLUSFEE"]), 2);
                    }
                    decimal decMoney = Convert.ToDecimal(Ghf) + Convert.ToDecimal(Zj) + Convert.ToDecimal(Gbf) + Convert.ToDecimal(jcf);

                    lblghfy.Text = "挂号费用：" + DecimalRound.Round(decMoney, 2);
                }
                else
                {
                    this.lblDate.Text = "预约时间：" + BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString();
                    label1.Text = "确认预约信息";
                    lblghfy.Visible = false;
                }
            }

            ucTime1.Sec = 60;

            ucTime1.timer1.Start();
        }

        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            ucTime1.timer1.Stop();
            SkyComm.CloseWin(this);
        }

        private void lblOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date == DateTime.Now.Date) //如果预约时间为当天，则调用挂号方法，重新组织数据
                {
                    #region 增加挂号提示信息
                    try
                    {
                        string registerMsg = SkyComm.getvalue("挂号提示");
                        if (!string.IsNullOrEmpty(registerMsg))
                        {
                            using (FrmYesNoAlert frmCheck = new FrmYesNoAlert())
                            {
                                frmCheck.Title = "挂号提示";
                                frmCheck.Msg = registerMsg;
                                frmCheck.sec = 90;
                                if (frmCheck.ShowDialog()!= DialogResult.OK)
                                {
                                    return;
                                }
                            }
                        }
                    }
                    catch
                    { }
                    #endregion

                    if (registerInfoData.Tables.Count > 0 && registerInfoData.Tables[0].Rows.Count > 0)
                    {
                        registerInfoData.Tables[0].Rows.Clear();
                    }
                    DataRow rowReg = registerInfoData.Tables[0].NewRow();
                    int i = registerInfoData.Tables[0].Rows.Count + 1;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_REGISTERID] = "新增" + i.ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_CANCELMARK] = "0";
                    rowReg[RegisterInfoData.T_REGISTER_INFO_OPERATORID] = SysOperatorInfo.OperatorID;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_OPERATEDATE] = new CommonFacade().GetServerDateTime();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_CHARGEMARK] = 0;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_PATIENTNAME] = patientData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_SEX] = patientData.Tables[0].Rows[0]["SEX"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_REGISTEROFFICEID] = BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_OFFICE] = BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICENAME"].ToString();

                    rowReg[RegisterInfoData.T_REGISTER_INFO_REGISTERCLASS] = REGISTERCLASS;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_DOCTORID] = BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORID"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_USERNAME] = BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORNAME"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_CASECOST] = Gbf;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_BESPEAKMONEY] = 0;//预约费
                    rowReg[RegisterInfoData.T_REGISTER_INFO_REGISTERFEE] = Ghf;

                    rowReg[RegisterInfoData.T_REGISTER_INFO_EXAMINEMONEY] = Zj;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_BESPEAKID] = "";
                    rowReg[RegisterInfoData.T_REGISTER_INFO_DIAGNOSEID] = SkyComm.DiagnoseID;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_AGE] = patientData.Tables[0].Rows[0]["AGE"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_AGEUNIT] = patientData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_ADDRESS] = patientData.Tables[0].Rows[0]["ADDRESS"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_TELEPHONE] = patientData.Tables[0].Rows[0]["TELEPHONE"].ToString();

                    OfficeFacade officeFacade = new OfficeFacade();
                    DataSet officeSet = officeFacade.QueryByOfficeType(26, "");
                    DataRow officeRow = officeSet.Tables[0].AsEnumerable().FirstOrDefault(o => o.Field<string>("OFFICEID") == BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString());
                    rowReg[RegisterInfoData.T_REGISTER_INFO_OFFICEADDRESS] = officeRow["OFFICEADDRESS"].ToString().Trim();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_DIAGNOSESTATE] = 0;

                    rowReg[RegisterInfoData.T_REGISTER_INFO_CHECKFEE] = jcf;
                    rowReg[RegisterInfoData.T_REGISTER_INFO_WORKTYPE] = BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_ARRANAGERECORDID] = BespeakDataset.Tables[0].Rows[0]["ARRANAGERECORDID"].ToString();
                    //if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
                    //{
                    rowReg[RegisterInfoData.T_REGISTER_INFO_QUEUEID] = BespeakDataset.Tables[0].Rows[0]["QUEUEID"].ToString();
                    //}
                    rowReg[RegisterInfoData.T_REGISTER_INFO_BIRTHDAY] = patientData.Tables[0].Rows[0]["BIRTHDAY"].ToString();
                    rowReg[RegisterInfoData.T_REGISTER_INFO_EXECDATE] = new CommonFacade().GetServerDateTime();
                    rowReg["BESPEAKDATE"] = Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString());
                    rowReg["BESPEAKMODE"] = "";

                    decimal decMoney = Convert.ToDecimal(Ghf) + Convert.ToDecimal(Zj) + Convert.ToDecimal(Gbf) + Convert.ToDecimal(jcf);
                    rowReg["CASHDEFRAY"] = decMoney;
                    rowReg["ACCOUNTDEFRAY"] = 0;
                    rowReg["DISCOUNTDEFRAY"] = 0;
                    rowReg["BALANCEMARK"] = 2;
                    rowReg["BALANCESTATE"] = 0;
                    rowReg["BALANCESTATE"] = 0;
                    rowReg["BALANCEMODE"] = "预交金";//结算方式
                    registerInfoData.Tables[0].Rows.Add(rowReg);
                    //chenqiang 2017-03-09 add by Case:27423
                    bespeakFac = new BespeakRegisterFacade();

                    //ZHOUHU ADD 20180130  CASE:29222
                    string isRegContr = new OfficeFacade().FindByOfficeID(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString()).Tables[0].Rows[0]["ISREGCONTR"].ToString();

                    if ("1" == SystemInfo.SystemConfigs["挂号、预约限制条件"].DefaultValue && isRegContr == "1")
                    {
                        if (registerFacade.FindNowDateRegisterInfo(patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), DateTime.Now.Date) > 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一位患者同天同科室限一个号，系统不允许挂号");
                            registerInfo.ShowDialog();
                            return;
                        }
                        if (bespeakFac.FindBespeakPatientInfo(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), DateTime.Now.Date, patientData.Tables[0].Rows[0]["IDENTITYCARD"].ToString(), patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString()) > 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一位患者同天同科室限一个号,当天已有预约信息，系统不允许挂号");
                            registerInfo.ShowDialog();
                            return;
                        }
                    }
                    //chenqiang 2017.06.16  add by Case:28088
                    if (registerFacade.FindNowDateRegisterInfo(registerInfoData.Tables[0].Rows[0]["DOCTORID"].ToString(), registerInfoData.Tables[0].Rows[0]["REGISTEROFFICEID"].ToString(), registerInfoData.Tables[0].Rows[0]["WORKTYPE"].ToString(), registerInfoData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), Convert.ToDateTime(registerInfoData.Tables[0].Rows[0]["OPERATEDATE"].ToString()).Date) > 0)
                    {
                        MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一个患者同一个班次只能挂一个号，系统不允许挂号");
                        registerInfo.ShowDialog();
                        return;
                    }
                    if (bespeakFac.FindBespeakPatientInfo(registerInfoData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), registerInfoData.Tables[0].Rows[0]["DOCTORID"].ToString(), registerInfoData.Tables[0].Rows[0]["REGISTEROFFICEID"].ToString(), registerInfoData.Tables[0].Rows[0]["WORKTYPE"].ToString(), Convert.ToDateTime(registerInfoData.Tables[0].Rows[0]["OPERATEDATE"].ToString()).Date) > 0)
                    {
                        MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一个患者同一个班次只能挂一个号，系统不允许挂号");
                        registerInfo.ShowDialog();
                        return;
                    }
                    DataSet ds = this.registerFacade.insertEntityForFZBD(this.registerInfoData);

                    MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "挂号成功！");
                    frmAlter.ShowDialog();

                    //输出挂号凭证
                    if (null == ds.Tables[0].Columns["EXAMINENAME"])
                    {
                        DataColumn col = new DataColumn("EXAMINENAME", typeof(System.String));
                        col.Caption = "EXAMINENAME";
                        ds.Tables[0].Columns.Add(col);
                    }

                    if (!ds.Tables[0].Columns.Contains("EXAMINELOC"))
                    {
                        ds.Tables[0].Columns.Add("EXAMINELOC");
                    }

                    UsersFacade uf = new UsersFacade();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string keyval = row["DOCTORID"].ToString();
                        if (keyval != "-1")
                            row["EXAMINENAME"] = uf.FindByUserInfo(keyval).Tables[0].Rows[0]["EXAMINENAME"].ToString();

                        ExamineFacade ef = new ExamineFacade();
                        string i_EXAMINENAME = uf.FindByPrimaryKeyString(keyval).ToString();
                        if (i_EXAMINENAME == "")
                        {
                            row["EXAMINELOC"] = "";
                        }
                        else
                        {
                            DataRow[] rw = ef.FindAllExamine().Tables[0].Select("EXAMINENAME = '" + i_EXAMINENAME + "' AND OFFICEID = '" + row["REGISTEROFFICEID"].ToString() + "'");
                            if (rw.Length > 0)
                            {
                                row["EXAMINELOC"] = rw[0]["EXAMINELOC"].ToString();
                            }
                            else
                            {
                                row["EXAMINELOC"] = "";
                            }
                        }
                    }

                    SkyComm.GetCardBalance();

                    PrintReport(ds, "自助挂号");
                    // PrintTriageInfo(ds.Tables[0], theServerTime);
                }
                else
                {
                    DataRow dr = BespeakDataset.Tables[0].Rows[0];
                    dr["PATIENTNAME"] = patientData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                    dr["TELEPHONE"] = patientData.Tables[0].Rows[0]["TELEPHONE"].ToString();
                    dr["IDENTITYCARD"] = patientData.Tables[0].Rows[0]["IDENTITYCARD"].ToString();
                    dr["SEX"] = patientData.Tables[0].Rows[0]["SEX"].ToString();

                    //判断是否已经预约
                    DataSet dsBespeak = new DataSet();
                    if (!string.IsNullOrEmpty(patientData.Tables[0].Rows[0]["IDENTITYCARD"].ToString()))
                    {
                        dsBespeak = bespeakFac.FindBespeakInfoByIdentityOrDiagnoseId(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORID"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()), BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString(), patientData.Tables[0].Rows[0]["IDENTITYCARD"].ToString(), "", patientData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
                    }
                    else
                    {
                        dsBespeak = bespeakFac.FindBespeakInfoByIdentityOrDiagnoseId(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORID"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()), BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString(), "", patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), "");
                    }
                    if (Convert.ToInt16(dsBespeak.Tables[0].Rows[0][0]) > 0)
                    {
                        MyAlert frm = new MyAlert(AlertTypeenum.信息, Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).ToShortDateString() + "已有预约信息，不能再次预约！");
                        frm.ShowDialog();
                        return;
                    }

                    //ZHOUHU ADD 20180130  CASE:29222
                    string isRegContr = new OfficeFacade().FindByOfficeID(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString()).Tables[0].Rows[0]["ISREGCONTR"].ToString();
                    
                    //chenqiang 2017-03-09 add by Case:27423
                    if ("1" == SystemInfo.SystemConfigs["挂号、预约限制条件"].DefaultValue && isRegContr == "1")
                    {
                        bespeakFac = new BespeakRegisterFacade();
                        if (bespeakFac.FindBespeakPatientInfo(BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date, patientData.Tables[0].Rows[0]["IDENTITYCARD"].ToString(), patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString()) > 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一位患者同天同科室限一个号,当天已有预约信息，系统不允许挂号");
                            registerInfo.ShowDialog();
                            return;
                        }
                        if (registerFacade.FindNowDateRegisterInfo(patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date) > 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一位患者同天同科室限一个号,当天已有预约信息，系统不允许挂号");
                            registerInfo.ShowDialog();
                            return;
                        }
                    }

                    //chenqiang 2017.06.16  add by Case:28088
                    if (registerFacade.FindNowDateRegisterInfo(BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString(), patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date) > 0)
                    {
                        MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一个患者同一个班次只能挂一个号，系统不允许挂号");
                        registerInfo.ShowDialog();
                        return;
                    }
                    if (bespeakFac.FindBespeakPatientInfo(patientData.Tables[0].Rows[0]["DIAGNOSEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORID"].ToString(), BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICEID"].ToString(), BespeakDataset.Tables[0].Rows[0]["WORKTYPE"].ToString(), Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date) > 0)
                    {
                        MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "一个患者同一个班次只能挂一个号，系统不允许挂号");
                        registerInfo.ShowDialog();
                        return;
                    }
                    //chenqiang add case:33511
                    if (SystemInfo.SystemConfigs["是否启用号源云平台接口"].DefaultValue != "0")
                    {
                        if (BespeakDataset.Tables[0].Rows.Count > 1)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "在启用号源接口时，同一次只能预约一个医生！");
                            registerInfo.ShowDialog();
                            return;
                        }
                    }

                    BespeakDataset = (BespeakRegisterData)bespeakFac.insertBespeakEntityReturn(BespeakDataset, patientData, 1);
                    //chenqiang add case:33511
                    if (SystemInfo.SystemConfigs["是否启用号源云平台接口"].DefaultValue != "0")
                    {
                        int stepCount = 0;
                        try
                        {
                            if (arrangeSource == "1" && arranageDetailSource == "0" && Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date != new CommonFacade().GetServerDateTime().Date)
                            {
                                RegisterClouldInterface theRegisterClouldInterface = new RegisterClouldInterface();
                                stepCount++;
                                int intResult = theRegisterClouldInterface.BespeakRegisteInfo(BespeakDataset);
                                if (intResult < 0)
                                {
                                    bespeakFac.CancelBespeak(BespeakDataset.Tables[0].Rows[0]["BESPEAKID"].ToString(), SysOperatorInfo.OperatorID);
                                    BespeakDataset.Tables[0].Clear();
                                    patientData.Tables[0].Clear();
                                    SkynetMessage.MsgInfo("调用微信同步号源失败，HIS预约数据已回滚，预约失败！");
                                    return;
                                }
                            }
                        }
                        catch
                        {
                            if (stepCount != 0)
                            {
                                bespeakFac.CancelBespeak(BespeakDataset.Tables[0].Rows[0]["BESPEAKID"].ToString(), SysOperatorInfo.OperatorID);
                                BespeakDataset.Tables[0].Clear();
                                patientData.Tables[0].Clear();
                                SkynetMessage.MsgInfo("调用微信同步号源失败，HIS预约数据已回滚，预约失败！");
                                return;
                            }
                        }
                    }

                    MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "预约成功！");
                    frmAlter.ShowDialog();
                    PrintReport(BespeakDataset, "自助预约");
                    //预约成功后，发短信
                    try
                    {
                        Hashtable htPara = new Hashtable();
                        htPara.Add("@预约时间", Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"]).ToString("MM月dd日HH时mm分"));
                        string OfficeAddress = SysOperatorInfo.OperatorAreaname + BespeakDataset.Tables[0].Rows[0]["OFFICEADDRESS"].ToString();
                        htPara.Add("@科室位置", OfficeAddress);
                        htPara.Add("@科室", BespeakDataset.Tables[0].Rows[0]["BESPEAKOFFICENAME"].ToString());
                        htPara.Add("@医生", BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORNAME"].ToString());
                        htPara.Add("@排队号", BespeakDataset.Tables[0].Rows[0]["QUEUEID"].ToString());
                        htPara.Add("@预约号", BespeakDataset.Tables[0].Rows[0]["BESPEAKID"].ToString());
                        UMSMsgLib.UMSMsg.Instance.SendMsg(BespeakDataset.Tables[0].Rows[0]["TELEPHONE"].ToString(), "预约挂号", htPara);
                    }
                    catch (Exception ex)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("预约成功后，发短信失败！" + ex.Message);
                    }

                }

                SkyComm.CloseWin(this);
            }
            catch (Exception ex)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, ex.Message);
                frm.ShowDialog();
            }
        }

        private void FrmBespeakConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        #region 打印挂号凭证
        /// <summary>
        /// 打印自助挂号
        /// </summary>
        private void PrintReport(DataSet ds, string printType)
        {
            if (printType == "自助挂号")
            {
                ds.WriteXml(Application.StartupPath + @"\\ReportXml\\" + printType + ds.Tables[0].Rows[0]["REGISTERID"].ToString() + ".xml");
            }
            else
            {
                ds.WriteXml(Application.StartupPath + @"\\ReportXml\\" + printType + ds.Tables[0].Rows[0]["BESPEAKID"].ToString() + ".xml");
            }
            string path = Application.StartupPath + @"\\Reports\\" + printType + ".frx";

            if (System.IO.File.Exists(path) == false)
            {
                SkynetMessage.MsgInfo(printType + "票据不存在,请联系管理员!");
                return;
            }
            //Common_XH theCamera_XH = new Common_XH();
            //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
            PrintManager print = new PrintManager();
            print.InitReport(printType);
            print.AddParam("医院名称", SysOperatorInfo.CustomerName);
            print.AddParam("姓名", SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString());
            print.AddParam("卡余额", SkyComm.cardBlance);
            print.AddParam("操作员", SysOperatorInfo.OperatorCode);
            print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
            print.AddData(ds.Tables[0], "report");
            PrintManager.CanDesign = false;
            //print.PreView();
            print.Print();
            print.Dispose();
            Thread.Sleep(100);
        }

        #endregion
    }
}
