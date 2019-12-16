using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessFacade.His.Clinic;
using SystemFramework.SyncLoading;
using TiuWeb.ReportBase;
using System.Xml;
using System.IO;
using System.Net;
using O2S.Components.PDFRender4NET;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmGeneticReportPrint : Form
    {

        #region 属性
        public string downLoadpath = Application.StartupPath + @"\pdfs";
        public string PdfPath = string.Empty;
        private List<string> PdfPathList = new List<string>();

        private string strResponsexml = string.Empty;
        public enum Result
        {
            success = 0,
            fail = -1,
            notfound = -2
        }
        public enum Definition
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10
        }
        #endregion

        #region 构造
        public FrmGeneticReportPrint()
        {
            InitializeComponent();
            ucTime1.Visible = true;
        }
        #endregion

        #region Load

        private void FrmGeneticReportPrint_Load(object sender, EventArgs e)
        {
            initGentciData();
        }
        #endregion

        #region 调用webservice查询数据

        /// <summary>
        /// 查询报表数据并打印
        /// </summary>
        private void initGentciData() 
        {
            try
            {
                //查询打印数据
                String responsexml = string.Empty;
                var resultEnum = getGenticReportData(out responsexml);
                if (resultEnum != Result.success)
                {
                    SkyComm.ShowMessageInfo(responsexml);
                    return;
                }
                responsexml = responsexml.Replace("<samples></samples>", "");
                int returnNumber = Regex.Matches(responsexml, @"</sample>").Count;
                string printNameAll = string.Empty;
                for (int i = 0; i < returnNumber; i++)
                {
                    string itemName = GetNoteValue(responsexml, "sample", "item_name",i);
                    //lblPrint.Text = string.Format("正在打印(项目名称[{0}])，请稍等...", itemName);
                    printNameAll += string.IsNullOrEmpty(printNameAll) ? itemName : "," + itemName;

                    PdfPath = GetNoteValue(responsexml, "sample", "report_url",i);

                    if (string.IsNullOrEmpty(PdfPath))
                    {
                        //SkyComm.ShowMessageInfo("没有找到需要打印的报告PDF路径!");
                        //return;
                    }
                    else
                    {
                        PdfPathList.Add(PdfPath);
                    }
                }
                printNameAll = "【" + printNameAll + "】";
                lblPrint.Text = "正在打印" + printNameAll + ",请稍等...";
                ///打印
                timer1.Start();
            }
            catch (Exception ex)
            {               
                 SkyComm.ShowMessageInfo("没有找到需要打印的报告PDF路径!");                    
            }
          
        }

        /// <summary>
        /// 调用产前诊断平台,webservice
        /// </summary>
        /// <returns></returns>
        private Result getGenticReportData(out string genticxml)
        {
            string strResult = string.Empty;
            StringBuilder stbRuestxml = new StringBuilder();
            string geneticpath = SkyComm.getvalue("产前诊断接口");

            ///入参
            stbRuestxml.Append("<PDxml> ");
            stbRuestxml.Append("<uid>HIS</uid>");
            stbRuestxml.Append("<pwd>HIS</pwd>");
            stbRuestxml.Append("<params>");
            stbRuestxml.Append(string.Format("<personid>{0}</personid>", SkyComm.cardInfoStruct.CardNo));//卡号
            stbRuestxml.Append("<code></code>");
            stbRuestxml.Append("<frm_code></frm_code>");
            stbRuestxml.Append("</params>");
            stbRuestxml.Append("</PDxml>");

            //webservice配置地址
            if (geneticpath == string.Empty)
            {
                genticxml = "[遗传报告接口]服务地址未配置！";
                return Result.fail;
            }

            ///参数
            object[] obj = new object[1];
            obj[0] = stbRuestxml.ToString();

            try
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用遗传报告接口getReport方法输入参数：" + stbRuestxml.ToString());
                //执行
                strResult= Common.WebServiceHelper.InvokeWebService(geneticpath, "getReport", obj).ToString();
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用遗传报告接口getReport方法返回值：" + strResult.ToString());

                //strResult = "<PDxml><msg>True</msg><sample><personid>110</personid><frm_code>123</frm_code>  <code>1001</code>  <name>测试</name><gender>女</gender><old>28</old>" +
                //    "<tel>133333333333333</tel>  <birthday>1989-06-14</birthday>  <receivedate>2017-06-16</receivedate>" +
                //    "  <item_code>EAR</item_code>  <item_name>遗传性耳聋基因检测（9个位点）</item_name>  <report_msg>true</report_msg> " +
                //    "<report_url>http://localhost:7068/SQLScript/图片文档.pdf</report_url> </sample> </PDxml>";

                string msg = GetNoteValue(strResult, "PDxml", "msg",0);
                if (msg.ToLower() != "true")
                {
                    genticxml = msg;
                    Skynet.LoggingService.LogService.GlobalInfoMessage("调用遗传报告接口getReport方法失败，返回值：" + strResult.ToString());
                    return Result.fail;
                }

            }
            catch (Exception ex)
            {
                genticxml = ex.ToString();
                Skynet.LoggingService.LogService.GlobalInfoMessage("调用遗传报告接口getReport方法失败，返回值：" + ex.ToString());
                throw ex;
            }
            genticxml = strResult;
            strResponsexml = strResult;
            return Result.success;
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                foreach (string _pdfPath in PdfPathList)
                {
                    if (!string.IsNullOrEmpty(_pdfPath) && _pdfPath.Contains(".pdf"))
                    {
                        string saveFileName = Guid.NewGuid().ToString();
                        //downLoadpath = downLoadpath + @"\" + System.IO.Path.GetFileName(_pdfPath);
                        string _downLoadpath = downLoadpath + @"\" +saveFileName+".pdf";
                        //下载pdf文件
                        if (!System.IO.File.Exists(_downLoadpath))
                        {
                            HttpDownload(_pdfPath, _downLoadpath);
                        }
                        PrintPDF(_downLoadpath);
                    }
                }
                timer1.Stop();
                this.ucTime1.timer1.Stop();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("打印报告异常!");
                Skynet.LoggingService.LogService.GlobalInfoMessage("打印报告异常：" + ex.Message);
            }
            finally
            {
                ucTime1.Sec = 60;
                ucTime1.timer1.Start();
            }
        }

        /// <summary>
        /// 根据检查单信息
        /// </summary>
        /// <param name="rowList">进行打印</param>
        public void PrintPDF(string PdfPath)
        {
            try
            {
                DataTable dtPrint = ConvertPDF2Image(PdfPath, 1, 30, ImageFormat.Jpeg, Definition.Five);
                PrintManager PrintPDF = new PrintManager();
                PrintPDF.InitReport("遗传报告单");
                PrintPDF.AddData(dtPrint, "IMGPrintData");
                //PrintManager.CanDesign = true;
                //PrintPDF.PreView();
                PrintPDF.Print();
                PrintPDF.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //退出
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucTime1.timer1.Stop();
            timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 将PDF文档转换为图片的方法
        /// <summary>
        /// 将PDF文档转换为图片的方法
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="definition">设置图片的清晰度，数字越大越清晰</param>
        public DataTable ConvertPDF2Image(string pdfInputPath, int startPageNum, int endPageNum, ImageFormat imageFormat, Definition definition)
        {
            // pdfInputPath = "\\\\192.168.23.75\\web\\TEMPPDF\\1.pdf";
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
                Image datatableImage = Image.FromStream(ms);
                DataRow newRow = dtImage.NewRow();
                newRow["PageCount"] = i;
                newRow["ImagePrint"] = datatableImage;
                dtImage.Rows.Add(newRow);
                pageImage.Dispose();
            }
            pdfFile.Dispose();
            return dtImage;
        }
        #endregion

        #region xml操作

        ///  获取XML某节点值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetNoteValue(string tradeMsg, string tradeType, string nodeName,int i)
        {
            //XDocument xDoc = XDocument.Load("a.xml");
            //XNamespace n = @"http://www.abc.com";
            //var ele = from item in xDoc.Descendants(n + "book")
            //          select item.Value;     
            string tragetField = string.Empty;
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(tradeMsg);
                //if (xd.NameTable != null)
                //{
                //    //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xd.NameTable);
                //    //nsmgr.AddNamespace("card", "http://www.sxhealth.gov.cn/card");
                //    //tragetField = xd.SelectSingleNode("//" + tradeType, nsmgr).SelectSingleNode(nodeName).InnerText.Trim();
                //}
                tragetField = xd.SelectNodes("//" + tradeType)[i].SelectSingleNode(nodeName).InnerText.Trim();
                return tragetField;
            }
            catch
            {
                return "-1";//节ú点?错洙?误ó
            }
        }
        #endregion

        #region 根据地址下载pdf文件

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public bool HttpDownload(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(tempPath))
            {
                System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            }
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path); //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                return true;

            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("下载目标pdf失败，返回值：" + ex.ToString());
                return false;
            }
        }
        #endregion
     
    }
}
