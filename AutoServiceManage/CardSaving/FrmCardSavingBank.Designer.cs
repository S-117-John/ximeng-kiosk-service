using AutoServiceManage.Inc;
namespace AutoServiceManage.CardSaving
{
    partial class FrmCardSavingBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardSavingBank));
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.lblBankStored = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNoPaymentCharge = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblwjf = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.lblye = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.lblBankStored);
            this.backGroundPanelTrend1.Controls.Add(this.label12);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.lblNoPaymentCharge);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Controls.Add(this.lblwjf);
            this.backGroundPanelTrend1.Controls.Add(this.label4);
            this.backGroundPanelTrend1.Controls.Add(this.label11);
            this.backGroundPanelTrend1.Controls.Add(this.label5);
            this.backGroundPanelTrend1.Controls.Add(this.label6);
            this.backGroundPanelTrend1.Controls.Add(this.lblPatientInfo);
            this.backGroundPanelTrend1.Controls.Add(this.lblye);
            this.backGroundPanelTrend1.Controls.Add(this.label8);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 1;
            // 
            // lblBankStored
            // 
            this.lblBankStored.BackColor = System.Drawing.Color.Transparent;
            this.lblBankStored.Image = ((System.Drawing.Image)(resources.GetObject("lblBankStored.Image")));
            this.lblBankStored.Location = new System.Drawing.Point(755, 439);
            this.lblBankStored.Name = "lblBankStored";
            this.lblBankStored.Size = new System.Drawing.Size(224, 60);
            this.lblBankStored.TabIndex = 73;
            this.lblBankStored.Click += new System.EventHandler(this.lblBankStored_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.label12.Location = new System.Drawing.Point(44, 349);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(362, 234);
            this.label12.TabIndex = 67;
            this.label12.Text = "操作步骤：\r\n\r\n1. 请仔细核对上述基本信息，无误后开始\r\n   点击银行卡预存。\r\n\r\n2. 输入金额后，根据提示刷卡，并在卡机\r\n   上输入密码。\r\n\r\n" +
    "3. 若您持有的银行卡为IC卡，请等待交易\r\n   提示成功后再将银行卡拔出。\r\n\r\n4. 自助终端不设置退费功能，如需退费请\r\n   到窗口办理。";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(888, 674);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 66;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnReturn.Location = new System.Drawing.Point(778, 674);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 65;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(18, 724);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 50;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 72;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(43, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 20);
            this.label1.TabIndex = 45;
            this.label1.Text = "*请您按照系统提示完成操作！";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(629, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 20);
            this.label2.TabIndex = 46;
            this.label2.Text = "点击查看您的一卡通：";
            // 
            // lblNoPaymentCharge
            // 
            this.lblNoPaymentCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblNoPaymentCharge.Image = ((System.Drawing.Image)(resources.GetObject("lblNoPaymentCharge.Image")));
            this.lblNoPaymentCharge.Location = new System.Drawing.Point(859, 122);
            this.lblNoPaymentCharge.Name = "lblNoPaymentCharge";
            this.lblNoPaymentCharge.Size = new System.Drawing.Size(110, 35);
            this.lblNoPaymentCharge.TabIndex = 62;
            this.lblNoPaymentCharge.Click += new System.EventHandler(this.lblNoPaymentCharge_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(57, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "请您";
            // 
            // lblwjf
            // 
            this.lblwjf.AutoSize = true;
            this.lblwjf.BackColor = System.Drawing.Color.Transparent;
            this.lblwjf.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblwjf.ForeColor = System.Drawing.Color.Red;
            this.lblwjf.Location = new System.Drawing.Point(850, 248);
            this.lblwjf.Name = "lblwjf";
            this.lblwjf.Size = new System.Drawing.Size(107, 20);
            this.lblwjf.TabIndex = 55;
            this.lblwjf.Text = "1262.00元";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(102, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 48;
            this.label4.Text = "确认";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(765, 248);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 54;
            this.label11.Text = "未缴费：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(150, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 20);
            this.label5.TabIndex = 49;
            this.label5.Text = "以下身份信息";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(45, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(924, 23);
            this.label6.TabIndex = 50;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.lblPatientInfo.ForeColor = System.Drawing.Color.White;
            this.lblPatientInfo.Location = new System.Drawing.Point(66, 248);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(423, 22);
            this.lblPatientInfo.TabIndex = 51;
            this.lblPatientInfo.Text = "姓名  男 身份证：612424198702072222";
            // 
            // lblye
            // 
            this.lblye.AutoSize = true;
            this.lblye.BackColor = System.Drawing.Color.Transparent;
            this.lblye.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblye.ForeColor = System.Drawing.Color.Red;
            this.lblye.Location = new System.Drawing.Point(629, 250);
            this.lblye.Name = "lblye";
            this.lblye.Size = new System.Drawing.Size(107, 20);
            this.lblye.TabIndex = 53;
            this.lblye.Text = "1262.00元";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(530, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 20);
            this.label8.TabIndex = 52;
            this.label8.Text = "卡余额：";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // FrmCardSavingBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCardSavingBank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCardSavingBank";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardSavingBank_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardSavingBank_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNoPaymentCharge;
        private System.Windows.Forms.Label lblwjf;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblye;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private UCHead ucHead1;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label lblBankStored;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private UcTime ucTime1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}