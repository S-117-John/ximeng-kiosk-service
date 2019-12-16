namespace AutoServiceManage.Charge
{
    partial class FrmReserveConfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReserveConfirm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.lblOfficeDddress = new System.Windows.Forms.Label();
            this.pcReturn = new System.Windows.Forms.PictureBox();
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.lblOk = new System.Windows.Forms.Label();
            this.lblExOffice = new System.Windows.Forms.Label();
            this.lblReserveItem = new System.Windows.Forms.Label();
            this.lblReserveDate = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblRemark = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(42, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "确认预约信息";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.ForeColor = System.Drawing.Color.White;
            this.lblPatientName.Location = new System.Drawing.Point(236, 194);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(82, 23);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "姓名：";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.BackColor = System.Drawing.Color.Transparent;
            this.lblSex.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblSex.ForeColor = System.Drawing.Color.White;
            this.lblSex.Location = new System.Drawing.Point(237, 243);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(82, 23);
            this.lblSex.TabIndex = 2;
            this.lblSex.Text = "性别：";
            // 
            // lblOffice
            // 
            this.lblOffice.AutoSize = true;
            this.lblOffice.BackColor = System.Drawing.Color.Transparent;
            this.lblOffice.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblOffice.ForeColor = System.Drawing.Color.White;
            this.lblOffice.Location = new System.Drawing.Point(191, 292);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(130, 23);
            this.lblOffice.TabIndex = 3;
            this.lblOffice.Text = "开单科室：";
            // 
            // lblOfficeDddress
            // 
            this.lblOfficeDddress.AutoSize = true;
            this.lblOfficeDddress.BackColor = System.Drawing.Color.Transparent;
            this.lblOfficeDddress.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblOfficeDddress.ForeColor = System.Drawing.Color.White;
            this.lblOfficeDddress.Location = new System.Drawing.Point(189, 586);
            this.lblOfficeDddress.Name = "lblOfficeDddress";
            this.lblOfficeDddress.Size = new System.Drawing.Size(130, 23);
            this.lblOfficeDddress.TabIndex = 9;
            this.lblOfficeDddress.Text = "科室位置：";
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
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(880, 670);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(80, 80);
            this.pcExit.TabIndex = 83;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // lblOk
            // 
            this.lblOk.BackColor = System.Drawing.Color.Transparent;
            this.lblOk.Image = ((System.Drawing.Image)(resources.GetObject("lblOk.Image")));
            this.lblOk.Location = new System.Drawing.Point(399, 642);
            this.lblOk.Name = "lblOk";
            this.lblOk.Size = new System.Drawing.Size(220, 61);
            this.lblOk.TabIndex = 86;
            this.lblOk.Click += new System.EventHandler(this.lblOk_Click);
            // 
            // lblExOffice
            // 
            this.lblExOffice.AutoSize = true;
            this.lblExOffice.BackColor = System.Drawing.Color.Transparent;
            this.lblExOffice.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblExOffice.ForeColor = System.Drawing.Color.White;
            this.lblExOffice.Location = new System.Drawing.Point(188, 341);
            this.lblExOffice.Name = "lblExOffice";
            this.lblExOffice.Size = new System.Drawing.Size(130, 23);
            this.lblExOffice.TabIndex = 87;
            this.lblExOffice.Text = "执行科室：";
            // 
            // lblReserveItem
            // 
            this.lblReserveItem.AutoSize = true;
            this.lblReserveItem.BackColor = System.Drawing.Color.Transparent;
            this.lblReserveItem.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblReserveItem.ForeColor = System.Drawing.Color.White;
            this.lblReserveItem.Location = new System.Drawing.Point(188, 390);
            this.lblReserveItem.Name = "lblReserveItem";
            this.lblReserveItem.Size = new System.Drawing.Size(130, 23);
            this.lblReserveItem.TabIndex = 88;
            this.lblReserveItem.Text = "预约项目：";
            // 
            // lblReserveDate
            // 
            this.lblReserveDate.AutoSize = true;
            this.lblReserveDate.BackColor = System.Drawing.Color.Transparent;
            this.lblReserveDate.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblReserveDate.ForeColor = System.Drawing.Color.White;
            this.lblReserveDate.Location = new System.Drawing.Point(188, 439);
            this.lblReserveDate.Name = "lblReserveDate";
            this.lblReserveDate.Size = new System.Drawing.Size(130, 23);
            this.lblReserveDate.TabIndex = 89;
            this.lblReserveDate.Text = "预约时间：";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.BackColor = System.Drawing.Color.Transparent;
            this.lblGroup.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblGroup.ForeColor = System.Drawing.Color.White;
            this.lblGroup.Location = new System.Drawing.Point(189, 488);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(130, 23);
            this.lblGroup.TabIndex = 90;
            this.lblGroup.Text = "预约分组：";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.BackColor = System.Drawing.Color.Transparent;
            this.lblMoney.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold);
            this.lblMoney.ForeColor = System.Drawing.Color.White;
            this.lblMoney.Location = new System.Drawing.Point(236, 537);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(82, 23);
            this.lblMoney.TabIndex = 91;
            this.lblMoney.Text = "金额：";
            // 
            // lblRemark
            // 
            this.lblRemark.BackColor = System.Drawing.Color.Transparent;
            this.lblRemark.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold);
            this.lblRemark.ForeColor = System.Drawing.Color.White;
            this.lblRemark.Location = new System.Drawing.Point(720, 505);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(130, 23);
            this.lblRemark.TabIndex = 92;
            this.lblRemark.Text = "注意事项：";
            this.lblRemark.Click += new System.EventHandler(this.lblRemark_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(720, 532);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 93;
            this.label2.Text = "1.预约时扣费";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(720, 553);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 16);
            this.label3.TabIndex = 94;
            this.label3.Text = "2.如需变更预约请去导医台变更";
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 48;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 85;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 84;
            // 
            // FrmReserveConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblReserveDate);
            this.Controls.Add(this.lblReserveItem);
            this.Controls.Add(this.lblExOffice);
            this.Controls.Add(this.lblOk);
            this.Controls.Add(this.ucTime1);
            this.Controls.Add(this.ucHead1);
            this.Controls.Add(this.pcExit);
            this.Controls.Add(this.pcReturn);
            this.Controls.Add(this.lblOfficeDddress);
            this.Controls.Add(this.lblOffice);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReserveConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReserveConfirm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReserveConfirm_FormClosing);
            this.Load += new System.EventHandler(this.FrmReserveConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.Label lblOfficeDddress;
        private System.Windows.Forms.PictureBox pcReturn;
        private System.Windows.Forms.PictureBox pcExit;
        private Inc.UCHead ucHead1;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label lblOk;
        private System.Windows.Forms.Label lblExOffice;
        private System.Windows.Forms.Label lblReserveItem;
        private System.Windows.Forms.Label lblReserveDate;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}