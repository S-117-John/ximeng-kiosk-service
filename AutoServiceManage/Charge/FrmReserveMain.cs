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

namespace AutoServiceManage.Charge
{
    public partial class FrmReserveMain : Form
    {
        #region 变量
        public string OfficeType { get; set; }

        public DataSet dsRecipe { get; set; }

        DataTable dtRecipe = new DataTable();

        DataTable dtRev = new DataTable(); //预约表

        string OfficeID = string.Empty;//执行科室ID

        bool isBindRev = false; //判断是否添加预约表列(字段)

        string sourceGroupID = ""; //根据医嘱用语找出对应的组ID，原始组ID
        string sourceGroupName = "";
        #endregion

        #region 窗体初始化,Load

        public FrmReserveMain()
        {
            InitializeComponent();
        }

        private void FrmReserveMain_Load(object sender, EventArgs e)
        {
            lblYE.Text = SkyComm.cardBlance.ToString();//余额
            this.lblxm.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();//姓名
            this.lblxb.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();//性别
            this.lblnl.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();//年龄
            this.lblcsrq.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["BIRTHDAY"].ToString();//出生日期
            OfficeID = SkyComm.getvalue(OfficeType);//获取执行科室ID
            if (string.IsNullOrEmpty(OfficeID))
            {
                SkyComm.ShowMessageInfo("请先维护医技科室对应的科室ID！");
                this.Close();
                return;
            }
            //string Offices = SkyComm.getvalue("不能挂号的科室");
 
            ucTime1.timer1.Start();
            if (BindRecipe())
            {
                SelectAllRecipe(false);
                decimal sumMoney = ReturnTotalMoney();
                lblTotalMoney.Text = string.Format("{0:0.00}", sumMoney);
                this.gdvMain.Focus();
            }
            else
            {
                this.Close();
                return;
            }

        }

        private void FrmReserveMain_FormClosing(object sender, FormClosingEventArgs e)
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
            //根据用户所选处方设置分组列表的数据源
            if (isSelected)
            {
                if (selectRow["GROUPID"].ToString().Trim().Length < 1)
                {
                    SkyComm.ShowMessageInfo("所选处方尚未进行医嘱用语部位维护，不能进行预约，请先维护！");
                    selectRow["PITCHON1"] = false;
                    dtRecipe.AcceptChanges();
                    refreshUI();
                    return false;
                }

                if (sourceGroupID != "")//用户已经选中一列数据
                {
                    if (sourceGroupID != selectRow["GROUPID"].ToString()) //新选中的处方和上次选择的处方不再同一个组，不能进行预约
                    {
                        SkyComm.ShowMessageInfo("两个所选处方不在同一组，需要分开预约!");
                        selectRow["PITCHON1"] = false;
                        dtRecipe.AcceptChanges();
                        refreshUI();
                        return false;
                    }
                }
                sourceGroupName = selectRow["GROUPNAME"].ToString();
                sourceGroupID = selectRow["GROUPID"].ToString();
                //if (!GetDataForReserveDate()) return; //加载时间预约列表
            }
            else
            {
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
                //dtForUpdateRev = DateTime.Parse("1900-01-01 00:00:00");
                //if (!GetDataForReserveDate()) return;//加载时间预约列表

            }
            bool haveSelected = false;
            //相同处方号全部选中-----同时排除不在同一组内的同一个处方-----限定不能同时选择两个处方
            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if (this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString() == selectRow["CLINICRECIPEID"].ToString())
                {
                    //同一个处方不能出现两个不同的组 zpq 2016年5月19日 17:41:45
                    if (this.gdvMain.GetDataRow(i)["GROUPID"].ToString() != selectRow["GROUPID"].ToString())
                    {
                        SkyComm.ShowMessageInfo("数据错误：同一个处方存在两个不同的组，请修改！");
                        SelectAllRecipe(false);
                        dtRecipe.AcceptChanges();
                        this.gdcMain.DataSource = null;
                        refreshUI();

                        return false;
                    }
                    this.gdvMain.GetDataRow(i)["PITCHON1"] = isSelected;
                    dtRecipe.AcceptChanges();
                }
                if ((bool)this.gdvMain.GetDataRow(i)["PITCHON1"])
                {
                    haveSelected = true;
                }

            }

            refreshUI();
            return true;
        }
        #endregion

        #region 预约
        private void lblOK_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ye = Convert.ToDecimal(this.lblYE.Text.Trim());
                decimal costMoney = Convert.ToDecimal(this.lblTotalMoney.Text.Trim());
                if (ye < costMoney)
                {
                    SkyComm.ShowMessageInfo("卡余额不足！");
                    ucTime1.timer1.Start();
                    return;
                }
            }
            catch
            { }
            ucTime1.timer1.Stop();
            using (FrmReserveInfo frm = new FrmReserveInfo())
            {
                frm.GroupID = sourceGroupID;
                frm.GroupName = sourceGroupName;
                frm.OfficeID = OfficeID;
                frm.CostMoney = this.lblTotalMoney.Text;
                frm.exOfficeName = dtRecipe.Rows[0]["OFFICE"].ToString();

                if (!SelectItem())
                {
                    ucTime1.timer1.Start();
                    return;
                }
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
                        lblYE.Text = SkyComm.cardBlance.ToString();
                    }
                    else
                    {
                        ucTime1.timer1.Start();
                        return;
                    }
                }
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

        #region 处方信息

        private bool BindRecipe()
        {
            string diagnoseidC = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString().Trim();  //诊疗号
            string strjfType = SystemInfo.SystemConfigs["健康卡操作员扣费条件"].DefaultValue.Trim();
            DataSet dsRecipeDetail = new DataSet();
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            #region MyRegion
            Dictionary<string, int> recipeList = new Dictionary<string, int>();//预约列表<处方ID,预约状态>
            Dictionary<string, int> dic = GetRecipeState();


            if (true)
            {
                #region 构建dtRecipe
                dtRecipe.Columns.Add("PITCHON1", typeof(System.Boolean)).DefaultValue = false;
                dtRecipe.Columns.Add("CLINICRECIPEID");
                dtRecipe.Columns.Add("MEDORDNAME");
                dtRecipe.Columns.Add("UNITPRICE");
                dtRecipe.Columns.Add("AMOUNT");
                dtRecipe.Columns.Add("DOSEUNIT");
                dtRecipe.Columns.Add("TOTALMONEY");
                dtRecipe.Columns.Add("OFFICE");
                dtRecipe.Columns.Add("APPLYREMARK");
                dtRecipe.Columns.Add("APPLYDOCNO");
                dtRecipe.Columns.Add("INHOSID");
                //douyaming 2016-05-14 增加是否收费字段
                dtRecipe.Columns.Add("RECIPESTATE");
                //zpq 2016年7月1日  开单医生，开单时间 ,挂号号 ,开单科室
                dtRecipe.Columns.Add("DOCTOR");
                dtRecipe.Columns.Add("OPERATETIME");
                dtRecipe.Columns.Add("REGISTERID");
                dtRecipe.Columns.Add("OPERATOROFFICE");
                dtRecipe.Columns.Add("OFFICEADDRESS");
                dtRecipe.Columns.Add("REMARK");
                dtRecipe.Columns.Add("STATE");
                dtRecipe.Columns.Add("RECIPECONTENT");
                #endregion

                //if (strjfType == "1")
                //{
                //dsRecipeDetail = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByOperatorID(diagnoseidC, SysOperatorInfo.OperatorID);
                //}
                //else
                //{
                dsRecipeDetail = clinicPhysicianRecipeFacade.FindAllRecipeMedordInfoByExeofficeID(diagnoseidC, OfficeID);
                //}

                #region 添加过滤附加费功能 zpq 2016年6月6日---2016年6月20日
                DataTable dtFJF = dsRecipeDetail.Tables[0].Copy();
                if (dtFJF == null || dtFJF.Rows.Count < 1)
                {
                    SkyComm.ShowMessageInfo("未查询到对应处方信息或该处方数据已经过期！");
                    return false;
                }
                String filterStr = " RECIPETYPE <>'医材' and RECIPETYPE <> '附加' and VALID <>'0' ";
                DataRow[] drs = dtFJF.Select(filterStr);
                if (drs.Length < 1)
                {
                    SkyComm.ShowMessageInfo("未查询到对应处方信息或该处方数据已经过期！");
                    return false;
                }

                dsRecipeDetail = new DataSet();//重新赋值
                dsRecipeDetail.Tables.Add(drs.CopyToDataTable());

                #endregion

                #region 赋值dtRecipe
                for (int i = 0; i < dsRecipeDetail.Tables[0].Rows.Count; i++)
                {

                    #region 判断是否已经预约过了(一个处方可能有多条明细) zpq 2016年5月24日
                    string repID = dsRecipeDetail.Tables[0].Rows[i]["CLINICRECIPEID"].ToString();
                    if (recipeList.ContainsKey(repID) && recipeList[repID] > 0)
                        continue;
                    else
                    {
                        if (!recipeList.ContainsKey(repID))
                        {
                            DataSet dsRes = clinicPhysicianRecipeFacade.GetReserveInfoByClinicrecipeID(repID);
                            if (dsRes.Tables[0].Rows.Count > 0)
                            {
                                recipeList.Add(repID, (int)dsRes.Tables[0].Rows[0]["RESSTATUS"]);
                                continue;
                            }
                            else
                            {
                                recipeList.Add(repID, -1);//无预约信息
                            }
                        }
                    }
                    #endregion

                    DataRow dr = dtRecipe.NewRow();

                    dr["CLINICRECIPEID"] = dsRecipeDetail.Tables[0].Rows[i]["CLINICRECIPEID"].ToString();
                    dr["MEDORDNAME"] = dsRecipeDetail.Tables[0].Rows[i]["CHARGEITEM"].ToString();
                    dr["UNITPRICE"] = Math.Round(Convert.ToDouble(dsRecipeDetail.Tables[0].Rows[i]["UNITPRICE"]), 2).ToString() + "元";
                    dr["AMOUNT"] = dsRecipeDetail.Tables[0].Rows[i]["AMOUNT"].ToString();
                    dr["DOSEUNIT"] = dsRecipeDetail.Tables[0].Rows[i]["UNIT"].ToString();
                    dr["TOTALMONEY"] = Math.Round(Convert.ToDouble(dsRecipeDetail.Tables[0].Rows[i]["TOTALMONEY"]), 2).ToString() + "元";
                    dr["OFFICE"] = dsRecipeDetail.Tables[0].Rows[i]["EXECOFFICE"].ToString();
                    dr["APPLYREMARK"] = "";
                    dr["APPLYDOCNO"] = dsRecipeDetail.Tables[0].Rows[i]["APPLYDOCNO"].ToString();
                    dr["INHOSID"] = "";
                    dr["RECIPESTATE"] = dsRecipeDetail.Tables[0].Rows[i]["RECIPESTATE"].ToString();
                    //2016年7月1日
                    dr["DOCTOR"] = dsRecipeDetail.Tables[0].Rows[i]["DOCTOR"].ToString();
                    dr["OPERATETIME"] = dsRecipeDetail.Tables[0].Rows[i]["OPERATETIME"].ToString();
                    dr["REGISTERID"] = dsRecipeDetail.Tables[0].Rows[i]["REGISTERID"].ToString();
                    dr["OPERATOROFFICE"] = dsRecipeDetail.Tables[0].Rows[i]["OPERATOROFFICE"].ToString();
                    dr["STATE"] = dsRecipeDetail.Tables[0].Rows[i]["RECIPESTATE"].ToString() == "1" ? "已收费" : "未收费";
                    dr["REMARK"] = dsRecipeDetail.Tables[0].Rows[i]["REMARK"].ToString();
                    dr["OFFICEADDRESS"] = dsRecipeDetail.Tables[0].Rows[i]["OFFICEADDRESS"].ToString();
                    dr["RECIPECONTENT"] = dsRecipeDetail.Tables[0].Rows[i]["RECIPECONTENT"].ToString();

                    dtRecipe.Rows.Add(dr);
                }
                #endregion
            }
            if (dtRecipe.Rows.Count > 0)
            {
                ClinicPhysicianRecipeFacade facade = new ClinicPhysicianRecipeFacade();
                DataTable dtableTemp = facade.GetGroupByRecipeDetail("门诊", dtRecipe);

                if (dtableTemp == null)
                {
                    SkyComm.ShowMessageInfo("执行获取组信息失败");
                    this.gdcMain.DataSource = null;
                    return false;
                }
                //wangchao modify 2016-08-15 根据开单时间和申请单号排序
                DataTable dtSort = dtableTemp.Copy();
                DataView dvSort = dtSort.DefaultView;
                dvSort.Sort = "OPERATETIME DESC,APPLYDOCNO DESC";
                dtRecipe = dvSort.ToTable();

                
                this.gdcMain.DataSource = getTrueOfficeDataTable(diagnoseidC, dtRecipe, clinicPhysicianRecipeFacade);
                //                this.gdcMain.DataSource = dtRecipe;
            }
            else
            {
                SkyComm.ShowMessageInfo("未查询到对应处方信息或该处方数据已经过期！");
                return false;
            }
            return true;
            #endregion

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

        private bool SelectItem()
        {
            if (isBindRev == false)
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
                isBindRev = true;
                #endregion
            }
            dtRev.Clear();

            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if ((Boolean)gdvMain.GetDataRow(i)["PITCHON1"]) //添加用户选择数据
                {
                    #region 增加医技预约次数限制 wangchao 2017.03.23 case:27593
                    try
                    {
                        if (SystemInfo.SystemConfigs["医技预约限制次数"].DefaultValue == "1")
                        {
                            string _execOffice = this.gdvMain.GetDataRow(i)["OFFICEID"].ToString();
                            string _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                            string _content= this.gdvMain.GetDataRow(i)["RECIPECONTENT"].ToString();
                            CLINICMtReserveFacade _reserveFacade = new CLINICMtReserveFacade();
                            DataSet dsCheck = _reserveFacade.checkRecipeHasReserveRecord(_diagnoseID, _execOffice, _content);
                            if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
                            {
                                SkyComm.ShowMessageInfo("检查项目【"+ this.gdvMain.GetDataRow(i)["MEDORDNAME"].ToString()+"】在日期【"+Convert.ToDateTime(dsCheck.Tables[0].Rows[0]["RESERVESTARTTIME"].ToString ()).ToString ("yyyy年MM月dd日")+ "】已存在预约信息，不能重复预约！");
                                return false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Skynet.LoggingService.LogService.GlobalInfoMessage("医技预约次数限制模块发生异常,原因："+ex.Message);
                    }
                    #endregion

                    DataRow newRow = dtRev.NewRow();
                    newRow["处方号"] = this.gdvMain.GetDataRow(i)["CLINICRECIPEID"].ToString();
                    newRow["处方内容"] = this.gdvMain.GetDataRow(i)["MEDORDNAME"].ToString();
                    newRow["注意事项"] = "";
                    newRow["预约号"] = "1";
                    newRow["诊疗号"] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    newRow["挂号号"] = this.gdvMain.GetDataRow(i)["REGISTERID"].ToString();
                    newRow["科室ID"] = this.gdvMain.GetDataRow(i)["OFFICEID"].ToString(); ;// this.cmbExamineName.SelectedValue.ToString(); 
                    //newRow["诊室名称"] = this.cmbExamineName.Text; 
                    newRow["诊室名称"] = this.gdvMain.GetDataRow(i)["OFFICE"].ToString();//new
                    newRow["开单科室"] = this.gdvMain.GetDataRow(i)["OPERATOROFFICE"].ToString();
                    newRow["执行科室地址"] = this.gdvMain.GetDataRow(i)["OFFICEADDRESS"].ToString();
                    newRow["医嘱用语备注"] = this.gdvMain.GetDataRow(i)["REMARK"].ToString();
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
                        var revState = this.gdvMain.GetDataRow(i)["RECIPESTATE"];
                        newRow["收费状态"] = this.gdvMain.GetDataRow(i)["RECIPESTATE"] == null ? "" : this.gdvMain.GetDataRow(i)["RECIPESTATE"].ToString() == "0" ? "未收费" : "已收费";
                    }
                    catch (Exception)
                    {
                        newRow["收费状态"] = "";
                    }
                    dtRev.Rows.Add(newRow);
                }
            }
            return true;
        }
        private decimal ReturnTotalMoney()
        {
            decimal sumMoney = 0;
            for (int i = 0; i < this.gdvMain.RowCount; i++)
            {
                if ((Boolean)gdvMain.GetDataRow(i)["PITCHON1"]) //添加用户选择数据
                {
                    if (gdvMain.GetDataRow(i)["RECIPESTATE"].ToString() != "1")
                    {
                        sumMoney += DecimalRound.Round(Convert.ToDecimal(gdvMain.GetDataRow(i)["TOTALMONEY"].ToString().Replace("元", "")), 2);
                    }

                }
            }
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
            decimal sumMoney = ReturnTotalMoney();
            lblTotalMoney.Text = string.Format("{0:0.00}", sumMoney);
            ucTime1.Sec = 60;
        }

        #endregion

        /// <summary>
        /// 更新开单科室
        /// </summary>
        /// <param name="diagnoseId"></param>
        /// <param name="dataTable"></param>
        /// <param name="clinicPhysicianRecipeFacade"></param>
        /// <returns></returns>
        public DataTable getTrueOfficeDataTable(string diagnoseId,DataTable dataTable, ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade)
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

    }
}
