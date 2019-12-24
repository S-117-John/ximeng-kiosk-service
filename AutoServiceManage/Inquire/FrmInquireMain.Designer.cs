namespace AutoServiceManage.Inquire
{
    partial class FrmInquireMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInquireMain));
            this.lblStoredInquire = new System.Windows.Forms.Label();
            this.lblChargeDetailInquire = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStoredInquire
            // 
            this.lblStoredInquire.BackColor = System.Drawing.Color.Transparent;
            this.lblStoredInquire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblStoredInquire.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStoredInquire.ForeColor = System.Drawing.Color.Transparent;
            this.lblStoredInquire.Image = ((System.Drawing.Image)(resources.GetObject("lblStoredInquire.Image")));
            this.lblStoredInquire.Location = new System.Drawing.Point(158, 293);
            this.lblStoredInquire.Name = "lblStoredInquire";
            this.lblStoredInquire.Size = new System.Drawing.Size(347, 210);
            this.lblStoredInquire.TabIndex = 22;
            this.lblStoredInquire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStoredInquire.Click += new System.EventHandler(this.lblStoredInquire_Click);
            // 
            // lblChargeDetailInquire
            // 
            this.lblChargeDetailInquire.BackColor = System.Drawing.Color.Transparent;
            this.lblChargeDetailInquire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChargeDetailInquire.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChargeDetailInquire.ForeColor = System.Drawing.Color.Transparent;
            this.lblChargeDetailInquire.Image = ((System.Drawing.Image)(resources.GetObject("lblChargeDetailInquire.Image")));
            this.lblChargeDetailInquire.Location = new System.Drawing.Point(565, 293);
            this.lblChargeDetailInquire.Name = "lblChargeDetailInquire";
            this.lblChargeDetailInquire.Size = new System.Drawing.Size(362, 210);
            this.lblChargeDetailInquire.TabIndex = 23;
            this.lblChargeDetailInquire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChargeDetailInquire.Click += new System.EventHandler(this.lblChargeDetailInquire_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.Location = new System.Drawing.Point(710, 9);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 24;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(846, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 25;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(40, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 24);
            this.label2.TabIndex = 51;
            this.label2.Text = "请选择查询类型";
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(43, 33);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 52;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 2;
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(1, 0);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1024, 100);
            this.newHead1.TabIndex = 52;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(1, 670);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 95);
            this.panel1.TabIndex = 53;
            // 
            // FrmInquireMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.newHead1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblStoredInquire);
            this.Controls.Add(this.lblChargeDetailInquire);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmInquireMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInquireMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInquireMain_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label lblStoredInquire;
        private System.Windows.Forms.Label lblChargeDetailInquire;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label label2;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel1;
    }
}