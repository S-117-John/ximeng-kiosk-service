using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Common;
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

namespace AutoServiceManage.Electronics
{
    public partial class FormScan : Form
    {

        public int sec { get; set; }

        public FormScan()
        {
            InitializeComponent();
            timer2.Start();
            sec = 30;
        }
        CardAuthorizationFacade theCardAuthorizationFacade = new CardAuthorizationFacade();
        private QuerySolutionFacade query = new QuerySolutionFacade();
        private void timer1_Tick(object sender, EventArgs e)
        {
            string eCardNo = string.Empty;
            if (!string.IsNullOrEmpty(textBox_ecard.Text.ToString()))
            {
                eCardNo = textBox_ecard.Text.ToString().Split(':')[0].ToString();
                string sql1 = "select * from t_card_authorization where VICECARDID = @VICECARDID and CIRCUIT_STATE = 0";
                Hashtable hashtable = new Hashtable();

                hashtable.Add("@VICECARDID", eCardNo);
                DataSet dataSet = query.ExeQuery(sql1, hashtable);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string cardNo = dataSet.Tables[0].Rows[0]["CARDID"].ToString();
                    SkyComm.cardInfoStruct.CardNo = cardNo;
                    SkyComm.DiagnoseID = dataSet.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    SkyComm.cardBlance = DecimalRound.Round(theCardAuthorizationFacade.FindCardBalance(SkyComm.DiagnoseID), 2);
                    SkyComm.eCardAuthorizationData = (CardAuthorizationData)theCardAuthorizationFacade.SelectPatientAndCardInfoByCardID(SkyComm.cardInfoStruct.CardNo);
                    this.DialogResult = DialogResult.OK;
                }
            }
           
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SystemFramework.Voice.Voice voice = new SystemFramework.Voice.Voice();
            voice.PlayText("请在扫码口扫描您的电子健康卡二维码");
            voice.EndJtts();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (sec == 0)
                {
                    ReadCardClose();
                }
                sec = sec - 1;
                this.btnClose.Text = "返回(" + sec.ToString() + ")";
            }
            catch (Exception ex)
            {
                timer1.Stop();
                SkyComm.ShowMessageInfo(ex.Message);

            }
        }

        private void ReadCardClose()
        {
            this.timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormScan_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            this.textBox_ecard.Focus();
            this.textBox_ecard.SelectAll();

            timer1.Start();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
