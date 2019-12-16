using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using EntityData.His.Register;
using BusinessFacade.His.Common;
using AutoServiceManage.BespeakRegister;

namespace AutoServiceManage.Inc
{
    public partial class UcTimeDetailList : UserControl
    {
        public UcTimeDetailList()
        {
            InitializeComponent();
        }

        public event Action<bool> itemClick;
        public string arrangeSource = string.Empty;//HIS排班数据来源

        public BespeakRegisterData BespeakDataset = new BespeakRegisterData();

        DataTable dt = new DataTable();

        DataTable dtTimeDetail = new DataTable();

        public string office = string.Empty;
        public DataTable DtTimeDetail
        {
            get { return dtTimeDetail; }
            set { dtTimeDetail = value; }
        }

        private int newPage = 1;  //当前第几页
        private int Amountpage = 0;

        private void plBeforePage_Click(object sender, EventArgs e)
        {
            if (newPage == 1)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是第一页了！");
                frm.ShowDialog();
                return;
            }
            newPage--;
            DataBind();
        }

        public void DataBind()
        {
            if (DtTimeDetail == null || DtTimeDetail.Rows.Count == 0)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.GetType().ToString() == "AutoServiceManage.Inc.UcTimeDetailItem")
                    {
                        (control as AutoServiceManage.Inc.UcTimeDetailItem).Visible = false;
                    }
                }
                Amountpage = 1;
                newPage = 1;
                return;
            }

            dt.Clear();
            dt = dtTimeDetail.Clone();
            dt.Clear();

            //计算一共有几页
            if (dtTimeDetail.Rows.Count <= 20)
            {
                Amountpage = 1;
            }
            else if (dtTimeDetail.Rows.Count % 20 == 0)
            {
                Amountpage = dtTimeDetail.Rows.Count / 20;
            }
            else if (dtTimeDetail.Rows.Count % 20 > 0)
            {
                Amountpage = (dtTimeDetail.Rows.Count / 20) + 1;
            }

            if (newPage == Amountpage)
            {
                for (int i = (newPage - 1) * 20; i < dtTimeDetail.Rows.Count; i++)
                {
                    dt.ImportRow(dtTimeDetail.Rows[i]);
                }
            }
            else
            {
                for (int i = (newPage - 1) * 20; i < newPage * 20; i++)
                {
                    dt.ImportRow(dtTimeDetail.Rows[i]);
                }
            }

            SetVisable(this.TimeDetailItem1, true);
            setValue(dt, this.TimeDetailItem1, 0);

            if (dt.Rows.Count >= 2)
            {
                SetVisable(TimeDetailItem2, true);
                setValue(dt, this.TimeDetailItem2, 1);
            }
            else
            {
                SetVisable(TimeDetailItem2, false);
            }
            if (dt.Rows.Count >= 3)
            {
                SetVisable(TimeDetailItem3, true);
                setValue(dt, this.TimeDetailItem3, 2);
            }
            else
            {
                SetVisable(TimeDetailItem3, false);
            }
            if (dt.Rows.Count >= 4)
            {
                SetVisable(TimeDetailItem4, true);
                setValue(dt, this.TimeDetailItem4, 3);
            }
            else
            {
                SetVisable(TimeDetailItem4, false);
            }
            if (dt.Rows.Count >= 5)
            {
                SetVisable(TimeDetailItem5, true);
                setValue(dt, this.TimeDetailItem5, 4);
            }
            else
            {
                SetVisable(TimeDetailItem5, false);
            }
            if (dt.Rows.Count >= 6)
            {
                SetVisable(TimeDetailItem6, true);
                setValue(dt, this.TimeDetailItem6, 5);
            }
            else
            {
                SetVisable(TimeDetailItem6, false);
            }
            if (dt.Rows.Count >= 7)
            {
                SetVisable(TimeDetailItem7, true);
                setValue(dt, this.TimeDetailItem7, 6);
            }
            else
            {
                SetVisable(TimeDetailItem7, false);
            }
            if (dt.Rows.Count >= 8)
            {
                SetVisable(TimeDetailItem8, true);
                setValue(dt, this.TimeDetailItem8, 7);
            }
            else
            {
                SetVisable(TimeDetailItem8, false);
            }
            if (dt.Rows.Count >= 9)
            {
                SetVisable(TimeDetailItem9, true);
                setValue(dt, this.TimeDetailItem9, 8);
            }
            else
            {
                SetVisable(TimeDetailItem9, false);
            }
            if (dt.Rows.Count >= 10)
            {
                SetVisable(TimeDetailItem10, true);
                setValue(dt, this.TimeDetailItem10, 9);
            }
            else
            {
                SetVisable(TimeDetailItem10, false);
            }
            if (dt.Rows.Count >= 11)
            {
                SetVisable(TimeDetailItem11, true);
                setValue(dt, this.TimeDetailItem11, 10);
            }
            else
            {
                SetVisable(TimeDetailItem11, false);
            }
            if (dt.Rows.Count >= 12)
            {
                SetVisable(TimeDetailItem12, true);
                setValue(dt, this.TimeDetailItem12, 11);
            }
            else
            {
                SetVisable(TimeDetailItem12, false);
            }
            if (dt.Rows.Count >= 13)
            {
                SetVisable(TimeDetailItem13, true);
                setValue(dt, this.TimeDetailItem13, 12);
            }
            else
            {
                SetVisable(TimeDetailItem13, false);
            }
            if (dt.Rows.Count >= 14)
            {
                SetVisable(TimeDetailItem14, true);
                setValue(dt, this.TimeDetailItem14, 13);
            }
            else
            {
                SetVisable(TimeDetailItem14, false);
            }
            if (dt.Rows.Count >= 15)
            {
                SetVisable(TimeDetailItem15, true);
                setValue(dt, this.TimeDetailItem15, 14);
            }
            else
            {
                SetVisable(TimeDetailItem15, false);
            }
            if (dt.Rows.Count >= 16)
            {
                SetVisable(TimeDetailItem16, true);
                setValue(dt, this.TimeDetailItem16, 15);
            }
            else
            {
                SetVisable(TimeDetailItem16, false);
            }
            if (dt.Rows.Count >= 17)
            {
                SetVisable(TimeDetailItem17, true);
                setValue(dt, this.TimeDetailItem17, 16);
            }
            else
            {
                SetVisable(TimeDetailItem17, false);
            }
            if (dt.Rows.Count >= 18)
            {
                SetVisable(TimeDetailItem18, true);
                setValue(dt, this.TimeDetailItem18, 17);
            }
            else
            {
                SetVisable(TimeDetailItem18, false);
            }
            if (dt.Rows.Count >= 19)
            {
                SetVisable(TimeDetailItem19, true);
                setValue(dt, this.TimeDetailItem19, 18);
            }
            else
            {
                SetVisable(TimeDetailItem19, false);
            }
            if (dt.Rows.Count >= 20)
            {
                SetVisable(TimeDetailItem20, true);
                setValue(dt, this.TimeDetailItem20, 19);
            }
            else
            {
                SetVisable(TimeDetailItem20, false);
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

        private void plNextPage_Click(object sender, EventArgs e)
        {
            if (newPage == Amountpage)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是最后一页了！");
                frm.ShowDialog();
                return;
            }
            newPage++;
            DataBind();
        }

        private void SetVisable(UcTimeDetailItem lb, bool isVisable)
        {
            lb.Visible = isVisable;
        }

        private void setValue(DataTable dts, UcTimeDetailItem lb, int index)
        {
            lb.lblOrder.Text = dts.Rows[index]["QUEUEID"].ToString();
            lb.lblTime.Text = dts.Rows[index]["DETAILTIME"].ToString();
            lb.arranageDetailSource.Text = dts.Rows[index]["SOURCE"].ToString();
        }

        private void TimeDetailItem1_Click(object sender, EventArgs e)
        {
            UcTimeDetailItem doctor = sender as UcTimeDetailItem;
            string order = doctor.lblOrder.Text;
            string time = doctor.lblTime.Text;

            DataRow dr = BespeakDataset.Tables[0].Rows[0];
            dr["QUEUEID"] = order;
            DateTime dtCurrent = new CommonFacade().GetServerDateTime();
            //if (Convert.ToDateTime(time).TimeOfDay < dtCurrent.TimeOfDay)
            //{
            //    time = dtCurrent.AddMinutes(20).TimeOfDay.ToString();
            //}
            dr["BESPEAKDATE"] = Convert.ToDateTime(Convert.ToDateTime(dr["BESPEAKDATE"].ToString()).ToShortDateString() + " " + time);

            //Case #27421
            string mEndTime = "";
            foreach (DataRow row in DtTimeDetail.Rows)
            {
                mEndTime = row["DETAILTIME"].ToString();
            }

            DateTime mDateTime = Convert.ToDateTime(Convert.ToDateTime(dr["BESPEAKDATE"]).ToString("yyyy-MM-dd") + " " + mEndTime);
            if (Convert.ToDateTime(dr["BESPEAKDATE"]) > mDateTime)
            {
                dr["BESPEAKDATE"] = mDateTime;
            }
            dr["OPERATEDATE"] = dtCurrent;


            if (this.itemClick != null)
                this.itemClick(true);
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

            FrmBespeakConfirm frm = new FrmBespeakConfirm();
            frm.BespeakDataset = BespeakDataset;
            frm.arrangeSource = arrangeSource;
            frm.arranageDetailSource = doctor.arranageDetailSource.Text;
            frm.ShowDialog(this);
            frm.Dispose();
            if (this.itemClick != null)
                this.itemClick(false);
        }
    }
}
