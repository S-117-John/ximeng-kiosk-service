namespace SunRise.HOSP.CLIENT
{
    partial class FrmCameraReplay
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
            this.gvPager = new SunRise.HOSP.ExControl.DataGridViewPager();
            this.pbCapture = new System.Windows.Forms.PictureBox();
            this.btnResume = new SunRise.HOSP.ExControl.TransButton();
            this.btnPause = new SunRise.HOSP.ExControl.TransButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnPlay = new SunRise.HOSP.ExControl.TransButton();
            this.btn_Next = new SunRise.HOSP.ExControl.TransButton();
            this.btn_Prev = new SunRise.HOSP.ExControl.TransButton();
            this.lbPage = new System.Windows.Forms.Label();
            this.btnQuery = new SunRise.HOSP.ExControl.TransButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lbState = new System.Windows.Forms.Label();
            this.rbCameraSelAll = new System.Windows.Forms.RadioButton();
            this.rbCameraSelTop = new System.Windows.Forms.RadioButton();
            this.rbCameraSelBottom = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCameraFast = new System.Windows.Forms.RadioButton();
            this.rbCameraNormal = new System.Windows.Forms.RadioButton();
            this.rbCameraSlow = new System.Windows.Forms.RadioButton();
            this.btnClose = new SunRise.HOSP.ExControl.TransButton();
            this.btnTestCamera = new SunRise.HOSP.ExControl.TransButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvPager
            // 
            this.gvPager.AutoSize = true;
            this.gvPager.BackgroundImageEx = null;
            this.gvPager.ControlNext = null;
            this.gvPager.ControlPageInfo = null;
            this.gvPager.ControlPrev = null;
            this.gvPager.DataSource = null;
            this.gvPager.DoBindDataSource = null;
            this.gvPager.DoPreBindFirstSource = null;
            this.gvPager.Location = new System.Drawing.Point(12, 92);
            this.gvPager.Name = "gvPager";
            this.gvPager.PagerSize = 0;
            this.gvPager.Size = new System.Drawing.Size(573, 613);
            this.gvPager.TabIndex = 88;
            // 
            // pbCapture
            // 
            this.pbCapture.BackColor = System.Drawing.Color.Transparent;
            this.pbCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCapture.Location = new System.Drawing.Point(591, 92);
            this.pbCapture.Name = "pbCapture";
            this.pbCapture.Size = new System.Drawing.Size(423, 328);
            this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCapture.TabIndex = 89;
            this.pbCapture.TabStop = false;
            // 
            // btnResume
            // 
            this.btnResume.BackColor = System.Drawing.Color.Transparent;
            this.btnResume.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnResume.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnResume.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnResume.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnResume.DefaultFontOffsetX = 0;
            this.btnResume.FadingSpeed = 20;
            this.btnResume.FlatAppearance.BorderSize = 0;
            this.btnResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResume.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnResume.ForeColor = System.Drawing.Color.Transparent;
            this.btnResume.ImgDisable = null;
            this.btnResume.ImgDisablePath = null;
            this.btnResume.ImgEnter = null;
            this.btnResume.ImgEnterPath = null;
            this.btnResume.ImgNormal = null;
            this.btnResume.ImgNormalPath = null;
            this.btnResume.ImgPress = null;
            this.btnResume.ImgPressPath = null;
            this.btnResume.Location = new System.Drawing.Point(884, 498);
            this.btnResume.Name = "btnResume";
            this.btnResume.PressFontSize = 12;
            this.btnResume.Radius = 20;
            this.btnResume.ShowGradient = false;
            this.btnResume.ShowText = true;
            this.btnResume.Size = new System.Drawing.Size(120, 51);
            this.btnResume.SplitDraw = true;
            this.btnResume.Stretch = false;
            this.btnResume.TabIndex = 93;
            this.btnResume.Text = "继续";
            this.btnResume.UseDefaultImg = true;
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPause.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnPause.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnPause.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnPause.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnPause.DefaultFontOffsetX = 0;
            this.btnPause.FadingSpeed = 20;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnPause.ForeColor = System.Drawing.Color.Transparent;
            this.btnPause.ImgDisable = null;
            this.btnPause.ImgDisablePath = null;
            this.btnPause.ImgEnter = null;
            this.btnPause.ImgEnterPath = null;
            this.btnPause.ImgNormal = null;
            this.btnPause.ImgNormalPath = null;
            this.btnPause.ImgPress = null;
            this.btnPause.ImgPressPath = null;
            this.btnPause.Location = new System.Drawing.Point(747, 498);
            this.btnPause.Name = "btnPause";
            this.btnPause.PressFontSize = 12;
            this.btnPause.Radius = 20;
            this.btnPause.ShowGradient = false;
            this.btnPause.ShowText = true;
            this.btnPause.Size = new System.Drawing.Size(120, 51);
            this.btnPause.SplitDraw = true;
            this.btnPause.Stretch = false;
            this.btnPause.TabIndex = 92;
            this.btnPause.Text = "暂停";
            this.btnPause.UseDefaultImg = true;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.Color.White;
            this.trackBar1.Location = new System.Drawing.Point(12, 711);
            this.trackBar1.Maximum = 0;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(1000, 45);
            this.trackBar1.TabIndex = 91;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnPlay.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnPlay.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnPlay.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnPlay.DefaultFontOffsetX = 0;
            this.btnPlay.FadingSpeed = 20;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnPlay.ForeColor = System.Drawing.Color.Transparent;
            this.btnPlay.ImgDisable = null;
            this.btnPlay.ImgDisablePath = null;
            this.btnPlay.ImgEnter = null;
            this.btnPlay.ImgEnterPath = null;
            this.btnPlay.ImgNormal = null;
            this.btnPlay.ImgNormalPath = null;
            this.btnPlay.ImgPress = null;
            this.btnPlay.ImgPressPath = null;
            this.btnPlay.Location = new System.Drawing.Point(610, 498);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.PressFontSize = 12;
            this.btnPlay.Radius = 20;
            this.btnPlay.ShowGradient = false;
            this.btnPlay.ShowText = true;
            this.btnPlay.Size = new System.Drawing.Size(120, 51);
            this.btnPlay.SplitDraw = true;
            this.btnPlay.Stretch = false;
            this.btnPlay.TabIndex = 90;
            this.btnPlay.Text = "播放";
            this.btnPlay.UseDefaultImg = true;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.BackColor = System.Drawing.Color.Transparent;
            this.btn_Next.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_Next.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btn_Next.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btn_Next.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btn_Next.DefaultFontOffsetX = 0;
            this.btn_Next.FadingSpeed = 10;
            this.btn_Next.FlatAppearance.BorderSize = 0;
            this.btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Next.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btn_Next.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Next.ImgDisable = null;
            this.btn_Next.ImgDisablePath = null;
            this.btn_Next.ImgEnter = null;
            this.btn_Next.ImgEnterPath = null;
            this.btn_Next.ImgNormal = null;
            this.btn_Next.ImgNormalPath = null;
            this.btn_Next.ImgPress = null;
            this.btn_Next.ImgPressPath = null;
            this.btn_Next.Location = new System.Drawing.Point(884, 426);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.PressFontSize = 12;
            this.btn_Next.Radius = 20;
            this.btn_Next.ShowGradient = false;
            this.btn_Next.ShowText = true;
            this.btn_Next.Size = new System.Drawing.Size(120, 51);
            this.btn_Next.SplitDraw = true;
            this.btn_Next.Stretch = false;
            this.btn_Next.TabIndex = 95;
            this.btn_Next.Text = "下一页";
            this.btn_Next.UseDefaultImg = true;
            this.btn_Next.UseVisualStyleBackColor = false;
            // 
            // btn_Prev
            // 
            this.btn_Prev.BackColor = System.Drawing.Color.Transparent;
            this.btn_Prev.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btn_Prev.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btn_Prev.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btn_Prev.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btn_Prev.DefaultFontOffsetX = 0;
            this.btn_Prev.FadingSpeed = 10;
            this.btn_Prev.FlatAppearance.BorderSize = 0;
            this.btn_Prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Prev.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btn_Prev.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Prev.ImgDisable = null;
            this.btn_Prev.ImgDisablePath = null;
            this.btn_Prev.ImgEnter = null;
            this.btn_Prev.ImgEnterPath = null;
            this.btn_Prev.ImgNormal = null;
            this.btn_Prev.ImgNormalPath = null;
            this.btn_Prev.ImgPress = null;
            this.btn_Prev.ImgPressPath = null;
            this.btn_Prev.Location = new System.Drawing.Point(610, 426);
            this.btn_Prev.Name = "btn_Prev";
            this.btn_Prev.PressFontSize = 12;
            this.btn_Prev.Radius = 20;
            this.btn_Prev.ShowGradient = false;
            this.btn_Prev.ShowText = true;
            this.btn_Prev.Size = new System.Drawing.Size(120, 51);
            this.btn_Prev.SplitDraw = true;
            this.btn_Prev.Stretch = false;
            this.btn_Prev.TabIndex = 94;
            this.btn_Prev.Text = "上一页";
            this.btn_Prev.UseDefaultImg = true;
            this.btn_Prev.UseVisualStyleBackColor = false;
            // 
            // lbPage
            // 
            this.lbPage.BackColor = System.Drawing.Color.Transparent;
            this.lbPage.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lbPage.Location = new System.Drawing.Point(747, 426);
            this.lbPage.Name = "lbPage";
            this.lbPage.Size = new System.Drawing.Size(120, 51);
            this.lbPage.TabIndex = 96;
            this.lbPage.Text = "0/0";
            this.lbPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Transparent;
            this.btnQuery.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnQuery.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnQuery.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnQuery.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnQuery.DefaultFontOffsetX = 0;
            this.btnQuery.FadingSpeed = 20;
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnQuery.ForeColor = System.Drawing.Color.Transparent;
            this.btnQuery.ImgDisable = null;
            this.btnQuery.ImgDisablePath = null;
            this.btnQuery.ImgEnter = null;
            this.btnQuery.ImgEnterPath = null;
            this.btnQuery.ImgNormal = null;
            this.btnQuery.ImgNormalPath = null;
            this.btnQuery.ImgPress = null;
            this.btnQuery.ImgPressPath = null;
            this.btnQuery.Location = new System.Drawing.Point(598, 37);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.PressFontSize = 12;
            this.btnQuery.Radius = 20;
            this.btnQuery.ShowGradient = false;
            this.btnQuery.ShowText = true;
            this.btnQuery.Size = new System.Drawing.Size(113, 45);
            this.btnQuery.SplitDraw = true;
            this.btnQuery.Stretch = false;
            this.btnQuery.TabIndex = 98;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseDefaultImg = true;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("微软雅黑", 14F);
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(125, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 32);
            this.dateTimePicker1.TabIndex = 97;
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.BackColor = System.Drawing.Color.Transparent;
            this.lbState.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lbState.Location = new System.Drawing.Point(615, 656);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(50, 25);
            this.lbState.TabIndex = 99;
            this.lbState.Text = "状态";
            // 
            // rbCameraSelAll
            // 
            this.rbCameraSelAll.AutoSize = true;
            this.rbCameraSelAll.Checked = true;
            this.rbCameraSelAll.Location = new System.Drawing.Point(41, 24);
            this.rbCameraSelAll.Name = "rbCameraSelAll";
            this.rbCameraSelAll.Size = new System.Drawing.Size(68, 29);
            this.rbCameraSelAll.TabIndex = 100;
            this.rbCameraSelAll.TabStop = true;
            this.rbCameraSelAll.Text = "全部";
            this.rbCameraSelAll.UseVisualStyleBackColor = true;
            this.rbCameraSelAll.CheckedChanged += new System.EventHandler(this.rbCameraSelAll_CheckedChanged);
            // 
            // rbCameraSelTop
            // 
            this.rbCameraSelTop.AutoSize = true;
            this.rbCameraSelTop.Location = new System.Drawing.Point(137, 26);
            this.rbCameraSelTop.Name = "rbCameraSelTop";
            this.rbCameraSelTop.Size = new System.Drawing.Size(49, 29);
            this.rbCameraSelTop.TabIndex = 101;
            this.rbCameraSelTop.Text = "上";
            this.rbCameraSelTop.UseVisualStyleBackColor = true;
            this.rbCameraSelTop.CheckedChanged += new System.EventHandler(this.rbCameraSelTop_CheckedChanged);
            // 
            // rbCameraSelBottom
            // 
            this.rbCameraSelBottom.AutoSize = true;
            this.rbCameraSelBottom.Location = new System.Drawing.Point(228, 29);
            this.rbCameraSelBottom.Name = "rbCameraSelBottom";
            this.rbCameraSelBottom.Size = new System.Drawing.Size(49, 29);
            this.rbCameraSelBottom.TabIndex = 101;
            this.rbCameraSelBottom.Text = "下";
            this.rbCameraSelBottom.UseVisualStyleBackColor = true;
            this.rbCameraSelBottom.CheckedChanged += new System.EventHandler(this.rbCameraSelBottom_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rbCameraSelBottom);
            this.groupBox1.Controls.Add(this.rbCameraSelAll);
            this.groupBox1.Controls.Add(this.rbCameraSelTop);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox1.Location = new System.Drawing.Point(300, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 60);
            this.groupBox1.TabIndex = 102;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "摄像头位置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 103;
            this.label1.Text = "采集日期：";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rbCameraFast);
            this.groupBox2.Controls.Add(this.rbCameraNormal);
            this.groupBox2.Controls.Add(this.rbCameraSlow);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.groupBox2.Location = new System.Drawing.Point(626, 555);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 65);
            this.groupBox2.TabIndex = 104;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "播放速度";
            // 
            // rbCameraFast
            // 
            this.rbCameraFast.AutoSize = true;
            this.rbCameraFast.Location = new System.Drawing.Point(247, 29);
            this.rbCameraFast.Name = "rbCameraFast";
            this.rbCameraFast.Size = new System.Drawing.Size(49, 29);
            this.rbCameraFast.TabIndex = 0;
            this.rbCameraFast.Text = "快";
            this.rbCameraFast.UseVisualStyleBackColor = true;
            this.rbCameraFast.CheckedChanged += new System.EventHandler(this.rbCameraFast_CheckedChanged);
            // 
            // rbCameraNormal
            // 
            this.rbCameraNormal.AutoSize = true;
            this.rbCameraNormal.Checked = true;
            this.rbCameraNormal.Location = new System.Drawing.Point(146, 29);
            this.rbCameraNormal.Name = "rbCameraNormal";
            this.rbCameraNormal.Size = new System.Drawing.Size(68, 29);
            this.rbCameraNormal.TabIndex = 0;
            this.rbCameraNormal.TabStop = true;
            this.rbCameraNormal.Text = "正常";
            this.rbCameraNormal.UseVisualStyleBackColor = true;
            this.rbCameraNormal.CheckedChanged += new System.EventHandler(this.rbCameraNormal_CheckedChanged);
            // 
            // rbCameraSlow
            // 
            this.rbCameraSlow.AutoSize = true;
            this.rbCameraSlow.Location = new System.Drawing.Point(32, 29);
            this.rbCameraSlow.Name = "rbCameraSlow";
            this.rbCameraSlow.Size = new System.Drawing.Size(49, 29);
            this.rbCameraSlow.TabIndex = 0;
            this.rbCameraSlow.Text = "慢";
            this.rbCameraSlow.UseVisualStyleBackColor = true;
            this.rbCameraSlow.CheckedChanged += new System.EventHandler(this.rbCameraSlow_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnClose.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnClose.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnClose.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnClose.DefaultFontOffsetX = 0;
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
            this.btnClose.Location = new System.Drawing.Point(891, 37);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressFontSize = 12;
            this.btnClose.Radius = 20;
            this.btnClose.ShowGradient = false;
            this.btnClose.ShowText = true;
            this.btnClose.Size = new System.Drawing.Size(113, 45);
            this.btnClose.SplitDraw = true;
            this.btnClose.Stretch = false;
            this.btnClose.TabIndex = 105;
            this.btnClose.Text = "退出";
            this.btnClose.UseDefaultImg = true;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTestCamera
            // 
            this.btnTestCamera.BackColor = System.Drawing.Color.Transparent;
            this.btnTestCamera.ColorDisable = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnTestCamera.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnTestCamera.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(186)))), ((int)(((byte)(209)))), ((int)(((byte)(240)))));
            this.btnTestCamera.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnTestCamera.DefaultFontOffsetX = 0;
            this.btnTestCamera.FadingSpeed = 20;
            this.btnTestCamera.FlatAppearance.BorderSize = 0;
            this.btnTestCamera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestCamera.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnTestCamera.ForeColor = System.Drawing.Color.Transparent;
            this.btnTestCamera.ImgDisable = null;
            this.btnTestCamera.ImgDisablePath = null;
            this.btnTestCamera.ImgEnter = null;
            this.btnTestCamera.ImgEnterPath = null;
            this.btnTestCamera.ImgNormal = null;
            this.btnTestCamera.ImgNormalPath = null;
            this.btnTestCamera.ImgPress = null;
            this.btnTestCamera.ImgPressPath = null;
            this.btnTestCamera.Location = new System.Drawing.Point(754, 37);
            this.btnTestCamera.Name = "btnTestCamera";
            this.btnTestCamera.PressFontSize = 12;
            this.btnTestCamera.Radius = 20;
            this.btnTestCamera.ShowGradient = false;
            this.btnTestCamera.ShowText = true;
            this.btnTestCamera.Size = new System.Drawing.Size(113, 45);
            this.btnTestCamera.SplitDraw = true;
            this.btnTestCamera.Stretch = false;
            this.btnTestCamera.TabIndex = 106;
            this.btnTestCamera.Text = "测试位置";
            this.btnTestCamera.UseDefaultImg = true;
            this.btnTestCamera.UseVisualStyleBackColor = true;
            this.btnTestCamera.Visible = false;
            this.btnTestCamera.Click += new System.EventHandler(this.btnTestCamera_Click);
            // 
            // FrmCameraReplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btnTestCamera);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Prev);
            this.Controls.Add(this.lbPage);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.pbCapture);
            this.Controls.Add(this.gvPager);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCameraReplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraReplay";
            this.Load += new System.EventHandler(this.CameraReplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HOSP.ExControl.DataGridViewPager gvPager;
        private System.Windows.Forms.PictureBox pbCapture;
        private HOSP.ExControl.TransButton btnResume;
        private HOSP.ExControl.TransButton btnPause;
        private System.Windows.Forms.TrackBar trackBar1;
        private HOSP.ExControl.TransButton btnPlay;
        private HOSP.ExControl.TransButton btn_Next;
        private HOSP.ExControl.TransButton btn_Prev;
        private System.Windows.Forms.Label lbPage;
        private HOSP.ExControl.TransButton btnQuery;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.RadioButton rbCameraSelAll;
        private System.Windows.Forms.RadioButton rbCameraSelTop;
        private System.Windows.Forms.RadioButton rbCameraSelBottom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCameraFast;
        private System.Windows.Forms.RadioButton rbCameraNormal;
        private System.Windows.Forms.RadioButton rbCameraSlow;
        private HOSP.ExControl.TransButton btnClose;
        private ExControl.TransButton btnTestCamera;
    }
}