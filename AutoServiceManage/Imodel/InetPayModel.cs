using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoServiceManage.Imodel
{
    interface InetPayModel
    {
        void saveTradeInfo(Hashtable hashtable);

        DataSet getPayResult(string hisSerialNo);

        void svaeCard(string type, decimal money, string payMethod, string checkLot);

        void updateHisState(string hisSerialNo);

        string getUrl(Hashtable hashtable);

        void revokedTrade(string hisSerialNo,string errorId);

        List<string> refundTrade(Hashtable hashtable);

        void updateRefundOrderResult(List<string> list,string hisSerialNo);
    }
}
