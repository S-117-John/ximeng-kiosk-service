using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inc
{
    public partial class UcTime : UserControl
    {
        private int sec = 60;
        //自动关闭时间
        public int Sec
        {
            get { return sec; }
            set { sec = value; }
        }

        private bool autoClose;
        //是否自动关闭
        public bool AutoClose
        {
            get { return autoClose; }
            set { autoClose = value; }
        }

        public UcTime()
        {            
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (sec == 0)
            {
                // if (SkyComm.cardInfoStruct.isIdentityCard )
                //{
                //    SkyComm.ClearCookie();
                //}
                this.timer1.Stop();
                this.ParentForm.Close();
            }
            this.lblWaitTime.Text = sec.ToString();
            sec = sec - 1;
        }

        private void UcTime_Load(object sender, EventArgs e)
        {
            this.lblWaitTime.Text = sec.ToString();
            if (AutoClose)
            {
                this.lblWaitTime.Visible = true;
            }
            sec = sec - 1;
        }
    }
}
