using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessFacade.His.ClinicDoctor;
using EntityData.His.ClinicDoctor;
using SystemFramework.NewCommon;
using AutoServiceManage.CardSaving;
using AutoServiceManage.Common;
using BusinessFacade.His.Register;
using SystemFramework.SyncLoading;
using System.Threading;

namespace AutoServiceManage.AutoPrint
{
    public partial class FrmPrintMain : Form
    {
        private bool ishosCostListPrint = false;//住院费用清单打印
        private bool isEmrPrint = false;//门诊病历打印
        private bool isClinicCostListPrint = false;//门诊费用清单打印
        private bool isOutHos = false;//出院病历打印
        private bool isLisReportPrint = true;//Lis报告单打印
        private bool isGeneticReportPrint = false;//Lis报告单打印

        #region 构造函数，LOAD,关闭按钮事件
        public FrmPrintMain()
        {
            InitializeComponent();
        }

        private void FrmPrintMain_Load(object sender, EventArgs e)
        {
            this.lblLisReportPrint.Image = Properties.Resources.lblButton;
            this.ucTime1.Sec = 60;
            this.ucTime1.timer1.Start();

            this.lblxm.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
            this.lblxb.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
            this.lblnl.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGE"].ToString() + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["AGEUNIT"].ToString();

            string Role = AutoHostConfig.MachineType;
            AutoServiceMachineInfoFacade AutoServiceFac = new AutoServiceMachineInfoFacade();
            DataSet dsRoleMenu = AutoServiceFac.GetAutoServiceRoleMenuByRoleID(Role, "二级");
            if (dsRoleMenu.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsRoleMenu.Tables[0].Rows.Count; i++)
                {
                    string MenuID = dsRoleMenu.Tables[0].Rows[i]["MENUID"].ToString();
                    switch (MenuID)
                    {
                        case "16"://住院费用清单打印
                            ishosCostListPrint = true;
                            //this.btnInhosCostListPrint.Image = Image.FromFile("");
                            this.btnInhosCostListPrint.Image = Properties.Resources.lblButton; ;
                            break;
                        case "17"://检验报告打印
                            isLisReportPrint = true;
                            this.lblLisReportPrint.Image = Properties.Resources.lblButton;
                            break;
                        case "18"://门诊病历打印
                            isEmrPrint = true;
                            this.lblClinicEmrPrint.Image = Properties.Resources.lblButton;
                            break;
                        case "19"://门诊费用清单打印
                            isClinicCostListPrint = true;
                            this.btnClinicCostListPrint.Image = Properties.Resources.lblButton;
                            break;
                        case "20"://出院病历打印
                            isOutHos = true;
                            this.txtOutHos.Image = Properties.Resources.lblButton;
                            break;
                        case "23"://遗传报告打印 wangchenyang case 30344 自助机增加遗传报告打印功能
                            isGeneticReportPrint = true;//遗传报告打印
                            this.txtGeneticReport.Image = Properties.Resources.lblButton;
                            break;
                    }

                }
            }
        }

        private void FrmPrintMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }
        #endregion

        #region 返回，退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        #endregion

        #region 门诊病历打印
        private void lblClinicEmrPrint_Click(object sender, EventArgs e)
        {
            if (isEmrPrint)
            {
                #region
                this.ucTime1.timer1.Stop();

                //查询当前的所有病历
                DateTime startTime = Convert.ToDateTime(DateTime.Now.Date);
                DateTime endTime = Convert.ToDateTime(DateTime.Now.Date.AddDays(1));
                EntityList<ClinicBriefemrData> list = new ClinicBriefemrFacade().GetHistoryEmr(SkyComm.DiagnoseID, string.Empty, string.Empty, startTime, endTime);
                if (list.Count == 0)
                {
                    SkyComm.ShowMessageInfo("当天没有需要打印的门诊病历信息！");
                    this.ucTime1.Sec = 60;
                    this.ucTime1.timer1.Start();
                    return;
                }
                FrmPrintClinicEMR frm = new FrmPrintClinicEMR();
                try
                {
                    frm.listEmr = list;
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("门诊病历打印失败：" + ex.Message);
                    SkyComm.ShowMessageInfo("门诊病历打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }

        }
        #endregion

        #region LIS报告打印
        private void lblLisReportPrint_Click(object sender, EventArgs e)
        {
            if (isLisReportPrint)
            {
                #region
                this.ucTime1.timer1.Stop();
                Form frm = new Form();
                try
                {
                    if (SkyComm.getvalue("LIS厂商类型").ToString() == "智方")
                    {
                        frm = new FrmPrintListReport();
                        frm.ShowDialog(this);
                    }
                    else if (SkyComm.getvalue("LIS厂商类型").ToString() == "杏和")
                    {
                        frm = new FrmPrintLisReportXH();
                        frm.ShowDialog(this);
                    }
                    else if (SkyComm.getvalue("LIS厂商类型").ToString() == "省医院智方")
                    {
                        string path = System.IO.Directory.GetCurrentDirectory() + "\\LisPrint_ZF\\";
                        string fileName = "WindowsFormsApplication1.exe";
                        StartProcess(null, path, fileName);
                        //SkyComm.ShowMessageInfo("检查报告单打印完毕!");
                    }
                    else if (SkyComm.getvalue("LIS厂商类型").ToString() == "锡盟智方")
                    {
                        PrintListForXM();
                    }
                    else if (SkyComm.getvalue("LIS厂商类型").ToString() == "瑞美")
                    {
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                        p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                        p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                        p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                        p.Start();//启动程序

                        //向cmd窗口发送输入信息
                        p.StandardInput.WriteLine(@"D:\rmlis6\rmlis6_report 2@" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString() + " &exit");

                        p.StandardInput.AutoFlush = true;
                        //p.StandardInput.WriteLine("exit");
                        //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
                        //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



                        //获取cmd窗口的输出信息
                        //string output = p.StandardOutput.ReadToEnd();

                        //StreamReader reader = p.StandardOutput;
                        //string line=reader.ReadLine();
                        //while (!reader.EndOfStream)
                        //{
                        //    str += line + "  ";
                        //    line = reader.ReadLine();
                        //}

                        p.WaitForExit();//等待程序执行完退出进程
                        p.Close();



                        //1@" + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString()
                        //Process p = Process.Start(@"D:\rmlis6\rmlis6_report.exe");
                        //p.WaitForExit();

                    }
                }
                catch (Exception ex)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("检验报告打印失败：" + ex.Message);
                    SkyComm.ShowMessageInfo("检验报告打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }

        }
        #endregion
        private void PrintListForXM()
        {
            try
            {
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Stop();
                this.btnReturn.Enabled = false;
                this.btnExit.Enabled = false;
                FrmYesNoAlert frmAlert = new FrmYesNoAlert();
                frmAlert.Title = "提示";
                //frmAlert.Msg = "是否确认打印检查报告单？                    注意：如果打印过程结束而自助机未打印报告单，则代表您没有存在检查结果的检查报告单信息！";
                frmAlert.Msg = "是否确认打印？                                如未打印，代表当前没有可打印的报告单！";
                if (frmAlert.ShowDialog() == DialogResult.Cancel)
                {
                    this.ucTime1.timer1.Start();
                    this.btnExit.Enabled = true;
                    this.btnReturn.Enabled = true;
                    return;
                }

                SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
                voice.PlayText("正在打印。如果未打印，代表当前没有可打印的报告单!");
                voice.EndJtts();

                this.AnsyWorker(ui =>
                {
                    ui.UpdateTitle("正在打印，请稍等...");

                    ui.SynUpdateUI(() =>
                    {
                        //string printParams = SkyComm.DiagnoseID;
                        string printParams =SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["CARDID"].ToString();
                        string[] argsList = new string[1];
                        argsList[0] = printParams;
                        string path = System.IO.Directory.GetCurrentDirectory() + "\\LisPrint_ZF\\";
                        string fileName = "AutoPrint.exe";
                        StartProcess_New(argsList, path, fileName);
                        Thread.Sleep(100);
                        SkyComm.ShowMessageInfo("检查报告单打印完毕!");
                        this.ucTime1.timer1.Start();
                        this.btnExit.Enabled = true;
                        this.btnReturn.Enabled = true;
                    });

                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void StartProcess_New(string[] argsList, string path, string fileName)
        {
            //声明一个程序信息类
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名
            Info.FileName = fileName;
            //设置外部程序的启动参数（命令行参数）为test.txt
            Info.Arguments = " " + argsList[0].ToString();
            //设置外部程序工作目录为  C:\
            Info.WorkingDirectory = path;
            try
            {
                //
                //启动外部程序
                //
                System.Diagnostics.Process.Start(Info).WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                SkyComm.ShowMessageInfo("检查报告单打印失败!");
            }
        }
        private void StartProcess(string[] argsList, string path, string fileName)
        {
            //声明一个程序信息类
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名
            Info.FileName = fileName;
            //设置外部程序的启动参数（命令行参数）为test.txt
            Info.Arguments =null;
            //设置外部程序工作目录为  C:\
            Info.WorkingDirectory = path;
            try
            {
                //
                //启动外部程序
                //
                System.Diagnostics.Process.Start(Info).WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                SkyComm.ShowMessageInfo("检查报告单打印失败!");
            }
        }
        /// <summary>
        /// 门诊费用清单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClinicCostListPrint_Click(object sender, EventArgs e)
        {
            if (isClinicCostListPrint)
            {
                #region
                this.ucTime1.timer1.Stop();


                FrmClinicCostListPrint frm = new FrmClinicCostListPrint();
                try
                {
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("门诊费用清单打印失败：" + ex.Message);
                    SkyComm.ShowMessageInfo("门诊费用清单打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }
        
        }
        /// <summary>
        /// 住院费用清单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInhosCostListPrint_Click(object sender, EventArgs e)
        {
            if (ishosCostListPrint)
            {
                #region
                this.ucTime1.timer1.Stop();
                FrmInhosCostListPrint frm = new FrmInhosCostListPrint();
                try
                {
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    Skynet.LoggingService.LogService.GlobalInfoMessage("住院费用清单打印失败：" + ex.Message);
                    SkyComm.ShowMessageInfo("住院费用清单打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }
           
        }

        /// <summary>
        /// 出院病历打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOutHos_Click(object sender, EventArgs e)
        {
            if (isOutHos)
            {
                #region
                Log.Info(GetType().ToString(), "点击出院病例打印");

                this.ucTime1.timer1.Stop();


                FrmInhosCostListPrint frm = new FrmInhosCostListPrint();
                try
                {
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {

                    Log.Info(GetType().ToString(), "点击出院病例打印异常信息：" + ex.Message);
                    SkyComm.ShowMessageInfo("出院病例打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }
          
        }
        /// <summary>
        /// 遗传报告打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGeneticReport_Click(object sender, EventArgs e)
        {
            if (isGeneticReportPrint)
            {
                #region
                Log.Info(GetType().ToString(), "点击遗传报告打印");

                this.ucTime1.timer1.Stop();


                FrmGeneticReportPrint frm = new FrmGeneticReportPrint();
                try
                {
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {

                    Log.Info(GetType().ToString(), "点击遗传报告打印异常信息：" + ex.Message);
                    SkyComm.ShowMessageInfo("遗传报告打印失败!" + ex.Message);
                }
                finally
                {
                    frm.Dispose();
                }
                this.ucTime1.Sec = 60;
                this.ucTime1.timer1.Start();
                #endregion
            }
        }
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
