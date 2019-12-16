using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skynet.Framework.Common;

namespace AutoServiceManage.Inc
{
    public partial class UcBespeakItem : UserControl
    {

        public UcBespeakItem()
        {
            InitializeComponent();

            this.ckbSelect.CheckedChanged += ckbSelect_CheckedChanged;
        }

        void ckbSelect_CheckedChanged(bool obj)
        {
            Row["SELECT"] = obj;

            if (this.moneyChanged != null)
                moneyChanged(decMoney, obj);
        }

        public decimal decMoney = 0;
        public string BespeakID = string.Empty;
        public DataRow Row { get; set; }
        public event Action<decimal, bool> moneyChanged;
        public void SetData(DataRow row)
        {
            Row = row;
            this.lbloffice.Text = Row["OFFICE"].ToString();
            this.lblDoctor.Text = Row["USERNAME"].ToString();
            if (SystemInfo.SystemConfigs["是否启用分时预约"] != null && SystemInfo.SystemConfigs["是否启用分时预约"].DefaultValue == "1")
            {
                this.lblXH.Text = Row["QUEUEID"].ToString();
            }
            else
            {
                this.lblXH.Visible = false;
                this.label4.Visible = false;
                this.label2.Visible = false;
            }
            this.lblBespeakdate.Text = Convert.ToDateTime(Row["BESPEAKDATE"]).ToString("MM-dd HH:mm");
            decMoney = DecimalRound.Round(Convert.ToDecimal(row["ALLCOST"]), 2);
            lblMoney.Text = decMoney.ToString();
            if (Convert.ToBoolean(Row["SELECT"]) == true)
            {
                ckbSelect.Checked = true;
                this.ckbSelect.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxTrue;
            }
            else
            {
                ckbSelect.Checked = false;
                this.ckbSelect.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxFalse;
            }            
        }

        private void ckbSelect_Click(object sender, EventArgs e)
        {
            //if (ckbSelect.Checked == true)
            //{
            //    Row["SELECT"] = true;
            //    ckbSelect.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxTrue;
            //}
            //else
            //{
            //    Row["SELECT"] = false;
            //    ckbSelect.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxFalse;
            //}
            //if (this.moneyChanged != null)
            //    moneyChanged(decMoney, ckbSelect.Checked);
        }

        private void lblBespeakdate_Click(object sender, EventArgs e)
        {
            //if (ckbSelect.Checked == true)
            //    Row["SELECT"] = true;
            //else
            //    Row["SELECT"] = false;
            //if (this.moneyChanged != null)
            //    moneyChanged(decMoney, ckbSelect.Checked);
        }

    }

}
