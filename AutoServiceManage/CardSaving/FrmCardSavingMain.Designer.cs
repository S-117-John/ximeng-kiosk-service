using AutoServiceManage.Inc;
namespace AutoServiceManage.CardSaving
{
    partial class FrmCardSavingMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardSavingMain));
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.btnApliPay = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.label2 = new System.Windows.Forms.Label();
            this.btnWxPay = new System.Windows.Forms.Label();
            this.lblCashStored = new System.Windows.Forms.Label();
            this.lblBankCardStored = new System.Windows.Forms.Label();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backGroundPanelTrend1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backGroundPanelTrend1.BackgroundImage")));
            this.backGroundPanelTrend1.Controls.Add(this.panel1);
            this.backGroundPanelTrend1.Controls.Add(this.newHead1);
            this.backGroundPanelTrend1.Controls.Add(this.btnApliPay);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.btnWxPay);
            this.backGroundPanelTrend1.Controls.Add(this.lblCashStored);
            this.backGroundPanelTrend1.Controls.Add(this.lblBankCardStored);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 51;
            // 
            // btnApliPay
            // 
            this.btnApliPay.BackColor = System.Drawing.Color.Transparent;
            this.btnApliPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApliPay.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnApliPay.ForeColor = System.Drawing.Color.Transparent;
            this.btnApliPay.Image = ((System.Drawing.Image)(resources.GetObject("btnApliPay.Image")));
            this.btnApliPay.Location = new System.Drawing.Point(708, 293);
            this.btnApliPay.Name = "btnApliPay";
            this.btnApliPay.Size = new System.Drawing.Size(274, 179);
            this.btnApliPay.TabIndex = 20;
            this.btnApliPay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnApliPay.Click += new System.EventHandler(this.btnApliPay_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.Location = new System.Drawing.Point(724, 11);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 22;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(865, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 23;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(27, 35);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 44;
            this.ucTime1.Size = new System.Drawing.Size(207, 32);
            this.ucTime1.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(40, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "请选择预存类型";
            // 
            // btnWxPay
            // 
            this.btnWxPay.BackColor = System.Drawing.Color.Transparent;
            this.btnWxPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWxPay.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWxPay.ForeColor = System.Drawing.Color.Transparent;
            this.btnWxPay.Image = ((System.Drawing.Image)(resources.GetObject("btnWxPay.Image")));
            this.btnWxPay.Location = new System.Drawing.Point(399, 293);
            this.btnWxPay.Name = "btnWxPay";
            this.btnWxPay.Size = new System.Drawing.Size(274, 179);
            this.btnWxPay.TabIndex = 20;
            this.btnWxPay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnWxPay.Click += new System.EventHandler(this.btnWxPay_Click);
            // 
            // lblCashStored
            // 
            this.lblCashStored.BackColor = System.Drawing.Color.Transparent;
            this.lblCashStored.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCashStored.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCashStored.ForeColor = System.Drawing.Color.Transparent;
            this.lblCashStored.Image = ((System.Drawing.Image)(resources.GetObject("lblCashStored.Image")));
            this.lblCashStored.Location = new System.Drawing.Point(79, 293);
            this.lblCashStored.Name = "lblCashStored";
            this.lblCashStored.Size = new System.Drawing.Size(277, 179);
            this.lblCashStored.TabIndex = 20;
            this.lblCashStored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCashStored.Click += new System.EventHandler(this.lblCashStored_Click);
            // 
            // lblBankCardStored
            // 
            this.lblBankCardStored.BackColor = System.Drawing.Color.Transparent;
            this.lblBankCardStored.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBankCardStored.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBankCardStored.ForeColor = System.Drawing.Color.Transparent;
            this.lblBankCardStored.Image = ((System.Drawing.Image)(resources.GetObject("lblBankCardStored.Image")));
            this.lblBankCardStored.Location = new System.Drawing.Point(63, 531);
            this.lblBankCardStored.Name = "lblBankCardStored";
            this.lblBankCardStored.Size = new System.Drawing.Size(274, 131);
            this.lblBankCardStored.TabIndex = 21;
            this.lblBankCardStored.Text = "银行卡预存";
            this.lblBankCardStored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBankCardStored.Visible = false;
            this.lblBankCardStored.Click += new System.EventHandler(this.label1_Click);
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(0, 0);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1024, 100);
            this.newHead1.TabIndex = 51;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(0, 668);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 100);
            this.panel1.TabIndex = 52;
            // 
            // FrmCardSavingMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCardSavingMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCardSavingMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardSavingMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardSavingMain_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCashStored;
        private System.Windows.Forms.Label lblBankCardStored;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label label2;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label btnWxPay;
        private System.Windows.Forms.Label btnApliPay;
        private System.Windows.Forms.Panel panel1;
        private NewHead newHead1;
    }
}