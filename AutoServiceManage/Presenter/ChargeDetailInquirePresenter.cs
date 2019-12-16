using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.Clinic;

namespace AutoServiceManage.Presenter
{
    public class ChargeDetailInquirePresenter
    {
        public ChargeDetailInquirePresenter()
        {
        }

        public DataSet getNewDetailInfo(DataSet dataSet)
        {
            DetailAccountFacade mDetailAccountFacade = new DetailAccountFacade();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string mType = dataRow["TYPE"].ToString();

                if (mType.Equals("检查费"))
                {
                    decimal mMoney = Convert.ToDecimal(dataRow["UNITPRICE"].ToString());
                    decimal count = Convert.ToDecimal(dataRow["AMOUNT"].ToString());
                    decimal mSelfMoney = Convert.ToDecimal(dataRow["SELFMONEY"].ToString());

                    decimal mTotalMoney = mMoney * count;

                    if (mTotalMoney != mSelfMoney)
                    {
                        dataRow["AMOUNT"] = mSelfMoney / mMoney;
                    }

                    
                }
                
            }
            return dataSet;
        }
    }
}
