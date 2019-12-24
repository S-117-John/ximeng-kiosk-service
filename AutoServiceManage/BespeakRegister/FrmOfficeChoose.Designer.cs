namespace AutoServiceManage
{
    partial class FrmOfficeChoose
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOfficeChoose));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.officeList1 = new AutoServiceManage.Inc.OfficeList();
            this.plAll = new System.Windows.Forms.Panel();
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.pcReturn = new System.Windows.Forms.PictureBox();
            this.lblC = new System.Windows.Forms.Label();
            this.lblD = new System.Windows.Forms.Label();
            this.menuList1 = new MenuList();
            this.lblE = new System.Windows.Forms.Label();
            this.lblF = new System.Windows.Forms.Label();
            this.lblG = new System.Windows.Forms.Label();
            this.lblH = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            this.lblJ = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblk = new System.Windows.Forms.Label();
            this.lblW = new System.Windows.Forms.Label();
            this.lblL = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblM = new System.Windows.Forms.Label();
            this.lblT = new System.Windows.Forms.Label();
            this.lblN = new System.Windows.Forms.Label();
            this.lblS = new System.Windows.Forms.Label();
            this.lblO = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.lblP = new System.Windows.Forms.Label();
            this.lblQ = new System.Windows.Forms.Label();
            this.backGroundPanelTrend1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backGroundPanelTrend1.BackgroundImage")));
            this.backGroundPanelTrend1.Controls.Add(this.panel1);
            this.backGroundPanelTrend1.Controls.Add(this.newHead1);
            this.backGroundPanelTrend1.Controls.Add(this.officeList1);
            this.backGroundPanelTrend1.Controls.Add(this.plAll);
            this.backGroundPanelTrend1.Controls.Add(this.lblA);
            this.backGroundPanelTrend1.Controls.Add(this.lblB);
            this.backGroundPanelTrend1.Controls.Add(this.lblC);
            this.backGroundPanelTrend1.Controls.Add(this.lblD);
            this.backGroundPanelTrend1.Controls.Add(this.menuList1);
            this.backGroundPanelTrend1.Controls.Add(this.lblE);
            this.backGroundPanelTrend1.Controls.Add(this.lblF);
            this.backGroundPanelTrend1.Controls.Add(this.lblG);
            this.backGroundPanelTrend1.Controls.Add(this.lblH);
            this.backGroundPanelTrend1.Controls.Add(this.lblZ);
            this.backGroundPanelTrend1.Controls.Add(this.lblJ);
            this.backGroundPanelTrend1.Controls.Add(this.lblY);
            this.backGroundPanelTrend1.Controls.Add(this.lblk);
            this.backGroundPanelTrend1.Controls.Add(this.lblW);
            this.backGroundPanelTrend1.Controls.Add(this.lblL);
            this.backGroundPanelTrend1.Controls.Add(this.lblX);
            this.backGroundPanelTrend1.Controls.Add(this.lblM);
            this.backGroundPanelTrend1.Controls.Add(this.lblT);
            this.backGroundPanelTrend1.Controls.Add(this.lblN);
            this.backGroundPanelTrend1.Controls.Add(this.lblS);
            this.backGroundPanelTrend1.Controls.Add(this.lblO);
            this.backGroundPanelTrend1.Controls.Add(this.lblR);
            this.backGroundPanelTrend1.Controls.Add(this.lblP);
            this.backGroundPanelTrend1.Controls.Add(this.lblQ);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 91;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.pcReturn);
            this.panel1.Controls.Add(this.pcExit);
            this.panel1.Location = new System.Drawing.Point(0, 668);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 100);
            this.panel1.TabIndex = 95;
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(0, 0);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1024, 100);
            this.newHead1.TabIndex = 94;
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(55, 25);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 53;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 93;
            // 
            // officeList1
            // 
            this.officeList1.BackColor = System.Drawing.Color.Transparent;
            this.officeList1.Location = new System.Drawing.Point(41, 183);
            this.officeList1.Name = "officeList1";
            this.officeList1.Size = new System.Drawing.Size(931, 481);
            this.officeList1.TabIndex = 91;
            // 
            // plAll
            // 
            this.plAll.BackColor = System.Drawing.Color.Transparent;
            this.plAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("plAll.BackgroundImage")));
            this.plAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.plAll.Location = new System.Drawing.Point(30, 115);
            this.plAll.Name = "plAll";
            this.plAll.Size = new System.Drawing.Size(54, 33);
            this.plAll.TabIndex = 53;
            this.plAll.Click += new System.EventHandler(this.plAll_Click);
            // 
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(899, 8);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(80, 80);
            this.pcExit.TabIndex = 90;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.BackColor = System.Drawing.Color.Transparent;
            this.lblA.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblA.ForeColor = System.Drawing.Color.Black;
            this.lblA.Location = new System.Drawing.Point(103, 124);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(23, 24);
            this.lblA.TabIndex = 54;
            this.lblA.Text = "A";
            this.lblA.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.BackColor = System.Drawing.Color.Transparent;
            this.lblB.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblB.ForeColor = System.Drawing.Color.Black;
            this.lblB.Location = new System.Drawing.Point(139, 124);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(23, 24);
            this.lblB.TabIndex = 55;
            this.lblB.Text = "B";
            this.lblB.Click += new System.EventHandler(this.label1_Click);
            // 
            // pcReturn
            // 
            this.pcReturn.BackColor = System.Drawing.Color.Transparent;
            this.pcReturn.Image = ((System.Drawing.Image)(resources.GetObject("pcReturn.Image")));
            this.pcReturn.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcReturn.InitialImage")));
            this.pcReturn.Location = new System.Drawing.Point(766, 8);
            this.pcReturn.Name = "pcReturn";
            this.pcReturn.Size = new System.Drawing.Size(80, 80);
            this.pcReturn.TabIndex = 81;
            this.pcReturn.TabStop = false;
            this.pcReturn.Click += new System.EventHandler(this.pcReturn_Click);
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.BackColor = System.Drawing.Color.Transparent;
            this.lblC.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblC.ForeColor = System.Drawing.Color.Black;
            this.lblC.Location = new System.Drawing.Point(175, 124);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(23, 24);
            this.lblC.TabIndex = 56;
            this.lblC.Text = "C";
            this.lblC.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblD
            // 
            this.lblD.AutoSize = true;
            this.lblD.BackColor = System.Drawing.Color.Transparent;
            this.lblD.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblD.ForeColor = System.Drawing.Color.Black;
            this.lblD.Location = new System.Drawing.Point(211, 124);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(23, 24);
            this.lblD.TabIndex = 57;
            this.lblD.Text = "D";
            this.lblD.Click += new System.EventHandler(this.label3_Click);
            // 
            // menuList1
            // 
            this.menuList1.BackColor = System.Drawing.Color.Transparent;
            this.menuList1.ContentPadding = new System.Windows.Forms.Padding(60, 20, 60, 20);
            this.menuList1.DefaultImage = null;
            this.menuList1.Font = new System.Drawing.Font("宋体", 12F);
            this.menuList1.ForeColor = System.Drawing.Color.White;
            this.menuList1.HorizontalSpace = 20;
            this.menuList1.Location = new System.Drawing.Point(54, 183);
            this.menuList1.MenuSize = new System.Drawing.Size(140, 75);
            this.menuList1.Name = "menuList1";
            this.menuList1.Size = new System.Drawing.Size(30, 23);
            this.menuList1.TabIndex = 85;
            this.menuList1.VerticalSpace = 20;
            this.menuList1.Visible = false;
            // 
            // lblE
            // 
            this.lblE.AutoSize = true;
            this.lblE.BackColor = System.Drawing.Color.Transparent;
            this.lblE.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblE.ForeColor = System.Drawing.Color.Black;
            this.lblE.Location = new System.Drawing.Point(247, 124);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(23, 24);
            this.lblE.TabIndex = 58;
            this.lblE.Text = "E";
            this.lblE.Click += new System.EventHandler(this.label4_Click);
            // 
            // lblF
            // 
            this.lblF.AutoSize = true;
            this.lblF.BackColor = System.Drawing.Color.Transparent;
            this.lblF.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblF.ForeColor = System.Drawing.Color.Black;
            this.lblF.Location = new System.Drawing.Point(283, 124);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(23, 24);
            this.lblF.TabIndex = 59;
            this.lblF.Text = "F";
            this.lblF.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblG
            // 
            this.lblG.AutoSize = true;
            this.lblG.BackColor = System.Drawing.Color.Transparent;
            this.lblG.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblG.ForeColor = System.Drawing.Color.Black;
            this.lblG.Location = new System.Drawing.Point(319, 124);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(23, 24);
            this.lblG.TabIndex = 60;
            this.lblG.Text = "G";
            this.lblG.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblH
            // 
            this.lblH.AutoSize = true;
            this.lblH.BackColor = System.Drawing.Color.Transparent;
            this.lblH.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblH.ForeColor = System.Drawing.Color.Black;
            this.lblH.Location = new System.Drawing.Point(355, 124);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(23, 24);
            this.lblH.TabIndex = 61;
            this.lblH.Text = "H";
            this.lblH.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.BackColor = System.Drawing.Color.Transparent;
            this.lblZ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblZ.ForeColor = System.Drawing.Color.Black;
            this.lblZ.Location = new System.Drawing.Point(895, 124);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(23, 24);
            this.lblZ.TabIndex = 76;
            this.lblZ.Text = "Z";
            this.lblZ.Click += new System.EventHandler(this.lblZ_Click);
            // 
            // lblJ
            // 
            this.lblJ.AutoSize = true;
            this.lblJ.BackColor = System.Drawing.Color.Transparent;
            this.lblJ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblJ.ForeColor = System.Drawing.Color.Black;
            this.lblJ.Location = new System.Drawing.Point(391, 124);
            this.lblJ.Name = "lblJ";
            this.lblJ.Size = new System.Drawing.Size(23, 24);
            this.lblJ.TabIndex = 62;
            this.lblJ.Text = "J";
            this.lblJ.Click += new System.EventHandler(this.lblJ_Click);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.BackColor = System.Drawing.Color.Transparent;
            this.lblY.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblY.ForeColor = System.Drawing.Color.Black;
            this.lblY.Location = new System.Drawing.Point(859, 124);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(23, 24);
            this.lblY.TabIndex = 75;
            this.lblY.Text = "Y";
            this.lblY.Click += new System.EventHandler(this.lblY_Click);
            // 
            // lblk
            // 
            this.lblk.AutoSize = true;
            this.lblk.BackColor = System.Drawing.Color.Transparent;
            this.lblk.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblk.ForeColor = System.Drawing.Color.Black;
            this.lblk.Location = new System.Drawing.Point(427, 124);
            this.lblk.Name = "lblk";
            this.lblk.Size = new System.Drawing.Size(23, 24);
            this.lblk.TabIndex = 63;
            this.lblk.Text = "K";
            this.lblk.Click += new System.EventHandler(this.lblk_Click);
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.BackColor = System.Drawing.Color.Transparent;
            this.lblW.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblW.ForeColor = System.Drawing.Color.Black;
            this.lblW.Location = new System.Drawing.Point(787, 124);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(23, 24);
            this.lblW.TabIndex = 74;
            this.lblW.Text = "W";
            this.lblW.Click += new System.EventHandler(this.lblW_Click);
            // 
            // lblL
            // 
            this.lblL.AutoSize = true;
            this.lblL.BackColor = System.Drawing.Color.Transparent;
            this.lblL.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblL.ForeColor = System.Drawing.Color.Black;
            this.lblL.Location = new System.Drawing.Point(463, 124);
            this.lblL.Name = "lblL";
            this.lblL.Size = new System.Drawing.Size(23, 24);
            this.lblL.TabIndex = 64;
            this.lblL.Text = "L";
            this.lblL.Click += new System.EventHandler(this.lblL_Click);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.BackColor = System.Drawing.Color.Transparent;
            this.lblX.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblX.ForeColor = System.Drawing.Color.Black;
            this.lblX.Location = new System.Drawing.Point(823, 124);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(23, 24);
            this.lblX.TabIndex = 73;
            this.lblX.Text = "X";
            this.lblX.Click += new System.EventHandler(this.lblX_Click);
            // 
            // lblM
            // 
            this.lblM.AutoSize = true;
            this.lblM.BackColor = System.Drawing.Color.Transparent;
            this.lblM.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblM.ForeColor = System.Drawing.Color.Black;
            this.lblM.Location = new System.Drawing.Point(499, 124);
            this.lblM.Name = "lblM";
            this.lblM.Size = new System.Drawing.Size(23, 24);
            this.lblM.TabIndex = 65;
            this.lblM.Text = "M";
            this.lblM.Click += new System.EventHandler(this.lblM_Click);
            // 
            // lblT
            // 
            this.lblT.AutoSize = true;
            this.lblT.BackColor = System.Drawing.Color.Transparent;
            this.lblT.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblT.ForeColor = System.Drawing.Color.Black;
            this.lblT.Location = new System.Drawing.Point(751, 124);
            this.lblT.Name = "lblT";
            this.lblT.Size = new System.Drawing.Size(23, 24);
            this.lblT.TabIndex = 72;
            this.lblT.Text = "T";
            this.lblT.Click += new System.EventHandler(this.lblT_Click);
            // 
            // lblN
            // 
            this.lblN.AutoSize = true;
            this.lblN.BackColor = System.Drawing.Color.Transparent;
            this.lblN.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblN.ForeColor = System.Drawing.Color.Black;
            this.lblN.Location = new System.Drawing.Point(535, 124);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(23, 24);
            this.lblN.TabIndex = 66;
            this.lblN.Text = "N";
            this.lblN.Click += new System.EventHandler(this.lblN_Click);
            // 
            // lblS
            // 
            this.lblS.AutoSize = true;
            this.lblS.BackColor = System.Drawing.Color.Transparent;
            this.lblS.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblS.ForeColor = System.Drawing.Color.Black;
            this.lblS.Location = new System.Drawing.Point(715, 124);
            this.lblS.Name = "lblS";
            this.lblS.Size = new System.Drawing.Size(23, 24);
            this.lblS.TabIndex = 71;
            this.lblS.Text = "S";
            this.lblS.Click += new System.EventHandler(this.lblS_Click);
            // 
            // lblO
            // 
            this.lblO.AutoSize = true;
            this.lblO.BackColor = System.Drawing.Color.Transparent;
            this.lblO.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblO.ForeColor = System.Drawing.Color.Black;
            this.lblO.Location = new System.Drawing.Point(571, 124);
            this.lblO.Name = "lblO";
            this.lblO.Size = new System.Drawing.Size(23, 24);
            this.lblO.TabIndex = 67;
            this.lblO.Text = "O";
            this.lblO.Click += new System.EventHandler(this.lblO_Click);
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.BackColor = System.Drawing.Color.Transparent;
            this.lblR.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblR.ForeColor = System.Drawing.Color.Black;
            this.lblR.Location = new System.Drawing.Point(679, 124);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(23, 24);
            this.lblR.TabIndex = 70;
            this.lblR.Text = "R";
            this.lblR.Click += new System.EventHandler(this.lblR_Click);
            // 
            // lblP
            // 
            this.lblP.AutoSize = true;
            this.lblP.BackColor = System.Drawing.Color.Transparent;
            this.lblP.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblP.ForeColor = System.Drawing.Color.Black;
            this.lblP.Location = new System.Drawing.Point(607, 124);
            this.lblP.Name = "lblP";
            this.lblP.Size = new System.Drawing.Size(23, 24);
            this.lblP.TabIndex = 68;
            this.lblP.Text = "P";
            this.lblP.Click += new System.EventHandler(this.lblP_Click);
            // 
            // lblQ
            // 
            this.lblQ.AutoSize = true;
            this.lblQ.BackColor = System.Drawing.Color.Transparent;
            this.lblQ.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.lblQ.ForeColor = System.Drawing.Color.Black;
            this.lblQ.Location = new System.Drawing.Point(643, 124);
            this.lblQ.Name = "lblQ";
            this.lblQ.Size = new System.Drawing.Size(23, 24);
            this.lblQ.TabIndex = 69;
            this.lblQ.Text = "Q";
            this.lblQ.Click += new System.EventHandler(this.lblQ_Click);
            // 
            // FrmOfficeChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOfficeChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOfficeChoose";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOfficeChoose_FormClosing);
            this.Load += new System.EventHandler(this.FrmOfficeChoose_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel plAll;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.Label lblH;
        private System.Windows.Forms.Label lblJ;
        private System.Windows.Forms.Label lblk;
        private System.Windows.Forms.Label lblL;
        private System.Windows.Forms.Label lblM;
        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.Label lblO;
        private System.Windows.Forms.Label lblP;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.Label lblS;
        private System.Windows.Forms.Label lblT;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblW;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pcReturn;
        private MenuList menuList1;
        private System.Windows.Forms.PictureBox pcExit;
        private Inc.BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.OfficeList officeList1;
        private Inc.UcTime ucTime1;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel1;
    }
}