using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessFacade.His.Common;
using AutoServiceManage.Common;
using AutoServiceManage.BespeakRegister;
using AutoServiceManage.Model;
using EntityData.His.Common;

namespace AutoServiceManage.Inc
{
    public partial class OfficeList : UserControl
    {
        

        public event Action<bool> itemClick;

        DataTable dt = new DataTable();

        private PatientInfoFacade patientFacade = new PatientInfoFacade();

        private PatientInfoData patientData = new PatientInfoData();

        private DataTable dtOffice = new DataTable();
        public DataTable DtOffice
        {
            get { return dtOffice; }
            set { dtOffice = value; }
        }
        private int newPage = 1;  //当前第几页
        private int Amountpage = 0;  //一共多少页

        public OfficeList()
        {
            InitializeComponent();

            
        }


       


        /// <summary>
        /// type 0 翻页检索 1 字母检索
        /// </summary>
        /// <param name="type"></param>
        public void DataBind(int type)
        {
            if (DtOffice == null || DtOffice.Rows.Count == 0)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.GetType().ToString() == "AutoServiceManage.Inc.OfficeItem")
                    {
                        (control as AutoServiceManage.Inc.OfficeItem).Visible = false;
                    }
                }
                Amountpage = 1;
                newPage = 1;
                return;
            }

            if (type == 1)
                newPage = 1;
            dt.Clear();
            dt = DtOffice.Clone();
            dt.Clear();

            //计算一共有几页
            if (DtOffice.Rows.Count <= 20)
            {
                Amountpage = 1;
            }
            else if (DtOffice.Rows.Count % 20 == 0)
            {
                Amountpage = DtOffice.Rows.Count / 20;
            }
            else if (DtOffice.Rows.Count % 20 > 0)
            {
                Amountpage = (DtOffice.Rows.Count / 20) + 1;
            }

            if (newPage == Amountpage)
            {
                for (int i = (newPage - 1) * 20; i < DtOffice.Rows.Count; i++)
                {
                    dt.ImportRow(DtOffice.Rows[i]);
                }
            }
            else
            {
                for (int i = (newPage - 1) * 20; i < newPage * 20; i++)
                {
                    dt.ImportRow(DtOffice.Rows[i]);
                }
            }

            setValue(dt, this.office1, 0);
            SetVisable(office1, true);

            if (dt.Rows.Count >= 2)
            {
                SetVisable(office2, true);
                setValue(dt, this.office2, 1);
            }
            else
            {
                SetVisable(office2, false);
            }
            if (dt.Rows.Count >= 3)
            {
                SetVisable(office3, true);
                setValue(dt, this.office3, 2);
            }
            else
            {
                SetVisable(office3, false);
            }
            if (dt.Rows.Count >= 4)
            {
                SetVisable(office4, true);
                setValue(dt, this.office4, 3);
            }
            else
            {
                SetVisable(office4, false);
            }
            if (dt.Rows.Count >= 5)
            {
                SetVisable(office5, true);
                setValue(dt, this.office5, 4);
            }
            else
            {
                SetVisable(office5, false);
            }
            if (dt.Rows.Count >= 6)
            {
                SetVisable(office6, true);
                setValue(dt, this.office6, 5);
            }
            else
            {
                SetVisable(office6, false);
            }
            if (dt.Rows.Count >= 7)
            {
                SetVisable(office7, true);
                setValue(dt, this.office7, 6);
            }
            else
            {
                SetVisable(office7, false);
            }
            if (dt.Rows.Count >= 8)
            {
                SetVisable(office8, true);
                setValue(dt, this.office8, 7);
            }
            else
            {
                SetVisable(office8, false);
            }
            if (dt.Rows.Count >= 9)
            {
                SetVisable(office9, true);
                setValue(dt, this.office9, 8);
            }
            else
            {
                SetVisable(office9, false);
            }
            if (dt.Rows.Count >= 10)
            {
                SetVisable(office10, true);
                setValue(dt, this.office10, 9);
            }
            else
            {
                SetVisable(office10, false);
            }
            if (dt.Rows.Count >= 11)
            {
                SetVisable(office11, true);
                setValue(dt, this.office11, 10);
            }
            else
            {
                SetVisable(office11, false);
            }
            if (dt.Rows.Count >= 12)
            {
                SetVisable(office12, true);
                setValue(dt, this.office12, 11);
            }
            else
            {
                SetVisable(office12, false);
            }
            if (dt.Rows.Count >= 13)
            {
                SetVisable(office13, true);
                setValue(dt, this.office13, 12);
            }
            else
            {
                SetVisable(office13, false);
            }
            if (dt.Rows.Count >= 14)
            {
                SetVisable(office14, true);
                setValue(dt, this.office14, 13);
            }
            else
            {
                SetVisable(office14, false);
            }
            if (dt.Rows.Count >= 15)
            {
                SetVisable(office15, true);
                setValue(dt, this.office15, 14);
            }
            else
            {
                SetVisable(office15, false);
            }
            if (dt.Rows.Count >= 16)
            {
                SetVisable(office16, true);
                setValue(dt, this.office16, 15);
            }
            else
            {
                SetVisable(office16, false);
            }
            if (dt.Rows.Count >= 17)
            {
                SetVisable(office17, true);
                setValue(dt, this.office17, 16);
            }
            else
            {
                SetVisable(office17, false);
            }
            if (dt.Rows.Count >= 18)
            {
                SetVisable(office18, true);
                setValue(dt, this.office18, 17);
            }
            else
            {
                SetVisable(office18, false);
            }
            if (dt.Rows.Count >= 19)
            {
                SetVisable(office19, true);
                setValue(dt, this.office19, 18);
            }
            else
            {
                SetVisable(office19, false);
            }
            if (dt.Rows.Count >= 20)
            {
                SetVisable(office20, true);
                setValue(dt, this.office20, 19);
            }
            else
            {
                SetVisable(office20, false);
            }

            this.lblPage.Text ="第" +newPage + "页，共" + Amountpage+"页";

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

        private void setValue(DataTable dts, OfficeItem lb, int index)
        {
            lb.lblOffice.Text = dts.Rows[index]["OFFICE"] == null ? "无" : dts.Rows[index]["OFFICE"].ToString();
            lb.lblOfficeId.Text = dts.Rows[index]["OFFICEID"].ToString();
        }
        //设置科室可见性
        private void SetVisable(OfficeItem lb, bool isVisable)
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

        private void office1_Click(object sender, EventArgs e)
        {
            if (this.itemClick != null)
                this.itemClick(true);
            OfficeItem list = sender as OfficeItem;
            string oficeId = list.lblOfficeId.Text;
            string ofice = list.lblOffice.Text;

            try 
            {
                string[] officename = SkyComm.getvalue("科室挂号开始时间").Split('|');
                for (int i = 0; i < officename.Length; i++)
                {
                    if (ofice == officename[i].Split('/')[0].ToString())
                    {
                        DateTime Hourset = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + officename[i].Split('/')[1].ToString());//拼接凑成正确时间格式
                        DateTime Hournow = new CommonFacade().GetServerDateTime();  
                        if (Hournow < Hourset)
                        {
                            MyAlert frm1 = new MyAlert(AlertTypeenum.信息, "未到放号时间，暂时不能挂号！");
                            frm1.ShowDialog();
                            return;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyAlert frm1 = new MyAlert(AlertTypeenum.信息, "科室挂号开始时间配置有误,请检查！");
                frm1.ShowDialog();
                return;
            }
            
            //douyaming 2016-05-05 CASE:24356
            if (ofice.Contains("妇") || ofice.Contains("产"))
            {
                if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count > 0 && SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString() != "女")
                {
                    MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "当前性别【" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString() + "】不能进行【" + ofice + "】就诊!");
                    frmAlter.ShowDialog();
                    return;
                }
            }
            //wangchao 2016.10.27 case:25866
            if (ofice.Contains("儿"))
            {
                if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count > 0 && SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString().Contains("岁"))
                {
                    string ageString = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString().Trim();
                    if (ageString != "" && Convert.ToInt32(ageString) > 18)
                    {
                        MyAlert frmAlter = new MyAlert(AlertTypeenum.信息, "当前患者年龄超过18岁,不允许就诊【" + ofice + "】!");
                        frmAlter.ShowDialog();
                        return;
                    }
                }
            }

            string mOfficeMessage = "";

            mOfficeMessage = SkyComm.getvalue(oficeId);

            if (!string.IsNullOrEmpty(mOfficeMessage))
            {
               SkyComm.ShowMessageInfo(mOfficeMessage); 
            }

            FrmDoctorChoose frm = new FrmDoctorChoose();
            frm.officeId = oficeId;
            frm.office = ofice;
            frm.ShowDialog(this);
            frm.Dispose();
            if (this.itemClick != null)
                this.itemClick(false);
        }

    }
}
