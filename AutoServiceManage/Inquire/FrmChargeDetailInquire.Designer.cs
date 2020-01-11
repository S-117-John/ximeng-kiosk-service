namespace AutoServiceManage.Inquire
{
    partial class FrmChargeDetailInquire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChargeDetailInquire));
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.label1 = new System.Windows.Forms.Label();
            this.pcExit = new System.Windows.Forms.PictureBox();
            this.pcReturn = new System.Windows.Forms.PictureBox();
            this.gdcMain = new DevExpress.XtraGrid.GridControl();
            this.gdvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.lblThreeMoth = new System.Windows.Forms.Label();
            this.lblOneMoth = new System.Windows.Forms.Label();
            this.lblOneWeek = new System.Windows.Forms.Label();
            this.newHead1 = new AutoServiceManage.Inc.NewHead();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(711, 770);
            this.ucTime1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 49;
            this.ucTime1.Size = new System.Drawing.Size(240, 38);
            this.ucTime1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(51, 171);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 30);
            this.label1.TabIndex = 87;
            this.label1.Text = "收费明细查询";
            // 
            // pcExit
            // 
            this.pcExit.BackColor = System.Drawing.Color.Transparent;
            this.pcExit.Image = ((System.Drawing.Image)(resources.GetObject("pcExit.Image")));
            this.pcExit.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcExit.InitialImage")));
            this.pcExit.Location = new System.Drawing.Point(1179, 0);
            this.pcExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcExit.Name = "pcExit";
            this.pcExit.Size = new System.Drawing.Size(107, 100);
            this.pcExit.TabIndex = 89;
            this.pcExit.TabStop = false;
            this.pcExit.Click += new System.EventHandler(this.pcExit_Click);
            // 
            // pcReturn
            // 
            this.pcReturn.BackColor = System.Drawing.Color.Transparent;
            this.pcReturn.Image = ((System.Drawing.Image)(resources.GetObject("pcReturn.Image")));
            this.pcReturn.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcReturn.InitialImage")));
            this.pcReturn.Location = new System.Drawing.Point(1004, 0);
            this.pcReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcReturn.Name = "pcReturn";
            this.pcReturn.Size = new System.Drawing.Size(107, 100);
            this.pcReturn.TabIndex = 88;
            this.pcReturn.TabStop = false;
            this.pcReturn.Click += new System.EventHandler(this.pcReturn_Click);
            // 
            // gdcMain
            // 
            // 
            // 
            // 
            this.gdcMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gdcMain.Location = new System.Drawing.Point(44, 214);
            this.gdcMain.LookAndFeel.SkinName = "Blue";
            this.gdcMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gdcMain.MainView = this.gdvMain;
            this.gdcMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gdcMain.Name = "gdcMain";
            this.gdcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gdcMain.Size = new System.Drawing.Size(1277, 500);
            this.gdcMain.TabIndex = 90;
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
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn6});
            this.gdvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gdvMain.GridControl = this.gdcMain;
            this.gdvMain.Name = "gdvMain";
            this.gdvMain.OptionsView.ShowGroupPanel = false;
            this.gdvMain.RowHeight = 35;
            this.gdvMain.ViewCaptionHeight = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "类型";
            this.gridColumn7.FieldName = "TYPE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn7.OptionsFilter.AllowFilter = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 50;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "项目名称";
            this.gridColumn1.FieldName = "NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 180;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单价";
            this.gridColumn2.DisplayFormat.FormatString = "0.00";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "UNITPRICE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 70;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "数量";
            this.gridColumn3.DisplayFormat.FormatString = "0.00";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "AMOUNT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "自付金额";
            this.gridColumn5.DisplayFormat.FormatString = "0.00";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "SELFMONEY";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "发票号";
            this.gridColumn4.FieldName = "INVOICEID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "打票时间";
            this.gridColumn8.FieldName = "INVOICEDATE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn8.OptionsFilter.AllowFilter = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "收费时间";
            this.gridColumn6.FieldName = "OPERATEDATE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.AllowFilter = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 80;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // lblThreeMoth
            // 
            this.lblThreeMoth.BackColor = System.Drawing.Color.Transparent;
            this.lblThreeMoth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblThreeMoth.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblThreeMoth.ForeColor = System.Drawing.Color.Transparent;
            this.lblThreeMoth.Image = ((System.Drawing.Image)(resources.GetObject("lblThreeMoth.Image")));
            this.lblThreeMoth.Location = new System.Drawing.Point(487, 770);
            this.lblThreeMoth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThreeMoth.Name = "lblThreeMoth";
            this.lblThreeMoth.Size = new System.Drawing.Size(173, 58);
            this.lblThreeMoth.TabIndex = 93;
            this.lblThreeMoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblThreeMoth.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // lblOneMoth
            // 
            this.lblOneMoth.BackColor = System.Drawing.Color.Transparent;
            this.lblOneMoth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOneMoth.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOneMoth.ForeColor = System.Drawing.Color.Transparent;
            this.lblOneMoth.Image = ((System.Drawing.Image)(resources.GetObject("lblOneMoth.Image")));
            this.lblOneMoth.Location = new System.Drawing.Point(254, 768);
            this.lblOneMoth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOneMoth.Name = "lblOneMoth";
            this.lblOneMoth.Size = new System.Drawing.Size(173, 60);
            this.lblOneMoth.TabIndex = 92;
            this.lblOneMoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOneMoth.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // lblOneWeek
            // 
            this.lblOneWeek.BackColor = System.Drawing.Color.Transparent;
            this.lblOneWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOneWeek.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOneWeek.ForeColor = System.Drawing.Color.Transparent;
            this.lblOneWeek.Image = ((System.Drawing.Image)(resources.GetObject("lblOneWeek.Image")));
            this.lblOneWeek.Location = new System.Drawing.Point(51, 768);
            this.lblOneWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOneWeek.Name = "lblOneWeek";
            this.lblOneWeek.Size = new System.Drawing.Size(173, 60);
            this.lblOneWeek.TabIndex = 91;
            this.lblOneWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOneWeek.Click += new System.EventHandler(this.lblOneWeek_Click);
            // 
            // newHead1
            // 
            this.newHead1.Location = new System.Drawing.Point(-1, -2);
            this.newHead1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.newHead1.Name = "newHead1";
            this.newHead1.Size = new System.Drawing.Size(1365, 125);
            this.newHead1.TabIndex = 94;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pcReturn);
            this.panel1.Controls.Add(this.pcExit);
            this.panel1.Location = new System.Drawing.Point(-1, 857);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1365, 100);
            this.panel1.TabIndex = 95;
            // 
            // FrmChargeDetailInquire
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
            this.Controls.Add(this.gdcMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucTime1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmChargeDetailInquire";
            this.Text = "FrmChargeDetailInquire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChargeDetailInquire_FormClosing);
            this.Load += new System.EventHandler(this.FrmChargeDetailInquire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Inc.UcTime ucTime1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcExit;
        private System.Windows.Forms.PictureBox pcReturn;
        private DevExpress.XtraGrid.GridControl gdcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gdvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Label lblThreeMoth;
        private System.Windows.Forms.Label lblOneMoth;
        private System.Windows.Forms.Label lblOneWeek;
        private Inc.NewHead newHead1;
        private System.Windows.Forms.Panel panel1;
    }
}