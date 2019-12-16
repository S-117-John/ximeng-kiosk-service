using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inquire
{
    public partial class FrmInquireMain : Form
    {
        public FrmInquireMain()
        {
            InitializeComponent();
        }

        private void lblStoredInquire_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();
                int intResult = SkyComm.ReadCard("预存查询");
                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }
            
            FrmStoredInquire frm = new FrmStoredInquire();
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();
        }

        private void lblChargeDetailInquire_Click(object sender, EventArgs e)
        {
            this.ucTime1.timer1.Stop();
            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();
                int intResult = SkyComm.ReadCard("收费明细查询");
                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            FrmChargeDetailInquire frm = new FrmChargeDetailInquire();
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmInquireMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
    }
}
