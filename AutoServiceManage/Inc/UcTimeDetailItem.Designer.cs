namespace AutoServiceManage.Inc
{
    partial class UcTimeDetailItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTimeDetailItem));
            this.plTime = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.arranageDetailSource = new System.Windows.Forms.Label();
            this.plTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // plTime
            // 
            this.plTime.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("plTime.BackgroundImage")));
            this.plTime.Controls.Add(this.arranageDetailSource);
            this.plTime.Controls.Add(this.lblTime);
            this.plTime.Controls.Add(this.lblOrder);
            this.plTime.Location = new System.Drawing.Point(2, 3);
            this.plTime.Name = "plTime";
            this.plTime.Size = new System.Drawing.Size(153, 58);
            this.plTime.TabIndex = 0;
            this.plTime.Click += new System.EventHandler(this.plTime_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(63, 19);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(64, 20);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "08:00";
            this.lblTime.Click += new System.EventHandler(this.plTime_Click);
            // 
            // lblOrder
            // 
            this.lblOrder.AutoSize = true;
            this.lblOrder.BackColor = System.Drawing.Color.Transparent;
            this.lblOrder.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblOrder.ForeColor = System.Drawing.Color.Red;
            this.lblOrder.Location = new System.Drawing.Point(12, 19);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(20, 20);
            this.lblOrder.TabIndex = 0;
            this.lblOrder.Text = "1";
            this.lblOrder.Click += new System.EventHandler(this.plTime_Click);
            // 
            // arranageDetailSource
            // 
            this.arranageDetailSource.AutoSize = true;
            this.arranageDetailSource.Location = new System.Drawing.Point(65, 7);
            this.arranageDetailSource.Name = "arranageDetailSource";
            this.arranageDetailSource.Size = new System.Drawing.Size(0, 12);
            this.arranageDetailSource.TabIndex = 2;
            this.arranageDetailSource.Visible = false;
            // 
            // UcTimeDetailItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plTime);
            this.Name = "UcTimeDetailItem";
            this.Size = new System.Drawing.Size(157, 62);
            this.plTime.ResumeLayout(false);
            this.plTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plTime;
        public System.Windows.Forms.Label lblOrder;
        public System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label arranageDetailSource;
    }
}
