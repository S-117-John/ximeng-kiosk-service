using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.SystemManage
{
    public partial class FrmDoorlook : Form
    {
        public FrmDoorlook()
        {
            InitializeComponent();
        }
        private int Sec = 60;
        Common_XH theCommon_XH = new Common_XH();
        private void btnOpen_Click(object sender, EventArgs e)
        {
            Sec = 60;
            int intIndex = 0;
            if (btn1.Checked == true)
            {
                intIndex = 1;
            }
            else if (btn2.Checked == true)
            {
                intIndex = 2;
            }
            else if (btn3.Checked == true)
            {
                intIndex = 3;
            }
            if (intIndex == 0)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo("请选择需要打开的门锁！");
                return;
            }

            theCommon_XH.Doorlock(intIndex, 1);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Sec = 60;
            int intIndex = 0;
            if (btn1.Checked == true)
            {
                intIndex = 1;
            }
            else if (btn2.Checked == true)
            {
                intIndex = 2;
            }
            else if (btn3.Checked == true)
            {
                intIndex = 3;
            }
            if (intIndex == 0)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo("请选择需要打开的门锁！");
                return;
            }
            theCommon_XH.Doorlock(intIndex, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Sec = Sec - 1;
            btnExit.Text = "退出(" + Sec + ")";

            if (Sec == 0)
            {
                this.Close();
            }
        }

        private void FrmDoorlook_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FrmDoorlook_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
