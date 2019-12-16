using AutoServiceManage.Common;
using BusinessFacade.His.Common;
using BusinessFacade.His.Disjoin;
using BusinessFacade.His.Register;
using EntityData.His.Common;
using EntityData.His.Register;
using Skynet.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TiuWeb.ReportBase;
using System.Threading;
using System.Collections;

namespace AutoServiceManage.Charge
{
    public partial class FrmReserveConfirm : Form
    {
        #region 自定义变量

        public string OfficeName { get; set; }//开单科室
        public string ExOfficeName { get; set; }//执行科室
        public string reserveItem { get; set; }//预约项目
        public string reserveDate { get; set; }//预约时间
        public string reserveGroup { get; set; }//预约分组
        //public string queueNO { get; set; }//排队号
        public string CostMoney { get; set; }//金额
        public string ExOfficeID { get; set; }//执行科室ID
        public string OfficeAddress { get; set; }//科室位置
        public string queueNO;
        #endregion

        public FrmReserveConfirm()
        {
            InitializeComponent();
        }

        private void FrmReserveConfirm_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            this.lblPatientName.Text ="姓名："+ SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
            this.lblSex.Text ="性别："+ SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
            this.lblOffice.Text = "开单科室：" + OfficeName;
            this.lblExOffice.Text = "执行科室：" + ExOfficeName;
            this.lblReserveItem.Text = "预约项目：" + reserveItem;
            this.lblReserveDate.Text = "预约时间：" + reserveDate;
            this.lblGroup.Text = "预约分组：" + reserveGroup;
            this.lblMoney.Text = "金额：" + CostMoney+"元";
            this.lblOfficeDddress.Text = "科室位置：" + OfficeAddress;

        }

        private void pcReturn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ucTime1.timer1.Stop();
            SkyComm.CloseWin(this);
        }

        private void lblOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void FrmReserveConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            this.ucTime1.timer1.Stop();
        }

        private void lblRemark_Click(object sender, EventArgs e)
        {

        }
    }
}
