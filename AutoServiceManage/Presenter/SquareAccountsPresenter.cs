using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Model;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Clinic;
using BusinessFacade.His.Common;
using BusinessFacade.His.Medicare;
using BusinessFacade.His.ReportDesign;
using EntityData.His.Clinic;
using EntityData.His.Common;
using Skynet.ExceptionManagement;
using Skynet.Framework.Common;
using Skynet.LoggingService;
using TiuWeb.ReportBase;
using EntityDAO.His.Common;

namespace AutoServiceManage.Presenter
{
    /// <summary>
    /// 结账控制器
    /// </summary>
    public class SquareAccountsPresenter
    {
        private GatheringMasterFacade gatheringMasterFacade;

        private ReckonAccountsFacade reckonAccountsFacade;

        private DetailAccountFacade detailAccountFacade;

        private SquareAccountsModel mSquareAccountsModel;

        private int iAccountMark = 0;

        private int iIsStatisticRegister = 0;//是否统计挂号费用

        public static string MBeginTime;

        public static string MEndTime;

        private bool isBtnOk = false;//是否点确定

        private WaitDialogForm dialog; //等侍窗体定义

        

        private GatheringDetailFacade gatheringDetailFacade;

        private GatheringDetailData gatheringDetailData;

        private GatheringInvoiceUseDetailFacade gatheringInvoiceUseDetailFacade;

        private Form form;

        private ReckonAccountTimeFacade reckonAccountTimeFacade;

        private ReckonAccountsTimeData reckonTimeData;

        public SquareAccountsPresenter(Form form)
        {
            this.form = form;
            gatheringMasterFacade = new GatheringMasterFacade();
            reckonTimeData = new ReckonAccountsTimeData();
            reckonAccountTimeFacade = new ReckonAccountTimeFacade();
            mSquareAccountsModel = new SquareAccountsModel();
        }

        public void GetBeginTime()
        {
            reckonAccountsFacade = new ReckonAccountsFacade();
            iAccountMark = Convert.ToInt32(SystemInfo.SystemConfigs["门诊是否使用结帐功能"].DefaultValue);
            if (iAccountMark == 1)
            {
                reckonAccountsFacade = new ReckonAccountsFacade();
                //获取当前操作员的两次结帐时间
                DataSet ds = (DataSet)reckonAccountsFacade.GetChargeAccount(SysOperatorInfo.OperatorID, "门诊");
                if (ds.Tables[0].Rows.Count != 0)
                {
                   
                    if (ds.Tables[0].Rows.Count >= 2)
                    {
                        //MBeginTime = Convert.ToString(ds.Tables[0].Rows[1]["SETTLEDATE"]);
                        //MEndTime = Convert.ToString(ds.Tables[0].Rows[0]["SETTLEDATE"]);
                        MBeginTime = Convert.ToString(ds.Tables[0].Rows[0]["SETTLEDATE"]);
                        MEndTime = GetServerTime().ToString();
                    }
                    else
                    {
                        if (detailAccountFacade == null)
                        {
                            detailAccountFacade = new DetailAccountFacade();
                        }
                        MBeginTime = Convert.ToDateTime(detailAccountFacade.GetFirstChargeTime()).AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss");
                        MEndTime = Convert.ToString(ds.Tables[0].Rows[0]["SETTLEDATE"]);
                    }       
                }
            }
            else
            {
                reckonTimeData = (ReckonAccountsTimeData)reckonAccountTimeFacade.FindCustomByOperatorAndDays(SysOperatorInfo.OperatorID, "门诊", "1");
                if (reckonTimeData.Tables[0].Rows.Count == 1)
                {
                    MBeginTime = reckonTimeData.Tables[0].Rows[0]["ENDTIME"].ToString();
                    MEndTime = GetServerTime().ToString();
                }
                else
                {
                    if (detailAccountFacade == null)
                    {
                        detailAccountFacade = new DetailAccountFacade();
                    }
                    MBeginTime = Convert.ToDateTime(detailAccountFacade.GetFirstChargeTime()).AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss");
                    MEndTime = GetServerTime().ToString();
                }
            }
        }

        /// <summary>
        /// 获取服务器时间  
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            CommonFacade commonfacade = new CommonFacade();
            return commonfacade.GetServerDateTime();
        }

        public void GetSquareAccount()
        {
            GetBeginTime();
            if (MBeginTime == "" || MEndTime == "")
            {
                SkynetMessage.MsgInfo("请输入正确的时间！");
                return;
            }
            if (Convert.ToDateTime(MBeginTime) == Convert.ToDateTime(MEndTime))
            {
                SkynetMessage.MsgInfo("起始时间和终止时间一样！！");
                return;
            }
            if (Convert.ToDateTime(MBeginTime) > Convert.ToDateTime(MEndTime))
            {
                SkynetMessage.MsgInfo("起始时间不能大于终止时间！！");
                return;
            }
            DateTime ServerTime = new CommonFacade().GetServerDateTime();
            if (Convert.ToDateTime(MEndTime) > ServerTime)
            {
                if (SkynetMessage.MsgInfo("终止时间大于系统当前时间，是否继续?", false) == false)
                {
                    return;
                }
            }
            try
            {
                if (iAccountMark == 0)
                {
                    //查询报账单中间表是否有数据
                   
                    DataSet dsGatheringMaster = gatheringMasterFacade.FindByOperatorIDAndTime(Convert.ToDateTime(MBeginTime), SysOperatorInfo.OperatorID);
                    if (dsGatheringMaster.Tables.Count > 0)
                    {
                        if (dsGatheringMaster.Tables[0].Rows.Count <= 0)//如果不存在就重新插入
                        {
                            if (SkynetMessage.MsgInfo("是否确定交款!", true) == true)
                            {
                                isBtnOk = true;
                                this.Print(SysOperatorInfo.OperatorID, Convert.ToDateTime(MBeginTime), Convert.ToDateTime(MEndTime), false);
                                isBtnOk = false;
                            }
                        }
                        else
                        {
//                            QueryHistoryData(SysOperatorInfo.OperatorID, Convert.ToDateTime(txtBeginTime.Text), Convert.ToDateTime(txtEndTime.Text));
                        }
                    }
                }
                else
                {
                    //查询报账单中间表是否有数据
                    DataSet dsGatheringMaster = gatheringMasterFacade.FindByOperatorIDAndTime(Convert.ToDateTime(MBeginTime), SysOperatorInfo.OperatorID);
                    if (dsGatheringMaster.Tables.Count > 0)
                    {
                        if (dsGatheringMaster.Tables[0].Rows.Count <= 0)//如果不存在就重新插入
                        {
                            isBtnOk = true;
                            this.Print(SysOperatorInfo.OperatorID, Convert.ToDateTime(MBeginTime), Convert.ToDateTime(MEndTime), false);
                            isBtnOk = false;
                        }
                        else
                        {
                            QueryHistoryData(SysOperatorInfo.OperatorID, Convert.ToDateTime(MBeginTime), Convert.ToDateTime(MEndTime));
                        }
                    }
                }
                SkyComm.CloseCash = true;

            }
            catch (Exception err)
            {
                SkynetMessage.MsgInfo(err.Message);
            }
        }



        private void Print(string strOperator, DateTime startTime, DateTime endTime, bool Preview)
        {
            
            string useType = SystemInfo.SystemConfigs["统计报表调用模式"].DefaultValue.Trim();
            iIsStatisticRegister = Convert.ToInt32(SystemInfo.SystemConfigs["门诊交款报表是否统计挂号费用"].DefaultValue.Trim());
            DataSet ds, dsTemp;
            try
            {
                //useType = "1";
                if (useType == "0")
                {
                    Hashtable inParamList = new Hashtable(); //输入参数名及参数值
                    Hashtable outParamList = new Hashtable(); //输出参数名及参数类型
                    ArrayList keys = new ArrayList(); //输入参数名

                    Hashtable inParamList1 = new Hashtable(); //输入参数名及参数值
                    Hashtable outParamList1 = new Hashtable(); //输出参数名及参数类型
                    ArrayList keys1 = new ArrayList(); //输入参数名

                    //0=统计挂号金额；1=不统计挂号金额
                    if (iIsStatisticRegister == 0)
                    {
                        //添加输入参数名和参数值
                        inParamList.Add("OperatorID", strOperator);
                        inParamList.Add("StartTime", startTime);
                        inParamList.Add("EndTime", endTime);
                        inParamList.Add("RegisterMark", 0);
                        inParamList.Add("DetailMark", 1);

                        inParamList1.Add("OperatorID", strOperator);
                        inParamList1.Add("StartTime", startTime);
                        inParamList1.Add("EndTime", endTime);
                        inParamList1.Add("RegisterMark", 0);

                        //添加输入参数名
                        keys.Add("OperatorID");
                        keys.Add("StartTime");
                        keys.Add("EndTime");
                        keys.Add("RegisterMark");
                        keys.Add("DetailMark");

                        keys1.Add("OperatorID");
                        keys1.Add("StartTime");
                        keys1.Add("EndTime");
                        keys1.Add("RegisterMark");
                    }
                    else
                    {
                        //添加输入参数名和参数值
                        inParamList.Add("OperatorID", strOperator);
                        inParamList.Add("StartTime", startTime);
                        inParamList.Add("EndTime", endTime);
                        inParamList.Add("RegisterMark", 1);
                        inParamList.Add("DetailMark", 1);

                        inParamList1.Add("OperatorID", strOperator);
                        inParamList1.Add("StartTime", startTime);
                        inParamList1.Add("EndTime", endTime);
                        inParamList1.Add("RegisterMark", 1);

                        //添加输入参数名
                        keys.Add("OperatorID");
                        keys.Add("StartTime");
                        keys.Add("EndTime");
                        keys.Add("RegisterMark");
                        keys.Add("DetailMark");

                        keys1.Add("OperatorID");
                        keys1.Add("StartTime");
                        keys1.Add("EndTime");
                        keys1.Add("RegisterMark");
                    }

                    dialog = new Skynet.Framework.Common.WaitDialogForm("调用存储过程...");

                    ReportSolutionFacade reportSolutionFacade = new ReportSolutionFacade();
                    DrawInvoiceFacade drawInvoiceFacade = new DrawInvoiceFacade();

                    
                    //调用存储过程方法
                    //ExecStoreProc(存储过程名,参数列表,输入参数名及值的哈希表,输出参数名及类型哈希表);
                    ds = reportSolutionFacade.ExecStoredProc("PH_FinancingReport", keys, inParamList, outParamList);
                    dsTemp = reportSolutionFacade.ExecStoredProc("PH_FinancingReport_invoice", keys1, inParamList1, outParamList1);
                   
                }
                else
                {
                    dialog = new Skynet.Framework.Common.WaitDialogForm("正在组织数据...");

                    FinancingReportFacade financingReportFacade = new FinancingReportFacade();
                    FinancingReportInvoiceFacade financingReportInvoiceFacade = new FinancingReportInvoiceFacade();
                    ds = financingReportFacade.FinancingReport(strOperator, startTime, endTime, iIsStatisticRegister == 0 ? 0 : 1);
                    dsTemp = financingReportInvoiceFacade.FinancingReport(strOperator, startTime, endTime, iIsStatisticRegister == 0 ? 0 : 1);
                }
            }
            catch (LogonException err)
            {
                dialog.Close();
                ExceptionManager.Publish(err);
                SkynetMessage.MsgError("数据生成失败！原因：" + err.Message);
                return;
            }
            catch (Exception er)
            {
                dialog.Close();
                SkynetMessage.MsgError("数据生成失败！" + er.Message);
                return;
            }
            if (ds.Tables.Count == 0)
            {

                dialog.Close();
                SkynetMessage.MsgError("不是有效的数据集！");
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                dialog.Close();
                SkynetMessage.MsgInfo("所选条件中没有数据！");
                return;
            }

            if (isBtnOk == true)
            {
                DataSet dsGatheringMaster = gatheringMasterFacade.FindByOperatorIDAndTime(startTime, strOperator);
                if (dsGatheringMaster.Tables.Count > 0)
                {
                    if (dsGatheringMaster.Tables[0].Rows.Count <= 0)//如果已经存在就不插入
                    {
                        string bgndate = startTime.ToString("yyyy-MM-dd") + " " + SystemInfo.SystemConfigs["门诊结帐时间"].DefaultValue;
                        string enddate = endTime.ToString("yyyy-MM-dd") + " " + SystemInfo.SystemConfigs["门诊结帐时间"].DefaultValue;
                        gatheringMasterFacade.insertGathering(Convert.ToDateTime(bgndate), Convert.ToDateTime(enddate), ds, dsTemp); // T_RECKONACCOUNTS_TIME表插记录
                        //OrganizeData(ds, dsTemp, startTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        try
                        {
                            //T_RECKONACCOUNTS表插记录
                            ReckonAccountsData dsinfo = new ReckonAccountsData();
                            DataRow dr = dsinfo.Tables[0].NewRow();
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_SETTLEID] = GetMaxSerialNo("T_ReckonAccounts");
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_OPERATORID] = strOperator;
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_SETTLEDATE] = DateTime.Now.ToString("yyyy-MM-dd") + " " + SystemInfo.SystemConfigs["门诊结帐时间"].DefaultValue;//门诊结帐时间 
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_OPERATEDATE] = GetServerTime().ToString();
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_SETTLEOPERATORID] = strOperator;
                            dr[ReckonAccountsData.T_RECKONACCOUNTS_SETTLETYPE] = "门诊";
                            dsinfo.Tables[0].Rows.Add(dr);
                            dsinfo.Tables[0].TableName = "T_RECKONACCOUNTS";
                            new ReckonAccountsFacade().insertEntity(dsinfo);
                        }
                        catch (Exception ex)
                        {
                            SkynetMessage.MsgInfo("保存数据失败:" + ex.Message);
                            return;
                        }

                        MessageBox.Show("日结帐成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            DataColumn myDataColumn;
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "退票号";
            ds.Tables[0].Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "废票号";
            ds.Tables[0].Columns.Add(myDataColumn);

            //mitao 20140729 增加退费废票号，重打废票号，作废废票号 15821
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "退费废票号";
            ds.Tables[0].Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "重打废票号";
            ds.Tables[0].Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "作废废票号";
            ds.Tables[0].Columns.Add(myDataColumn);

            DataRow drFinancing = ds.Tables[0].Rows[0];
            foreach (DataRow drInvoice in dsTemp.Tables[0].Select("类型 = '退票'"))
            {
                if (drFinancing["退票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0)
                {
                    drFinancing.BeginEdit();
                    drFinancing["退票号"] = drFinancing["退票号"].ToString() + drInvoice["原始发票号"].ToString() + ",";
                    drFinancing.EndEdit();
                }
            }


            foreach (DataRow drInvoice in dsTemp.Tables[0].Select("类型 = '废票' or 类型 = '作废'"))
            {
                if (drFinancing["废票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0)
                {
                    drFinancing.BeginEdit();
                    drFinancing["废票号"] = drFinancing["废票号"].ToString() + drInvoice["原始发票号"].ToString() + ",";
                    drFinancing.EndEdit();
                }

                //mitao 20140729 增加退费废票号，重打废票号，作废废票号 15821
                if (drInvoice["类型"].ToString() == "废票")
                {
                    if (string.IsNullOrEmpty(drInvoice["当前发票号"].ToString()))
                    {
                        //因为目前门诊发票使用表中不能正常区分是门诊使用发票还是挂号使用发票，所以在挂号发票重打多次以后再统计时存储过程返回的值可能会重复
                        //如果有多条时这里只能统计在废票里面不能统计到退费废票里面
                        if (drFinancing["退费废票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0 && drFinancing["重打废票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0)
                        {
                            DataRow[] arrRow = dsTemp.Tables[0].Select("类型 = '废票' AND 当前发票号<>''  AND 原始发票号 = '" + drInvoice["原始发票号"].ToString() + "'");
                            if (arrRow.Length == 0)
                            {
                                drFinancing.BeginEdit();
                                drFinancing["退费废票号"] = drFinancing["退费废票号"].ToString() + drInvoice["原始发票号"].ToString() + ",";
                                drFinancing.EndEdit();
                            }
                        }
                    }
                    else
                    {
                        if (drFinancing["重打废票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0)
                        {
                            drFinancing.BeginEdit();
                            drFinancing["重打废票号"] = drFinancing["重打废票号"].ToString() + drInvoice["原始发票号"].ToString() + ",";
                            drFinancing.EndEdit();
                        }
                    }
                }

                if (drInvoice["类型"].ToString() == "作废")
                {
                    if (drFinancing["作废废票号"].ToString().IndexOf(drInvoice["原始发票号"].ToString()) < 0)
                    {
                        drFinancing.BeginEdit();
                        drFinancing["作废废票号"] = drFinancing["作废废票号"].ToString() + drInvoice["原始发票号"].ToString() + ",";
                        drFinancing.EndEdit();
                    }
                }
            }

            ds.Tables[0].TableName = "DetailAccount";
            DataTable dt = new DataTable();
            if (dsTemp.Tables.Count != 0)
            {
                dt = dsTemp.Tables[0].Copy();
            }
            dt.TableName = "RetrunAndInvalidated";
            ds.Tables.Add(dt);

//            PrintReport(ds, startTime, endTime, Preview);
            dialog.Close();
            //PrintClinicAdvanceReport(strOperator, startTime, endTime, 1, Preview);
           

        }


        private void QueryHistoryData(string operatorID, DateTime startTime, DateTime endTime)
        {
            gatheringMasterFacade = new GatheringMasterFacade();
            DataSet ds = gatheringMasterFacade.FindByOperatorIDAndTime(startTime, endTime, operatorID);
            if (ds.Tables.Count <= 0)
            {
                SkynetMessage.MsgInfo("没有找到数据!");
                return;
            }
            else
            {
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    SkynetMessage.MsgInfo("没有找到数据!");
                    return;
                }
            }

            DataRow dr = ds.Tables[0].Rows[0];

            DataTable dtFinancing = BuildDataTable();
            DataRow drFinancingNew = dtFinancing.NewRow();
            drFinancingNew["操作员编号"] = dr["OPERATORID"];
            drFinancingNew["操作员姓名"] = dr["OPERATORNAME"];
            drFinancingNew["操作员代码"] = dr["CODENO"];
            drFinancingNew["应收合计"] = dr["PAYMENTMONEY"];
            drFinancingNew["实收合计"] = dr["INFACTMONEY"];
            drFinancingNew["挂账合计"] = dr["HANGMONEY"];
            drFinancingNew["发票数"] = dr["INVOICECOUNT"];
            drFinancingNew["起始发票号"] = dr["STARTINVOICE"];
            drFinancingNew["截止发票号"] = dr["ENDINVOICE"];
            drFinancingNew["发票号段"] = dr["INVOICESEGMENT"];
            drFinancingNew["结帐人"] = dr["OPERATORID"];
            drFinancingNew["退票数量"] = dr["RETURNCOUNT"];
            drFinancingNew["退票应收"] = dr["RETURNPAYMENT"];
            drFinancingNew["退票实收"] = dr["RETURNMONEY"];
            drFinancingNew["废票数量"] = dr["NULLITYCOUNT"];
            drFinancingNew["废票应收"] = dr["NULLITYPAYMENT"];
            drFinancingNew["废票实收"] = dr["NULLITYMONEY"];
            drFinancingNew["报损数量"] = dr["REJECTCOUNT"];
            drFinancingNew["重打数量"] = dr["REPRINTCOUNT"];
            drFinancingNew["POS应收金额"] = dr["POSPAYMENT"];
            drFinancingNew["POS实收金额"] = dr["POSMONEY"];
            drFinancingNew["POS挂账金额"] = dr["POSHANG"];
            drFinancingNew["附加费应收金额"] = dr["SUBJOINPAYMENT"];
            drFinancingNew["附加费实收金额"] = dr["SUBJOINMONEY"];
            drFinancingNew["附加费挂账金额"] = dr["SUBJOINHANG"];
            drFinancingNew["挂号金额"] = dr["REGISTERMONEY"];
            drFinancingNew["诊金"] = dr["DIAGNOSISMONEY"];
            drFinancingNew["工本费"] = dr["STUFFMONEY"];
            drFinancingNew["统筹挂账"] = dr["PLANMONEY"];
            drFinancingNew["优惠打折"] = dr["REBATEMONEY"];
            drFinancingNew["卡支付额"] = dr["CARDPAY"];
            drFinancingNew["不打票应收金额"] = dr["NOPRINTMONEY"];
            drFinancingNew["不打票实收金额"] = dr["NOPRINTSELFMONEY"];
            drFinancingNew["慢性病统筹"] = dr["CHRONICPLAN"];
            drFinancingNew["挂号预约费"] = dr["BESPEAKMONEY"];

            //李新华 内附院修改 0709
            drFinancingNew["收费起始号"] = dr["CHARGEBEGININVOICE"];
            drFinancingNew["收费终止号"] = dr["CHARGEENDINVOICE"];
            drFinancingNew["挂号起始号"] = dr["REGBEGININVOICE"];
            drFinancingNew["挂号终止号"] = dr["REGENDINVOICE"];
            drFinancingNew["收费发票数"] = dr["CHARGEINVOICESUM"];
            drFinancingNew["挂号发票数"] = dr["REGINVOICESUM"];

            gatheringDetailFacade = new GatheringDetailFacade();
            gatheringDetailData = new GatheringDetailData();
            gatheringDetailData = (GatheringDetailData)gatheringDetailFacade.FindByOperatorID(dr["GATHERINGID"].ToString(), SysOperatorInfo.OperatorID);
            foreach (DataRow drGatheringDetail in gatheringDetailData.Tables[0].Rows)
            {
                if (dtFinancing.Columns.IndexOf(drGatheringDetail["ITEM"].ToString() + "应收") >= 0)
                {
                    drFinancingNew[drGatheringDetail["ITEM"].ToString() + "应收"] = drGatheringDetail["PAYMENTMONEY"];
                    drFinancingNew[drGatheringDetail["ITEM"].ToString() + "实收"] = drGatheringDetail["INFACTMONEY"];
                    drFinancingNew[drGatheringDetail["ITEM"].ToString() + "挂账"] = drGatheringDetail["HANGMONEY"];
                    drFinancingNew[drGatheringDetail["ITEM"].ToString() + "应收退费"] = drGatheringDetail["PAYMENTRETURN"];
                    drFinancingNew[drGatheringDetail["ITEM"].ToString() + "实收退费"] = drGatheringDetail["INFACTRETURN"];
                }
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '发票补打数量'").Length > 0)
            {
                drFinancingNew["发票补打数量"] = gatheringDetailData.Tables[0].Select("ITEM = '发票补打数量'")[0]["INFACTMONEY"];
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '预交金收款金额'").Length > 0)
            {
                drFinancingNew["预交金收款金额"] = gatheringDetailData.Tables[0].Select("ITEM = '预交金收款金额'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["预交金收款金额"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '预交金退款金额'").Length > 0)
            {
                drFinancingNew["预交金退款金额"] = gatheringDetailData.Tables[0].Select("ITEM = '预交金退款金额'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["预交金退款金额"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '实收现金'").Length > 0)
            {
                drFinancingNew["实收现金"] = gatheringDetailData.Tables[0].Select("ITEM = '实收现金'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["实收现金"] = 0;
            }

          

            BalanceModeFacade bm = new BalanceModeFacade();
            DataSet bds = bm.FindContrastAll();
            foreach (DataRow bdr in bds.Tables[0].Rows)
            {

                string aa = bdr["BALANCEMODE"].ToString().Trim();
                string str1 = string.Empty;
                if (aa.Substring(aa.Length - 2, 2).Equals("支付"))
                {
                    str1 = aa;
                }
                else
                {
                    str1 = aa + "支付";
                }
                if (gatheringDetailData.Tables[0].Select("ITEM = '" + str1 + "'").Length > 0)
                {
                    drFinancingNew["" + bdr["BALANCEMODE"] + ""] = gatheringDetailData.Tables[0].Select("ITEM = '" + str1 + "'")[0]["INFACTMONEY"];
                }
                else
                {
                    drFinancingNew["" + bdr["BALANCEMODE"] + ""] = 0;
                }
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '已结算退费金额'").Length > 0)
            {
                drFinancingNew["已结算退费金额"] = gatheringDetailData.Tables[0].Select("ITEM = '已结算退费金额'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["已结算退费金额"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '应收退费合计'").Length > 0)
            {
                drFinancingNew["应收退费合计"] = gatheringDetailData.Tables[0].Select("ITEM = '应收退费合计'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["应收退费合计"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '实收退费合计'").Length > 0)
            {
                drFinancingNew["实收退费合计"] = gatheringDetailData.Tables[0].Select("ITEM = '实收退费合计'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["实收退费合计"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号检查费'").Length > 0)
            {
                drFinancingNew["挂号检查费"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号检查费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号检查费"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号金额退费'").Length > 0)
            {
                drFinancingNew["挂号金额退费"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号金额退费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号金额退费"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '诊金退费'").Length > 0)
            {
                drFinancingNew["诊金退费"] = gatheringDetailData.Tables[0].Select("ITEM = '诊金退费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["诊金退费"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '工本费退费'").Length > 0)
            {
                drFinancingNew["工本费退费"] = gatheringDetailData.Tables[0].Select("ITEM = '工本费退费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["工本费退费"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号预约费退费'").Length > 0)
            {
                drFinancingNew["挂号预约费退费"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号预约费退费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号预约费退费"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号检查费退费'").Length > 0)
            {
                drFinancingNew["挂号检查费退费"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号检查费退费'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号检查费退费"] = 0;
            }
            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号统筹金额'").Length > 0)
            {
                drFinancingNew["挂号统筹金额"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号统筹金额'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号统筹金额"] = 0;
            }
            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号账户金额'").Length > 0)
            {
                drFinancingNew["挂号账户金额"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号账户金额'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号账户金额"] = 0;
            }
            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号退统筹'").Length > 0)
            {
                drFinancingNew["挂号退统筹"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号退统筹'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号退统筹"] = 0;
            }
            if (gatheringDetailData.Tables[0].Select("ITEM = '挂号退账户'").Length > 0)
            {
                drFinancingNew["挂号退账户"] = gatheringDetailData.Tables[0].Select("ITEM = '挂号退账户'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["挂号退账户"] = 0;
            }

            if (gatheringDetailData.Tables[0].Select("ITEM = '门诊转住院预交金'").Length > 0)
            {
                drFinancingNew["门诊转住院预交金"] = gatheringDetailData.Tables[0].Select("ITEM = '门诊转住院预交金'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["门诊转住院预交金"] = 0;
            }
            if (gatheringDetailData.Tables[0].Select("ITEM = '住院转门诊预交金'").Length > 0)
            {
                drFinancingNew["住院转门诊预交金"] = gatheringDetailData.Tables[0].Select("ITEM = '住院转门诊预交金'")[0]["INFACTMONEY"];
            }
            else
            {
                drFinancingNew["住院转门诊预交金"] = 0;
            }

            ChargeKindFacade chargeKindFacade = new ChargeKindFacade();
            DataSet chargeKind = chargeKindFacade.FindAll();
            foreach (DataRow drChargeKind in chargeKind.Tables[0].Select("(ISNOTMEDINSURANCE = '是' or ISNOTMEDINSURANCE = '不打票'OR ISNOTMEDINSURANCE = '院内医保') and USECONFINE <> '住院'"))
            {
               
                BalanceModeFacade bm1 = new BalanceModeFacade();
                DataSet bds1 = bm1.FindContrastAllout();
                foreach (DataRow bdr in bds1.Tables[0].Rows)
                {

                    if (gatheringDetailData.Tables[0].Select("ITEM = '" + drChargeKind["CHARGEKIND"].ToString().Trim() + "" + bdr["BALANCEMODE"] + "'").Length > 0)
                    {
                        drFinancingNew[drChargeKind["CHARGEKIND"].ToString().Trim() + "" + bdr["BALANCEMODE"] + ""] = gatheringDetailData.Tables[0].Select("ITEM = '" + drChargeKind["CHARGEKIND"].ToString().Trim() + "" + bdr["BALANCEMODE"] + "'")[0]["INFACTMONEY"];
                    }
                    else
                    {
                        drFinancingNew[drChargeKind["CHARGEKIND"].ToString().Trim() + "" + bdr["BALANCEMODE"] + ""] = 0;
                    }
                }
            }
           

            dtFinancing.Rows.Add(drFinancingNew);



            gatheringInvoiceUseDetailFacade = new GatheringInvoiceUseDetailFacade();
            DataSet dsInvoice = gatheringInvoiceUseDetailFacade.FindInvoiceByOperatorID(dr["GATHERINGID"].ToString(), SysOperatorInfo.OperatorID);

            foreach (DataRow drInvoice in dsInvoice.Tables[0].Select("OPERATETYPE = '退票'"))
            {
                if (drFinancingNew["退票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0)
                {
                    drFinancingNew["退票号"] = drFinancingNew["退票号"].ToString() + drInvoice["QUONDAMINVOICE"].ToString() + ",";
                }
            }

            foreach (DataRow drInvoice in dsInvoice.Tables[0].Select("OPERATETYPE = '废票' or OPERATETYPE = '作废'"))
            {
                if (drFinancingNew["废票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0)
                {
                    drFinancingNew.BeginEdit();
                    drFinancingNew["废票号"] = drFinancingNew["废票号"].ToString() + drInvoice["QUONDAMINVOICE"].ToString() + ",";
                    drFinancingNew.EndEdit();
                }

                //mitao 20140729 增加退费废票号，重打废票号，作废废票号 15821
                if (drInvoice["OPERATETYPE"].ToString() == "废票")
                {
                    if (string.IsNullOrEmpty(drInvoice["CURRENTINVOICE"].ToString()))
                    {
                        //因为目前门诊发票使用表中不能正常区分是门诊使用发票还是挂号使用发票，所以在挂号发票重打多次以后再统计时存储过程返回的值可能会重复
                        //如果有多条时这里只能统计在废票里面不能统计到退费废票里面

                        if (drFinancingNew["退费废票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0 && drFinancingNew["重打废票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0)
                        {
                            DataRow[] arrRow = dsInvoice.Tables[0].Select("OPERATETYPE = '废票' AND CURRENTINVOICE<>'' AND QUONDAMINVOICE = '" + drInvoice["QUONDAMINVOICE"].ToString() + "'");
                            if (arrRow.Length == 0)
                            {
                                drFinancingNew.BeginEdit();
                                drFinancingNew["退费废票号"] = drFinancingNew["退费废票号"].ToString() + drInvoice["QUONDAMINVOICE"].ToString() + ",";
                                drFinancingNew.EndEdit();
                            }
                        }
                    }
                    else
                    {
                        if (drFinancingNew["重打废票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0)
                        {
                            drFinancingNew.BeginEdit();
                            drFinancingNew["重打废票号"] = drFinancingNew["重打废票号"].ToString() + drInvoice["QUONDAMINVOICE"].ToString() + ",";
                            drFinancingNew.EndEdit();
                        }
                    }
                }

                if (drInvoice["OPERATETYPE"].ToString() == "作废")
                {
                    if (drFinancingNew["作废废票号"].ToString().IndexOf(drInvoice["QUONDAMINVOICE"].ToString()) < 0)
                    {
                        drFinancingNew.BeginEdit();
                        drFinancingNew["作废废票号"] = drFinancingNew["作废废票号"].ToString() + drInvoice["QUONDAMINVOICE"].ToString() + ",";
                        drFinancingNew.EndEdit();
                    }
                }
            }

            DataSet invoiceUseDetailData = new DataSet();
            invoiceUseDetailData = gatheringInvoiceUseDetailFacade.FindByOperatorID(dr["GATHERINGID"].ToString(), SysOperatorInfo.OperatorID);
            DataTable dtGatheringInvoiceUseDetail = new DataTable();
            if (invoiceUseDetailData.Tables.Count > 0)
            {
                dtGatheringInvoiceUseDetail = invoiceUseDetailData.Tables[0].Copy();
            }

            dtGatheringInvoiceUseDetail.TableName = "RetrunAndInvalidated";

            DataSet dsFinancing = new DataSet();
            dsFinancing.Tables.Add(dtFinancing);
            dsFinancing.Tables.Add(dtGatheringInvoiceUseDetail);

            dialog = new Skynet.Framework.Common.WaitDialogForm();
//            PrintReport(dsFinancing, startTime, endTime, false);
            PrintClinicAdvanceReport(operatorID, startTime, endTime, 1, false);
        }


        private DataTable BuildDataTable()
        {
            DataTable myDataTable = new DataTable("DetailAccount");
            DataColumn myDataColumn;
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "操作员编号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "操作员姓名";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "操作员代码";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "应收合计";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "实收合计";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂账合计";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "发票数";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "起始发票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "截止发票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "发票号段";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "退票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "废票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "结帐人";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "退票数量";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "退票应收";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "退票实收";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "废票数量";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "废票应收";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "废票实收";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "报损数量";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "重打数量";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "POS应收金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "POS实收金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "POS挂账金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "附加费应收金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "附加费实收金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "附加费挂账金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号金额";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "诊金";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "工本费";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "统筹挂账";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "优惠打折";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "卡支付额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "不打票应收金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "不打票实收金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "慢性病统筹";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号预约费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "发票补打数量";
            myDataTable.Columns.Add(myDataColumn);

            //李新华 内附院修改 0709
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "收费起始号";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "收费终止号";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "挂号起始号";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "挂号终止号";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "收费发票数";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Int32");
            myDataColumn.ColumnName = "挂号发票数";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            //mitao 20140729 增加退费废票号，重打废票号，作废废票号 15821
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "退费废票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "重打废票号";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "作废废票号";
            myDataTable.Columns.Add(myDataColumn);

            SummaryInfoFacade summaryInfoFacade = new SummaryInfoFacade();
            DataSet dsSummaryInfo = summaryInfoFacade.FindReportItem();
            foreach (DataRow drSummaryInfo in dsSummaryInfo.Tables[0].Rows)
            {
                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drSummaryInfo["REPORTITEM"].ToString().Trim() + "应收";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drSummaryInfo["REPORTITEM"].ToString().Trim() + "应收退费";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drSummaryInfo["REPORTITEM"].ToString().Trim() + "实收";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drSummaryInfo["REPORTITEM"].ToString().Trim() + "实收退费";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drSummaryInfo["REPORTITEM"].ToString().Trim() + "挂账";
                myDataTable.Columns.Add(myDataColumn);
            }

            BalanceModeFacade balanceModeFacade = new BalanceModeFacade();
            DataSet dsBalanceMode = balanceModeFacade.FindAll();
            foreach (DataRow drBalanceMode in dsBalanceMode.Tables[0].Rows)
            {
                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drBalanceMode["BALANCEMODE"].ToString().Trim() + "应收";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drBalanceMode["BALANCEMODE"].ToString().Trim() + "应收退费";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drBalanceMode["BALANCEMODE"].ToString().Trim() + "实收";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drBalanceMode["BALANCEMODE"].ToString().Trim() + "实收退费";
                myDataTable.Columns.Add(myDataColumn);

                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = drBalanceMode["BALANCEMODE"].ToString().Trim() + "挂账";
                myDataTable.Columns.Add(myDataColumn);
            }


            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "预交金收款金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "预交金退款金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "实收现金";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "现金";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "银联卡";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "预交金";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "支票";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "统筹支付";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //myDataColumn.ColumnName = "账户支付";
            //myDataColumn.DefaultValue = 0;
            //myDataTable.Columns.Add(myDataColumn);
            BalanceModeFacade bm = new BalanceModeFacade();
            DataSet bds = bm.FindContrastAll();
            foreach (DataRow bdr in bds.Tables[0].Rows)
            {
                myDataColumn = new DataColumn();
                myDataColumn.DataType = System.Type.GetType("System.Decimal");
                myDataColumn.ColumnName = "" + bdr["BALANCEMODE"] + "";
                myDataColumn.DefaultValue = 0;
                myDataTable.Columns.Add(myDataColumn);
            }

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "已结算退费金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "应收退费合计";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "实收退费合计";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号检查费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);


            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号金额退费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "诊金退费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "工本费退费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号预约费退费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号检查费退费";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号统筹金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号账户金额";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号退统筹";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "挂号退账户";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            ChargeKindFacade chargeKindFacade = new ChargeKindFacade();
            DataSet chargeKind = chargeKindFacade.FindAll();
            foreach (DataRow dr in chargeKind.Tables[0].Select("(ISNOTMEDINSURANCE = '是' or ISNOTMEDINSURANCE = '不打票'OR ISNOTMEDINSURANCE = '院内医保') and USECONFINE <> '住院'"))
            {
                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "统筹支付";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);

                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "账户支付";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);

                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "现金";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);

                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "银联卡";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);

                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "预交金";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);

                //myDataColumn = new DataColumn();
                //myDataColumn.DataType = System.Type.GetType("System.Decimal");
                //myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "支票";
                //myDataColumn.DefaultValue = 0;
                //myDataTable.Columns.Add(myDataColumn);
                BalanceModeFacade bm1 = new BalanceModeFacade();
                DataSet bds1 = bm.FindContrastAllout();
                foreach (DataRow bdr in bds1.Tables[0].Rows)
                {
                    myDataColumn = new DataColumn();
                    myDataColumn.DataType = System.Type.GetType("System.Decimal");
                    myDataColumn.ColumnName = dr["CHARGEKIND"].ToString().Trim() + "" + bdr["BALANCEMODE"] + "";
                    myDataColumn.DefaultValue = 0;
                    myDataTable.Columns.Add(myDataColumn);
                }
            }

            //CardTypesFacade Cardtypesfac = new CardTypesFacade();
            //DataSet dsCardTypes = Cardtypesfac.FindAllBankItems();

            //foreach (DataRow dr in dsCardTypes.Tables[0].Rows)
            //{
            //    myDataColumn = new DataColumn();
            //    myDataColumn.DataType = System.Type.GetType("System.Decimal");
            //    myDataColumn.ColumnName = dr["TYPE_NAME"].ToString().Trim();
            //    myDataColumn.DefaultValue = 0;
            //    myDataTable.Columns.Add(myDataColumn);
            //}

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "门诊转住院预交金";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.Decimal");
            myDataColumn.ColumnName = "住院转门诊预交金";
            myDataColumn.DefaultValue = 0;
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        /// <summary>
		/// 打印门诊报账单
		/// </summary>
		/// <param name="dsDetail">收费明细</param>
		/// <param name="dsReturn">退票，废票明细</param>
		private void PrintReport(DataSet ds, DateTime startTime, DateTime endTime, bool Preview)
        {
            ds.WriteXml(Application.StartupPath + @"\\ReportXml\\门诊报账单.xml");
            //引用 RM.ReportEngine.dll
            RMReportEngine.RMReport rmReport1 = new RMReportEngine.RMReport();

            if (ds.Tables["RetrunAndInvalidated"].Rows.Count != 0)
            {
                string path = Application.StartupPath + @"\\Reports\\门诊报账单.rmf";

                if (System.IO.File.Exists(path) == false)
                {
                    dialog.Close();
                    SkynetMessage.MsgError("门诊报账单票据不存在,请查证!");
                    rmReport1.Destroy();
                    rmReport1.Dispose();
                    return;
                }

                string result = string.Empty;
                DataRelation myRela1;

                myRela1 = new DataRelation("Rela1", ds.Tables["DetailAccount"].Columns["操作员编号"], ds.Tables["RetrunAndInvalidated"].Columns["操作员编号"]);
                ds.Relations.Add(myRela1);


                rmReport1.Init(form, RMReportEngine.RMReportType.rmrtReport);
                rmReport1.AddDataSet(ds.Tables["DetailAccount"], "report");
                rmReport1.AddDetailDataSet(ds.Tables["RetrunAndInvalidated"], "report1", "report", myRela1);

                dialog.Caption = "准备参数...";

                rmReport1.AddVariable("qssj", startTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                rmReport1.AddVariable("zzsj", endTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                rmReport1.AddVariable("医院名称", SysOperatorInfo.CustomerName, true);
                if (Preview == true)
                {
                    rmReport1.AddVariable("预览标识", "预览", true);
                }
                else
                {
                    rmReport1.AddVariable("预览标识", "", true);
                }

                //查询医保相关金额信息
                I_BALANCE_OTHERMONEYINFOFacade balanceothermoneyFacade = new I_BALANCE_OTHERMONEYINFOFacade();
                DataSet medMoney = balanceothermoneyFacade.FindItemMoney(SysOperatorInfo.OperatorID, startTime, endTime, 1, 0, "2");
                if (medMoney.Tables.Count > 0)
                {
                    foreach (DataRow drMedMoney in medMoney.Tables[0].Rows)
                    {
                        rmReport1.AddVariable(drMedMoney["ITEMNAME"].ToString(), drMedMoney["ITEMVALUE"].ToString().Trim(), true);
                    }
                }

                medMoney = balanceothermoneyFacade.FindClinicItemMoneyByAlone(SysOperatorInfo.OperatorID, startTime, endTime, 1, 0, "2");
                if (medMoney.Tables.Count > 0)
                {
                    foreach (DataRow drMedMoney in medMoney.Tables[0].Rows)
                    {
                        rmReport1.AddVariable(drMedMoney["ITEMNAME"].ToString(), drMedMoney["ITEMVALUE"].ToString().Trim(), true);
                    }
                }

                dialog.Caption = "准备报表...";

                rmReport1.LoadFromFile(path);

                dialog.Close();

                if (Preview == true)
                {
                    rmReport1.ShowReport();
                    rmReport1.Destroy();
                }
                else
                {
                    rmReport1.ShowPrintDialog = false;
                    rmReport1.ShowReport();
                    //					rmReport1.PrintReport();
                    rmReport1.Destroy();
                }
                rmReport1.Dispose();
            }
            else
            {
                string path = Application.StartupPath + @"\\Reports\\门诊报账单1.rmf";

                if (System.IO.File.Exists(path) == false)
                {
                    dialog.Close();
                    SkynetMessage.MsgError("门诊报账单1票据不存在,请查证!");
                    rmReport1.Destroy();
                    rmReport1.Dispose();
                    return;
                }

                rmReport1.Init(form, RMReportEngine.RMReportType.rmrtReport);
                rmReport1.AddDataSet(ds.Tables["DetailAccount"], "report");

                dialog.Caption = "准备参数...";

                rmReport1.AddVariable("qssj", startTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                rmReport1.AddVariable("zzsj", endTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                rmReport1.AddVariable("医院名称", SysOperatorInfo.CustomerName, true);
                if (Preview == true)
                {
                    rmReport1.AddVariable("预览标识", "预览", true);
                }
                else
                {
                    rmReport1.AddVariable("预览标识", "", true);
                }

                //查询医保相关金额信息
                I_BALANCE_OTHERMONEYINFOFacade balanceothermoneyFacade = new I_BALANCE_OTHERMONEYINFOFacade();
                DataSet medMoney = balanceothermoneyFacade.FindItemMoney(SysOperatorInfo.OperatorID, startTime, endTime, 1, 0, "2");
                if (medMoney.Tables.Count > 0)
                {
                    foreach (DataRow drMedMoney in medMoney.Tables[0].Rows)
                    {
                        rmReport1.AddVariable(drMedMoney["ITEMNAME"].ToString(), drMedMoney["ITEMVALUE"].ToString().Trim(), true);
                    }
                }

                dialog.Caption = "准备报表...";

                rmReport1.LoadFromFile(path);
                dialog.Close();

                if (Preview == true)
                {
                    rmReport1.ShowReport();
                    rmReport1.Destroy();
                }
                else
                {
                    rmReport1.ShowPrintDialog = false;
                    rmReport1.ShowReport();
                    //					rmReport1.PrintReport();
                    rmReport1.Destroy();
                }
                rmReport1.Dispose();
            }

        }


        /// <summary>
        /// 打印门诊预交金交款报表
        /// </summary>
        /// <param name="strOperatorID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="Preview"></param>
        private void PrintClinicAdvanceReport(string strOperatorID, DateTime startTime, DateTime endTime, int DetailMark, bool Preview)
        {
            dialog.Close();
            string path = Application.StartupPath + @"\\Reports\\门诊预交金交款报表.frx";
            if (System.IO.File.Exists(path) == true)
            {
                CardSavingFacade cardSavingFacade = new CardSavingFacade();
                DataSet ds = cardSavingFacade.ClinicAdvanceReport(startTime, endTime, strOperatorID, DetailMark);

                //引用 RM.ReportEngine.dll
                //                RMReportEngine.RMReport rmReport1 = new RMReportEngine.RMReport();
                //                rmReport1.Init(form, RMReportEngine.RMReportType.rmrtReport);
                PrintManager rmReport1 = new PrintManager();
                rmReport1.InitReport("门诊预交金交款报表");
                #region CASE:16654 douyaming 2014-03-21
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    DataRow[] list = ds.Tables[0].Select(" MODETYPE='退住院预交金' or MODETYPE='转住院预交金'");
                    if (list.Length == 2)
                    {
                        string type1 = list[0]["MODETYPE"] + "";
                        string type2 = list[1]["MODETYPE"] + "";
                        if (type1 == "转住院预交金")
                        {
                            list[0]["收款金额"] = Convert.ToDecimal(list[0]["收款金额"]) + Convert.ToDecimal(list[1]["收款金额"]);
                            list[0]["作废金额"] = Convert.ToDecimal(list[0]["作废金额"]) + Convert.ToDecimal(list[1]["作废金额"]);
                            list[0]["退款金额"] = Convert.ToDecimal(list[0]["退款金额"]) + Convert.ToDecimal(list[1]["退款金额"]);
                            // list[0]["收款数量"] = Convert.ToDecimal(list[0]["收款数量"]) + Convert.ToDecimal(list[1]["收款数量"]);
                            // list[0]["退款数量"] = Convert.ToDecimal(list[0]["退款数量"]) + Convert.ToDecimal(list[1]["退款数量"]);
                            list[0]["收款数量"] = 0;
                            list[0]["退款数量"] = 0;
                            list[0]["作废数量"] = Convert.ToDecimal(list[0]["作废数量"]) + Convert.ToDecimal(list[1]["作废数量"]);
                            list[1].Delete();

                        }
                        else
                        {
                            list[1]["收款金额"] = Convert.ToDecimal(list[1]["收款金额"]) + Convert.ToDecimal(list[0]["收款金额"]);
                            list[1]["作废金额"] = Convert.ToDecimal(list[1]["作废金额"]) + Convert.ToDecimal(list[0]["作废金额"]);
                            list[1]["退款金额"] = Convert.ToDecimal(list[1]["退款金额"]) + Convert.ToDecimal(list[0]["退款金额"]);
                            //list[1]["收款数量"] = Convert.ToDecimal(list[1]["收款数量"]) + Convert.ToDecimal(list[0]["收款数量"]);
                            //list[1]["退款数量"] = Convert.ToDecimal(list[1]["退款数量"]) + Convert.ToDecimal(list[0]["退款数量"]);
                            list[1]["收款数量"] = 0;
                            list[1]["退款数量"] = 0;
                            list[1]["作废数量"] = Convert.ToDecimal(list[1]["作废数量"]) + Convert.ToDecimal(list[0]["作废数量"]);

                            list[0].Delete();
                        }
                    }
                    else if (list.Length == 1)
                    {
                        string type1 = list[0]["MODETYPE"] + "";
                        if (type1 == "退住院预交金")
                        {
                            list[0]["MODETYPE"] = "转住院预交金";
                        }

                        list[0]["收款数量"] = 0;
                        list[0]["退款数量"] = 0;
                    }
                    ds.AcceptChanges();
                }

                #endregion

//                rmReport1.AddDataSet(ds.Tables[0], "report");
                rmReport1.AddData(ds.Tables[0], "report");

                if (Preview == true)
                {
//                    rmReport1.AddVariable("预览标识", "预览", true);
                    rmReport1.AddParam("预览标识", "预览");
                }
                else
                {
//                    rmReport1.AddVariable("预览标识", "", true);
                    rmReport1.AddParam("预览标识", "");
                }
//                rmReport1.AddVariable("StartDate", startTime.ToString(), true);
//                rmReport1.AddVariable("EndDate", endTime.ToString(), true);
//                rmReport1.AddVariable("操作员", SysOperatorInfo.OperatorName, true);
//                rmReport1.AddVariable("医院名称", SysOperatorInfo.CustomerName, true);
//                rmReport1.LoadFromFile(path);
                rmReport1.AddParam("StartDate", startTime.ToString());
                rmReport1.AddParam("EndDate", endTime.ToString());
                rmReport1.AddParam("OPERATORNAME", SysOperatorInfo.OperatorName);
                rmReport1.AddParam("操作员", SysOperatorInfo.OperatorName);
                rmReport1.AddParam("医院名称", SysOperatorInfo.CustomerName);

                if (Preview == true)
                {
//                    rmReport1.ShowReport();
//                    rmReport1.Destroy();
//                    rmReport1.Dispose();
                    rmReport1.PreView();
                    rmReport1.Dispose();
                }
                else
                {
//                    rmReport1.ShowPrintDialog = false;
//                    rmReport1.ShowReport();
//                    //rmReport1.PrintReport();
//                    rmReport1.Destroy();

                    rmReport1.Print();
                    rmReport1.Dispose();
                }
            }
        }

        public DataSet GetPreData(string strOperatorID, DateTime startTime, DateTime endTime, int DetailMark, bool Preview)
        {
            CardSavingFacade cardSavingFacade = new CardSavingFacade();

            DataSet dataSet = cardSavingFacade.ClinicAdvanceReport(startTime, endTime, strOperatorID, DetailMark);
            
            DataTable dataTable = dataSet.Tables[0].Clone();

            var ds = from a in dataSet.Tables[0].AsEnumerable()
                     where a.Field<string>("MODETYPE") == "自助现金"
                     select a;

            foreach (var VARIABLE in ds)
            {
                dataTable.ImportRow(VARIABLE);
            }
            DataSet data = new DataSet();
            data.Tables.Add(dataTable);
            return dataSet;
        }


        public string mGetCardMakeMoney(string operatorID, DateTime startTime, DateTime endTime)
        {
            try
            {
                DataSet dataSet = mSquareAccountsModel.mGetCardMakeMoney(operatorID, startTime, endTime);


                string s = "0";
                if (dataSet.Tables[0].Rows.Count <= 0)
                {

                    s = "0";
                }
                else
                {
                    s = dataSet.Tables[0].Rows[0]["FEES"].ToString();
                }
                return s;
            }
            catch (Exception e)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("发卡工本费异常信息：" + e.Message);
                return "0";
            }


        }

        public int getTotalBankCardTransactions(string operatorID, DateTime startTime, DateTime endTime)
        {


            try
            {
                DataSet dataSet = mSquareAccountsModel.getTotalBankCardTransactions(operatorID, startTime, endTime);

                int mTotal = 0;

                var mAmount = from a in dataSet.Tables[0].AsEnumerable()
                              select a.Field<string>("TRFAMT");
                foreach (var VARIABLE in mAmount)
                {
                    mTotal += Convert.ToInt32(VARIABLE.ToString());
                }

                return mTotal;
            }
            catch (Exception e)
            {

                LogService.GlobalInfoMessage("结帐==银行卡交易中间表数据==" + e.Message);
                return 0;
            }
        }

        public string GetMaxSerialNo(string OPERATIONMARK)
        {
            string maxValue = string.Empty;
            string sql = "UPDATE T_SERIALNO SET MAXVALUE=MAXVALUE+1 WHERE OPERATIONMARK='" + OPERATIONMARK + "'";
            int i = new QuerySolutionFacade().ExecCustomUpdate(sql);
            if (i == 0)
            {
                sql = "INSERT INTO T_SERIALNO(OPERATIONMARK,MAXVALUE)";
                sql += " VALUES('" + OPERATIONMARK + "', 1)";
                new QuerySolutionFacade().ExecCustomUpdate(sql);
            }
            sql = "SELECT MAXVALUE FROM T_SERIALNO WHERE OPERATIONMARK='" + OPERATIONMARK + "'";
            maxValue = new QuerySolutionFacade().ExecCustomQuery(sql).Tables[0].Rows[0]["MAXVALUE"].ToString();
            return maxValue;
        }
    }
}
