using AutoServiceManage.Common;
using BusinessFacade.His.Common;
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

namespace AutoServiceManage.BespeakRegister
{
    public partial class FrmDoctorChoose : Form
    {
        private DoctorChoosePresenter mDoctorChoosePresenter;

        public BespeakRegisterData BespeakDataset;

        public OfficeFacade officeFacade;

        public string officeId;

        public string office;

        public FrmDoctorChoose()
        {
            InitializeComponent();

            mDoctorChoosePresenter = new DoctorChoosePresenter();
        }

        private void FrmDoctorChoose_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;

            this.ucDoctorList2.itemClick += ucDoctorList2_itemClick;

            ucTime1.timer1.Start();

            BespeakDataset = new BespeakRegisterData();

            officeFacade = new OfficeFacade();

            backgroundWorker1.RunWorkerAsync();
        }

        void ucDoctorList2_itemClick(bool obj)
        {
            if (obj)
                this.ucTime1.timer1.Stop();
            else
                this.ucTime1.timer1.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ArranageRecordFacade arrageRecordFacade = new ArranageRecordFacade();
            string id = officeId;
            DataSet dsArrange = arrageRecordFacade.FindArrangeInfoByOfficeId(officeId, DateTime.Now.AddDays(7));  //"1130"   officeId
            string Doctors = SkyComm.getvalue("不能挂号的医生USERID");
            string[] ArrDoctor = Doctors.Split(',');
            foreach (DataRow row in dsArrange.Tables[0].Rows)
            { 
                if (row.RowState == DataRowState.Deleted)
                    continue;
                if (ArrDoctor.Contains(row["DOCTORID"].ToString()))
                {
                    row.Delete();
                }
            }
            dsArrange.AcceptChanges();
            if (dsArrange.Tables.Count > 0 && dsArrange.Tables[0].Rows.Count > 0)
            {
                //此处将医生排序
                string mDoctorConfig1 = SkyComm.getvalue("专家挂号");
                string mDoctorConfig2 = SkyComm.getvalue("普通挂号");
                if (string.IsNullOrEmpty(mDoctorConfig1))
                {
                    e.Result = dsArrange;
                }
                else
                {
                    e.Result = mDoctorChoosePresenter.getNewSortDataSet(dsArrange);
                }

                
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ucDoctorList2.office = office;

            if (e.Result == null)
            {
                ucDoctorList2.DtDoctor = null;
                ucDoctorList2.DataBind(0);
            }
            else
            {
                BespeakDataset.Clear();
                DataRow dr = BespeakDataset.Tables[0].NewRow();
                dr["BESPEAKID"] = string.Empty;
                dr["DIAGNOSEID"] = string.Empty;
                dr["BESPEAKOFFICENAME"] = office;
                dr["BESPEAKOFFICEID"] = officeId;

                dr["PATIENTNAME"] = "";
                dr["IDENTITYCARD"] = "";
                dr["TELEPHONE"] = "";
                dr["ADDRESS"] = "";
                dr["SEX"] = "";
                dr["BESPEAKDOCTORID"] = "";
                dr["BESPEAKDOCTORNAME"] = "";
                dr["BESPEAKMODE"] = "";
                dr["BESPEAKMODENAME"] = "";
                dr["WORKTYPE"] = "";
                dr["USEMARK"] = 0;
                dr["BESPEAKMONEY"] = 0;
                dr["CANCELMARK"] = 0;
                dr["INVOICEID"] = "";
                dr["OVERTYPETIMES"] = 0;
                dr["CASHDEFRAY"] = 0;
                dr["ACCOUNTDEFRAY"] = 0;
                dr["DISCOUNTDEFRAY"] = 0;
                dr["OPERATORID"] = "";
                dr["OPERATORNAME"] = "";
                dr["STATE"] = 3;
                dr["ARRANAGERECORDID"] = "";
                dr["QUEUEID"] = 0;
                dr["BESPEAKDATE"] = new CommonFacade().GetServerDateTime();

                if (!BespeakDataset.Tables[0].Columns.Contains("ROLE"))
                {
                    BespeakDataset.Tables[0].Columns.Add("ROLE");
                }
                if (!BespeakDataset.Tables[0].Columns.Contains("OFFICEADDRESS"))
                {
                    BespeakDataset.Tables[0].Columns.Add("OFFICEADDRESS");
                }

                string officeaddress = string.Empty;
                DataSet dsofficaddreddress = officeFacade.FindOfficeInfo(officeId);
                if (dsofficaddreddress.Tables.Count > 0 && dsofficaddreddress.Tables[0].Rows.Count > 0) 
                {
                    officeaddress = dsofficaddreddress.Tables[0].Rows[0]["OFFICEADDRESS"].ToString();
                }
                dr["OFFICEADDRESS"] = officeaddress;

                BespeakDataset.Tables[0].Rows.Add(dr);

                ucDoctorList2.BespeakDataset = BespeakDataset;
                ucDoctorList2.DtDoctor = ((DataSet)e.Result).Tables[0];
                ucDoctorList2.DataBind(0);
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

        private void FrmDoctorChoose_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
    }
}
