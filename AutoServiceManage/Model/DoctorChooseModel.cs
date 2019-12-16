using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.Disjoin;

namespace AutoServiceManage.Model
{
    public class DoctorChooseModel
    {
        public DoctorChooseModel()
        {
        }

        /// <summary>
        /// 获取E_TECHNICALPOST表中的REGISTERTYPEID
        /// </summary>
        /// <param name="role">TECHNICALPOST</param>
        /// <returns></returns>
        public string getRegisterTypeId(string role)
        {
            ArranageRecordFacade arrageRecordFacade = new ArranageRecordFacade();

            DataSet dataSet = arrageRecordFacade.getRegisterTypeId(role);

            string mRegisterTypeId = "";

            if (dataSet == null || dataSet.Tables[0].Rows.Count == 0)
            {
                return "";
            }

            mRegisterTypeId = dataSet.Tables[0].Rows[0]["REGISTERTYPEID"].ToString();
            return mRegisterTypeId;
        }
    }
}
