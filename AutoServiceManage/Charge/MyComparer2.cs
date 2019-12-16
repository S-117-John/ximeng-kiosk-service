using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace AutoServiceManage.Charge
{
    public class MyComparer2:IEqualityComparer<DataRow>
    {
        #region IEqualityComparer<DataRow> 成员

        public bool Equals(DataRow x, DataRow y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Field<string>("处方号").Equals(y.Field<string>("处方号"));
        }

        public int GetHashCode(DataRow obj)
        {
            return obj.ToString().GetHashCode();
        }

        #endregion
    }
}
