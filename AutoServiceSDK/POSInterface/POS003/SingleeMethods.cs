using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoServiceSDK.POSInterface.POS003
{
    /// <summary>
    /// 新利 MIS-POS 软件自助机接口
    /// </summary>
    internal static class SingleeMethods
    {
        /// <summary>
        /// 密码输入回调函数，用于将用户的按键回显给 UI 界面。
        /// </summary>
        public delegate void EnterPasswordCallBack(IntPtr pConf, char chKey);

        /// <summary>
        /// 新利 MIS-POS 自助机接口API
        /// </summary>
        [DllImport(@"SLMisTrans.dll", EntryPoint = "CardTransCBK")]
        public static extern int CardTransCBK(string strRequest, StringBuilder strResponse, EnterPasswordCallBack fnPwdHandler, IntPtr pConf);

        /// <summary>
        /// 新利 MIS-POS 动态库自画窗口需要提供一张图片，输密时动态库显示那张图片并在图片上显示输密过程
        /// </summary>
        [DllImport(@"SLMisTrans.dll", EntryPoint = "CardTransDllWin")]
        public static extern int CardTransDllWin(string strRequest, StringBuilder strResponse);

        /// <summary>
        /// 新利 MIS-POS 动态库自画窗口需要提供一张图片，输密时动态库显示那张图片并在图片上显示输密过程
        /// </summary>
        [DllImport(@"SLMisTrans.dll", EntryPoint = "CardTransDllWin")]
        public static extern int CardTransDllWin(byte[] InStr, byte[] OutStr);


        /// <summary>
        /// 截取字节数组
        /// </summary>
        /// <param name="srcBytes">要截取的字节数组</param>
        /// <param name="startIndex">开始截取位置的索引</param>
        /// <param name="length">要截取的字节长度</param>
        /// <returns>截取后的字节数组</returns>
        public static byte[] SubByte(byte[] srcBytes, int startIndex, int length)
        {
            using (System.IO.MemoryStream bufferStream = new System.IO.MemoryStream())
            {
                byte[] returnByte = new byte[] { };
                if (srcBytes == null) { return returnByte; }
                if (startIndex < 0) { startIndex = 0; }
                if (startIndex < srcBytes.Length)
                {
                    if (length < 1 || length > srcBytes.Length - startIndex) { length = srcBytes.Length - startIndex; }
                    bufferStream.Write(srcBytes, startIndex, length);
                    returnByte = bufferStream.ToArray();
                    bufferStream.SetLength(0);
                    bufferStream.Position = 0;
                }
                bufferStream.Close();
                bufferStream.Dispose();
                return returnByte;
            }

        }

    }
}
