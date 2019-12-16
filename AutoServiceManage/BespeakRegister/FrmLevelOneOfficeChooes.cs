using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Model;
using AutoServiceManage.Presenter;

namespace AutoServiceManage.BespeakRegister
{
    public partial class FrmLevelOneOfficeChooes : Form
    {
        private List<string> mOfficeNamelList;

        private OfficeChoosePresenter mOfficeChoosePresenter;

        private OfficeChooseModel mOfficeChooseModel;

        public FrmLevelOneOfficeChooes()
        {
            InitializeComponent();

            mOfficeChooseModel = new OfficeChooseModel();

            mOfficeChoosePresenter = new OfficeChoosePresenter();
        }

        private void FrmLevelOneOfficeChooes_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            this.levelOneOfficeList.itemClick += officeList1_itemClick;


            mOfficeNamelList = mOfficeChooseModel.getTotalOfficeNameList();



            this.levelOneOfficeList.MOfficeNamelList = mOfficeNamelList;

            this.levelOneOfficeList.officeNameBind(0);
        }

        void officeList1_itemClick(bool obj)
        {
            if (obj)
                this.ucTime1.timer1.Stop();
            else
                this.ucTime1.timer1.Start();
        }

        /// <summary>
        /// 按字母搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblA_Click(object sender, EventArgs e)
        {
            Label label = (Label) sender;

            List<string> mList = new List<string>();

            mList = mOfficeChoosePresenter.getAlphabetQueryFromLevelOne(label.Text, mOfficeNamelList);

            this.levelOneOfficeList.MOfficeNamelList = mList;

            this.levelOneOfficeList.officeNameBind(0);
        }

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plAll_Click(object sender, EventArgs e)
        {
            this.levelOneOfficeList.MOfficeNamelList = mOfficeNamelList;

            this.levelOneOfficeList.officeNameBind(0);
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLevelOneOfficeChooes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
    }
}
