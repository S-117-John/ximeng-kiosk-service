using CardInterface.IDCard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SystemFramework.IDCard;
using AutoServiceSDK.SdkData;
using AutoServiceManage.Common;

namespace AutoServiceManage.InCard
{
    /// <summary>
    /// 锡盟医院柜式自助机
    /// </summary>
    public partial class FrmReadIdenetityGS : Form
    {
        public FrmReadIdenetityGS()
        {
            InitializeComponent();
            timer1.Start();
        }
        int sec = 20;
        private AbstractIDCard rc;

        private IDCardInfo idinfo = null;

        public string strCardNO = string.Empty;

        private void FrmReadIdenetityGS_Load(object sender, EventArgs e)
        {
            //读取二代身份证信息  
            rc = IDCardConfig.GetIDCardReader("0");

            rc.OnReadedInfo += new EventHandler<ReadEventArgs>(readIDCrad_OnReadedInfo);
            rc.OnReadError += new EventHandler<ReadErrorEventArgs>(readIDCrad_OnReadError);
            if (!rc.IsRun)
            {
                rc.Start(ReadType.读基本信息);
            }
            timer1.Start();
            timer2.Start();
            backgroundWorker1.RunWorkerAsync();
        }
        private void readIDCrad_OnReadedInfo(object sender, ReadEventArgs e)
        {
            idinfo = new IDCardInfo();

            strCardNO = e.NewHuman.IDCardNo;
        }
        private void readIDCrad_OnReadError(object sender, ReadErrorEventArgs e)
        {
            Skynet.LoggingService.LogService.GlobalInfoMessage("readIDCrad_OnReadError:" + e.Error);
            MessageBox.Show(e.Error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strCardNO))
            {
                timer1.Stop();
                timer2.Stop();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.timer2.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (sec == 0)
            {
                ReadCardClose();
            }
            sec = sec - 1;
            this.btnClose.Text = "返回(" + sec.ToString() + ")";
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void ReadCardClose()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请将身份证放置在感应区!");
            voice.EndJtts();
        }

    }
}
