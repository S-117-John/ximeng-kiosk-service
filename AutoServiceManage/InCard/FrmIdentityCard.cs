using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkData;
using CardInterface.IDCard;
using SystemFramework.IDCard;

namespace AutoServiceManage.InCard
{
    public partial class FrmIdentityCard : Form
    {
        private IDCardInfo idinfo = null;

        public IDCardInfo IdInfo
        {
            get { return idinfo; }
        }
        int sec = 30;

        private AbstractIDCard rc;
        public FrmIdentityCard()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (sec == 0)
                {
                    this.timer1.Stop();
                    this.DialogResult = DialogResult.OK;
                }
                this.btnClose.Text = "返回(" + sec.ToString() + ")";
                sec = sec - 1;
                if (idinfo != null)
                {
                    FrmMain.userInfo = idinfo;
                    if (FrmMain.userInfo != null)
                    {
                        timer1.Stop();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                SkyComm.ShowMessageInfo(ex.Message);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmIdentityCard_Load(object sender, EventArgs e)
        {
            //读取二代身份证信息  
            rc = IDCardConfig.GetIDCardReader("0");

            rc.OnReadedInfo += new EventHandler<ReadEventArgs>(readIDCrad_OnReadedInfo);
            rc.OnReadError += new EventHandler<ReadErrorEventArgs>(readIDCrad_OnReadError);
            if (!rc.IsRun)
            {
                rc.Start(ReadType.读基本信息);
            }
        }

        private void FrmIdentityCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rc != null)
            {
                rc.OnReadedInfo -= readIDCrad_OnReadedInfo;
                rc.OnReadError -= readIDCrad_OnReadError;
                if (!rc.IsRegister)
                {
                    rc.Over();
                }
            }
        }

        private void readIDCrad_OnReadedInfo(object sender, ReadEventArgs e)
        {
            idinfo = new IDCardInfo();
            idinfo.Name = e.NewHuman.Name;

            idinfo.Address = e.NewHuman.Address;
            idinfo.Sex = e.NewHuman.Gender;
            idinfo.Birthday = e.NewHuman.BirthDay.ToString("yyyy-MM-dd");
            idinfo.Signdate = e.NewHuman.InceptDate;
            idinfo.Number = e.NewHuman.IDCardNo;
            idinfo.Name = e.NewHuman.Name;
            idinfo.People = e.NewHuman.Nation;
            idinfo.ValidDate = e.NewHuman.ExpireDate;
        }

        private void readIDCrad_OnReadError(object sender, ReadErrorEventArgs e)
        {
            Skynet.LoggingService.LogService.GlobalInfoMessage("readIDCrad_OnReadError:" + e.Error);
            MessageBox.Show(e.Error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
