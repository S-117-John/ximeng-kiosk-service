using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceSDK.ISdkService
{
    public interface IMoneyService
    {
        /// <summary>
        /// 打开纸币器端口
        /// </summary>
         bool OpenPort(string CardNo);
        
        /// <summary>
        /// 得到进入纸币面额
        /// </summary>
        /// <param name="Machineno">自助机编号</param>
        /// <param name="OperatorID">操作员ID</param>
         /// <returns>返回纸币面额</returns>
         int GetInMoney(string Machineno,string OperatorID);
        /// <summary>
        /// 关闭纸币器端口
        /// </summary>
         bool ClosePort();

         /// <summary>
         /// 获取状态
         /// </summary>
         /// <returns></returns>
         int GetStatus();

         bool NotAllowCashin();

         bool AllowCashin();

    }
}
