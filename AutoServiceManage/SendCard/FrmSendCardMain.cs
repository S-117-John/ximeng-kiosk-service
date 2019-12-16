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
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.SendCard
{
    public partial class FrmSendCardMain : Form
    {       
        #region 构造函数,load,窗体事件

        private IDCardInfo idinfo = null;
        public IDCardInfo IdInfo
        {
            get { return idinfo; }
        }

        public string SendCardType { get; set; }//发卡类型
        private AbstractIDCard rc;

        private CardAuthorizationData eLCardAuthorizationData = new CardAuthorizationData();

        public FrmSendCardMain()
        {
            InitializeComponent();            
        }

        private void FrmSendCardMain_Load(object sender, EventArgs e)
        {
            if (SendCardType == "成人")
            {
                this.label5.Text = "等待读取...";
            }
            string projectType= SkyComm.getvalue("项目版本标识");
            if (!string.IsNullOrEmpty(projectType) && projectType == "锡林郭勒盟医院")
            {
                this.label12.Text = "2．	请仔细核对身份信息，本卡系就诊专用卡。";
            }

            Log.Info("开始读取身份证", "开始读取身份证", "开始读取身份证");
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

        private void FrmSendCardMain_FormClosing(object sender, FormClosingEventArgs e)
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
            Log.Info("获取身份证信息", "获取身份证信息", "获取身份证信息");

            if (isShowFrom == false)
            {
                idinfo = new IDCardInfo();
                idinfo.Name = e.NewHuman.Name;

                idinfo.Address = e.NewHuman.Address;
                idinfo.Sex = e.NewHuman.Gender;
                idinfo.Birthday = e.NewHuman.BirthDay.ToString("yyyy-MM-dd");
                idinfo.Signdate = e.NewHuman.InceptDate;
                idinfo.Number = e.NewHuman.IDCardNo.ToUpper();//chenqiang 2018.03.13 add by Case:30785
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
            #region 验证身份证号码
            if (SendCardType == "成人")
            {
                Log.Info("查询身份证信息", "查询身份证信息", "查询身份证信息");
                //通过身份证查询是否已经个办理过就诊卡，如果已经办理过则不充许再次办理
                string ConditionStr = " AND A.IDENTITYNAME='身份证' AND A.IDENTITYCARD='" + idinfo.Number.Trim() + "' AND B.CIRCUIT_STATE IN (0,1) ";
                PatientInfoFacade pfacade = new PatientInfoFacade();
                DataSet pdata = pfacade.FindCardPatientinfoByCondition(ConditionStr);

                if (pdata.Tables[0].Rows.Count > 0)
                {
                    string strMessage = "患者[" + pdata.Tables[0].Rows[0]["PATIENTNAME"].ToString() + "]已于" + Convert.ToDateTime(pdata.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办卡，为保证就诊档案完整，不可重复办卡；请核实患者身份,挂失原卡后，补发卡！";
                    if (pdata.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")
                    {
                        strMessage = "患者[" + pdata.Tables[0].Rows[0]["PATIENTNAME"].ToString() + "]已于" + Convert.ToDateTime(pdata.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请补发卡！";
                    }
                    SkyComm.ShowMessageInfo(strMessage);
                    idinfo = null;
                    ucTime1.Sec = 60;
                    ucTime1.timer1.Start();
                    return false;
                }
            }
            #endregion

            #region 输入手机号码
            if (SendCardType == "成人")
            {
                Log.Info("输入手机号码", "输入手机号码", "输入手机号码");
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
                    return false;
                }
            }
            else
            {
                string TelePhone = string.Empty;
                FrmInputForChild frmChild = new FrmInputForChild(idinfo);
                frmChild.IdInfo = idinfo;
                if (frmChild.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    TelePhone = frmChild.TelePhone;
                }
                else
                {
                    idinfo = null;
                    ucTime1.Sec = 60;
                    ucTime1.timer1.Start();
                    return false;
                }
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

            #region 验证身份证号码
            if (SendCardType == "成人")
            {
                //通过身份证查询是否已经个办理过就诊卡，如果已经办理过则不充许再次办理
                string ConditionStr = string.Empty;

                if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() == "0")
                {
                    ConditionStr = " AND A.IDENTITYNAME='身份证' AND A.IDENTITYCARD='" + idinfo.Number.Trim().ToUpper() + "' AND B.CIRCUIT_STATE IN (0,1) ";
                }
                else
                {
                    ConditionStr = " AND A.IDENTITYNAME='身份证' AND A.IDENTITYCARD='" + idinfo.Number.Trim().ToUpper() + "' AND B.CIRCUIT_STATE IN (0,1) AND B.TYPEID="+ SkyComm.dsCardType.Tables[0].Rows[0]["TYPE_NO"];
                }
                PatientInfoFacade pfacade = new PatientInfoFacade();
                DataSet pdata = pfacade.FindCardPatientinfoByCondition(ConditionStr);

                if (pdata.Tables[0].Rows.Count > 0)
                {
                    string strMessage = "患者[" + pdata.Tables[0].Rows[0]["PATIENTNAME"].ToString() + "]已于" + Convert.ToDateTime(pdata.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办卡，为保证就诊档案完整，不可重复办卡；请核实患者身份,挂失原卡后，补发卡！";
                    if (pdata.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")
                    {
                        strMessage = "患者[" + pdata.Tables[0].Rows[0]["PATIENTNAME"].ToString() + "]已于" + Convert.ToDateTime(pdata.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请补发卡！";
                    }
                    SkyComm.ShowMessageInfo(strMessage);
                    idinfo = null;
                    ucTime1.Sec = 60;
                    ucTime1.timer1.Start();
                    return;
                }
            }
            #endregion

            #region 输入手机号码
            if (SendCardType == "成人")
            {
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
            }
            else
            {
                string TelePhone = string.Empty;
                FrmInputForChild frmChild = new FrmInputForChild(idinfo);
                frmChild.IdInfo = idinfo;
                if (frmChild.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    TelePhone = frmChild.TelePhone;
                }
                else
                {
                    idinfo = null;
                    ucTime1.Sec = 60;
                    ucTime1.timer1.Start();
                    return;
                }
            }
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();

            #endregion
            
        }              

        private void timer1_Tick(object sender, EventArgs e)
        {
            idinfo = new IDCardInfo();
            idinfo.Name = "西安天网A";
            idinfo.Sex = "男";
            idinfo.People = "汉族";
            idinfo.Number = "610100199401010339";
            idinfo.Birthday = "1994-01-01";
            idinfo.Address = "西安市雁塔区丈八六路51号";

            timer1.Stop();
            ShowForm();
        }

        private void ucPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private Common_XH theCamera_XH = new Common_XH();
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            theCamera_XH.DoorLightOpen(LightTypeenum.身份证, LightOpenTypeenum.闪烁);
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
            theCamera_XH.DoorLightClose(LightTypeenum.身份证);
            Log.Info("省医院读取身份证", "读身份证线程结束", "读身份证线程结束");

            if (ShowForm() == true)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
