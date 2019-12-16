using AutoServiceManage.Inc;
namespace AutoServiceManage
{
    partial class FrmBespeakQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBespeakQuery));
            this.panel1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.lblhm = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.btnExit = new System.Windows.Forms.Label();
            this.lblInputBespeakID = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.lblInputTelePhone = new System.Windows.Forms.Label();
            this.lblErr = new System.Windows.Forms.Label();
            this.lbltitle = new System.Windows.Forms.Label();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl8 = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.lbl9 = new System.Windows.Forms.Label();
            this.lblDelete = new System.Windows.Forms.Label();
            this.lblclear = new System.Windows.Forms.Label();
            this.lbl0 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.panel1.Controls.Add(this.lblhm);
            this.panel1.Controls.Add(this.ucHead1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.lblInputBespeakID);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.lblInputTelePhone);
            this.panel1.Controls.Add(this.lblErr);
            this.panel1.Controls.Add(this.lbltitle);
            this.panel1.Controls.Add(this.lbl7);
            this.panel1.Controls.Add(this.lbl8);
            this.panel1.Controls.Add(this.lblOK);
            this.panel1.Controls.Add(this.lbl9);
            this.panel1.Controls.Add(this.lblDelete);
            this.panel1.Controls.Add(this.lblclear);
            this.panel1.Controls.Add(this.lbl0);
            this.panel1.Controls.Add(this.lbl4);
            this.panel1.Controls.Add(this.lbl3);
            this.panel1.Controls.Add(this.lbl5);
            this.panel1.Controls.Add(this.lbl2);
            this.panel1.Controls.Add(this.lbl6);
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 768);
            this.panel1.TabIndex = 42;
            // 
            // lblhm
            // 
            this.lblhm.BackColor = System.Drawing.Color.White;
            this.lblhm.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblhm.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblhm.Location = new System.Drawing.Point(557, 208);
            this.lblhm.Name = "lblhm";
            this.lblhm.Size = new System.Drawing.Size(421, 35);
            this.lblhm.TabIndex = 60;
            this.lblhm.Text = "请输入预约号";
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(118, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(297, 54);
            this.label4.TabIndex = 66;
            this.label4.Text = "请输入预约短信中的预约号或者预约时的手机号码来进行取号！";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 26);
            this.label1.TabIndex = 41;
            this.label1.Text = "取号";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(119, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 65;
            this.label2.Text = "温馨提示：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 18;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 43;
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
            this.btnExit.TabIndex = 64;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblInputBespeakID
            // 
            this.lblInputBespeakID.BackColor = System.Drawing.Color.Transparent;
            this.lblInputBespeakID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInputBespeakID.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputBespeakID.ForeColor = System.Drawing.Color.Transparent;
            this.lblInputBespeakID.Image = global::AutoServiceManage.Properties.Resources.预约号签到;
            this.lblInputBespeakID.Location = new System.Drawing.Point(118, 197);
            this.lblInputBespeakID.Name = "lblInputBespeakID";
            this.lblInputBespeakID.Size = new System.Drawing.Size(274, 62);
            this.lblInputBespeakID.TabIndex = 44;
            this.lblInputBespeakID.Text = "预约号取号";
            this.lblInputBespeakID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInputBespeakID.Click += new System.EventHandler(this.lblInputBespeakID_Click);
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
            this.btnReturn.TabIndex = 63;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblInputTelePhone
            // 
            this.lblInputTelePhone.BackColor = System.Drawing.Color.Transparent;
            this.lblInputTelePhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInputTelePhone.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInputTelePhone.ForeColor = System.Drawing.Color.Transparent;
            this.lblInputTelePhone.Image = global::AutoServiceManage.Properties.Resources.手机号签到;
            this.lblInputTelePhone.Location = new System.Drawing.Point(118, 285);
            this.lblInputTelePhone.Name = "lblInputTelePhone";
            this.lblInputTelePhone.Size = new System.Drawing.Size(274, 62);
            this.lblInputTelePhone.TabIndex = 45;
            this.lblInputTelePhone.Text = "手机号取号";
            this.lblInputTelePhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInputTelePhone.Click += new System.EventHandler(this.lblInputTelePhone_Click);
            // 
            // lblErr
            // 
            this.lblErr.BackColor = System.Drawing.Color.Transparent;
            this.lblErr.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblErr.ForeColor = System.Drawing.Color.Red;
            this.lblErr.Location = new System.Drawing.Point(96, 536);
            this.lblErr.Name = "lblErr";
            this.lblErr.Size = new System.Drawing.Size(410, 149);
            this.lblErr.TabIndex = 62;
            this.lblErr.Text = "取号失败：输入的预约号太长！";
            this.lblErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErr.Visible = false;
            // 
            // lbltitle
            // 
            this.lbltitle.BackColor = System.Drawing.Color.Transparent;
            this.lbltitle.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltitle.ForeColor = System.Drawing.Color.White;
            this.lbltitle.Location = new System.Drawing.Point(443, 192);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(93, 56);
            this.lbltitle.TabIndex = 61;
            this.lbltitle.Text = "预约号";
            this.lbltitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl7
            // 
            this.lbl7.BackColor = System.Drawing.Color.Transparent;
            this.lbl7.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl7.ForeColor = System.Drawing.Color.Black;
            this.lbl7.Image = ((System.Drawing.Image)(resources.GetObject("lbl7.Image")));
            this.lbl7.Location = new System.Drawing.Point(545, 273);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(73, 56);
            this.lbl7.TabIndex = 47;
            this.lbl7.Text = "7";
            this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl7.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl8
            // 
            this.lbl8.BackColor = System.Drawing.Color.Transparent;
            this.lbl8.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl8.ForeColor = System.Drawing.Color.Black;
            this.lbl8.Image = ((System.Drawing.Image)(resources.GetObject("lbl8.Image")));
            this.lbl8.Location = new System.Drawing.Point(641, 273);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(73, 56);
            this.lbl8.TabIndex = 48;
            this.lbl8.Text = "8";
            this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl8.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.Black;
            this.lblOK.Image = global::AutoServiceManage.Properties.Resources.键盘_确定;
            this.lblOK.Location = new System.Drawing.Point(837, 400);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(146, 156);
            this.lblOK.TabIndex = 59;
            this.lblOK.Text = "确定";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // lbl9
            // 
            this.lbl9.BackColor = System.Drawing.Color.Transparent;
            this.lbl9.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl9.ForeColor = System.Drawing.Color.Black;
            this.lbl9.Image = ((System.Drawing.Image)(resources.GetObject("lbl9.Image")));
            this.lbl9.Location = new System.Drawing.Point(735, 273);
            this.lbl9.Name = "lbl9";
            this.lbl9.Size = new System.Drawing.Size(73, 56);
            this.lbl9.TabIndex = 49;
            this.lbl9.Text = "9";
            this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl9.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lblDelete
            // 
            this.lblDelete.BackColor = System.Drawing.Color.Transparent;
            this.lblDelete.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDelete.ForeColor = System.Drawing.Color.Black;
            this.lblDelete.Image = global::AutoServiceManage.Properties.Resources.退格;
            this.lblDelete.Location = new System.Drawing.Point(642, 500);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(166, 56);
            this.lblDelete.TabIndex = 58;
            this.lblDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDelete.Click += new System.EventHandler(this.lblDelete_Click);
            // 
            // lblclear
            // 
            this.lblclear.BackColor = System.Drawing.Color.Transparent;
            this.lblclear.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblclear.ForeColor = System.Drawing.Color.Black;
            this.lblclear.Image = global::AutoServiceManage.Properties.Resources.清除;
            this.lblclear.Location = new System.Drawing.Point(832, 273);
            this.lblclear.Name = "lblclear";
            this.lblclear.Size = new System.Drawing.Size(146, 96);
            this.lblclear.TabIndex = 50;
            this.lblclear.Text = "清除";
            this.lblclear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblclear.Click += new System.EventHandler(this.lblclear_Click);
            // 
            // lbl0
            // 
            this.lbl0.BackColor = System.Drawing.Color.Transparent;
            this.lbl0.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl0.ForeColor = System.Drawing.Color.Black;
            this.lbl0.Image = global::AutoServiceManage.Properties.Resources.数字键盘0_9;
            this.lbl0.Location = new System.Drawing.Point(545, 499);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(73, 56);
            this.lbl0.TabIndex = 57;
            this.lbl0.Text = "0";
            this.lbl0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl0.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl4
            // 
            this.lbl4.BackColor = System.Drawing.Color.Transparent;
            this.lbl4.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl4.ForeColor = System.Drawing.Color.Black;
            this.lbl4.Image = ((System.Drawing.Image)(resources.GetObject("lbl4.Image")));
            this.lbl4.Location = new System.Drawing.Point(545, 348);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(73, 56);
            this.lbl4.TabIndex = 51;
            this.lbl4.Text = "4";
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl4.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.Transparent;
            this.lbl3.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl3.ForeColor = System.Drawing.Color.Black;
            this.lbl3.Image = ((System.Drawing.Image)(resources.GetObject("lbl3.Image")));
            this.lbl3.Location = new System.Drawing.Point(735, 426);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(73, 56);
            this.lbl3.TabIndex = 56;
            this.lbl3.Text = "3";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl3.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl5
            // 
            this.lbl5.BackColor = System.Drawing.Color.Transparent;
            this.lbl5.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl5.ForeColor = System.Drawing.Color.Black;
            this.lbl5.Image = ((System.Drawing.Image)(resources.GetObject("lbl5.Image")));
            this.lbl5.Location = new System.Drawing.Point(641, 348);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(73, 56);
            this.lbl5.TabIndex = 52;
            this.lbl5.Text = "5";
            this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl5.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.Transparent;
            this.lbl2.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.ForeColor = System.Drawing.Color.Black;
            this.lbl2.Image = ((System.Drawing.Image)(resources.GetObject("lbl2.Image")));
            this.lbl2.Location = new System.Drawing.Point(641, 426);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(73, 56);
            this.lbl2.TabIndex = 55;
            this.lbl2.Text = "2";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl2.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl6
            // 
            this.lbl6.BackColor = System.Drawing.Color.Transparent;
            this.lbl6.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl6.ForeColor = System.Drawing.Color.Black;
            this.lbl6.Image = ((System.Drawing.Image)(resources.GetObject("lbl6.Image")));
            this.lbl6.Location = new System.Drawing.Point(735, 348);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(73, 56);
            this.lbl6.TabIndex = 53;
            this.lbl6.Text = "6";
            this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl6.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("新宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl1.ForeColor = System.Drawing.Color.Black;
            this.lbl1.Image = global::AutoServiceManage.Properties.Resources.数字键盘0_9;
            this.lbl1.Location = new System.Drawing.Point(545, 426);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(73, 56);
            this.lbl1.TabIndex = 54;
            this.lbl1.Text = "1";
            this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl1.Click += new System.EventHandler(this.lbl0_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(541, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(450, 55);
            this.label3.TabIndex = 46;
            this.label3.Text = "预约号取号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmBespeakQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBespeakQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助预约查询";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBespeakQuery_FormClosing);
            this.Load += new System.EventHandler(this.FrmBespeakQuery_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BackGroundPanelTrend panel1;
        private Inc.UCHead ucHead1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label lblInputBespeakID;
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label lblInputTelePhone;
        private System.Windows.Forms.Label lblErr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lblhm;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label lbl9;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Label lblclear;
        private System.Windows.Forms.Label lbl0;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl1;

    }
}