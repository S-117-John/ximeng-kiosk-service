using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.Common;

namespace AutoServiceManage.Model
{
    public class ChargeMainModel
    {
        public ChargeMainModel()
        {
        }

        /// <summary>
        /// 根据诊疗号获取发药窗口
        /// </summary>
        /// <param name="dignoseId"></param>
        /// <returns></returns>
        public DataSet getWindowId(string dignoseId)
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00";

            DataSet mDataSet = new DataSet();

            string mSql = " select DISTINCT D_ECIPE_MEDICINE.DIAGNOSEID,T_PATIENT_INFO.PATIENTNAME,  D_ECIPE_MEDICINE.OPERATEDATE,P_DOSAGECOMPLETE.WINDOWID ,P_DOSAGECOMPLETE.PHARMARYWINDOWID,P_PHARMACYWINDOW.WINDOWNAME,P_DOSAGECOMPLETE.PHARMACYID from P_DOSAGECOMPLETE,D_ECIPE_MEDICINE,T_PATIENT_INFO,T_OFFICE,T_USERS,T_OPERATOR TP ,P_PHARMACYWINDOW where P_DOSAGECOMPLETE.DOSAGESTATE= 1 AND P_DOSAGECOMPLETE.DETAILACCOUNTID = D_ECIPE_MEDICINE.DETAILACCOUNTID AND CANCELMARK = 0 AND  SENDLEECHDOMMARK = 0  AND D_ECIPE_MEDICINE.DIAGNOSEID = T_PATIENT_INFO.DIAGNOSEID\tAND REGISTEROFFICEID = T_OFFICE.OFFICEID AND DOCTORID =  T_USERS.USERID AND D_ECIPE_MEDICINE.OPERATEDATE >= @OPERATEDATE AND D_ECIPE_MEDICINE.OPERATORID = TP.OPERATORID AND P_DOSAGECOMPLETE.MEDICALCODE =D_ECIPE_MEDICINE.MEDICALCODE AND  D_ECIPE_MEDICINE.MEDICALCODE= T_PATIENT_INFO.MEDICALCODE AND T_PATIENT_INFO.MEDICALCODE = T_OFFICE.MEDICALCODE AND T_OFFICE.MEDICALCODE= T_USERS.MEDICALCODE AND T_USERS.MEDICALCODE =  TP.MEDICALCODE AND TP.MEDICALCODE=@MEDICALCODE and  P_DOSAGECOMPLETE.PHARMARYWINDOWID = P_PHARMACYWINDOW.PHARMARYWINDOWID and P_DOSAGECOMPLETE.WINDOWID = P_PHARMACYWINDOW.WINDOWID order by D_ECIPE_MEDICINE.OPERATEDATE ";
                

            Hashtable ht = new Hashtable();
            
            ht.Add("@OPERATEDATE", Convert.ToDecimal(start));
            ht.Add("@MEDICALCODE", SkyComm.getvalue("MedicalCode"));
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            mDataSet = querySolutionFacade.ExeQuery(mSql, ht);

            return mDataSet;
        }


        public DataSet getWindowIdSend(string dignoseID)
        {
            string mSql = " select DISTINCT D_ECIPE_MEDICINE.DIAGNOSEID,T_PATIENT_INFO.PATIENTNAME,D_ECIPE_MEDICINE.OPERATEDATE , P_DOSAGE.WINDOWID,P_DOSAGE.PHARMACYID,P_PHARMACYWINDOW.PHARMARYWINDOWID,P_PHARMACYWINDOW.WINDOWNAME\r\nfrom P_DOSAGE,D_ECIPE_MEDICINE,T_PATIENT_INFO,T_OFFICE,T_USERS,P_PHARMACYWINDOW where D_ECIPE_MEDICINE.OPERATEDATE>@OPERATEDATE and P_DOSAGE.DOSAGESTATE= 0 AND P_DOSAGE.DETAILACCOUNTID = D_ECIPE_MEDICINE.DETAILACCOUNTID  AND  CANCELMARK = 0 AND SENDLEECHDOMMARK = 0 AND D_ECIPE_MEDICINE.DIAGNOSEID = T_PATIENT_INFO.DIAGNOSEID AND REGISTEROFFICEID = T_OFFICE.OFFICEID AND DOCTORID = USERID AND P_DOSAGE.MEDICALCODE  = D_ECIPE_MEDICINE.MEDICALCODE AND D_ECIPE_MEDICINE.MEDICALCODE =T_PATIENT_INFO.MEDICALCODE AND T_PATIENT_INFO.MEDICALCODE =T_OFFICE.MEDICALCODE AND T_OFFICE.MEDICALCODE =T_USERS.MEDICALCODE  AND T_USERS.MEDICALCODE = @MEDICALCODE and P_PHARMACYWINDOW.WINDOWTYPE = '发药' and  P_DOSAGE.PHARMACYID = P_PHARMACYWINDOW.PHARMACYID and P_DOSAGE.WINDOWID = P_PHARMACYWINDOW.WINDOWID ";
            string start = DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00";

            DataSet mDataSet = new DataSet();

            Hashtable ht = new Hashtable();
           
            ht.Add("@OPERATEDATE", Convert.ToDecimal(start));
            ht.Add("@MEDICALCODE", SkyComm.getvalue("MedicalCode"));
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            mDataSet = querySolutionFacade.ExeQuery(mSql, ht);

            return mDataSet;
        }


        public string getOfficeId(string officeName)
        {

            string mSql = "SELECT* FROM T_OFFICE a where a.OFFICE = @OFFICE and a.MEDICALCODE = @MEDICALCODE";
           

            DataSet mDataSet = new DataSet();

            Hashtable ht = new Hashtable();

            ht.Add("@OFFICE", officeName);
            ht.Add("@MEDICALCODE", SkyComm.getvalue("MedicalCode"));
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            mDataSet = querySolutionFacade.ExeQuery(mSql, ht);

            return mDataSet.Tables[0].Rows[0]["OFFICEID"].ToString();
            
        }

        public int getArrayCount(string officeID,string windowId)
        {
            string mSql = "SELECT * FROM P_DOSAGECOMPLETE where P_DOSAGECOMPLETE.PHARMACYID = @PHARMACYID and P_DOSAGECOMPLETE.PHARMARYWINDOWID = @PHARMARYWINDOWID and P_DOSAGECOMPLETE.MEDICALCODE = @MEDICALCODE";


            DataSet mDataSet = new DataSet();

            Hashtable ht = new Hashtable();

            ht.Add("@PHARMACYID", officeID);
            ht.Add("@PHARMARYWINDOWID", windowId);
            ht.Add("@MEDICALCODE", SkyComm.getvalue("MedicalCode"));
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();

            mDataSet = querySolutionFacade.ExeQuery(mSql, ht);

            return mDataSet.Tables[0].Rows.Count;
        }
    }
}
