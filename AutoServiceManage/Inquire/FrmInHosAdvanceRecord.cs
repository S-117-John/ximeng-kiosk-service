using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using BusinessFacade.His.Inpatient;
using EntityData.His.CardClubManager;
using Skynet.Framework.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inquire
{
    public partial class FrmInHosAdvanceRecord : Form
    {
        #region 自定义变量
        string _inHosID = string.Empty;//住院号
        string _diagnoseID = string.Empty;//诊疗号
        string _inHosOfficeID = string.Empty;
        DataSet InHosData = new DataSet();//住院病人信息

        DataSet dsAdvance = new DataSet();
        #endregion
        public FrmInHosAdvanceRecord()
        {
            InitializeComponent();
        }


        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmInHosAdvanceRecord_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;

            ucTime1.timer1.Start();

            if (BindPage())
            {
                dsAdvance = this.GetAdvanceRecord();
                if (dsAdvance != null && dsAdvance.Tables.Count > 0)
                {
                    this.gdcMain.DataSource = this.dsAdvance.Tables[0];
                }
                else
                {
                    this.gdcMain.DataSource = null;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void FrmInHosAdvanceRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        private void lblOneWeek_Click(object sender, EventArgs e)
        {
            if (dsAdvance!=null && dsAdvance.Tables.Count>0 && dsAdvance.Tables[0].Rows.Count > 0)
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

                DataTable dtcopy = dsAdvance.Tables[0].Clone();
                DataRow[] dr = dsAdvance.Tables[0].Select("OPERATEDATE>'" + dtOneWeek + "'");
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
        #region 数据查询方法
        private DataSet GetAdvanceRecord()
        {
            try
            {
                string sql = " SELECT H_ADVANCE_RECORD.*,H_CHARGE_TYPE.CHARGETYPE AS PAYMODE, T_PATIENT_INFO.PATIENTNAME ,T_OPERATOR.OPERATORNAME" +
              " FROM H_ADVANCE_RECORD,H_CHARGE_TYPE ,H_INHOS_RECORD,T_PATIENT_INFO,T_OPERATOR" +
              " WHERE H_ADVANCE_RECORD.PAYMODEID = H_CHARGE_TYPE.CHARGETYPEID " +
              " AND H_ADVANCE_RECORD.INHOSID = H_INHOS_RECORD.INHOSID " +
              " AND H_INHOS_RECORD.DIAGNOSEID = T_PATIENT_INFO.DIAGNOSEID " +
              " AND H_ADVANCE_RECORD.OPERATORID = T_OPERATOR.OPERATORID " +
              " AND H_ADVANCE_RECORD.INHOSID = @INHOSID " +
              " ORDER BY OPERATEDATE";

                Hashtable htm = new Hashtable();
                htm.Add("@INHOSID", _inHosID);
                QuerySolutionFacade facadem = new QuerySolutionFacade();
                DataSet dsAdvance = facadem.ExeQuery(sql, htm);
                return dsAdvance;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("调用住院预交金充值记录查询失败，原因：" + ex.Message);
                return null;
            }
        }
        private bool BindPage()
        {
            try
            {
                InHosRecordFacade theInHosRecordFacade = new InHosRecordFacade();
                InHosData = theInHosRecordFacade.FindInfoByDiagnoseID(SkyComm.DiagnoseID);
                _diagnoseID = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                if (InHosData != null && InHosData.Tables.Count != 0 && InHosData.Tables[0].Rows.Count != 0)
                {
                    DataRow drInHos = InHosData.Tables[0].Rows[0];

                    _inHosID = drInHos["INHOSID"].ToString();
                    return true;
                }
                else
                {
                    SkyComm.ShowMessageInfo("未找到您的住院信息，点击关闭后返回!");
                    return false;
                }

            }
            catch (Exception ex)
            {
                SkyComm.ShowMessageInfo("未找到您的住院信息，点击关闭后返回!");

                return false;
            }
        }
        #endregion
    }
}
