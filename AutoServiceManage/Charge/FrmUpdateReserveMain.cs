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
using EntityData.His.ClinicDoctor;

namespace AutoServiceManage.Charge
{
    public partial class FrmUpdateReserveMain : Form
    {
        #region 变量
        public string OfficeType { get; set; }

        public DataSet dsRecipe { get; set; }

        DataTable dtRecipe = new DataTable();//预约信息

        DataTable dtRev = new DataTable(); //处方表

        string OfficeID = string.Empty;//执行科室ID

        bool isBindRev = false; //判断是否添加预约表列(字段)

        DateTime tempTime;//预约时间

        string sourceGroupID = ""; //根据医嘱用语找出对应的组ID，原始组ID
        string sourceGroupName = "";
        string sourceOfficeID = "";
        private string tempID = "", tempNO = "";
        public string tempeserveid = "", tempregisterid = "";
        private CLINICMtReserveFacade reserveFacade = null;
        #endregion

        #region 窗体初始化,Load

        public FrmUpdateReserveMain()
        {
            InitializeComponent();
        }
        private void FrmUpdateReserveMain_Load(object sender, EventArgs e)
        {
            refreshUI();
            ucTime1.timer1.Start();
            if (BindRecipe())
            {
                SelectAllRecipe(false);
                this.gdvMain.Focus();
            }
            else
            {
                this.Close();
                return;
            }
        }
        private void FrmUpdateReserveMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
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

        #region 选择处方信息
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
            //根据用户所选预约信息查找处方信息
            if (isSelected)
            {

                DataRow[] dsPitchon = dtTemp.Select("PITCHON1=true");
                if (dsPitchon.Length > 0)
                {
                    for (int i = 0; i < dsPitchon.Length; i++)
                    {
                        if (dsPitchon[i]["RESERVEID"].ToString() != selectRow["RESERVEID"].ToString())
                        {
                            dsPitchon[i]["PITCHON1"] = false;
                        }
                        else
                        {
                            dsPitchon[i]["PITCHON1"] = true;
                        }
                    }

                    reserveFacade = new CLINICMtReserveFacade();
                    DataSet ds = reserveFacade.ReserveRecipeInfo(selectRow["RESERVEID"].ToString());


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        tempID = ds.Tables[0].Rows[0]["CLINICRECIPEID"].ToString();
                        tempNO = ds.Tables[0].Rows[0]["APPLYDOCNO"].ToString();
                        tempTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["RESERVESTARTTIME"].ToString());
                        tempeserveid = ds.Tables[0].Rows[0]["RESERVEID"].ToString();
                        tempregisterid = ds.Tables[0].Rows[0]["REGISTERID"].ToString();

                        dtRev.Clear();
                        dtRev.Columns.Clear();
                        dtRev = new DataTable();

                        #region  构建dtRev
                        dtRev.Columns.Add("CLINICRECIPEID");
                        dtRev.Columns.Add("RECIPECONTENT");
                        dtRev.Columns.Add("RECIPETYPE");
                        dtRev.Columns.Add("UNITPRICE");
                        dtRev.Columns.Add("DOSEUNIT");
                        dtRev.Columns.Add("AMOUNT");
                        dtRev.Columns.Add("TOTALMONEY");
                        dtRev.Columns.Add("ADVICE");
                        #endregion
                        #region 处方表格绑定值
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            try
                            {
                                DataRow dr = dtRev.NewRow();
                                dr["CLINICRECIPEID"] = ds.Tables[0].Rows[i]["CLINICRECIPEID"].ToString();
                                //dr["RECIPECONTENT"] = ds.Tables[0].Rows[i]["RECIPECONTENT"].ToString();
                                dr["RECIPECONTENT"] = ds.Tables[0].Rows[i]["MEDORDNAME"].ToString();
                                dr["RECIPETYPE"] = ds.Tables[0].Rows[i]["RECIPETYPE"].ToString();
                                dr["UNITPRICE"] = ds.Tables[0].Rows[i]["UNITPRICE"].ToString();
                                dr["DOSEUNIT"] = ds.Tables[0].Rows[i]["DOSEUNIT"].ToString();
                                dr["AMOUNT"] = ds.Tables[0].Rows[i]["AMOUNT"].ToString();
                                dr["TOTALMONEY"] = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["TOTALMONEY"]), 2).ToString() + "元";
                                dr["ADVICE"] = ds.Tables[0].Rows[i]["ADVICE"].ToString();
                                dtRev.Rows.Add(dr);

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            this.gridControl1.DataSource = dtRev;
                        }

                        #endregion
                    }

                }
                else
                {
                    #region
                    int flag = 0;
                    for (int i = 0; i < this.gdvMain.RowCount; i++)
                    {
                        if ((Boolean)this.gdvMain.GetDataRow(i)["PITCHON1"])
                        {
                            flag++;
                        }
                    }
                    if (flag == 0)
                    {
                        sourceGroupName = "";
                        sourceGroupID = "";
                    }
                    #endregion

                }
            }

            return true;
        }
        #endregion

        #region 预约
        private void lblOK_Click(object sender, EventArgs e)
        {
            bool isAll = false;
            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if ((Boolean)gdvMain.GetDataRow(i)["PITCHON1"])
                {
                    isAll = true;
                }
            }
            if (isAll)
            {
                #region
                CLINICMtQueueFacade queueFacade = new CLINICMtQueueFacade();
                CLINICMtQueueData queue = new CLINICMtQueueData();
                queue = queueFacade.GetByReserveID(tempeserveid);
                if (!string.IsNullOrEmpty(queue.Exitnooff))
                {
                    #region
                    reserveFacade = new CLINICMtReserveFacade();
                    DataSet ds = reserveFacade.ReserveRecipeToGroup(tempID, tempNO);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        sourceGroupID = ds.Tables[0].Rows[0]["GROUPID"].ToString();
                        sourceGroupName = ds.Tables[0].Rows[0]["GROUPNAME"].ToString();
                        sourceOfficeID = ds.Tables[0].Rows[0]["OFFICEID"].ToString();
                    }
                    if (!SelectItem(sourceOfficeID))
                    {
                        ucTime1.timer1.Start();
                        return;
                    }

                    ucTime1.timer1.Stop();
                    using (FrmReserveInfo frm = new FrmReserveInfo())
                    {
                        frm.isupdatereserve = true;//是否修改
                        frm.reserveOldTime = tempTime;//预约时间
                        frm.streserveid = tempeserveid;
                        frm.stregisterid = tempregisterid;
                        frm.GroupID = sourceGroupID;
                        frm.GroupName = sourceGroupName;
                        frm.OfficeID = sourceOfficeID;
                        frm.CostMoney = string.Format("{0:0.00}", ReturnTotalMoney(sourceOfficeID));
                        frm.exOfficeName = dtRecipe.Rows[0]["OFFICEID"].ToString();

                        if (dtRev.Rows.Count == 0)
                        {
                            SkyComm.ShowMessageInfo("请选择处方信息后再进行预约！");
                            ucTime1.timer1.Start();
                            return;
                        }
                        else
                        {
                            frm.dtRev = dtRev;
                            frm.DiagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                            if (frm.ShowDialog(this) == DialogResult.OK)
                            {
                                using (FrmReserveAlert frmAlert = new FrmReserveAlert())
                                {
                                    frmAlert.reserveDate = frm.reserveDateNew;
                                    frmAlert.GroupName = sourceGroupName;
                                    frmAlert.queueNO = frm.queueNO;
                                    frmAlert.CostMoney = frm.CostMoney;
                                    frmAlert.queueNO = frm.QueueID;
                                    frmAlert.ShowDialog();
                                }
                                for (int i = this.dtRecipe.Rows.Count - 1; i >= 0; i--)
                                {
                                    if ((Boolean)dtRecipe.Rows[i]["PITCHON1"])
                                    {
                                        dtRecipe.Rows.RemoveAt(i);
                                    }
                                }
                                //清空预约组信息
                                sourceGroupID = "";
                                sourceGroupName = "";

                                SelectAllRecipe(false);
                                gdcMain.DataSource = null;
                                this.gdcMain.DataSource = dtRecipe;

                                //
                                frm.Dispose();
                                if (dtRecipe.Rows.Count == 0)
                                {
                                    ucTime1.timer1.Stop();
                                    SkyComm.CloseWin(this);
                                }
                                SkyComm.GetCardBalance();
                                //  lblYE.Text = SkyComm.cardBlance.ToString();
                            }
                            else
                            {
                                ucTime1.timer1.Start();
                                return;
                            }
                        }
                    }
                    #endregion
                }
                else
                {

                    SkyComm.ShowMessageInfo("您好：当前预约数据需要去窗口进行改签，谢谢。");
                }
                #endregion
            }
            else
            {
                SkyComm.ShowMessageInfo("您好：请选择预约改签项目，谢谢。");

            }
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

        #region 预约信息

        private bool BindRecipe()
        {
            string diagnoseid = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString().Trim();  //诊疗号
            //DateTime dtNow = new CommonFacade().GetServerDateTime();//服务器时间获取
            reserveFacade = new CLINICMtReserveFacade();
            DataSet ds = new DataSet();
            try
            {
                ds = reserveFacade.checkReserveRecord(diagnoseid);
            }
            catch (Exception ex)
            {

                throw;
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 构建DeptPrice
                dtRecipe.Columns.Add("PITCHON1", typeof(System.Boolean)).DefaultValue = false;
                dtRecipe.Columns.Add("QUEUENO");
                dtRecipe.Columns.Add("PATIENTNAME");
                dtRecipe.Columns.Add("EXAMINENAME");
                dtRecipe.Columns.Add("RESERVESTARTTIME");
                dtRecipe.Columns.Add("CHECKDOCTORID");
                dtRecipe.Columns.Add("DIAGNOSEID");
                dtRecipe.Columns.Add("REGISTERID");
                dtRecipe.Columns.Add("RESERVEID");
                dtRecipe.Columns.Add("OFFICEID");

                #endregion
                #region 表格绑定数据
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr = dtRecipe.NewRow();
                        dr["QUEUENO"] = ds.Tables[0].Rows[i]["QUEUENO"].ToString();
                        dr["PATIENTNAME"] = ds.Tables[0].Rows[i]["PATIENTNAME"].ToString();
                        dr["EXAMINENAME"] = ds.Tables[0].Rows[i]["EXAMINENAME"].ToString();
                        dr["RESERVESTARTTIME"] = ds.Tables[0].Rows[i]["RESERVESTARTTIME"].ToString();
                        //  dr["TOTALMONEY"] = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["TOTALMONEY"]), 2).ToString() + "元";
                        dr["CHECKDOCTORID"] = ds.Tables[0].Rows[i]["CHECKDOCTORID"].ToString();
                        dr["DIAGNOSEID"] = ds.Tables[0].Rows[i]["DIAGNOSEID"].ToString();
                        dr["REGISTERID"] = ds.Tables[0].Rows[i]["REGISTERID"].ToString();
                        dr["RESERVEID"] = ds.Tables[0].Rows[i]["RESERVEID"].ToString();
                        dr["OFFICEID"] = ds.Tables[0].Rows[i]["OFFICE"].ToString();
                        dtRecipe.Rows.Add(dr);

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                //try
                //{
                //    ClinicPhysicianRecipeFacade facade = new ClinicPhysicianRecipeFacade();
                //    DataTable dtableTemp = facade.GetGroupByRecipeDetail("门诊", dtRecipe);
                //    if (dtableTemp == null)
                //    {
                //        SkyComm.ShowMessageInfo("执行获取组信息失败");
                //        this.gdcMain.DataSource = null;
                //        return false;
                //    }
                //    this.gdcMain.DataSource = dtableTemp;
                //}
                //catch (Exception)
                //{

                //    throw;
                //}


                this.gdcMain.DataSource = dtRecipe;




                #endregion



            }
            else
            {
                SkyComm.ShowMessageInfo("未查询到对应处方信息或该处方数据已经过期！");
                return false;
            }
            return true;
        }



        /// <summary>
        /// 选择所有处方(全选或全不选)
        /// </summary>
        /// <param name="isAll">true全选，false全不选</param>
        private void SelectAllRecipe(bool isAll)
        {
            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                this.gdvMain.GetDataRow(i)["PITCHON1"] = isAll;
            }
        }
        /// <summary>
        /// 得到当前预约处方的收费状态
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetRecipeState()
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                dic.Clear();
                for (int i = 0; i < this.gdvMain.RowCount; i++)
                {
                    if (dic.ContainsKey(this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString()) == false)
                    {
                        dic.Add(this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString(), Convert.ToInt32(this.gdvMain.GetDataRow(i)["RECIPESTATE"]));
                    }
                    else
                    {
                        if (this.gdvMain.GetDataRow(i)["RECIPESTATE"].ToString() == "0")
                        {
                            if (dic[this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString()] == 1)
                            {
                                dic[this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString()] = 0;
                            }
                        }
                    }
                }
                return dic;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool SelectItem(string OfficeIDitem)
        {
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            string diagnoseid = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString().Trim();  //诊疗号
            DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByExeofficeID(diagnoseid, OfficeIDitem);
            if (!dtRev.Columns.Contains("处方号"))
            {
                #region MyRegion
                dtRev.Columns.Add("处方号");
                dtRev.Columns.Add("处方内容");
                dtRev.Columns.Add("注意事项");
                dtRev.Columns.Add("预约号");
                dtRev.Columns.Add("诊疗号");
                dtRev.Columns.Add("挂号号");
                dtRev.Columns.Add("科室ID");
                dtRev.Columns.Add("诊室名称");
                dtRev.Columns.Add("执行科室地址");
                dtRev.Columns.Add("医嘱用语备注");
                dtRev.Columns.Add("开单科室");
                dtRev.Columns.Add("检查医师");
                dtRev.Columns.Add("检查医师ID");
                dtRev.Columns.Add("预约日期");
                dtRev.Columns.Add("预约结束日期");
                dtRev.Columns.Add("报到时间");
                dtRev.Columns.Add("完成时间");
                dtRev.Columns.Add("挂起时间");
                dtRev.Columns.Add("操作员ID");
                dtRev.Columns.Add("预约状态");
                dtRev.Columns.Add("检查状态");
                dtRev.Columns.Add("检查目的");  //douyaming 2014-8-4 
                dtRev.Columns.Add("收费状态");  //douyaming 2016-05-14
                //isBindRev = true;
                #endregion
            }
            dtRev.Clear();

            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if ((Boolean)gdvMain.GetDataRow(i)["PITCHON1"]) //添加用户选择数据
                {
                    #region 增加医技预约次数限制 wangchao 2017.03.23 case:27593
                    //try
                    //{
                    //    if (SystemInfo.SystemConfigs["医技预约限制次数"].DefaultValue == "1")
                    //    {
                    //        string _execOffice = ds.Tables[0].Rows[i]["EXECOFFICEID"].ToString();
                    //        string _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    //        string _content = ds.Tables[0].Rows[i]["RECIPECONTENT"].ToString();
                    //        CLINICMtReserveFacade _reserveFacade = new CLINICMtReserveFacade();
                    //        DataSet dsCheck = _reserveFacade.checkRecipeHasReserveRecord(_diagnoseID, _execOffice, _content);
                    //        if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
                    //        {
                    //            SkyComm.ShowMessageInfo("检查项目【" + ds.Tables[0].Rows[i]["CHARGEITEM"].ToString() + "】在日期【" + Convert.ToDateTime(dsCheck.Tables[0].Rows[0]["RESERVESTARTTIME"].ToString()).ToString("yyyy年MM月dd日") + "】已存在预约信息，不能重复预约！");
                    //            return false;
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Skynet.LoggingService.LogService.GlobalInfoMessage("医技预约次数限制模块发生异常,原因：" + ex.Message);
                    //}
                    #endregion

                    DataSet dsClinic = reserveFacade.ReserveRecipeInfo(gdvMain.GetDataRow(i)["RESERVEID"].ToString ());
                    //dsClinic.Tables[0].TableName = "data1";
                    //ds.Tables[0].TableName = "data2";
                    //dsClinic.WriteXml(Application.StartupPath + "\\数据集1.xml");
                    //ds.WriteXml(Application.StartupPath + "\\数据集2.xml");
                    foreach (DataRow drClinic in dsClinic.Tables[0].Rows)
                    {
                        foreach (DataRow drTemp in ds.Tables[0].Rows)
                        {
                            if (drClinic["CLINICRECIPEID"].ToString() == drTemp["CLINICRECIPEID"].ToString() && drClinic["APPLYDOCNO"].ToString() == drTemp["APPLYDOCNO"].ToString())
                            {
                                DataRow newRow = dtRev.NewRow();
                                newRow["处方号"] = drTemp["CLINICRECIPEID"].ToString();
                                newRow["处方内容"] = drTemp["CHARGEITEM"].ToString();
                                newRow["注意事项"] = "";
                                newRow["预约号"] = "1";
                                newRow["诊疗号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                                newRow["挂号号"] = drTemp["REGISTERID"].ToString();
                                newRow["科室ID"] = drTemp["EXECOFFICEID"].ToString(); ;// this.cmbExamineName.SelectedValue.ToString(); 
                                newRow["诊室名称"] = drTemp["EXECOFFICE"].ToString();//new
                                newRow["开单科室"] = drTemp["OPERATOROFFICE"].ToString();
                                newRow["执行科室地址"] = drTemp["OFFICEADDRESS"].ToString();
                                newRow["医嘱用语备注"] = drTemp["REMARK"].ToString();
                                newRow["检查医师ID"] = "";// this.cmbUserName.SelectedValue;
                                newRow["检查医师"] = "";// this.cmbUserName.Text;
                                newRow["预约日期"] = "";
                                newRow["预约结束日期"] = null;
                                newRow["报到时间"] = null;
                                newRow["完成时间"] = null;
                                newRow["挂起时间"] = null;
                                newRow["操作员ID"] = SysOperatorInfo.OperatorID;
                                newRow["预约状态"] = 1; //预约
                                newRow["检查状态"] = 1; //未检查
                                newRow["检查目的"] = "";// this.gridView1.GetDataRow(i)["APPLYREMARK"].ToString(); 
                                try
                                {
                                    var revState = drTemp["RECIPESTATE"];
                                    newRow["收费状态"] = drTemp["RECIPESTATE"] == null ? "" : drTemp["RECIPESTATE"].ToString() == "0" ? "未收费" : "已收费";
                                }
                                catch (Exception)
                                {
                                    newRow["收费状态"] = "";
                                }
                                dtRev.Rows.Add(newRow);
                            }
                        }
                    }
                    //DataTable dtTemp = dtRev.Copy();
                    //DataSet dsTemp = new DataSet();
                    //dsTemp.Tables.Add(dtTemp);
                    //dsTemp.Tables[0].TableName = "data3";
                    //dsTemp.WriteXml(Application.StartupPath + "\\数据集3.xml");

                }
            }
            return true;
        }
        private decimal ReturnTotalMoney(string OfficeIDitem)
        {
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            string diagnoseid = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString().Trim();  //诊疗号
            //DataSet ds = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByExeofficeID(diagnoseid, OfficeIDitem);
            decimal sumMoney = 0;
            //for (int i = 0; i < this.gdvMain.RowCount; i++)
            //{
            //    if ((Boolean)gdvMain.GetDataRow(i)["PITCHON1"]) //添加用户选择数据
            //    {
            //        if (ds.Tables[0].Rows[i]["RECIPESTATE"].ToString() != "1")
            //        {
            //            sumMoney += DecimalRound.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TOTALMONEY"].ToString().Replace("元", "")), 2);
            //        }

            //    }
            //}
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
        private void refreshUI()
        {
            // decimal sumMoney = ReturnTotalMoney();
            //  lblTotalMoney.Text = string.Format("{0:0.00}", sumMoney);
            ucTime1.Sec = 60;
        }

        #endregion
        #region 更新开单科室
        /// <summary>
        /// 更新开单科室
        /// </summary>
        /// <param name="diagnoseId"></param>
        /// <param name="dataTable"></param>
        /// <param name="clinicPhysicianRecipeFacade"></param>
        /// <returns></returns>
        public DataTable getTrueOfficeDataTable(string diagnoseId, DataTable dataTable, ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade)
        {

            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string mApplyNo = dataRow["APPLYDOCNO"].ToString();//申请单号
                    string mCheckItem = dataRow["RECIPECONTENT"].ToString();//检查项目ID

                    string mOffice = clinicPhysicianRecipeFacade.getApplyOffice(diagnoseId, mApplyNo, mCheckItem);

                    dataRow["OPERATOROFFICE"] = mOffice;
                }

                return dataTable;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion






    }
}
