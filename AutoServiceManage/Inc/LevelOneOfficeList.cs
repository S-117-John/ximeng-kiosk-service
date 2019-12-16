using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.BespeakRegister;
using AutoServiceManage.Common;
using AutoServiceManage.Model;

namespace AutoServiceManage.Inc
{
    public partial class LevelOneOfficeList : UserControl
    {
        public event Action<bool> itemClick;

        private OfficeChooseModel mOfficeChooseModel;

        private List<string> mOfficeNamelList;

        private int newPage = 1;  //当前第几页
        private int Amountpage = 0;  //一共多少页

        public List<string> MOfficeNamelList
        {
            get
            {
                return mOfficeNamelList;
            }

            set
            {
                mOfficeNamelList = value;
            }
        }

        public LevelOneOfficeList()
        {
            InitializeComponent();

            mOfficeChooseModel = new OfficeChooseModel();
        }

        /// <summary>
        /// 显示一级科室菜单
        /// type 0 翻页检索 1 字母检索
        /// </summary>
        /// <param name="type"></param>
        public void officeNameBind(int type)
        {
           

            if (mOfficeNamelList == null || mOfficeNamelList.Count == 0)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.GetType().ToString() == "AutoServiceManage.Inc.OfficeItem")
                    {
                        (control as AutoServiceManage.Inc.OfficeItem).Visible = false;
                    }
                }
                Amountpage = 1;
                newPage = 1;

                this.lblPage.Text = newPage + "/" + Amountpage;

                return;
            }

            List<string> mlList = new List<string>();

            if (type == 1)
                newPage = 1;

            //计算一共有几页
            if (mOfficeNamelList.Count <= 20)
            {
                Amountpage = 1;
            }
            else if (mOfficeNamelList.Count % 20 == 0)
            {
                Amountpage = mOfficeNamelList.Count / 20;
            }
            else if (mOfficeNamelList.Count % 20 > 0)
            {
                Amountpage = (mOfficeNamelList.Count / 20) + 1;
            }

            if (newPage > Amountpage)
            {
                newPage = 1;
            }

            if (newPage == Amountpage)
            {
                for (int i = (newPage - 1) * 20; i < mOfficeNamelList.Count; i++)
                {
                    mlList.Add(mOfficeNamelList[i]);
                    
                }
            }
            else
            {
                for (int i = (newPage - 1) * 20; i < newPage * 20; i++)
                {
                    mlList.Add(mOfficeNamelList[i]);
                }
            }

            setOfficeName(mlList, office1, 0);
            SetVisable(office1, true);

            if (mlList.Count >= 2)
            {
                SetVisable(office2, true);
                setOfficeName(mlList, this.office2, 1);
            }
            else
            {
                SetVisable(office2, false);
            }
            if (mlList.Count >= 3)
            {
                SetVisable(office3, true);
                setOfficeName(mlList, this.office3, 2);
            }
            else
            {
                SetVisable(office3, false);
            }
            if (mlList.Count >= 4)
            {
                SetVisable(office4, true);
                setOfficeName(mlList, this.office4, 3);
            }
            else
            {
                SetVisable(office4, false);
            }
            if (mlList.Count >= 5)
            {
                SetVisable(office5, true);
                setOfficeName(mlList, this.office5, 4);
            }
            else
            {
                SetVisable(office5, false);
            }
            if (mlList.Count >= 6)
            {
                SetVisable(office6, true);
                setOfficeName(mlList, this.office6, 5);
            }
            else
            {
                SetVisable(office6, false);
            }
            if (mlList.Count >= 7)
            {
                SetVisable(office7, true);
                setOfficeName(mlList, this.office7, 6);
            }
            else
            {
                SetVisable(office7, false);
            }
            if (mlList.Count >= 8)
            {
                SetVisable(office8, true);
                setOfficeName(mlList, this.office8, 7);
            }
            else
            {
                SetVisable(office8, false);
            }
            if (mlList.Count >= 9)
            {
                SetVisable(office9, true);
                setOfficeName(mlList, this.office9, 8);
            }
            else
            {
                SetVisable(office9, false);
            }
            if (mlList.Count >= 10)
            {
                SetVisable(office10, true);
                setOfficeName(mlList, this.office10, 9);
            }
            else
            {
                SetVisable(office10, false);
            }
            if (mlList.Count >= 11)
            {
                SetVisable(office11, true);
                setOfficeName(mlList, this.office11, 10);
            }
            else
            {
                SetVisable(office11, false);
            }
            if (mlList.Count >= 12)
            {
                SetVisable(office12, true);
                setOfficeName(mlList, this.office12, 11);
            }
            else
            {
                SetVisable(office12, false);
            }
            if (mlList.Count >= 13)
            {
                SetVisable(office13, true);
                setOfficeName(mlList, this.office13, 12);
            }
            else
            {
                SetVisable(office13, false);
            }
            if (mlList.Count >= 14)
            {
                SetVisable(office14, true);
                setOfficeName(mlList, this.office14, 13);
            }
            else
            {
                SetVisable(office14, false);
            }
            if (mlList.Count >= 15)
            {
                SetVisable(office15, true);
                setOfficeName(mlList, this.office15, 14);
            }
            else
            {
                SetVisable(office15, false);
            }
            if (mlList.Count >= 16)
            {
                SetVisable(office16, true);
                setOfficeName(mlList, this.office16, 15);
            }
            else
            {
                SetVisable(office16, false);
            }
            if (mlList.Count >= 17)
            {
                SetVisable(office17, true);
                setOfficeName(mlList, this.office17, 16);
            }
            else
            {
                SetVisable(office17, false);
            }
            if (mlList.Count >= 18)
            {
                SetVisable(office18, true);
                setOfficeName(mlList, this.office18, 17);
            }
            else
            {
                SetVisable(office18, false);
            }
            if (mlList.Count >= 19)
            {
                SetVisable(office19, true);
                setOfficeName(mlList, this.office19, 18);
            }
            else
            {
                SetVisable(office19, false);
            }
            if (mlList.Count >= 20)
            {
                SetVisable(office20, true);
                setOfficeName(mlList, this.office20, 19);
            }
            else
            {
                SetVisable(office20, false);
            }
            this.lblPage.Text = "第" + newPage + "页，共" + Amountpage + "页";

            if (newPage == 1)
            {
                this.plBeforePage.Enabled = false;
                this.plBeforePage.BackgroundImage = global::AutoServiceManage.Properties.Resources.lblEnablePicNew;
            }
            else
            {
                this.plBeforePage.Enabled = true;
                this.plBeforePage.BackgroundImage = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            }
            if (newPage == Amountpage)
            {
                this.plNextPage.Enabled = false;
                this.plNextPage.BackgroundImage = global::AutoServiceManage.Properties.Resources.lblEnablePicNew;
            }
            else
            {
                this.plNextPage.Enabled = true;
                this.plNextPage.BackgroundImage = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            }
        }

        /// <summary>
        /// 设置一级科室名称
        /// </summary>
        /// <param name="list"></param>
        /// <param name="lb"></param>
        /// <param name="index"></param>
        private void setOfficeName(List<string> list, OfficeItem lb, int index)
        {
            lb.lblOffice.Text = list[index] == null ? "无" : list[index];

        }

        //设置科室可见性
        private void SetVisable(OfficeItem lb, bool isVisable)
        {
            lb.Visible = isVisable;
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void office1_Click(object sender, EventArgs e)
        {
            if (this.itemClick != null)
                this.itemClick(true);

            OfficeItem list = sender as OfficeItem;
            string mLevelOneOficeName = list.lblOffice.Text;

//            FrmLevelTwoOfficeChooes mFrmLevelTwoOfficeChooes = new FrmLevelTwoOfficeChooes();//转向到二级科室选择界面
//
//            mFrmLevelTwoOfficeChooes.MLevelOneOficeName = mLevelOneOficeName;
//
//            mFrmLevelTwoOfficeChooes.ShowDialog();

            FrmOfficeChoose frmOfficeChoose = new FrmOfficeChoose();
            frmOfficeChoose.MLevelOneOficeName = mLevelOneOficeName;
            frmOfficeChoose.ShowDialog();

            if (this.itemClick != null)
                this.itemClick(false);

        }

        
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plNextPage_Click(object sender, EventArgs e)
        {
            if (newPage == Amountpage)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是最后一页了！");
                frm.ShowDialog();
                return;
            }
            newPage++;
            officeNameBind(0);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plBeforePage_Click(object sender, EventArgs e)
        {
            if (newPage == 1)
            {
                MyAlert frm = new MyAlert(AlertTypeenum.信息, "已经是第一页了！");
                frm.ShowDialog();
                return;
            }
            newPage--;
            officeNameBind(0);
        }
    }
}
