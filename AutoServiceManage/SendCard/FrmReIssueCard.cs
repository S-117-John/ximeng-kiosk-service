using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.InCard;
using AutoServiceSDK.ISdkService;
using AutoServiceSDK.SdkData;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using CardInterface.IDCard;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using SystemFramework.IDCard;
using AutoServiceManage.Common;

namespace AutoServiceManage.SendCard
{
    public partial class FrmReIssueCard : Form
    {       
        #region 构造函数,load,窗体事件

        private IDCardInfo idinfo = null;
        public IDCardInfo IdInfo
        {
            get { return idinfo; }
        }

        private AbstractIDCard rc;

        private CardAuthorizationData eLCardAuthorizationData = new CardAuthorizationData();

        public FrmReIssueCard()
        {
            InitializeComponent();            
        }

        private void FrmReIssueCard_Load(object sender, EventArgs e)
        {
             this.label5.Text = "等待读取...";

            string projectType = SkyComm.getvalue("项目版本标识");
            if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
            {
                this.label12.Text = "2．	请仔细核对身份信息，本卡系就诊专用卡。";
                this.label2.Visible = false;
                this.label3.Top=this.label12.Location.Y+30;
                this.label8.Top = this.label12.Location.Y + 60;
            }

            if (AutoHostConfig.IDCardType.Equals("XUHUI_PH"))
            {
                this.backgroundWorker2.RunWorkerAsync();
            }
            else
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

            
            backgroundWorker1.RunWorkerAsync();
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
            if (Debugger.IsAttached)
                timer1.Start();
        }

        private void FrmReIssueCard_FormClosing(object sender, FormClosingEventArgs e)
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
            //if (AutoHostConfig.IDCardType.Equals("XUHUI_PH"))
            //{
            //    this.backgroundWorker2.CancelAsync();
            //}
            //this.backgroundWorker1.CancelAsync();
            ucTime1.timer1.Stop();
        }

        private bool isShowFrom = false;
        private void readIDCrad_OnReadedInfo(object sender, ReadEventArgs e)
        {
            if (isShowFrom == false)
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
                idinfo.ImagePath = e.NewHuman.PhotoLocation;
                if (idinfo != null && !string.IsNullOrEmpty(idinfo.Name))
                {
                    rc.Over();
                    isShowFrom = true;
                    if (ShowForm() == true)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (!rc.IsRun)
                        {
                            rc.Start(ReadType.读基本信息);
                        }
                    }
                }
            }
        }

        private void readIDCrad_OnReadError(object sender, ReadErrorEventArgs e)
        {
            Skynet.LoggingService.LogService.GlobalInfoMessage("readIDCrad_OnReadError:" + e.Error);
            //MessageBox.Show(e.Error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region 返回,退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private bool ShowForm()
        {

            #region 验证身份证下是否存在卡信息
            string sqlSelect = " AND ((A.IDENTITYNAME='身份证' AND A.IDENTITYCARD='" + idinfo.Number.Trim() + "' AND A.PATIENTNAME='" + idinfo.Name.Trim() + "') OR (A.GUARDIANIDNUMBER='" + idinfo.Number.Trim() + "')) AND B.CIRCUIT_STATE IN (0,1) ";
            PatientInfoFacade pfacade = new PatientInfoFacade();
            DataSet pdata = pfacade.FindCardPatientinfoByCondition(sqlSelect);
            if (pdata == null || pdata.Tables.Count == 0 || pdata.Tables[0].Rows.Count == 0)
            {
                SkyComm.ShowMessageInfo("无该身份证对应的就诊卡信息，请使用自助办卡功能办理就诊卡！");
                idinfo = null;
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
                return false;
            }

            #endregion

            #region 输入手机号码
            FrmReIssueCardInfo frmTel = new FrmReIssueCardInfo();
            frmTel.IdInfo = idinfo;
            if (frmTel.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
            }
            else
            {
                idinfo = null;
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
                return false;
            }
            ucTime1.Sec = 60;

            return true;
            #endregion
        }

        #endregion

        private void lblOk_Click(object sender, EventArgs e)
        {
            ucTime1.timer1.Stop();

            #region 获取身份证信息
            IDCardInfo idinfo = null;
            FrmIdentityCard frm = new FrmIdentityCard();
            try
            {
                if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    //获取身份份信息
                    idinfo = frm.IdInfo;
                }
                else
                {
                    //ucTime1.Sec = 60;
                    //ucTime1.timer1.Start();

                    //idinfo = new IDCardInfo();
                    //idinfo.Name = "西安天网";
                    //idinfo.Sex = "男";
                    //idinfo.People = "汉族";
                    //idinfo.Number = "610100199401010229";
                    //idinfo.Birthday = "1994-01-01";
                    //idinfo.Address = "西安市雁塔区丈八六路51号";

                    //return;
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("读取居民身份证信息失败：" + ex.Message);
                return;
            }
            finally
            {
                frm.Dispose();
            }
            #endregion

            #region 验证身份证下是否存在卡信息
            string sqlSelect = " AND ((A.IDENTITYNAME='身份证' AND A.IDENTITYCARD='" + idinfo.Number.Trim() + "' AND A.PATIENTNAME='" + idinfo.Name.Trim() + "') OR (A.GUARDIANIDNUMBER='" + idinfo.Number.Trim() + "')) AND B.CIRCUIT_STATE IN (0,1) ";
            PatientInfoFacade pfacade = new PatientInfoFacade();
            DataSet pdata = pfacade.FindCardPatientinfoByCondition(sqlSelect);
            if (pdata == null || pdata.Tables.Count == 0 || pdata.Tables[0].Rows.Count == 0)
            {
                SkyComm.ShowMessageInfo("无该身份证对应的就诊卡信息，请使用自助办卡功能办理就诊卡！");
                idinfo = null;
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
                return;
            }

            #endregion

            #region 输入手机号码
            string TelePhone = string.Empty;
            FrmSendCardInputTel frmTel = new FrmSendCardInputTel(idinfo);
            frmTel.IdInfo = idinfo;
            if (frmTel.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                TelePhone = frmTel.TelePhone;
            }
            else
            {
                idinfo = null;
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
                return;
            }

            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            #endregion

        }              

        private void timer1_Tick(object sender, EventArgs e)
        {
            idinfo = new IDCardInfo();
            idinfo.Name = "西安天网";
            idinfo.Sex = "男";
            idinfo.People = "汉族";
            idinfo.Number = "610100199401010228";
            idinfo.Birthday = "1994-01-01";
            idinfo.Address = "西安市雁塔区丈八六路51号";

            timer1.Stop();
            ShowForm();
        }

        private void ucPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请将您的二代身份证放在身份证阅读区!");
            voice.EndJtts();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                do
                {
                    idinfo = CardCpu.readIdCard();

                    Thread.Sleep(1000);

                } while (idinfo == null);

                e.Result = idinfo;
            }
            catch { }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Info("省医院读取身份证", "读身份证线程结束", "读身份证线程结束");

            if (ShowForm() == true)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
