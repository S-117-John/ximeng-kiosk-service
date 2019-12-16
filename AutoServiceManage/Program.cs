using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoServiceManage;
using AutoServiceManage.Common;

namespace AutoServiceManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Runtime.Remoting.RemotingConfiguration.Configure("AutoServiceManage.exe.config", false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (SkyComm.Init() == true)
            {
                Application.Run(new FrmMainAuto());
            }
        }
    }
}
