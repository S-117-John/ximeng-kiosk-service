namespace AutoServiceManage.BespeakRegister
{
    partial class FrmTimeDetailChoose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimeDetailChoose));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.lblDoctorRole = new System.Windows.Forms.Label();
            this.pcReturn = new System.Windows.Forms.PictureBox();
            this.ucTimeDetailList1 = new AutoServiceManage.Inc.UcTimeDetailList();
            this.backGroundPanelTrend1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.lblUserName);
            this.backGroundPanelTrend1.Controls.Add(this.pcExit);
            this.backGroundPanelTrend1.Controls.Add(this.lblDoctorRole);
            this.backGroundPanelTrend1.Controls.Add(this.pcReturn);
            this.backGroundPanelTrend1.Controls.Add(this.ucTimeDetailList1);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 85;
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 57;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 86;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 85;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(56, 131);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 24);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "姓名";
            // 
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(880, 670);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(80, 80);
            this.pcExit.TabIndex = 84;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // lblDoctorRole
            // 
            this.lblDoctorRole.AutoSize = true;
            this.lblDoctorRole.BackColor = System.Drawing.Color.Transparent;
            this.lblDoctorRole.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lblDoctorRole.ForeColor = System.Drawing.Color.White;
            this.lblDoctorRole.Location = new System.Drawing.Point(149, 127);
            this.lblDoctorRole.Name = "lblDoctorRole";
            this.lblDoctorRole.Size = new System.Drawing.Size(0, 14);
            this.lblDoctorRole.TabIndex = 1;
            // 
            // pcReturn
            // 
            this.pcReturn.BackColor = System.Drawing.Color.Transparent;
            this.pcReturn.Image = ((System.Drawing.Image)(resources.GetObject("pcReturn.Image")));
            this.pcReturn.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcReturn.InitialImage")));
            this.pcReturn.Location = new System.Drawing.Point(770, 670);
            this.pcReturn.Name = "pcReturn";
            this.pcReturn.Size = new System.Drawing.Size(80, 80);
            this.pcReturn.TabIndex = 82;
            this.pcReturn.TabStop = false;
            this.pcReturn.Click += new System.EventHandler(this.pcReturn_Click);
            // 
            // ucTimeDetailList1
            // 
            this.ucTimeDetailList1.BackColor = System.Drawing.Color.Transparent;
            this.ucTimeDetailList1.Location = new System.Drawing.Point(45, 192);
            this.ucTimeDetailList1.Name = "ucTimeDetailList1";
            this.ucTimeDetailList1.Size = new System.Drawing.Size(956, 459);
            this.ucTimeDetailList1.TabIndex = 2;
            // 
            // FrmTimeDetailChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTimeDetailChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTimeDetailChoose";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTimeDetailChoose_FormClosing);
            this.Load += new System.EventHandler(this.FrmTimeDetailChoose_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblDoctorRole;
        private Inc.UcTimeDetailList ucTimeDetailList1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pcReturn;
        private System.Windows.Forms.PictureBox pcExit;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.UCHead ucHead1;
        private Inc.UcTime ucTime1;
    }
}