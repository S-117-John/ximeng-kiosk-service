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
using System.Runtime.InteropServices;
using AutoServiceSDK.SdkService;

namespace AutoServiceManage.InCard
{
    /// <summary>
    /// 锡盟医院壁挂式自助机
    /// </summary>
    public partial class FrmReadIdenetityBG : Form
    {
        #region  旭辉dll动态库调用31784

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="nPort"></param>
        /// <param name="nBaud"></param>
        /// <returns></returns>
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_InitComm(System.Int32 nPort, System.Int32 nBaud);


        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ExitComm(System.Int32 nDevHandle);

        //开始读
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Start_Read(System.Int32 nDevHandle);
        //结束读取
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Stop_Read(System.Int32 nDevHandle);

        //获取姓名
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_Name(System.Int32 nDevHandle, StringBuilder rBuff);
        //获取身份证
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_IDNumber(System.Int32 nDevHandle, StringBuilder rBuff);

        //性别
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_SexID(System.Int32 nDevHandle, StringBuilder rBuff);

        //民族
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_Nation(System.Int32 nDevHandle, StringBuilder rBuff);

        //出生日期
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_Birth(System.Int32 nDevHandle, StringBuilder rBuff);


        // 获取头像

        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_PhotoFile(System.Int32 nDevHandle, IntPtr ucBmpBuffer);


        //地址
        [DllImport(@"RdCard100X1.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern System.Int32 SS_ID_Query_Address(System.Int32 nDevHandle, StringBuilder rBuff);

        #endregion

        int deviceHandle = -1;

        public FrmReadIdenetityBG()
        {
            InitializeComponent();
            timer1.Start();
        }
        int sec = 20;
        private AbstractIDCard rc;

        //private IDCardInfo idinfo = null;

        public string strCardNO = string.Empty;

        private void FrmReadIdenetityBG_Load(object sender, EventArgs e)
        {
            Open();

            timer1.Start();
            timer2.Start();
            backgroundWorker1.RunWorkerAsync();

            #region  神盾驱动读卡方法
            //try
            //{
            //    //读取二代身份证信息  
            //    rc = IDCardConfig.GetIDCardReader("0");

            //    rc.OnReadedInfo += new EventHandler<ReadEventArgs>(readIDCrad_OnReadedInfo);
            //    rc.OnReadError += new EventHandler<ReadErrorEventArgs>(readIDCrad_OnReadError);
            //    if (!rc.IsRun)
            //    {
            //        rc.Start(ReadType.读基本信息);
            //    }
            //    timer1.Start();
            //    timer2.Start();
            //}
            //catch (Exception ex)
            //{
            //    MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, ex.ToString());
            //    registerInfo.ShowDialog();
            //    this.Close();
            //}

            #endregion
        }
        //private void readIDCrad_OnReadedInfo(object sender, ReadEventArgs e)
        //{
        //    idinfo = new IDCardInfo();

        //    strCardNO = e.NewHuman.IDCardNo;
        //}
        //private void readIDCrad_OnReadError(object sender, ReadErrorEventArgs e)
        //{
        //    Skynet.LoggingService.LogService.GlobalInfoMessage("readIDCrad_OnReadError:" + e.Error);
        //    MessageBox.Show(e.Error, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadCard2();

        }

        /// <summary>
        /// 返回关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.timer2.Stop();
            CloseDev();
            this.DialogResult = DialogResult.Cancel;
        }


        /// <summary>
        /// 返回按钮时间指针
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (sec == 0)//倒计时结束关闭窗体
            {
                ReadCardClose();
            }
            sec = sec - 1;
            this.btnClose.Text = "返回(" + sec.ToString() + ")";

            if (!string.IsNullOrEmpty(strCardNO))
            {
                timer1.Stop();
                timer2.Stop();
                CloseDev();
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void ReadCardClose()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            CloseDev();
            this.DialogResult = DialogResult.Cancel;
        }



        private Common_XH theCamera_XH = new Common_XH();

        /// <summary>
        /// 语音提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            theCamera_XH.DoorLightOpen(LightTypeenum.身份证, LightOpenTypeenum.闪烁);
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请将身份证放置在感应区!");
            voice.EndJtts();
        }


               /// <summary>
       /// 打开端口
       /// </summary>
       /// <param name="Port"></param>
       /// <param name="Repeat"></param>
       /// <returns></returns>
        public  void Open()
        {

            deviceHandle = SS_InitComm(1000, 115200);
            if (deviceHandle < 0)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("端口打开失败");
            }
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public  void CloseDev()
        {
            if (deviceHandle > 0)
            {
                SS_ExitComm(deviceHandle);
            }
        }

            /// <summary>
            /// 读取身份证
            /// </summary>
            /// <returns></returns>
        public  void ReadCard2()
        {
            int iReturn = SS_ID_Start_Read(deviceHandle);
            if (iReturn < 0)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("读取二代证信息失败！" + "错误代码：" + iReturn.ToString() + "\n");
            }
            else
            {
                ////身份证号
                StringBuilder ID = new StringBuilder(256);
                SS_ID_Query_IDNumber(iReturn, ID);
                strCardNO = ID.ToString();
                Skynet.LoggingService.LogService.GlobalInfoMessage("读取二代证信息成功！" + "身份证号：" + strCardNO.ToString());
            }
            SS_ID_Stop_Read(iReturn);

            }

    }
}
