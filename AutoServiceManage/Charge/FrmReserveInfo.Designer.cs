namespace AutoServiceManage.Charge
{
    partial class FrmReserveInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReserveInfo));
            this.backGroundPanelTrend1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.gbDate = new System.Windows.Forms.GroupBox();
            this.monthCalendar1 = new Pabo.Calendar.MonthCalendar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.gbSD = new System.Windows.Forms.GroupBox();
            this.lblWarn = new System.Windows.Forms.Label();
            this.gridControlDate = new DevExpress.XtraGrid.GridControl();
            this.gridViewDate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEnableNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnReservedNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblCancel = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.backGroundPanelTrend1.SuspendLayout();
            this.gbDate.SuspendLayout();
            this.gbSD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDate)).BeginInit();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackColor = System.Drawing.Color.Transparent;
            this.backGroundPanelTrend1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backGroundPanelTrend1.BackgroundImage")));
            this.backGroundPanelTrend1.Controls.Add(this.label1);
            this.backGroundPanelTrend1.Controls.Add(this.lblGroup);
            this.backGroundPanelTrend1.Controls.Add(this.gbDate);
            this.backGroundPanelTrend1.Controls.Add(this.gbSD);
            this.backGroundPanelTrend1.Controls.Add(this.lblCancel);
            this.backGroundPanelTrend1.Controls.Add(this.lblOK);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backGroundPanelTrend1.ForeColor = System.Drawing.Color.Transparent;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(814, 515);
            this.backGroundPanelTrend1.TabIndex = 65;
            this.backGroundPanelTrend1.Paint += new System.Windows.Forms.PaintEventHandler(this.backGroundPanelTrend1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(433, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 86;
            this.label1.Text = "所属分组：";
            // 
            // lblGroup
            // 
            this.lblGroup.BackColor = System.Drawing.Color.White;
            this.lblGroup.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGroup.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblGroup.Location = new System.Drawing.Point(561, 22);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(242, 35);
            this.lblGroup.TabIndex = 83;
            this.lblGroup.Text = "所属分组";
            // 
            // gbDate
            // 
            this.gbDate.Controls.Add(this.monthCalendar1);
            this.gbDate.Controls.Add(this.label2);
            this.gbDate.Controls.Add(this.lblDate);
            this.gbDate.Location = new System.Drawing.Point(18, 12);
            this.gbDate.Name = "gbDate";
            this.gbDate.Size = new System.Drawing.Size(407, 491);
            this.gbDate.TabIndex = 85;
            this.gbDate.TabStop = false;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.ActiveMonth.Month = 7;
            this.monthCalendar1.ActiveMonth.Year = 2016;
            this.monthCalendar1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None;
            this.monthCalendar1.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.monthCalendar1.Footer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.monthCalendar1.Header.BackColor1 = System.Drawing.Color.White;
            this.monthCalendar1.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold);
            this.monthCalendar1.Header.TextColor = System.Drawing.Color.DodgerBlue;
            this.monthCalendar1.ImageList = null;
            this.monthCalendar1.Location = new System.Drawing.Point(6, 57);
            this.monthCalendar1.MaxDate = new System.DateTime(2026, 7, 18, 17, 37, 19, 330);
            this.monthCalendar1.MinDate = new System.DateTime(2006, 7, 18, 17, 37, 19, 330);
            this.monthCalendar1.Month.BackgroundImage = null;
            this.monthCalendar1.Month.Colors.Days.BackColor1 = System.Drawing.Color.LightSkyBlue;
            this.monthCalendar1.Month.Colors.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.monthCalendar1.Month.Colors.Selected.Border = System.Drawing.Color.White;
            this.monthCalendar1.Month.Colors.Trailing.BackColor1 = System.Drawing.Color.LightSkyBlue;
            this.monthCalendar1.Month.Colors.Trailing.Date = System.Drawing.Color.Black;
            this.monthCalendar1.Month.Colors.Trailing.Text = System.Drawing.Color.Black;
            this.monthCalendar1.Month.Colors.Weekend.BackColor1 = System.Drawing.Color.LightSkyBlue;
            this.monthCalendar1.Month.DateFont = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.monthCalendar1.Month.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
            this.monthCalendar1.ShowFooter = false;
            this.monthCalendar1.Size = new System.Drawing.Size(395, 428);
            this.monthCalendar1.TabIndex = 88;
            this.monthCalendar1.Weekdays.Font = new System.Drawing.Font("宋体", 14F);
            this.monthCalendar1.Weekdays.TextColor = System.Drawing.Color.Black;
            this.monthCalendar1.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.monthCalendar1.Weeknumbers.TextColor = System.Drawing.Color.DodgerBlue;
            this.monthCalendar1.DayRender += new Pabo.Calendar.DayRenderEventHandler(this.monthCalendar1_DayRender);
            this.monthCalendar1.DayClick += new Pabo.Calendar.DayClickEventHandler(this.monthCalendar1_DayClick);
            this.monthCalendar1.DaySelected += new Pabo.Calendar.DaySelectedEventHandler(this.monthCalendar1_DaySelected);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 87;
            this.label2.Text = "所选时段：";
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.White;
            this.lblDate.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblDate.Location = new System.Drawing.Point(144, 13);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(257, 35);
            this.lblDate.TabIndex = 82;
            this.lblDate.Text = "请选择预约日期";
            // 
            // gbSD
            // 
            this.gbSD.Controls.Add(this.lblWarn);
            this.gbSD.Controls.Add(this.gridControlDate);
            this.gbSD.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbSD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.gbSD.Location = new System.Drawing.Point(431, 69);
            this.gbSD.Name = "gbSD";
            this.gbSD.Size = new System.Drawing.Size(367, 372);
            this.gbSD.TabIndex = 80;
            this.gbSD.TabStop = false;
            this.gbSD.Text = "请选择时段";
            this.gbSD.Paint += new System.Windows.Forms.PaintEventHandler(this.gbSD_Paint);
            // 
            // lblWarn
            // 
            this.lblWarn.BackColor = System.Drawing.Color.White;
            this.lblWarn.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarn.ForeColor = System.Drawing.Color.Red;
            this.lblWarn.Location = new System.Drawing.Point(42, 131);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(300, 78);
            this.lblWarn.TabIndex = 88;
            this.lblWarn.Text = "该日期没有排班，请重新选择预约日期。";
            // 
            // gridControlDate
            // 
            this.gridControlDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gridControlDate.Location = new System.Drawing.Point(8, 24);
            this.gridControlDate.LookAndFeel.SkinName = "Blue";
            this.gridControlDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlDate.MainView = this.gridViewDate;
            this.gridControlDate.Name = "gridControlDate";
            this.gridControlDate.Size = new System.Drawing.Size(353, 342);
            this.gridControlDate.TabIndex = 79;
            this.gridControlDate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDate});
            // 
            // gridViewDate
            // 
            this.gridViewDate.Appearance.EvenRow.BackColor = System.Drawing.Color.DodgerBlue;
            this.gridViewDate.Appearance.EvenRow.BackColor2 = System.Drawing.Color.White;
            this.gridViewDate.Appearance.EvenRow.BorderColor = System.Drawing.Color.Linen;
            this.gridViewDate.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewDate.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewDate.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gridViewDate.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridViewDate.Appearance.FocusedRow.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gridViewDate.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightSkyBlue;
            this.gridViewDate.Appearance.FocusedRow.BorderColor = System.Drawing.Color.MistyRose;
            this.gridViewDate.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewDate.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewDate.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridViewDate.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewDate.Appearance.OddRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewDate.Appearance.OddRow.BackColor2 = System.Drawing.Color.Transparent;
            this.gridViewDate.Appearance.OddRow.BorderColor = System.Drawing.Color.AliceBlue;
            this.gridViewDate.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewDate.Appearance.OddRow.Options.UseBackColor = true;
            this.gridViewDate.Appearance.OddRow.Options.UseBorderColor = true;
            this.gridViewDate.Appearance.OddRow.Options.UseForeColor = true;
            this.gridViewDate.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridViewDate.Appearance.Row.Options.UseFont = true;
            this.gridViewDate.Appearance.SelectedRow.BackColor = System.Drawing.Color.Blue;
            this.gridViewDate.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.Blue;
            this.gridViewDate.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridViewDate.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewDate.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewDate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTime,
            this.gridColumnEnableNo,
            this.gridColumnReservedNo,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn15,
            this.gridColumn1});
            this.gridViewDate.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewDate.GridControl = this.gridControlDate;
            this.gridViewDate.Name = "gridViewDate";
            this.gridViewDate.OptionsBehavior.Editable = false;
            this.gridViewDate.OptionsBehavior.ReadOnly = true;
            this.gridViewDate.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDate.OptionsView.ShowColumnHeaders = false;
            this.gridViewDate.OptionsView.ShowGroupPanel = false;
            this.gridViewDate.RowHeight = 40;
            this.gridViewDate.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            // 
            // gridColumnTime
            // 
            this.gridColumnTime.Caption = "时间段";
            this.gridColumnTime.FieldName = "PERIOD";
            this.gridColumnTime.Name = "gridColumnTime";
            this.gridColumnTime.OptionsColumn.AllowEdit = false;
            this.gridColumnTime.Visible = true;
            this.gridColumnTime.VisibleIndex = 0;
            this.gridColumnTime.Width = 116;
            // 
            // gridColumnEnableNo
            // 
            this.gridColumnEnableNo.Caption = "可预约数";
            this.gridColumnEnableNo.FieldName = "reserveEnableNo";
            this.gridColumnEnableNo.Name = "gridColumnEnableNo";
            this.gridColumnEnableNo.OptionsColumn.AllowEdit = false;
            this.gridColumnEnableNo.Width = 66;
            // 
            // gridColumnReservedNo
            // 
            this.gridColumnReservedNo.Caption = "已预约数";
            this.gridColumnReservedNo.FieldName = "reservedNo";
            this.gridColumnReservedNo.Name = "gridColumnReservedNo";
            this.gridColumnReservedNo.OptionsColumn.AllowEdit = false;
            this.gridColumnReservedNo.Width = 65;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "开始时间";
            this.gridColumn6.FieldName = "STARTTIME";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "结束时间";
            this.gridColumn11.FieldName = "ENDTIME";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "预约最大号";
            this.gridColumn12.FieldName = "RESERVEMAXNUM";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Width = 48;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "明细ID";
            this.gridColumn15.FieldName = "MEDGROUPRDETAILID";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "reserveEnableNew";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // lblCancel
            // 
            this.lblCancel.BackColor = System.Drawing.Color.Transparent;
            this.lblCancel.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblCancel.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.lblCancel.Location = new System.Drawing.Point(659, 453);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(133, 44);
            this.lblCancel.TabIndex = 78;
            this.lblCancel.Text = "取消";
            this.lblCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancel.Click += new System.EventHandler(this.lblCancel_Click);
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblOK.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.lblOK.Location = new System.Drawing.Point(435, 453);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(133, 44);
            this.lblOK.TabIndex = 77;
            this.lblOK.Text = "确认预约";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // FrmReserveInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(814, 515);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReserveInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReserveInfo";
            this.TransparencyKey = System.Drawing.Color.DarkGray;
            this.Load += new System.EventHandler(this.FrmReserveInfo_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            this.gbDate.ResumeLayout(false);
            this.gbDate.PerformLayout();
            this.gbSD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel backGroundPanelTrend1;
        private System.Windows.Forms.Label lblCancel;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.GroupBox gbSD;
        private DevExpress.XtraGrid.GridControl gridControlDate;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTime;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEnableNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnReservedNo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox gbDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWarn;
        private Pabo.Calendar.MonthCalendar monthCalendar1;


    }
}