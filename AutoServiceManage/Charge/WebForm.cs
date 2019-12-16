using Skynet.AreaDataExchangeInterfaces.TianXuanCheckWebservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Charge
{
    public partial class WebForm : Form
    {
        public WebForm()
        {
            InitializeComponent();
        }

        private void WebForm_Load(object sender, EventArgs e)
        {

            string URL = new TianXuanWebservice().SelfHelpmachine(SkyComm.DiagnoseID);
            this.webBrowser1.Navigate(URL);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
    }
}
