using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Properties;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;

namespace AutoServiceManage
{
    public partial class FrmBespeakQuery : Form
    {
        #region 变量
        private DataSet bespeakData;
        public DataSet BespeakData
        {
            get
            {
                return this.bespeakData;
            }
        }
        #endregion

        #region 构造函数,load,窗体事件
        public FrmBespeakQuery()
        {
            InitializeComponent();
            //this.BackgroundImage = Resources.自助预约_背景;

            //this.DoubleBuffered = true;//设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            ////Thread.Sleep(500);
        }
        
        private void FrmBespeakQuery_Load(object sender, EventArgs e)
        {
                      
        }

        private void FrmBespeakQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
           // ucTime1.timer1.Stop();
        }
              
        #endregion

        #region 输入预约号，手机号

        private void lblInputBespeakID_Click(object sender, EventArgs e)
        {
            this.lblInputBespeakID.Image = global::AutoServiceManage.Properties.Resources.预约号签到;
            this.lblInputTelePhone.Image = global::AutoServiceManage.Properties.Resources.手机号签到;
            lbltitle.Text = "预约号";
            if (lblhm.Text.IndexOf("号") > 0)
            {
                lblhm.Text = "请输入预约号";
                this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
            }
            ucTime1.Sec = 60;
        }

        private void lblInputTelePhone_Click(object sender, EventArgs e)
        {
            this.lblInputTelePhone.Image = global::AutoServiceManage.Properties.Resources.预约号签到;
            this.lblInputBespeakID.Image = global::AutoServiceManage.Properties.Resources.手机号签到;
            lbltitle.Text = "手机号";
            if (lblhm.Text.IndexOf("号") > 0)
            {
                lblhm.Text = "请输入手机号";
                this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
            }
            ucTime1.Sec = 60;
        }
        #endregion

        #region 清屏
        private void lblclear_Click(object sender, EventArgs e)
        {
            if (lbltitle.Text == "手机号")
            {
                lblhm.Text = "请输入手机号";
            }
            else
            {
                lblhm.Text = "请输入预约号";
            }
            lblErr.Text = string.Empty;
            lblErr.Visible = false;
            ucTime1.Sec = 60;
            this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
        }
        #endregion

        #region 退格
        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (lblhm.Text.IndexOf("号") < 0)
            {
                string hmValue = lblhm.Text.Substring(0, lblhm.Text.Length - 1);
                lblhm.Text = hmValue;
            }
            if (lblhm.Text.Length == 0)
            {
                if (lbltitle.Text == "手机号")
                {
                    lblhm.Text = "请输入手机号";
                }
                else
                {
                    lblhm.Text = "请输入预约号";
                }
                this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;                
            }
            ucTime1.Sec = 60;
        }
        #endregion

        #region 确认
        private void lblOK_Click(object sender, EventArgs e)
        {

            CommonFacade connonFac = new CommonFacade();
            DateTime ServerTime = connonFac.GetServerDateTime();
            double minutes = Convert.ToDouble(SystemInfo.SystemConfigs["预约挂号报到延时时间"].DefaultValue);

            if (lbltitle.Text == "预约号")
            {
                //根据预约号查询病人的预约信息
                BespeakRegisterFacade bespeakRegisterFacade = new BespeakRegisterFacade();
                this.bespeakData = bespeakRegisterFacade.FindCurrentBespeakByDiagnoseID(lblhm.Text.Trim(), 2, ServerTime.AddMinutes(-minutes));

                if (0 == this.bespeakData.Tables[0].Rows.Count)
                {
                    lblErr.Text = "确认失败：此预约号无效,请重新输入！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

                if (1 == Convert.ToInt32(this.bespeakData.Tables[0].Rows[0]["USEMARK"]))
                {
                    lblErr.Text = "确认失败：此预约号已使用,不能再进行取号！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

                if (-1 == Convert.ToInt32(this.bespeakData.Tables[0].Rows[0]["USEMARK"]))
                {
                    lblErr.Text = "确认失败：此预约号已退约,不能再进行取号！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

                if (2 == Convert.ToInt32(this.bespeakData.Tables[0].Rows[0]["USEMARK"]))
                {
                    lblErr.Text = "确认失败：此预约号的预约时间为：" + this.bespeakData.Tables[0].Rows[0]["BESPEAKDATE"].ToString() + "，预约时间已过期，不能再使用！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

                if (ServerTime.ToLongDateString() != Convert.ToDateTime(this.bespeakData.Tables[0].Rows[0]["BESPEAKDATE"]).ToLongDateString())
                {
                    lblErr.Text = "确认失败：此预约号预约时间为：" + Convert.ToDateTime(this.bespeakData.Tables[0].Rows[0]["BESPEAKDATE"]).ToShortDateString() + "，今天不能使用！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

                if (Convert.ToDateTime(this.bespeakData.Tables[0].Rows[0]["BESPEAKDATE"]).AddMinutes(minutes) < ServerTime)
                {
                    lblErr.Text = "确认失败：此预约号的预约时间为：" + this.bespeakData.Tables[0].Rows[0]["BESPEAKDATE"].ToString() + "，预约时间已过期，不能再使用！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }

            }
            else
            {
                //根据手机号查询病人的预约信息
                BespeakRegisterFacade bespeakRegisterFacade = new BespeakRegisterFacade();
                this.bespeakData = bespeakRegisterFacade.FindCurrentBespeakByDiagnoseID(lblhm.Text.Trim(), 3, ServerTime.AddMinutes(-minutes));
                if (bespeakData.Tables[0].Rows.Count == 0)
                {
                    lblErr.Text = "确认失败：此手机号没有找到可取号的预约数据！";
                    lblErr.Visible = true;
                    bespeakData = null;
                    ucTime1.Sec = 60;
                    return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion

        #region 输入数字0-9
        private void lbl0_Click(object sender, EventArgs e)
        {
            lblErr.Visible = false;
            if (lbltitle.Text == "手机号" && lblhm.Text.Length > 11)
            {
                lblErr.Text = "手机号长度不能大于11位!";
                lblErr.Visible = true;
                return;
            }

            if (lbltitle.Text == "预约号" && lblhm.Text.Length > 18)
            {
                lblErr.Text = "预约号长度不能大于18位！";
                lblErr.Visible = true;
                return;
            }           

            Label lbl = (Label) sender;
            string num = lbl.Name.Substring(lbl.Name.Length - 1, 1);
            this.lblhm.ForeColor = System.Drawing.Color.Black;
            InputText(num);
            ucTime1.Sec = 60;
        }

        private void InputText(string Num)
        {
            if (lblhm.Text.IndexOf("号") > 0)
            {
                lblhm.Text = Num;
            }
            else
            {
                lblhm.Text = lblhm.Text + Num;
            }
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

        #endregion
                
    }
}
