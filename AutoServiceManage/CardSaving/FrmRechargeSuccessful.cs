using AutoServiceManage.Charge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmRechargeSuccessful : Form
    {
        public FrmRechargeSuccessful()
        {
            InitializeComponent();
        }

        public decimal RechargeMoney = 0;
        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmRechargeSuccessful_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;

            ucTime1.timer1.Start();

            this.lblRechargeMoney.Text = "您已成功充值" + RechargeMoney + "元";

            SkyComm.GetCardBalance();

            this.lblye.Text = SkyComm.cardBlance.ToString() + "元";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SkyComm.CardSavingType = 1;
            SkyComm.CloseWin(this);

          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SkyComm.CardSavingType = 2;
            SkyComm.CloseWin(this);
        }

        private void FrmRechargeSuccessful_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
        }
    }
}
