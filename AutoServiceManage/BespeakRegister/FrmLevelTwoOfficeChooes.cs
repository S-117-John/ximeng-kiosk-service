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
    public partial class FrmLevelTwoOfficeChooes : Form
    {
        private string mLevelOneOficeName;//一级科室名称

        private OfficeChoosePresenter mOfficeChoosePresenter;

        private DataTable mOfficNameDataTable;

        public FrmLevelTwoOfficeChooes()
        {
            InitializeComponent();

            mOfficeChoosePresenter = new OfficeChoosePresenter();
        }

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

        private void FrmLevelTwoOfficeChooes_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            this.levelTwoOfficeList.itemClick += officeList1_itemClick;


            this.levelTwoOfficeList.MLevelOneOficeName = mLevelOneOficeName;
            this.backgroundWorkerGetOfficeName.RunWorkerAsync();
        }

        void officeList1_itemClick(bool obj)
        {
            if (obj)
                this.ucTime1.timer1.Stop();
            else
                this.ucTime1.timer1.Start();
        }

        /// <summary>
        /// 获取二级科室线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerGetOfficeName_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = mOfficeChoosePresenter.getLevelTwoOfficeDataTable(mLevelOneOficeName);
        }

        private void backgroundWorkerGetOfficeName_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error !=null)
            {
                SkyComm.ShowMessageInfo(e.Error.Message);
                return;
            }

            DataTable mDataTable = (DataTable) e.Result;

            mOfficNameDataTable = mDataTable;

            this.levelTwoOfficeList.MLeveTwoOfficeNameDataTable = mDataTable;
            this.levelTwoOfficeList.officeNameBind(0);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelTwoOfficeChooes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        /// <summary>
        /// 字母检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblA_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            DataTable mDataTable = new DataTable();

            mDataTable = mOfficeChoosePresenter.getAlphabetQueryFromLevelTwo(label.Text, mOfficNameDataTable);

            this.levelTwoOfficeList.MLeveTwoOfficeNameDataTable = mDataTable;

            this.levelTwoOfficeList.officeNameBind(0);
        }

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plAll_Click(object sender, EventArgs e)
        {
            this.levelTwoOfficeList.MLeveTwoOfficeNameDataTable = mOfficNameDataTable;
            this.levelTwoOfficeList.officeNameBind(0);
        }
    }
}
