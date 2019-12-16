namespace AutoServiceManage.SendCard
{
    partial class FrmCardSavingMode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardSavingMode));
            this.label1 = new System.Windows.Forms.Label();
            this.lblBankCardStored = new System.Windows.Forms.Label();
            this.lblCashStored = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 27);
            this.label1.TabIndex = 47;
            this.label1.Text = "请选择办卡预存方式";
            // 
            // lblBankCardStored
            // 
            this.lblBankCardStored.BackColor = System.Drawing.Color.Transparent;
            this.lblBankCardStored.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBankCardStored.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBankCardStored.ForeColor = System.Drawing.Color.Transparent;
            this.lblBankCardStored.Image = ((System.Drawing.Image)(resources.GetObject("lblBankCardStored.Image")));
            this.lblBankCardStored.Location = new System.Drawing.Point(136, 256);
            this.lblBankCardStored.Name = "lblBankCardStored";
            this.lblBankCardStored.Size = new System.Drawing.Size(222, 102);
            this.lblBankCardStored.TabIndex = 51;
            this.lblBankCardStored.Text = "银行卡预存";
            this.lblBankCardStored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashStored
            // 
            this.lblCashStored.BackColor = System.Drawing.Color.Transparent;
            this.lblCashStored.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCashStored.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCashStored.ForeColor = System.Drawing.Color.Transparent;
            this.lblCashStored.Image = ((System.Drawing.Image)(resources.GetObject("lblCashStored.Image")));
            this.lblCashStored.Location = new System.Drawing.Point(136, 110);
            this.lblCashStored.Name = "lblCashStored";
            this.lblCashStored.Size = new System.Drawing.Size(222, 102);
            this.lblCashStored.TabIndex = 52;
            this.lblCashStored.Text = "现金预存";
            this.lblCashStored.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmCardSavingMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = global::AutoServiceManage.Properties.Resources.圆角矩形_13_2;
            this.ClientSize = new System.Drawing.Size(531, 403);
            this.Controls.Add(this.lblCashStored);
            this.Controls.Add(this.lblBankCardStored);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCardSavingMode";
            this.ShowInTaskbar = false;
            this.Text = "办卡预存方式";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBankCardStored;
        private System.Windows.Forms.Label lblCashStored;
    }
}