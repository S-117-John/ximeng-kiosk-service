using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using BusinessFacade.His.EMRPAD;
using EntityData.His.ClinicDoctor;
using Skynet.Framework.Common;
using SystemFramework.NewCommon;
using TiuWeb.ReportBase;


namespace AutoServiceManage.AutoPrint
{
    public partial class FrmPrintClinicEMR : Form
    {
        #region 变量
        public EntityList<ClinicBriefemrData> listEmr { get; set; }
        #endregion

        #region 构造函数，LOAD,关闭按钮事件

        public FrmPrintClinicEMR()
        {
            InitializeComponent();
        }

        private void FrmPrintClinicEMR_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            listEmr[0].PITCHON = true;
            this.gdcMain.DataSource = listEmr;
            //repositoryItemCheckEdit1_Click(null, null);
        }

        private void FrmPrintClinicEMR_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
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

        #region 选择需要打印病历

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            if (this.gdvMain.FocusedRowHandle < 0) return;

            if (gdvMain.SelectedRowsCount > 0)
            {
                ClinicBriefemrData emr = gdvMain.GetRow(gdvMain.FocusedRowHandle) as ClinicBriefemrData;
                if (emr == null)
                    return;

                if (emr.PITCHON == true)
                {
                    emr.PITCHON = false;
                }
                else
                {
                    emr.PITCHON = true;
                }

                foreach (ClinicBriefemrData data in listEmr)
                {
                    if (data.Emrid != emr.Emrid)
                        data.PITCHON = false;
                }
                gdvMain.RefreshData();
            }
            ucTime1.Sec = 60;
        }

        private void gdvMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (this.gdvMain.FocusedRowHandle < 0) return;

            if (gdvMain.SelectedRowsCount > 0)
            {
                ClinicBriefemrData emr = gdvMain.GetRow(gdvMain.FocusedRowHandle) as ClinicBriefemrData;
                if (emr == null)
                    return;

                if (emr.PITCHON == true)
                {
                    emr.PITCHON = false;
                }
                else
                {
                    emr.PITCHON = true;
                }

                foreach (ClinicBriefemrData data in listEmr)
                {
                    if (data.Emrid != emr.Emrid)
                        data.PITCHON = false;
                }
                gdvMain.RefreshData();
            }
            ucTime1.Sec = 60;
        }

        #endregion
        
        #region 打印病历
        private void lblClinicEmrPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.ucTime1.timer1.Stop();
                ClinicBriefemrData emr = listEmr.Find(a => a.PITCHON == true);
                if (emr == null)
                {
                    SkyComm.ShowMessageInfo("请选择您要打印的门诊病历信息");
                    return;
                }
                if (false == System.IO.File.Exists(Application.StartupPath + @"\\Reports\\门诊病历.frx"))
                {
                    SkyComm.ShowMessageInfo("系统没有找到报表文件“门诊病历.frx”!");
                    return;
                }
                ClinicPhysicianRecipeFacade recipeFacade = new ClinicPhysicianRecipeFacade();
                ClinicPhysicianRecipeData RecipeData = (ClinicPhysicianRecipeData)recipeFacade.FindRecipeInfoRepForReprint(emr.DiagnoseId, emr.Registerid);
                ClinicPhysicianRecipeData dsTmp = (ClinicPhysicianRecipeData)RecipeData.Clone();
                ClinicPhysicianRecipeData dsTmpCheck = (ClinicPhysicianRecipeData)RecipeData.Clone();
                string item_Tmp = string.Empty;
                string item = string.Empty;
                string item2 = string.Empty;
                string item3 = string.Empty;
                string item4 = string.Empty;
                ClinicEmrFacade cef = new ClinicEmrFacade();
                DataSet CEFData = cef.GetSchedulInfo(emr.DiagnoseId, emr.Registerid);
                string ACCEPTSTIME = string.Empty;
                foreach (DataRow dsr in CEFData.Tables[0].Rows)
                {
                    if (dsr["ACCEPTSTIME"].ToString() != "")
                        ACCEPTSTIME = dsr["ACCEPTSTIME"].ToString();
                    else
                        ACCEPTSTIME = "";
                }
                foreach (DataRow datarow in RecipeData.Tables[0].Rows)
                {
                    if (Convert.ToInt32(datarow["RECIPESTATE"]) == 2)
                    { //过滤退过费
                        continue;
                    }

                    switch (datarow["RECIPETYPE"].ToString())
                    {
                        case "中草药":
                        case "药品费":
                            dsTmp.Tables[0].ImportRow(datarow);
                            break;
                        case "检查":
                            item += datarow["NAME"].ToString() + "    ";
                            dsTmpCheck.Tables[0].ImportRow(datarow);
                            break;
                        case "化验":
                            item2 += datarow["NAME"].ToString() + "    ";
                            dsTmpCheck.Tables[0].ImportRow(datarow);
                            break;
                        case "手术":
                            item3 += datarow["NAME"].ToString() + "    ";
                            dsTmpCheck.Tables[0].ImportRow(datarow);
                            break;
                        case "治疗":
                            datarow["RECIPETYPE"] = "治疗";
                            item4 += datarow["NAME"].ToString() + "    ";
                            dsTmpCheck.Tables[0].ImportRow(datarow);
                            break;
                    }
                }

                dsTmp = Facada(dsTmp);

                item_Tmp = (item == "" ? "" : item + "\r\n") + (item2 == "" ? "" : item2 + "\r\n") + (item3 == "" ? "" : item3 + "\r\n") + (item4 == "" ? "" : item4);
                if (!dsTmp.Tables[0].Columns.Contains("Type"))
                    dsTmp.Tables[0].Columns.Add("Type", typeof(System.String)).DefaultValue = "";


                foreach (DataRow row in dsTmp.Tables[0].Rows)
                {
                    DataSet ds = new MedUsageFacade().FindByMedUsage(row["MEDUSAGE"].ToString());
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["TYPE"].ToString().Contains("静滴"))
                    {
                        row["Type"] = "静滴";
                    }
                    if (row["RECIPETYPE"].ToString() == "中草药")
                    {
                        row["DOSE"] = row["AMOUNT"];
                    }
                    foreach (DataColumn dc in dsTmp.Tables[0].Columns)
                    {
                        if (dc.DataType == typeof(System.Decimal))
                        {
                            double decValue = 0;
                            if (double.TryParse(row[dc.ColumnName].ToString(), out decValue))
                            {
                                row[dc.ColumnName] = Convert.ToDouble(decValue).ToString("0.######");
                            }
                        }
                    }

                }

                DataTable tb = dsTmp.Tables[0];
                if (dsTmp.Tables[0].Rows.Count > 0)
                    tb = dsTmp.Tables[0].Select("", "CLINICRECIPEID,GROUPNUM").CopyToDataTable();


                dsTmp.WriteXml(Application.StartupPath + @"\\ReportXML\\门诊病历or治疗建议YP.xml");
                dsTmpCheck.WriteXml(Application.StartupPath + @"\\ReportXML\\门诊病历or治疗建议ZL.xml");

                PatientInfoFacade patientinfo = new PatientInfoFacade();
                DataSet dataset = patientinfo.FindPateintByDiagnoseID(emr.DiagnoseId);

                F_DIAGNOSEFacade diagnose = new F_DIAGNOSEFacade();
                DataSet dataset1 = diagnose.FindByCustomID(emr.DiagnoseId, "DIAGNOSEID");

                PrintManager print = new PrintManager();
                print.InitReport("门诊病历");
                print.AddParam("诊疗号", emr.DiagnoseId);
                print.AddParam("挂号号", emr.Registerid);
                print.AddParam("PATIENTNAME", emr.PATIENTNAME);           //姓名  PATIENTNAME
                print.AddParam("SEX", emr.SEX);                   //性别   SEX
                print.AddParam("AVOIRDUPOIS", emr.AVOIRDUPOIS);                   //体重   AVOIRDUPOIS
                print.AddParam("AGE", emr.AGE);                   //年龄   AGE  
                print.AddParam("AGEUNIT", emr.AGEUNIT);        //年龄单位
                print.AddParam("费用类别", "");         //费用类别
                print.AddParam("OPERATETIME", emr.VisitTime);         //就诊时间  VisitTime
                print.AddParam("REGISTEROFFIC", emr.Office);            //就诊科室  office
                print.AddParam("主诉", emr == null ? "" : emr.CaseinChief);
                print.AddParam("病史", emr == null ? "" : emr.Emrcontent);
                print.AddParam("查体", emr == null ? "" : emr.Physical);
                print.AddParam("治疗建议", emr == null ? "" : emr.Notice);
                print.AddParam("临床诊断", emr.DiagResult);        //门诊诊断  DiagResult
                print.AddParam("医生", emr.VisitDoctorName);   //接诊医生  VisitDoctorName
                print.AddParam("检查", item_Tmp);            //检查
                print.AddParam("过敏史", emr.Allergen);      //过敏史
                print.AddParam("TELEPHONE", emr.TELEPHONE);      //联系电话
                print.AddParam("ADDRESS", emr.ADDRESS);      //地址
                print.AddParam("EMPLOYMENT", emr.EMPLOYMENT);    //职业
                print.AddParam("监护人", emr.Guardian);    //监护人
                print.AddParam("血压", emr.SystolicPressure + "/" + emr.DiastolicPressure + " mmHg");    //血压
                print.AddParam("ACCEPTSTIME", ACCEPTSTIME.ToString()); //就诊开始时间 17417 
                print.AddParam("医院名称", SysOperatorInfo.CustomerName);    //医院名称
                print.AddParam("OPERATETIME", emr.VisitTime == null ? "" : emr.VisitTime.ToString());

                if (dataset != null && dataset.Tables[0].Rows.Count > 0)
                {
                    print.AddParam("BIRTHDAY", dataset.Tables[0].Rows[0]["BIRTHDAY"].ToString());  //出生日期
                    print.AddParam("EMPLOYMENT", dataset.Tables[0].Rows[0]["EMPLOYMENT"].ToString());  //职业
                    print.AddParam("MARRIAGESTATUS", dataset.Tables[0].Rows[0]["MARRIAGESTATUS"].ToString());  //婚姻状况
                    print.AddParam("NATION", dataset.Tables[0].Rows[0]["NATION"].ToString());  //民族
                    //  print.AddParam("ACCEPTSTIME", dataset.Tables[0].Rows[0]["ACCEPTSTIME"].ToString());   //就诊开始时间
                }
                else
                {
                    print.AddParam("BIRTHDAY", "");  //出生日期
                    print.AddParam("EMPLOYMENT", "");  //职业
                    print.AddParam("MARRIAGESTATUS", "");  //婚姻状况
                    print.AddParam("NATION", "");  //民族
                }

                if (dataset1 != null && dataset1.Tables.Count > 0 && dataset1.Tables[0].Rows.Count > 0)
                {
                    print.AddParam("病名", dataset1.Tables[0].Rows[0]["SICKNESSNAME3"].ToString());   //病名
                    print.AddParam("证型", dataset1.Tables[0].Rows[0]["SICKNESS2"].ToString());   //证型
                }
                else
                {
                    print.AddParam("病名", "");   //病名
                    print.AddParam("证型", "");   //证型
                }
                print.AddData(dsTmpCheck.Tables[0], "report1");

                #region 构造病历数据集

                DataTable dt = new DataTable("reportemr");
                dt.Columns.Add("主诉");
                dt.Columns.Add("病史");
                dt.Columns.Add("查体");
                dt.Columns.Add("治疗建议");
                dt.Columns.Add("临床诊断");
                dt.Columns.Add("病名");
                dt.Columns.Add("证型");
                dt.Columns.Add("医生");
                dt.Columns.Add("检查");

                dt.Columns.Add("病生状态");
                dt.Columns.Add("过敏史");
                dt.Columns.Add("TELEPHONE");
                dt.Columns.Add("ADDRESS");
                dt.Columns.Add("EMPLOYMENT");
                dt.Columns.Add("监护人");

                DataRow theNewRow = dt.NewRow();
                theNewRow["主诉"] = emr == null ? "" : emr.CaseinChief;
                theNewRow["病史"] = emr == null ? "" : emr.Emrcontent;
                theNewRow["查体"] = emr == null ? "" : emr.Physical;
                theNewRow["治疗建议"] = emr == null ? "" : emr.Notice;
                theNewRow["临床诊断"] = emr.DiagResult;
                if (dataset1 != null && dataset1.Tables[0].Rows.Count > 0)
                {
                    theNewRow["病名"] = dataset1.Tables[0].Rows[0]["SICKNESSNAME3"].ToString();
                    theNewRow["证型"] = dataset1.Tables[0].Rows[0]["SICKNESS2"].ToString();
                }
                theNewRow["医生"] = SysOperatorInfo.OperatorName;
                theNewRow["检查"] = item;
                theNewRow["病生状态"] = emr.Morbidity;
                theNewRow["过敏史"] = emr.Allergen;
                theNewRow["TELEPHONE"] = emr.TELEPHONE;
                theNewRow["ADDRESS"] = emr.ADDRESS;
                theNewRow["EMPLOYMENT"] = emr.EMPLOYMENT;
                theNewRow["监护人"] = emr.Guardian;

                dt.Rows.Add(theNewRow);

                dt.WriteXml(Application.StartupPath + @"\\ReportXML\\门诊病历打印病历数据集.xml");

                print.AddData(dt, "reportemr");

                #endregion

                EPadidiographFacade eFacade = new EPadidiographFacade();
                DataSet eData = eFacade.GetByUserid(emr.VisitDoctor);
                print.AddData(eData.Tables[0], "dsPic"); //电子签名
                print.AddData(tb, "report");

                PrintManager.CanDesign = true;

                print.Print();
                print.Dispose();
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("门诊病历打印异常：" + ex.Message);
            }
            finally
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
            }
        }
      
        private ClinicPhysicianRecipeData Facada(ClinicPhysicianRecipeData RecipeData)
        {
            //增加一个组号 用于打印门诊病例使用
            RecipeData.Tables[0].Columns.Add("GROUPNUMBER", typeof(string));

            foreach (DataRow editRow in RecipeData.Tables[0].Rows)
            {
                editRow.BeginEdit();

                editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_NAME] = editRow["NAME"].ToString().Trim()
                    + "<" + editRow["SPECS"].ToString().Trim() + ">"; //合并
                if (editRow["POSITION"].ToString().Trim() == "包装")
                {
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_POSITION] = "包装";
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_UNIT] = editRow["PACK"].ToString();
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_HUNITPRICE]
                        = Convert.ToDecimal(editRow["UNITPRICE"]) * Convert.ToInt32(editRow["CHANGERATIO"]);
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_HAMOUNT]
                        = Convert.ToInt32(editRow["AMOUNT"]) / Convert.ToInt32(editRow["CHANGERATIO"]);
                    //YF内附院修改程序
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_UNITPRICE]
                        = Convert.ToDecimal(editRow["UNITPRICE"]) * Convert.ToInt32(editRow["CHANGERATIO"]);
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_AMOUNT]
                        = Convert.ToInt32(editRow["AMOUNT"]) / Convert.ToInt32(editRow["CHANGERATIO"]);

                }
                else
                {
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_POSITION] = "单位";
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_UNIT] = editRow["UNIT"].ToString();
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_UNITPRICE] = editRow["UNITPRICE"];
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_HUNITPRICE] = editRow["UNITPRICE"];
                    editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_HAMOUNT] = editRow["AMOUNT"];
                }
                
                //组合 组编号
                editRow["GROUPNUMBER"] = editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_CLINICRECIPEID].ToString() + editRow[ClinicPhysicianRecipeData.CLINICPHYSICIANRECIPE_GROUPNUM].ToString();

                editRow.EndEdit();
            }
            return RecipeData;
        }
        #endregion

    }
}
