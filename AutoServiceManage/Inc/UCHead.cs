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
    public partial class UCHead : UserControl
    {
        public UCHead()
        {
            InitializeComponent();

            try
            {
                //picTwLogo.Image = Image.FromFile(Application.StartupPath + "\\TiuWeblogo.png");
                //pcImageLogo.Image = Image.FromFile(Application.StartupPath + "\\TiuWeblogo.png");
                pcImageLogo.Image = Image.FromFile(Application.StartupPath + "\\" + SkyComm.getvalue("logoImage"));
                
            }
            catch { pcImageLogo.Visible = false; }

            //加载医院名称
            //try
            //{
            //    lblTitle.Text = SkyComm.getvalue("医院名称") + "自助服务";
            //}
            //catch { lblTitle.Text = ""; }
        }
    }
}
