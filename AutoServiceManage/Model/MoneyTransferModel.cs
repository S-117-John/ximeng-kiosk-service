using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.Common;

namespace AutoServiceManage.Model
{
    class MoneyTransferModel
    {

        public string getOfficeName(string id)
        {
            DataSet dataSet = new DataSet();

            string sql = "select * from T_OFFICE a where a.OFFICEID = @OFFICEID";

            Hashtable ht = new Hashtable();
            ht.Add("@OFFICEID", id);
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            dataSet = querySolutionFacade.ExeQuery(sql, ht);

            return dataSet.Tables[0].Rows[0]["OFFICE"].ToString();
        }

        public string getType(string id)
        {
            string sql = "SELECT * FROM S_CHARGE_KIND a where a.CHARGEKINDID = @CHARGEKINDID";
            DataSet dataSet = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@CHARGEKINDID", id);
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            dataSet = querySolutionFacade.ExeQuery(sql, ht);

            return dataSet.Tables[0].Rows[0]["CHARGEKIND"].ToString();
           
        }

        public DataSet getBankInfo(string id, string HisSeqNo)
        {
            string sql = "SELECT * FROM T_BANKHISEXCHANGE_TRANS a where a.DIAGNOSEID = @HISID and a.HISSEQNO = @HISSEQNO";
            DataSet dataSet = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@HISID", id);
            ht.Add("@HISSEQNO", HisSeqNo);
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            return querySolutionFacade.ExeQuery(sql, ht);
        }

        public DataSet getPayType(string id)
        {
            string sql = "SELECT * FROM H_CHARGE_TYPE a where a.CHARGETYPEID = @CHARGETYPEID";
            DataSet dataSet = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@CHARGETYPEID", id);
           
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            return querySolutionFacade.ExeQuery(sql, ht);
        }

    }
}
