namespace AutoServiceManage.Inc
{
    partial class UcDoctorItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDoctorItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.arrangeSource = new System.Windows.Forms.Label();
            this.lblDoctorId = new System.Windows.Forms.Label();
            this.lblOffduty = new System.Windows.Forms.Label();
            this.lblDetailId = new System.Windows.Forms.Label();
            this.lblYh = new System.Windows.Forms.Label();
            this.lblWorkType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblDoctorName = new System.Windows.Forms.Label();
            this.ONDUTY = new System.Windows.Forms.Label();
            this.OFFDUTY = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.OFFDUTY);
            this.panel1.Controls.Add(this.ONDUTY);
            this.panel1.Controls.Add(this.arrangeSource);
            this.panel1.Controls.Add(this.lblDoctorId);
            this.panel1.Controls.Add(this.lblOffduty);
            this.panel1.Controls.Add(this.lblDetailId);
            this.panel1.Controls.Add(this.lblYh);
            this.panel1.Controls.Add(this.lblWorkType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblRole);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.lblDoctorName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 157);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // arrangeSource
            // 
            this.arrangeSource.AutoSize = true;
            this.arrangeSource.Location = new System.Drawing.Point(76, 107);
            this.arrangeSource.Name = "arrangeSource";
            this.arrangeSource.Size = new System.Drawing.Size(0, 12);
            this.arrangeSource.TabIndex = 12;
            this.arrangeSource.Visible = false;
            // 
            // lblDoctorId
            // 
            this.lblDoctorId.AutoSize = true;
            this.lblDoctorId.Location = new System.Drawing.Point(3, 139);
            this.lblDoctorId.Name = "lblDoctorId";
            this.lblDoctorId.Size = new System.Drawing.Size(11, 12);
            this.lblDoctorId.TabIndex = 11;
            this.lblDoctorId.Text = "l";
            this.lblDoctorId.Visible = false;
            // 
            // lblOffduty
            // 
            this.lblOffduty.AutoSize = true;
            this.lblOffduty.Location = new System.Drawing.Point(237, 139);
            this.lblOffduty.Name = "lblOffduty";
            this.lblOffduty.Size = new System.Drawing.Size(11, 12);
            this.lblOffduty.TabIndex = 10;
            this.lblOffduty.Text = "l";
            this.lblOffduty.Visible = false;
            // 
            // lblDetailId
            // 
            this.lblDetailId.AutoSize = true;
            this.lblDetailId.Location = new System.Drawing.Point(237, 139);
            this.lblDetailId.Name = "lblDetailId";
            this.lblDetailId.Size = new System.Drawing.Size(11, 12);
            this.lblDetailId.TabIndex = 10;
            this.lblDetailId.Text = "l";
            this.lblDetailId.Visible = false;
            // 
            // lblYh
            // 
            this.lblYh.AutoSize = true;
            this.lblYh.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYh.ForeColor = System.Drawing.Color.Red;
            this.lblYh.Location = new System.Drawing.Point(190, 43);
            this.lblYh.Name = "lblYh";
            this.lblYh.Size = new System.Drawing.Size(0, 20);
            this.lblYh.TabIndex = 9;
            this.lblYh.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblWorkType
            // 
            this.lblWorkType.AutoSize = true;
            this.lblWorkType.Font = new System.Drawing.Font("宋体", 10F);
            this.lblWorkType.Location = new System.Drawing.Point(176, 18);
            this.lblWorkType.Name = "lblWorkType";
            this.lblWorkType.Size = new System.Drawing.Size(0, 14);
            this.lblWorkType.TabIndex = 8;
            this.lblWorkType.Click += new System.EventHandler(this.panel1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "擅长";
            this.label1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(33, 50);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(0, 12);
            this.lblRole.TabIndex = 6;
            this.lblRole.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(10, 50);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(0, 12);
            this.lblSex.TabIndex = 5;
            this.lblSex.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblDoctorName
            // 
            this.lblDoctorName.AutoSize = true;
            this.lblDoctorName.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoctorName.Location = new System.Drawing.Point(7, 9);
            this.lblDoctorName.Name = "lblDoctorName";
            this.lblDoctorName.Size = new System.Drawing.Size(0, 27);
            this.lblDoctorName.TabIndex = 4;
            this.lblDoctorName.Click += new System.EventHandler(this.panel1_Click);
            // 
            // ONDUTY
            // 
            this.ONDUTY.AutoSize = true;
            this.ONDUTY.Location = new System.Drawing.Point(100, 97);
            this.ONDUTY.Name = "ONDUTY";
            this.ONDUTY.Size = new System.Drawing.Size(0, 12);
            this.ONDUTY.TabIndex = 13;
            this.ONDUTY.Visible = false;
            // 
            // OFFDUTY
            // 
            this.OFFDUTY.AutoSize = true;
            this.OFFDUTY.Location = new System.Drawing.Point(145, 97);
            this.OFFDUTY.Name = "OFFDUTY";
            this.OFFDUTY.Size = new System.Drawing.Size(0, 12);
            this.OFFDUTY.TabIndex = 14;
            this.OFFDUTY.Visible = false;
            // 
            // UcDoctorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "UcDoctorItem";
            this.Size = new System.Drawing.Size(251, 157);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblWorkType;
        public System.Windows.Forms.Label lblYh;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblRole;
        public System.Windows.Forms.Label lblSex;
        public System.Windows.Forms.Label lblDoctorName;
        public System.Windows.Forms.Label lblDetailId;
        public System.Windows.Forms.Label lblOffduty;
        public System.Windows.Forms.Label lblDoctorId;
        public System.Windows.Forms.Label arrangeSource;
        public System.Windows.Forms.Label OFFDUTY;
        public System.Windows.Forms.Label ONDUTY;

    }
}
