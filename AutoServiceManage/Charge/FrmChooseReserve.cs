using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkService;
using BusinessFacade.His.Clinic;
using BusinessFacade.His.ClinicDoctor;

namespace AutoServiceManage.Charge
{
    public partial class FrmChooseReserve : Form
    {
        public FrmChooseReserve()
        {
            InitializeComponent();
        }

        private void FrmChooseReserve_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblCashStored_Click(object sender, EventArgs e)
        {
            ChooseReserve("超声预约科室ID");
        }
        private void lblTXJH_Click(object sender, EventArgs e)
        {
            ChooseReserve("胎心监护预约科室ID");
        }

        private void lblQXL_Click(object sender, EventArgs e)
        {
            ChooseReserve("脐血流预约科室ID");
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmChooseReserve_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
        /// <summary>
        /// 选择医技预约类别
        /// </summary>
        /// <param name="OfficeName"></param>
        private void ChooseReserve(string OfficeName)
        {
            this.ucTime1.timer1.Stop();

            //判断打印机是否有纸
            if (AutoHostConfig.ReadCardType == "XUHUI")
            {
                PrintManage_XH thePrintManage = new PrintManage_XH();
                string CheckInfo = thePrintManage.CheckPrintStatus();
                if (!string.IsNullOrEmpty(CheckInfo))
                {
                    SkyComm.ShowMessageInfo(CheckInfo);
                    return;
                }
            }

            if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
            {
                FrmMain frmM = new FrmMain();
                int intResult = SkyComm.ReadCard("医技预约");
                if (intResult == 0)
                {
                    this.ucTime1.timer1.Start();
                    return;
                }
            }

            FrmReserveMain frm = new FrmReserveMain();
            frm.OfficeType = OfficeName;
            if (frm.ShowDialog(this) == DialogResult.Cancel)
            {
                this.ucTime1.timer1.Start();
            }
            frm.Dispose();
        }

        private void lblXD_Click(object sender, EventArgs e)
        {
            ChooseReserve("心电预约科室ID");
        }

        private void labelYYGQ_Click(object sender, EventArgs e)
        {
            string diagnoseid = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString().Trim();  //诊疗号
            CLINICMtReserveFacade reserveFacade = new CLINICMtReserveFacade();
            DataSet ds = new DataSet();
            try
            {
                ds = reserveFacade.checkReserveRecord(diagnoseid);
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("没有预约信息,请先预约!");
                return;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ucTime1.timer1.Stop();
                //判断打印机是否有纸
                if (AutoHostConfig.ReadCardType == "XUHUI")
                {
                    PrintManage_XH thePrintManage = new PrintManage_XH();
                    string CheckInfo = thePrintManage.CheckPrintStatus();
                    if (!string.IsNullOrEmpty(CheckInfo))
                    {
                        SkyComm.ShowMessageInfo(CheckInfo);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    FrmMain frmM = new FrmMain();
                    int intResult = SkyComm.ReadCard("医技预约");
                    if (intResult == 0)
                    {
                        this.ucTime1.timer1.Start();
                        return;
                    }
                }

                FrmUpdateReserveMain frm = new FrmUpdateReserveMain();
                if (frm.ShowDialog(this) == DialogResult.Cancel)
                {
                    this.ucTime1.timer1.Start();
                }


            }
            else
            {
                SkyComm.ShowMessageInfo("没有预约信息,请先预约!");
                return;
            }


        }
    }
}
