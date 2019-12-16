namespace AutoServiceManage.Inc
{
    partial class UcCheckBox
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
            this.btnCheckBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheckBox
            // 
            this.btnCheckBox.BackgroundImage = global::AutoServiceManage.Properties.Resources.CheckBoxFalse;
            this.btnCheckBox.FlatAppearance.BorderSize = 0;
            this.btnCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckBox.Location = new System.Drawing.Point(4, 4);
            this.btnCheckBox.Name = "btnCheckBox";
            this.btnCheckBox.Size = new System.Drawing.Size(29, 29);
            this.btnCheckBox.TabIndex = 0;
            this.btnCheckBox.UseVisualStyleBackColor = true;
            this.btnCheckBox.Click += new System.EventHandler(this.btnCheckBox_Click);
            // 
            // UcCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnCheckBox);
            this.Name = "UcCheckBox";
            this.Size = new System.Drawing.Size(40, 40);
            this.Click += new System.EventHandler(this.UcCheckBox_Click);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnCheckBox;

    }
}
