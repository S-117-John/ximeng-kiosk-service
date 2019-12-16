using AutoServiceManage.BespeakRegister;
using AutoServiceManage.Common;
using BusinessFacade.His.Common;
using BusinessFacade.His.Disjoin;
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

namespace AutoServiceManage
{
    public partial class FrmOfficeChoose : Form
    {
        private string mLevelOneOficeName;//一级科室名称

        private OfficeChoosePresenter mOfficeChoosePresenter;

        public FrmOfficeChoose()
        {
            InitializeComponent();

            mOfficeChoosePresenter = new OfficeChoosePresenter();
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dtcopy = new DataTable();
        DataTable dtNumcopy = new DataTable();

        private int newPage = 1;  //当前第几页
        private int Amountpage = 0;  //一共多少页
        private bool isClickNum = false;  //是否字母检索
        private string clinkNum = string.Empty;  //记录选择的是哪个字母

        public string MLevelOneOficeName
        {
            get
            {
                return mLevelOneOficeName;
            }

            set
            {
                mLevelOneOficeName = value;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ArranageRecordFacade of = new ArranageRecordFacade();
            ds = of.FindAllArrangeInfo(SysOperatorInfo.OperatorAreaid, 1);

            string Offices = SkyComm.getvalue("不能挂号的科室");
            string[] arrOffice = Offices.Split(',');
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                if (arrOffice.Contains(row["OFFICEID"].ToString()))
                {
                    row.Delete();
                }
            }
            ds.AcceptChanges();

            if (string.IsNullOrEmpty(SkyComm.getvalue("预约挂号一级科室分类")))
            {
                
            }
            else
            {
                ds = mOfficeChoosePresenter.fliterLevelTwoOfficeDataSet(mLevelOneOficeName, ds);
            }

            

            //OfficeFacade officeFacade = new OfficeFacade();
            //ds = officeFacade.QueryByOfficeType(34, SysOperatorInfo.OperatorAreaid);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                SkyComm.ShowMessageInfo("无可挂号科室！");

                return;
            }

            this.officeList1.DtOffice = ds.Tables[0];
            this.officeList1.DataBind(0);
            
        }

        private void DataBind()
        {
            if (isClickNum == true)
            {
                dt = dtNumcopy;
            }
            else
            {
                dt = ds.Tables[0];
            }

            this.officeList1.DtOffice = dt;
            this.officeList1.DataBind(1);

            //this.menuList1.RefreshLayout();
            //foreach (var item in this.menuList1.List)
            //    item.Control.Click += Control_Click;

        }

        private void FrmOfficeChoose_Load(object sender, EventArgs e)
        {
            //setLable();

            backgroundWorker1.RunWorkerAsync();

            ucTime1.Sec = 60;

            this.officeList1.itemClick += officeList1_itemClick;

            ucTime1.timer1.Start();
        }

        void officeList1_itemClick(bool obj)
        {
            if (obj)
                this.ucTime1.timer1.Stop();
            else
                this.ucTime1.timer1.Start();
        }

        private void NumQuery(string num)
        {
            if (!string.IsNullOrEmpty(num))
            {
                dtNumcopy.Clear();
                dtNumcopy = ds.Tables[0].Clone();
                DataRow[] dr = ds.Tables[0].Select("SPELLNO LIKE '%" + num + "%'");
                if (dr.Length > 0)
                {
                    foreach (DataRow item in dr)
                    {
                        dtNumcopy.ImportRow(item);
                    }

                    isClickNum = true;

                    if (clinkNum != num)
                    {
                        newPage = 1;
                    }
                    DataBind();
                }
                else
                {
                    this.officeList1.DtOffice = null;
                    this.officeList1.DataBind(1);
                }
                clinkNum = num;
            }
            else
            {
                isClickNum = false;
                DataBind();
            }
        }

        private void plNextPage_Click(object sender, EventArgs e)
        {
            if (newPage == Amountpage)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是最后一页了！");
                frm.ShowDialog();
                return;
            }
            newPage++;
            DataBind();
        }

        #region 字母检索

        private void lblA_Click(object sender, EventArgs e)
        {
            NumQuery("A");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            NumQuery("B");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            NumQuery("C");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            NumQuery("D");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            NumQuery("E");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            NumQuery("F");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            NumQuery("G");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            NumQuery("H");
        }

        private void lblJ_Click(object sender, EventArgs e)
        {
            NumQuery("J");
        }

        private void lblk_Click(object sender, EventArgs e)
        {
            NumQuery("K");
        }

        private void lblL_Click(object sender, EventArgs e)
        {
            NumQuery("L");
        }

        private void lblM_Click(object sender, EventArgs e)
        {
            NumQuery("M");
        }

        private void lblN_Click(object sender, EventArgs e)
        {
            NumQuery("N");
        }

        private void lblO_Click(object sender, EventArgs e)
        {
            NumQuery("O");
        }

        private void lblP_Click(object sender, EventArgs e)
        {
            NumQuery("P");
        }

        private void lblQ_Click(object sender, EventArgs e)
        {
            NumQuery("Q");
        }

        private void lblR_Click(object sender, EventArgs e)
        {
            NumQuery("R");
        }

        private void lblS_Click(object sender, EventArgs e)
        {
            NumQuery("S");
        }

        private void lblT_Click(object sender, EventArgs e)
        {
            NumQuery("T");
        }

        private void lblW_Click(object sender, EventArgs e)
        {
            NumQuery("W");
        }

        private void lblX_Click(object sender, EventArgs e)
        {
            NumQuery("X");
        }

        private void lblY_Click(object sender, EventArgs e)
        {
            NumQuery("Y");
        }

        private void lblZ_Click(object sender, EventArgs e)
        {
            NumQuery("Z");
        }

        private void plAll_Click(object sender, EventArgs e)
        {
            NumQuery("");
        }

        #endregion

        private void plBeforePage_Click(object sender, EventArgs e)
        {
            if (newPage == 1)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是第一页了！");
                frm.ShowDialog();
                return;
            }
            newPage--;
            DataBind();
        }

        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmOfficeChoose_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        //private void setLable()
        //{
        //    string msg = SkyComm.getvalue("挂号提示");

        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        return;
        //    }

        //    this.label1.Text = msg;
        //}
    }
}
