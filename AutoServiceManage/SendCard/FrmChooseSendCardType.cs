using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.SendCard
{
    public partial class FrmChooseSendCardType : Form
    {
        public FrmChooseSendCardType()
        {
            InitializeComponent();
        }

        private void FrmChooseSendCardType_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblCR_Click(object sender, EventArgs e)
        {
            ChooseSendCardType("成人");
        }

        private void lblET_Click(object sender, EventArgs e)
        {
            ChooseSendCardType("儿童");
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmChooseSendCardType_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
        /// <summary>
        /// 选择办卡类别
        /// </summary>
        /// <param name="sendCardType"></param>
        private void ChooseSendCardType(string sendCardType)
        {
            this.ucTime1.timer1.Stop();

            //判断打印机是否有纸
            if (AutoHostConfig.ReadCardType == "XUHUI")
            {
                PrintManage_XH thePrintManage = new PrintManage_XH();
                string CheckInfo = thePrintManage.CheckPrintStatus();
                if (!string.IsNullOrEmpty(CheckInfo))
                {
                    SkyComm.ShowMessageInfo(CheckInfo);
                    return;
                }
            }

            FrmSendCardMain frm = new FrmSendCardMain();
            frm.SendCardType = sendCardType;
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();
        }


    }
}
