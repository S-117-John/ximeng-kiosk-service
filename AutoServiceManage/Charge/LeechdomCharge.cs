using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityData.His.Clinic;
using System.Data;
using Skynet.Framework.Common;

namespace AutoServiceManage.Charge
{
    public class LeechdomCharge
    {
        public DetailAccountData detailAccountData;
        public EcipeMedicineData ecipeMedicineData;
        private int detailAccountID;
        private string DiagnoseID;
        public string operatorid = string.Empty;
        private string serialNo;
        public LeechdomCharge(string diagnoseid,string opid)
        {
            DiagnoseID = diagnoseid;
            operatorid = opid;
            detailAccountData = new DetailAccountData();
            ecipeMedicineData = new EcipeMedicineData();
        }

        public void AddRecipeCharge(DataSet recipeInfo)
        {
            decimal money = 0;
            string OrderID = "";
            DataRow selrow = null;
            money = 0;
            decimal packAmount = 0;
            detailAccountID = 0;
            
            foreach (DataRow rowRecipe in recipeInfo.Tables[0].Rows)
            {
                selrow = rowRecipe;
                money = Convert.ToDecimal(selrow["TOTALMONEY"]);
                string summary = string.Empty;
                string itemid = string.Empty;

                if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
                {
                    #region 收费项目
                    AddNewRecipeClinic(selrow, out OrderID);
                    foreach (DataRow row in detailAccountData.Tables[0].Rows)
                    {
                        if (rowRecipe["RECIPETYPE"].ToString() != "附加")
                        {
                            if (row["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                                row["SUMMARY"].ToString() == selrow["SUMMARY"].ToString() &&
                                row["ITEMID"].ToString() == selrow["ITEMID"].ToString() &&
                                row["DETAILACCOUNTID"].ToString().Trim() == OrderID &&
                                row["UNITECODE"].ToString().Trim() == selrow["RECIPECONTENT"].ToString().Trim()) //在组合收费表的CODENO对应收费项目的ITEMID
                            {
                                row.BeginEdit();
                                row["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]) + Convert.ToDecimal(selrow["AMOUNT"]);
                                //							row["UNITPRICE"] =selrow["UNITPRICE"];
                                row["MONEY"] = Convert.ToDecimal(row["MONEY"]) + money;
                                row.EndEdit();
                            }
                        }
                        else
                        {
                            if (row["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                                row["SUMMARY"].ToString() == selrow["SUMMARY"].ToString() &&
                                row["ITEMID"].ToString() == selrow["ITEMID"].ToString() &&
                                row["DETAILACCOUNTID"].ToString().Trim() == OrderID)
                            {
                                row.BeginEdit();
                                row["AMOUNT"] = Convert.ToDecimal(row["AMOUNT"]) + Convert.ToDecimal(selrow["AMOUNT"]);
                                //							row["UNITPRICE"] =selrow["UNITPRICE"];
                                row["MONEY"] = Convert.ToDecimal(row["MONEY"]) + money;
                                row.EndEdit();
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 药品，医材
                    packAmount = Convert.ToDecimal(selrow["AMOUNT"]);

                    //packAmount = packAmount * Convert.ToDecimal(selrow["DOSECOUNT"]);

                    AddNewRecipeClinic(selrow, out OrderID);

                   
                    foreach (DataRow rowYpmx in ecipeMedicineData.Tables[0].Rows)
                    {
                        switch (selrow["SUMMARY"].ToString())
                        {
                            case "西药":
                                summary = "西药费";
                                break;
                            case "中成药":
                                summary = "中成药";
                                break;
                            case "中草药":
                                summary = "中草药";
                                break;
                            case "医材":
                                summary = "医材费";
                                break;
                        }

                        if (rowYpmx["STOREROOMID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                                rowYpmx["SUMMARY"].ToString() == summary &&
                                rowYpmx["LEECHDOMNO"].ToString() == selrow["ITEMID"].ToString() &&
                                rowYpmx["DETAILACCOUNTID"].ToString() == OrderID &&
                            rowYpmx["SERIALNO"].ToString() == serialNo &&
                            rowYpmx["ECIPENUM"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                        {
                            rowYpmx.BeginEdit();
                            rowYpmx["AMOUNT"] = Convert.ToDecimal(rowYpmx["AMOUNT"]) + packAmount;
                            rowYpmx["OLDAMOUNT"] = Convert.ToDecimal(rowYpmx["OLDAMOUNT"]) + packAmount;
                            rowYpmx["UNITAMOUNT"] = Convert.ToDecimal(rowYpmx["UNITAMOUNT"]) + (Convert.ToDecimal(selrow["AMOUNT"]) * Convert.ToDecimal(selrow["DOSECOUNT"]));
                            rowYpmx["OLDUNITAMOUNT"] = Convert.ToDecimal(rowYpmx["OLDUNITAMOUNT"]) + (Convert.ToDecimal(selrow["AMOUNT"]) * Convert.ToDecimal(selrow["DOSECOUNT"]));
                            rowYpmx["TOTALMONEY"] = Convert.ToDecimal(rowYpmx["TOTALMONEY"]) + money;
                            rowYpmx.EndEdit();
                        }
                    }

                    summary = string.Empty;
                    itemid = string.Empty;
                    foreach (DataRow rowSfxm in detailAccountData.Tables[0].Rows)
                    {
                        switch (selrow["SUMMARY"].ToString())
                        {
                            case "西药":
                                summary = "西药费";
                                itemid = "01";
                                break;
                            case "中成药":
                                summary = "中成药";
                                itemid = "02";
                                break;
                            case "中草药":
                                summary = "中草药";
                                itemid = "03";
                                break;
                            case "医材":
                                summary = "医材费";
                                itemid = "04";
                                break;
                        }

                        if (rowSfxm["EXECOFFICEID"].ToString() == selrow["EXECOFFICEID"].ToString() &&
                            rowSfxm["SUMMARY"].ToString() == summary &&
                            rowSfxm["ITEMID"].ToString() == itemid &&
                            rowSfxm["DETAILACCOUNTID"].ToString() == OrderID &&
                            rowSfxm["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                        {
                            rowSfxm.BeginEdit();
                            rowSfxm["AMOUNT"] = 0;
                            rowSfxm["UNITPRICE"] = 0;
                            rowSfxm["MONEY"] = Convert.ToDecimal(rowSfxm["MONEY"]) + money;
                            rowSfxm.EndEdit();
                        }
                    }
                    #endregion
                }
            }

            money = 0;

            decimal dMoney = 0;
            int iConfigClinicMoney = Convert.ToInt32(SystemInfo.SystemConfigs["门诊收费时分币处理方式"].DefaultValue);
            foreach (DataRow dr in detailAccountData.Tables[0].Rows)
            {

                dr.BeginEdit();
                //dr[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = "现金";
                dr[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = "预交金";
                dr["ISBANKCARD"] = 0;
                dMoney = DecimalRound.Round(Convert.ToDecimal(dr[DetailAccountData.D_DETAIL_ACCOUNT_MONEY]), 2);
                switch (iConfigClinicMoney)
                {
                    //0.不处理;1.四舍五入;2.见分进位;
                    case 0:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney, 2);
                        break;
                    case 1:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney, 1);
                        break;
                    case 2:
                        dr[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = DecimalRound.Round(dMoney + Convert.ToDecimal(0.04), 1);
                        break;
                }

                dr[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = DecimalRound.Round(dMoney, 2);

                dr["PITCHON"] = false;
                dr["ORDERID"] = Convert.ToInt64(dr["DETAILACCOUNTID"]);
                dr["OLDORDERID"] = Convert.ToInt64(dr["DETAILACCOUNTID"]);
                dr.EndEdit();
            }

            //处理行序号
            foreach (DataRow datarow in ecipeMedicineData.Tables[0].Rows)
            {
                datarow.BeginEdit();
                datarow["ORDERID"] = datarow["DETAILACCOUNTID"];
                datarow["OLDORDERID"] = datarow["DETAILACCOUNTID"];
                datarow.EndEdit();
            }
        }

        private void AddNewRecipeClinic(DataRow selrow, out string ID)
        {
            //bool isHaving = false;
            bool havingSfmx = false;
            bool havingYpmx = false;
            string OrderID = Convert.ToString(detailAccountData.Tables[0].Rows.Count + 1);//selrow["CLINICRECIPEID"].ToString().Trim();
            string Summary = "";
            string ItemID = "";
            string execOfficeName;
            string execOfficeID;

            execOfficeName = selrow["EXECOFFICE"].ToString().Trim();
            execOfficeID = selrow["EXECOFFICEID"].ToString().Trim();

            if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
            {
                ItemID = selrow["ITEMID"].ToString().Trim();
                Summary = selrow["SUMMARY"].ToString().Trim();
            }
            else
            {
                switch (selrow["SUMMARY"].ToString())
                {
                    case "西药":
                        ItemID = "01";
                        Summary = "西药费";
                        break;
                    case "中成药":
                        ItemID = "02";
                        Summary = "中成药";
                        break;
                    case "中草药":
                        ItemID = "03";
                        Summary = "中草药";
                        break;
                    case "医材费":
                        ItemID = "04";
                        Summary = "医材费";
                        break;
                }
            }

            //药品费,医材费
            //STOREROOM,LEECHDOMNO
            foreach (DataRow rowYpmx in ecipeMedicineData.Tables[0].Rows)
            {
                if (rowYpmx["STOREROOMID"].ToString().Trim() == execOfficeID &&
                    rowYpmx["SUMMARY"].ToString().Trim() == Summary &&
                    rowYpmx["LEECHDOMNO"].ToString().Trim() == selrow["ITEMID"].ToString().Trim() &&
                    rowYpmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                {
                    //mitao 20121127 不能读取该配置7.0和7.1版本以后如果有相同药品时不再合并。
                    //if (SystemInfo.SystemConfigs["门诊收费相同药品是否合并"].DefaultValue == "0")
                    //{
                    havingYpmx = false;
                    //}
                    //else
                    //{
                    //    havingYpmx = true;
                    //}
                }
            }

            if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
            {
                foreach (DataRow rowSfmx in detailAccountData.Tables[0].Rows)
                {
                    if (selrow["RECIPETYPE"].ToString() != "附加")
                    {
                        if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                            rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                            rowSfmx["ITEMID"].ToString().Trim() == ItemID &&
                            rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim() &&
                            rowSfmx["UNITECODE"].ToString().Trim() == selrow["RECIPECONTENT"].ToString().Trim())
                        {
                            havingSfmx = true;
                        }
                    }
                    else
                    {
                        if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                              rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                              rowSfmx["ITEMID"].ToString().Trim() == ItemID &&
                              rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                        {
                            havingSfmx = true;
                        }
                    
                    }
                }
            }
            else
            {
                foreach (DataRow rowSfmx in detailAccountData.Tables[0].Rows)
                {
                    if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                        rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                        rowSfmx["ITEMID"].ToString().Trim() == ItemID &&
                        rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                    {
                        havingSfmx = true;
                    }
                }
            }
            foreach (DataRow rowSfmx in detailAccountData.Tables[0].Rows)
            {
                if (rowSfmx["EXECOFFICEID"].ToString().Trim() == execOfficeID &&
                    rowSfmx["SUMMARY"].ToString().Trim() == Summary &&
                    rowSfmx["CLINICRECIPEID"].ToString().Trim() == selrow["CLINICRECIPEID"].ToString().Trim())
                {
                    OrderID = rowSfmx["ORDERID"].ToString().Trim();
                    break;
                }
            }


            //住院明细帐业务实体(检治费)
            if (selrow["SUMMARY"].ToString().Trim() != "西药" && selrow["SUMMARY"].ToString().Trim() != "中成药" && selrow["SUMMARY"].ToString().Trim() != "中草药" && selrow["SUMMARY"].ToString().Trim() != "医材")
            {
                if (havingSfmx == false)
                {
                    AddNewRecipeSfxmDetailAccountData(OrderID, selrow);
                }
            }
            else //住院处方明细业务实体(药品费，医材费)
            {
                if (havingSfmx == false)
                {
                    AddNewRecipeYpDetailAccountData(OrderID, selrow);
                }
                if (havingYpmx == false)
                {
                    AddNewRecipeEcipeMedicineData(OrderID, selrow);
                }
            }

            ID = OrderID;
        }

        private void AddNewRecipeEcipeMedicineData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = ecipeMedicineData.Tables[0].Rows.Count + 1;
            DataRow rowYp = ecipeMedicineData.Tables[0].NewRow();
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDORDERID] = OrderID;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ORDERID] = OrderID;
            //xhw 2012-11-10 
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SERIALNO] = SelUniteRow["RECIPELISTNUM"];//ecipeMedicineData.Tables[0].Select("DetailAccountID = '" + DetailAccountID + "'").Length + 1;
            serialNo = rowYp["SERIALNO"].ToString().Trim();
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DETAILACCOUNTID] = DetailAccountID;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_CANCELMARK] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_PITCHON] = false;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOCTORID] = SelUniteRow["DOCTORID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_STOREROOMID] = SelUniteRow["EXECOFFICEID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_STOREROOM] = SelUniteRow["EXECOFFICE"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OPERATORID] = operatorid;

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_LEECHDOMNO] = SelUniteRow["ITEMID"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_LEECHDOMNAME] = SelUniteRow["CHARGEITEM"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SPECS] = SelUniteRow["SPECS"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_MEDICARETYPE] = SelUniteRow["MEDICARETYPE"];

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNIT] = SelUniteRow["UNIT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNITPRICE] = SelUniteRow["UNITPRICE"];

            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDUNITPRICE] = SelUniteRow["UNITPRICE"];
            //划价号（处方号）
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ECIPENUM] = "";
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_AMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_TOTALMONEY] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOSAGE] = SelUniteRow["DOSECOUNT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SENDLEECHDOMMARK] = 0;//药房发药标示0=未发；1=已发
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_UNITAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OLDUNITAMOUNT] = 0;
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_OUTPATIENTUNIT] = SelUniteRow["OUTPATIENTUNIT"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_CHANGERATIO] = SelUniteRow["CHANGERATIO"];
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_ECIPENUM] = SelUniteRow["CLINICRECIPEID"];//将处方号保存到药品的划价号中，为了在药房方便打印处方
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_DOSE] = 0;//输入剂量
            rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_TOPRETAILPRICE] = SelUniteRow["TOPRETAILPRICE"];//最高限价

            switch (SelUniteRow["SUMMARY"].ToString().Trim())
            {
                case "西药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "西药费";
                    break;
                case "中成药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "中成药";
                    break;
                case "中草药":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "中草药";
                    break;
                case "医材":
                    rowYp[EcipeMedicineData.D_ECIPE_MEDICINE_SUMMARY] = "医材费";
                    break;
            }
            ecipeMedicineData.Tables[0].Rows.Add(rowYp);
        }

        private void AddNewRecipeYpDetailAccountData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = detailAccountData.Tables[0].Rows.Count + 1;
            DataRow rowSfxm = detailAccountData.Tables[0].NewRow();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OLDORDER] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ORDERID] = OrderID;

            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DETAILACCOUNTID] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DIAGNOSEID] = DiagnoseID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CANCELMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_PITCHON] = false;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DOCTORID] = SelUniteRow["DOCTORID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ESCAPECHARGEMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_EXECOFFICEID] = SelUniteRow["EXECOFFICEID"];
            rowSfxm["OFFICE"] = SelUniteRow["EXECOFFICE"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATORID] = operatorid;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITPRICE] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEDATE] = DateTime.Now;
            //自付金额SELFMONEY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = 0;
            //BALANCEMODE结算方式
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = 0;
            //现金支付额CASHDEFRAY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DISCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTBALANCE] = 0;
            //OPERATEORDERID操作流水号
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEORDERID] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OVERTYPE] = 0;//重打标识,0=正常;1=重打
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_AMOUNT] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MONEY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTERID] = SelUniteRow["REGISTERID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CLINICRECIPEID] = SelUniteRow["CLINICRECIPEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CARDID]= SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();

            if (SelUniteRow["SICKTYPE"].ToString().Trim() != string.Empty)
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = SelUniteRow["SICKTYPE"].ToString().Trim();
            }
            else
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = "0001";
            }


            switch (SelUniteRow["SUMMARY"].ToString().Trim())
            {
                case "西药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "西药费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "西药费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "01";
                    break;
                case "中成药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "中成药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "中成药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "02";
                    break;
                case "中草药":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "中草药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "中草药";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "03";
                    break;
                case "医材":
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = "医材费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = "医材费";
                    rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = "04";
                    break;
            }

            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_INVOICEITEM] = SelUniteRow["INVOICEITEM"];
            detailAccountData.Tables[0].Rows.Add(rowSfxm);
        }

        private void AddNewRecipeSfxmDetailAccountData(string DetailAccountID, DataRow SelUniteRow)
        {
            int OrderID = detailAccountData.Tables[0].Rows.Count + 1;
            DataRow rowSfxm = detailAccountData.Tables[0].NewRow();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OLDORDER] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ORDERID] = OrderID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DETAILACCOUNTID] = DetailAccountID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DIAGNOSEID] = DiagnoseID;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CANCELMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ITEMID] = SelUniteRow["ITEMID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CHARGEITEM] = SelUniteRow["CHARGEITEM"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SUMMARY] = SelUniteRow["SUMMARY"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_PITCHON] = false;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DOCTORID] = SelUniteRow["DOCTORID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTEROFFICEID] = SelUniteRow["REGISTEROFFICEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ESCAPECHARGEMARK] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CODENO] = SelUniteRow["CODENO"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNIT] = SelUniteRow["UNIT"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CARDID] = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_EXECOFFICEID] = SelUniteRow["EXECOFFICEID"];
            rowSfxm["OFFICE"] = SelUniteRow["EXECOFFICE"];

            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATORID] = operatorid;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITPRICE] = SelUniteRow["UNITPRICE"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEDATE] = DateTime.Now;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MEDICARETYPE] = SelUniteRow["MEDICARETYPE"];
            //自付金额SELFMONEY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SELFMONEY] = 0;
            //BALANCEMODE结算方式
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_BALANCEMODE] = 0;
            //现金支付额CASHDEFRAY
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CASHDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_DISCOUNTDEFRAY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_ACCOUNTBALANCE] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OVERTYPE] = 0;//重打标识,0=正常;1=重打
            //OPERATEORDERID操作流水号
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_OPERATEORDERID] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_AMOUNT] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_MONEY] = 0;
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_REGISTERID] = SelUniteRow["REGISTERID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_CLINICRECIPEID] = SelUniteRow["CLINICRECIPEID"];
            rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_INVOICEITEM] = SelUniteRow["INVOICEITEM"];

            if (SelUniteRow["SICKTYPE"].ToString().Trim() != string.Empty)
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = SelUniteRow["SICKTYPE"].ToString().Trim();
            }
            else
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_SICKTYPEID] = "0001";
            }
            //mitao 20141218 当时附加费时，处方表中的记录的是收费项目ID,不是医生用语ID,UNITECODE不用保存，否则在个人结算,
            //               统一结算发票补打时，输出的项目名称不正确,在这里附加费为空时，同时要修改扣费时更新处方的问题。19870   
            if (SelUniteRow["RECIPETYPE"].ToString() != "附加")
            {
                rowSfxm[DetailAccountData.D_DETAIL_ACCOUNT_UNITECODE] = SelUniteRow["RECIPECONTENT"];
            }
            detailAccountData.Tables[0].Rows.Add(rowSfxm);
        }
    }
}
