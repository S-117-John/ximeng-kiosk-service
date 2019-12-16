using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardInterface;
using System.Data;
using System.Windows.Forms;
using System.IO;
using BusinessFacade.His.CardClubManager;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;

namespace AutoServiceManage.Common
{
    public class CardRead : CardBase
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="parent">载体</param>
        public CardRead(object parent)
            : base(parent)
        {

        }

        /// <summary>
        /// 初始化并打开卡机
        /// </summary>
        protected override void Open()
        {
            return;
        }

        /// <summary>
        /// 写卡
        /// </summary>
        /// <param name="message"></param>
        public override void Write(CardInformationStruct message)
        {
            return;
        }

        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        public override CardInformationStruct Read()
        {

            try
            {
                return new CardInformationStruct(SkyComm.cardInfoStruct.CardNo);
            }
            catch (Exception)
            {
                throw new Exception("读卡失败！");
            }


        }
        
        /// <summary>
        /// 读卡返回病人信息实体
        /// </summary>
        /// <param name="mark">区别门诊【mark="C"】或者住院【mark!="H"】或者出院【mark!="L"】</param>
        /// <param name="cardInfoStruct">卡信息</param>
        /// <param name="isZz">是否是自助打印</param>
        /// <returns>病人信息实体</returns>
        public override DataSet OpenAndGetPatiantInfo(string mark, ref  CardInformationStruct cardInfoStruct)
        {
            if (mark.ToUpper() == "R")//若为挂号,采用原有刷卡操作逻辑
            {
                try
                {
                    SkyComm.cardInfoStruct = this.Read();

                    //FrmMain.cardInfoStruct.CardNo = "000118247826721";//仅作测试 需要删除
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            int IS_FEECHARGING_CARD = -1;
            DataSet dsReturn = new DataSet();

            BusinessFacade.His.CardClubManager.CardAuthorizationFacade eCardAuthorizationFacade =
                new BusinessFacade.His.CardClubManager.CardAuthorizationFacade();

            //判断当前卡状态
            //0 正常1 冻结2 注销
            if (cardInfoStruct.CardNo != null)
            {
                DataSet dsCard = eCardAuthorizationFacade.getCardStatusByAccount_ID(SkyComm.cardInfoStruct.CardNo);
                if (dsCard != null && dsCard.Tables.Count <= 0 && dsCard.Tables[0].Rows.Count <= 0)
                {                    
                    throw new Exception("卡号不存在或此卡已被注销，不能继续使用!");
                }
                if (dsCard.Tables[0].Rows.Count <= 0)
                {
                     throw new Exception("卡号不存在或此卡已被注销，不能继续使用!");
                }
                //SkyComm.cardData = dsCard.Copy();
                SkyComm.cardInfoStruct.CardAccountID = dsCard.Tables[0].Rows[0]["ACCOUNT_ID"].ToString();
                string cir_sta = dsCard.Tables[0].Rows[0]["CIRCUIT_STATE"].ToString();
                string cir_sta_auditing = dsCard.Tables[0].Rows[0]["AUDITING_STATE"].ToString();
                SkyComm.cardInfoStruct.CardTypeID = Convert.ToInt32(dsCard.Tables[0].Rows[0]["TYPEID"]);
                SkyComm.DiagnoseID = dsCard.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                if (cir_sta == "1")
                {                    
                    throw new Exception("此卡已冻结，不能继续使用!");
                }
                else if (cir_sta == "2")
                {
                    throw new Exception("此卡已注销，不能继续使用!");
                }
                else if (cir_sta_auditing == "1")//当卡没有冻结或注销时，才判断是否需要审核
                {
                    //卡未审核
                    throw new Exception("此卡还未审核，不能继续使用!");
                }

                if (mark.ToUpper() == "C" || mark.ToUpper() == "R")//门诊
                {
                    dsReturn = eCardAuthorizationFacade.OpenAndGetPatiantInfoByAccount_IDForClinic(SkyComm.cardInfoStruct.CardAccountID, ref IS_FEECHARGING_CARD);
                }
                else if (mark.ToUpper() == "H")//住院
                {
                    dsReturn = eCardAuthorizationFacade.OpenAndGetPatiantInfoByAccount_IDForInpatient(SkyComm.cardInfoStruct.CardAccountID, ref IS_FEECHARGING_CARD);
                }
                else if (mark.ToUpper() == "L")//出院
                {
                    dsReturn = eCardAuthorizationFacade.OpenAndGetPatiantInfoByAccount_IDForLeave(SkyComm.cardInfoStruct.CardAccountID, ref IS_FEECHARGING_CARD);
                }
                else
                {
                    throw new Exception("参数错误，不能继续使用!");
                }

                if (!Directory.Exists(Application.StartupPath + "\\ReportXML"))
                    Directory.CreateDirectory(Application.StartupPath + "\\ReportXML");//

                SkyComm.cardInfoStruct.Is_FEECHARGING_CARD = IS_FEECHARGING_CARD;
                dsReturn.WriteXml(Application.StartupPath + "\\ReportXML\\CardInterface.xml");
                return dsReturn;
            }
            return null;
        }

        public string GetPatiantInfo()
        {
            CardAuthorizationFacade theCardAuthorizationFacade = new CardAuthorizationFacade();

            SkyComm.eCardAuthorizationData = (CardAuthorizationData)theCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
            if (SkyComm.eCardAuthorizationData.Tables[0].Rows.Count == 0)
            {
                //SkyComm.ShowMessageInfo("卡号无效！ 卡号：" + SkyComm.cardInfoStruct.CardNo);
                return "卡号无效！";
            }
            string CardID = SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CARDID].ToString();

            if (!string.IsNullOrEmpty(CardID))
            {
                if (Convert.ToInt32(SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 1)
                {
                    SkyComm.ShowMessageInfo("此卡已挂失不能使用！");
                    return "此卡已挂失不能使用！";
                }
                if (Convert.ToInt32(SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0][CardAuthorizationData.T_CARD_AUTHORIZATION_CIRCUIT_STATE]) == 2)
                {
                    SkyComm.ShowMessageInfo("此卡已注销不能使用！");
                    return "此卡已注销不能使用！";
                }
                DataSet dsType = new CardTypesFacade().FindByPrimaryKey(SkyComm.eCardAuthorizationData.Tables["T_CARD_AUTHORIZATION"].Rows[0]["TYPEID"].ToString());
                if (dsType.Tables[0].Rows[0]["IS_FEECHARGING_CARD"].ToString() == "1")
                {          
                    return "此卡为不储值卡,不能使用!";
                }
            }
            else
            {                
                return "此卡信息不存在！";
            }
            if (SkyComm.eCardAuthorizationData == null || SkyComm.eCardAuthorizationData.Tables[0].Rows.Count <= 0)
            {                
                return "读取病人信息失败！";
            }

            int IS_FEECHARGING_CARD = -1;
            IS_FEECHARGING_CARD = Convert.ToInt32(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["IS_FEECHARGING_CARD"]);
            SkyComm.cardInfoStruct.Is_FEECHARGING_CARD = IS_FEECHARGING_CARD;
            SkyComm.cardInfoStruct.CardTypeID = Convert.ToInt32(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["TYPEID"]);
            SkyComm.DiagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
            if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
            {
                SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance(SkyComm.DiagnoseID), 2);
            }
            else
            {
                SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance_New(SkyComm.DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString()), 2);
            }
            CardSavingFacade cf = new CardSavingFacade();
            if (SystemInfo.SystemConfigs["院内就诊卡模式"].DefaultValue.ToString() != "2")
            {
                SkyComm.cardallmoney = cf.FindBalanceMoneyByDiagnoseID(SkyComm.DiagnoseID);
            }
            else
            {
                SkyComm.cardallmoney = cf.FindBalanceMoneyByDiagnoseID_New(SkyComm.DiagnoseID, SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["ACCOUNT_ID"].ToString());
            }
            return "";
 
        }

       

        /// <summary>zx
        /// 关闭卡机
        /// </summary>
        protected override void Close()
        {
            return;
        }
    }
}
