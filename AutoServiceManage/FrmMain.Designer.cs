namespace AutoServiceManage
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ucPanel1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.ucfoot1 = new AutoServiceManage.Inc.Ucfoot();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.lblyygk = new System.Windows.Forms.Label();
            this.lblTuiKa = new System.Windows.Forms.Label();
            this.lblsfbz = new System.Windows.Forms.Label();
            this.LblDaYin = new System.Windows.Forms.Label();
            this.lbljzzn = new System.Windows.Forms.Label();
            this.lblBanKa = new System.Windows.Forms.Label();
            this.lblQianDao = new System.Windows.Forms.Label();
            this.lblzjjs = new System.Windows.Forms.Label();
            this.lblYuYue = new System.Windows.Forms.Label();
            this.lblypjg = new System.Windows.Forms.Label();
            this.lblYuCun = new System.Windows.Forms.Label();
            this.lblksjs = new System.Windows.Forms.Label();
            this.lblJiaoFei = new System.Windows.Forms.Label();
            this.lblChaXun = new System.Windows.Forms.Label();
            this.ucPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // ucPanel1
            // 
            this.ucPanel1.BackgroundImage = global::AutoServiceManage.Properties.Resources.首页_深色压缩;
            this.ucPanel1.Controls.Add(this.ucfoot1);
            this.ucPanel1.Controls.Add(this.ucHead1);
            this.ucPanel1.Controls.Add(this.lblyygk);
            this.ucPanel1.Controls.Add(this.lblTuiKa);
            this.ucPanel1.Controls.Add(this.lblsfbz);
            this.ucPanel1.Controls.Add(this.LblDaYin);
            this.ucPanel1.Controls.Add(this.lbljzzn);
            this.ucPanel1.Controls.Add(this.lblBanKa);
            this.ucPanel1.Controls.Add(this.lblQianDao);
            this.ucPanel1.Controls.Add(this.lblzjjs);
            this.ucPanel1.Controls.Add(this.lblYuYue);
            this.ucPanel1.Controls.Add(this.lblypjg);
            this.ucPanel1.Controls.Add(this.lblYuCun);
            this.ucPanel1.Controls.Add(this.lblksjs);
            this.ucPanel1.Controls.Add(this.lblJiaoFei);
            this.ucPanel1.Controls.Add(this.lblChaXun);
            this.ucPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPanel1.Location = new System.Drawing.Point(0, 0);
            this.ucPanel1.Name = "ucPanel1";
            this.ucPanel1.Size = new System.Drawing.Size(1024, 768);
            this.ucPanel1.TabIndex = 10;
            // 
            // ucfoot1
            // 
            this.ucfoot1.BackColor = System.Drawing.Color.Transparent;
            this.ucfoot1.Location = new System.Drawing.Point(60, 689);
            this.ucfoot1.Name = "ucfoot1";
            this.ucfoot1.Size = new System.Drawing.Size(898, 54);
            this.ucfoot1.TabIndex = 11;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 96);
            this.ucHead1.TabIndex = 10;
            // 
            // lblyygk
            // 
            this.lblyygk.BackColor = System.Drawing.Color.Transparent;
            this.lblyygk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblyygk.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblyygk.ForeColor = System.Drawing.Color.White;
            this.lblyygk.Image = ((System.Drawing.Image)(resources.GetObject("lblyygk.Image")));
            this.lblyygk.Location = new System.Drawing.Point(79, 153);
            this.lblyygk.Name = "lblyygk";
            this.lblyygk.Size = new System.Drawing.Size(155, 40);
            this.lblyygk.TabIndex = 3;
            this.lblyygk.Click += new System.EventHandler(this.lblyygk_Click);
            // 
            // lblTuiKa
            // 
            this.lblTuiKa.BackColor = System.Drawing.Color.Transparent;
            this.lblTuiKa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTuiKa.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTuiKa.ForeColor = System.Drawing.Color.White;
            this.lblTuiKa.Image = global::AutoServiceManage.Properties.Resources.退_卡_2x;
            this.lblTuiKa.Location = new System.Drawing.Point(693, 550);
            this.lblTuiKa.Name = "lblTuiKa";
            this.lblTuiKa.Size = new System.Drawing.Size(207, 62);
            this.lblTuiKa.TabIndex = 9;
            this.lblTuiKa.Click += new System.EventHandler(this.lblTuiKa_Click);
            // 
            // lblsfbz
            // 
            this.lblsfbz.BackColor = System.Drawing.Color.Transparent;
            this.lblsfbz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblsfbz.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblsfbz.ForeColor = System.Drawing.Color.White;
            this.lblsfbz.Image = ((System.Drawing.Image)(resources.GetObject("lblsfbz.Image")));
            this.lblsfbz.Location = new System.Drawing.Point(79, 563);
            this.lblsfbz.Name = "lblsfbz";
            this.lblsfbz.Size = new System.Drawing.Size(155, 40);
            this.lblsfbz.TabIndex = 8;
            this.lblsfbz.Click += new System.EventHandler(this.lblsfbz_Click);
            // 
            // LblDaYin
            // 
            this.LblDaYin.BackColor = System.Drawing.Color.Transparent;
            this.LblDaYin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LblDaYin.Enabled = false;
            this.LblDaYin.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LblDaYin.ForeColor = System.Drawing.Color.White;
            this.LblDaYin.Image = global::AutoServiceManage.Properties.Resources.打_印_2x;
            this.LblDaYin.Location = new System.Drawing.Point(693, 410);
            this.LblDaYin.Name = "LblDaYin";
            this.LblDaYin.Size = new System.Drawing.Size(207, 62);
            this.LblDaYin.TabIndex = 8;
            this.LblDaYin.Click += new System.EventHandler(this.LblDaYin_Click);
            // 
            // lbljzzn
            // 
            this.lbljzzn.BackColor = System.Drawing.Color.Transparent;
            this.lbljzzn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbljzzn.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbljzzn.ForeColor = System.Drawing.Color.White;
            this.lbljzzn.Image = ((System.Drawing.Image)(resources.GetObject("lbljzzn.Image")));
            this.lbljzzn.Location = new System.Drawing.Point(79, 235);
            this.lbljzzn.Name = "lbljzzn";
            this.lbljzzn.Size = new System.Drawing.Size(155, 40);
            this.lbljzzn.TabIndex = 4;
            this.lbljzzn.Click += new System.EventHandler(this.lbljzzn_Click);
            // 
            // lblBanKa
            // 
            this.lblBanKa.BackColor = System.Drawing.Color.Transparent;
            this.lblBanKa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBanKa.Enabled = false;
            this.lblBanKa.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBanKa.ForeColor = System.Drawing.Color.White;
            this.lblBanKa.Image = global::AutoServiceManage.Properties.Resources.办_卡_2x;
            this.lblBanKa.Location = new System.Drawing.Point(377, 158);
            this.lblBanKa.Name = "lblBanKa";
            this.lblBanKa.Size = new System.Drawing.Size(207, 62);
            this.lblBanKa.TabIndex = 2;
            this.lblBanKa.Click += new System.EventHandler(this.lblBanKa_Click);
            // 
            // lblQianDao
            // 
            this.lblQianDao.BackColor = System.Drawing.Color.Transparent;
            this.lblQianDao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblQianDao.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQianDao.ForeColor = System.Drawing.Color.White;
            this.lblQianDao.Image = global::AutoServiceManage.Properties.Resources.取_号_2x;
            this.lblQianDao.Location = new System.Drawing.Point(693, 283);
            this.lblQianDao.Name = "lblQianDao";
            this.lblQianDao.Size = new System.Drawing.Size(207, 62);
            this.lblQianDao.TabIndex = 7;
            this.lblQianDao.Click += new System.EventHandler(this.lblQianDao_Click);
            // 
            // lblzjjs
            // 
            this.lblzjjs.BackColor = System.Drawing.Color.Transparent;
            this.lblzjjs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblzjjs.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblzjjs.ForeColor = System.Drawing.Color.White;
            this.lblzjjs.Image = ((System.Drawing.Image)(resources.GetObject("lblzjjs.Image")));
            this.lblzjjs.Location = new System.Drawing.Point(79, 317);
            this.lblzjjs.Name = "lblzjjs";
            this.lblzjjs.Size = new System.Drawing.Size(155, 40);
            this.lblzjjs.TabIndex = 5;
            this.lblzjjs.Click += new System.EventHandler(this.lblzjjs_Click);
            // 
            // lblYuYue
            // 
            this.lblYuYue.BackColor = System.Drawing.Color.Transparent;
            this.lblYuYue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblYuYue.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYuYue.ForeColor = System.Drawing.Color.White;
            this.lblYuYue.Image = global::AutoServiceManage.Properties.Resources.预_约_2x;
            this.lblYuYue.Location = new System.Drawing.Point(377, 283);
            this.lblYuYue.Name = "lblYuYue";
            this.lblYuYue.Size = new System.Drawing.Size(207, 62);
            this.lblYuYue.TabIndex = 3;
            this.lblYuYue.Click += new System.EventHandler(this.lblYuYue_Click);
            // 
            // lblypjg
            // 
            this.lblypjg.BackColor = System.Drawing.Color.Transparent;
            this.lblypjg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblypjg.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblypjg.ForeColor = System.Drawing.Color.White;
            this.lblypjg.Image = ((System.Drawing.Image)(resources.GetObject("lblypjg.Image")));
            this.lblypjg.Location = new System.Drawing.Point(79, 481);
            this.lblypjg.Name = "lblypjg";
            this.lblypjg.Size = new System.Drawing.Size(155, 40);
            this.lblypjg.TabIndex = 7;
            this.lblypjg.Click += new System.EventHandler(this.lblypjg_Click);
            // 
            // lblYuCun
            // 
            this.lblYuCun.BackColor = System.Drawing.Color.Transparent;
            this.lblYuCun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblYuCun.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYuCun.ForeColor = System.Drawing.Color.White;
            this.lblYuCun.Image = global::AutoServiceManage.Properties.Resources.预_存_2x;
            this.lblYuCun.Location = new System.Drawing.Point(693, 158);
            this.lblYuCun.Name = "lblYuCun";
            this.lblYuCun.Size = new System.Drawing.Size(207, 62);
            this.lblYuCun.TabIndex = 6;
            this.lblYuCun.Click += new System.EventHandler(this.lblYuCun_Click);
            // 
            // lblksjs
            // 
            this.lblksjs.BackColor = System.Drawing.Color.Transparent;
            this.lblksjs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblksjs.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblksjs.ForeColor = System.Drawing.Color.White;
            this.lblksjs.Image = ((System.Drawing.Image)(resources.GetObject("lblksjs.Image")));
            this.lblksjs.Location = new System.Drawing.Point(79, 399);
            this.lblksjs.Name = "lblksjs";
            this.lblksjs.Size = new System.Drawing.Size(155, 40);
            this.lblksjs.TabIndex = 6;
            this.lblksjs.Click += new System.EventHandler(this.lblksjs_Click);
            // 
            // lblJiaoFei
            // 
            this.lblJiaoFei.BackColor = System.Drawing.Color.Transparent;
            this.lblJiaoFei.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblJiaoFei.Enabled = false;
            this.lblJiaoFei.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblJiaoFei.ForeColor = System.Drawing.Color.White;
            this.lblJiaoFei.Image = global::AutoServiceManage.Properties.Resources.缴_费_2x;
            this.lblJiaoFei.Location = new System.Drawing.Point(377, 410);
            this.lblJiaoFei.Name = "lblJiaoFei";
            this.lblJiaoFei.Size = new System.Drawing.Size(207, 62);
            this.lblJiaoFei.TabIndex = 4;
            this.lblJiaoFei.Click += new System.EventHandler(this.lblJiaoFei_Click);
            // 
            // lblChaXun
            // 
            this.lblChaXun.BackColor = System.Drawing.Color.Transparent;
            this.lblChaXun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblChaXun.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblChaXun.ForeColor = System.Drawing.Color.White;
            this.lblChaXun.Image = global::AutoServiceManage.Properties.Resources.查_询_2x;
            this.lblChaXun.Location = new System.Drawing.Point(377, 550);
            this.lblChaXun.Name = "lblChaXun";
            this.lblChaXun.Size = new System.Drawing.Size(207, 62);
            this.lblChaXun.TabIndex = 5;
            this.lblChaXun.Click += new System.EventHandler(this.lblChaXun_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.ucPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMain";
            this.Text = "自助服务程序";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ucPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTuiKa;
        private System.Windows.Forms.Label LblDaYin;
        private System.Windows.Forms.Label lblQianDao;
        private System.Windows.Forms.Label lblYuCun;
        private System.Windows.Forms.Label lblChaXun;
        private System.Windows.Forms.Label lblJiaoFei;
        private System.Windows.Forms.Label lblYuYue;
        private System.Windows.Forms.Label lblBanKa;
        private System.Windows.Forms.Label lblsfbz;
        private System.Windows.Forms.Label lblypjg;
        private System.Windows.Forms.Label lblksjs;
        private System.Windows.Forms.Label lblzjjs;
        private System.Windows.Forms.Label lbljzzn;
        private System.Windows.Forms.Label lblyygk;
        private Inc.BackGroundPanelTrend ucPanel1;
        private Inc.Ucfoot ucfoot1;
        private Inc.UCHead ucHead1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

