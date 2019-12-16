using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Clinic;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using EntityData.His.Clinic;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;
using EntityData.His.CardClubManager;
using AutoServiceSDK.SdkData;
using AutoServiceSDK.ISdkService;
using AutoServiceManage.CardSaving;
using CardInterface;
using AutoServiceManage.Common;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.Register;

namespace AutoServiceManage.SendCard
{
    public partial class FrmReIssueCardInfo : Form
    {
        #region 变量
        private ISendCardInterFace SendCard = null;
        public IDCardInfo IdInfo { get; set; }
        private CardAuthorizationData eLCardAuthorizationData = new CardAuthorizationData();
        private DataTable dtCardInfo = new DataTable();
        #endregion

        #region 窗体初始化,Load

        public FrmReIssueCardInfo()
        {
            InitializeComponent();
        }

        private void FrmReIssueCardInfo_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
            if (AutoHostConfig.SendCardType == "XUHUI")
            {
                SendCard = new AutoServiceSDK.SdkService.SendCard_XH();
            }
            else if (AutoHostConfig.SendCardType == "XUHUI_PH")//省医院
            {
                SendCard = new AutoServiceSDK.SdkService.SendCardNew_XH();//射频卡
            }
            else if (AutoHostConfig.SendCardType == "XUHUI_XM")
            {
                SendCard = new AutoServiceSDK.SdkService.SendCardNew_XH();//射频卡
            }
            if (!BindPageList())
            {
                SkyComm.CloseWin(this);
            }
        }

        private void FrmReIssueCardInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
        }

        private bool BindPageList()
        {
            bool flag = true;
            try
            {
                CardAuthorizationFacade eFacade = new CardAuthorizationFacade();
                DataSet dsInfo = eFacade.GetCardInformationByIDCard(IdInfo.Number, IdInfo.Name);
                if (dsInfo != null && dsInfo.Tables.Count != 0 && dsInfo.Tables[0].Rows.Count != 0)
                {
                    dtCardInfo.Columns.Add("PITCHON1", typeof(System.Boolean)).DefaultValue = false;
                    dtCardInfo.Columns.Add("CARDID");
                    dtCardInfo.Columns.Add("ACCOUNT_ID");
                    dtCardInfo.Columns.Add("DIAGNOSEID");
                    dtCardInfo.Columns.Add("SENDCARDTYPE");
                    dtCardInfo.Columns.Add("PATIENTNAME");
                    dtCardInfo.Columns.Add("PROVIDECARDDATE");
                    dtCardInfo.Columns.Add("CARDSTATE");
                    dtCardInfo.Columns.Add("CIRCUIT_STATE");

                    foreach (DataRow drTemp in dsInfo.Tables[0].Rows)
                    {
                        DataRow drNew = dtCardInfo.NewRow();
                        drNew["CARDID"] = drTemp["CARDID"].ToString();
                        drNew["ACCOUNT_ID"] = drTemp["ACCOUNT_ID"].ToString();
                        drNew["DIAGNOSEID"] = drTemp["DIAGNOSEID"].ToString();
                        drNew["SENDCARDTYPE"] = drTemp["SENDCARDTYPE"].ToString();
                        drNew["PATIENTNAME"] = drTemp["PATIENTNAME"].ToString();
                        drNew["PROVIDECARDDATE"] = drTemp["PROVIDECARDDATE"].ToString();
                        drNew["CARDSTATE"] = drTemp["CARDSTATE"].ToString();
                        drNew["CIRCUIT_STATE"] = drTemp["CIRCUIT_STATE"].ToString();
                        dtCardInfo.Rows.Add(drNew);
                    }
                    this.gdcMain.DataSource = dtCardInfo;
                }
                else
                {
                    SkyComm.ShowMessageInfo("卡信息加载失败，未找到患者卡信息！");

                    flag = false;
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("卡信息加载失败，原因："+ex.Message);
                flag = false;
            }
            
            return flag;
        }
        #endregion

        #region 返回，退出


        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 选择就诊卡信息
        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            if (!checkBox_Click())
                return;
        }


        private void gdvMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (!checkBox_Click())
                return;
        }

        private bool checkBox_Click()
        {
            DataRow selectRow = this.gdvMain.GetDataRow(gdvMain.FocusedRowHandle);
            bool isSelected = Convert.ToBoolean(selectRow["PITCHON1"]);
            selectRow["PITCHON1"] = isSelected ? false : true;
            isSelected = isSelected ? false : true;

            DataTable dtTemp = (DataTable)this.gdcMain.DataSource;

            if (isSelected)
            {
                DataRow[] dsPitchon = dtTemp.Select("PITCHON1=true");
                if (dsPitchon.Length > 0)
                {
                    for (int i = 0; i < dsPitchon.Length; i++)
                    {
                        if (dsPitchon[i]["CARDID"].ToString() != selectRow["CARDID"].ToString())
                        {
                            dsPitchon[i]["PITCHON1"] = false;
                        }
                        else
                        {
                            dsPitchon[i]["PITCHON1"] = true;
                        }
                    }
                }
            }
            return true;
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

        #region 确认补卡
        private void lblOK_Click(object sender, EventArgs e)
        {
           
            ucTime1.Sec = 60;
            ucTime1.timer1.Stop();


           if (SendCard.CheckCard() == "1")
            {
                SkyComm.ShowMessageInfo("该自助机没有卡，请在其他自助机上进行补卡操作！");
                ucTime1.timer1.Start();
                return;
            }
            this.lblOK.Enabled = false;
            //this.btnReturn.Enabled = false;
            this.btnExit.Enabled = false;
            //DataSet dsNewInfo = new DataSet();
            int checkCount = 0;
            DataRow drCard =((DataView)this.gdvMain.DataSource).ToTable().NewRow();

            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if (Convert.ToBoolean(gdvMain.GetDataRow(i)["PITCHON1"].ToString ()))
                {
                    drCard = gdvMain.GetDataRow(i);
                    checkCount++;
                }
            }
            if (checkCount == 0)
            {
                SkyComm.ShowMessageInfo("请选择一条卡信息进行补卡操作！");
                ucTime1.timer1.Start();
                eLCardAuthorizationData = new CardAuthorizationData();
                this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
                return;
            }
            else if(checkCount>1)
            {
                SkyComm.ShowMessageInfo("只能选择一条卡信息进行补卡操作！");
                ucTime1.timer1.Start();
                eLCardAuthorizationData = new CardAuthorizationData();
                this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
                return;
            }

            if (SkyComm.eCardAuthorizationData != null && SkyComm.eCardAuthorizationData.Tables.Count != 0 && SkyComm.eCardAuthorizationData.Tables[0].Rows.Count != 0)
            {
                if (drCard["CARDID"].ToString() == SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString())
                {
                    SkyComm.ShowMessageInfo("不能对当前正在使用的就诊卡进行补卡操作！");
                    ucTime1.timer1.Start();
                    eLCardAuthorizationData = new CardAuthorizationData();
                    this.lblOK.Enabled = true;
                    //this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    return;
                }
            }

            FrmYesNoAlert frmAlert = new FrmYesNoAlert();
            frmAlert.Title = "提示";
            frmAlert.Msg = "是否确认对选择的卡信息进行补卡操作？         【卡号：" + drCard["CARDID"].ToString() + "】【姓名：" + drCard["PATIENTNAME"].ToString() + "】";
            if (frmAlert.ShowDialog() == DialogResult.Cancel)
            { 
                this.ucTime1.timer1.Start();
                eLCardAuthorizationData = new CardAuthorizationData();
                this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
                return;
            }
            CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();
            using (WaitDialogForm form = new WaitDialogForm("正在发卡中，请稍候...", "正在组织发卡数据,请稍候......", new Size(240, 60)))
            {
                #region 补卡

                eLCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.CancleAndReissueCard(drCard["ACCOUNT_ID"].ToString(), drCard["CARDID"].ToString (), SysOperatorInfo.OperatorID,drCard["CIRCUIT_STATE"].ToString ());
                //写卡，如果失败则重试
                bool isSuccess = false;
                form.Caption = "正在写卡中，请稍候...";
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        //第一次写卡
                        if (SendCard.WriteCard(eLCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString ()) == false)
                        {
                            //第一次写卡失败，再进行第二次写卡
                            if (SendCard.WriteCard(eLCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString()) == false)
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
                    //this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    this.lblOK.Enabled = true;
                }

                //写卡失败
                if (isSuccess == false)
                {
                    //撤消已注销挂失并补卡的信息
                    //eCardAuthorizationFacade.deleteEntityAndCardSaving(eLCardAuthorizationData);
                    eCardAuthorizationFacade.TranBackBussiness(drCard["ACCOUNT_ID"].ToString(), eLCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString(), drCard["CARDID"].ToString(),drCard["CIRCUIT_STATE"].ToString (), eLCardAuthorizationData);
                    SkynetMessage.MsgInfo("写卡失败，请在其他自助机上重试！");
                    this.lblOK.Enabled = true;
                    //this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;
                    this.ucTime1.timer1.Start();
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
                SkyComm.eCardAuthorizationData = eLCardAuthorizationData;//暂注释
                SkyComm.DiagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                SkyComm.cardInfoStruct.CardNo = eLCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                SkyComm.cardBlance = 0;
                CardSavingFacade cf = new CardSavingFacade();
                SkyComm.cardallmoney = 0;

                int Savingsucceed = 0;
                decimal RechargeMoney = 0;
                string strModeType = "现金";
                string projectType = SkyComm.getvalue("项目版本标识");
                if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
                {
                    //eLCardAuthorizationData.Tables[0].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_MODETYPE] = strModeType;
                    if (!eLCardAuthorizationData.Tables[0].Columns.Contains("OPERATORNAME"))
                    {
                        eLCardAuthorizationData.Tables[0].Columns.Add("OPERATORNAME");

                        eLCardAuthorizationData.Tables[0].Rows[0]["OPERATORNAME"] = SysOperatorInfo.OperatorName;
                    }
                    eCardAuthorizationFacade.updateEntity(eLCardAuthorizationData);
                    //eLCardAuthorizationData.Tables[0].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_ADDMONEY] = RechargeMoney;
                    Savingsucceed = 1;
                    //打印发卡凭证
                    PrintSendCardReport(eLCardAuthorizationData, drCard["PATIENTNAME"].ToString());
                }
                else
                {
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
                                case "XUHUIM1":
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
                            PrintSendCardReport(eLCardAuthorizationData, drCard["PATIENTNAME"].ToString());
                        }
                    }
                    catch (Exception ex2)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("补卡充值失败：" + ex2.Message);
                    }
                    finally
                    {
                        //SkyComm.cardInfoStruct = new CardInformationStruct();
                        //SkyComm.eCardAuthorizationData.Tables[0].Clear();
                        //SkyComm.DiagnoseID = string.Empty;
                        //SkyComm.cardBlance = 0;
                        //SkyComm.cardallmoney = 0;
                    }
                }
                if (Savingsucceed == 1)
                {
                    SendCard.OutputCard();

                    SkyComm.ShowMessageInfo("补卡成功，请取走您的就诊卡!");
                }
                else
                {
                    //撤消已注销挂失并补卡的信息
                    Skynet.LoggingService.LogService.GlobalInfoMessage("充值失败，撤消发卡信息");
                    //eCardAuthorizationFacade.deleteEntityAndCardSaving(eLCardAuthorizationData);
                    DataSet dsCardAuthorizationData = (DataSet)eLCardAuthorizationData;
                    eCardAuthorizationFacade.TranBackBussiness(drCard["ACCOUNT_ID"].ToString(), eLCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString(), drCard["CARDID"].ToString(), drCard["CIRCUIT_STATE"].ToString(), eLCardAuthorizationData);
                    eLCardAuthorizationData = new CardAuthorizationData();
                    this.lblOK.Enabled = true;
                    //this.btnReturn.Enabled = true;
                    this.btnExit.Enabled = true;

                    //发卡失败以后将卡进行回收
                    SendCard.ReturnCard();
                    ucTime1.timer1.Start();

                    return;
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("发卡失败：" + ex.Message + ",系统回收卡机中的卡");

                //发卡失败以后将卡进行回收
                SendCard.ReturnCard();

                SkynetMessage.MsgInfo("发卡失败：" + ex.Message);
                ucTime1.timer1.Start();

                return;
            }
            finally
            {
                SkyComm.cardInfoStruct = new CardInformationStruct();
                SkyComm.eCardAuthorizationData.Tables[0].Clear();
                SkyComm.DiagnoseID = string.Empty;
                SkyComm.cardBlance = 0;
                SkyComm.cardallmoney = 0;

                this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
            }
            #endregion

            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请在就诊卡出口取走您的就诊卡!");
            voice.EndJtts();
            ucTime1.timer1.Start();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void PrintSendCardReport(DataSet eLCardAuthorizationData,string patientName)
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
            print.AddParam("姓名", patientName);
            print.AddParam("卡押金", decYJ);
            print.AddParam("操作员", SysOperatorInfo.OperatorCode);
            print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
            print.AddData(eLCardAuthorizationData.Tables[0], "report");
            //PrintManager.CanDesign = true;
            print.Print();
            print.Dispose();
            Thread.Sleep(100);
        }

        #endregion

    }
}
