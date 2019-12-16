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
    public partial class FrmWebLoad : Form
    {
        public FrmWebLoad()
        {
            InitializeComponent();
        }
        private int sec = 60;

        private void FrmWebLoad_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FrmWebLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec = sec - 1;
            btnClose.Text = "返回(" + sec + ")";
            if (sec == 0)
            {
                this.Close();
            }

        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            sec = 60;
        }
    }
}
