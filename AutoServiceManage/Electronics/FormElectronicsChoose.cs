using BusinessFacade.His.Clinic;
using BusinessFacade.His.Common;
using CardInterface;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AutoServiceManage.Electronics
{
    public partial class FormElectronicsChoose : Form
    {
        private bool isHadCard = false;//签约电子卡前检测患者是否有院内卡
        private string cardNub = string.Empty;//电子卡签约获取患者既往院内卡号

        private string cbxCertificateNO = string.Empty;//身份证号
        private string patientName = string.Empty;//患者姓名
        private string strDiagnoseid = string.Empty;//诊疗号
        private string gender = string.Empty;//性别
        private string phoneNo = string.Empty;//电话
        private string birthday = string.Empty;//出生日期
        private string address = string.Empty;//地址
        private string nation = string.Empty;//民族


        public FormElectronicsChoose()
        {
            InitializeComponent();
        }
        #region 电子卡发放并签约
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                isHadCard = false;
                cardNub = string.Empty;
                string cardType, cardTypeId;
                HealthCardInfoStruct Entity = new HealthCardInfoStruct();
                if (!cardRegulation(out cardType, out cardTypeId))//电子卡注册签约条件判断
                    return;
                decimal decMoney = 0;
               
                #region 卡管平台电子卡注册
                LogService.GlobalInfoMessage("HIS组织调用电子卡数据");
                HealthCardBase _healthcardBase = HealthCardBase.CreateEhealthCardInstance(this);
                LogService.GlobalInfoMessage("电子XML加载完成");
                HealthCardInfoStruct patientInfo = new HealthCardInfoStruct();
                patientInfo.XM = patientName;
                patientInfo.XB = gender;
                patientInfo.LXDH = phoneNo;
                patientInfo.CSRQ = birthday;
                patientInfo.SFZH = cbxCertificateNO;
                patientInfo.DZ = address;
                patientInfo.MZ = nation;

                Entity = _healthcardBase.registerEHC(patientInfo);
                if (cardType == "0")
                {
                    //Entity = _healthcardBase.registerEHC(patientInfo);
                }
                else if (cardType == "1")
                {
                    //FrmMaxCard frm = new FrmMaxCard();
                    //frm.labelText = "请扫描家长电子卡......";
                    //if (frm.ShowDialog() == DialogResult.OK)
                    //    patientInfo.ehealthCardId = frm.VirtualNub.Substring(0, frm.VirtualNub.Length - 2);
                    //else
                    //    return;
                    //Entity = _healthcardBase.registerEHC_Child(patientInfo);
                }
                else
                {
                    //if (strDiagnoseid == "")
                    //{
                    //    BindHis("00000000000000000");
                    //    strDiagnoseid = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_DIAGNOSEID].ToString();
                    //    cardNub = eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();
                    //    this.txtCureID.Text = cardNub;
                    //}
                    //patientInfo.diagnoseId = strDiagnoseid;
                    //patientInfo.cardId = cardNub;
                    //Entity = _healthcardBase.registerEHC_medicalCard(patientInfo);
                }

                #endregion

                #region  HIS业务机制处理
                //if (cardType != "2")
                //{
                //    PatientInfoFacade pfacade = new PatientInfoFacade();
                //    string ConditionStr = cardType == "0" ? " AND A.IDENTITYCARD='" + cbxCertificateNO.Text.Trim() + "' " : " AND A.GUARDIANIDNUMBER='" + txtGudrdianIDNumber.Text.Trim() + "' ";
                //    ConditionStr += " AND A.PATIENTNAME='" + this.tbxName.Text + "'  ORDER BY  A.DIAGNOSEID DESC ";
                //    DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                //    if (dsState != null && dsState.Tables[0].Rows.Count > 0)
                //    {
                //        this.txtCureID.Text = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                //    }
                //}
                //virtualCardId = cardType == "1" ? Entity.childEhealthCardId : Entity.ehealthCardId;
                //if (strDiagnoseid == "")//未办卡，办卡完成后获取患者院内卡号
                //{
                //    LogService.GlobalInfoMessage("患者未办理院内卡");

                //    BindHis(virtualCardId);
                //    if (eLCardAuthorizationData != null)
                //    {
                //        cardNub = eLCardAuthorizationData.Tables[0].Rows.Count > 0 ? eLCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString() : "";
                //    }
                //}

                //if (cardNub != "")
                //{
                //    LogService.GlobalInfoMessage("患者已办理院内卡");
                //    eCardAuthorizationFacade.updateViceCardIdByCardId(cardNub, virtualCardId);//根据卡号修改电子健康卡号
                //    printVirtualCard(Entity);//打印二维码
                //}
                #endregion
            }
            catch (Exception ex)
            {
                SkynetMessage.MsgInfo(ex.Message);
            }
        }

        /// <summary>
        /// 电子卡注册签约条件判断
        /// </summary>
        /// <returns></returns>
        private bool cardRegulation(out string cardType, out string cardTypeId)
        {
            getElectronicCardType(out cardType, out cardTypeId);
            LogService.GlobalInfoMessage("XML配置发卡类型" + cardType + "卡类型ID:" + cardTypeId);
          
          
            DateTime GetServerDateTime = new CommonFacade().GetServerDateTime();
     
           
          
            PatientInfoFacade pfacade = new PatientInfoFacade();





            string electronicCardType = SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue;
            if (electronicCardType == "0" || SystemInfo.SystemConfigs["是否启用身份证签约"].DefaultValue.ToString() == "1")
            {

                #region  验证该患者是否已有院内卡
                string ConditionStr = cardType == "0" ? " AND A.IDENTITYCARD='" + cbxCertificateNO + "' " : " AND A.GUARDIANIDNUMBER='" + cbxCertificateNO + "' ";
                ConditionStr += " AND A.PATIENTNAME='" + this.patientName + "' AND B.CIRCUIT_STATE IN (0,1)  ORDER BY  A.DIAGNOSEID DESC ";
                DataSet dsState = pfacade.FindCardPatientinfoByCondition(ConditionStr);
                if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)//已办理院内卡
                {
                    if (dsState.Tables[0].Rows[0]["CARDTYPENAME"].ToString() != "身份证" && cardType == "0")
                    {
                        SkynetMessage.MsgInfo("患者[" + this.patientName + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡,请先注销");
                        return false;
                    }
                    else
                    {
                        if (dsState.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")//院内卡已挂失
                        {
                            SkynetMessage.MsgInfo("患者[" + this.patientName + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请先补发卡！");
                            return false;
                        }
                        cardNub = dsState.Tables[0].Rows[0]["CARDID"].ToString();
                        strDiagnoseid = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                        //isHadCard = true;
                    }
                }
                #endregion
            }
            else
            {
                #region 验证患者是否有用身份证签约的院内卡

                DataSet dsState = pfacade.FindIsCardTypeIdentity(cbxCertificateNO);
                if (dsState.Tables.Count > 0 && dsState.Tables[0].Rows.Count > 0)//已办理院内卡
                {
                    if (dsState.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString() == "1")//院内卡已挂失
                    {
                        SkynetMessage.MsgInfo("患者[" + this.patientName + "]已于" + Convert.ToDateTime(dsState.Tables[0].Rows[0]["PROVIDECARDDATE"].ToString()).ToShortDateString() + "办过卡，此卡已挂失，请先补发卡！");
                        return false;
                    }
                    cardNub = dsState.Tables[0].Rows[0]["CARDID"].ToString();
                    strDiagnoseid = dsState.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    //isHadCard = true;
                }
                #endregion
            }
            return true;
        }

        /// <summary>
        /// 获取电子卡卡类型和卡类型ID
        /// </summary>
        /// <returns>cardType:0电子卡  1：儿童卡 2：电子就诊卡</returns>
        private void getElectronicCardType(out string cardType, out string cardTypeId)
        {
            try
            {
                cardType = "0";
                cardTypeId = GetElectronicHealthCardConfig("CardType");

                //儿童
                //cardType = "1";
                //cardTypeId = GetElectronicHealthCardConfig("childCardType");

              
            }
            catch
            {
                cardTypeId = GetElectronicHealthCardConfig("CardType");
                cardType = "0";
            }
        }

        /// <summary>
        /// 获取电子健康卡配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string GetElectronicHealthCardConfig(string config)
        {
            LogService.GlobalInfoMessage("调用发卡类型XML配置");
            string applicationDocumentPath = Path.Combine(Application.StartupPath, "ResidentsHealthCard.xml");
            LogService.GlobalInfoMessage("XML路径读取" + applicationDocumentPath);
            if (File.Exists(applicationDocumentPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                LogService.GlobalInfoMessage("xmlDoc实例化");
                xmlDoc.Load(applicationDocumentPath);
                LogService.GlobalInfoMessage("xml路径加载完毕");
                XmlNode xmlNode = xmlDoc.SelectSingleNode("healthCardConfigs/ElectronicHealthCardConfig/" + config);
                LogService.GlobalInfoMessage("xmlNode.IndexText：" + xmlNode.InnerText);
                if (xmlNode != null)
                {
                    string CardType = xmlNode.InnerText;
                    LogService.GlobalInfoMessage("CardType:" + CardType);
                    return CardType;
                }

            }
            return "err";
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
