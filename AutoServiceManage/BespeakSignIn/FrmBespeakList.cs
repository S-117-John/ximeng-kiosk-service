using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Common;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using EntityData.His.Register;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;

namespace AutoServiceManage
{
    public partial class FrmBespeakList : Form
    {
        #region 定义变量
        /// <summary>
        /// 预约挂号数据
        /// </summary>
        public DataSet dsBespeak { get; set; }

        /// <summary>
        /// 是否通过查询检索到的预约信息
        /// </summary>
        public bool IsQueryBespeakData { set; get; }

        #endregion

        #region 构造函数，LOAD,关闭按钮事件

        public FrmBespeakList()
        {
            InitializeComponent();
        }

        private void FrmBespeakList_Load(object sender, EventArgs e)
        {
            //setLable();

            decimal decTotalMoney = 0;
            foreach(DataRow Row in dsBespeak.Tables[0].Rows)
            {
                decTotalMoney += Skynet.Framework.Common.DecimalRound.Round(Convert.ToDecimal(Row["ALLCOST"]),2);
            }

            if (!string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                btnAddMoney.Enabled = true;
            }
            else
            {
                btnAddMoney.Enabled = false;
            }
            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
            {
                lblAddMoneyBank.Enabled = true;
            }
            else
            {
                lblAddMoneyBank.Enabled = false;
            }

            ucSelectList.SetDataSource(dsBespeak);
            ucSelectList.totalMoney = decTotalMoney;
            this.lblTotalMoney.Text = decTotalMoney.ToString();
            lblYE.Text = SkyComm.cardBlance.ToString();
            if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count > 0)
                this.label6.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
            else
                this.label6.Text = string.Empty;
            ucSelectList.moneyChanged += ucSelectList_moneyChanged;           
            ucTime1.timer1.Start();

            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
            {
                lblAddMoneyBank.Visible = true;
            }
            else
            {
                lblAddMoneyBank.Visible = false;
            }

        }

        void ucSelectList_moneyChanged(decimal totalMoney)
        {
            this.lblTotalMoney.Text = totalMoney.ToString();
            decimal decTotal = Convert.ToDecimal(lblTotalMoney.Text);
            this.ucTime1.Sec = 60;
            if (decTotal > SkyComm.cardBlance)
            {
                lblOK.Visible = false;
            }
            else
            {
                lblOK.Visible = true;
            }
        }

        private void FrmBespeakList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        #endregion
               
        #region 签到确认
        private void lblOK_Click(object sender, EventArgs e)
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
                        if (frmCheck.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
            }
            catch
            { }
            #endregion

            this.AnsyWorker(ui =>
            {
                ui.UpdateTitle("取号确认中，请稍等...");
                
                ui.SynUpdateUI(() =>
                {
                    decimal decYE = Convert.ToDecimal(lblYE.Text);
                    decimal decTotal = Convert.ToDecimal(lblTotalMoney.Text);
                    decimal decTotalMoney = 0;
                    ucTime1.timer1.Stop();

                    string strMessage = string.Empty;
                    int intRowChange = 0;
                    foreach (DataRow Row in dsBespeak.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(Row["SELECT"]) == true)
                        {
                            decTotalMoney += DecimalRound.Round(Convert.ToDecimal(Row["ALLCOST"]), 2);
                        }
                        if (IsQueryBespeakData == true && Row["DIAGNOSEID"].ToString() != SkyComm.DiagnoseID)
                        {
                            intRowChange++;
                            strMessage = Row["OFFICE"].ToString() + "|" + Row["USERNAME"].ToString() + "  第" + Row["QUEUEID"].ToString() + "号" + "\r\n";
                        }
                    }

                    if (!string.IsNullOrEmpty(strMessage))
                    {
                        if (intRowChange < 3)
                            strMessage += "\r\n";
                        strMessage = strMessage + "是否要绑定到当前就诊卡?";
                        MyAlert myalert = new MyAlert(AlertTypeenum.确认取消, strMessage, "预约信息绑卡确认", 30);
                        if (myalert.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            DataSet dsChagnge = dsBespeak.Clone();
                            foreach (DataRow Row in dsBespeak.Tables[0].Rows)
                            {
                                if (Convert.ToBoolean(Row["SELECT"]) == true && Row["DIAGNOSEID"].ToString() != SkyComm.DiagnoseID)
                                {
                                    Row["DIAGNOSEID"] = SkyComm.DiagnoseID;
                                    dsChagnge.Tables[0].ImportRow(Row);
                                }
                            }

                            if (dsChagnge.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    BespeakRegisterFacade bespeakRegisterFacade = new BespeakRegisterFacade();
                                    dsBespeak.Tables[0].TableName = "T_BESPEAK_REGISTER";
                                    bespeakRegisterFacade.updateDiagnoseID(dsChagnge);
                                }
                                catch (Exception ex)
                                {
                                    Skynet.LoggingService.LogService.GlobalInfoMessage("绑卡失败：" + ex.Message);
                                    SkyComm.ShowMessageInfo("绑卡失败：" + ex.Message);
                                    ucTime1.Sec = 60;
                                    ucTime1.timer1.Start();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                    if (decTotal != decTotalMoney)
                    {
                        SkyComm.ShowMessageInfo("请检查您选择的预约记录是否正确！");
                        ucTime1.Sec = 60;
                        ucTime1.timer1.Start();
                        return;
                    }

                    if (decTotal > decYE)
                    {
                        SkyComm.ShowMessageInfo("您的健康卡余额不足，不能进行取号，请在自助预存后再进行确认操作！");
                        ucTime1.Sec = 60;
                        ucTime1.timer1.Start();
                        return;
                    }
                    ucTime1.timer1.Stop();

                    //构造挂号的数据
                    CommonFacade commonFacade = new CommonFacade();
                    DateTime ServerdateTime = commonFacade.GetServerDateTime();
                    double minutes = Convert.ToDouble(SystemInfo.SystemConfigs["预约挂号报到延时时间"].DefaultValue);

                    RegisterInfoData registerInfoData = new RegisterInfoData();
                    int registerID = 0;
                    foreach (DataRow Row in dsBespeak.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(Row["SELECT"]) == true)
                        {
                            if (Convert.ToDateTime(Row["BESPEAKDATE"]).AddMinutes(minutes) < ServerdateTime)
                            {
                                MyAlert frm = new MyAlert(AlertTypeenum.信息, "此预约号的预约时间为：" + Row["BESPEAKDATE"].ToString() + "，预约时间已过，不能使用！");
                                frm.ShowDialog();
                                frm.Dispose();
                                registerInfoData = new RegisterInfoData();
                                ucTime1.Sec = 60;
                                ucTime1.timer1.Start();
                                return;
                            }

                            registerID++;
                            DataRow Newrow = registerInfoData.Tables[0].NewRow();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_REGISTERID] = "新增"+registerID.ToString ();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_CANCELMARK] = "0";
                            Newrow[RegisterInfoData.T_REGISTER_INFO_OPERATORID] = SysOperatorInfo.OperatorID;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_OPERATEDATE] = ServerdateTime;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_CHARGEMARK] = 0;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_PATIENTNAME] = Row["PATIENTNAME"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_SEX] = Row["SEX"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_REGISTEROFFICEID] = Row["BESPEAKOFFICEID"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_OFFICE] = Row["OFFICE"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_REGISTERCLASS] = Row["REGISTERCLASS"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_DOCTORID] = Row["BESPEAKDOCTORID"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_USERNAME] = Row["USERNAME"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_CASECOST] = Row["MEDICALRECORDFEE"];   //工本费
                            Newrow[RegisterInfoData.T_REGISTER_INFO_BESPEAKMONEY] = Row["BESPEAKMONEY"]; //预约费
                            Newrow[RegisterInfoData.T_REGISTER_INFO_REGISTERFEE] = Row["REGISTERFEE"]; //挂号费
                            Newrow[RegisterInfoData.T_REGISTER_INFO_EXAMINEMONEY] = Row["EXAMINEMONEY"]; //诊金
                            Newrow[RegisterInfoData.T_REGISTER_INFO_BESPEAKID] = Row["BESPEAKID"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_DIAGNOSEID] = Row["DIAGNOSEID"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_ADDRESS] = Row["ADDRESS"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_TELEPHONE] = Row["TELEPHONE"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_OFFICEADDRESS] = Row["OFFICEADDRESS"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_DIAGNOSESTATE] = 0;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_WORKTYPE] = Row["WORKTYPE"].ToString();//班次
                            Newrow[RegisterInfoData.T_REGISTER_INFO_BIRTHDAY] = Convert.ToDateTime(Row["BIRTHDAY"].ToString()).Date;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_EXECDATE] = ServerdateTime;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_BALANCEMARK] = "2";
                            Newrow[RegisterInfoData.T_REGISTER_INFO_CARDID] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                            Newrow["BALANCEMODE"] = "预交金";//结算方式
                            Newrow[RegisterInfoData.T_REGISTER_INFO_BALANCESTATE] = 0;
                            Newrow[RegisterInfoData.T_REGISTER_INFO_CHECKFEE] = Row["PLUSFEE"];
                            //row[RegisterInfoData.T_REGISTER_INFO_BALANCEOPERATOR] = operatorId;
                            //row[RegisterInfoData.T_REGISTER_INFO_BALANCEDATE] = dateTime;

                            //数据传入方式0：门急诊挂号，1:专家挂号，2:门诊医生站读卡挂号，4：医生站急诊划价收费，5：储值卡挂号，6：银医服务，7,自助挂号
                            Newrow[RegisterInfoData.T_REGISTER_INFO_DATAINPUTTYPE] = 7;//结算状态
                            Newrow[RegisterInfoData.T_REGISTER_INFO_ARRANAGERECORDID] = Row["ARRANAGERECORDID"].ToString();
                            Newrow[RegisterInfoData.T_REGISTER_INFO_QUEUEID] = Row["QUEUEID"].ToString();

                 
                            registerInfoData.Tables[0].Rows.Add(Newrow);
                        }
                    }

                    RegisterInfoFacade registerFacade = new RegisterInfoFacade();
                    try
                    {
                        DataSet resultDs = registerFacade.insertEntityZj(registerInfoData);

                        SkyComm.GetCardBalance();

                        if (resultDs.Tables[0].Columns.Contains("EXAMINENAME") ==false)
                        {
                            DataColumn col = new DataColumn("EXAMINENAME", typeof(System.String));
                            col.Caption = "EXAMINENAME";
                            resultDs.Tables[0].Columns.Add(col);
                        }

                        // douyaming 2013-6-21 挂号发票输出 诊室位置
                        if (!resultDs.Tables[0].Columns.Contains("EXAMINELOC"))
                        {
                            resultDs.Tables[0].Columns.Add("EXAMINELOC");
                        }

                        UsersFacade uf = new UsersFacade();
                        BespeakRegisterFacade bespeakRegisterFacade = new BespeakRegisterFacade();

                        foreach (DataRow row in resultDs.Tables[0].Rows)
                        {
                            //13470 在挂号发票中输出诊室
                            string keyval = row["DOCTORID"].ToString();
                            if (keyval != "-1")
                                row["EXAMINENAME"] = uf.FindByUserInfo(keyval).Tables[0].Rows[0]["EXAMINENAME"].ToString();

                            //douyaming 2013-5-28 挂号发票输出 诊室位置
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
                        foreach (DataRow dr in resultDs.Tables[0].Rows)
                        {
                            //打印挂号凭证
                            DataTable dtPrint = resultDs.Tables[0].Clone();
                            dtPrint.ImportRow(dr);
                            DataSet dsPrint = new DataSet();
                            dsPrint.Tables.Add(dtPrint.Copy());
                            PrintReport(dsPrint);
                        }

                        try
                        {
                            Hashtable htPara = new Hashtable();
                            htPara.Add("@预约时间", Convert.ToDateTime(resultDs.Tables[0].Rows[0]["BESPEAKDATE"]).ToString("MM月dd日HH时mm分"));
                            string OfficeAddress = SysOperatorInfo.OperatorAreaname + resultDs.Tables[0].Rows[0]["OFFICEADDRESS"].ToString();
                            htPara.Add("@科室位置", OfficeAddress);
                            htPara.Add("@科室", resultDs.Tables[0].Rows[0][RegisterInfoData.T_REGISTER_INFO_OFFICE].ToString());
                            htPara.Add("@医生", resultDs.Tables[0].Rows[0][RegisterInfoData.T_REGISTER_INFO_USERNAME].ToString());
                            htPara.Add("@排队号", resultDs.Tables[0].Rows[0]["QUEUEID"].ToString());
                            htPara.Add("@预约号", resultDs.Tables[0].Rows[0]["BESPEAKID"].ToString());
                            htPara.Add("@诊室位置", resultDs.Tables[0].Rows[0]["EXAMINELOC"].ToString());
                            htPara.Add("@诊室名称", resultDs.Tables[0].Rows[0]["EXAMINENAME"].ToString());
                            UMSMsgLib.UMSMsg.Instance.SendMsg(resultDs.Tables[0].Rows[0]["TELEPHONE"].ToString(), "取号", htPara);
                        }
                        catch (Exception ex)
                        {
                            Skynet.LoggingService.LogService.GlobalInfoMessage("取号成功后，发短信失败:" + ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage(SkyComm.DiagnoseID + "取号[" + registerInfoData.Tables[0].Rows.Count + "]条记录,失败：" + ex.Message);
                        SkyComm.ShowMessageInfo("取号失败：" + ex.Message);

                        ucTime1.Sec = 60;
                        ucTime1.timer1.Start();

                        return;
                    }
                    SkyComm.ShowMessageInfo("取号成功，请提前15分钟到达分诊大厅进行等候！");
                    ucTime1.timer1.Stop();
                    //后台直接进行处理挂号操作
                    DialogResult = System.Windows.Forms.DialogResult.OK;  
                });
            });

            //DialogResult = System.Windows.Forms.DialogResult.OK;  
            
        }

        #endregion

        #region 预存

        //现金预存
        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            //判断打印机是否有纸
            if (AutoHostConfig.ReadCardType == "XUHUI")
            {
                PrintManage_XH thePrintManage = new PrintManage_XH();
                string CheckInfo = thePrintManage.CheckPrintStatus();
                if (!string.IsNullOrEmpty(CheckInfo))
                {
                    SkyComm.ShowMessageInfo(CheckInfo);
                    return;
                }
            }
            this.ucTime1.timer1.Stop();
            FrmCardSavingCash frm = new FrmCardSavingCash();
            frm.CallType = 1;
            frm.ShowDialog(this);
            frm.Dispose();
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();

            lblYE.Text = SkyComm.cardBlance.ToString();
            decimal decTotal = Convert.ToDecimal(lblTotalMoney.Text);
            
            if (decTotal > SkyComm.cardBlance)
            {
                lblOK.Visible = false;
            }
            else
            {
                lblOK.Visible = true;
            }
            



            //this.AnsyWorker(ui => {
            //    ui.UpdateTitle("业务处理中...");
            //    intaa++;
            //    for (int i = 0; i < 900000000; i++)
            //    {

            //    }
            //    ui.SynUpdateUI(() =>
            //    {
            //        ucTime1.timer1.Stop();
            //        this.btnAddMoney.Text = intaa.ToString() + ",时间" + DateTime.Now.ToString();
            //        //MessageBox.Show ("自助预存:" + intaa +",时间"+ DateTime.Now.ToString());
            //        ucTime1.timer1.Start();
            //    }
            //    );
            //});

                       
        }

        //银行卡预存
        private void lblAddMoneyBank_Click(object sender, EventArgs e)
        {
            //判断打印机是否有纸
            if (AutoHostConfig.ReadCardType == "XUHUI")
            {
                PrintManage_XH thePrintManage = new PrintManage_XH();
                string CheckInfo = thePrintManage.CheckPrintStatus();
                if (!string.IsNullOrEmpty(CheckInfo))
                {
                    SkyComm.ShowMessageInfo(CheckInfo);
                    return;
                }
            }

            this.ucTime1.timer1.Stop();
            FrmCardSavingBank frm = new FrmCardSavingBank();
            frm.CallType = 1;
            frm.ShowDialog(this);
            frm.Dispose();
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();

            lblYE.Text = SkyComm.cardBlance.ToString();
            decimal decTotal = Convert.ToDecimal(lblTotalMoney.Text);

            if (decTotal > SkyComm.cardBlance)
            {
                lblOK.Visible = false;
            }
            else
            {
                lblOK.Visible = true;
            }
        }
        #endregion

        #region 返回，退出

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

        #region 打印挂号凭证
        /// <summary>
        /// 打印自助挂号
        /// </summary>
        private void PrintReport(DataSet ds)
        {
            ds.WriteXml(Application.StartupPath + @"\\ReportXml\\自助挂号" + ds.Tables[0].Rows[0]["REGISTERID"].ToString() + ".xml");
            string path = Application.StartupPath + @"\\Reports\\自助挂号.frx";

            if (System.IO.File.Exists(path) == false)
            {
                SkynetMessage.MsgInfo("自助挂号票据不存在,请联系管理员!");
                return;
            }
            //Common_XH theCamera_XH = new Common_XH();
            //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
            PrintManager print = new PrintManager();
            print.InitReport("自助挂号");
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


        //private void setLable()
        //{
        //    string msg = SkyComm.getvalue("挂号提示");

        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        return;
        //    }

        //    this.label2.Text = msg;
        //}

    }
}
