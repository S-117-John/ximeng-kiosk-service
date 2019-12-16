using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceSDK.SDK;
using AutoServiceSDK.SdkService;
using CardInterface;
using Skynet.LoggingService;

namespace AutoServiceManage.InCard
{
    public partial class FrmReadCardXH : Form
    {
        public int sec { get; set; }
        Common_XH theCamera_XH = null;

        /// <summary>
        /// 读卡机类型：1:磁条卡，2：M1卡
        /// </summary>
        public string CardType { get; set; }

        public FrmReadCardXH()
        {
            InitializeComponent();
            theCamera_XH = new Common_XH();
            sec = 30;
        }

        private void FrmReadCardXH_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(200);
            StringBuilder sbinput =
                new StringBuilder("<invoke name=\"READCARDALLOWCARDIN\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            Skynet.LoggingService.LogService.GlobalInfoMessage("调用XmlTcp允许进卡方法返回：" + strResult + ",输出参数：" +
                                                               sbinput.ToString());

            LogService.GlobalInfoMessage("FrmReadCardXH_Load_1");
            theCamera_XH.DoorLightOpen(LightTypeenum.读卡器, LightOpenTypeenum.闪烁);
            LogService.GlobalInfoMessage("FrmReadCardXH_Load_2");
            timer1.Start();
            timer2.Start();
            backgroundWorker2.RunWorkerAsync();
        }

        /// <summary>
        /// 检查是否有卡，如果是有卡则进行读卡信息
        /// </summary>
        /// <returns>如果无卡返回0，有卡并且卡信息正常返回1,有卡时如果验证失败返回错误信息</returns>
        public string CheckCard()
        {
            Thread.Sleep(200);
            StringBuilder sbinput =
                new StringBuilder("<invoke name=\"READCARDTESTINSERTCARD\"><arguments></arguments></invoke>");
            string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            LogService.GlobalInfoMessage("调用检测有卡方法输出返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (string.IsNullOrEmpty(sbinput.ToString()))
                return "0";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDSTACKSTATE']");
            if (xnd != null)
            {
                string CardStackState = xnd.InnerText;
                LogService.GlobalInfoMessage("读卡的类型：" + CardType + "****************" + CardStackState);

                if (CardStackState == "1")
                {
                    //如果有卡则读取磁条卡号
                    Thread.Sleep(200);

                    if (CardType == "1")
                    {
                        sbinput =
                            new StringBuilder("<invoke name=\"READCARDREADMAGCARDNO\"><arguments></arguments></invoke>");
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用读取磁条卡号卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                        doc.LoadXml(sbinput.ToString());
                        xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
                        if (xnd != null)
                        {
                            string strCardNO = xnd.InnerText;
                            SkyComm.cardInfoStruct.CardNo = strCardNO;
                            CardRead cardUtility = new CardRead(this);
                            string strMsg = cardUtility.GetPatiantInfo();
                            if (!string.IsNullOrEmpty(strMsg))
                            {
                                return strMsg;
                            }
                            else
                            {
                                return "1";
                            }
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else if (CardType == "2")
                    {
                        #region 读M1卡方法

                        //卡片移到射频位
                        sbinput =
                            new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                        //读第8块的数据
                        string strInput = "<invoke name=\"READCARDREADRFCARD\"><arguments>" +
                                          "<string id=\"SECTORNO\">8</string>" +
                                          "<string id=\"BLOCKNO\">2</string>" +
                                          "<string id=\"PASSWORD\">FFFFFFFFFFFF</string>" +
                                          "</arguments></invoke>";
                        LogService.GlobalInfoMessage("调用读射频卡数据方法输入参数1：" + strInput);
                        sbinput = new StringBuilder(strInput);
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用读射频卡数据方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                        doc.LoadXml(sbinput.ToString());
                        xnd = doc.SelectSingleNode("/return/arguments/string[@id='ERROR']");
                        string strCardNO = string.Empty;
                        if (xnd != null && xnd.InnerText == "SUCCESS")
                        {
                            strCardNO =
                                SkyComm.HexStringToString(
                                    doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']").InnerText,
                                    ASCIIEncoding.ASCII);


                            LogService.GlobalInfoMessage("=============读8扇区的内容===========================" + strCardNO +
                                                         "======================================");

                            if (strCardNO.Length >= 16)
                            {
                                strCardNO = strCardNO.Substring(0, 16);
                            }


                        }

                        if (strCardNO.Length >= 16) //第八块内容小于16
                        {

                        }

                        LogService.GlobalInfoMessage("读取到的卡号信息：" + strCardNO + "===============================");

                        if (!string.IsNullOrEmpty(strCardNO) && strCardNO.Length >= 15)
                        {
                            SkyComm.cardInfoStruct.CardNo = strCardNO.Replace(" ", "").Replace("\0", "").Trim();
                            CardRead cardUtility = new CardRead(this);
                            string strMsg = cardUtility.GetPatiantInfo();
                            if (!string.IsNullOrEmpty(strMsg))
                            {
                                return strMsg;
                            }
                            else
                            {
                                return "1";
                            }
                        }
                        else
                        {
                            return "0";
                        }

                        #endregion

                       
                    }
                    else if (CardType == "3")
                    {
                        #region 读芯片卡方法，暂时屏蔽

                        string mCardNO = CardCpu.getCardNo();

                        SkyComm.cardInfoStruct.CardNo = mCardNO;
                        CardRead cardUtility = new CardRead(this);
                        string strMsg = cardUtility.GetPatiantInfo();
                        if (!string.IsNullOrEmpty(strMsg))
                        {
                            return strMsg;
                        }
                        else
                        {
                            return "1";
                        }

                        #endregion
                    }


                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
            return "0";
        }
    

    private void FrmReadCardXH_FormClosing(object sender, FormClosingEventArgs e)
        {
            theCamera_XH.DoorLightClose(LightTypeenum.读卡器);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ReadCardClose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Log.Info(GetType().ToString(),"执行time1", "开始执行timer1_Tick");

            //检测是否有卡
            Thread.Sleep(200);
            StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDTESTINSERTCARD\"><arguments></arguments></invoke>");
            string strResult = "";

            try
            {
                strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            }
            catch (Exception exception)
            {
                Log.Info(GetType().ToString(), "检测有卡XuHuiInterface_DLL.XmlTcp", exception.Message);
            }
            LogService.GlobalInfoMessage("调用检测有卡方法输出返回：" + strResult + ",输出参数：" + sbinput.ToString());
            if (string.IsNullOrEmpty(sbinput.ToString()))
                return;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(sbinput.ToString());
            System.Xml.XmlNode xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDSTACKSTATE']");
            if (xnd != null)
            {
                string CardStackState = xnd.InnerText;
                if (CardStackState == "1")
                {
                    timer1.Stop();
                    timer2.Stop();

                    //如果有卡则读取磁条卡号
                    Thread.Sleep(200);
                    if (CardType == "1")
                    {
                        sbinput = new StringBuilder("<invoke name=\"READCARDREADMAGCARDNO\"><arguments></arguments></invoke>");
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用读取磁条卡号卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                        doc.LoadXml(sbinput.ToString());
                        xnd = doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']");
                        if (xnd != null)
                        {
                            string strCardNO = xnd.InnerText;
                            //如果读取到的磁条卡号长度不对，则读取居民健康卡
                            if (strCardNO.Length != 15)
                            {
                                try
                                {
                                    //LogService.GlobalInfoMessage("2");
                                    //移动到IC位读居民健康卡
                                    sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTOREAR\"><arguments></arguments></invoke>");
                                    Thread.Sleep(200);
                                    LogService.GlobalInfoMessage("调用移动卡到卡后端方法输入参数：" + sbinput.ToString());
                                    strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                                    LogService.GlobalInfoMessage("调用移动卡到卡后端方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                                    if (sbinput.ToString().IndexOf("SUCCESS") > 0)
                                    {
                                        //读居民健康卡
                                        //居民健康卡                           
                                        HealthCardBase _cardBase = HealthCardBase.CreateInstance(this);
                                        HealthCardInfoStruct Entity = _cardBase.HisRead();
                                        SkyComm.cardInfoStruct.CardNo = Entity.HealthCardNo;
                                        if (!backgroundWorker1.IsBusy)
                                            backgroundWorker1.RunWorkerAsync();
                                        if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                                        {
                                            SkyComm.ShowMessageInfo("读卡失败，请重新检查卡位方向!");
                                            closePort();
                                            this.DialogResult = DialogResult.Cancel;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogService.GlobalInfoMessage("移动卡到卡后端，读居民健康卡失败：" + ex.Message);

                                    Thread.Sleep(200);
                                    sbinput = new StringBuilder("<invoke name=\"READCARDOUTCARD\"><arguments></arguments></invoke>");
                                    strResult = AutoServiceSDK.SDK.XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                                    LogService.GlobalInfoMessage("调用退卡卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString()); ;
                                    SkyComm.ShowMessageInfo("读卡失败，请重新检查卡位方向!");
                                    closePort();
                                    this.DialogResult = DialogResult.Cancel;
                                }
                            }
                            else
                            {
                                //读取就诊卡
                                SkyComm.cardInfoStruct.CardNo = strCardNO;
                                if (!backgroundWorker1.IsBusy)
                                    backgroundWorker1.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            //移动到IC位读居民健康卡
                            sbinput = new StringBuilder("<invoke name=\"ReadCardMoveCardToIC\"><arguments></arguments></invoke>");
                            Thread.Sleep(200);
                            strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                            if (strResult.IndexOf("SUCCESS") > 0)
                            {
                                //读居民健康卡
                                //居民健康卡                           
                                HealthCardBase _cardBase = HealthCardBase.CreateInstance(this);
                                HealthCardInfoStruct Entity = _cardBase.HisRead();
                                SkyComm.cardInfoStruct.CardNo = Entity.HealthCardNo;
                                if (!backgroundWorker1.IsBusy)
                                    backgroundWorker1.RunWorkerAsync();
                            }
                        }
                    }
                    else if(CardType == "2")
                    {
                        #region 读取M1卡方法
                        //卡片移到射频位
                        sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTORF\"><arguments></arguments></invoke>");
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用卡片移到射频位方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                        //读第8块的数据
                        string strInput = "<invoke name=\"READCARDREADRFCARD\"><arguments>" +
                            "<string id=\"SECTORNO\">8</string>" +
                            "<string id=\"BLOCKNO\">2</string>" +
                            "<string id=\"PASSWORD\">FFFFFFFFFFFF</string>" +
                            "</arguments></invoke>";
                        LogService.GlobalInfoMessage("调用读射频卡数据方法输入参数1：" + strInput);
                        sbinput = new StringBuilder(strInput);
                        strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用读射频卡数据方法返回：" + strResult + ",输出参数：" + sbinput.ToString());

                        doc.LoadXml(sbinput.ToString());
                        xnd = doc.SelectSingleNode("/return/arguments/string[@id='ERROR']");
                        string strCardNO = string.Empty;
                        if (xnd != null && xnd.InnerText == "SUCCESS")
                        {
                            strCardNO = SkyComm.HexStringToString(doc.SelectSingleNode("/return/arguments/string[@id='CARDNO']").InnerText, ASCIIEncoding.ASCII);
                            LogService.GlobalInfoMessage("=============读8扇区的内容===========================" + strCardNO + "======================================");

                            if (strCardNO.Length >= 16)
                            {
                                strCardNO = strCardNO.Substring(0, 16);
                            }
                        }

                        if (strCardNO.Length >= 16)
                        {
                        }


                        LogService.GlobalInfoMessage("读取到的卡号信息：" + strCardNO + "===========================");

                        if (!string.IsNullOrEmpty(strCardNO) && strCardNO.Length >= 15)
                        {
                            //读取就诊卡
                            SkyComm.cardInfoStruct.CardNo = strCardNO.Replace(" ", "").Replace("\0", "").Trim();
                            if (!backgroundWorker1.IsBusy)
                                backgroundWorker1.RunWorkerAsync();
                        }
                        else
                        {
                            SkyComm.ShowMessageInfo("读卡失败，请重新检查卡位方向!");
                            closePort();
                            this.DialogResult = DialogResult.Cancel;
                        }
                        #endregion


                    }
                    else if (CardType == "3")
                    {

                        #region 读取芯片卡方法，暂时屏蔽
                        SkyComm.cardInfoStruct.CardNo = CardCpu.getCardNo();


                        if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                        {
                            try
                            {
                                //LogService.GlobalInfoMessage("2");
                                //移动到IC位读居民健康卡
                                sbinput = new StringBuilder("<invoke name=\"READCARDMOVECARDTOREAR\"><arguments></arguments></invoke>");
                                Thread.Sleep(200);
                                LogService.GlobalInfoMessage("调用移动卡到卡后端方法输入参数：" + sbinput.ToString());
                                strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                                LogService.GlobalInfoMessage("调用移动卡到卡后端方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                                if (sbinput.ToString().IndexOf("SUCCESS") > 0)
                                {
                                    //读居民健康卡
                                    //居民健康卡                           
                                    HealthCardBase _cardBase = HealthCardBase.CreateInstance(this);
                                    HealthCardInfoStruct Entity = _cardBase.HisRead();
                                    SkyComm.cardInfoStruct.CardNo = Entity.HealthCardNo;
                                    if (!backgroundWorker1.IsBusy)
                                        backgroundWorker1.RunWorkerAsync();
                                    if (string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                                    {
                                        SkyComm.ShowMessageInfo("读卡失败，请重新检查卡位方向!");
                                        closePort();
                                        this.DialogResult = DialogResult.Cancel;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                LogService.GlobalInfoMessage("移动卡到卡后端，读居民健康卡失败：" + ex.Message);

                                Thread.Sleep(200);
                                sbinput = new StringBuilder("<invoke name=\"READCARDOUTCARD\"><arguments></arguments></invoke>");
                                strResult = AutoServiceSDK.SDK.XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                                LogService.GlobalInfoMessage("调用退卡卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString()); ;
                                SkyComm.ShowMessageInfo("读卡失败，请重新检查卡位方向!");
                                closePort();
                                this.DialogResult = DialogResult.Cancel;
                            }
                        }
                        else
                        {

                            CardRead cardUtility = new CardRead(this);

                            string strMsg = cardUtility.GetPatiantInfo();

                            if (!string.IsNullOrEmpty(strMsg))
                            {
                                SkyComm.ShowMessageInfo(strMsg);
                            }

                            if (!backgroundWorker1.IsBusy)

                                backgroundWorker1.RunWorkerAsync();
                        }



                        #endregion
                    }

                }
            }
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
        /// 关闭窗体，禁止进卡
        /// </summary>
        private void ReadCardClose()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            //StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDNOTALLOWCARDIN\"><arguments></arguments></invoke>");
            //string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
            //LogService.GlobalInfoMessage("调用禁止进卡号卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
            this.DialogResult = DialogResult.Cancel;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SkyComm.cardInfoStruct.CardNo))
                {
                    CardRead cardUtility = new CardRead(this);
                    
                    string strMsg = cardUtility.GetPatiantInfo();
                    if (!string.IsNullOrEmpty(strMsg))
                    {
                        SkyComm.ShowMessageInfo(strMsg);
                        e.Result = "失败";
                        closePort();

                        StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDOUTCARD\"><arguments></arguments></invoke>");
                        string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                        LogService.GlobalInfoMessage("调用退卡卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                    }
                    else
                    {
                        e.Result = "成功";
                    }
                }
                else
                {
                    SkyComm.ShowMessageInfo("请检查您的就诊卡位置然后重试或者在窗口办理业务!");
                    e.Result = "失败";
                    closePort();

                    StringBuilder sbinput = new StringBuilder("<invoke name=\"READCARDOUTCARD\"><arguments></arguments></invoke>");
                    string strResult = XuHuiInterface_DLL.XmlTcp(sbinput, 0);
                    LogService.GlobalInfoMessage("调用退卡卡方法返回：" + strResult + ",输出参数：" + sbinput.ToString());
                }
                
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo(ex.Message);
                closePort();
                e.Result = "失败";
                return;
            }
        }
        private void closePort()
        {
            SkyComm.cardInfoStruct.CardNo = string.Empty;
            if (SkyComm.eCardAuthorizationData.Tables.Count > 0)
                SkyComm.eCardAuthorizationData.Tables[0].Clear();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && e.Result.Equals("失败"))
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请插入您的就诊卡!");
            voice.EndJtts();
        }

    }
}
