using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Charge;
using AutoServiceManage.Common;
using AutoServiceManage.InCard;
using AutoServiceManage.Inquire;
using AutoServiceManage.SendCard;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using CardInterface;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using AutoServiceSDK.SdkData;
using EntityData.His.Common;
using System.Collections;
using AutoServiceSDK.POSInterface;

namespace AutoServiceManage
{
    public partial class FrmMain : Form
    {
        #region 变量
        public static DataSet patientInfoData = new DataSet();
        public static IDCardInfo userInfo = new IDCardInfo();

        #endregion

        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock);                    

        #region 构造函数，load
         public FrmMain()
         {
             //LockWindowUpdate(this.Handle);
             InitializeComponent();
             //LockWindowUpdate(IntPtr.Zero);
             //this.SetStyle(ControlStyles.AllPaintingInWmPaint | //不擦除背景 ,减少闪烁
             //             ControlStyles.OptimizedDoubleBuffer | //双缓冲
             //             ControlStyles.UserPaint, //使用自定义的重绘事件,减少闪烁
             //             true);

             //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
             //this.SetStyle(ControlStyles.DoubleBuffer, true);
             //this.SetStyle(ControlStyles.UserPaint, true);
             //this.SetStyle(ControlStyles.ResizeRedraw, true);

             this.DoubleBuffered = true;//设置本窗体
             SetStyle(ControlStyles.UserPaint, true);
             SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
             SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
         }
        
        private void FrmMain_Load(object sender, EventArgs e)
        {           
            patientInfoData = new PatientInfoData();
            //SkyComm.Init();


            //POS签到
            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
            {
                backgroundWorker1.RunWorkerAsync();                
            }

            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType) || !string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                lblYuCun.Enabled = true;
            }
            else
            {
                lblYuCun.Enabled = false;
            }
            
        }

        #endregion

        #region 办卡
        private void lblBanKa_Click(object sender, EventArgs e)
        {
            //#region 读取身份证

            //FrmSendCardMain frm = new FrmSendCardMain();
            //IDCardInfo idinfo = null;
            //try
            //{
            //    if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //    {
            //        idinfo = frm.IdInfo;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Skynet.LoggingService.LogService.GlobalInfoMessage("读取身份证失败：" + ex.Message);
            //    return;
            //}
            //finally
            //{
            //    frm.Dispose();
            //}
            //#endregion

            //#region 输入手机号码
            //string TelePhone = string.Empty;
            //using (FrmSendCardInputTel frmTel = new FrmSendCardInputTel(idinfo))
            //{
            //    frmTel.IdInfo = idinfo;
            //    if (frmTel.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            //    {
            //        TelePhone = frmTel.TelePhone;
            //    }
            //    else
            //    {
            //        idinfo = null;
            //        return;
            //    }
            //}
            //#endregion
            //FrmSendCardMain frm = new FrmSendCardMain();
            FrmChooseSendCardType frm = new FrmChooseSendCardType();
            frm.ShowDialog(this);
            frm.Dispose();

        }
        #endregion

        #region 预存
        private void lblYuCun_Click(object sender, EventArgs e)
        {
           
            SkyComm.CardSavingType = 0;

            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType) && !string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                FrmCardSavingMain frm = new FrmCardSavingMain();
                frm.ShowDialog(this);
                frm.Dispose();
            }
            else if (!string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                #region 直接现金预存
                //现金预存前先刷卡
                //如果已经有卡号时，则表示已经读过卡，则不需要再重新读卡
                if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    int intResult = SkyComm.ReadCard("现金预存");
                    if (intResult == 0)
                        return;
                }

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
                frm.CallType = 0;
                if (frm.ShowDialog(this) == DialogResult.Cancel)
                {
                    frm.Dispose();
                    return;
                }
                frm.Dispose();

                #endregion
            }
            else if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
            {
                #region 直接银行预存
                //现金预存前先刷卡
                //如果已经有卡号时，则表示已经读过卡，则不需要再重新读卡
                if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    int intResult = SkyComm.ReadCard("银行预存");
                    if (intResult == 0)
                        return;
                }

                FrmCardSavingBank frm = new FrmCardSavingBank();
                frm.CallType = 0;
                if (frm.ShowDialog(this) == DialogResult.Cancel)
                {
                    frm.Dispose();
                    return;
                }
                frm.Dispose();

                #endregion
            }


            if (SkyComm.CardSavingType == 1)
            {
                lblQianDao_Click(null, null);
            }
            else if (SkyComm.CardSavingType == 2)
            {
                lblJiaoFei_Click(null, null);
            }
        }
        #endregion

        #region 预约
        private void lblYuYue_Click(object sender, EventArgs e)
        {
            FrmOfficeChoose frm = new FrmOfficeChoose();
            frm.ShowDialog(this);
            frm.Dispose();
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

        #region 签到

        private void lblQianDao_Click(object sender, EventArgs e)
        {
            //如果已经有卡号时，则表示已经读过卡，则不需要再重新读卡
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("挂号");
                if (intResult == 0)
                    return;
            }

            //this.AnsyWorker(ui =>
            {
                //根据是否有预约信息，如果有预约信息，弹出界面选择。
                double minutes = Convert.ToDouble(SystemInfo.SystemConfigs["预约挂号报到延时时间"].DefaultValue);
                BespeakRegisterFacade bespeakFacade = new BespeakRegisterFacade();
                CommonFacade commonFacade = new CommonFacade();
                DateTime ServerdateTime = commonFacade.GetServerDateTime();
                DataSet bespeakInfoData = bespeakFacade.FindCurrentBespeakByDiagnoseID(SkyComm.DiagnoseID, 1, ServerdateTime.AddMinutes(-minutes));
                bool IsQueryBespeakData = false;
                //ui.SynUpdateUI(() =>
                {
                    if (bespeakInfoData.Tables[0].Rows.Count == 0)
                    {
                        #region 没有预约信息时查询预约

                        SkyComm.ShowMessageInfo("没有查询到预约信息，如果已经预约请到相应窗口取号!");
                        return;                        
                        #endregion
                    }

                    //有预约信息时
                    if (bespeakInfoData == null || bespeakInfoData.Tables[0].Rows.Count > 0)
                    {
                        #region 有预约信息确认取号
                        //有预约信息,再判断预约信息是几条

                        FrmBespeakList frm = new FrmBespeakList();
                        try
                        {
                            frm.dsBespeak = bespeakInfoData;
                            frm.IsQueryBespeakData = IsQueryBespeakData;
                            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                            {
                                SkyComm.GetCardBalance();
                            }
                        }
                        catch (Exception ex)
                        {
                            Skynet.LoggingService.LogService.GlobalInfoMessage("就诊号：" + SkyComm.DiagnoseID + "取号失败：" + ex.Message);
                        }
                        finally
                        {
                            frm.Dispose();
                        }
                        #endregion
                    }
                }
                //);
            }
            //);
        }
        
        #endregion

        #region 缴费

        private void lblJiaoFei_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("缴费");
                if (intResult == 0)
                    return;
            }            
            FrmChargeMain frm = new FrmChargeMain();
            try
            {
                DataSet dsRecipe = GetRecipeInfo();
                if (dsRecipe == null)
                    return;

                frm.dsRecipe = dsRecipe;
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("缴费失败：" + ex.Message);
                SkyComm.ShowMessageInfo("调用自助缴费出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }

        public DataSet GetRecipeInfo()
        {
            //查询未缴费的处方信息，如果未缴费的处方没有的，则不需要缴费
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsRecipe = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID);
            if (dsRecipe.Tables[0].Rows.Count == 0)
            {
                SkyComm.ShowMessageInfo("没有需要缴费的处方信息！");
                return null;
            }

            dsRecipe.Tables[0].Columns.Add("PRICECHANGE", typeof(decimal));  //单价
            dsRecipe.Tables[0].Columns.Add("AMOUNTCHANGE", typeof(decimal));  //数量
            dsRecipe.Tables[0].Columns.Add("UNITCHANGE", typeof(string));  //单位
            dsRecipe.Tables[0].Columns.Add("PITCHON1", typeof(bool));  //单位

            //dsRecipe.Tables[0].Columns["PITCHON"].DataType = typeof(System.Boolean);
            UnitToPack unitToPack = new UnitToPack();
            foreach (DataRow item in dsRecipe.Tables[0].Rows)
            {
                item["PITCHON1"] = Convert.ToBoolean(item["PITCHON"]);
                if (item["OUTPATIENTUNIT"].ToString().Trim() == "包装")
                {
                    item.BeginEdit();
                    int amount = Convert.ToInt32(item["AMOUNT"]);
                    int changeratio = Convert.ToInt32(item["CHANGERATIO"]);
                    item["AMOUNTCHANGE"] = unitToPack.GetPackAmount(Convert.ToInt32(item["CHANGERATIO"]), Convert.ToInt32(item["AMOUNT"]));
                    item["UNITCHANGE"] = item["PACK"];
                    item["PRICECHANGE"] = Convert.ToDecimal(item["UNITPRICE"]) * changeratio;
                    item.EndEdit();
                }
                else
                {
                    item.BeginEdit();
                    item["PRICECHANGE"] = item["UNITPRICE"];
                    item["AMOUNTCHANGE"] = item["AMOUNT"];
                    item["UNITCHANGE"] = item["UNIT"];
                    item.EndEdit();
                }
            }
            return dsRecipe;
        }
        #endregion

        #region 打印
        private void LblDaYin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("打印");
                if (intResult == 0)
                    return;
            }
            AutoServiceManage.AutoPrint.FrmPrintMain frm = new AutoPrint.FrmPrintMain();
            try
            {                
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("自助打印失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助打印出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }
        #endregion

        #region 查询
        private void lblChaXun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("查询");
                if (intResult == 0)
                    return;
            }
            FrmInquireMain frm = new FrmInquireMain();
            try
            {
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("自助查询失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助查询出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }
        #endregion

        #region 退卡
        private void lblTuiKa_Click(object sender, EventArgs e)
        {
            SkyComm.cardInfoStruct = new CardInformationStruct();
            SkyComm.eCardAuthorizationData.Tables[0].Clear();
            SkyComm.DiagnoseID = string.Empty;
            SkyComm.cardBlance = 0;
            SkyComm.cardallmoney = 0;
            SkyComm.ExitCard();
            //this.Close();
        }
        #endregion
                
        #region 左边功能列表
        private void lblyygk_Click(object sender, EventArgs e)
        {
            showweb(1);
        }

        private void lbljzzn_Click(object sender, EventArgs e)
        {
            showweb(2);
        }

        private void lblzjjs_Click(object sender, EventArgs e)
        {
            showweb(3);
        }

        private void lblksjs_Click(object sender, EventArgs e)
        {
            showweb(4);
        }

        private void lblypjg_Click(object sender, EventArgs e)
        {
            showweb(5);
        }

        private void lblsfbz_Click(object sender, EventArgs e)
        {
            showweb(6);
        }

        private void showweb(int index)
        {
            FrmWebLoad frmWebLoad = new FrmWebLoad();
            switch (index)
            {
                case 1:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("yygk"));
                        break;
                    }
                case 2:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("jzzn"));
                        break;
                    }
                case 3:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("zjjs"));
                        break;
                    }
                case 4:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("ksjs"));
                        break;
                    }
                case 5:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("ypjg"));
                        break;
                    }               
                case 6:
                    {
                        frmWebLoad.webBrowser1.Url = new Uri(SkyComm.getvalue("sfbz"));
                        break;
                    }
            }
            //if (frmWebLoad.webBrowser1.Url.AbsoluteUri != string.Empty)
            //{
            frmWebLoad.ShowDialog();
            //}
        }

        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Hashtable BankTranht = new Hashtable();
            BankTranht.Add("POSNO", AutoHostConfig.PosNo);
            POSBase Posfac = IPOSFactory.CreateIPOS(AutoHostConfig.PosInterfaceType);
            try
            {
                Posfac.Trans("5", BankTranht);
                SkyComm.PosEnabled = true;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("POS签到失败：" + ex.Message);                
            }
        }               

        #region 公用方法

       

        #endregion
    }
}
