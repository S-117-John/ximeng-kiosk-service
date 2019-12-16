using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceSDK.ISdkService
{
    public interface IPrintManage
    {
        /// <summary>
        /// 检查打印机状态是否正常
        /// </summary>
        /// <returns>可状态返回空，不能使用返回原因</returns>
        string CheckPrintStatus();
    }
}
