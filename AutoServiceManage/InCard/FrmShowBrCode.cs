using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceManage.Ipresenter;
using AutoServiceManage.Presenter;
using ThoughtWorks.QRCode.Codec;
using System.IO;
using Skynet.Framework.Common;
using BusinessFacade.His.CardClubManager;

namespace AutoServiceManage.InCard
{
    public partial class FrmShowBrCode : Form
    {

        private string pkOrg = SkyComm.getvalue("医疗机构代码");
        private string opId = SysOperatorInfo.OperatorID;
        public string strMsg = string.Empty;
        private const string NotFound = "NotFound";//卡平台返回结果未找到患者卡号
        private const string _isReadCardFail = "1";//读卡失败
        private string StrtimeStamp = string.Empty;
        private const string CardType = "DZJKK";//卡平台返回结果未找到患者卡号
        public FrmShowBrCode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmNetPay_Load(object sender, EventArgs e)
        {

            timer3.Interval = 1000;

            timer3.Start();

            StrtimeStamp = DateTime.Now.ToLocalTime().ToString("hh:mm:ss"); //时间戳

            string strBarCode = CardType + "|" + pkOrg + "|" + opId + "_" + StrtimeStamp; //合并二维码

            this.createBarCode(strBarCode);//生成二维码


        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBarCode(String strBrCode)
        {
            try
            {

                this.label4.Enabled = true;


                //取消等待窗体
                this.loading.Visible = false;

                //生成二维码
                Image imageBr = Image.FromFile(Application.StartupPath + "\\" + SkyComm.getvalue("BrCodeImage"));
                if (imageBr == null)
                {
                    SkyComm.ShowMessageInfo(string.Format("生成二维码失败，系统根目录下缺少{0}图片,请手动添加!", SkyComm.getvalue("BrCodeImage")));
                    return;
                }
                qrCodeImgControl1.Image = CombinImage(ECode(strBrCode), imageBr);

                Log.Info(GetType().ToString(), string.Format("生成二维码成功,参数:医疗机构号：{0},自助机编号：{1},时间戳：{2}", pkOrg, opId, StrtimeStamp));
                ////显示二维码
                qrCodeImgControl1.Visible = true;

                timer2.Interval = 30000;
                timer2.Start();

                ///生成后每隔五秒查询返回结果
                timer1.Start();
            }
            catch (Exception e)
            {
                Log.Info(GetType().ToString(), "生成二维码", e.Message);
                //生成二维码失败
                SkyComm.ShowMessageInfo("生成二维码失败，请返回重新操作或更换自助机！");

                this.loading.Visible = false;

                this.DialogResult = DialogResult.Cancel;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //每5秒查询交易结果
            try
            {
                //StrtimeStamp
                string strCardId = new CardAuthorizationFacade().getCardInfoByPkOrgAndMacNo(pkOrg, opId, StrtimeStamp);
                if (!string.IsNullOrEmpty(strCardId))
                {
                    if (NotFound.Equals(strCardId))
                    {
                        strMsg = _isReadCardFail;
                        SkyComm.ShowMessageInfo("该卡信息未签约！");
                        this.timer1.Stop();
                        this.timer2.Stop();
                        this.DialogResult = DialogResult.OK;
                    }
                    SkyComm.ShowMessageInfo("扫码成功！");
                    SkyComm.cardInfoStruct.CardNo = strCardId;
                    SkyComm.cardInfoStruct.isVirtualcard = true;//是否电子卡

                    CardRead cardUtility = new CardRead(this);
                    strMsg = cardUtility.GetPatiantInfo();

                    this.timer1.Stop();
                    this.timer2.Stop();
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                this.timer1.Stop();
                this.timer2.Stop();
                SkyComm.ShowMessageInfo(exception.Message);
                this.DialogResult = DialogResult.No;
            }
        }



        private void timer2_Tick(object sender, EventArgs e)
        {

            SkyComm.ShowMessageInfo("扫码超时！");
            this.timer2.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 点击返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label4_Click(object sender, EventArgs e)
        {
            FrmYesNoAlert mFrmInfoAlert = new FrmYesNoAlert();

            mFrmInfoAlert.Msg = "如果您已经扫码，请在此界面等待结果！是否离开？";
            mFrmInfoAlert.sec = 10;
            mFrmInfoAlert.timer1.Start();

            if (mFrmInfoAlert.ShowDialog() == DialogResult.OK)
            {
                timer1.Stop();
                timer2.Stop();
                this.DialogResult = DialogResult.Cancel;
            }

        }


        private void FrmNetPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer3.Stop();
        }


        private int time = 300;
        private void timer3_Tick(object sender, EventArgs e)
        {
            time = time - 1;
            this.label6.Text = "操作时间：" + time;

        }



        #region 带图片的二维码生成

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static Image ECode(String datas)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式  

            qrCodeEncoder.QRCodeScale = 3;//像素（即二维码大小）  
            qrCodeEncoder.QRCodeVersion = 7;//版本（即信息含量多少）  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;

            try
            {
                qrCodeEncoder.Encode(datas, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("二维码信息输入过大，请精简后重新输入！");
                return null;
            }
            var qrImg = qrCodeEncoder.Encode(datas, System.Text.Encoding.UTF8);

            Bitmap bmp = new Bitmap(qrImg.Width + 2, qrImg.Height + 2);//生成的二维码图片实际大小  
            Graphics g = Graphics.FromImage(bmp);
            var c = System.Drawing.Color.DarkGray;//背景颜色  
            g.FillRectangle(new SolidBrush(c), 0, 0, qrImg.Width + 2, qrImg.Height + 2);//背景矩形位置和大小  

            g.DrawImage(qrImg, 1, 1);//生成的二维码在矩形中的位置  
            g.Dispose();
            return bmp;
        }

        /// <summary>
        /// 合并图片和二维码
        /// </summary>
        /// <param name="imgBack"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Image CombinImage(Image imgBack, Image img)
        {
            if (img.Height != 30 || img.Width != 30)
            {
                img = KiResizeImage(img, 30, 30, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);
            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);//g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);     
            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 2, imgBack.Width / 2 - img.Width / 2 - 2,54,54);  
            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);    
            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2 + 1, imgBack.Width / 2 - img.Width / 2 + 1, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="newW"></param>
        /// <param name="newH"></param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public static Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量    
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }

}
