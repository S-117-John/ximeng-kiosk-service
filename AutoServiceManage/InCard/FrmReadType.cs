using AutoServiceManage.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityData.His.CardClubManager;
using BusinessFacade.His.CardClubManager;
using Skynet.Framework.Common;
using BusinessFacade.His.Common;
using System.Collections;
using AutoServiceManage.Electronics;

namespace AutoServiceManage.InCard
{
    public partial class FrmReadType : Form
    {
        public int sec { get; set; }

        CardAuthorizationFacade eCardAuthorizationFacade = new CardAuthorizationFacade();
        private Boolean _isVilid = false;//wangchenyang 2018/6/6 case 31022 系统程序能够达到卡有效期为1天时，超过1天此卡不能在流通
        public FrmReadType()
        {
            InitializeComponent();
            timer1.Start();
            sec = 30;
        }
        public CardType? cardType = null;
        public string strMsg = string.Empty;
        public enum CardType
        {
            Entitycard = 1,//实体卡
            IdentityCard = 2　//身份证
        }
        private void picCard_Click(object sender, EventArgs e)
        {
            cardType = CardType.Entitycard;

            this.DialogResult = DialogResult.Cancel;
            timer1.Stop();
        }

        private void identityCard_Click(object sender, EventArgs e)
        {

            if (SkyComm.getvalue("自助机类型")=="0")
            {
                #region  柜式自助机驱动
                FrmReadIdenetityGS FrmReadIdenetity = new FrmReadIdenetityGS();
                try
                {
                    timer1.Stop();
                    if (FrmReadIdenetity.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        #region 验证卡状态
                        string CardID = FrmReadIdenetity.strCardNO;
                        BusinessFacade.His.CardClubManager.CardAuthorizationFacade eCardAuthorizationFacade = new BusinessFacade.His.CardClubManager.CardAuthorizationFacade();
                        if (SystemInfo.SystemConfigs["是否启用就诊卡与副卡关联"].DefaultValue.ToString() == "1")
                        {
                            bool isSecondaryCard = false;//是否副卡
                            CardID = eCardAuthorizationFacade.GetMotherCardID(CardID, "", ref isSecondaryCard);
                        }
                        CardAuthorizationData eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(CardID);
                        if (eCardAuthorizationData.Tables[0].Rows.Count == 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "卡号无效");
                            registerInfo.ShowDialog();
                            this.Close();
                            return;
                        }
                        CardID = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();
                        if (!string.IsNullOrEmpty(CardID))
                        {
                            if (Convert.ToInt32(eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 1)
                            {
                                MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "此卡已挂失不能使用!");
                                registerInfo.ShowDialog();
                                this.Close();
                                return;
                            }
                            if (Convert.ToInt32(eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 2)
                            {
                                MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "此卡已注销不能使用");
                                registerInfo.ShowDialog();
                                this.Close();
                                return;
                            }
                            if (Skynet.Framework.Common.SystemInfo.SystemConfigs["是否启用卡有效期"].DefaultValue.Equals("1"))
                            {
                                var dtCard = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].AsEnumerable();
                                _isVilid = dtCard.Where(a => a.Field<DateTime>(CardAuthorizationData.T_CARD_AUTHORIZATION_CARDVALIDDATE) <= DateTime.Now).ToList().Count > 0 ? true : false;
                                if (_isVilid)
                                {
                                    MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "该卡已经过了有效期，请联系管理员！");
                                    registerInfo.ShowDialog();
                                    this.Close();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "请持身份证在窗口签约");
                            registerInfo.ShowDialog();
                            this.Close();
                            return;
                        }
                        #endregion
                        SkyComm.cardInfoStruct.CardNo = CardID;
                        SkyComm.DiagnoseID = eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        SkyComm.cardInfoStruct.isIdentityCard = true;
                        CardRead cardUtility = new CardRead(this);
                        strMsg = cardUtility.GetPatiantInfo();
                        this.DialogResult = DialogResult.OK;
                    }
                    timer1.Start();
                }
                catch (Exception ex)
                {
                    timer1.Start();
                    FrmReadIdenetity.Dispose();
                    Skynet.LoggingService.LogService.GlobalInfoMessage("读取居民身份证信息失败：" + ex.Message);
                    return;
                }
                finally
                {
                    FrmReadIdenetity.Dispose();
                }
                #endregion
                
            }
            else
            {
                #region  壁挂式自助机驱动
                FrmReadIdenetityBG FrmReadIdenetity = new FrmReadIdenetityBG();
                try
                {
                    timer1.Stop();
                    if (FrmReadIdenetity.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        #region 验证卡状态
                        string CardID = FrmReadIdenetity.strCardNO;
                        BusinessFacade.His.CardClubManager.CardAuthorizationFacade eCardAuthorizationFacade = new BusinessFacade.His.CardClubManager.CardAuthorizationFacade();
                        if (SystemInfo.SystemConfigs["是否启用就诊卡与副卡关联"].DefaultValue.ToString() == "1")
                        {
                            bool isSecondaryCard = false;//是否副卡
                            CardID = eCardAuthorizationFacade.GetMotherCardID(CardID, "", ref isSecondaryCard);
                        }
                        CardAuthorizationData eCardAuthorizationData = (CardAuthorizationData)eCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(CardID);
                        if (eCardAuthorizationData.Tables[0].Rows.Count == 0)
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "卡号无效");
                            registerInfo.ShowDialog();
                            this.Close();
                            return;
                        }
                        CardID = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();
                        if (!string.IsNullOrEmpty(CardID))
                        {
                            if (Convert.ToInt32(eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 1)
                            {
                                MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "此卡已挂失不能使用!");
                                registerInfo.ShowDialog();
                                this.Close();
                                return;
                            }
                            if (Convert.ToInt32(eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 2)
                            {
                                MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "此卡已注销不能使用");
                                registerInfo.ShowDialog();
                                this.Close();
                                return;
                            }
                            if (Skynet.Framework.Common.SystemInfo.SystemConfigs["是否启用卡有效期"].DefaultValue.Equals("1"))
                            {
                                var dtCard = eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].AsEnumerable();
                                _isVilid = dtCard.Where(a => a.Field<DateTime>(CardAuthorizationData.T_CARD_AUTHORIZATION_CARDVALIDDATE) <= DateTime.Now).ToList().Count > 0 ? true : false;
                                if (_isVilid)
                                {
                                    MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "该卡已经过了有效期，请联系管理员！");
                                    registerInfo.ShowDialog();
                                    this.Close();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MyAlert registerInfo = new MyAlert(AlertTypeenum.信息, "请持身份证在窗口签约");
                            registerInfo.ShowDialog();
                            this.Close();
                            return;
                        }
                        #endregion
                        SkyComm.cardInfoStruct.CardNo = CardID;
                        SkyComm.DiagnoseID = eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        SkyComm.cardInfoStruct.isIdentityCard = true;
                        CardRead cardUtility = new CardRead(this);
                        strMsg = cardUtility.GetPatiantInfo();
                        this.DialogResult = DialogResult.OK;
                    }
                    timer1.Start();
                }
                catch (Exception ex)
                {
                    timer1.Start();
                    FrmReadIdenetity.Dispose();
                    Skynet.LoggingService.LogService.GlobalInfoMessage("读取居民身份证信息失败：" + ex.Message);
                    return;
                }
                finally
                {
                    FrmReadIdenetity.Dispose();
                }
                #endregion
               
            }



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ReadCardClose();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void ReadCardClose()
        {
            this.timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (sec == 0)
                {
                    ReadCardClose();
                }
                sec = sec - 1;
                this.btnClose.Text = "返回(" + sec.ToString() + ")";
            }
            catch (Exception ex)
            {
                timer1.Stop();
                SkyComm.ShowMessageInfo(ex.Message);

            }
        }
        private QuerySolutionFacade query = new QuerySolutionFacade();

        CardAuthorizationFacade theCardAuthorizationFacade = new CardAuthorizationFacade();
        private void timer2_Tick(object sender, EventArgs e)
        {
            string eCardNo = string.Empty;
            if (!string.IsNullOrEmpty(textBox_ecard.Text.ToString()))
            {
                eCardNo = textBox_ecard.Text.ToString().Split(':')[0].ToString();
               
            }
            string sql1 = "select * from t_card_authorization where VICECARDID = @VICECARDID and CIRCUIT_STATE = 0";
            Hashtable hashtable = new Hashtable();

            hashtable.Add("@VICECARDID", eCardNo);
            DataSet dataSet = query.ExeQuery(sql1, hashtable);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string cardNo = dataSet.Tables[0].Rows[0]["CARDID"].ToString();
                SkyComm.cardInfoStruct.CardNo = cardNo;
                SkyComm.DiagnoseID = dataSet.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                SkyComm.eCardAuthorizationData = (CardAuthorizationData)theCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
                this.DialogResult = DialogResult.OK;
            }
        }

      

        private void FrmReadType_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            SkyComm.SCANCARD = true;
            //this.textBox_ecard.Focus();
            //this.textBox_ecard.SelectAll();

            //timer2.Start();
            
            timer1.Stop();
            using (FormScan formScan = new FormScan())
            {
                if (formScan.ShowDialog()==DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请选择您的就诊卡类型");
            voice.EndJtts();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请在扫码口扫描您的电子健康卡二维码");
            voice.EndJtts();
        }
    }
}
