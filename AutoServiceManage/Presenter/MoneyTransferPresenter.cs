using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AutoServiceManage.Model;

namespace AutoServiceManage.Presenter
{
    public class MoneyTransferPresenter
    {
        private MoneyTransferModel moneyTransferModel;

        public MoneyTransferPresenter()
        {
            moneyTransferModel = new MoneyTransferModel();
        }

        public void addDatas(DataSet dataSet,string queue,string payType)
        {

            //string mPaytype = SkyComm.getvalue("住院预交金充值方式_门诊预交金");

            string mPaytype = string.Empty;

            if (payType == "现金")
            {
                mPaytype= SkyComm.getvalue("住院预交金充值方式_现金");
            }
            else if(payType=="银行卡")
            {
                mPaytype = SkyComm.getvalue("住院预交金充值方式_银行卡");
            }
            else if(payType== "门诊预交金转住院预交金")
            {
                mPaytype = SkyComm.getvalue("住院预交金充值方式_门诊预交金");
            }
            
            DataSet payDataSet = moneyTransferModel.getPayType(mPaytype);
            if (payDataSet == null || payDataSet.Tables.Count == 0 || payDataSet.Tables[0].Rows.Count == 0)
            {
                SkyComm.ShowMessageInfo("支付方式["+mPaytype+"]设置错误！");
            }

            if ( !dataSet.Tables[0].Columns.Contains("科室"))
            {
                dataSet.Tables[0].Columns.Add("科室", typeof(string));  
            }
     
            dataSet.Tables[0].Columns.Add("费用类别", typeof(string));
            dataSet.Tables[0].Columns.Add("流水", typeof(string));
            dataSet.Tables[0].Columns.Add("支付方式", typeof(string));
            string officeName =
                moneyTransferModel.getOfficeName(dataSet.Tables[0].Rows[0]["ENTERHOSOFFICEID"].ToString());

            string type = moneyTransferModel.getType(dataSet.Tables[0].Rows[0]["FEETYPEID"].ToString());

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                row["科室"] = officeName;
                row["费用类别"] = type;
                row["流水"] = queue;
                row["支付方式"] = payDataSet.Tables[0].Rows[0]["CHARGETYPE"];
            }
        }

        public DataSet getBankInfo(string id, string HisSeqNo)
        {
            return moneyTransferModel.getBankInfo(id, HisSeqNo);
        }
    }
}
