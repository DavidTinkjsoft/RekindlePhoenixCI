﻿namespace PhoenixCI.FormUI.Prefix5 {
    partial class W50120 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtMonth = new PhoenixCI.Widget.TextDateEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MPDF_YM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPDF_FCM_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPDF_ACC_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPDF_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPDF_KIND_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MPDF_EFF_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Is_NewRow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panParent
            // 
            this.panParent.Controls.Add(this.gcMain);
            this.panParent.Location = new System.Drawing.Point(0, 100);
            this.panParent.Size = new System.Drawing.Size(1055, 644);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(1055, 30);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.SkyBlue;
            this.panelControl1.Appearance.BackColor2 = System.Drawing.Color.SkyBlue;
            this.panelControl1.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Appearance.Options.UseBorderColor = true;
            this.panelControl1.Controls.Add(this.lblDate);
            this.panelControl1.Controls.Add(this.txtMonth);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1055, 70);
            this.panelControl1.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Location = new System.Drawing.Point(19, 24);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(57, 20);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "年月：";
            // 
            // txtMonth
            // 
            this.txtMonth.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtMonth.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
            this.txtMonth.EditValue = "2018/12";
            this.txtMonth.EnterMoveNextControl = true;
            this.txtMonth.Location = new System.Drawing.Point(82, 21);
            this.txtMonth.MenuManager = this.ribbonControl;
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMonth.Properties.Mask.EditMask = "yyyy/MM";
            this.txtMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonth.Size = new System.Drawing.Size(100, 26);
            this.txtMonth.TabIndex = 6;
            this.txtMonth.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 100);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1055, 644);
            this.panelControl2.TabIndex = 1;
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(12, 12);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.MenuManager = this.ribbonControl;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(1031, 620);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MPDF_YM,
            this.MPDF_FCM_NO,
            this.MPDF_ACC_NO,
            this.MPDF_STATUS,
            this.MPDF_KIND_ID,
            this.MPDF_EFF_DATE,
            this.Is_NewRow});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMain_RowCellStyle);
            this.gvMain.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvMain_ShowingEditor);
            this.gvMain.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvMain_InitNewRow);
            this.gvMain.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvMain_FocusedColumnChanged);
            // 
            // MPDF_YM
            // 
            this.MPDF_YM.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.MPDF_YM.AppearanceCell.Options.UseBackColor = true;
            this.MPDF_YM.AppearanceHeader.BackColor = System.Drawing.Color.Yellow;
            this.MPDF_YM.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_YM.Caption = "年月";
            this.MPDF_YM.FieldName = "MPDF_YM";
            this.MPDF_YM.Name = "MPDF_YM";
            this.MPDF_YM.Visible = true;
            this.MPDF_YM.VisibleIndex = 0;
            this.MPDF_YM.Width = 100;
            // 
            // MPDF_FCM_NO
            // 
            this.MPDF_FCM_NO.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.MPDF_FCM_NO.AppearanceCell.Options.UseBackColor = true;
            this.MPDF_FCM_NO.AppearanceHeader.BackColor = System.Drawing.Color.Yellow;
            this.MPDF_FCM_NO.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_FCM_NO.Caption = "造市者代號";
            this.MPDF_FCM_NO.FieldName = "MPDF_FCM_NO";
            this.MPDF_FCM_NO.Name = "MPDF_FCM_NO";
            this.MPDF_FCM_NO.Visible = true;
            this.MPDF_FCM_NO.VisibleIndex = 1;
            this.MPDF_FCM_NO.Width = 240;
            // 
            // MPDF_ACC_NO
            // 
            this.MPDF_ACC_NO.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.MPDF_ACC_NO.AppearanceCell.Options.UseBackColor = true;
            this.MPDF_ACC_NO.AppearanceHeader.BackColor = System.Drawing.Color.Yellow;
            this.MPDF_ACC_NO.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_ACC_NO.Caption = "帳號";
            this.MPDF_ACC_NO.FieldName = "MPDF_ACC_NO";
            this.MPDF_ACC_NO.Name = "MPDF_ACC_NO";
            this.MPDF_ACC_NO.Visible = true;
            this.MPDF_ACC_NO.VisibleIndex = 2;
            this.MPDF_ACC_NO.Width = 120;
            // 
            // MPDF_STATUS
            // 
            this.MPDF_STATUS.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.MPDF_STATUS.AppearanceCell.Options.UseBackColor = true;
            this.MPDF_STATUS.AppearanceHeader.BackColor = System.Drawing.Color.Yellow;
            this.MPDF_STATUS.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_STATUS.Caption = "狀態";
            this.MPDF_STATUS.FieldName = "MPDF_STATUS";
            this.MPDF_STATUS.Name = "MPDF_STATUS";
            this.MPDF_STATUS.Visible = true;
            this.MPDF_STATUS.VisibleIndex = 3;
            this.MPDF_STATUS.Width = 160;
            // 
            // MPDF_KIND_ID
            // 
            this.MPDF_KIND_ID.AppearanceCell.BackColor = System.Drawing.Color.Silver;
            this.MPDF_KIND_ID.AppearanceCell.Options.UseBackColor = true;
            this.MPDF_KIND_ID.AppearanceHeader.BackColor = System.Drawing.Color.Yellow;
            this.MPDF_KIND_ID.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_KIND_ID.Caption = "契約";
            this.MPDF_KIND_ID.FieldName = "MPDF_KIND_ID";
            this.MPDF_KIND_ID.Name = "MPDF_KIND_ID";
            this.MPDF_KIND_ID.Visible = true;
            this.MPDF_KIND_ID.VisibleIndex = 4;
            this.MPDF_KIND_ID.Width = 200;
            // 
            // MPDF_EFF_DATE
            // 
            this.MPDF_EFF_DATE.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MPDF_EFF_DATE.AppearanceHeader.Options.UseBackColor = true;
            this.MPDF_EFF_DATE.Caption = "生效日期";
            this.MPDF_EFF_DATE.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.MPDF_EFF_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.MPDF_EFF_DATE.FieldName = "MPDF_EFF_DATE";
            this.MPDF_EFF_DATE.Name = "MPDF_EFF_DATE";
            this.MPDF_EFF_DATE.Visible = true;
            this.MPDF_EFF_DATE.VisibleIndex = 5;
            this.MPDF_EFF_DATE.Width = 170;
            // 
            // Is_NewRow
            // 
            this.Is_NewRow.Caption = "Is_NewRow";
            this.Is_NewRow.FieldName = "Is_NewRow";
            this.Is_NewRow.Name = "Is_NewRow";
            // 
            // W50120
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 744);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "W50120";
            this.Text = "W50120";
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panParent, 0);
            this.panParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private Widget.TextDateEdit txtMonth;
        private System.Windows.Forms.Label lblDate;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_YM;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_FCM_NO;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_ACC_NO;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_STATUS;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_KIND_ID;
        private DevExpress.XtraGrid.Columns.GridColumn MPDF_EFF_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn Is_NewRow;
    }
}