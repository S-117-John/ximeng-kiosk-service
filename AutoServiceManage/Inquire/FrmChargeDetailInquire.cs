using BusinessFacade.His.Clinic;
using BusinessFacade.His.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Presenter;
using System.Collections;

namespace AutoServiceManage.Inquire
{
    public partial class FrmChargeDetailInquire : Form
    {
        private ChargeDetailInquirePresenter mChargeDetailInquirePresenter;
        private QuerySolutionFacade query = new QuerySolutionFacade();
        public FrmChargeDetailInquire()
        {
            InitializeComponent();

            mChargeDetailInquirePresenter = new ChargeDetailInquirePresenter();
        }

        DetailAccountFacade detailFacade = null;

        DataSet ds = new DataSet();

        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmChargeDetailInquire_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        private void FrmChargeDetailInquire_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;

            ucTime1.timer1.Start();

            detailFacade = new DetailAccountFacade();

            string sql = "SELECT TYPE,NAME,UNITPRICE,AMOUNT,SUM(SELFMONEY) SELFMONEY,INVOICEID,INVOICEDATE,OPERATEDATE FROM (" +
            " SELECT CASE CLE.RECIPETYPE WHEN '检查' THEN '检查费' WHEN '药品费' THEN '药品费' WHEN '中草药' THEN '药品费' WHEN '附加' THEN '检查费' WHEN '化验' THEN '检查费' ELSE '其他' END TYPE,SM.MEDORDNAME NAME,CLE.UNITPRICE,CLE.AMOUNT,SUM(DDA.SELFMONEY) SELFMONEY,CASE WHEN DDA.INVOICEID LIKE 'k%' THEN '' ELSE DDA.INVOICEID END INVOICEID,DDA.INVOICEDATE,DDA.OPERATEDATE" +
            " FROM (SELECT N.CLINICRECIPEID,N.RECIPECONTENT,N.DIAGNOSEID,N.RECIPETYPE,N.UNITPRICE,N.RECIPESTATE,SUM(N.AMOUNT) AS AMOUNT FROM CLINICPHYSICIANRECIPE N WHERE N.DIAGNOSEID=@DIAGNOSEID GROUP BY N.RECIPETYPE,N.UNITPRICE,N.RECIPECONTENT,N.DIAGNOSEID,N.RECIPESTATE,N.CLINICRECIPEID) as CLE,S_MEDORD_MAIN SM,D_DETAIL_ACCOUNT DDA" +
            " WHERE CLE.RECIPECONTENT=SM.MEDORDID AND CLE.RECIPETYPE<>'附加'  AND CLE.RECIPESTATE=1 AND DDA.CLINICRECIPEID IS NOT NULL" +
            " AND CLE.CLINICRECIPEID=DDA.CLINICRECIPEID AND CLE.DIAGNOSEID=DDA.DIAGNOSEID AND CLE.RECIPECONTENT=DDA.UNITECODE  " +
            " AND CLE.DIAGNOSEID=@DIAGNOSEID" +
            " GROUP BY CLE.RECIPETYPE,SM.MEDORDNAME,CLE.UNITPRICE,CLE.AMOUNT,DDA.INVOICEID,DDA.INVOICEDATE,DDA.OPERATEDATE" +
            " ) as tempTable group by TYPE,NAME,UNITPRICE,AMOUNT,INVOICEID,INVOICEDATE,OPERATEDATE " +
            " UNION ALL" +
            " SELECT CASE CLE.RECIPETYPE WHEN '检查' THEN '检查费' WHEN '药品费' THEN '药品费' WHEN '中草药' THEN '药品费' WHEN '附加' THEN '检查费' WHEN '化验' THEN '检查费' ELSE '其他'  END TYPE,DS.CHARGEITEM NAME,CLE.UNITPRICE,CLE.AMOUNT,DDA.SELFMONEY,CASE WHEN DDA.INVOICEID LIKE 'k%' THEN '' ELSE DDA.INVOICEID END INVOICEID,DDA.INVOICEDATE,DDA.OPERATEDATE " +
            " FROM CLINICPHYSICIANRECIPE CLE,D_SUMMARY_INFO DS,D_DETAIL_ACCOUNT DDA" +
            " WHERE CLE.RECIPECONTENT=DS.ITEMID AND CLE.RECIPETYPE='附加' AND CLE.RECIPESTATE=1 AND DDA.CLINICRECIPEID IS NOT NULL" +
            " AND CLE.CLINICRECIPEID=DDA.CLINICRECIPEID AND CLE.DIAGNOSEID=DDA.DIAGNOSEID AND CLE.RECIPECONTENT=DDA.ITEMID  " +
            " AND CLE.DIAGNOSEID=@DIAGNOSEID" +
            " UNION ALL" +
            " SELECT DS.SUMMARY TYPE, DS.CHARGEITEM NAME,DS.UNITPRICE,DDA.AMOUNT,DDA.SELFMONEY,CASE WHEN DDA.INVOICEID LIKE 'k%' THEN '' ELSE DDA.INVOICEID END INVOICEID,DDA.INVOICEDATE,DDA.OPERATEDATE" +
            " FROM D_SUMMARY_INFO DS,D_DETAIL_ACCOUNT DDA" +
            " WHERE DDA.CLINICRECIPEID IS NULL AND DDA.ITEMID=DS.ITEMID AND DDA.ITEMID<>'01' AND DDA.ITEMID<>'02' AND DDA.ITEMID<>'03' AND DDA.ITEMID<>'04'" +
            " AND DDA.DIAGNOSEID =@DIAGNOSEID" +
            " UNION ALL" +
            " SELECT '药品费' TYPE,TC.CURRENCYNAME NAME ,DEM.UNITPRICE,DEM.AMOUNT,DEM.TOTALMONEY SELFMONEY,CASE WHEN DDA.INVOICEID LIKE 'k%' THEN '' ELSE DDA.INVOICEID END INVOICEID,DDA.INVOICEDATE,DDA.OPERATEDATE" +
            " FROM D_ECIPE_MEDICINE DEM,T_CODEX  TC,T_CODEX_DETAIL TCD,D_DETAIL_ACCOUNT DDA" +
            " WHERE DEM.LEECHDOMNO = TCD.LEECHDOMDETAILNO AND TC.LEECHDOMNO=TCD.LEECHDOMNO AND DEM.CANCELMARK=0 AND DEM.DIAGNOSEID=DDA.DIAGNOSEID AND DEM.DETAILACCOUNTID=DDA.DETAILACCOUNTID" +
            " AND DEM.DIAGNOSEID =@DIAGNOSEID" +
             " UNION ALL" +
            " SELECT DISTINCT '挂号费' as TYPE,A.REGISTERCLASS AS NAME,(a.CASHDEFRAY+a.ACCOUNTDEFRAY+a.DISCOUNTDEFRAY) as UNITPRICE, " +
            " 1.00 as AMOUNT,(a.CASHDEFRAY+a.ACCOUNTDEFRAY+a.DISCOUNTDEFRAY) as SELFMONEY,A.INVOICEID,A.INVOICEDATE, A.OPERATEDATE " +
            " from T_REGISTER_INFO A WHERE   a.CANCELMARK = 0 AND A.DIAGNOSEID=@DIAGNOSEID";
            Hashtable ht = new Hashtable();
            ht.Add("@DIAGNOSEID", SkyComm.DiagnoseID);
            ds = query.ExeQuery(sql, ht);





            //ds = detailFacade.GetAllDetailInfo(SkyComm.DiagnoseID);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gdcMain.DataSource = mChargeDetailInquirePresenter.getNewDetailInfo(ds).Tables[0];

//                gdcMain.DataSource = ds.Tables[0];
            }
            else
            {
                gdcMain.DataSource = null;
            }
        }

        private void lblOneWeek_Click(object sender, EventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label l = (Label)sender;
                int days = 0;
                if (l.Text.Contains("一周"))
                {
                    days = 7;
                }
                else if (l.Text.Contains("一月"))
                {
                    days = 30;
                }
                else
                {
                    days = 90;
                }
                DateTime dtOneWeek = new CommonFacade().GetServerDateTime().AddDays(-1 * days);

                DataTable dtcopy = ds.Tables[0].Clone();
                DataRow[] dr = ds.Tables[0].Select("OPERATEDATE>'" + dtOneWeek + "'");
                if (dr.Length > 0)
                {
                    foreach (DataRow item in dr)
                    {
                        dtcopy.ImportRow(item);
                    }

                    this.gdcMain.DataSource = dtcopy;
                }
                else
                {
                    this.gdcMain.DataSource = null;
                }
            }
        }
    }
}
