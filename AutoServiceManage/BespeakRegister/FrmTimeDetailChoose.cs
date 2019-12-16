using BusinessFacade.His.Disjoin;
using EntityData.His.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Presenter;
using Skynet.Framework.Common;

namespace AutoServiceManage.BespeakRegister
{
    public partial class FrmTimeDetailChoose : Form
    {
        private TimeDetailChoosePresenter mTimeDetailChoosePresenter;

        public FrmTimeDetailChoose()
        {
            InitializeComponent();

            mTimeDetailChoosePresenter = new TimeDetailChoosePresenter();
        }

        public BespeakRegisterData BespeakDataset = new BespeakRegisterData();

        public string detailId;

        public string office;
        public string DoctorRole { get; set; }
        public string arrangeSource = string.Empty;//HIS排班数据来源

        private void FrmTimeDetailChoose_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();

            ucTime1.Sec = 60;

            this.ucTimeDetailList1.itemClick += ucTimeDetailList1_itemClick;

            ucTime1.timer1.Start();

            this.lblUserName.Text = BespeakDataset.Tables[0].Rows[0]["BESPEAKDOCTORNAME"].ToString();
            this.lblDoctorRole.Text = DoctorRole;
        }

        void ucTimeDetailList1_itemClick(bool obj)
        {
            if (obj)
                this.ucTime1.timer1.Stop();
            else
                this.ucTime1.timer1.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ArranageRecordFacade arrangeFac = new ArranageRecordFacade();
            DataSet dsArrage = arrangeFac.FindArrageDetailByarrangerecordId(detailId);
            int ReservedDays = Convert.ToInt32(SystemInfo.SystemConfigs["预约预留现场当天号源个数"].DefaultValue);
            if (dsArrage.Tables.Count > 0 && dsArrage.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToDateTime(BespeakDataset.Tables[0].Rows[0]["BESPEAKDATE"].ToString()).Date != DateTime.Now.Date)
                {
                    dsArrage.Tables[0].DefaultView.RowFilter = "QUEUEID>" + ReservedDays + "";
                }

                
                e.Result = mTimeDetailChoosePresenter.reArrayDatas(dsArrage).Tables[0].DefaultView.ToTable();//号源重新安排
//                                e.Result = dsArrage.Tables[0].DefaultView.ToTable();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                ucTimeDetailList1.DtTimeDetail = null;
                ucTimeDetailList1.DataBind();
            }
            else
            {
                DataRow dr = BespeakDataset.Tables[0].Rows[0];
                dr["ARRANAGERECORDID"] = detailId;
                ucTimeDetailList1.BespeakDataset = BespeakDataset;
                ucTimeDetailList1.office = office;
                ucTimeDetailList1.arrangeSource = arrangeSource;
                ucTimeDetailList1.DtTimeDetail = ((DataTable)e.Result);
                ucTimeDetailList1.DataBind();
            }
        }

        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmTimeDetailChoose_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
    }
}
