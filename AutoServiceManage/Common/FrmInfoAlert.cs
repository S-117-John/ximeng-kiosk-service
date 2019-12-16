using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Common
{
    public partial class FrmInfoAlert : Form
    {
        public string Title { get; set; }
        public string Msg { get; set; }
        public int sec { get; set; }

        public FrmInfoAlert()
        {
            InitializeComponent();
            
            sec = 30;
        }
        
        private void FrmInfoAlert_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Title))
                this.lblTitle.Text = Title;
            this.lblmsg.Text = Msg;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sec == 0)
            {
                this.timer1.Stop();
                this.DialogResult = DialogResult.OK;
            }            
            sec = sec - 1;
            this.btnClose.Text = "关闭(" + sec.ToString() + ")";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }       
    }   
}
