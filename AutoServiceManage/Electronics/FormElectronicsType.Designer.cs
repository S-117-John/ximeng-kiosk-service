namespace AutoServiceManage.Electronics
{
    partial class FormElectronicsType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormElectronicsType));
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.lblET = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCR = new System.Windows.Forms.Label();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(13, 38);
            this.ucHead1.Margin = new System.Windows.Forms.Padding(5);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1352, 116);
            this.ucHead1.TabIndex = 48;
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.lblET);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.lblCR);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Margin = new System.Windows.Forms.Padding(4);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1365, 960);
            this.backGroundPanelTrend1.TabIndex = 52;
            // 
            // lblET
            // 
            this.lblET.BackColor = System.Drawing.Color.Transparent;
            this.lblET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblET.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblET.ForeColor = System.Drawing.Color.Transparent;
            this.lblET.Image = ((System.Drawing.Image)(resources.GetObject("lblET.Image")));
            this.lblET.Location = new System.Drawing.Point(768, 438);
            this.lblET.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblET.Name = "lblET";
            this.lblET.Size = new System.Drawing.Size(365, 164);
            this.lblET.TabIndex = 51;
            this.lblET.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnReturn.Location = new System.Drawing.Point(1027, 838);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(107, 100);
            this.btnReturn.TabIndex = 22;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(1173, 838);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(107, 100);
            this.btnExit.TabIndex = 23;
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(13, 900);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 37;
            this.ucTime1.Size = new System.Drawing.Size(276, 40);
            this.ucTime1.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 25);
            this.label2.TabIndex = 50;
            this.label2.Text = "请选择办卡类型";
            // 
            // lblCR
            // 
            this.lblCR.BackColor = System.Drawing.Color.Transparent;
            this.lblCR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCR.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCR.ForeColor = System.Drawing.Color.Transparent;
            this.lblCR.Image = ((System.Drawing.Image)(resources.GetObject("lblCR.Image")));
            this.lblCR.Location = new System.Drawing.Point(221, 438);
            this.lblCR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCR.Name = "lblCR";
            this.lblCR.Size = new System.Drawing.Size(365, 164);
            this.lblCR.TabIndex = 20;
            this.lblCR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCR.Click += new System.EventHandler(this.lblCR_Click);
            // 
            // FormElectronicsType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 960);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormElectronicsType";
            this.Text = "FormElectronicsType";
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Inc.UCHead ucHead1;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label lblET;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label btnExit;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCR;
    }
}