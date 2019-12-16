using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.SystemManage;

namespace AutoServiceManage.Inc
{
    public partial class Ucfoot : UserControl
    {
        private bool ManageFormControl = false;
        public Ucfoot()
        {
            InitializeComponent();
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour < 12)
            {
                this.lblTIME.Text = DateTime.Now.ToShortTimeString();
            }
            else
            {
                this.lblTIME.Text = DateTime.Now.ToShortTimeString();
            }
        }

        private void label17_DoubleClick(object sender, EventArgs e)
        {
            if (!ManageFormControl)
            {
                return;
            }
            ManageFormControl = false;
            if (!string.IsNullOrEmpty(SkyComm.DiagnoseID))
            {
                return;
            }
            this.timer1.Stop();
            string getChoose = string.Empty;
            //退出系统，验证密码
            FrmInputPassword frm = new FrmInputPassword();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                frm.Dispose();
                this.timer1.Start();
                return;
            }
            getChoose = frm.CheckType;
            frm.Dispose();

            if (getChoose == "1")
            {

//                SkyComm.CloseReader();//关闭读卡器
                this.ParentForm.Close();
            }
            else if (getChoose == "2")
            {
                FrmDoorlook frm2 = new FrmDoorlook();
                frm2.ShowDialog();
                frm2.Dispose();
            }
            else if (getChoose == "3")
            {
                FrmBalance frm2 = new FrmBalance();
                frm2.ShowDialog();
                frm2.Dispose();
            }
            this.timer1.Start();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
//            FrmInputPassword frm = new FrmInputPassword();
//            frm.CheckType = "1";
//            if (frm.ShowDialog() != DialogResult.OK)
//            {
//                frm.Dispose();
//                return;
//            }
//            frm.Dispose();
//
//            if (frm.CheckType == "1")
//            {
//                FrmDoorlook frm2 = new FrmDoorlook();
//                frm2.ShowDialog();
//                frm2.Dispose();
//            }
//            else
//            {
//                FrmBalance frm2 = new FrmBalance();
//                frm2.ShowDialog();
//                frm2.Dispose();
//            }
        }

        private void picTwLogo_Click(object sender, EventArgs e)
        {
            if (!ManageFormControl)
            {
                ManageFormControl = true;
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ManageFormControl = false;
            timer2.Stop();
        }
    }
}
