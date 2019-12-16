using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceManage.CardSaving
{
    public class RequestUrlJson
    {
        public string hosId { get; set; }
        public string token { get; set; }
        public string serviceType { get; set; }
        public string hisTradeId { get; set; }
        public string payFee { get; set; }
        public string payMethod { get; set; }
        public string mchNum { get; set; }
    }
}
