using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Charge;
using AutoServiceManage.Inquire;
using AutoServiceManage.SendCard;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.POSInterface;
using AutoServiceSDK.SdkData;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using CardInterface;
using EntityData.His.Common;
using Skynet.Framework.Common;
using AutoServiceManage.Evaluation;
using AutoServiceManage.AutoPrint;
using AutoServiceManage.BespeakRegister;
using AutoServiceManage.Common;
using Skynet.LoggingService;
using AutoServiceManage.Electronics;

namespace AutoServiceManage
{
    public class MainBase
    {
        #region 变量,构造函数
        public static DataSet patientInfoData = new DataSet();
        public static IDCardInfo userInfo = new IDCardInfo();
        BackgroundWorker backgroundWorker1 = null;

        public MainBase()
        {
            backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
        }
        #endregion

        #region 加载窗体

        public void Load()
        {
            patientInfoData = new PatientInfoData();
            //SkyComm.Init();

            //POS签到
            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType))
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        #endregion              

        #region 办卡

        /// <summary>
        /// 办卡
        /// </summary>
        /// <param name="owner"></param>
        public void SendCard(IWin32Window owner)
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
            //FrmSendCardMain frm = new FrmSendCardMain();
            FrmChooseSendCardType frm = new FrmChooseSendCardType();
            frm.ShowDialog(owner);
            frm.Dispose();
            ExitScanCard();
        }
        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="owner"></param>
        public void ReissueCard(IWin32Window owner)
        {
            //判断打印机是否有纸
            if (AutoHostConfig.ReadCardType == "XUHUI" || AutoHostConfig.ReadCardType == "XUHUIM1")
            {
                PrintManage_XH thePrintManage = new PrintManage_XH();
                string CheckInfo = thePrintManage.CheckPrintStatus();
                if (!string.IsNullOrEmpty(CheckInfo))
                {
                    SkyComm.ShowMessageInfo(CheckInfo);
                    return;
                }
            }
            FrmReIssueCard frm = new FrmReIssueCard();
            frm.ShowDialog(owner);
            frm.Dispose();
        }
        #endregion


        #region 打印检验报告
        public void PrintListReport(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("打印");
                if (intResult == 0)
                    return;
            }

            //AutoServiceManage.AutoPrint.FrmPrintMain frm = new AutoPrint.FrmPrintMain();
            Form frm = new Form();
            try
            {
                if (SkyComm.getvalue("LIS厂商类型").ToString() == "智方")
                {
                    frm = new FrmPrintListReport();
                    frm.ShowDialog(owner);
                }
                else if (SkyComm.getvalue("LIS厂商类型").ToString() == "杏和")
                {
                    frm = new FrmPrintLisReportXH();
                    frm.ShowDialog(owner);
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("自助打印失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助打印出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 预约
        public void BespeakRegister(IWin32Window owner)
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

            /*
             * 如果有预约挂号一级科室分类，则进入一级科室选择界面
             */
            if (string.IsNullOrEmpty(SkyComm.getvalue("预约挂号一级科室分类")))
            {
                FrmOfficeChoose frm = new FrmOfficeChoose();
                frm.ShowDialog(owner);
                frm.Dispose();
            }
            else
            {
                FrmLevelOneOfficeChooes frm = new FrmLevelOneOfficeChooes();
                frm.ShowDialog(owner);
                frm.Dispose();

            }
            ExitScanCard();

        }
        #endregion

        #region 充值

        public void AddMoney(IWin32Window owner)
        {
           
            SkyComm.CardSavingType = 0;

            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType) && !string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                FrmCardSavingMain frm = new FrmCardSavingMain();
                frm.serviceType = NetPayServiceTypeEnum.门诊预交金充值.ToString();
                frm.ShowDialog(owner);
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
                if (frm.ShowDialog(owner) == DialogResult.Cancel)
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
                if (frm.ShowDialog(owner) == DialogResult.Cancel)
                {
                    frm.Dispose();
                    return;
                }
                frm.Dispose();

                #endregion
            }

            if (SkyComm.CardSavingType == 1)
            {
                BespeakSignIn(owner);
            }
            else if (SkyComm.CardSavingType == 2)
            {
                Charge(owner);
            }
            ExitScanCard();
        }

        #endregion

        #region 住院预交金充值
        public void AddInHosMoney(IWin32Window owner)
        {
           
            if (!string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType) && !string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
            {
                //如果已经有卡号时，则表示已经读过卡，则不需要再重新读卡
                if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    int intResult = SkyComm.ReadCard("现金预存");
                    if (intResult == 0)
                        return;
                }

                FrmInhosAdvance frm = new FrmInhosAdvance();
                frm.ShowDialog(owner);
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
                    case "XUHUIM1":
                        AutoServiceSDK.SdkService.Common_XH camera = new AutoServiceSDK.SdkService.Common_XH();
                        camera.TakeCamera(SkyComm.cardInfoStruct.CardNo, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString(), AutoHostConfig.Machineno);
                        break;
                    default:
                        break;
                }

                FrmInHosSavingCash frm = new FrmInHosSavingCash();
                if (frm.ShowDialog(owner) == DialogResult.Cancel)
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

                FrmInHosSavingBank frm = new FrmInHosSavingBank();
                if (frm.ShowDialog(owner) == DialogResult.Cancel)
                {
                    frm.Dispose();
                    return;
                }
                frm.Dispose();

                #endregion
            }

            ExitScanCard();
        }
        #endregion

        #region 住院预交金充值记录查询
        public void InHosAdvanceRecord(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("住院预存明细查询");
                if (intResult == 0)
                    return;
            }
            FrmInHosAdvanceRecord frm = new FrmInHosAdvanceRecord();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用住院预存明细查询模块失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("调用住院预存明细查询模块失败!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 住院费用明细查询
        public void InHosCostRecord(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("住院费用查询");
                if (intResult == 0)
                    return;
            }
            FrmCostRecord_InHos frm = new FrmCostRecord_InHos();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用住院费用查询模块失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("调用住院费用查询模块失败!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 住院一日清单打印
        public void LeaveHosCostPrint(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("住院一日清单打印");
                if (intResult == 0)
                    return;
            }
            FrmInhosCostListPrint frm = new FrmInhosCostListPrint();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用住院一日清单打印模块失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("调用住院一日清单打印模块失败!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 门诊预存转住院预存
        public void MoneyTransfer(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("门诊预存转住院预存");
                if (intResult == 0)
                    return;
            }
            FrmMoneyTransfer frm = new FrmMoneyTransfer();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用门诊预存转住院预存模块失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("调用门诊预存转住院预存模块失败!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 取号
        public void BespeakSignIn(IWin32Window owner)
        {
            //如果已经有卡号时，则表示已经读过卡，则不需要再重新读卡
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("挂号");
                if (intResult == 0)
                    return;
            }

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

            ////根据是否有预约信息，如果有预约信息，弹出界面选择。
            //double minutes = Convert.ToDouble(SystemInfo.SystemConfigs["预约挂号报到延时时间"].DefaultValue);
            //BespeakRegisterFacade bespeakFacade = new BespeakRegisterFacade();
            //CommonFacade commonFacade = new CommonFacade();
            //DateTime ServerdateTime = commonFacade.GetServerDateTime();
            //DataSet bespeakInfoData = bespeakFacade.FindCurrentBespeakByDiagnoseID(SkyComm.DiagnoseID, 1, ServerdateTime.AddMinutes(-minutes));
            //bool IsQueryBespeakData = false;
            ////ui.SynUpdateUI(() =>
            //{
            //    if (bespeakInfoData.Tables[0].Rows.Count == 0)
            //    {
            //        #region 没有预约信息时查询预约

            //        SkyComm.ShowMessageInfo("没有查询到预约信息，如果已经预约请到相应窗口取号!");
            //        return;
            //        #endregion
            //    }

            //    //有预约信息时
            //    if (bespeakInfoData == null || bespeakInfoData.Tables[0].Rows.Count > 0)
            //    {
            //        #region 有预约信息确认取号
            //        //有预约信息,再判断预约信息是几条

            //        FrmBespeakList frm = new FrmBespeakList();
            //        try
            //        {
            //            frm.dsBespeak = bespeakInfoData;
            //            frm.IsQueryBespeakData = IsQueryBespeakData;
            //            if (frm.ShowDialog(owner) == System.Windows.Forms.DialogResult.OK)
            //            {
            //                SkyComm.GetCardBalance();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Skynet.LoggingService.LogService.GlobalInfoMessage("就诊号：" + SkyComm.DiagnoseID + "取号失败：" + ex.Message);
            //        }
            //        finally
            //        {
            //            frm.Dispose();
            //        }
            //        #endregion
            //    }
            //}

            ExitScanCard();
        }

        #endregion

        #region 缴费
        public void Charge(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("缴费");
                if (intResult == 0)
                    return;
            }

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

            FrmChargeMain frm = new FrmChargeMain();
            try
            {
                DataSet dsRecipe = GetRecipeInfo();
                if (dsRecipe == null)
                    return;

                frm.dsRecipe = dsRecipe;
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("缴费失败：" + ex.Message);
                SkyComm.ShowMessageInfo("调用自助缴费出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }

        public DataSet GetRecipeInfo()
        {
            //查询未缴费的处方信息，如果未缴费的处方没有的，则不需要缴费
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsRecipe = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID);

            string Offices = SkyComm.getvalue("使用自助缴费的执行科室ID");
            string[] arrOffice = Offices.Split(',');
            //if (!string.IsNullOrEmpty(Offices))
            //{
            //    foreach (DataRow row in dsRecipe.Tables[0].Rows)
            //    {
            //        if (row.RowState == DataRowState.Deleted)
            //            continue;
            //        if (!arrOffice.Contains(row["EXECOFFICEID"].ToString()))
            //        {
            //            row.Delete();
            //        }
            //    }
            //    dsRecipe.AcceptChanges();
            //}
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

        #region 自助查询
        public void AutoQuery(IWin32Window owner)
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
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("自助查询失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助查询出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 自助打印
        public void AutoPrint(IWin32Window owner)
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
                frm.ShowDialog(owner);

            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("自助打印失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助打印出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }





        }

        #endregion

        #region 医技预约
        /// <summary>
        /// 医技预约 wangchao add 2016-07-11
        /// </summary>
        /// <param name="owner"></param>
        public void MedicalReserve(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("医技预约");
                if (intResult == 0)
                    return;
            }
            FrmChooseReserve frm = new FrmChooseReserve();
            try
            {
                //if (SystemInfo.SystemConfigs["医技预约方式"].DefaultValue.ToString()=="1")
                if (SkyComm.getvalue("医技预约方式").ToString()=="1")
                {
                    WebForm webfrm = new WebForm();
                    webfrm.ShowDialog();
                }
                else//HIS
                {
                    frm.ShowDialog(owner);
                }
                
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("医技预约失败：" + ex.Message);
                SkyComm.ShowMessageInfo("调用医技预约出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
                ExitScanCard();
            }
        }
        #endregion

        #region 满意度调查

        public void SurveyInfo(IWin32Window owner)
        {
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                int intResult = SkyComm.ReadCard("满意度调查");
                if (intResult == 0)
                    return;
            }
            FrmEvaluation frm = new FrmEvaluation();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用满意度调查失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("调用满意度调查失败!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }

        #endregion

        #region 公用查询

        /// <summary>
        /// 左边列表查询
        /// </summary>
        /// <param name="index">1：医院概况</param>
        public void showweb(int index)
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

        #region 退卡

        public void ExitCard()
        {
            SkyComm.cardInfoStruct.CardNo = "";
            //电子卡退卡不调用退卡接口 wangchenyang case 31245 自助机健康卡改造
            var _isVirtualcard = SkyComm.cardInfoStruct.isVirtualcard;
            //身份证退卡不调用退卡接口  chenqinag Case:31784   
            var _isIdentityCard = SkyComm.cardInfoStruct.isIdentityCard;
            SkyComm.cardInfoStruct = new CardInformationStruct();
            SkyComm.eCardAuthorizationData.Tables[0].Clear();
            SkyComm.DiagnoseID = string.Empty;
            SkyComm.cardBlance = 0;
            SkyComm.cardallmoney = 0;
            if (!_isVirtualcard && !_isIdentityCard) SkyComm.ExitCard();
        }
        #endregion

        #region 后台线程
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

        #endregion


        #region 点餐

        public void foodOrder(IWin32Window owner)
        {
            
            FrmFood frm = new FrmFood();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                LogService.GlobalInfoMessage("自助查询失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助查询出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }

        #endregion

        public void hosGps(IWin32Window owner)
        {
            
            FrmHosGps frm = new FrmHosGps();
            try
            {
                frm.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                LogService.GlobalInfoMessage("自助查询失败：" + ex.Message);
                SkyComm.ShowMessageInfo("自助查询出错!" + ex.Message);
            }
            finally
            {
                frm.Dispose();
            }
        }

        #region 电子健康卡

        public void Electronics(IWin32Window owner)
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
            FormElectronicsReadIdCard form = new FormElectronicsReadIdCard();
            form.SendCardType = "成人";
            form.ShowDialog(owner);
            form.Dispose();
           
        }

        #endregion


        public void ExitScanCard()
        {
            if (SkyComm.SCANCARD)
            {
                SkyComm.cardInfoStruct.CardNo = "";
                //电子卡退卡不调用退卡接口 wangchenyang case 31245 自助机健康卡改造
                var _isVirtualcard = SkyComm.cardInfoStruct.isVirtualcard;
                //身份证退卡不调用退卡接口  chenqinag Case:31784   
                var _isIdentityCard = SkyComm.cardInfoStruct.isIdentityCard;
                SkyComm.cardInfoStruct = new CardInformationStruct();
                SkyComm.eCardAuthorizationData.Tables[0].Clear();
                SkyComm.DiagnoseID = string.Empty;
                SkyComm.cardBlance = 0;
                SkyComm.cardallmoney = 0;
                if (!_isVirtualcard && !_isIdentityCard) SkyComm.ExitCard();

            }
            
        }







    }
}
