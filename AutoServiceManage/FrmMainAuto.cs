using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Charge;
using AutoServiceManage.Common;
using AutoServiceManage.InCard;
using AutoServiceManage.Inquire;
using AutoServiceManage.SendCard;
using BusinessFacade.His.ClinicDoctor;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using CardInterface;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using AutoServiceSDK.SdkData;
using EntityData.His.Common;
using System.Collections;
using AutoServiceSDK.POSInterface;
using AutoServiceManage.SystemManage;

namespace AutoServiceManage
{
    public partial class FrmMainAuto : Form
    {
        MainBase theMainBase = null;
        private DataSet dsRoleMenu = new DataSet();
        #region 构造函数，load
        public FrmMainAuto()
        {
             InitializeComponent();

             theMainBase = new MainBase();
             this.DoubleBuffered = true;//设置本窗体
             SetStyle(ControlStyles.UserPaint, true);
             SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
             SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
         }

        private void FrmMainAuto_Load(object sender, EventArgs e)
        {
            //SkyComm.CloseCash = false;
            theMainBase.Load();
            string Role = AutoHostConfig.MachineType;
            AutoServiceMachineInfoFacade AutoServiceFac = new AutoServiceMachineInfoFacade();
             dsRoleMenu = AutoServiceFac.GetAutoServiceRoleMenuByRoleID(Role,"一级");

            //if (dsRoleMenu.Tables[0].Rows.Count > 0)
            //    AddLable(dsRoleMenu);
            //else
            //{
            //    SkyComm.ShowMessageInfo("自助机功能授权不正确，请与管理员联系重新授权！");
            //}
    
            this.label1.Text = DateTime.Now.ToLongDateString().ToString();
            string week = string.Empty;
            switch ((int)DateTime.Now.DayOfWeek)
            {
                case 0:
                    week = "星期日";
                    break;
                case 1:
                    week = "星期一";
                    break;
                case 2:
                    week = "星期二";
                    break;
                case 3:
                    week = "星期三";
                    break;
                case 4:
                    week = "星期四";
                    break;
                case 5:
                    week = "星期五";
                    break;
                default:
                    week = "星期六";
                    break;
            }

            this.label2.Text = week;
            this.timer1.Start();
        }

        private void AddLable(DataSet dsRoleMenu)
        {
            //控件宽度
            int intButtonWidth = 207;

            //控件高度
            int intButtonHight = 62;

            //控件总数
            int intCount = dsRoleMenu.Tables[0].Rows.Count;

            //下一行控件与上行控件的高度
            int RowHight = 120;

            int intRowCount = intCount / 2;
            if (intCount % 2 != 0)
            {
                intRowCount++;
            }
            RowHight = (panel1.Height - 50 - (intRowCount * intButtonHight)) / (intRowCount - 1);
            RowHight = RowHight + intButtonHight;

            for (int i = 1; i <= intCount; i++)
            {
                DataRow row = dsRoleMenu.Tables[0].Rows[i - 1];
                Label lbl1 = new Label();

                lbl1.BackColor = System.Drawing.Color.Transparent;
                lbl1.Cursor = System.Windows.Forms.Cursors.Hand;
                int intx = 24;
                int inty = 25;

                int intMod = i % 2;
                if (intMod == 0)
                {
                    intx = intx + 326;
                }
                int intRow = i / 2;
                if (i > 2)
                {
                    if (intMod != 0)
                    {
                        inty += intRow * RowHight;
                    }
                    else
                    {
                        inty += (intRow - 1) * RowHight;
                    }
                }
                if (i == intCount && intCount % 2 != 0)
                {
                    intx = intx + 326; ;
                }

                if (row["MENUID"].ToString() == "02")
                {
                    if (string.IsNullOrEmpty(AutoHostConfig.PosInterfaceType) && string.IsNullOrEmpty(AutoHostConfig.CashBoxType))
                    {
                        lbl1.Enabled = false;
                    }
                }

                if (row["ISENABLE"].ToString() == "否")
                {
                    lbl1.Enabled = false;
                }
                lbl1.Location = new System.Drawing.Point(intx, inty);
                lbl1.Name = "lbl_" + row["MENUID"].ToString();
                lbl1.Tag = row["MENUID"].ToString();
                lbl1.Size = new System.Drawing.Size(intButtonWidth, intButtonHight);
                lbl1.TabIndex = 0;
                //lbl1.Text = "lbl_" + row["MENUID"].ToString();
                lbl1.Click += new System.EventHandler(this.lbl_Click);

                if (!string.IsNullOrEmpty(row["IMAGEPATH"].ToString()))
                    lbl1.Image = Image.FromFile(Application.StartupPath + "\\" + row["IMAGEPATH"].ToString());

                //chenqiang 2018.09.27 add by Case:31784
                //if (SkyComm.cardInfoStruct.isIdentityCard && row["MENUNAME"].ToString()=="退出")
                //    lbl1.Image = Image.FromFile(Application.StartupPath + "\\17.png");
                panel1.Controls.Add(lbl1);
            }
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            switch (lbl.Tag.ToString())
            { 
                case "01":
                    theMainBase.SendCard(this);  
                    break;

                case "02":
                    theMainBase.AddMoney(this);            
                    break;

                case "03":
                    theMainBase.BespeakRegister(this);     
                    break;

                case "04":
                    theMainBase.BespeakSignIn(this);
                    break;

                case "05":
                    theMainBase.Charge(this);         
                    break;

                case "06":
                    theMainBase.AutoPrint(this);            
                    break;

                case "07":
                    theMainBase.AutoQuery(this);
                    break;
                case "08"://医技预约
                    theMainBase.MedicalReserve(this);
                    break;
                case "09"://住院预交金充值
                    theMainBase.AddInHosMoney(this);
                    break;
                case "10"://门诊预存转住院预存
                    theMainBase.MoneyTransfer(this);
                    break;
                case "11":
                    theMainBase.InHosAdvanceRecord(this);
                    break;
                case "12":
                    theMainBase.InHosCostRecord(this);
                    break;
                case "13":
                    theMainBase.LeaveHosCostPrint(this);
                    break;
                case "14"://补卡
                    theMainBase.ReissueCard(this);
                    break;
                case "15":
                    theMainBase.PrintListReport(this);
                    break;
                case "96"://院内导航
                    theMainBase.hosGps(this);
                    break;
                case "97"://订餐
                    theMainBase.foodOrder(this);
                    break;
                case "98"://满意度调查
                    theMainBase.SurveyInfo(this);
                    break;
                case "99":
                    theMainBase.ExitCard();
                    break;
                case "101":
                    theMainBase.Electronics(this);
                    break;

            }

            if (SkyComm.cardInfoStruct.isIdentityCard )
            {
                SkyComm.ClearCookie();
            }
        }

        private void FrmMainAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            theMainBase = null;
        }

        #endregion

        #region 办卡
        private void lblBanKa_Click(object sender, EventArgs e)
        {
            theMainBase.SendCard(this);       
        }
        #endregion

        #region 预存
        private void lblYuCun_Click(object sender, EventArgs e)
        {
            theMainBase.AddMoney(this);            
        }
        #endregion

        #region 预约
        private void lblYuYue_Click(object sender, EventArgs e)
        {
            theMainBase.BespeakRegister(this);     
        }
        #endregion
                
        #region 签到

        private void lblQianDao_Click(object sender, EventArgs e)
        {
            theMainBase.BespeakSignIn(this);
        }
        
        #endregion

        #region 缴费

        private void lblJiaoFei_Click(object sender, EventArgs e)
        {
            theMainBase.Charge(this);            
        }
        
        #endregion

        #region 打印
        private void LblDaYin_Click(object sender, EventArgs e)
        {
            theMainBase.AutoPrint(this);            
        }
        #endregion

        #region 查询
        private void lblChaXun_Click(object sender, EventArgs e)
        {
            theMainBase.AutoQuery(this);
        }
        #endregion

        #region 退卡
        private void lblTuiKa_Click(object sender, EventArgs e)
        {
            theMainBase.ExitCard();
            //this.Close();
        }
        #endregion
                
        #region 左边功能列表
        private void lblyygk_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(1);
        }

        private void lbljzzn_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(2);
        }

        private void lblzjjs_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(3);
        }

        private void lblksjs_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(4);
        }

        private void lblypjg_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(5);
        }

        private void lblsfbz_Click(object sender, EventArgs e)
        {
            theMainBase.showweb(6);
        }

        #endregion

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            theMainBase.ExitCard();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            theMainBase.Electronics(this);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            theMainBase.AddMoney(this);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            theMainBase.AutoPrint(this);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            theMainBase.AutoQuery(this);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            theMainBase.SurveyInfo(this);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            theMainBase.Charge(this);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            theMainBase.MoneyTransfer(this);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            theMainBase.BespeakRegister(this);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            theMainBase.BespeakSignIn(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label3.Text = DateTime.Now.ToString("hh:mm:ss");

        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SkyComm.DiagnoseID))
                {
                    return;
                }

                string getChoose = string.Empty;
                //退出系统，验证密码
                FrmInputPassword frm = new FrmInputPassword();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    frm.Dispose();

                    return;
                }
                getChoose = frm.CheckType;
                frm.Dispose();

                if (getChoose == "1")
                {

                    //                SkyComm.CloseReader();//关闭读卡器
                    this.Close();
                }
                else if (getChoose == "2")
                {
                    FrmDoorlook frm2 = new FrmDoorlook();
                    frm2.ShowDialog();
                    frm2.Dispose();
                }
                else if (getChoose == "3")
                {
                    FrmBalance frm2 = new FrmBalance();
                    frm2.ShowDialog();
                    frm2.Dispose();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
