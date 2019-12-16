using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skynet.LoggingService;

namespace AutoServiceManage.Common
{
    public class Log
    {
        public static void Info(string className, string handType, string content)
        {
            LogService.GlobalInfoMessage("****************************************************************************************************************************");
            LogService.GlobalInfoMessage("**");
            LogService.GlobalInfoMessage("**");
            LogService.GlobalInfoMessage("**\t【" + className + "】\t**");
            LogService.GlobalInfoMessage("**\t【" + handType + "】\t**");
            LogService.GlobalInfoMessage("**\t【" + content + "】\t**");
            LogService.GlobalInfoMessage("**");
            LogService.GlobalInfoMessage("**");
            LogService.GlobalInfoMessage("****************************************************************************************************************************\r");
            LogService.GlobalInfoMessage("\r");
            LogService.GlobalInfoMessage("\r");
            LogService.GlobalInfoMessage("\r");
            LogService.GlobalInfoMessage("\r");
        }

         //在网站根目录下创建日志目录
        public static string path = Application.StartupPath + @"\\logs\\";

        /**
         * 向日志文件写入调试信息
         * @param className 类名
         * @param content 写入内容
         */
        public static void Debug(string className, string content)
        {
            WriteLog("DEBUG", className, content);
        }

        /**
        * 向日志文件写入运行时信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Info(string className, string content)
        {
            WriteLog(SkyComm.DiagnoseID, className, content);
        }

        /**
        * 向日志文件写入出错信息
        * @param className 类名
        * @param content 写入内容
        */
        public static void Error(string className, string content)
        {
            WriteLog("ERROR", className, content);
        }

        /**
        * 实际的写日志操作
        * @param type 日志记录类型
        * @param className 类名
        * @param content 写入内容
        */
        protected static void WriteLog(string type, string className, string content)
        {
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = "";
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString()))
            {
                filename = path + className + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            else {
                filename = path + type + SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString() + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//用日期对日志文件命名
            }

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            //向日志文件写入内容
            string write_content = time + " " + type + " " + className + ": " + content;

            mySw.WriteLine("***************************************************");
            mySw.WriteLine("");
            mySw.WriteLine(write_content);
            mySw.WriteLine("");
            mySw.WriteLine("***************************************************");
            mySw.WriteLine("");
            mySw.WriteLine("");
            mySw.WriteLine("");
            mySw.WriteLine("");
            mySw.WriteLine("");
            //关闭日志文件
            mySw.Close();
        }
    }
}
