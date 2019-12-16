using AutoServiceManage.Inc;
namespace AutoServiceManage.Charge
{
    partial class FrmChooseReserve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChooseReserve));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.labelYYGQ = new System.Windows.Forms.Label();
            this.lblXD = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblQXL = new System.Windows.Forms.Label();
            this.lblTXJH = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.btnReturn = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUltrasonic = new System.Windows.Forms.Label();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.labelYYGQ);
            this.backGroundPanelTrend1.Controls.Add(this.lblXD);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Controls.Add(this.lblQXL);
            this.backGroundPanelTrend1.Controls.Add(this.lblTXJH);
            this.backGroundPanelTrend1.Controls.Add(this.label4);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.lblUltrasonic);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 51;
            // 
            // labelYYGQ
            // 
            this.labelYYGQ.BackColor = System.Drawing.Color.Transparent;
            this.labelYYGQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelYYGQ.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelYYGQ.ForeColor = System.Drawing.Color.Transparent;
            this.labelYYGQ.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.labelYYGQ.Location = new System.Drawing.Point(842, 116);
            this.labelYYGQ.Name = "labelYYGQ";
            this.labelYYGQ.Size = new System.Drawing.Size(141, 51);
            this.labelYYGQ.TabIndex = 73;
            this.labelYYGQ.Text = "预约改签";
            this.labelYYGQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelYYGQ.Click += new System.EventHandler(this.labelYYGQ_Click);
            // 
            // lblXD
            // 
            this.lblXD.BackColor = System.Drawing.Color.Transparent;
            this.lblXD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblXD.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblXD.ForeColor = System.Drawing.Color.Transparent;
            this.lblXD.Image = ((System.Drawing.Image)(resources.GetObject("lblXD.Image")));
            this.lblXD.Location = new System.Drawing.Point(69, 343);
            this.lblXD.Name = "lblXD";
            this.lblXD.Size = new System.Drawing.Size(274, 131);
            this.lblXD.TabIndex = 72;
            this.lblXD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblXD.Click += new System.EventHandler(this.lblXD_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(277, 545);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(548, 54);
            this.label3.TabIndex = 71;
            this.label3.Text = "2.脐血流预约包含妊高症预约。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblQXL
            // 
            this.lblQXL.BackColor = System.Drawing.Color.Transparent;
            this.lblQXL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblQXL.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQXL.ForeColor = System.Drawing.Color.Transparent;
            this.lblQXL.Image = ((System.Drawing.Image)(resources.GetObject("lblQXL.Image")));
            this.lblQXL.Location = new System.Drawing.Point(675, 185);
            this.lblQXL.Name = "lblQXL";
            this.lblQXL.Size = new System.Drawing.Size(274, 131);
            this.lblQXL.TabIndex = 70;
            this.lblQXL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblQXL.Click += new System.EventHandler(this.lblQXL_Click);
            // 
            // lblTXJH
            // 
            this.lblTXJH.BackColor = System.Drawing.Color.Transparent;
            this.lblTXJH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTXJH.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTXJH.ForeColor = System.Drawing.Color.Transparent;
            this.lblTXJH.Image = ((System.Drawing.Image)(resources.GetObject("lblTXJH.Image")));
            this.lblTXJH.Location = new System.Drawing.Point(362, 185);
            this.lblTXJH.Name = "lblTXJH";
            this.lblTXJH.Size = new System.Drawing.Size(274, 131);
            this.lblTXJH.TabIndex = 69;
            this.lblTXJH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTXJH.Click += new System.EventHandler(this.lblTXJH_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(277, 505);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(548, 54);
            this.label4.TabIndex = 68;
            this.label4.Text = "1.医技预约最多可预约【60】天以内数据，感谢您的使用。";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(176, 520);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 67;
            this.label1.Text = "温馨提示：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 48;
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
            this.btnReturn.TabIndex = 22;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
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
            this.btnExit.TabIndex = 23;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 31;
            this.ucTime1.Size = new System.Drawing.Size(207, 32);
            this.ucTime1.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(40, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "请选择预约类型";
            // 
            // lblUltrasonic
            // 
            this.lblUltrasonic.BackColor = System.Drawing.Color.Transparent;
            this.lblUltrasonic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUltrasonic.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUltrasonic.ForeColor = System.Drawing.Color.Transparent;
            this.lblUltrasonic.Image = ((System.Drawing.Image)(resources.GetObject("lblUltrasonic.Image")));
            this.lblUltrasonic.Location = new System.Drawing.Point(69, 185);
            this.lblUltrasonic.Name = "lblUltrasonic";
            this.lblUltrasonic.Size = new System.Drawing.Size(274, 131);
            this.lblUltrasonic.TabIndex = 20;
            this.lblUltrasonic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUltrasonic.Click += new System.EventHandler(this.lblCashStored_Click);
            // 
            // FrmChooseReserve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmChooseReserve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmChooseReserve";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChooseReserve_FormClosing);
            this.Load += new System.EventHandler(this.FrmChooseReserve_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUltrasonic;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private Inc.UCHead ucHead1;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label label2;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQXL;
        private System.Windows.Forms.Label lblTXJH;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblXD;
        private System.Windows.Forms.Label labelYYGQ;
    }
}