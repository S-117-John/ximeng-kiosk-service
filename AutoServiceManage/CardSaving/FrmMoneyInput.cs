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
    public partial class FrmMoneyInput : Form
    {
        public FrmMoneyInput()
        {
            InitializeComponent();
        }

        public decimal inputMoney = 0;
        /// <summary>
        /// 调用类型：0：主界面，2:办卡
        /// </summary>
        public int CallType { get; set; }

        private string strMaxAmt = SkyComm.getvalue("银联卡充值上限");


        private void lbl50_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "50";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl100_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "100";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl200_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "200";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl500_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "500";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl1000_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "1000";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl2000_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "2000";
            this.lblMoney.ForeColor = Color.Black;
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string num = lbl.Name.Substring(lbl.Name.Length - 1, 1);
            this.lblMoney.ForeColor = Color.Black;
            InputText(num);
        }

        private void InputText(string num)
        {
            if (lblMoney.Text.IndexOf("金额") > 0)
            {
                lblMoney.Text = num;
            }
            else
            {
                lblMoney.Text = lblMoney.Text + num;
            }
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (lblMoney.Text.IndexOf("金额") < 0)
            {
                string monetyValue = lblMoney.Text.Substring(0, lblMoney.Text.Length - 1);
                lblMoney.Text = monetyValue;
                if (lblMoney.Text.Length == 0)
                {
                    lblMoney.Text = "请输入转账金额";
                    this.lblMoney.ForeColor = System.Drawing.SystemColors.ControlLight;
                }
            }
        }

        private void lblReSet_Click(object sender, EventArgs e)
        {
            lblMoney.Text = "请输入转账金额";
            this.lblMoney.ForeColor = System.Drawing.SystemColors.ControlLight;
        }

        private void lblOK_Click(object sender, EventArgs e)

        {
            if (lblMoney.Text.IndexOf("金额") < 0)
            {
                //wangchenyang 31408 工行住院自助机银联卡充值上限最高为2000元，医院要求上限设置为10000元
                decimal dDefault = 2000;
                decimal.TryParse(strMaxAmt, out dDefault);
                if (Convert.ToDecimal(lblMoney.Text) > dDefault)
                {
                    SkyComm.ShowMessageInfo(String.Format("单次充值金额不能大于{0}！", dDefault));
                    return;
                }

                //if (Convert.ToDecimal(lblMoney.Text) > 2000) 
                //{
                //    SkyComm.ShowMessageInfo("单次充值金额不能大于2000！");
                //    return;
                //}
                inputMoney = Convert.ToDecimal(lblMoney.Text);

                if (inputMoney < 50 && CallType == 2)
                {
                    SkyComm.ShowMessageInfo("办卡充值金额必须大于50！");
                    return;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmMoneyInput_Load(object sender, EventArgs e)
        {
            label1.Text = !string.IsNullOrEmpty(strMaxAmt) ? String.Format("最高金额：{0:N2}", strMaxAmt) : "最高金额：2000.00";
        }
    }
}
