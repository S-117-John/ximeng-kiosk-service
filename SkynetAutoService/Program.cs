using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using AutoServiceManage;
using Skynet.LoggingService;
using SkynetAutoUpdate;

namespace SkynetAutoService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                System.Runtime.Remoting.RemotingConfiguration.Configure("SkynetAutoService.exe.config", false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //判断自动更新程序是否存在，自动更新配置是否存在。
                if (System.IO.File.Exists("Autoupdater.config"))
                {
                    LogService.GlobalInfoMessage("开始自动更新");
                    #region check and download new version program
                    bool bHasError = false;
                    IAutoUpdater autoUpdater = new AutoUpdater();
                    try
                    {
                        autoUpdater.Update();
                    }
                    catch (WebException exp)
                    {
                        MessageBox.Show("客户端智能更新系统配置文件错误！\r\n" + exp.Message);
                        bHasError = true;
                    }
                    catch (XmlException exp)
                    {
                        bHasError = true;
                        MessageBox.Show("下载升级文件错误！\r\n" + exp.Message);
                    }
                    catch (NotSupportedException exp)
                    {
                        bHasError = true;
                        MessageBox.Show("升级地址配置错误！\r\n" + exp.Message);
                    }
                    catch (ArgumentException exp)
                    {
                        bHasError = true;
                        MessageBox.Show("下载升级文件错误！\r\n" + exp.Message);
                    }
                    catch (Exception exp)
                    {
                        bHasError = true;
                        MessageBox.Show("在升级过程中发生错误！\r\n" + exp.Message);
                    }
                    finally
                    {
                        if (bHasError == true)
                        {
                            try
                            {
                                autoUpdater.RollBack();
                            }
                            catch (Exception ex)
                            {
                                //Log the message to your file or database
                                MessageBox.Show("回滚失败！" + ex.Message);
                            }
                        }
                    }
                    #endregion

                    LogService.GlobalInfoMessage("开始自动完成");
                }
                try
                {                    

                    if (SkyComm.Init() == true)
                    {
                        string Classname = SkyComm.getvalue("ClassName");
                        if (string.IsNullOrEmpty(Classname))
                            Classname = "FrmMainAuto";

                        object Object = (object)Activator.CreateInstance(Type.GetType("AutoServiceManage." + Classname + ",AutoServiceManage"));
                        Application.Run(Object as Form);
                    }
                }
                catch (Exception ex)
                {
                    SkyComm.ShowMessageInfo(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Skynet.Framework.Common.SkynetMessage.MsgInfo("打开失败：" + ex.Message);
            }
           
        }
    }
}
