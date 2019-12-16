namespace SunRise.HOSP.CLIENT
{
    partial class FrmTestCamera
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
            this.pbCapture = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCameraSelBottom = new System.Windows.Forms.RadioButton();
            this.rbCameraSelTop = new System.Windows.Forms.RadioButton();
            this.btnClose = new HOSP.ExControl.TransButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCapture
            // 
            this.pbCapture.Location = new System.Drawing.Point(133, 188);
            this.pbCapture.Name = "pbCapture";
            this.pbCapture.Size = new System.Drawing.Size(763, 568);
            this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCapture.TabIndex = 94;
            this.pbCapture.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbCameraSelBottom);
            this.groupBox1.Controls.Add(this.rbCameraSelTop);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.Location = new System.Drawing.Point(133, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 76);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "摄像头位置";
            // 
            // rbCameraSelBottom
            // 
            this.rbCameraSelBottom.AutoSize = true;
            this.rbCameraSelBottom.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.rbCameraSelBottom.Location = new System.Drawing.Point(582, 29);
            this.rbCameraSelBottom.Name = "rbCameraSelBottom";
            this.rbCameraSelBottom.Size = new System.Drawing.Size(49, 29);
            this.rbCameraSelBottom.TabIndex = 101;
            this.rbCameraSelBottom.Text = "下";
            this.rbCameraSelBottom.UseVisualStyleBackColor = true;
            this.rbCameraSelBottom.Click += new System.EventHandler(this.rbCameraSelBottom_Click);
            // 
            // rbCameraSelTop
            // 
            this.rbCameraSelTop.AutoSize = true;
            this.rbCameraSelTop.Checked = true;
            this.rbCameraSelTop.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.rbCameraSelTop.Location = new System.Drawing.Point(271, 29);
            this.rbCameraSelTop.Name = "rbCameraSelTop";
            this.rbCameraSelTop.Size = new System.Drawing.Size(49, 29);
            this.rbCameraSelTop.TabIndex = 101;
            this.rbCameraSelTop.TabStop = true;
            this.rbCameraSelTop.Text = "上";
            this.rbCameraSelTop.UseVisualStyleBackColor = true;
            this.rbCameraSelTop.Click += new System.EventHandler(this.rbCameraSelTop_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnClose.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnClose.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnClose.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnClose.FadingSpeed = 20;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.ImgDisable = null;
            this.btnClose.ImgDisablePath = null;
            this.btnClose.ImgEnter = null;
            this.btnClose.ImgEnterPath = null;
            this.btnClose.ImgNormal = null;
            this.btnClose.ImgNormalPath = null;
            this.btnClose.ImgPress = null;
            this.btnClose.ImgPressPath = null;
            this.btnClose.Location = new System.Drawing.Point(902, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressFontSize = 12;
            this.btnClose.Radius = 20;
            this.btnClose.ShowGradient = false;
            this.btnClose.ShowText = true;
            this.btnClose.Size = new System.Drawing.Size(113, 45);
            this.btnClose.SplitDraw = true;
            this.btnClose.Stretch = false;
            this.btnClose.TabIndex = 106;
            this.btnClose.Text = "退出";
            this.btnClose.UseDefaultImg = true;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(70, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 27);
            this.label1.TabIndex = 102;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(356, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 27);
            this.label2.TabIndex = 102;
            this.label2.Text = "label1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(20, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(724, 67);
            this.listBox1.TabIndex = 107;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox2.Location = new System.Drawing.Point(133, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(763, 100);
            this.groupBox2.TabIndex = 108;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已装摄像头";
            // 
            // FrmTestCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbCapture);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTestCamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraReplay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTestCamera_FormClosing);
            this.Load += new System.EventHandler(this.FrmTestCamera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCapture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCameraSelBottom;
        private System.Windows.Forms.RadioButton rbCameraSelTop;
        private HOSP.ExControl.TransButton btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}