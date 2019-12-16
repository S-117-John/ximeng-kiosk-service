using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using BusinessFacade.His.CardClubManager;
using EntityData.His.CardClubManager;

namespace AutoServiceManage.InCard
{
    public partial class FrmReadCardTest : Form
    {
        public FrmReadCardTest()
        {
            InitializeComponent();
            this.ucTime1.label1.Visible = false;
            this.ucTime1.timer1.Start();
        }

        private void txtCardID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (string.IsNullOrEmpty(txtCardID.Text.Trim()))
                {
                    lblerr.Text = "卡号不能为空，请输入卡号!";
                    txtCardID.Focus();
                    return;
                }
                SkyComm.cardInfoStruct.CardNo = txtCardID.Text.Trim();
                this.ucTime1.timer1.Stop();
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
                //DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
                
        private void txtCardID_TextChanged(object sender, EventArgs e)
        {
            lblerr.Visible = false;
        }

        private void FrmReadCardTest_Shown(object sender, EventArgs e)
        {
            this.txtCardID.Focus();
        }

        private void closePort()
        {
            SkyComm.cardInfoStruct.CardNo = string.Empty;
            if (SkyComm.eCardAuthorizationData.Tables.Count > 0)
                SkyComm.eCardAuthorizationData.Tables[0].Clear();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CardRead cardUtility = new CardRead(this);
                string strMsg = cardUtility.GetPatiantInfo();
                if (!string.IsNullOrEmpty(strMsg))
                {
                    SkyComm.ShowMessageInfo(strMsg);
                    e.Result = "失败";
                    closePort();
                }
                else
                {
                    e.Result = "成功";
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo(ex.Message);
                closePort();

                e.Result = "失败";

                return;

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result.Equals("失败"))
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
