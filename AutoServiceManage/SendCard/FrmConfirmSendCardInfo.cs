using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkData;

namespace AutoServiceManage.SendCard
{
    public partial class FrmConfirmSendCardInfo : Form
    {
        #region 初始化
        public FrmConfirmSendCardInfo()
        {
            InitializeComponent();
        }

        public FrmConfirmSendCardInfo(IDCardInfo idinfo, string tel)
        {
            InitializeComponent();
            this.lblxm.Text = idinfo.Name;
            this.lblsex.Text = idinfo.Sex;
            this.lblmz.Text = idinfo.People;
            this.lblzfzh.Text = idinfo.Number;
            this.lblcsrq.Text = idinfo.Birthday;
            this.lbldz.Text = idinfo.Address;
            this.lbltel.Text = tel;
        }
        private void FrmConfirmSendCardInfo_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
        }

        private void FrmConfirmSendCardInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucTime1.timer1.Stop();
        }

        #endregion

        #region 确认
        private void lblOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion

        #region 返回，退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        #endregion

        
    }
}
