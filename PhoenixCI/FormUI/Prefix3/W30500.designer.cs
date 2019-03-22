﻿namespace PhoenixCI.FormUI.Prefix3
{
    partial class W30500
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtEDate = new PhoenixCI.Widget.TextDateEdit();
            this.txtSDate = new PhoenixCI.Widget.TextDateEdit();
            this.ExportShow = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.PROD_ID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.APDK_NAME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.WEIGHT_DIFF = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.SIMPLE_DIFF = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.MAX_DIFF = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand10 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.MAX_DIFF_TIME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.MIN_DIFF = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand12 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.MIN_DIFF_TIME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.NO_TWO_SIDE_TIME = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panParent
            // 
            this.panParent.Size = new System.Drawing.Size(813, 694);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(813, 30);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labTime);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.lblDate);
            this.panelControl1.Controls.Add(this.txtEDate);
            this.panelControl1.Controls.Add(this.txtSDate);
            this.panelControl1.Controls.Add(this.ExportShow);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(813, 143);
            this.panelControl1.TabIndex = 0;
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(486, 120);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(54, 20);
            this.labTime.TabIndex = 19;
            this.labTime.Text = "label2";
            this.labTime.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "~";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(9, 39);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(89, 20);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "統計期間：";
            // 
            // txtEDate
            // 
            this.txtEDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtEDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Date;
            this.txtEDate.EditValue = "2018/12";
            this.txtEDate.EnterMoveNextControl = true;
            this.txtEDate.Location = new System.Drawing.Point(281, 36);
            this.txtEDate.MenuManager = this.ribbonControl;
            this.txtEDate.Name = "txtEDate";
            this.txtEDate.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.txtEDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtEDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtEDate.Size = new System.Drawing.Size(144, 26);
            this.txtEDate.TabIndex = 17;
            this.txtEDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // txtSDate
            // 
            this.txtSDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtSDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Date;
            this.txtSDate.EditValue = "2018/12";
            this.txtSDate.EnterMoveNextControl = true;
            this.txtSDate.Location = new System.Drawing.Point(104, 36);
            this.txtSDate.MenuManager = this.ribbonControl;
            this.txtSDate.Name = "txtSDate";
            this.txtSDate.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.txtSDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtSDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSDate.Size = new System.Drawing.Size(144, 26);
            this.txtSDate.TabIndex = 16;
            this.txtSDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // ExportShow
            // 
            this.ExportShow.AutoSize = true;
            this.ExportShow.Location = new System.Drawing.Point(15, 120);
            this.ExportShow.Name = "ExportShow";
            this.ExportShow.Size = new System.Drawing.Size(54, 20);
            this.ExportShow.TabIndex = 12;
            this.ExportShow.Text = "label1";
            // 
            // panelControl2
            // 
            this.panelControl2.AllowTouchScroll = true;
            this.panelControl2.Controls.Add(this.gcMain);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 173);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(813, 551);
            this.panelControl2.TabIndex = 1;
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 2);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(809, 547);
            this.gcMain.TabIndex = 10;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5,
            this.gridBand6,
            this.gridBand7});
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.PROD_ID,
            this.APDK_NAME,
            this.WEIGHT_DIFF,
            this.SIMPLE_DIFF,
            this.MAX_DIFF,
            this.MAX_DIFF_TIME,
            this.MIN_DIFF,
            this.MIN_DIFF_TIME,
            this.NO_TWO_SIDE_TIME});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsCustomization.AllowSort = false;
            this.gvMain.OptionsPrint.PrintHeader = false;
            this.gvMain.OptionsView.ShowColumnHeaders = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "股票期貨代號";
            this.gridBand1.Columns.Add(this.PROD_ID);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 125;
            // 
            // PROD_ID
            // 
            this.PROD_ID.FieldName = "PROD_ID";
            this.PROD_ID.MinWidth = 30;
            this.PROD_ID.Name = "PROD_ID";
            this.PROD_ID.OptionsColumn.AllowEdit = false;
            this.PROD_ID.OptionsColumn.ShowCaption = false;
            this.PROD_ID.Visible = true;
            this.PROD_ID.Width = 125;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "股票期貨契約名稱";
            this.gridBand2.Columns.Add(this.APDK_NAME);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 108;
            // 
            // APDK_NAME
            // 
            this.APDK_NAME.FieldName = "APDK_NAME";
            this.APDK_NAME.MinWidth = 30;
            this.APDK_NAME.Name = "APDK_NAME";
            this.APDK_NAME.OptionsColumn.AllowEdit = false;
            this.APDK_NAME.OptionsColumn.ShowCaption = false;
            this.APDK_NAME.Visible = true;
            this.APDK_NAME.Width = 108;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "加權平均委託買賣價差(單位:點)";
            this.gridBand3.Columns.Add(this.WEIGHT_DIFF);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 113;
            // 
            // WEIGHT_DIFF
            // 
            this.WEIGHT_DIFF.FieldName = "WEIGHT_DIFF";
            this.WEIGHT_DIFF.MinWidth = 30;
            this.WEIGHT_DIFF.Name = "WEIGHT_DIFF";
            this.WEIGHT_DIFF.OptionsColumn.AllowEdit = false;
            this.WEIGHT_DIFF.OptionsColumn.ShowCaption = false;
            this.WEIGHT_DIFF.Visible = true;
            this.WEIGHT_DIFF.Width = 113;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "簡單平均委託買賣價差(單位:點)";
            this.gridBand4.Columns.Add(this.SIMPLE_DIFF);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 3;
            this.gridBand4.Width = 106;
            // 
            // SIMPLE_DIFF
            // 
            this.SIMPLE_DIFF.FieldName = "SIMPLE_DIFF";
            this.SIMPLE_DIFF.MinWidth = 30;
            this.SIMPLE_DIFF.Name = "SIMPLE_DIFF";
            this.SIMPLE_DIFF.OptionsColumn.AllowEdit = false;
            this.SIMPLE_DIFF.OptionsColumn.ShowCaption = false;
            this.SIMPLE_DIFF.Visible = true;
            this.SIMPLE_DIFF.Width = 106;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = "最大委託買賣價差";
            this.gridBand5.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand9,
            this.gridBand10});
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 4;
            this.gridBand5.Width = 192;
            // 
            // gridBand9
            // 
            this.gridBand9.Caption = "價差(單位:點)";
            this.gridBand9.Columns.Add(this.MAX_DIFF);
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.VisibleIndex = 0;
            this.gridBand9.Width = 116;
            // 
            // MAX_DIFF
            // 
            this.MAX_DIFF.Caption = "價差(單位：點)";
            this.MAX_DIFF.FieldName = "MAX_DIFF";
            this.MAX_DIFF.MinWidth = 30;
            this.MAX_DIFF.Name = "MAX_DIFF";
            this.MAX_DIFF.OptionsColumn.AllowEdit = false;
            this.MAX_DIFF.Visible = true;
            this.MAX_DIFF.Width = 116;
            // 
            // gridBand10
            // 
            this.gridBand10.Caption = "時間(秒)";
            this.gridBand10.Columns.Add(this.MAX_DIFF_TIME);
            this.gridBand10.Name = "gridBand10";
            this.gridBand10.VisibleIndex = 1;
            this.gridBand10.Width = 76;
            // 
            // MAX_DIFF_TIME
            // 
            this.MAX_DIFF_TIME.Caption = "時間(秒)";
            this.MAX_DIFF_TIME.FieldName = "MAX_DIFF_TIME";
            this.MAX_DIFF_TIME.MinWidth = 30;
            this.MAX_DIFF_TIME.Name = "MAX_DIFF_TIME";
            this.MAX_DIFF_TIME.OptionsColumn.AllowEdit = false;
            this.MAX_DIFF_TIME.Visible = true;
            this.MAX_DIFF_TIME.Width = 76;
            // 
            // gridBand6
            // 
            this.gridBand6.Caption = "最小委託買賣價差";
            this.gridBand6.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand11,
            this.gridBand12});
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.VisibleIndex = 5;
            this.gridBand6.Width = 192;
            // 
            // gridBand11
            // 
            this.gridBand11.Caption = "價差(單位:點)";
            this.gridBand11.Columns.Add(this.MIN_DIFF);
            this.gridBand11.Name = "gridBand11";
            this.gridBand11.VisibleIndex = 0;
            this.gridBand11.Width = 116;
            // 
            // MIN_DIFF
            // 
            this.MIN_DIFF.Caption = "價差(單位：點)";
            this.MIN_DIFF.FieldName = "MIN_DIFF";
            this.MIN_DIFF.MinWidth = 30;
            this.MIN_DIFF.Name = "MIN_DIFF";
            this.MIN_DIFF.OptionsColumn.AllowEdit = false;
            this.MIN_DIFF.Visible = true;
            this.MIN_DIFF.Width = 116;
            // 
            // gridBand12
            // 
            this.gridBand12.Caption = "時間(秒)";
            this.gridBand12.Columns.Add(this.MIN_DIFF_TIME);
            this.gridBand12.Name = "gridBand12";
            this.gridBand12.VisibleIndex = 1;
            this.gridBand12.Width = 76;
            // 
            // MIN_DIFF_TIME
            // 
            this.MIN_DIFF_TIME.Caption = "時間(秒)";
            this.MIN_DIFF_TIME.FieldName = "MIN_DIFF_TIME";
            this.MIN_DIFF_TIME.MinWidth = 30;
            this.MIN_DIFF_TIME.Name = "MIN_DIFF_TIME";
            this.MIN_DIFF_TIME.OptionsColumn.AllowEdit = false;
            this.MIN_DIFF_TIME.Visible = true;
            this.MIN_DIFF_TIME.Width = 76;
            // 
            // gridBand7
            // 
            this.gridBand7.Caption = "無雙邊買賣委託價格之累計時間(單位:秒) 註";
            this.gridBand7.Columns.Add(this.NO_TWO_SIDE_TIME);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.VisibleIndex = 6;
            this.gridBand7.Width = 168;
            // 
            // NO_TWO_SIDE_TIME
            // 
            this.NO_TWO_SIDE_TIME.FieldName = "NO_TWO_SIDE_TIME";
            this.NO_TWO_SIDE_TIME.MinWidth = 30;
            this.NO_TWO_SIDE_TIME.Name = "NO_TWO_SIDE_TIME";
            this.NO_TWO_SIDE_TIME.OptionsColumn.AllowEdit = false;
            this.NO_TWO_SIDE_TIME.OptionsColumn.ShowCaption = false;
            this.NO_TWO_SIDE_TIME.Visible = true;
            this.NO_TWO_SIDE_TIME.Width = 168;
            // 
            // W30500
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 724);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "W30500";
            this.Text = "W30500";
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.panParent, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcMain;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label ExportShow;
        private Widget.TextDateEdit txtEDate;
        private Widget.TextDateEdit txtSDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn PROD_ID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn APDK_NAME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn WEIGHT_DIFF;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn SIMPLE_DIFF;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn MAX_DIFF;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn MAX_DIFF_TIME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn MIN_DIFF;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn MIN_DIFF_TIME;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn NO_TWO_SIDE_TIME;
        private System.Windows.Forms.Label labTime;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand10;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand11;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
    }
}