using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AutoServiceManage.Model;

namespace AutoServiceManage.Presenter
{
    public class ChargeMainPresenter
    {
        private ChargeMainModel mcChargeMainModel;

        public ChargeMainPresenter()
        {
            mcChargeMainModel = new ChargeMainModel();
        }

        public DataSet addWindowName(DataSet dataSet)
        {
           string officeId = mcChargeMainModel.getOfficeId(dataSet.Tables[0].Rows[0]["OFFICE"].ToString());
            string windowId = dataSet.Tables[0].Rows[0]["WINDOWID"].ToString();
            dataSet.Tables[0].Columns.Add("ArrayCount", typeof(string));

            int count = mcChargeMainModel.getArrayCount(officeId, windowId);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                row["ArrayCount"] = count + "";
            }
            return dataSet;
        }
    }
}
