namespace AutoServiceManage.AutoPrint
{
    partial class FrmPrintMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintMain));
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.txtGeneticReport = new System.Windows.Forms.Label();
            this.btnInhosCostListPrint = new System.Windows.Forms.Label();
            this.btnClinicCostListPrint = new System.Windows.Forms.Label();
            this.lblnl = new System.Windows.Forms.Label();
            this.lblxb = new System.Windows.Forms.Label();
            this.lblxm = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblClinicEmrPrint = new System.Windows.Forms.Label();
            this.txtOutHos = new System.Windows.Forms.Label();
            this.lblLisReportPrint = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
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
            this.backGroundPanelTrend1.Controls.Add(this.txtGeneticReport);
            this.backGroundPanelTrend1.Controls.Add(this.btnInhosCostListPrint);
            this.backGroundPanelTrend1.Controls.Add(this.btnClinicCostListPrint);
            this.backGroundPanelTrend1.Controls.Add(this.lblnl);
            this.backGroundPanelTrend1.Controls.Add(this.lblxb);
            this.backGroundPanelTrend1.Controls.Add(this.lblxm);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.lblClinicEmrPrint);
            this.backGroundPanelTrend1.Controls.Add(this.txtOutHos);
            this.backGroundPanelTrend1.Controls.Add(this.lblLisReportPrint);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 0;
            // 
            // txtGeneticReport
            // 
            this.txtGeneticReport.BackColor = System.Drawing.Color.Transparent;
            this.txtGeneticReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtGeneticReport.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGeneticReport.ForeColor = System.Drawing.Color.Transparent;
            this.txtGeneticReport.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.txtGeneticReport.Location = new System.Drawing.Point(685, 487);
            this.txtGeneticReport.Name = "txtGeneticReport";
            this.txtGeneticReport.Size = new System.Drawing.Size(274, 131);
            this.txtGeneticReport.TabIndex = 83;
            this.txtGeneticReport.Text = "遗传报告打印";
            this.txtGeneticReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtGeneticReport.Visible = false;
            this.txtGeneticReport.Click += new System.EventHandler(this.txtGeneticReport_Click);
            // 
            // btnInhosCostListPrint
            // 
            this.btnInhosCostListPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnInhosCostListPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInhosCostListPrint.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInhosCostListPrint.ForeColor = System.Drawing.Color.Transparent;
            this.btnInhosCostListPrint.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.btnInhosCostListPrint.Location = new System.Drawing.Point(63, 289);
            this.btnInhosCostListPrint.Name = "btnInhosCostListPrint";
            this.btnInhosCostListPrint.Size = new System.Drawing.Size(274, 131);
            this.btnInhosCostListPrint.TabIndex = 82;
            this.btnInhosCostListPrint.Text = "住院费用清单打印";
            this.btnInhosCostListPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInhosCostListPrint.Visible = false;
            this.btnInhosCostListPrint.Click += new System.EventHandler(this.btnInhosCostListPrint_Click);
            // 
            // btnClinicCostListPrint
            // 
            this.btnClinicCostListPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnClinicCostListPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClinicCostListPrint.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClinicCostListPrint.ForeColor = System.Drawing.Color.Transparent;
            this.btnClinicCostListPrint.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.btnClinicCostListPrint.Location = new System.Drawing.Point(63, 487);
            this.btnClinicCostListPrint.Name = "btnClinicCostListPrint";
            this.btnClinicCostListPrint.Size = new System.Drawing.Size(274, 131);
            this.btnClinicCostListPrint.TabIndex = 81;
            this.btnClinicCostListPrint.Text = "门诊费用清单打印";
            this.btnClinicCostListPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClinicCostListPrint.Visible = false;
            this.btnClinicCostListPrint.Click += new System.EventHandler(this.btnClinicCostListPrint_Click);
            // 
            // lblnl
            // 
            this.lblnl.BackColor = System.Drawing.Color.Transparent;
            this.lblnl.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblnl.ForeColor = System.Drawing.Color.Black;
            this.lblnl.Location = new System.Drawing.Point(322, 195);
            this.lblnl.Name = "lblnl";
            this.lblnl.Size = new System.Drawing.Size(70, 30);
            this.lblnl.TabIndex = 80;
            this.lblnl.Text = "42";
            // 
            // lblxb
            // 
            this.lblxb.BackColor = System.Drawing.Color.Transparent;
            this.lblxb.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblxb.ForeColor = System.Drawing.Color.Black;
            this.lblxb.Location = new System.Drawing.Point(276, 195);
            this.lblxb.Name = "lblxb";
            this.lblxb.Size = new System.Drawing.Size(40, 30);
            this.lblxb.TabIndex = 79;
            this.lblxb.Text = "男";
            // 
            // lblxm
            // 
            this.lblxm.BackColor = System.Drawing.Color.Transparent;
            this.lblxm.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblxm.ForeColor = System.Drawing.Color.Black;
            this.lblxm.Location = new System.Drawing.Point(136, 195);
            this.lblxm.Name = "lblxm";
            this.lblxm.Size = new System.Drawing.Size(109, 30);
            this.lblxm.TabIndex = 78;
            this.lblxm.Text = "自助测试";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(34, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 52;
            this.label1.Text = "自助打印";
            // 
            // lblClinicEmrPrint
            // 
            this.lblClinicEmrPrint.BackColor = System.Drawing.Color.Transparent;
            this.lblClinicEmrPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblClinicEmrPrint.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblClinicEmrPrint.ForeColor = System.Drawing.Color.Transparent;
            this.lblClinicEmrPrint.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.lblClinicEmrPrint.Location = new System.Drawing.Point(685, 289);
            this.lblClinicEmrPrint.Name = "lblClinicEmrPrint";
            this.lblClinicEmrPrint.Size = new System.Drawing.Size(274, 131);
            this.lblClinicEmrPrint.TabIndex = 22;
            this.lblClinicEmrPrint.Text = "门诊病历打印";
            this.lblClinicEmrPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClinicEmrPrint.Visible = false;
            this.lblClinicEmrPrint.Click += new System.EventHandler(this.lblClinicEmrPrint_Click);
            // 
            // txtOutHos
            // 
            this.txtOutHos.BackColor = System.Drawing.Color.Transparent;
            this.txtOutHos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtOutHos.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutHos.ForeColor = System.Drawing.Color.Transparent;
            this.txtOutHos.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.txtOutHos.Location = new System.Drawing.Point(374, 487);
            this.txtOutHos.Name = "txtOutHos";
            this.txtOutHos.Size = new System.Drawing.Size(274, 131);
            this.txtOutHos.TabIndex = 23;
            this.txtOutHos.Text = "出院病历打印";
            this.txtOutHos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtOutHos.Visible = false;
            this.txtOutHos.Click += new System.EventHandler(this.txtOutHos_Click);
            // 
            // lblLisReportPrint
            // 
            this.lblLisReportPrint.BackColor = System.Drawing.Color.Transparent;
            this.lblLisReportPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLisReportPrint.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLisReportPrint.ForeColor = System.Drawing.Color.Transparent;
            this.lblLisReportPrint.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.lblLisReportPrint.Location = new System.Drawing.Point(374, 289);
            this.lblLisReportPrint.Name = "lblLisReportPrint";
            this.lblLisReportPrint.Size = new System.Drawing.Size(274, 131);
            this.lblLisReportPrint.TabIndex = 23;
            this.lblLisReportPrint.Text = "检验报告打印";
            this.lblLisReportPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLisReportPrint.Click += new System.EventHandler(this.lblLisReportPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(879, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 17;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.Location = new System.Drawing.Point(697, 14);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 16;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(28, 40);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 36;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 2;
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(3, 3);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1024, 100);
            this.newHead1.TabIndex = 84;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(0, 665);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 100);
            this.panel1.TabIndex = 85;
            // 
            // FrmPrintMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPrintMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrintMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrintMain_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label lblClinicEmrPrint;
        private System.Windows.Forms.Label lblLisReportPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblnl;
        private System.Windows.Forms.Label lblxb;
        private System.Windows.Forms.Label lblxm;
        private System.Windows.Forms.Label btnInhosCostListPrint;
        private System.Windows.Forms.Label btnClinicCostListPrint;
        private System.Windows.Forms.Label txtGeneticReport;
        private System.Windows.Forms.Label txtOutHos;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel1;
    }
}