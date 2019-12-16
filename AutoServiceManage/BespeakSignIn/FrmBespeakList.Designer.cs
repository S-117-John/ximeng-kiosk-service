namespace AutoServiceManage
{
    partial class FrmBespeakList
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
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.lblAddMoneyBank = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.btnExit = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.btnReturn = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddMoney = new System.Windows.Forms.Label();
            this.ucSelectList = new AutoServiceManage.Inc.ucBespeakSelectList();
            this.label8 = new System.Windows.Forms.Label();
            this.lblYE = new System.Windows.Forms.Label();
            this.lblTotalMoney = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backGroundPanelTrend1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.lblAddMoneyBank);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.btnReturn);
            this.backGroundPanelTrend1.Controls.Add(this.lblOK);
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.label6);
            this.backGroundPanelTrend1.Controls.Add(this.label4);
            this.backGroundPanelTrend1.Controls.Add(this.label5);
            this.backGroundPanelTrend1.Controls.Add(this.btnAddMoney);
            this.backGroundPanelTrend1.Controls.Add(this.ucSelectList);
            this.backGroundPanelTrend1.Controls.Add(this.label8);
            this.backGroundPanelTrend1.Controls.Add(this.lblYE);
            this.backGroundPanelTrend1.Controls.Add(this.lblTotalMoney);
            this.backGroundPanelTrend1.Controls.Add(this.label7);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 18;
            // 
            // lblAddMoneyBank
            // 
            this.lblAddMoneyBank.BackColor = System.Drawing.Color.Transparent;
            this.lblAddMoneyBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddMoneyBank.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddMoneyBank.ForeColor = System.Drawing.Color.Transparent;
            this.lblAddMoneyBank.Image = global::AutoServiceManage.Properties.Resources.确定按钮;
            this.lblAddMoneyBank.Location = new System.Drawing.Point(222, 659);
            this.lblAddMoneyBank.Name = "lblAddMoneyBank";
            this.lblAddMoneyBank.Size = new System.Drawing.Size(133, 44);
            this.lblAddMoneyBank.TabIndex = 18;
            this.lblAddMoneyBank.Text = "银行卡预存";
            this.lblAddMoneyBank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAddMoneyBank.Click += new System.EventHandler(this.lblAddMoneyBank_Click);
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 13;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnExit.Image = global::AutoServiceManage.Properties.Resources.退出;
            this.btnExit.Location = new System.Drawing.Point(880, 670);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 80);
            this.btnExit.TabIndex = 15;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 46;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 16;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.btnReturn.Image = global::AutoServiceManage.Properties.Resources.返回;
            this.btnReturn.Location = new System.Drawing.Point(770, 670);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(80, 80);
            this.btnReturn.TabIndex = 14;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblOK.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.lblOK.Location = new System.Drawing.Point(387, 659);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(133, 44);
            this.lblOK.TabIndex = 17;
            this.lblOK.Text = "确认";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(31, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "取 号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(312, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 38);
            this.label6.TabIndex = 7;
            this.label6.Text = "天网测试";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(202, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 38);
            this.label4.TabIndex = 5;
            this.label4.Text = "你好，";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(451, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "请确认您的预约信息！";
            // 
            // btnAddMoney
            // 
            this.btnAddMoney.BackColor = System.Drawing.Color.Transparent;
            this.btnAddMoney.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMoney.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddMoney.ForeColor = System.Drawing.Color.Transparent;
            this.btnAddMoney.Image = global::AutoServiceManage.Properties.Resources.确定按钮;
            this.btnAddMoney.Location = new System.Drawing.Point(57, 659);
            this.btnAddMoney.Name = "btnAddMoney";
            this.btnAddMoney.Size = new System.Drawing.Size(133, 44);
            this.btnAddMoney.TabIndex = 12;
            this.btnAddMoney.Text = "现金预存";
            this.btnAddMoney.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddMoney.Click += new System.EventHandler(this.btnAddMoney_Click);
            // 
            // ucSelectList
            // 
            this.ucSelectList.BackColor = System.Drawing.Color.Transparent;
            this.ucSelectList.DataSource = null;
            this.ucSelectList.Location = new System.Drawing.Point(99, 274);
            this.ucSelectList.Name = "ucSelectList";
            this.ucSelectList.Size = new System.Drawing.Size(797, 259);
            this.ucSelectList.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(481, 551);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 30);
            this.label8.TabIndex = 10;
            this.label8.Text = "余额";
            // 
            // lblYE
            // 
            this.lblYE.BackColor = System.Drawing.Color.Transparent;
            this.lblYE.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYE.ForeColor = System.Drawing.Color.White;
            this.lblYE.Location = new System.Drawing.Point(543, 551);
            this.lblYE.Name = "lblYE";
            this.lblYE.Size = new System.Drawing.Size(93, 30);
            this.lblYE.TabIndex = 9;
            this.lblYE.Text = "666.8";
            this.lblYE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalMoney
            // 
            this.lblTotalMoney.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalMoney.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalMoney.ForeColor = System.Drawing.Color.Red;
            this.lblTotalMoney.Location = new System.Drawing.Point(779, 551);
            this.lblTotalMoney.Name = "lblTotalMoney";
            this.lblTotalMoney.Size = new System.Drawing.Size(61, 30);
            this.lblTotalMoney.TabIndex = 3;
            this.lblTotalMoney.Text = "0";
            this.lblTotalMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(846, 551);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 30);
            this.label7.TabIndex = 8;
            this.label7.Text = "元";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(681, 551);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "总费用";
            // 
            // FrmBespeakList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBespeakList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助预约确认";
            this.TransparencyKey = System.Drawing.SystemColors.Desktop;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBespeakList_FormClosing);
            this.Load += new System.EventHandler(this.FrmBespeakList_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Inc.ucBespeakSelectList ucSelectList;
        private System.Windows.Forms.Label lblTotalMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblYE;
        private System.Windows.Forms.Label btnAddMoney;
        private Inc.UCHead ucHead1;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label btnExit;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label lblOK;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private System.Windows.Forms.Label lblAddMoneyBank;
    }
}