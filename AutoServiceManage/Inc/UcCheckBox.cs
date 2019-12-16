using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inc
{
    public partial class UcCheckBox : UserControl
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public Boolean Checked { get; set; }

        public UcCheckBox()
        {
            InitializeComponent();
        }

        public event Action<bool> CheckedChanged;

        private void UcCheckBox_Click(object sender, EventArgs e)
        {
            if (Checked == true)
            {
                Checked = false;
                this.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxFalse;
            }
            else
            {
                Checked = true;
                this.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxTrue;

            }
            if (CheckedChanged != null)
                this.CheckedChanged(this.Checked);
        }
      
        private void btnCheckBox_Click(object sender, EventArgs e)
        {
            if (Checked == true)
            {
                Checked = false;
                this.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxFalse;
            }
            else
            {
                Checked = true;
                this.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxTrue;

            }
            if (CheckedChanged != null)
                this.CheckedChanged(this.Checked);
        }
    }
}
