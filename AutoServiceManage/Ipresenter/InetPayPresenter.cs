using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoServiceManage.Ipresenter
{
    interface InetPayPresenter
    {
        List<string> getRequestBaseData();

        string getUrlByWebservice(Hashtable hashtable);

        string getUrl(Hashtable hashtable);//获取二维码
        string getUrl1(Hashtable hashtable);
        bool getPayResult();//获取支付结果

        string getSerialNo();//生成流水号

        void saveTradeInfo(decimal tradeMoney,string type, string payMethod);//保存交易数据

        bool saveCard(string type, decimal money, string payMethod);

        void updateHisState();

        void revokedTrade(string errorId);

        bool refundOrder(Hashtable hashtable);

        void printInfo(Hashtable hashtable,string type);
    }
}
