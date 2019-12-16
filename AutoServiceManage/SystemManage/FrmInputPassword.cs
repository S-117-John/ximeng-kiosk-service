using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessFacade.His.Common;
using Skynet.Framework.Common;

namespace AutoServiceManage.SystemManage
{
    public partial class FrmInputPassword : Form
    {
        #region 变量

        /// <summary>
        /// 密码验证类型：1：门锁，维护，2：银行,3:退出系统
        /// </summary>
        public string CheckType { get; set; }

        private int Sec = 60;
        #endregion

        #region 构造函数

        public FrmInputPassword()
        {
            InitializeComponent();
        }

        private void FrmInputPassword_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.txtPassword.Focus();//密码焦点
        }

        private void FrmInputPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Sec = Sec - 1;
            txtSec.Text = Sec.ToString();
            if (Sec == 0)
            {
                timer1.Stop();
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

        }
        #endregion

        #region 输入数字控制
        private void lbl0_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string num = lbl.Name.Substring(lbl.Name.Length - 1, 1);
            txtPassword.Text = txtPassword.Text + num;
            Sec = 60;
        }

        private void lblReSet_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            Sec = 60;
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length > 0)
            {
                string PasswordValue = txtPassword.Text.Substring(0, txtPassword.Text.Length - 1);
                txtPassword.Text = PasswordValue;
            }
            Sec = 60;
        }

        #endregion

        #region 确定，取消
        private void btnOK_Click(object sender, EventArgs e)
        {
            Sec = 60;
            if (rbtnClose.Checked)
            {
                if (this.txtPassword.Text == AutoHostConfig.ClosePassword)
                {
                    timer1.Stop();
                    CheckType = "1";
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    SkyComm.ShowMessageInfo("密码输入不正确！");
                    txtPassword.Text = "";
                }
            }
            else if (rbtnOpendoor.Checked)
            {
                if (this.txtPassword.Text == AutoHostConfig.OpenDoorPassword)
                {
                    timer1.Stop();
                    CheckType = "2";
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    SkyComm.ShowMessageInfo("密码输入不正确！");
                    txtPassword.Text = "";
                }
            }
            else
            {
                if (CheckPassWord())
                {
                    timer1.Stop();
                    CheckType = "3";
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    txtPassword.Text = "";
                }
            }

        }

        private bool CheckPassWord()
        {
            try
            {
                OperatorFacade operatorFacade = new OperatorFacade();
                DataSet opds = operatorFacade.GetLoginOperatorInfo(SysOperatorInfo.OperatorCode);
                if (opds.Tables[0].Rows.Count == 0)
                {
                    SkyComm.ShowMessageInfo("找不到该用户！");
                    return false;
                }
                DataRow dr = opds.Tables[0].Rows[0];
                if (dr == null)
                {
                    SkyComm.ShowMessageInfo("错误的用户名！");
                    return false;
                }
                else if (EncryptDecrypt.Decrypt(dr["PASSWORD"].ToString()) != this.txtPassword.Text)
                {
                    SkyComm.ShowMessageInfo("密码输入不正确！");
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion  
    }
}
