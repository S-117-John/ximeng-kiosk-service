using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityData.His.CardClubManager;

namespace AutoServiceManage.CardSaving
{
    public partial class FrmChooseMoney : Form
    {
        private decimal chooseMoney;//所选金额

        public FrmChooseMoney()
        {
            InitializeComponent();
        }

        public decimal ChooseMoney
        {
            get
            {
                return chooseMoney;
            }

            set
            {
                chooseMoney = value;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                Label mLabel = (Label)sender;
                if (mLabel.Text.Contains("."))
                {
                    this.chooseMoney = Convert.ToDecimal(mLabel.Text.Substring(0, mLabel.Text.Length - 1));
                }
                else
                {
                    this.chooseMoney = Convert.ToDecimal(mLabel.Text.Substring(0, mLabel.Text.Length - 1) + ".00");
                }
                
                this.chooseMoney = Math.Round(chooseMoney, 2);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {

                this.chooseMoney = 0;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmChooseMoney_Load(object sender, EventArgs e)
        {
            this.txtCardNo.Text = SkyComm.cardInfoStruct.CardNo;
            this.txtName.Text = SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][
                CardAuthorizationData.T_CARD_AUTHORIZATION_PATIENTNAME].ToString();
        }
    }
}
