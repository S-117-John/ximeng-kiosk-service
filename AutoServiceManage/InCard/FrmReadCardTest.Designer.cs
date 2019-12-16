namespace AutoServiceManage.InCard
{
    partial class FrmReadCardTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReadCardTest));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.lblerr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.ForeColor = System.Drawing.Color.Black;
            this.ucTime1.Location = new System.Drawing.Point(347, 7);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 28;
            this.ucTime1.Size = new System.Drawing.Size(162, 32);
            this.ucTime1.TabIndex = 40;
            // 
            // lblerr
            // 
            this.lblerr.AutoSize = true;
            this.lblerr.BackColor = System.Drawing.Color.Transparent;
            this.lblerr.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblerr.ForeColor = System.Drawing.Color.Red;
            this.lblerr.Location = new System.Drawing.Point(118, 192);
            this.lblerr.Name = "lblerr";
            this.lblerr.Size = new System.Drawing.Size(112, 22);
            this.lblerr.TabIndex = 39;
            this.lblerr.Text = "卡号不能为空!";
            this.lblerr.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(53, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 22);
            this.label1.TabIndex = 38;
            this.label1.Text = "请插入就诊卡:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(119)))), ((int)(((byte)(208)))));
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(42, 22);
            this.lblTitle.TabIndex = 37;
            this.lblTitle.Text = "读卡";
            // 
            // txtCardID
            // 
            this.txtCardID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardID.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardID.Location = new System.Drawing.Point(66, 130);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.PasswordChar = '*';
            this.txtCardID.Size = new System.Drawing.Size(427, 40);
            this.txtCardID.TabIndex = 36;
            this.txtCardID.TextChanged += new System.EventHandler(this.txtCardID_TextChanged);
            this.txtCardID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardID_KeyPress);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(54, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(450, 55);
            this.label3.TabIndex = 35;
            this.label3.Text = "预约号取号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmReadCardTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = global::AutoServiceManage.Properties.Resources.弹出背景;
            this.ClientSize = new System.Drawing.Size(533, 277);
            this.Controls.Add(this.ucTime1);
            this.Controls.Add(this.lblerr);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReadCardTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测试读卡";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.Shown += new System.EventHandler(this.FrmReadCardTest_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblerr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Inc.UcTime ucTime1;
    }
}