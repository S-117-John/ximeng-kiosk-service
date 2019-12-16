using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
using EntityData.His.CardClubManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inquire
{
    public partial class FrmStoredInquire : Form
    {
        public FrmStoredInquire()
        {
            InitializeComponent();
        }

        CardSavingData eHistoryListCardAuthorizationData = null;

        CardSavingFacade eCardSavingFacade = null;

        private void pcReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmStoredInquire_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;

            ucTime1.timer1.Start();

            eHistoryListCardAuthorizationData = new CardSavingData();

            eCardSavingFacade = new CardSavingFacade();

            this.eHistoryListCardAuthorizationData = (CardSavingData)eCardSavingFacade.FindCardMoneyByCardID(SkyComm.cardInfoStruct.CardNo);

            if (eHistoryListCardAuthorizationData.Tables["T_CARD_SAVING"].Rows.Count > 0)
            {
                this.gdcMain.DataSource = this.eHistoryListCardAuthorizationData.Tables["T_CARD_SAVING"];
            }
            else
            {
                this.gdcMain.DataSource = null;
            }
        }

        private void FrmStoredInquire_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        private void lblOneWeek_Click(object sender, EventArgs e)
        {
            if (eHistoryListCardAuthorizationData.Tables["T_CARD_SAVING"].Rows.Count > 0)
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

                DataTable dtcopy = eHistoryListCardAuthorizationData.Tables["T_CARD_SAVING"].Clone();
                DataRow[] dr = eHistoryListCardAuthorizationData.Tables["T_CARD_SAVING"].Select("ADDMONEYDATE>'" + dtOneWeek + "'");
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
