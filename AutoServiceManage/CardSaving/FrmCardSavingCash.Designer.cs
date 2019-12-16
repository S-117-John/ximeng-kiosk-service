namespace AutoServiceManage.CardSaving
{
    partial class FrmCardSavingCash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardSavingCash));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Label();
            this.lblNoPaymentCharge = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblwjf = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblye = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.backGroundPanelTrend1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackColor = System.Drawing.Color.Transparent;
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.pictureBox1);
            this.backGroundPanelTrend1.Controls.Add(this.lblTime);
            this.backGroundPanelTrend1.Controls.Add(this.lblMoney);
            this.backGroundPanelTrend1.Controls.Add(this.label7);
            this.backGroundPanelTrend1.Controls.Add(this.btnStart);
            this.backGroundPanelTrend1.Controls.Add(this.btnEnd);
            this.backGroundPanelTrend1.Controls.Add(this.lblNoPaymentCharge);
            this.backGroundPanelTrend1.Controls.Add(this.label15);
            this.backGroundPanelTrend1.Controls.Add(this.label16);
            this.backGroundPanelTrend1.Controls.Add(this.lblAmount);
            this.backGroundPanelTrend1.Controls.Add(this.label14);
            this.backGroundPanelTrend1.Controls.Add(this.label12);
            this.backGroundPanelTrend1.Controls.Add(this.lblwjf);
            this.backGroundPanelTrend1.Controls.Add(this.label11);
            this.backGroundPanelTrend1.Controls.Add(this.lblye);
            this.backGroundPanelTrend1.Controls.Add(this.label8);
            this.backGroundPanelTrend1.Controls.Add(this.lblPatientInfo);
            this.backGroundPanelTrend1.Controls.Add(this.label6);
            this.backGroundPanelTrend1.Controls.Add(this.label5);
            this.backGroundPanelTrend1.Controls.Add(this.label4);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.btnClose);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Margin = new System.Windows.Forms.Padding(4);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1365, 960);
            this.backGroundPanelTrend1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AutoServiceManage.Properties.Resources.纸币入口;
            this.pictureBox1.Location = new System.Drawing.Point(579, 412);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(341, 375);
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.Red;
            this.lblTime.Location = new System.Drawing.Point(939, 701);
            this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(40, 25);
            this.lblTime.TabIndex = 49;
            this.lblTime.Text = "60";
            // 
            // lblMoney
            // 
            this.lblMoney.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblMoney.ForeColor = System.Drawing.Color.Red;
            this.lblMoney.Location = new System.Drawing.Point(1157, 598);
            this.lblMoney.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(84, 25);
            this.lblMoney.TabIndex = 47;
            this.lblMoney.Text = "0";
            this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(1240, 526);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 25);
            this.label7.TabIndex = 46;
            this.label7.Text = "张";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::AutoServiceManage.Properties.Resources.开始现金预存;
            this.btnStart.Location = new System.Drawing.Point(975, 406);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(299, 75);
            this.btnStart.TabIndex = 45;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Image = ((System.Drawing.Image)(resources.GetObject("btnEnd.Image")));
            this.btnEnd.Location = new System.Drawing.Point(988, 678);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(299, 75);
            this.btnEnd.TabIndex = 44;
            this.btnEnd.Click += new System.EventHandler(this.lblCashStored_Click);
            // 
            // lblNoPaymentCharge
            // 
            this.lblNoPaymentCharge.Image = ((System.Drawing.Image)(resources.GetObject("lblNoPaymentCharge.Image")));
            this.lblNoPaymentCharge.Location = new System.Drawing.Point(1133, 152);
            this.lblNoPaymentCharge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoPaymentCharge.Name = "lblNoPaymentCharge";
            this.lblNoPaymentCharge.Size = new System.Drawing.Size(147, 44);
            this.lblNoPaymentCharge.TabIndex = 43;
            this.lblNoPaymentCharge.Click += new System.EventHandler(this.lblNoPaymentCharge_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(1240, 598);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 25);
            this.label15.TabIndex = 41;
            this.label15.Text = "元";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Yellow;
            this.label16.Location = new System.Drawing.Point(985, 595);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 30);
            this.label16.TabIndex = 40;
            this.label16.Text = "投入金额:";
            // 
            // lblAmount
            // 
            this.lblAmount.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(1157, 526);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(84, 25);
            this.lblAmount.TabIndex = 39;
            this.lblAmount.Text = "0";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Yellow;
            this.label14.Location = new System.Drawing.Point(985, 524);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(153, 30);
            this.label14.TabIndex = 38;
            this.label14.Text = "投入张数:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.label12.Location = new System.Drawing.Point(77, 429);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(449, 374);
            this.label12.TabIndex = 37;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // lblwjf
            // 
            this.lblwjf.AutoSize = true;
            this.lblwjf.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblwjf.ForeColor = System.Drawing.Color.Red;
            this.lblwjf.Location = new System.Drawing.Point(1131, 312);
            this.lblwjf.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblwjf.Name = "lblwjf";
            this.lblwjf.Size = new System.Drawing.Size(136, 25);
            this.lblwjf.TabIndex = 36;
            this.lblwjf.Text = "1262.00元";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(1027, 312);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 25);
            this.label11.TabIndex = 35;
            this.label11.Text = "未缴费：";
            // 
            // lblye
            // 
            this.lblye.AutoSize = true;
            this.lblye.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblye.ForeColor = System.Drawing.Color.Red;
            this.lblye.Location = new System.Drawing.Point(851, 312);
            this.lblye.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblye.Name = "lblye";
            this.lblye.Size = new System.Drawing.Size(136, 25);
            this.lblye.TabIndex = 34;
            this.lblye.Text = "1262.00元";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(747, 312);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 25);
            this.label8.TabIndex = 33;
            this.label8.Text = "卡余额：";
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.lblPatientInfo.ForeColor = System.Drawing.Color.White;
            this.lblPatientInfo.Location = new System.Drawing.Point(88, 310);
            this.lblPatientInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(523, 27);
            this.lblPatientInfo.TabIndex = 32;
            this.lblPatientInfo.Text = "姓名  男 身份证：612424198702072222";
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.Location = new System.Drawing.Point(60, 369);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1232, 29);
            this.label6.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(201, 249);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 25);
            this.label5.TabIndex = 30;
            this.label5.Text = "以下身份信息";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(139, 249);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 25);
            this.label4.TabIndex = 29;
            this.label4.Text = "确认";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(76, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "请您";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(833, 162);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "点击查看您的一卡通：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(57, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "*请您按照系统提示完成操作！";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(1173, 838);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(107, 100);
            this.btnExit.TabIndex = 25;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnClose.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnClose.Location = new System.Drawing.Point(1027, 838);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 100);
            this.btnClose.TabIndex = 24;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(13, 900);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 29;
            this.ucTime1.Size = new System.Drawing.Size(240, 38);
            this.ucTime1.TabIndex = 1;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(13, 38);
            this.ucHead1.Margin = new System.Windows.Forms.Padding(5);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1352, 116);
            this.ucHead1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer3
            // 
            this.timer3.Interval = 3000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // FrmCardSavingCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 960);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCardSavingCash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCardSavingCash";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardSavingCash_FormClosing);
            this.Load += new System.EventHandler(this.FrmCardSavingCash_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.UCHead ucHead1;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblye;
        private System.Windows.Forms.Label lblwjf;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblNoPaymentCharge;
        private System.Windows.Forms.Label btnEnd;
        private System.Windows.Forms.Label btnStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer3;
    }
}