namespace AutoServiceManage.CardSaving
{
    partial class FrmRechargeSuccessful
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRechargeSuccessful));
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRechargeMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblye = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            this.SuspendLayout();
            // 
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(880, 670);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(80, 80);
            this.pcExit.TabIndex = 87;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(240, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 96);
            this.label1.TabIndex = 90;
            // 
            // lblRechargeMoney
            // 
            this.lblRechargeMoney.AutoSize = true;
            this.lblRechargeMoney.BackColor = System.Drawing.Color.Transparent;
            this.lblRechargeMoney.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblRechargeMoney.ForeColor = System.Drawing.Color.White;
            this.lblRechargeMoney.Location = new System.Drawing.Point(347, 323);
            this.lblRechargeMoney.Name = "lblRechargeMoney";
            this.lblRechargeMoney.Size = new System.Drawing.Size(174, 24);
            this.lblRechargeMoney.TabIndex = 91;
            this.lblRechargeMoney.Text = "您已充值100元";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(614, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 24);
            this.label3.TabIndex = 92;
            this.label3.Text = "卡中余额：";
            // 
            // lblye
            // 
            this.lblye.AutoSize = true;
            this.lblye.BackColor = System.Drawing.Color.Transparent;
            this.lblye.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblye.ForeColor = System.Drawing.Color.Red;
            this.lblye.Location = new System.Drawing.Point(755, 323);
            this.lblye.Name = "lblye";
            this.lblye.Size = new System.Drawing.Size(74, 24);
            this.lblye.TabIndex = 93;
            this.lblye.Text = "100元";
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 89;
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 52;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 88;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Image = global::AutoServiceManage.Properties.Resources.取__号1_2x;
            this.label5.Location = new System.Drawing.Point(308, 452);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 59);
            this.label5.TabIndex = 94;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Image = global::AutoServiceManage.Properties.Resources.缴__费_2x;
            this.label6.Location = new System.Drawing.Point(571, 452);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 59);
            this.label6.TabIndex = 95;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // FrmRechargeSuccessful
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblye);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRechargeMoney);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucHead1);
            this.Controls.Add(this.ucTime1);
            this.Controls.Add(this.pcExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRechargeSuccessful";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRechargeSuccessful";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRechargeSuccessful_FormClosing);
            this.Load += new System.EventHandler(this.FrmRechargeSuccessful_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Inc.UcTime ucTime1;
        private System.Windows.Forms.PictureBox pcExit;
        private Inc.UCHead ucHead1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRechargeMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblye;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}