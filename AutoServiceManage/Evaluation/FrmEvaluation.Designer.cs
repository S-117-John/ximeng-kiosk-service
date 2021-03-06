﻿namespace AutoServiceManage.Evaluation
{
    partial class FrmEvaluation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEvaluation));
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.backGroundPanelTrend2 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.lblNext = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOK = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.panel2 = new System.Windows.Forms.Panel();
            this.backGroundPanelTrend1.SuspendLayout();
            this.backGroundPanelTrend2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.backGroundPanelTrend2);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 1;
            // 
            // backGroundPanelTrend2
            // 
            this.backGroundPanelTrend2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backGroundPanelTrend2.BackgroundImage")));
            this.backGroundPanelTrend2.Controls.Add(this.panel2);
            this.backGroundPanelTrend2.Controls.Add(this.newHead1);
            this.backGroundPanelTrend2.Controls.Add(this.lblNext);
            this.backGroundPanelTrend2.Controls.Add(this.lblLast);
            this.backGroundPanelTrend2.Controls.Add(this.panel1);
            this.backGroundPanelTrend2.Controls.Add(this.lblOK);
            this.backGroundPanelTrend2.Controls.Add(this.label4);
            this.backGroundPanelTrend2.Controls.Add(this.lblPatient);
            this.backGroundPanelTrend2.Controls.Add(this.pictureBox1);
            this.backGroundPanelTrend2.Controls.Add(this.label1);
            this.backGroundPanelTrend2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend2.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend2.Name = "backGroundPanelTrend2";
            this.backGroundPanelTrend2.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend2.TabIndex = 1;
            // 
            // lblNext
            // 
            this.lblNext.BackColor = System.Drawing.Color.Transparent;
            this.lblNext.Font = new System.Drawing.Font("宋体", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNext.ForeColor = System.Drawing.Color.White;
            this.lblNext.Location = new System.Drawing.Point(879, 370);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(73, 113);
            this.lblNext.TabIndex = 93;
            this.lblNext.Text = ">";
            this.lblNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNext.Click += new System.EventHandler(this.lblNext_Click);
            // 
            // lblLast
            // 
            this.lblLast.BackColor = System.Drawing.Color.Transparent;
            this.lblLast.Font = new System.Drawing.Font("宋体", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLast.ForeColor = System.Drawing.Color.White;
            this.lblLast.Location = new System.Drawing.Point(70, 370);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(73, 113);
            this.lblLast.TabIndex = 92;
            this.lblLast.Text = "<";
            this.lblLast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLast.Click += new System.EventHandler(this.lblLast_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(149, 283);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(724, 279);
            this.panel1.TabIndex = 91;
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblOK.Image = ((System.Drawing.Image)(resources.GetObject("lblOK.Image")));
            this.lblOK.Location = new System.Drawing.Point(431, 575);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(142, 55);
            this.lblOK.TabIndex = 90;
            this.lblOK.Text = "提  交";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(106, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(800, 60);
            this.label4.TabIndex = 89;
            this.label4.Text = "    为了更好的满足患者的就医体验，我院积极响应国家医疗卫生系统提出的“三好一满意”政策的号召，对我院患者进行满意度调查，在此花费您的宝贵时间，征求您的意见。具" +
    "体调查如下：";
            // 
            // lblPatient
            // 
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatient.ForeColor = System.Drawing.Color.Black;
            this.lblPatient.Location = new System.Drawing.Point(127, 176);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(202, 29);
            this.lblPatient.TabIndex = 88;
            this.lblPatient.Text = "西安天网女士：";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(102, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(33, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 26);
            this.label1.TabIndex = 57;
            this.label1.Text = "患者满意度调查";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(869, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 56;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.Location = new System.Drawing.Point(733, 11);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 55;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(21, 38);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 7;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 54;
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(0, 0);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1024, 100);
            this.newHead1.TabIndex = 94;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.ucTime1);
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Location = new System.Drawing.Point(3, 668);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1021, 100);
            this.panel2.TabIndex = 95;
            // 
            // FrmEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEvaluation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "患者满意度调查";
            this.Load += new System.EventHandler(this.FrmprintClinicEmrNote_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend2.ResumeLayout(false);
            this.backGroundPanelTrend2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.BackGroundPanelTrend backGroundPanelTrend2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnReturn;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblOK;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel2;
    }
}