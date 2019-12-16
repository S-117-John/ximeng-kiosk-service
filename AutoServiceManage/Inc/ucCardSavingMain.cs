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
    public partial class ucCardSavingMain : UserControl
    {
        public ucCardSavingMain()
        {
            InitializeComponent();
        }

        private void ucCardSavingMain_Load(object sender, EventArgs e)
        {
        }

        private void menuList1_Load(object sender, EventArgs e)
        {

            this.menuList1.RefreshLayout();
        }
    }
}
