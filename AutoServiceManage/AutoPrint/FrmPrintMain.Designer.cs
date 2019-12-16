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
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
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
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1365, 960);
            this.backGroundPanelTrend1.TabIndex = 0;
            // 
            // txtGeneticReport
            // 
            this.txtGeneticReport.BackColor = System.Drawing.Color.Transparent;
            this.txtGeneticReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtGeneticReport.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGeneticReport.ForeColor = System.Drawing.Color.Transparent;
            this.txtGeneticReport.Image = global::AutoServiceManage.Properties.Resources.lblEnablePicture;
            this.txtGeneticReport.Location = new System.Drawing.Point(913, 609);
            this.txtGeneticReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtGeneticReport.Name = "txtGeneticReport";
            this.txtGeneticReport.Size = new System.Drawing.Size(365, 164);
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
            this.btnInhosCostListPrint.Location = new System.Drawing.Point(84, 361);
            this.btnInhosCostListPrint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnInhosCostListPrint.Name = "btnInhosCostListPrint";
            this.btnInhosCostListPrint.Size = new System.Drawing.Size(365, 164);
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
            this.btnClinicCostListPrint.Location = new System.Drawing.Point(84, 609);
            this.btnClinicCostListPrint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnClinicCostListPrint.Name = "btnClinicCostListPrint";
            this.btnClinicCostListPrint.Size = new System.Drawing.Size(365, 164);
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
            this.lblnl.ForeColor = System.Drawing.Color.White;
            this.lblnl.Location = new System.Drawing.Point(429, 244);
            this.lblnl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnl.Name = "lblnl";
            this.lblnl.Size = new System.Drawing.Size(93, 38);
            this.lblnl.TabIndex = 80;
            this.lblnl.Text = "42";
            // 
            // lblxb
            // 
            this.lblxb.BackColor = System.Drawing.Color.Transparent;
            this.lblxb.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblxb.ForeColor = System.Drawing.Color.White;
            this.lblxb.Location = new System.Drawing.Point(368, 244);
            this.lblxb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblxb.Name = "lblxb";
            this.lblxb.Size = new System.Drawing.Size(53, 38);
            this.lblxb.TabIndex = 79;
            this.lblxb.Text = "男";
            // 
            // lblxm
            // 
            this.lblxm.BackColor = System.Drawing.Color.Transparent;
            this.lblxm.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblxm.ForeColor = System.Drawing.Color.White;
            this.lblxm.Location = new System.Drawing.Point(181, 244);
            this.lblxm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblxm.Name = "lblxm";
            this.lblxm.Size = new System.Drawing.Size(145, 38);
            this.lblxm.TabIndex = 78;
            this.lblxm.Text = "自助测试";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(45, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 31);
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
            this.lblClinicEmrPrint.Location = new System.Drawing.Point(913, 361);
            this.lblClinicEmrPrint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClinicEmrPrint.Name = "lblClinicEmrPrint";
            this.lblClinicEmrPrint.Size = new System.Drawing.Size(365, 164);
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
            this.txtOutHos.Location = new System.Drawing.Point(499, 609);
            this.txtOutHos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtOutHos.Name = "txtOutHos";
            this.txtOutHos.Size = new System.Drawing.Size(365, 164);
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
            this.lblLisReportPrint.Location = new System.Drawing.Point(499, 361);
            this.lblLisReportPrint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLisReportPrint.Name = "lblLisReportPrint";
            this.lblLisReportPrint.Size = new System.Drawing.Size(365, 164);
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
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(1173, 838);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(107, 100);
            this.btnExit.TabIndex = 17;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnReturn.Location = new System.Drawing.Point(1027, 838);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(107, 100);
            this.btnReturn.TabIndex = 16;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(13, 900);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 37;
            this.ucTime1.Size = new System.Drawing.Size(240, 38);
            this.ucTime1.TabIndex = 2;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(13, 38);
            this.ucHead1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1352, 116);
            this.ucHead1.TabIndex = 1;
            // 
            // FrmPrintMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 960);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmPrintMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrintMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrintMain_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.UCHead ucHead1;
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
    }
}