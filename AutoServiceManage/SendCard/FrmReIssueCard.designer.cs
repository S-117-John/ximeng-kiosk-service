namespace AutoServiceManage.SendCard
{
    partial class FrmReIssueCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReIssueCard));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.label7);
            this.backGroundPanelTrend1.Controls.Add(this.label6);
            this.backGroundPanelTrend1.Controls.Add(this.label5);
            this.backGroundPanelTrend1.Controls.Add(this.label12);
            this.backGroundPanelTrend1.Controls.Add(this.label8);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.lblPatientName);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 0;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 111);
            this.ucHead1.TabIndex = 117;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.Location = new System.Drawing.Point(48, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(924, 5);
            this.label7.TabIndex = 116;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Image = global::AutoServiceManage.Properties.Resources.二代身份证;
            this.label6.Location = new System.Drawing.Point(588, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(344, 266);
            this.label6.TabIndex = 106;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Image = global::AutoServiceManage.Properties.Resources.圆角矩形_13;
            this.label5.Location = new System.Drawing.Point(48, 366);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(487, 309);
            this.label5.TabIndex = 105;
            this.label5.Text = "等待读取...";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(47, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(745, 19);
            this.label12.TabIndex = 103;
            this.label12.Text = "2．\t补卡时可保留原卡片就诊和充值信息，但需要收取卡押金10元，需要现金充值，程序可支持50,100的纸币，";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(46, 301);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(258, 19);
            this.label8.TabIndex = 98;
            this.label8.Text = "4．\t请妥善保管好就诊卡，谢谢合作。";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(45, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(923, 19);
            this.label3.TabIndex = 93;
            this.label3.Text = "3．\t如果再办理的过程中，出现异常，请携带身份证去人工服务台进行窗口补办。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(71, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(369, 19);
            this.label2.TabIndex = 92;
            this.label2.Text = "剩余金额自动转为预交金账户，可在就诊过程中使用。";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientName.ForeColor = System.Drawing.Color.White;
            this.lblPatientName.Location = new System.Drawing.Point(48, 177);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(888, 19);
            this.lblPatientName.TabIndex = 91;
            this.lblPatientName.Text = "1．\t请将二代身份证，放置在身份证感应区，系统会自动读取身份证信息，并查找可补办的就诊卡信息（包含正常状态和就诊状态）。";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 26);
            this.label1.TabIndex = 67;
            this.label1.Text = "自助补卡-请放置二代身份证";
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 53;
            this.ucTime1.Size = new System.Drawing.Size(300, 30);
            this.ucTime1.TabIndex = 69;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(880, 670);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 90;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnReturn.Location = new System.Drawing.Point(770, 670);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 89;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // FrmReIssueCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReIssueCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助补卡";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReIssueCard_FormClosing);
            this.Load += new System.EventHandler(this.FrmReIssueCard_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label label1;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer1;
        private Inc.UCHead ucHead1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}