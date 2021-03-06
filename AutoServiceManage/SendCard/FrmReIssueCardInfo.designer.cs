﻿using AutoServiceManage.Inc;
namespace AutoServiceManage.SendCard
{
    partial class FrmReIssueCardInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReIssueCardInfo));
            this.backGroundPanelTrend1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.gdcMain = new DevExpress.XtraGrid.GridControl();
            this.gdvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExit = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.backGroundPanelTrend1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // backGroundPanelTrend1
            // 
            this.backGroundPanelTrend1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.backGroundPanelTrend1.Controls.Add(this.label7);
            this.backGroundPanelTrend1.Controls.Add(this.label3);
            this.backGroundPanelTrend1.Controls.Add(this.label12);
            this.backGroundPanelTrend1.Controls.Add(this.lblPatientName);
            this.backGroundPanelTrend1.Controls.Add(this.label2);
            this.backGroundPanelTrend1.Controls.Add(this.lblOK);
            this.backGroundPanelTrend1.Controls.Add(this.gdcMain);
            this.backGroundPanelTrend1.Controls.Add(this.btnExit);
            this.backGroundPanelTrend1.Controls.Add(this.ucTime1);
            this.backGroundPanelTrend1.Controls.Add(this.ucHead1);
            this.backGroundPanelTrend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backGroundPanelTrend1.Location = new System.Drawing.Point(0, 0);
            this.backGroundPanelTrend1.Name = "backGroundPanelTrend1";
            this.backGroundPanelTrend1.Size = new System.Drawing.Size(1024, 768);
            this.backGroundPanelTrend1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.Location = new System.Drawing.Point(47, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(924, 5);
            this.label7.TabIndex = 117;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(58, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(551, 19);
            this.label3.TabIndex = 105;
            this.label3.Text = "3. 补卡只能预存50或100元，如需预存更多请在办理完就诊卡后，持卡再次充值。\r\n";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(58, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(656, 19);
            this.label12.TabIndex = 104;
            this.label12.Text = "2. 对就诊卡进行补卡操作后，原就诊卡不可再使用。原就诊卡中的预交金等信息会自动转入新卡。\r\n";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientName.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientName.ForeColor = System.Drawing.Color.White;
            this.lblPatientName.Location = new System.Drawing.Point(58, 173);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(251, 19);
            this.lblPatientName.TabIndex = 92;
            this.lblPatientName.Text = "1. 请选择一条卡信息进行补卡操作。";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(34, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 26);
            this.label2.TabIndex = 81;
            this.label2.Text = "自助补卡-请选择需要补办的就诊卡";
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblOK.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.lblOK.Location = new System.Drawing.Point(45, 658);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(133, 44);
            this.lblOK.TabIndex = 76;
            this.lblOK.Text = "确认补卡";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // gdcMain
            // 
            this.gdcMain.Location = new System.Drawing.Point(39, 259);
            this.gdcMain.LookAndFeel.SkinName = "Blue";
            this.gdcMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gdcMain.MainView = this.gdvMain;
            this.gdcMain.Name = "gdcMain";
            this.gdcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gdcMain.Size = new System.Drawing.Size(942, 383);
            this.gdcMain.TabIndex = 67;
            this.gdcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gdvMain});
            // 
            // gdvMain
            // 
            this.gdvMain.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.gdvMain.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray;
            this.gdvMain.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gdvMain.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gdvMain.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gdvMain.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.gdvMain.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(218)))), ((int)(((byte)(228)))));
            this.gdvMain.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.gdvMain.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue;
            this.gdvMain.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gdvMain.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gdvMain.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gdvMain.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.gdvMain.Appearance.Empty.Options.UseBackColor = true;
            this.gdvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite;
            this.gdvMain.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.gdvMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.EvenRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.gdvMain.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.gdvMain.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gdvMain.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gdvMain.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gdvMain.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.gdvMain.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.gdvMain.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gdvMain.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gdvMain.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(58)))), ((int)(((byte)(81)))));
            this.gdvMain.Appearance.FixedLine.Options.UseBackColor = true;
            this.gdvMain.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(128)))), ((int)(((byte)(151)))));
            this.gdvMain.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(178)))), ((int)(((byte)(201)))));
            this.gdvMain.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gdvMain.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gdvMain.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gdvMain.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.GroupButton.Options.UseBackColor = true;
            this.gdvMain.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gdvMain.Appearance.GroupButton.Options.UseForeColor = true;
            this.gdvMain.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(195)))), ((int)(((byte)(210)))));
            this.gdvMain.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(195)))), ((int)(((byte)(210)))));
            this.gdvMain.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gdvMain.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gdvMain.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gdvMain.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.gdvMain.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gdvMain.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gdvMain.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gdvMain.Appearance.GroupPanel.Options.UseFont = true;
            this.gdvMain.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gdvMain.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(128)))), ((int)(((byte)(151)))));
            this.gdvMain.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver;
            this.gdvMain.Appearance.GroupRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.GroupRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gdvMain.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gdvMain.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gdvMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.gdvMain.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gdvMain.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray;
            this.gdvMain.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.HorzLine.Options.UseBackColor = true;
            this.gdvMain.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(220)))), ((int)(((byte)(227)))));
            this.gdvMain.Appearance.OddRow.BackColor2 = System.Drawing.Color.White;
            this.gdvMain.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.gdvMain.Appearance.OddRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.OddRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.gdvMain.Appearance.Preview.BackColor2 = System.Drawing.Color.White;
            this.gdvMain.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(128)))), ((int)(((byte)(151)))));
            this.gdvMain.Appearance.Preview.Options.UseBackColor = true;
            this.gdvMain.Appearance.Preview.Options.UseForeColor = true;
            this.gdvMain.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gdvMain.Appearance.Row.Options.UseBackColor = true;
            this.gdvMain.Appearance.Row.Options.UseForeColor = true;
            this.gdvMain.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(205)))), ((int)(((byte)(220)))));
            this.gdvMain.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gdvMain.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(138)))), ((int)(((byte)(161)))));
            this.gdvMain.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gdvMain.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gdvMain.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gdvMain.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(200)))));
            this.gdvMain.Appearance.VertLine.Options.UseBackColor = true;
            this.gdvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn8,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn5});
            this.gdvMain.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gdvMain.GridControl = this.gdcMain;
            this.gdvMain.Name = "gdvMain";
            this.gdvMain.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gdvMain.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gdvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gdvMain.OptionsView.EnableAppearanceOddRow = true;
            this.gdvMain.OptionsView.ShowGroupPanel = false;
            this.gdvMain.RowHeight = 35;
            this.gdvMain.ViewCaptionHeight = 0;
            this.gdvMain.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gdvMain_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "选择";
            this.gridColumn1.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn1.FieldName = "PITCHON1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 50;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.Click += new System.EventHandler(this.repositoryItemCheckEdit1_Click);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "卡号";
            this.gridColumn8.FieldName = "CARDID";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.OptionsColumn.AllowMove = false;
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "诊疗号";
            this.gridColumn2.FieldName = "DIAGNOSEID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSize = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 132;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "持卡人姓名";
            this.gridColumn3.FieldName = "PATIENTNAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 116;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "发卡日期";
            this.gridColumn4.FieldName = "PROVIDECARDDATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.OptionsColumn.AllowSize = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 217;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "类型";
            this.gridColumn6.DisplayFormat.FormatString = "MM-dd";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn6.FieldName = "SENDCARDTYPE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn6.OptionsFilter.AllowFilter = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 99;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "卡状态";
            this.gridColumn5.FieldName = "CARDSTATE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 107;
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
            this.btnExit.TabIndex = 66;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = 25;
            this.ucTime1.Size = new System.Drawing.Size(180, 30);
            this.ucTime1.TabIndex = 1;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 0;
            // 
            // FrmReIssueCardInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.backGroundPanelTrend1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReIssueCardInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自助缴费";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReIssueCardInfo_FormClosing);
            this.Load += new System.EventHandler(this.FrmReIssueCardInfo_Load);
            this.backGroundPanelTrend1.ResumeLayout(false);
            this.backGroundPanelTrend1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BackGroundPanelTrend backGroundPanelTrend1;
        private Inc.UcTime ucTime1;
        private Inc.UCHead ucHead1;
        private System.Windows.Forms.Label btnExit;
        private DevExpress.XtraGrid.GridControl gdcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gdvMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Label lblOK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
    }
}