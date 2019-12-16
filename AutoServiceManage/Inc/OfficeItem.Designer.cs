namespace AutoServiceManage.Inc
{
    partial class OfficeItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfficeItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOfficeId = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.lblOfficeId);
            this.panel1.Controls.Add(this.lblOffice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 75);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblOfficeId
            // 
            this.lblOfficeId.AutoSize = true;
            this.lblOfficeId.BackColor = System.Drawing.Color.Transparent;
            this.lblOfficeId.ForeColor = System.Drawing.Color.Transparent;
            this.lblOfficeId.Location = new System.Drawing.Point(4, 52);
            this.lblOfficeId.Name = "lblOfficeId";
            this.lblOfficeId.Size = new System.Drawing.Size(17, 12);
            this.lblOfficeId.TabIndex = 1;
            this.lblOfficeId.Text = "lo";
            this.lblOfficeId.Visible = false;
            // 
            // lblOffice
            // 
            this.lblOffice.BackColor = System.Drawing.Color.Transparent;
            this.lblOffice.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblOffice.ForeColor = System.Drawing.Color.White;
            this.lblOffice.Location = new System.Drawing.Point(3, 17);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(134, 35);
            this.lblOffice.TabIndex = 0;
            this.lblOffice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOffice.Click += new System.EventHandler(this.panel1_Click);
            // 
            // OfficeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "OfficeItem";
            this.Size = new System.Drawing.Size(140, 75);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblOffice;
        public System.Windows.Forms.Label lblOfficeId;
    }
}
