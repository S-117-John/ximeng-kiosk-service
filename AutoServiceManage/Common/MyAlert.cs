using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoServiceManage.Common
{
    public partial class MyAlert : Form
    {
        /// <summary>
        /// 提示类型
        /// </summary>
        private AlertTypeenum AlertType = AlertTypeenum.信息;

        //提示内容
        private string AlertMsg = string.Empty;

        //提示标题
        private string AlertTitle = string.Empty;

        /// <summary>
        /// 显示时间
        /// </summary>
        private int ShowSec { get; set; }

     
        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="strAlertType">提示框类型</param>
        /// <param name="strAlertMsg">显示内容</param>
        public MyAlert(AlertTypeenum strAlertType, string strAlertMsg)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            AlertType = strAlertType;
            AlertMsg = strAlertMsg;            
        }

        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="strAlertType">提示框类型</param>
        /// <param name="strAlertMsg">显示内容</param>
        /// <param name="intShowSec">显示时间</param>
        public MyAlert(AlertTypeenum strAlertType, string strAlertMsg, int intShowSec)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            AlertType = strAlertType;
            AlertMsg = strAlertMsg;
            ShowSec = intShowSec;
        }

        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="strAlertType">提示框类型</param>
        /// <param name="strAlertMsg">显示内容</param>
        /// <param name="strAlertTitle">显示标题</param>
        public MyAlert(AlertTypeenum strAlertType, string strAlertMsg, string strAlertTitle)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            AlertType = strAlertType;
            AlertMsg = strAlertMsg;
            AlertTitle = strAlertTitle;
        }
        
        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="strAlertType">提示框类型</param>
        /// <param name="strAlertMsg">显示内容</param>
        /// <param name="strAlertTitle">显示标题</param>
        /// <param name="intShowSec">显示时间</param>
        public MyAlert(AlertTypeenum strAlertType, string strAlertMsg, string strAlertTitle,int intShowSec)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            AlertType = strAlertType;
            AlertMsg = strAlertMsg;
            AlertTitle = strAlertTitle;
            ShowSec = intShowSec;
        }

        private void MyAlert_Load(object sender, EventArgs e)
        {            
            backgroundWorker1.RunWorkerAsync();
        }

        private void MyAlert_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(100);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (AlertType)
            {
                case AlertTypeenum.信息:
                    FrmInfoAlert frm = new FrmInfoAlert();
                    frm.TopMost = true;
                    frm.Msg = AlertMsg;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        frm.timer1.Stop();
                        frm.Dispose();
                        CloseWin(this, true);
                    }  
                    
                    break;
                case AlertTypeenum.确认取消:
                    FrmYesNoAlert frmYN = new FrmYesNoAlert();
                    frmYN.TopMost = true;
                    frmYN.Msg = AlertMsg;
                    if (!string.IsNullOrEmpty(AlertTitle))
                    {
                        frmYN.Title = AlertTitle;
                    }
                    frmYN.ShowDialog();
                    if (frmYN.DialogResult == DialogResult.OK)
                    {
                        frmYN.timer1.Stop();
                        frmYN.Dispose();
                        CloseWin(this, true);
                    }
                    else
                    {
                        frmYN.timer1.Stop();
                        frmYN.Dispose();                        
                        CloseWin(this, false);
                    }
                    break;
            }
        }

        private delegate void DeCloseWin(Form lv, bool isok);
        //提示信息
        void CloseWin(Form lv, bool isok)
        {
            if (!lv.InvokeRequired)
            {
                if (isok)
                {
                    lv.DialogResult = DialogResult.OK;
                }
                else
                {
                    lv.DialogResult = DialogResult.Cancel;
                }

            }
            else
            {
                // 多线程调用时，通过主线程去访问
                DeCloseWin de = CloseWin;
                this.Invoke(de, lv);
            }
        }
    }

    public enum AlertTypeenum
    {
        信息,
        确认取消
    }
}
