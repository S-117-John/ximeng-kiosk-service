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
    public partial class FrmReserveAlert : Form
    {
        int sec;
        public string reserveDate { get; set; }
        public string queueNO { get; set; }
        public string GroupName { get; set; }
        public string CostMoney { get; set; }
        public FrmReserveAlert()
        {
            InitializeComponent();
            
             sec = 30;
        }
        
        private void FrmReserveAlert_Load(object sender, EventArgs e)
        {
                this.lblTitle.Text = "预约成功";
            this.lblmsg.Text = "您已预约"+reserveDate.Split(' ')[0].ToString ()+"日"+GroupName+reserveDate.Split(' ')[1].ToString ()+" "+queueNO+"号超声项目 ，请提前半小时报到。";
            this.lblMoney.Text = CostMoney + "元";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sec == 0)
            {
                this.timer1.Stop();
                this.DialogResult = DialogResult.OK;
            }            
            sec = sec - 1;
            this.btnClose.Text = "关闭(" + sec.ToString() + ")";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void lblmsg_Click(object sender, EventArgs e)
        {

        }       
    }   
}
