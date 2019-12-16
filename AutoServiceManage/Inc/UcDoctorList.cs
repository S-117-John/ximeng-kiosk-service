using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceManage.BespeakRegister;
using EntityData.His.Register;
using Skynet.Framework.Common;
using BusinessFacade.His.Common;
using EntityData.His.Common;
using System.Resources;
using AutoServiceManage.Properties;
using System.Reflection;

namespace AutoServiceManage.Inc
{
    public partial class UcDoctorList : UserControl
    {
        public UcDoctorList()
        {
            InitializeComponent();
        }

        public event Action<bool> itemClick;

        public string office = string.Empty;

        public DateTime chooseDate;

        public BespeakRegisterData BespeakDataset = new BespeakRegisterData();

        DataTable dt = new DataTable();

        DataTable dtOfficeChoose = new DataTable();  //科室选择后的医生排班信息
        DataTable dtDateChoose = new DataTable(); //时间选择后的医师排班信息

        DataTable dtDoctor = new DataTable();
        public DataTable DtDoctor
        {
            get { return dtDoctor; }
            set { dtDoctor = value; }
        }

        private int newPage = 1;  //当前第几页
        private int Amountpage = 0;  //一共多少页

        private string strBc = SkyComm.getvalue("班次顺序");
        private string strZc = SkyComm.getvalue("职称顺序");
        private string stringData = string.Empty;

        public void DataBind(int type)
        {
            lblOffice.Text = office;

            if (type == 0)  //0 科室检索 1 当前时间检索
            {
                if (DtDoctor == null || DtDoctor.Rows.Count == 0)
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control.GetType().ToString() == "AutoServiceManage.Inc.UcDoctorItem")
                        {
                            (control as AutoServiceManage.Inc.UcDoctorItem).Visible = false;
                        }
                    }
                    Amountpage = 1;
                    newPage = 1;
                    return;
                }

                DataRow[] dr = stringData == "" ? DtDoctor.Select("EXECDATE='" + Convert.ToDateTime(DateTime.Now.ToShortDateString()) + "'") : DtDoctor.Select("EXECDATE='" + Convert.ToDateTime(DateTime.Now.Year + "-" + stringData) + "'");

                if (dr.Length > 0)
                {
                    dtOfficeChoose = dr.CopyToDataTable();

                    if (SkyComm.getvalue("挂号医生排列顺序设置").Equals("1") && dr.Length > 1)
                    {
                        dtOfficeChoose = softByConfig(dtOfficeChoose);
                    }      
                }

                else
                {
                    dtOfficeChoose = null;
                }
               

                if (dtOfficeChoose == null || dtOfficeChoose.Rows.Count == 0)
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control.GetType().ToString() == "AutoServiceManage.Inc.UcDoctorItem")
                        {
                            (control as AutoServiceManage.Inc.UcDoctorItem).Visible = false;
                        }
                    }
                    Amountpage = 1;
                    newPage = 1;
                    return;
                }
                //Skynet.LoggingService.LogService.GlobalInfoMessage("医生排班记录数0：" + dtOfficeChoose.Rows.Count);
                dt.Clear();
                dt = dtOfficeChoose.Clone();
                dt.Clear();

                //计算一共有几页

                if (dtOfficeChoose.Rows.Count <= 6)
                {
                    Amountpage = 1;
                }
                else if (dtOfficeChoose.Rows.Count % 6 == 0)
                {
                    Amountpage = dtOfficeChoose.Rows.Count / 6;
                }
                else if (dtOfficeChoose.Rows.Count % 6 > 0)
                {
                    Amountpage = (dtOfficeChoose.Rows.Count / 6) + 1;
                }

                if (newPage == Amountpage)
                {
                    if (dtOfficeChoose.Rows.Count % 6 == 0)
                    {
                        for (int i = (newPage - 1) * 6; i < (newPage * 6); i++)
                        {
                            dt.ImportRow(dtOfficeChoose.Rows[i]);
                        }
                    }
                    else
                    {
                        for (int i = (newPage - 1) * 6; i < ((newPage - 1) * 6) + (dtOfficeChoose.Rows.Count % 6); i++)
                        {
                            dt.ImportRow(dtOfficeChoose.Rows[i]);
                        }
                    }
                }
                else
                {
                    for (int i = (newPage - 1) * 6; i < newPage * 6; i++)
                    {
                        dt.ImportRow(dtOfficeChoose.Rows[i]);
                    }
                }
                //Skynet.LoggingService.LogService.GlobalInfoMessage("医生排班记录数1：" + dt.Rows.Count);
            }
            else
            {
                if (dtDateChoose == null || dtDateChoose.Rows.Count == 0)
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control.GetType().ToString() == "AutoServiceManage.Inc.UcDoctorItem")
                        {
                            (control as AutoServiceManage.Inc.UcDoctorItem).Visible = false;
                        }
                    }
                    Amountpage = 1;
                    newPage = 1;
                    return;
                }

                dt.Clear();
                dt = dtDateChoose.Clone();
                dt.Clear();

                //计算一共有几页
                if (dtDateChoose.Rows.Count <= 6)
                {
                    Amountpage = 1;
                }
                else if (dtDateChoose.Rows.Count % 6 == 0)
                {
                    Amountpage = dtDateChoose.Rows.Count / 6;
                }
                else if (dtDateChoose.Rows.Count % 6 > 0)
                {
                    Amountpage = (dtDateChoose.Rows.Count / 6) + 1;
                }

                if (newPage == Amountpage)
                {
                    for (int i = (newPage - 1) * 6; i < dtDateChoose.Rows.Count; i++)
                    {
                        dt.ImportRow(dtDateChoose.Rows[i]);
                    }
                }
                else
                {
                    for (int i = (newPage - 1) * 6; i < newPage * 6; i++)
                    {
                        dt.ImportRow(dtDateChoose.Rows[i]);
                    }
                }
                //Skynet.LoggingService.LogService.GlobalInfoMessage("医生排班记录数2：" + dt.Rows.Count);
            }

            //Skynet.LoggingService.LogService.GlobalInfoMessage("医生排班记录数2：" + dt.Rows.Count);

            SetVisable(this.DoctorItem1, true);
            setValue(dt, this.DoctorItem1, 0);

            if (dt.Rows.Count >= 2)
            {
                SetVisable(DoctorItem2, true);
                setValue(dt, this.DoctorItem2, 1);
            }
            else
            {
                SetVisable(DoctorItem2, false);
            }
            if (dt.Rows.Count >= 3)
            {
                SetVisable(DoctorItem3, true);
                setValue(dt, this.DoctorItem3, 2);
            }
            else
            {
                SetVisable(DoctorItem3, false);
            }
            if (dt.Rows.Count >= 4)
            {
                SetVisable(DoctorItem4, true);
                setValue(dt, this.DoctorItem4, 3);
            }
            else
            {
                SetVisable(DoctorItem4, false);
            }
            if (dt.Rows.Count >= 5)
            {
                SetVisable(DoctorItem5, true);
                setValue(dt, this.DoctorItem5, 4);
            }
            else
            {
                SetVisable(DoctorItem5, false);
            }
            if (dt.Rows.Count >= 6)
            {
                SetVisable(DoctorItem6, true);
                setValue(dt, this.DoctorItem6, 5);
            }
            else
            {
                SetVisable(DoctorItem6, false);
            }

            this.lblPage.Text = "第" + newPage + "页，共" + Amountpage + "页";

            if (newPage == 1)
            {
                this.plBeforePage.Enabled = false;
                this.plBeforePage.BackgroundImage = global::AutoServiceManage.Properties.Resources.lblEnablePicNew;
            }
            else
            {
                this.plBeforePage.Enabled = true;
                this.plBeforePage.BackgroundImage = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            }
            if (newPage == Amountpage)
            {
                this.plNextPage.Enabled = false;
                this.plNextPage.BackgroundImage = global::AutoServiceManage.Properties.Resources.lblEnablePicNew;
            }
            else
            {
                this.plNextPage.Enabled = true;
                this.plNextPage.BackgroundImage = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            }
        }

        private void setValue(DataTable dts, UcDoctorItem lb, int index)
        {
            lb.lblDoctorId.Text = dts.Rows[index]["DOCTORID"].ToString();
            lb.lblDoctorName.Text = dts.Rows[index]["USERNAME"].ToString();
            lb.lblWorkType.Text = dts.Rows[index]["WORKTYPE"].ToString() + "余号";
            int intYh = 0;
            if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
            {
                intYh = Convert.ToInt32(dts.Rows[index]["BECLINICMAKEMAX"].ToString()) - Convert.ToInt32(dts.Rows[index]["BECLINICMAKEMIN"].ToString());
            }
            else
            {
                if (Convert.ToDateTime(dts.Rows[index]["EXECDATE"].ToString()).Date == new CommonFacade().GetServerDateTime().Date)
                {
                    intYh = Convert.ToInt32(dts.Rows[index]["REGISTMAX"].ToString()) - Convert.ToInt32(dts.Rows[index]["REGISTREMAIN"].ToString());
                }
                else
                {
                    intYh = Convert.ToInt32(dts.Rows[index]["BECLINICMAKEMAX"].ToString()) - Convert.ToInt32(dts.Rows[index]["BECLINICMAKEMIN"].ToString());
                }
            }
            if (intYh <= 0)
            {
                lb.Enabled = false;
            }
            else
            {
                lb.Enabled = true;
            }
            lb.lblYh.Text = intYh.ToString();
            lb.lblSex.Text = dts.Rows[index]["SEX"].ToString();
            lb.lblRole.Text = dts.Rows[index]["ROLE"].ToString();
            lb.lblDetailId.Text = dts.Rows[index]["ARRANAGERECORDID"].ToString();
            lb.lblOffduty.Text = dts.Rows[index]["OFFDUTY"].ToString();
            lb.arrangeSource.Text = dts.Rows[index]["SOURCE"].ToString();
            lb.ONDUTY.Text = dts.Rows[index]["ONDUTY"].ToString();
            lb.OFFDUTY.Text = dts.Rows[index]["OFFDUTY"].ToString();

            //chenqiang case：无  解决西北妇幼分时预约线程未加载完成数据时，操作界面点击过快早成预约时间为当当天问题
            WeekItem2.Enabled = true;
            WeekItem3.Enabled = true;
            WeekItem4.Enabled = true;
            WeekItem5.Enabled = true;
            WeekItem6.Enabled = true;
            WeekItem7.Enabled = true;

        }

        private void SetVisable(UcDoctorItem lb, bool isVisable)
        {
            lb.Visible = isVisable;
        }

        private void plBeforePage_Click(object sender, EventArgs e)
        {
            if (newPage == 1)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是第一页了！");
                frm.ShowDialog();
                return;
            }
            newPage--;
            DataBind(0);
        }

        private void plNextPage_Click(object sender, EventArgs e)
        {
            if (newPage == Amountpage)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是最后一页了！");
                frm.ShowDialog();
                return;
            }
            newPage++;
            DataBind(0);
        }

        private void DoctorItem1_Click(object sender, EventArgs e)
        {
            UcDoctorItem doctor = sender as UcDoctorItem;
            DataRow dr = BespeakDataset.Tables[0].Rows[0];
            dr["BESPEAKDOCTORID"] = doctor.lblDoctorId.Text;
            dr["BESPEAKDOCTORNAME"] = doctor.lblDoctorName.Text;
            dr["BESPEAKMODE"] = "自助预约";
            dr["BESPEAKMODENAME"] = "自助预约";
            dr["WORKTYPE"] = doctor.lblWorkType.Text.Replace("余号", "");
            dr["USEMARK"] = 0;
            dr["CANCELMARK"] = 0;
            dr["INVOICEID"] = "";
            dr["OVERTYPETIMES"] = 0;
            dr["CASHDEFRAY"] = 0;
            dr["ACCOUNTDEFRAY"] = 0;
            dr["DISCOUNTDEFRAY"] = 0;
            dr["OPERATORID"] = SysOperatorInfo.OperatorID;
            dr["OPERATORNAME"] = SysOperatorInfo.OperatorName;
            dr["STATE"] = 3;
            dr["STARTTIME"] = doctor.ONDUTY.Text;
            dr["ENDTIME"] = doctor.OFFDUTY.Text;
            dr["ARRANAGERECORDID"] = doctor.lblDetailId.Text;
            dr["ROLE"] = doctor.lblRole.Text;
            if (SystemInfo.SystemConfigs["是否启用分时预约"] == null || SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "0")
            {
                DateTime dtCurrent = new CommonFacade().GetServerDateTime();
                //dr["QUEUEID"] = "";
                
                dr["BESPEAKDATE"] = Convert.ToDateTime(Convert.ToDateTime(dr["BESPEAKDATE"].ToString()).ToShortDateString() + " "+doctor.lblOffduty.Text);
                dr["OPERATEDATE"] = dtCurrent;
            }
            BespeakModeFacade bespeakModeFac = new BespeakModeFacade();
            DataSet BespeakModeDataset = (BespeakModeData)bespeakModeFac.FindAll();
            if (BespeakModeDataset.Tables[0].Rows.Count > 0)
            {
                DataRow[] drr = BespeakModeDataset.Tables[0].Select("BESPEAKMODE='自助预约'");
                if (drr.Length > 0)
                {
                    dr["BESPEAKMONEY"] = DecimalRound.Round(Convert.ToDecimal(drr[0]["BESPEAKMONEY"].ToString()), 2).ToString();
                }
            }

            string detailId = doctor.lblDetailId.Text;

            if (this.itemClick != null)
                this.itemClick(true);

            if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
            {

                FrmTimeDetailChoose frm = new FrmTimeDetailChoose();
                frm.detailId = detailId;
                frm.office = office;
                frm.DoctorRole = doctor.lblRole.Text;
                frm.BespeakDataset = BespeakDataset;
                frm.arrangeSource = doctor.arrangeSource.Text;
                frm.ShowDialog(this);
                frm.Dispose();
                if (this.itemClick != null)
                    this.itemClick(false);
            }
            else
            {
                //预约前先刷卡
                if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    FrmMain frmM = new FrmMain();
                    int intResult = SkyComm.ReadCard("预约");
                    if (intResult == 0)
                    {
                        if (this.itemClick != null)
                            this.itemClick(false);
                        return;
                    }
                }
                if (office.Contains("妇") || office.Contains("产"))
                {
                    if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count > 0 && SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString() != "女")
                    {
                        MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "当前性别【" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString() + "】不能进行【" + office + "】就诊!");
                        frmAlter.ShowDialog();
                        if (this.itemClick != null)
                            this.itemClick(false);
                        return;
                    }
                }
                //wangchao 2016.10.27 case:25866
                if (office.Contains("儿"))
                {
                    if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count > 0 && SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString().Contains("岁"))
                    {
                        string ageString = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString().Trim();
                        if (ageString != "" && Convert.ToInt32(ageString) > 18)
                        {
                            MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "当前患者年龄超过18岁,不允许就诊【" + office + "】!");
                            frmAlter.ShowDialog();
                            if (this.itemClick != null)
                                this.itemClick(false);
                            return;
                        }
                    }
                }
                FrmBespeakConfirmWithoutTimeShare frm = new FrmBespeakConfirmWithoutTimeShare();
                frm.BespeakDataset = BespeakDataset;
                frm.arrangeSource = doctor.arrangeSource.Text;
                frm.ShowDialog(this);
                frm.Dispose();
                if (this.itemClick != null)
                    this.itemClick(false);
            }
        }

        private void UcDoctorList_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            UcWeekItem item = new UcWeekItem();

            WeekItem1.lblToday.Text = "今天";
            WeekItem1.lblTodayDate.Text = dateTime.Date.ToString("MM-dd");
            #region chenqiang 2018.11.28 update by Case:33511
            if (SkyComm.getvalue("是否启用号源六天预约") == "" || SkyComm.getvalue("是否启用号源六天预约") == "0")
            {
                WeekItem2.Visible = false;
                WeekItem3.Visible = false;
                WeekItem4.Visible = false;
                WeekItem5.Visible = false;
                WeekItem6.Visible = false;
                WeekItem7.Visible = false;

            }
            else
            {
                WeekItem2.lblToday.Text = Week(dateTime.AddDays(+1).DayOfWeek.ToString());
                WeekItem2.lblTodayDate.Text = dateTime.AddDays(+1).Date.ToString("MM-dd");

                WeekItem3.lblToday.Text = Week(dateTime.AddDays(+2).DayOfWeek.ToString());
                WeekItem3.lblTodayDate.Text = dateTime.AddDays(+2).Date.ToString("MM-dd");

                WeekItem4.lblToday.Text = Week(dateTime.AddDays(+3).DayOfWeek.ToString());
                WeekItem4.lblTodayDate.Text = dateTime.AddDays(+3).Date.ToString("MM-dd");

                WeekItem5.lblToday.Text = Week(dateTime.AddDays(+4).DayOfWeek.ToString());
                WeekItem5.lblTodayDate.Text = dateTime.AddDays(+4).Date.ToString("MM-dd");

                WeekItem6.lblToday.Text = Week(dateTime.AddDays(+5).DayOfWeek.ToString());
                WeekItem6.lblTodayDate.Text = dateTime.AddDays(+5).Date.ToString("MM-dd");

                WeekItem7.lblToday.Text = Week(dateTime.AddDays(+6).DayOfWeek.ToString());
                WeekItem7.lblTodayDate.Text = dateTime.AddDays(+6).Date.ToString("MM-dd");
            }
            #endregion
            //chenqiang case：无  解决西北妇幼分时预约线程未加载完成数据时，操作界面点击过快早成预约时间为当当天问题
            WeekItem2.Enabled = false;
            WeekItem3.Enabled = false;
            WeekItem4.Enabled = false;
            WeekItem5.Enabled = false;
            WeekItem6.Enabled = false;
            WeekItem7.Enabled = false;


            //默认当前选中
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem1.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
        }

        public string Week(string dayOfWeek)
        {
            string week;
            switch (dayOfWeek)
            {
                case "Sunday":
                    week = "周日";
                    break;
                case "Monday":
                    week = "周一";
                    break;
                case "Tuesday":
                    week = "周二";
                    break;
                case "Wednesday":
                    week = "周三";
                    break;
                case "Thursday":
                    week = "周四";
                    break;
                case "Friday":
                    week = "周五";
                    break;
                case "Saturday":
                    week = "周六";
                    break;
                default:
                    week = "未知";
                    break;
            }
            return week;
        }

        private void WeekItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem1.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }

            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem1.lblTodayDate.Text);
        }

        private void weekChoose(string date)
        {
            //chenqiang 2019.03.06 add by case:32908
            stringData = date;
            if (DtDoctor == null || DtDoctor.DataSet==null)
            {
                dtDateChoose = null;
            }
            else
            {
                DataRow[] dr = DtDoctor.Select("EXECDATE='" + Convert.ToDateTime(DateTime.Now.Year + "-" + date) + "'");
                if (dr.Length > 0)
                {
                    dtDateChoose = dr.CopyToDataTable();
                    if (SkyComm.getvalue("挂号医生排列顺序设置").Equals("1")&&dr.Length>1)
                    {
                        dtDateChoose = softByConfig(dtDateChoose);
                    }      
                }
                else
                {
                    dtDateChoose = null;
                }
            }

            if (BespeakDataset.Tables.Count > 0 && BespeakDataset.Tables[0].Rows.Count > 0) 
            {
                BespeakDataset.Tables[0].Rows[0].BeginEdit();
                BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"] = Convert.ToDateTime(DateTime.Now.Year + "-" + date);
                BespeakDataset.Tables[0].Rows[0].EndEdit();
            }
            DataBind(1);
        }

        #region wangchenyang 2018/6/5 case 31111 妇幼自助机挂号变更挂号医生排序规则
        /// <summary>
        /// 根据系统配置排序
        /// </summary>
        /// <returns></returns>
        private DataTable softByConfig(DataTable dtOld)
        {
            try
            {
                Dictionary<string, Int16> dicBc = new Dictionary<string, Int16>();
                Dictionary<string, Int16> dicZc = new Dictionary<string, Int16>();
                //职称排序规则
                createDicSoft(strZc, dicZc);
                //班次排序规则
                createDicSoft(strBc, dicBc);
                DataTable dtSoft = dtOld.Copy();

                dtSoft.Columns.Add(new DataColumn("bcSoft", typeof(Int16)));//班次
                dtSoft.Columns.Add(new DataColumn("zcSoft", typeof(Int16)));//职称
                dtSoft.Columns.Add(new DataColumn("hySoft", typeof(Int16)));//号源    
                foreach (DataRow item in dtSoft.Rows)
                {
                    Int16 softBc = dicBc.Where(p => p.Key.Equals(item.Field<string>("worktype"))).Select(n => n.Value).FirstOrDefault();//班次
                    item["bcSoft"] = softBc == 0 ? Int16.MaxValue : softBc;//没写如配置信息的默认往后排序

                    Int16 softZc = dicZc.Where(p => p.Key.Equals(item.Field<string>("role"))).Select(n => n.Value).FirstOrDefault();//职称
                    item["zcSoft"] = softZc == 0 ? Int16.MaxValue : softZc;//没写如配置信息的默认往后排序
                    //余号判断
                    int intYh = 0;
                    if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
                    {
                        intYh = Convert.ToInt32(item["BECLINICMAKEMAX"].ToString()) - Convert.ToInt32(item["BECLINICMAKEMIN"].ToString());
                    }
                    else
                    {
                        if (Convert.ToDateTime(item["EXECDATE"].ToString()).Date == new CommonFacade().GetServerDateTime().Date)
                        {
                            intYh = Convert.ToInt32(item["REGISTMAX"].ToString()) - Convert.ToInt32(item["REGISTREMAIN"].ToString());
                        }
                        else
                        {
                            intYh = Convert.ToInt32(item["BECLINICMAKEMAX"].ToString()) - Convert.ToInt32(item["BECLINICMAKEMIN"].ToString());
                        }
                    }
                    item["hySoft"] = intYh==0 ? 0 : 1;//号源 0为无号源，1为有号源,先显示有号源数据                 
                }
                //开始排序,排序规则 号源>班次>职称>姓名
                return dtSoft.AsEnumerable().OrderByDescending(p => p.Field<Int16>("hySoft")).ThenBy(a => a.Field<Int16>("bcSoft")).
                    ThenBy(b => b.Field<Int16>("zcSoft")).ThenBy(c => c.Field<string>("spellno")).CopyToDataTable();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 创建配置规则字典
        /// </summary>
        /// <param name="str">配置字符串</param>
        /// <param name="dic">配置字典</param>
        /// <returns></returns>
        private Dictionary<string, Int16> createDicSoft(string str, Dictionary<string, Int16> dic) 
        {
            try
            {
                Dictionary<string, Int16> dicSoft = dic;
                //班次排序规则
                string[] strBcs = str.Split(">".ToCharArray());
                if (strBcs.Length <= 0)
                {

                }
                Int16 i = 1;
                foreach (var item in strBcs)
                {
                    dicSoft.Add(item.Trim(), i);
                    i++;
                }
                return dicSoft;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }          
        }
        #endregion

        private void WeekItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem2.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem2.lblTodayDate.Text);
        }

        private void WeekItem3_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem3.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }

            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem3.lblTodayDate.Text);
        }

        private void WeekItem4_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem4.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem4.lblTodayDate.Text);
        }

        private void WeekItem5_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem5.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem5.lblTodayDate.Text);
        }

        private void WeekItem6_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem6.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem6.lblTodayDate.Text);
        }

        private void WeekItem7_Click(object sender, EventArgs e)
        {
            try
            {
                Image img = (Image)Resources.日期背景;
                WeekItem7.BackgroundImage = img;
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            newPage = 1;
            Amountpage = 0;
            weekChoose(WeekItem7.lblTodayDate.Text);
        }
    }
}
