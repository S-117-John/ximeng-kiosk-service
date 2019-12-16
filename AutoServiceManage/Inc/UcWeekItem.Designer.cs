namespace AutoServiceManage.Inc
{
    partial class UcWeekItem
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
            this.lblToday = new System.Windows.Forms.Label();
            this.lblTodayDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.BackColor = System.Drawing.Color.Transparent;
            this.lblToday.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblToday.ForeColor = System.Drawing.Color.White;
            this.lblToday.Location = new System.Drawing.Point(10, 7);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(42, 16);
            this.lblToday.TabIndex = 1;
            this.lblToday.Text = "今天";
            this.lblToday.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblTodayDate
            // 
            this.lblTodayDate.AutoSize = true;
            this.lblTodayDate.BackColor = System.Drawing.Color.Transparent;
            this.lblTodayDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblTodayDate.ForeColor = System.Drawing.Color.White;
            this.lblTodayDate.Location = new System.Drawing.Point(4, 32);
            this.lblTodayDate.Name = "lblTodayDate";
            this.lblTodayDate.Size = new System.Drawing.Size(53, 16);
            this.lblTodayDate.TabIndex = 2;
            this.lblTodayDate.Text = "11-08";
            this.lblTodayDate.Click += new System.EventHandler(this.panel1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblTodayDate);
            this.panel1.Controls.Add(this.lblToday);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(61, 61);
            this.panel1.TabIndex = 3;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // UcWeekItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Name = "UcWeekItem";
            this.Size = new System.Drawing.Size(61, 61);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblToday;
        public System.Windows.Forms.Label lblTodayDate;
    }
}
