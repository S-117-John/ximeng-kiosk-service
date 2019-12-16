using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoServiceManage.Properties;
using BusinessFacade.His.Common;
using BusinessFacade.His.Register;
using Skynet.Framework.Common;
using SystemFramework.SyncLoading;
using BusinessFacade.His.Inpatient;
using O2S.Components.PDFRender4NET;
using System.IO;
using TiuWeb.ReportBase;
using EntityData.His.Inpatient;
using AutoServiceManage.Common;
using System.Collections;
using System.Drawing.Imaging;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmPrintListReport : Form
    {
        #region 变量
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        string _inHosOfficeID = string.Empty;
        DataSet InHosData = new DataSet();//住院病人信息
        bool IsCanInput = false;
        #endregion

        #region 构造函数,load,窗体事件
        public FrmPrintListReport()
        {
            InitializeComponent();
        }
        
        private void FrmPrintListReport_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();
            if (!BindPage())
            {
                this.Close();
            }
        }

        private void FrmPrintListReport_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
              
        #endregion

        #region 返回,退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        #endregion

        #region 住院信息查询
        private bool BindPage()
        {
            try
            {
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();

                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                this.lblPatient.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                this.lblSex.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                this.lblAge.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();
                this.lblIDCard.Text = "身份证号：" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["IDENTITYCARD"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];
                    _inHosID = drInHos["INHOSID"].ToString();
                    _inHosOfficeID = drInHos["INHOSOFFICEID"].ToString();
                    this.lblInHosID.Text = "住院号："+_inHosID;
                }
                DataTable dtReport = GetListData();
                if (dtReport != null && dtReport.Rows.Count != 0)
                {
                    this.gdcMain.DataSource = dtReport;
                    return true;
                }
                else
                {
                    SkyComm.ShowMessageInfo("未找到您的检验报告单信息!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("获取患者信息失败!");
                return false;
            }
        }
        #endregion

        #region 数据查询及更新
        /// <summary>
        /// 查询检验中间表，查询患者检验报告信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetListData()
        {
            try
            {
                DataTable dtAllReport = GreateDataTable();
                DateTime dtEnd =Convert.ToDateTime( new CommonFacade().GetServerDateTime().AddMonths(-3).ToString("yyyy-MM-dd 00:00:00"));

                if (!string.IsNullOrEmpty(_inHosID))
                {
                    string sql = "SELECT CHECK_NO,PATIENT_ID,FilePath,FileName,FileOrder,ItemName,LastOpTime,CrudFlag FROM LIS_REPORTFILE WHERE PATIENT_ID=@INHOSID AND LastOpTime >=@ENDTIME AND CrudFlag=1 AND PRINTCOUNT=0 ORDER BY LastOpTime ASC,FileOrder ASC";
                    Hashtable htm = new Hashtable();
                    htm.Add("@INHOSID", _inHosID);
                    htm.Add("@ENDTIME", dtEnd);
                    QuerySolutionFacade facadem = new QuerySolutionFacade();
                    DataSet dsReport = facadem.ExeQuery(sql, htm);
                    if (dsReport != null && dsReport.Tables.Count != 0 && dsReport.Tables[0].Rows.Count != 0)
                    {
                        object[] obj = new object[dtAllReport.Columns.Count];

                        for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                        {
                            dsReport.Tables[0].Rows[i].ItemArray.CopyTo(obj, 0);
                            dtAllReport.Rows.Add(obj);
                        }
                    }
                }
                DataSet dsRegister = this.GetRegisterIDInfo();
                if (dsRegister != null && dsRegister.Tables.Count != 0 && dsRegister.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drRegister in dsRegister.Tables[0].Rows)
                    {
                        string registerID = drRegister["REGISTERID"].ToString();

                        string sql = "SELECT CHECK_NO,PATIENT_ID,FilePath,FileName,FileOrder,ItemName,LastOpTime,CrudFlag FROM LIS_REPORTFILE WHERE PATIENT_ID=@DIAGNOSEID AND CrudFlag=1 AND PRINTCOUNT=0 ORDER BY LastOpTime ASC,FileOrder ASC";
                        Hashtable htm = new Hashtable();
                        htm.Add("@DIAGNOSEID", registerID);
                        //htm.Add("@ENDTIME", dtEnd);
                        QuerySolutionFacade facadem = new QuerySolutionFacade();
                        DataSet dsReport = facadem.ExeQuery(sql, htm);
                        if (dsReport != null && dsReport.Tables.Count != 0 && dsReport.Tables[0].Rows.Count != 0)
                        {
                            object[] obj = new object[dtAllReport.Columns.Count];

                            for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                            {
                                DateTime samplingTime = Convert.ToDateTime(dsReport.Tables[0].Rows[i]["LASTOPTIME"].ToString().Trim().Replace("@", ":"));
                                if (samplingTime <= Convert.ToDateTime(dtEnd))
                                    continue;

                                dsReport.Tables[0].Rows[i].ItemArray.CopyTo(obj, 0);
                                dtAllReport.Rows.Add(obj);
                            }
                        }
                    }
                }
                return dtAllReport;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("查询检验结果报表失败，原因：" + ex.Message);
                return null;
            }
        }

        private DataTable GreateDataTable()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("CHECK_NO");
            dtTemp.Columns.Add("PATIENT_ID");
            dtTemp.Columns.Add("FilePath");
            dtTemp.Columns.Add("FileName");
            dtTemp.Columns.Add("FileOrder");
            dtTemp.Columns.Add("ItemName");
            dtTemp.Columns.Add("LastOpTime");
            dtTemp.Columns.Add("CrudFlag");
            return dtTemp;
        }

        private DataSet GetRegisterIDInfo()
        {
            try
            {

                DateTime dtEnd = Convert.ToDateTime(new CommonFacade().GetServerDateTime().AddMonths(-3).ToString("yyyy-MM-dd 00:00:00"));

                string sql = "SELECT REGISTERID FROM T_REGISTER_INFO WHERE DIAGNOSEID=@DIAGNOSEID AND OPERATEDATE>=@ENDTIME";

                Hashtable htm = new Hashtable();
                htm.Add("@DIAGNOSEID", _diagnoseID);
                htm.Add("@ENDTIME", dtEnd);
                QuerySolutionFacade facadem = new QuerySolutionFacade();
                DataSet dsRegister = facadem.ExeQuery(sql, htm);
                return dsRegister;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("获取患者挂号信息失败，原因：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 更新检验报告中间表，更新打印状态
        /// </summary>
        private void UpdateListData()
        {
            DataTable dtPrint = (DataTable)this.gdcMain.DataSource;
            QuerySolutionFacade facade = new QuerySolutionFacade();
            try
            {
                foreach (DataRow dr in dtPrint.Rows)
                {
                    string sql = "UPDATE LIS_REPORTFILE SET PRINTCOUNT=PRINTCOUNT+1 WHERE CHECK_NO=@CHECKNO";
                    Hashtable ht = new Hashtable();
                    ht.Add("@CHECKNO", dr["CHECK_NO"].ToString());
                    facade.ExeNonQuery(sql, ht);
                }
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("更新检验结果中间表失败，原因：" + ex.Message);
            }

        }

        #endregion

        #region 确认打印
        private void lblOK_Click(object sender, EventArgs e)
        {
            try
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Stop();

                this.lblOK.Enabled = false;
                this.btnReturn.Enabled = false;
                this.btnExit.Enabled = false;

                #region 打印方法
                this.AnsyWorker(ui =>
                {
                    ui.UpdateTitle("正在打印，请稍等...");

                    ui.SynUpdateUI(() =>
                    {
                        DataTable dtAll = new DataTable();
                        DataTable dtPrint =(DataTable) this.gdcMain.DataSource;
                        foreach (DataRow drPrint in dtPrint.Rows)
                        {
                            string pdfpath =SkyComm.getvalue("LIS报告存放路径").ToString ()+ "\\"+drPrint["FilePath"].ToString ();
                            //if (Directory.Exists(pdfpath))
                            //{
                            try
                            {
                                DataTable dt = ConvertPDF2Image(pdfpath, 1, 99, ImageFormat.Jpeg, Definition.Five);
                                if (dt != null && dt.Rows.Count != 0)
                                {
                                    if (dtAll == null || dtAll.Rows.Count==0)
                                    {
                                        dtAll = dt.Copy();
                                    }
                                    else
                                    {
                                        object[] obj = new object[dtAll.Columns.Count];

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            dt.Rows[i].ItemArray.CopyTo(obj, 0);
                                            dtAll.Rows.Add(obj);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }
                            //}
                        }
                        if (dtAll != null && dtAll.Rows.Count != 0)
                        {
                            PrintManager printFirstPage = new PrintManager();
                            printFirstPage.InitReport("检验报告单");
                            printFirstPage.AddData(dtAll, "IMGPrintData");
                            PrintManager.CanDesign = true;
                            //printFirstPage.PreView();
                            printFirstPage.Print();
                            this.UpdateListData();
                            SkyComm.ShowMessageInfo("检验报告单打印成功!");
                            this.Close();
                        }
                        else
                        {
                            ucTime1.timer1.Start();
                            this.lblOK.Enabled = true;
                            this.btnReturn.Enabled = true;
                            this.btnExit.Enabled = true;
                            Skynet.LoggingService.LogService.GlobalErrorMessage("未在指定路径下找到检验报告的PDF文件！");
                            SkyComm.ShowMessageInfo("未找到检验单，请到窗口进行检验报告打印!");
                        }
                    });

                });
                #endregion
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("检验报告单打印失败，原因：" + ex.Message);
                SkyComm.ShowMessageInfo("检验报告单打印失败!");
                ucTime1.timer1.Start();
                this.lblOK.Enabled = true;
                this.btnReturn.Enabled = true;
                this.btnExit.Enabled = true;
            }
            finally
            {
                //ucTime1.timer1.Start();
                //this.lblOK.Enabled = true;
                //this.btnReturn.Enabled = true;
                //this.btnExit.Enabled = true;

            }
        }
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }
        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        public DataTable ConvertPDF2Image(string pdfInputPath, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
        {
            PDFFile pdfFile = PDFFile.Open(pdfInputPath);
            DataTable dtImage = new DataTable("ImageData");
            dtImage.Columns.Add("PageCount", typeof(System.Int32));
            DataColumn ImagePrint = new DataColumn("ImagePrint", typeof(Image));
            dtImage.Columns.Add(ImagePrint);
            if (startPageNum <= 0)
            {
                startPageNum = 1;
            }
            if (endPageNum > pdfFile.PageCount)
            {
                endPageNum = pdfFile.PageCount;
            }
            if (startPageNum > endPageNum)
            {
                int tempPageNum = startPageNum;
                startPageNum = endPageNum;
                endPageNum = startPageNum;
            }
            for (int i = startPageNum; i <= endPageNum; i++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
                MemoryStream ms = new MemoryStream();
                pageImage.Save(ms, imageFormat);

                Image imageJPG = Image.FromStream(ms);
                DataRow newRow = dtImage.NewRow();
                newRow["PageCount"] = i;
                newRow["ImagePrint"] = imageJPG;
                dtImage.Rows.Add(newRow);
                pageImage.Dispose();
            }
            pdfFile.Dispose();
            return dtImage;
        }
        #endregion

        #region 异步方法
        AnsyCall _call;
        List<string> NotEnableArray = new List<string>();
        protected void AnsyWorker(Action<UpdataUIAction> action, AnsyStyle style)
        {
            if (this.AnsyIsBusy)
                return;
            if (_call == null)
            {
                _call = new AnsyCall(this);
                _call.WorkCompletedAction = () =>
                {
                    NotEnableArray.Clear();
                };
            }
            //使工具栏 置灰
            _call.AnsyWorker(action, style);
        }
        protected void AnsyWorker(Action<UpdataUIAction> action)
        {
            AnsyWorker(action, AnsyStyle.LoadingPanel);
        }

        protected bool AnsyIsBusy
        {
            get
            {
                if (_call == null)
                    return false;
                else
                    return _call.IsBusy;
            }
        }
        #endregion


    }
}
