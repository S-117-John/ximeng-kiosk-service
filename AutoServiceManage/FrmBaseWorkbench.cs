using AutoServiceManage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage
{
    public partial class FrmBaseWorkbench : Form
    {
        public FrmBaseWorkbench()
        {
            InitializeComponent();
        }

        public int Countdown = 0;  //倒计时

        private void FrmBaseWorkbench_Load(object sender, EventArgs e)
        {
            //加载Logo
            try
            {
                pcImageLogo.Image = Image.FromFile(Application.StartupPath + "\\" + SkyComm.getvalue("logoImage"));
            }
            catch { pcImageLogo.Visible = false; }

            //加载医院名称
            try
            {
                lblbiaoti.Text = SkyComm.getvalue("医院名称") + "自助服务";
            }
            catch { lblbiaoti.Text = ""; }

            //倒计时
            if (Countdown == 0)
            {
                lblTime.Text = "60";
            }
            else
            {
                lblTime.Text = Countdown.ToString();
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(lblTime.Text);
            time--;
            if (time == 0)
            {
                this.Close();
            }
            else
            {
                lblTime.Text = time.ToString();
            }
        }
    }
}
