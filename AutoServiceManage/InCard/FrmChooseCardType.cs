using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SdkService;
using AutoServiceManage.Common;

namespace AutoServiceManage.InCard
{
    public partial class FrmChooseCardType : Form
    {
        public FrmChooseCardType()
        {
            InitializeComponent();
        }

        public CardType? cardType = null;

        public  enum CardType
        {
            Entitycard = 1,//实体卡
            Virtualcard = 2　//虚拟卡　
        }
        public string strMsg = string.Empty;
        private void FrmChooseSendCardType_Load(object sender, EventArgs e)
        {
            ucTime1.Sec = 60;
            ucTime1.timer1.Start();
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }

        private void FrmChooseSendCardType_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucTime1.timer1.Stop();
        }

        private void lblCard_Click(object sender, EventArgs e)
        {
            cardType = CardType.Entitycard;
       
            this.DialogResult = DialogResult.Cancel;         
        }

        private void lblCardDz_Click(object sender, EventArgs e)
        {
            cardType = CardType.Virtualcard;
            FrmShowBrCode FrmShowBrCode = new FrmShowBrCode();
            if (FrmShowBrCode.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                strMsg = FrmShowBrCode.strMsg;
                this.DialogResult = DialogResult.OK;
            }

          
        }


    }
}
