using AutoServiceManage.Inc;
namespace AutoServiceManage.AutoPrint
{
    partial class FrmPrintListReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintListReport));
            this.panel1 = new AutoServiceManage.Inc.BackGroundPanelTrend(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOK = new System.Windows.Forms.Label();
            this.gdcMain = new DevExpress.XtraGrid.GridControl();
            this.gdvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.lblInHosID = new System.Windows.Forms.Label();
            this.lblIDCard = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ucHead1 = new AutoServiceManage.Inc.UCHead();
            this.label1 = new System.Windows.Forms.Label();
            this.ucTime1 = new AutoServiceManage.Inc.UcTime();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::AutoServiceManage.Properties.Resources.自助预约_背景;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblOK);
            this.panel1.Controls.Add(this.gdcMain);
            this.panel1.Controls.Add(this.lblInHosID);
            this.panel1.Controls.Add(this.lblIDCard);
            this.panel1.Controls.Add(this.lblAge);
            this.panel1.Controls.Add(this.lblSex);
            this.panel1.Controls.Add(this.lblPatient);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.ucHead1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ucTime1);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 768);
            this.panel1.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(366, 658);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(361, 54);
            this.label4.TabIndex = 126;
            this.label4.Text = "检验报告单只能打印一次。";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(258, 673);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 125;
            this.label3.Text = "温馨提示：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOK
            // 
            this.lblOK.BackColor = System.Drawing.Color.Transparent;
            this.lblOK.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(89)))), ((int)(((byte)(0)))));
            this.lblOK.Image = global::AutoServiceManage.Properties.Resources.关闭按钮背景;
            this.lblOK.Location = new System.Drawing.Point(41, 658);
            this.lblOK.Name = "lblOK";
            this.lblOK.Size = new System.Drawing.Size(128, 44);
            this.lblOK.TabIndex = 124;
            this.lblOK.Text = "确认打印";
            this.lblOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOK.Click += new System.EventHandler(this.lblOK_Click);
            // 
            // gdcMain
            // 
            this.gdcMain.Location = new System.Drawing.Point(42, 276);
            this.gdcMain.LookAndFeel.SkinName = "Blue";
            this.gdcMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gdcMain.MainView = this.gdvMain;
            this.gdcMain.Name = "gdcMain";
            this.gdcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gdcMain.Size = new System.Drawing.Size(927, 374);
            this.gdcMain.TabIndex = 85;
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
            this.gridColumn8,
            this.gridColumn2,
            this.gridColumn3});
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
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "报告单号";
            this.gridColumn8.FieldName = "CHECK_NO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.OptionsColumn.AllowMove = false;
            this.gridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 230;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "组合项目名称";
            this.gridColumn2.FieldName = "ItemName";
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
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 432;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "时间";
            this.gridColumn3.FieldName = "LastOpTime";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 244;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // lblInHosID
            // 
            this.lblInHosID.BackColor = System.Drawing.Color.Transparent;
            this.lblInHosID.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInHosID.ForeColor = System.Drawing.Color.White;
            this.lblInHosID.Location = new System.Drawing.Point(669, 221);
            this.lblInHosID.Name = "lblInHosID";
            this.lblInHosID.Size = new System.Drawing.Size(214, 29);
            this.lblInHosID.TabIndex = 123;
            this.lblInHosID.Text = "住院号：0000001170";
            this.lblInHosID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIDCard
            // 
            this.lblIDCard.BackColor = System.Drawing.Color.Transparent;
            this.lblIDCard.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIDCard.ForeColor = System.Drawing.Color.White;
            this.lblIDCard.Location = new System.Drawing.Point(317, 221);
            this.lblIDCard.Name = "lblIDCard";
            this.lblIDCard.Size = new System.Drawing.Size(329, 29);
            this.lblIDCard.TabIndex = 122;
            this.lblIDCard.Text = "身份证号：110235198901012352";
            this.lblIDCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAge
            // 
            this.lblAge.BackColor = System.Drawing.Color.Transparent;
            this.lblAge.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.ForeColor = System.Drawing.Color.White;
            this.lblAge.Location = new System.Drawing.Point(250, 221);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(61, 29);
            this.lblAge.TabIndex = 121;
            this.lblAge.Text = "24岁";
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSex
            // 
            this.lblSex.BackColor = System.Drawing.Color.Transparent;
            this.lblSex.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.ForeColor = System.Drawing.Color.White;
            this.lblSex.Location = new System.Drawing.Point(204, 221);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(40, 29);
            this.lblSex.TabIndex = 120;
            this.lblSex.Text = "男";
            this.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPatient
            // 
            this.lblPatient.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatient.ForeColor = System.Drawing.Color.White;
            this.lblPatient.Location = new System.Drawing.Point(108, 221);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(112, 29);
            this.lblPatient.TabIndex = 119;
            this.lblPatient.Text = "西安天网";
            this.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(65, 219);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 29);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 118;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(59, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 26);
            this.label2.TabIndex = 117;
            this.label2.Text = "账户信息：";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.Location = new System.Drawing.Point(40, 259);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(924, 14);
            this.label7.TabIndex = 116;
            // 
            // ucHead1
            // 
            this.ucHead1.BackColor = System.Drawing.Color.Transparent;
            this.ucHead1.Location = new System.Drawing.Point(10, 30);
            this.ucHead1.Name = "ucHead1";
            this.ucHead1.Size = new System.Drawing.Size(1014, 93);
            this.ucHead1.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(37, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 26);
            this.label1.TabIndex = 41;
            this.label1.Text = "检验报告单打印";
            // 
            // ucTime1
            // 
            this.ucTime1.AutoClose = false;
            this.ucTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucTime1.Location = new System.Drawing.Point(10, 720);
            this.ucTime1.Name = "ucTime1";
            this.ucTime1.Sec = -13;
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
            // FrmPrintListReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPrintListReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检验报告单打印";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrintListReport_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrintListReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label btnReturn;
        private System.Windows.Forms.Label btnExit;
        private UcTime ucTime1;
        private System.Windows.Forms.Label label1;
        private UCHead ucHead1;
        private BackGroundPanelTrend panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblIDCard;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblInHosID;
        private DevExpress.XtraGrid.GridControl gdcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gdvMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.Label lblOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}