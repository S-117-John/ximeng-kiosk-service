namespace AutoServiceManage.InCard
{
    partial class FrmShowBrCode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.loading = new System.Windows.Forms.PictureBox();
            this.qrCodeImgControl1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.loading);
            this.panel1.Controls.Add(this.qrCodeImgControl1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 552);
            this.panel1.TabIndex = 1;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(0, 35);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1024, 79);
            this.ucHead1.TabIndex = 0;
            // 
            // loading
            // 
            this.loading.Image = global::AutoServiceManage.Properties.Resources.loading;
            this.loading.Location = new System.Drawing.Point(446, 177);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(128, 128);
            this.loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.loading.TabIndex = 3;
            this.loading.TabStop = false;
            // 
            // qrCodeImgControl1
            // 
            this.qrCodeImgControl1.InitialImage = null;
            this.qrCodeImgControl1.Location = new System.Drawing.Point(358, 117);
            this.qrCodeImgControl1.Name = "qrCodeImgControl1";
            this.qrCodeImgControl1.Size = new System.Drawing.Size(302, 265);
            this.qrCodeImgControl1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.qrCodeImgControl1.TabIndex = 7;
            this.qrCodeImgControl1.TabStop = false;
            this.qrCodeImgControl1.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Orange;
            this.label7.Location = new System.Drawing.Point(333, 416);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 27);
            this.label7.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(31, 514);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "操作时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(58, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(516, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "温馨提示：请扫描二维码查询健康卡信息";
            // 
            // label5
            // 
            this.label5.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.label5.Location = new System.Drawing.Point(883, 448);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 93);
            this.label5.TabIndex = 0;
            this.label5.Click += new System.EventHandler(this.label4_Click);
            // 
            // label4
            // 
            this.label4.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.label4.Location = new System.Drawing.Point(750, 448);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 93);
            this.label4.TabIndex = 0;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // FrmShowBrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmShowBrCode";
            this.Text = "FrmNetPay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNetPay_FormClosing);
            this.Load += new System.EventHandler(this.FrmNetPay_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).EndInit();
            this.ResumeLayout(false);
            this.Controls.Add(this.ucHead1);

        }

        #endregion

        private Inc.UCHead ucHead1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox loading;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox qrCodeImgControl1;
    }
}