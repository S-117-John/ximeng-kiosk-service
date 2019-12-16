using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceSDK.ISdkService
{
    public interface ISendCardInterFace
    {
        /// <summary>
        /// 写卡
        /// </summary>
        bool WriteCard(string CardNo);

        /// <summary>
        /// 吐卡
        /// </summary>
        /// <returns></returns>
        void OutputCard();

        /// <summary>
        /// 回收卡
        /// </summary>
        /// <returns></returns>
        void ReturnCard();

        string ReadCard();

        string CheckCard();
    }

    
}
