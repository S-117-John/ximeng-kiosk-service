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
using Skynet.Pass;
using SystemFramework.SyncLoading;
using AutoServiceManage.CardSaving;
using TiuWeb.ReportBase;
using AutoServiceSDK.SdkService;
using System.Collections;
using EntityData.BankHisExchange;
using System.Web.Script.Serialization;
using Skynet.LoggingService;

namespace AutoServiceManage.Charge
{
    public partial class FrmChargeMain : Form
    {
        #region 变量
        public DataSet dsRecipe { get; set; }
        #endregion

        #region 窗体初始化,Load

        public FrmChargeMain()
        {
            InitializeComponent();

        }

        private void FrmChargeMain_Load(object sender, EventArgs e)
        {
            lblYE.Text = SkyComm.cardBlance.ToString();
            this.gdcMain.DataSource = dsRecipe.Tables[0].DefaultView;

            decimal sumMoney = ReturnTotalMoney();
            lblTotalMoney.Text = string.Format("{0:0.00}", sumMoney);

            this.lblxm.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
            this.lblxb.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
            this.lblnl.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString();

            ucTime1.timer1.Start();

        }

        private void FrmChargeMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
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

        #region 选择处方信息
        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            if (gdvMain.SelectedRowsCount > 0)
            {
                DataRow drTemp = gdvMain.GetDataRow(gdvMain.FocusedRowHandle);
                string strClinicRecipeID = drTemp["CLINICRECIPEID"].ToString();
                if (Convert.ToBoolean(drTemp["PITCHON1"]) == true)
                {
                    drTemp["PITCHON1"] = false;
                    foreach (DataRow Row in dsRecipe.Tables[0].Select("CLINICRECIPEID = '" + strClinicRecipeID + "'"))
                    {
                        Row["PITCHON1"] = false;
                    }
                }
                else
                {
                    drTemp["PITCHON1"] = true;
                    foreach (DataRow Row in dsRecipe.Tables[0].Select("CLINICRECIPEID = '" + strClinicRecipeID + "'"))
                    {
                        Row["PITCHON1"] = true;
                    }
                }

            }
            refreshUI();
        }


        private decimal ReturnTotalMoney()
        {
            DataTable dtSelect = dsRecipe.Tables[0].DefaultView.ToTable();
            if (0 == dtSelect.Select("PITCHON1 = true").Length)
            {
                return 0;
            }

            decimal sumMoney = DecimalRound.Round(Convert.ToDecimal(dtSelect.Compute("SUM(TOTALMONEY)", "PITCHON1 = true")), 2);

            switch (SystemInfo.SystemConfigs["门诊收费时分币处理方式"].DefaultValue)
            {
                //0.不处理;1.四舍五入;2.见分进位;
                case "0":
                    sumMoney = DecimalRound.Round(sumMoney, 2);
                    break;
                case "1":
                    sumMoney = DecimalRound.Round(sumMoney, 1);
                    break;
                case "2":
                    sumMoney = DecimalRound.Round(sumMoney + Convert.ToDecimal(0.04), 1);
                    break;
            }

            return sumMoney;
        }

        private void gdvMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gdvMain.SelectedRowsCount > 0)
            {
                DataRow drTemp = gdvMain.GetDataRow(gdvMain.FocusedRowHandle);
                string strClinicRecipeID = drTemp["CLINICRECIPEID"].ToString();
                if (Convert.ToBoolean(drTemp["PITCHON1"]) == true)
                {
                    drTemp["PITCHON1"] = false;
                    foreach (DataRow Row in dsRecipe.Tables[0].Select("CLINICRECIPEID = '" + strClinicRecipeID + "'"))
                    {
                        Row["PITCHON1"] = false;
                    }
                }
                else
                {
                    drTemp["PITCHON1"] = true;
                    foreach (DataRow Row in dsRecipe.Tables[0].Select("CLINICRECIPEID = '" + strClinicRecipeID + "'"))
                    {
                        Row["PITCHON1"] = true;
                    }
                }

            }
            refreshUI();
        }

        #endregion

        #region 缴费
        private void lblOK_Click(object sender, EventArgs e)
        {
            this.AnsyWorker(ui =>
            {
                ui.UpdateTitle("正在缴费中，请稍等...");

                ui.SynUpdateUI(() =>
                {

                    #region 验证缴费处方信息

                    DataTable dtMain = dsRecipe.Tables[0].DefaultView.ToTable();

                    IEnumerable<string> _CLINICRECIPEIDs = dtMain.AsEnumerable().Where(b => b.Field<bool>("PITCHON1") == true).Select(a => a.Field<string>("CLINICRECIPEID")).Distinct();
                    if (_CLINICRECIPEIDs.Count() == 0)
                    {
                        SkyComm.ShowMessageInfo("请选择要缴费的处方！");
                        return;
                    }
                    this.ucTime1.timer1.Stop();
                    //选中处方的金额
                    decimal sumMoney = DecimalRound.Round(Convert.ToDecimal(dtMain.Compute("SUM(TOTALMONEY)", "PITCHON1 = true")), 2);

                    ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
                    DataSet dsTemp = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID, string.Empty, "3");

                    DataSet ds = dsTemp.Clone();
                    foreach (DataRow row in dsTemp.Tables[0].Rows)
                    {
                        if (_CLINICRECIPEIDs.Contains(row["CLINICRECIPEID"].ToString()))
                        {
                            ds.Tables[0].ImportRow(row);
                        }
                    }
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        SkyComm.ShowMessageInfo("没有找到可进行缴费的处方记录，请检查处方号");
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }
                    //调用大通审方系统
                    if (!CheckDTPass(ds))
                    {
                        return;
                    }
                    #endregion

                    #region 组织缴费处方信息
                    CommonFacade commonFacade = new CommonFacade();
                    DateTime ServerTime = commonFacade.GetServerDateTime();

                    LeechdomCharge charge = new LeechdomCharge(SkyComm.DiagnoseID, SysOperatorInfo.OperatorID);
                    charge.AddRecipeCharge(ds);

                    decimal decHisMoney = DecimalRound.Round(Convert.ToDecimal(charge.detailAccountData.Tables[0].Compute("SUM(MONEY)", "")), 2);

                    if (decHisMoney != sumMoney)
                    {
                        charge.detailAccountData.Clear();
                        charge.ecipeMedicineData.Clear();
                        SkyComm.ShowMessageInfo("选择的处方金额不正确，请在门诊窗口进行缴费！");
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }

                    if (decHisMoney > SkyComm.cardBlance)
                    {
                        charge.detailAccountData.Clear();
                        charge.ecipeMedicineData.Clear();
                        SkyComm.ShowMessageInfo("余额不足，请先进行自助预存，再进行缴费！");
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }
                    #endregion

                    #region 缴费
                    try
                    {
                        //验证西北妇幼高值耗材
                        if (!CheckHValueMaterial(charge.detailAccountData))
                            throw new Exception("缴费信息中有高值耗材，请在门诊窗口进行缴费！");

                        DetailAccountFacade detailAccountFacade = new DetailAccountFacade();
                        DataSet dads = detailAccountFacade.insertEntityNoInvoice(charge.detailAccountData, ref charge.ecipeMedicineData);
                        SkyComm.GetCardBalance();
                        SkyComm.ShowMessageInfo("缴费成功!");

                        //根据“药房自动配药接口类型”配置为2时，调用西北妇幼的派昂接口 19797 
                        string AutoDoseConfig = SystemInfo.SystemConfigs["药房自动配药接口类型"].DefaultValue;
                        string strCK = string.Empty;
                        if (AutoDoseConfig == "2" && charge.ecipeMedicineData.Tables[0].Rows.Count > 0)
                        {
                            strCK = UploadAngPaiData(dads, charge.ecipeMedicineData, dsTemp);
                        }

                        PrintReport(dads, strCK, dsRecipe);//打印交费凭证

                    }
                    catch (Exception err)
                    {
                        charge.detailAccountData.Clear();
                        charge.ecipeMedicineData.Clear();
                        Skynet.LoggingService.LogService.GlobalInfoMessage(SkyComm.DiagnoseID + "缴费失败：" + err.Message);
                        SkyComm.ShowMessageInfo("缴费失败：" + err.Message);
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }

                    dsRecipe = GetRecipeInfo();
                    if (dsRecipe == null || dsRecipe.Tables[0].Rows.Count == 0)
                    {
                        SkyComm.CloseWin(this);
                    }
                    else
                    {
                        this.gdcMain.DataSource = dsRecipe.Tables[0].DefaultView;

                        lblYE.Text = SkyComm.cardBlance.ToString();

                        refreshUI();
                    }
                    #endregion

                });
            });
        }

        #region 大通合理用药 wangchao 2016.09.29 25759
        /// <summary>
        /// 大通PASS系统适合处方方法
        /// </summary>
        /// <param name="dsCheck"></param>
        private bool CheckDTPass(DataSet dsCheck)
        {
            string configs = SystemInfo.SystemConfigs["门诊系统是否启用合理用药"].DefaultValue;
            bool returnFlag = true;
            if (configs == "3")
            {
                try
                {
                    string checkStr = string.Empty;
                    foreach (DataRow dr in dsCheck.Tables[0].Rows)
                    {
                        if (checkStr == dr["REGISTERID"].ToString() + dr["CLINICRECIPEID"].ToString())
                            continue;
                        if (dr["RECIPETYPE"].ToString() == "药品费" || dr["RECIPETYPE"].ToString() == "中草药")
                        {
                            string basexml = PassXmlCreate(dr["DOCTORID"].ToString(), dr["REGISTEROFFICEID"].ToString());
                            string detailxml = "<details_xml> " +
                                  "<hosp_flag>op</hosp_flag> " +
                                  "<treat_code>" + dr["DIAGNOSEID"].ToString() + "_" + dr["REGISTERID"].ToString() + "</treat_code> " +
                                  "<prescription_id>" + dr["CLINICRECIPEID"].ToString() + "</prescription_id> " +
                                  "</details_xml>";

                            int i = PassFunctionForDTCRMs.GetPrescriptionResult(basexml, detailxml);
                            checkStr = dr["REGISTERID"].ToString() + dr["CLINICRECIPEID"].ToString();
                            if (i == 1 || i == 2)
                            {
                                if (i == 1)
                                {
                                    SkyComm.ShowMessageInfo("药品处方正在审核中，审核通过后方能进行缴费！");

                                }
                                else
                                {
                                    SkyComm.ShowMessageInfo("药品处方存在问题，审核未通过，不能进行缴费！");
                                }
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ee)
                {
                    Skynet.LoggingService.LogService.GlobalFatalMessage(ee.Message);
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 构造大通合理用药入参
        /// </summary>
        /// <returns></returns>
        private string PassXmlCreate(string doctorID, string OfficeID)
        {
            ETechnicalpostFacade eTechnicalpostFacade = new ETechnicalpostFacade();
            DataSet dsRole = eTechnicalpostFacade.GetTeachInfo(doctorID);
            DataSet dsDoctor = new OperatorFacade().FindOperatorInfobyUserID(doctorID);
            string office = new OfficeFacade().FindAllNameByOfficeID(OfficeID).Tables[0].Rows[0]["office"].ToString();
            string strbase = "<base_xml>" +
                "<source>HIS</source> " +
                "<hosp_code>1790</hosp_code> " +
                "<dept_code>" + OfficeID + "</dept_code> " +
                "<dept_name>" + office + "</dept_name> " +
                "<doct>" +
                "<code>" + dsDoctor.Tables[0].Rows[0]["OPERATORID"].ToString() + "</code>  " +
                "<name>" + dsDoctor.Tables[0].Rows[0]["OPERATORNAME"].ToString() + "</name>  " +
                "<type>" + dsRole.Tables[0].Rows[0]["TECHNICALPOSTID"].ToString() + "</type>  " +
                "<type_name>" + dsRole.Tables[0].Rows[0]["TECHNICALPOST"].ToString() + "</type_name> " +
                "</doct>" +
                "</base_xml>";

            return strbase;
        }
        #endregion

        #region 打印缴费凭证
        /// <summary>
        /// 打印自助挂号
        /// </summary>
        private void PrintReport(DataSet ds, string strCK)
        {
            ds.WriteXml(Application.StartupPath + @"\\ReportXml\\自助扣费凭证" + ds.Tables[0].Rows[0]["REGISTERID"].ToString() + ".xml");
            string path = Application.StartupPath + @"\\Reports\\自助扣费凭证.frx";

            if (System.IO.File.Exists(path) == false)
            {
                //SkynetMessage.MsgInfo("自助挂号票据不存在,请联系管理员!");
                SkyComm.ShowMessageInfo("自助扣费凭证不存在,请联系管理员!");
                return;
            }

            //Common_XH theCamera_XH = new Common_XH();
            //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
            PrintManager print = new PrintManager();
            print.InitReport("自助扣费凭证");
            print.AddParam("医院名称", SysOperatorInfo.CustomerName);
            print.AddParam("姓名", lblxm.Text);
            print.AddParam("卡余额", SkyComm.cardBlance);
            print.AddParam("操作员", SysOperatorInfo.OperatorCode);
            print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
            print.AddParam("窗口号", strCK);
            print.AddData(ds.Tables[0], "report");
            PrintManager.CanDesign = true;
            print.Print();
            print.Dispose();
            Thread.Sleep(100);

        }



        private void PrintReport(DataSet ds, string strCK, DataSet ds1)
        {

            for (int i = ds1.Tables[0].Rows.Count - 1; i >= 0; i--)
            {

                if ("False" == ds1.Tables[0].Rows[i]["PITCHON1"].ToString())
                {
                    ds1.Tables[0].Rows.RemoveAt(i);
                }


            }





            ds1.WriteXml(Application.StartupPath + @"\\ReportXml\\自助扣费凭证" + ds.Tables[0].Rows[0]["REGISTERID"].ToString() + ".xml");
            string path = Application.StartupPath + @"\\Reports\\自助扣费凭证.frx";

            if (System.IO.File.Exists(path) == false)
            {
                //SkynetMessage.MsgInfo("自助挂号票据不存在,请联系管理员!");
                SkyComm.ShowMessageInfo("自助扣费凭证不存在,请联系管理员!");
                return;
            }

            //Common_XH theCamera_XH = new Common_XH();
            //theCamera_XH.DoorLightOpen(LightTypeenum.凭条, LightOpenTypeenum.闪烁);
            PrintManager print = new PrintManager();
            print.InitReport("自助扣费凭证");
            print.AddParam("医院名称", SysOperatorInfo.CustomerName);
            print.AddParam("姓名", lblxm.Text);
            print.AddParam("卡余额", SkyComm.cardBlance);
            print.AddParam("操作员", SysOperatorInfo.OperatorCode);
            print.AddParam("操作员姓名", SysOperatorInfo.OperatorName);
            print.AddParam("窗口号", strCK);
            print.AddData(ds1.Tables[0], "report");
            PrintManager.CanDesign = true;
            print.Print();
            print.Dispose();
            Thread.Sleep(100);

        }

        /// <summary>
        /// 调用派昂WebService方法传数据  19797 
        /// </summary>
        private string UploadAngPaiData(DataSet detailAccountData, DataSet ecipeMedicineData, DataSet dsRecipeDetail)
        {
            PatientInfoFacade patientinfo = new PatientInfoFacade();
            UnitToPack unitToPack = new UnitToPack();
            DataSet dataset = patientinfo.FindInfoByDiagnoseidForLeechdom(SkyComm.DiagnoseID);
            string Sicknessname = string.Empty;
            if (dataset != null && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow[] dr = dataset.Tables[0].Select("OFFICEID='" + detailAccountData.Tables[0].Rows[0]["REGISTEROFFICEID"].ToString() + "'");
                if (dr != null && dr.Length > 0)
                {
                    Sicknessname = dr[0]["SICKNESSNAME1"].ToString();
                }
            }
            PerfromFreqFacade thePerfromFreqFacade = new PerfromFreqFacade();
            DataSet dsfreq = thePerfromFreqFacade.FindAllNotStop();
            string strXml = "<?xml version='1.0' encoding='GBK' standalone='yes' ?>";
            strXml += "<output>";
            //循环处理发药明细实体中的记录
            foreach (DataRow rowYp in ecipeMedicineData.Tables[0].Rows)
            {
                strXml += "<row>";
                strXml += "<diagnoseid>" + SkyComm.DiagnoseID + "</diagnoseid>";
                strXml += "<sicknessname>" + Sicknessname + "</sicknessname>";
                strXml += "<operatedate>" + Convert.ToDateTime(rowYp["OPERATEDATE"]).ToString("yyyy-MM-dd HH:mm:ss") + "</operatedate>";
                strXml += "<patientname>" + lblxm.Text + "</patientname>";
                strXml += "<registerid>" + detailAccountData.Tables[0].Rows[0][DetailAccountData.D_DETAIL_ACCOUNT_REGISTERID].ToString() + "</registerid>";

                //mitao 20150427 上传当时的开单科室和开单医生 21185
                strXml += "<registerofficeid>" + detailAccountData.Tables[0].Rows[0][DetailAccountData.D_DETAIL_ACCOUNT_REGISTEROFFICEID].ToString() + "</registerofficeid>";
                strXml += "<doctorid>" + detailAccountData.Tables[0].Rows[0][DetailAccountData.D_DETAIL_ACCOUNT_DOCTORID].ToString() + "</doctorid>";
                //strXml += "<registerofficeid>admin</registerofficeid>";
                //strXml += "<doctorid>admin</doctorid>";

                string ClinicrecipeId = rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ECIPENUM].ToString();
                string RecipelistNum = rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SERIALNO].ToString();
                if (!dsRecipeDetail.Tables[0].Columns.Contains("PRICECHANGE"))
                {
                    dsRecipeDetail.Tables[0].Columns.Add("PRICECHANGE", typeof(decimal));  //单价
                    dsRecipeDetail.Tables[0].Columns.Add("AMOUNTCHANGE", typeof(decimal));  //数量
                    dsRecipeDetail.Tables[0].Columns.Add("UNITCHANGE", typeof(string));  //单位
                }
                DataRow[] ArrRow = dsRecipeDetail.Tables[0].Select("CLINICRECIPEID='" + ClinicrecipeId + "' AND RECIPELISTNUM=" + RecipelistNum);

                if (ArrRow.Length > 0)
                {

                    if (ArrRow[0]["OUTPATIENTUNIT"].ToString().Trim() == "单位")
                    {
                        ArrRow[0].BeginEdit();
                        ArrRow[0]["PRICECHANGE"] = ArrRow[0]["UNITPRICE"];
                        ArrRow[0]["AMOUNTCHANGE"] = ArrRow[0]["AMOUNT"];
                        ArrRow[0]["UNITCHANGE"] = ArrRow[0]["UNIT"];
                        ArrRow[0].EndEdit();
                    }

                    if (ArrRow[0]["OUTPATIENTUNIT"].ToString().Trim() == "包装")
                    {
                        ArrRow[0].BeginEdit();
                        int amount = Convert.ToInt32(ArrRow[0]["AMOUNT"]);
                        int changeratio = Convert.ToInt32(ArrRow[0]["CHANGERATIO"]);
                        ArrRow[0]["AMOUNTCHANGE"] = unitToPack.GetPackAmount(Convert.ToInt32(ArrRow[0]["CHANGERATIO"]), Convert.ToInt32(ArrRow[0]["AMOUNT"]));
                        ArrRow[0]["UNITCHANGE"] = ArrRow[0]["PACK"];
                        ArrRow[0]["PRICECHANGE"] = Convert.ToDecimal(ArrRow[0]["UNITPRICE"]) * changeratio;
                        ArrRow[0].EndEdit();
                    }

                    strXml += "<dose>" + ArrRow[0]["DOSE"].ToString() + "</dose>";
                    strXml += "<doseunit>" + ArrRow[0]["DOSEUNIT"].ToString() + "</doseunit>";
                    strXml += "<medusage>" + ArrRow[0]["MEDUSAGE"].ToString() + "</medusage>";
                    strXml += "<tohavemedicinefrequency>" + ArrRow[0]["TOHAVEMEDICINEFREQUENCY"].ToString() + "</tohavemedicinefrequency>";

                    DataRow[] arrfreq = dsfreq.Tables[0].Select("PERFORMFREQ='" + ArrRow[0]["TOHAVEMEDICINEFREQUENCY"].ToString() + "'");
                    if (arrfreq.Length > 0)
                    {
                        strXml += "<times>" + arrfreq[0]["TIMES"].ToString() + "</times>";
                    }
                    else
                    {
                        strXml += "<times></times>";
                    }
                    strXml += "<usedays>" + ArrRow[0]["USEDAYS"].ToString() + "</usedays>";
                    strXml += "<clinicrecipeid>" + ClinicrecipeId + "</clinicrecipeid>";
                    strXml += "<icode>" + ArrRow[0]["ICODE"].ToString() + "</icode>";
                    strXml += "<unitprice>" + ArrRow[0]["PRICECHANGE"].ToString() + "</unitprice>";
                    strXml += "<amount>" + ArrRow[0]["AMOUNTCHANGE"].ToString() + "</amount>";
                    strXml += "<unit>" + ArrRow[0]["UNITCHANGE"].ToString() + "</unit>";
                    strXml += "<totalmoney>" + ArrRow[0]["TOTALMONEY"].ToString() + "</totalmoney>";
                    strXml += "<storeroomid>" + rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_STOREROOMID].ToString() + "</storeroomid>";
                    strXml += "<detailaccountid>" + rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DETAILACCOUNTID].ToString() + "</detailaccountid>";
                    strXml += "<pack>" + ArrRow[0]["PACK"].ToString() + "</pack>";
                    strXml += "<changeratio>" + rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_CHANGERATIO].ToString() + "</changeratio>";
                    strXml += "<outpatientunit>" + rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OUTPATIENTUNIT].ToString() + "</outpatientunit>";
                }

                strXml += "</row>";

            }
            strXml += "</output>";
            string[] detailtemp = SystemInfo.SystemConfigs["药房自动配药接口类型"].Detail.Split('#');
            string pacspath = string.Empty;
            for (int i = 0; i < detailtemp.Length; i++)
            {
                if (detailtemp[i].IndexOf("派昂") != -1)
                {
                    if (detailtemp[i].Split(',').Length != 2)
                    {
                        SkyComm.ShowMessageInfo("[药房自动配药接口类型]配置格式有误！");
                        return "";
                    }
                    pacspath = detailtemp[i].Split(',')[1];
                    break;
                }
            }
            if (pacspath == string.Empty)
            {
                SkyComm.ShowMessageInfo("[药房自动配药接口类型]服务地址未配置！");
                return "";
            }
            object[] obj = new object[1];
            obj[0] = strXml;

            try
            {
                //通过调用WebService给派昂传数据
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用派昂接口MZ_SetDoctorInfo方法输入参数：" + strXml);
                string strResult = Common.WebServiceHelper.InvokeWebService(pacspath, "MZ_SetDoctorInfo", obj).ToString();
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用派昂接口MZ_SetDoctorInfo方法返回值：" + strResult);
                if (strResult == "0" || strResult == "-1")
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用派昂接口MZ_SetDoctorInfo方法失败，返回值：" + strResult);
                    SkyComm.ShowMessageInfo("调用派昂接口MZ_SetDoctorInfo方法失败，返回值：" + strResult);

                    return "";
                }
                else
                {
                    return strResult;
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用派昂接口MZ_SetDoctorInfo方法失败：" + ex.Message);
                SkyComm.ShowMessageInfo("调用派昂接口MZ_SetDoctorInfo方法失败：" + ex.Message);
                return "";
            }
        }


        #endregion

        #region 高值耗材验证

        /// <summary>
        /// 高值耗材验证
        /// </summary>
        /// <param name="AccountDetailData">收费项目明细信息</param>
        /// <returns></returns>
        /// ZHOUHU ADD CASE:31590 2018101601  yyl UPDATE　CASE:33584 2019092701
        public bool CheckHValueMaterial(DetailAccountData detailAccountData)
        {
            bool result = true;
            int barCodeCount = 0;
            try
            {
                string IHMaterial = SystemInfo.SystemConfigs["门诊是否启用高值耗材接口"].DefaultValue;
                if ("1" == IHMaterial)
                {
                    string strWhere = string.Empty;
                    for (int i = 0; i < detailAccountData.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere += "ITEMID IN (";
                        }
                        strWhere += "'" + detailAccountData.Tables[0].Rows[i]["ITEMID"].ToString() + "',";
                        if (i == detailAccountData.Tables[0].Rows.Count - 1)
                        {
                            strWhere = strWhere.Remove(strWhere.Length - 1);
                            strWhere += ")";
                        }
                    }
                    SummaryInfoFacade summaryInfoFacade = new SummaryInfoFacade();
                    DataSet ds = summaryInfoFacade.Select(strWhere);
                    barCodeCount = ds.Tables[0].Select(" CODENO LIKE 'HC%' ").Length;

                    if (barCodeCount > 0)
                    {
                        result = false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        public DataSet GetRecipeInfo()
        {
            //查询未缴费的处方信息，如果未缴费的处方没有的，则不需要缴费
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsRecipe = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID);

            string Offices = SkyComm.getvalue("使用自助缴费的执行科室ID");
            if (!string.IsNullOrEmpty(Offices))
            {
                string[] arrOffice = Offices.Split(',');
                foreach (DataRow row in dsRecipe.Tables[0].Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;
                    if (!arrOffice.Contains(row["EXECOFFICEID"].ToString()))
                    {
                        row.Delete();
                    }
                }
                dsRecipe.AcceptChanges();
            }
            if (dsRecipe.Tables[0].Rows.Count == 0)
            {
                //SkyComm.ShowMessageInfo("没有需要缴费的处方信息！");
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

        #region 选择全部，药品，检查，检验，其他处方
        private void lblAll_Click(object sender, EventArgs e)
        {
            dsRecipe.Tables[0].DefaultView.RowFilter = "";
            refreshUI();
        }

        private void lblyp_Click(object sender, EventArgs e)
        {
            dsRecipe.Tables[0].DefaultView.RowFilter = "RECIPETYPE IN ('药品费','中草药')";
            refreshUI();
        }

        private void lbljc_Click(object sender, EventArgs e)
        {
            dsRecipe.Tables[0].DefaultView.RowFilter = "RECIPETYPE IN ('检查','病理')";
            refreshUI();
        }

        private void lbljy_Click(object sender, EventArgs e)
        {
            dsRecipe.Tables[0].DefaultView.RowFilter = "RECIPETYPE IN ('检验','化验')";
            refreshUI();
        }

        private void lblqt_Click(object sender, EventArgs e)
        {
            dsRecipe.Tables[0].DefaultView.RowFilter = "RECIPETYPE NOT IN ('药品费','中草药','检查','病理','检验','化验')";
            refreshUI();
        }

        private void refreshUI()
        {
            decimal sumMoney = ReturnTotalMoney();
            lblTotalMoney.Text = string.Format("{0:0.00}", sumMoney);
            ucTime1.Sec = 60;
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

        /// <summary>
        /// 微信交费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {


            this.ucTime1.timer1.Stop();

            #region 验证缴费处方信息

            DataTable dtMain = dsRecipe.Tables[0].DefaultView.ToTable();

            IEnumerable<string> _CLINICRECIPEIDs = dtMain.AsEnumerable().Where(b => b.Field<bool>("PITCHON1") == true).Select(a => a.Field<string>("CLINICRECIPEID")).Distinct();
            if (_CLINICRECIPEIDs.Count() == 0)
            {
                SkyComm.ShowMessageInfo("请选择要缴费的处方！");
                return;
            }
            this.ucTime1.timer1.Stop();
            //选中处方的金额
            decimal sumMoney = DecimalRound.Round(Convert.ToDecimal(dtMain.Compute("SUM(TOTALMONEY)", "PITCHON1 = true")), 2);

            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsTemp = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID, string.Empty, "3");

            DataSet ds = dsTemp.Clone();
            foreach (DataRow row in dsTemp.Tables[0].Rows)
            {
                if (_CLINICRECIPEIDs.Contains(row["CLINICRECIPEID"].ToString()))
                {
                    ds.Tables[0].ImportRow(row);
                }
            }
            if (ds.Tables[0].Rows.Count < 1)
            {
                SkyComm.ShowMessageInfo("没有找到可进行缴费的处方记录，请检查处方号");
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }
            ////调用大通审方系统
            //if (!CheckDTPass(ds))
            //{
            //    return;
            //}
            #endregion

            #region 组织缴费处方信息
            CommonFacade commonFacade = new CommonFacade();
            DateTime ServerTime = commonFacade.GetServerDateTime();

            LeechdomCharge charge = new LeechdomCharge(SkyComm.DiagnoseID, SysOperatorInfo.OperatorID);
            charge.AddRecipeCharge(ds);

            decimal decHisMoney = DecimalRound.Round(Convert.ToDecimal(charge.detailAccountData.Tables[0].Compute("SUM(MONEY)", "")), 2);

            if (decHisMoney != sumMoney)
            {
                charge.detailAccountData.Clear();
                charge.ecipeMedicineData.Clear();
                SkyComm.ShowMessageInfo("选择的处方金额不正确，请在门诊窗口进行缴费！");
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }


            #endregion

            FrmNetPay payFrm = new FrmNetPay();

            payFrm.PayMoney = Decimal.Parse(lblTotalMoney.Text.ToString());

            payFrm.ServiceType = "3";

            payFrm.PayMethod = "1";

            payFrm.PayType = "缴费";

            DialogResult mDialogResult = payFrm.ShowDialog();

            if (mDialogResult == DialogResult.OK)
            {
                string hisno = payFrm.hisNo;
                NetPay(hisno,"线上微信");

                SkyComm.ShowMessageInfo(payFrm.PayType + "成功！");
            }
            else
            {
                this.ucTime1.timer1.Start();//计时器动 
            }

        }

        /// <summary>
        /// 点击支付宝缴费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();

            #region 验证缴费处方信息

            DataTable dtMain = dsRecipe.Tables[0].DefaultView.ToTable();

            IEnumerable<string> _CLINICRECIPEIDs = dtMain.AsEnumerable().Where(b => b.Field<bool>("PITCHON1") == true).Select(a => a.Field<string>("CLINICRECIPEID")).Distinct();
            if (_CLINICRECIPEIDs.Count() == 0)
            {
                SkyComm.ShowMessageInfo("请选择要缴费的处方！");
                return;
            }
            this.ucTime1.timer1.Stop();
            //选中处方的金额
            decimal sumMoney = DecimalRound.Round(Convert.ToDecimal(dtMain.Compute("SUM(TOTALMONEY)", "PITCHON1 = true")), 2);

            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsTemp = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID, string.Empty, "3");

            DataSet ds = dsTemp.Clone();
            foreach (DataRow row in dsTemp.Tables[0].Rows)
            {
                if (_CLINICRECIPEIDs.Contains(row["CLINICRECIPEID"].ToString()))
                {
                    ds.Tables[0].ImportRow(row);
                }
            }
            if (ds.Tables[0].Rows.Count < 1)
            {
                SkyComm.ShowMessageInfo("没有找到可进行缴费的处方记录，请检查处方号");
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }
            ////调用大通审方系统
            //if (!CheckDTPass(ds))
            //{
            //    return;
            //}
            #endregion

            #region 组织缴费处方信息
            CommonFacade commonFacade = new CommonFacade();
            DateTime ServerTime = commonFacade.GetServerDateTime();

            LeechdomCharge charge = new LeechdomCharge(SkyComm.DiagnoseID, SysOperatorInfo.OperatorID);
            charge.AddRecipeCharge(ds);

            decimal decHisMoney = DecimalRound.Round(Convert.ToDecimal(charge.detailAccountData.Tables[0].Compute("SUM(MONEY)", "")), 2);

            if (decHisMoney != sumMoney)
            {
                charge.detailAccountData.Clear();
                charge.ecipeMedicineData.Clear();
                SkyComm.ShowMessageInfo("选择的处方金额不正确，请在门诊窗口进行缴费！");
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                return;
            }


            #endregion

            FrmNetPay payFrm = new FrmNetPay();

            payFrm.PayMoney = Decimal.Parse(lblTotalMoney.Text.ToString());

            payFrm.ServiceType = "3";

            payFrm.PayMethod = "2";

            payFrm.PayType = "缴费";

            DialogResult mDialogResult = payFrm.ShowDialog();

            if (mDialogResult == DialogResult.OK)
            {
                string hisno = payFrm.hisNo;
                NetPay(hisno,"线上支付宝");

                SkyComm.ShowMessageInfo(payFrm.PayType + "成功！");
            }
            else
            {
                this.ucTime1.timer1.Start();//计时器动 
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SkyComm.ShowMessageInfo("此功能未开放！");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SkyComm.ShowMessageInfo("此功能未开放！");
        }



        public void NetPay(string hisno,string payMethod)
        {
            this.AnsyWorker(ui =>
            {
                ui.UpdateTitle("正在缴费中，请稍等...");

                ui.SynUpdateUI(() =>
                {

                    #region 验证缴费处方信息

                    DataTable dtMain = dsRecipe.Tables[0].DefaultView.ToTable();

                    IEnumerable<string> _CLINICRECIPEIDs = dtMain.AsEnumerable().Where(b => b.Field<bool>("PITCHON1") == true).Select(a => a.Field<string>("CLINICRECIPEID")).Distinct();
                    if (_CLINICRECIPEIDs.Count() == 0)
                    {
                        SkyComm.ShowMessageInfo("请选择要缴费的处方！");
                        return;
                    }
                    this.ucTime1.timer1.Stop();
                    //选中处方的金额
                    decimal sumMoney = DecimalRound.Round(Convert.ToDecimal(dtMain.Compute("SUM(TOTALMONEY)", "PITCHON1 = true")), 2);

                    ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
                    DataSet dsTemp = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID, string.Empty, "3");

                    DataSet ds = dsTemp.Clone();
                    foreach (DataRow row in dsTemp.Tables[0].Rows)
                    {
                        if (_CLINICRECIPEIDs.Contains(row["CLINICRECIPEID"].ToString()))
                        {
                            ds.Tables[0].ImportRow(row);
                        }
                    }
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        SkyComm.ShowMessageInfo("没有找到可进行缴费的处方记录，请检查处方号");
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }
                    //调用大通审方系统
                    if (!CheckDTPass(ds))
                    {
                        return;
                    }
                    #endregion

                    #region 组织缴费处方信息
                    CommonFacade commonFacade = new CommonFacade();
                    DateTime ServerTime = commonFacade.GetServerDateTime();

                    LeechdomCharge charge = new LeechdomCharge(SkyComm.DiagnoseID, SysOperatorInfo.OperatorID);
                    charge.AddRecipeCharge(ds);

                    decimal decHisMoney = DecimalRound.Round(Convert.ToDecimal(charge.detailAccountData.Tables[0].Compute("SUM(MONEY)", "")), 2);

                    if (decHisMoney != sumMoney)
                    {
                        charge.detailAccountData.Clear();
                        charge.ecipeMedicineData.Clear();
                        SkyComm.ShowMessageInfo("选择的处方金额不正确，请在门诊窗口进行缴费！");
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }


                    #endregion

                    #region 缴费
                    try
                    {
                        //验证西北妇幼高值耗材
                        if (!CheckHValueMaterial(charge.detailAccountData))
                            throw new Exception("缴费信息中有高值耗材，请在门诊窗口进行缴费！");

                        DetailAccountFacade detailAccountFacade = new DetailAccountFacade();


                        DataSet data = new DataSet();

                        Hashtable hashtable = new Hashtable();

                        hashtable.Add("@HISSEQNO", hisno);

                        string mSql = "SELECT * FROM T_BANKHISEXCHANGE_TRANS where HISSEQNO = @HISSEQNO";// and BANKSTATE = '1'

                        QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

                        data = querySolutionFacade.ExeQuery(mSql, hashtable);

                        TBankhisexchangeTransData transData = new TBankhisexchangeTransData();

                        transData.Bankseqno = data.Tables[0].Rows[0]["BANKSEQNO"].ToString();
                        transData.Hisseqno = data.Tables[0].Rows[0]["HISSEQNO"].ToString();
                        transData.Ohisseqno = data.Tables[0].Rows[0]["OHISSEQNO"].ToString();
                        transData.Bankstate = data.Tables[0].Rows[0]["BANKSTATE"].ToString();
                        transData.Hisstate = data.Tables[0].Rows[0]["HISSTATE"].ToString();
                        transData.Buscd = data.Tables[0].Rows[0]["BUSCD"].ToString();
                        transData.Hisid = data.Tables[0].Rows[0]["HISID"].ToString();
                        transData.Trfamt = data.Tables[0].Rows[0]["TRFAMT"].ToString();
                        transData.Usetype = data.Tables[0].Rows[0]["USETYPE"].ToString();
                        transData.Operatorid = data.Tables[0].Rows[0]["OPERATORID"].ToString();
                        transData.Operatetime = Convert.ToDateTime(data.Tables[0].Rows[0]["OPERATETIME"].ToString());
                        transData.Diagnoseid = data.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        transData.BusinessType = data.Tables[0].Rows[0]["BUSINESSTYPE"].ToString();
                        transData.DataSources = data.Tables[0].Rows[0]["DATASOURCES"].ToString();
                        transData.DataSources = data.Tables[0].Rows[0]["DATASOURCES"].ToString();
                        transData.RefundType = Convert.ToInt32(data.Tables[0].Rows[0]["REFUNDTYPE"].ToString());
                        transData.RefundMoney = Convert.ToDecimal(data.Tables[0].Rows[0]["REFUNDMONEY"].ToString());
                        transData.Remark = "_FromPayForRecipeForm";


                        foreach (DataRow Row in charge.detailAccountData.Tables[0].Rows)
                        {
                            Row.BeginEdit();

                            Row["ISBANKCARD"] = 1;
                            Row["BALANCEMODE"] = payMethod;
                            Row["BANKTRANSNO"] = transData.Hisseqno;
                            Row["IS_FEECHARGING_CARD"] = "1";
                            Row.EndEdit();
                            decHisMoney += Convert.ToDecimal(Row["MONEY"]);
                        }





                        DataSet dads = detailAccountFacade.insertEntityNoInvoiceZZZD(charge.detailAccountData, ref charge.ecipeMedicineData, transData);

                        //DataSet dads = detailAccountFacade.insertEntityNoInvoice(charge.detailAccountData, ref charge.ecipeMedicineData);
                        SkyComm.GetCardBalance();
                        SkyComm.ShowMessageInfo("缴费成功!");

                        //根据“药房自动配药接口类型”配置为2时，调用西北妇幼的派昂接口 19797 
                        string AutoDoseConfig = SystemInfo.SystemConfigs["药房自动配药接口类型"].DefaultValue;
                        string strCK = string.Empty;
                        if (AutoDoseConfig == "2" && charge.ecipeMedicineData.Tables[0].Rows.Count > 0)
                        {
                            strCK = UploadAngPaiData(dads, charge.ecipeMedicineData, dsTemp);
                        }

                        PrintReport(dads, strCK, dsRecipe);//打印交费凭证

                    }
                    catch (Exception err)
                    {
                        charge.detailAccountData.Clear();
                        charge.ecipeMedicineData.Clear();
                        Skynet.LoggingService.LogService.GlobalInfoMessage(SkyComm.DiagnoseID + "缴费失败：" + err.Message);
                        SkyComm.ShowMessageInfo("缴费失败：" + err.Message);
                        this.ucTime1.Sec = 60;
                        this.ucTime1.timer1.Start();
                        return;
                    }

                    dsRecipe = GetRecipeInfo();
                    if (dsRecipe == null || dsRecipe.Tables[0].Rows.Count == 0)
                    {
                        SkyComm.CloseWin(this);
                    }
                    else
                    {
                        this.gdcMain.DataSource = dsRecipe.Tables[0].DefaultView;

                        lblYE.Text = SkyComm.cardBlance.ToString();

                        refreshUI();
                    }
                    #endregion

                });
            });
        }








    }
}
