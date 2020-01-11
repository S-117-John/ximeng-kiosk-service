namespace AutoServiceManage.Inquire
{
    partial class FrmStoredInquire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoredInquire));
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.gdcMain = new DevExpress.XtraGrid.GridControl();
            this.gdvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.pcReturn = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOneWeek = new System.Windows.Forms.Label();
            this.lblOneMoth = new System.Windows.Forms.Label();
            this.lblThreeMoth = new System.Windows.Forms.Label();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(29, 27);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 51;
            this.ucTime1.Size = new System.Drawing.Size(240, 38);
            this.ucTime1.TabIndex = 1;
            // 
            // gdcMain
            // 
            // 
            // 
            // 
            this.gdcMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gdcMain.Location = new System.Drawing.Point(44, 216);
            this.gdcMain.LookAndFeel.SkinName = "Blue";
            this.gdcMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gdcMain.MainView = this.gdvMain;
            this.gdcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gdcMain.Name = "gdcMain";
            this.gdcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gdcMain.Size = new System.Drawing.Size(1277, 500);
            this.gdcMain.TabIndex = 68;
            this.gdcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gdvMain});
            // 
            // gdvMain
            // 
            this.gdvMain.Appearance.FocusedRow.BackColor = System.Drawing.Color.Transparent;
            this.gdvMain.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.HeaderPanel.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gdvMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.gdvMain.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gdvMain.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gdvMain.Appearance.Row.BackColor = System.Drawing.Color.Transparent;
            this.gdvMain.Appearance.Row.BackColor2 = System.Drawing.Color.Transparent;
            this.gdvMain.Appearance.Row.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gdvMain.Appearance.Row.Options.UseBackColor = true;
            this.gdvMain.Appearance.Row.Options.UseFont = true;
            this.gdvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gdvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gdvMain.GridControl = this.gdcMain;
            this.gdvMain.Name = "gdvMain";
            this.gdvMain.OptionsView.ShowGroupPanel = false;
            this.gdvMain.RowHeight = 35;
            this.gdvMain.ViewCaptionHeight = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "日期";
            this.gridColumn1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "ADDMONEYDATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "金额";
            this.gridColumn2.FieldName = "ADDMONEY";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "类型";
            this.gridColumn3.FieldName = "BUSINESSTYPE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(1174, 0);
            this.pcExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(107, 100);
            this.pcExit.TabIndex = 85;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // pcReturn
            // 
            this.pcReturn.BackColor = System.Drawing.Color.Transparent;
            this.pcReturn.Image = ((System.Drawing.Image)(resources.GetObject("pcReturn.Image")));
            this.pcReturn.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcReturn.InitialImage")));
            this.pcReturn.Location = new System.Drawing.Point(980, 0);
            this.pcReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcReturn.Name = "pcReturn";
            this.pcReturn.Size = new System.Drawing.Size(107, 100);
            this.pcReturn.TabIndex = 84;
            this.pcReturn.TabStop = false;
            this.pcReturn.Click += new System.EventHandler(this.pcReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(52, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 30);
            this.label1.TabIndex = 86;
            this.label1.Text = "预存查询";
            // 
            // lblOneWeek
            // 
            this.lblOneWeek.BackColor = System.Drawing.Color.Transparent;
            this.lblOneWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOneWeek.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOneWeek.ForeColor = System.Drawing.Color.Transparent;
            this.lblOneWeek.Image = ((System.Drawing.Image)(resources.GetObject("lblOneWeek.Image")));
            this.lblOneWeek.Location = new System.Drawing.Point(52, 747);
            this.lblOneWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOneWeek.Name = "lblOneWeek";
            this.lblOneWeek.Size = new System.Drawing.Size(173, 67);
            this.lblOneWeek.TabIndex = 87;
            this.lblOneWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOneWeek.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // lblOneMoth
            // 
            this.lblOneMoth.BackColor = System.Drawing.Color.Transparent;
            this.lblOneMoth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOneMoth.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOneMoth.ForeColor = System.Drawing.Color.Transparent;
            this.lblOneMoth.Image = ((System.Drawing.Image)(resources.GetObject("lblOneMoth.Image")));
            this.lblOneMoth.Location = new System.Drawing.Point(305, 754);
            this.lblOneMoth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOneMoth.Name = "lblOneMoth";
            this.lblOneMoth.Size = new System.Drawing.Size(173, 53);
            this.lblOneMoth.TabIndex = 88;
            this.lblOneMoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOneMoth.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // lblThreeMoth
            // 
            this.lblThreeMoth.BackColor = System.Drawing.Color.Transparent;
            this.lblThreeMoth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblThreeMoth.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblThreeMoth.ForeColor = System.Drawing.Color.Transparent;
            this.lblThreeMoth.Image = ((System.Drawing.Image)(resources.GetObject("lblThreeMoth.Image")));
            this.lblThreeMoth.Location = new System.Drawing.Point(569, 747);
            this.lblThreeMoth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThreeMoth.Name = "lblThreeMoth";
            this.lblThreeMoth.Size = new System.Drawing.Size(173, 70);
            this.lblThreeMoth.TabIndex = 89;
            this.lblThreeMoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblThreeMoth.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(1, 1);
            this.newHead1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1365, 125);
            this.newHead1.TabIndex = 90;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.pcReturn);
            this.panel1.Controls.Add(this.pcExit);
            this.panel1.Location = new System.Drawing.Point(1, 860);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1365, 100);
            this.panel1.TabIndex = 91;
            // 
            // FrmStoredInquire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1365, 960);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.newHead1);
            this.Controls.Add(this.lblThreeMoth);
            this.Controls.Add(this.lblOneMoth);
            this.Controls.Add(this.lblOneWeek);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gdcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmStoredInquire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmStoredInquire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStoredInquire_FormClosing);
            this.Load += new System.EventHandler(this.FrmStoredInquire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Inc.UcTime ucTime1;
        private DevExpress.XtraGrid.GridControl gdcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gdvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.PictureBox pcExit;
        private System.Windows.Forms.PictureBox pcReturn;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.Label lblOneWeek;
        private System.Windows.Forms.Label lblOneMoth;
        private System.Windows.Forms.Label lblThreeMoth;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel1;
    }
}