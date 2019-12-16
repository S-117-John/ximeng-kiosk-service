using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skynet.LoggingService;
using SystemFramework.Voice;

namespace AutoServiceSDK.POSInterface.POS003.Dialogs
{
    public partial class FrmPasswordBox : Form
    {
        //密码长度
        private int length = 0;

        #region 构造函数
        public FrmPasswordBox()
        {
            InitializeComponent();
        }
        #endregion

        #region 绘制圆角
        private void SetWindowRegion(Control sender, int length, float tension)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddClosedCurve(new Point[]{
            new Point(0, sender.Height / length),
            new Point(sender.Width / length, 0),
            new Point(sender.Width - sender.Width / length, 0),
            new Point(sender.Width, sender.Height / length),
            new Point(sender.Width, sender.Height - sender.Height / length),
            new Point(sender.Width - sender.Width / length, sender.Height),
            new Point(sender.Width / length, sender.Height),
            new Point(0, sender.Height - sender.Height / length)}, tension);

            sender.Region = new Region(path);
        }

        private void FrmEnterPassword_Paint(object sender, PaintEventArgs e)
        {
            SetWindowRegion(this, 28, 0.1f);
        }
        #endregion

        #region 接受密码输入
        /// <summary>
        /// 接受密码输入
        /// </summary>
        public void AcceptPwdInput(IntPtr pConf, char chKey)
        {
            LogService.GlobalInfoMessage("当前密码长度：" + length);

            //超时、取消和回车同时表示密码输入结束
            switch (chKey)
            {
                #region 数字键
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ++length;
                    this.SetPwdBox(length);
                    break;
                #endregion

                #region 取消键
                case 'C': //取消
                    this.CancelInput();
                    break;
                #endregion

                #region 更正键
                case 'B': //更正、删除
                    --length;
                    this.SetPwdBox(length);
                    break;
                #endregion

                #region 确认键
                case 'E': //确认、确定
                    this.Close();
                    break;
                #endregion

                #region 加密数字键
                case '*': //加密数字键
                    ++length;
                    this.SetPwdBox(length);
                    break;
                #endregion

                #region 开始密码
                case 'S': //开始密码（显示密码框，提示输入密码）
                    this.ShowPwdBox();
                    break;
                #endregion

                #region 密码超时
                case 'O': //输入密码超时
                    this.InputTimeout();
                    break;
                #endregion

                default:
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 设置密码框
        /// </summary>
        /// <param name="pinLength">密码长度</param>
        private void SetPwdBox(int pinLength)
        {
            switch (pinLength) //密码位数
            {
                case 0:
                    mPwd1.Visible = false;
                    mPwd2.Visible = false;
                    mPwd3.Visible = false;
                    mPwd4.Visible = false;
                    mPwd5.Visible = false;
                    mPwd6.Visible = false;
                    break;
                case 1:
                    mPwd1.Visible = true;
                    break;
                case 2:
                    mPwd2.Visible = true;
                    break;
                case 3:
                    mPwd3.Visible = true;
                    break;
                case 4:
                    mPwd4.Visible = true;
                    break;
                case 5:
                    mPwd5.Visible = true;
                    break;
                case 6:
                    mPwd6.Visible = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 显示密码框，并提示输入密码
        /// </summary>
        private void ShowPwdBox()
        {
            this.length = 0;
            this.ShowDialog();
            Voice voice = new Voice();
            voice.PlayText("请输入您的密码。");
            voice.EndJtts();
        }

        /// <summary>
        /// 取消输入密码
        /// </summary>
        private void CancelInput()
        {
            length = 0;
            this.Close();
            throw new Exception("用户取消输入。");
        }

        /// <summary>
        /// 密码输入超时
        /// </summary>
        private void InputTimeout()
        {
            length = 0;
            this.Close();
            throw new Exception("密码输入超时。");
        }
        #endregion
    }
}
