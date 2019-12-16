namespace AutoServiceSDK.POSInterface.POS003.Dialogs
{
    partial class FrmReadCard
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
            this.lblCancel = new System.Windows.Forms.Label();
            this.pbxReadCard = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxReadCard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCancel
            // 
            this.lblCancel.BackColor = System.Drawing.Color.Transparent;
            this.lblCancel.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCancel.Image = global::AutoServiceSDK.Properties.Resources.圆角矩形_13;
            this.lblCancel.Location = new System.Drawing.Point(384, 347);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(135, 48);
            this.lblCancel.TabIndex = 5;
            this.lblCancel.Text = "取消";
            this.lblCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancel.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // pbxReadCard
            // 
            this.pbxReadCard.Image = global::AutoServiceSDK.Properties.Resources.chaka5;
            this.pbxReadCard.Location = new System.Drawing.Point(12, 64);
            this.pbxReadCard.Name = "pbxReadCard";
            this.pbxReadCard.Size = new System.Drawing.Size(507, 280);
            this.pbxReadCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxReadCard.TabIndex = 6;
            this.pbxReadCard.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Transparent;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(128, 16);
            this.lblTitle.TabIndex = 37;
            this.lblTitle.Text = "请插入银行卡...";
            // 
            // FrmReadCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoServiceSDK.Properties.Resources.圆角矩形_13_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(531, 404);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbxReadCard);
            this.Controls.Add(this.lblCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReadCard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmReadCard";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmEnterPassword_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbxReadCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.PictureBox pbxReadCard;
        private System.Windows.Forms.Label lblTitle;
    }
}