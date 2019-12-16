namespace AutoServiceManage.InCard
{
    partial class FrmReadType
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
            this.picCard = new System.Windows.Forms.PictureBox();
            this.identityCard = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.textBox_ecard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.identityCard)).BeginInit();
            this.SuspendLayout();
            // 
            // picCard
            // 
            this.picCard.BackgroundImage = global::AutoServiceManage.Properties.Resources.jzk;
            this.picCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picCard.Location = new System.Drawing.Point(56, 141);
            this.picCard.Margin = new System.Windows.Forms.Padding(4);
            this.picCard.Name = "picCard";
            this.picCard.Size = new System.Drawing.Size(380, 224);
            this.picCard.TabIndex = 0;
            this.picCard.TabStop = false;
            this.picCard.Click += new System.EventHandler(this.picCard_Click);
            // 
            // identityCard
            // 
            this.identityCard.BackgroundImage = global::AutoServiceManage.Properties.Resources.sfz;
            this.identityCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.identityCard.Location = new System.Drawing.Point(89, 392);
            this.identityCard.Margin = new System.Windows.Forms.Padding(4);
            this.identityCard.Name = "identityCard";
            this.identityCard.Size = new System.Drawing.Size(102, 53);
            this.identityCard.TabIndex = 1;
            this.identityCard.TabStop = false;
            this.identityCard.Visible = false;
            this.identityCard.Click += new System.EventHandler(this.identityCard_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnClose.Location = new System.Drawing.Point(712, 418);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(177, 55);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "返回(30)";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // textBox_ecard
            // 
            this.textBox_ecard.Location = new System.Drawing.Point(89, 87);
            this.textBox_ecard.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_ecard.Name = "textBox_ecard";
            this.textBox_ecard.Size = new System.Drawing.Size(10, 25);
            this.textBox_ecard.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(507, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 224);
            this.label1.TabIndex = 48;
            this.label1.Text = "点击扫描您自己的电子健康卡二维码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork_1);
            // 
            // FrmReadType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AutoServiceManage.Properties.Resources.bg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(955, 505);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_ecard);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.identityCard);
            this.Controls.Add(this.picCard);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmReadType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmReadType";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.Load += new System.EventHandler(this.FrmReadType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.identityCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCard;
        private System.Windows.Forms.PictureBox identityCard;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox textBox_ecard;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}