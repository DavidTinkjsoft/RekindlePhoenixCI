﻿namespace PhoenixCI.FormUI.Prefix9 {
   partial class W95140 {
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
         this.lblProcessing = new System.Windows.Forms.Label();
         this.grpxDescription = new System.Windows.Forms.GroupBox();
         this.txtStartMth = new PhoenixCI.Widget.TextDateEdit();
         this.label2 = new System.Windows.Forms.Label();
         this.txtEndMth = new PhoenixCI.Widget.TextDateEdit();
         this.label3 = new System.Windows.Forms.Label();
         this.lblCheck = new System.Windows.Forms.Label();
         this.txtStartDate = new PhoenixCI.Widget.TextDateEdit();
         this.label1 = new System.Windows.Forms.Label();
         this.txtEndDate = new PhoenixCI.Widget.TextDateEdit();
         this.lblDate = new System.Windows.Forms.Label();
         this.chkMon = new DevExpress.XtraEditors.CheckEdit();
         this.chkFcm = new DevExpress.XtraEditors.CheckEdit();
         this.chkAcc = new DevExpress.XtraEditors.CheckEdit();
         this.panParent.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
         this.grpxDescription.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtStartMth.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtEndMth.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkMon.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkFcm.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkAcc.Properties)).BeginInit();
         this.SuspendLayout();
         // 
         // panParent
         // 
         this.panParent.Controls.Add(this.lblProcessing);
         this.panParent.Controls.Add(this.grpxDescription);
         this.panParent.Size = new System.Drawing.Size(865, 600);
         // 
         // ribbonControl
         // 
         this.ribbonControl.ExpandCollapseItem.Id = 0;
         this.ribbonControl.Size = new System.Drawing.Size(865, 32);
         this.ribbonControl.Toolbar.ShowCustomizeItem = false;
         // 
         // lblProcessing
         // 
         this.lblProcessing.AutoSize = true;
         this.lblProcessing.ForeColor = System.Drawing.Color.Blue;
         this.lblProcessing.Location = new System.Drawing.Point(15, 215);
         this.lblProcessing.Name = "lblProcessing";
         this.lblProcessing.Size = new System.Drawing.Size(85, 20);
         this.lblProcessing.TabIndex = 13;
         this.lblProcessing.Text = "開始轉檔...";
         this.lblProcessing.Visible = false;
         // 
         // grpxDescription
         // 
         this.grpxDescription.AutoSize = true;
         this.grpxDescription.Controls.Add(this.chkAcc);
         this.grpxDescription.Controls.Add(this.chkFcm);
         this.grpxDescription.Controls.Add(this.chkMon);
         this.grpxDescription.Controls.Add(this.txtStartMth);
         this.grpxDescription.Controls.Add(this.label2);
         this.grpxDescription.Controls.Add(this.txtEndMth);
         this.grpxDescription.Controls.Add(this.label3);
         this.grpxDescription.Controls.Add(this.lblCheck);
         this.grpxDescription.Controls.Add(this.txtStartDate);
         this.grpxDescription.Controls.Add(this.label1);
         this.grpxDescription.Controls.Add(this.txtEndDate);
         this.grpxDescription.Controls.Add(this.lblDate);
         this.grpxDescription.Location = new System.Drawing.Point(12, 15);
         this.grpxDescription.Name = "grpxDescription";
         this.grpxDescription.Size = new System.Drawing.Size(723, 197);
         this.grpxDescription.TabIndex = 12;
         this.grpxDescription.TabStop = false;
         this.grpxDescription.Text = "請輸入交易日期";
         // 
         // txtStartMth
         // 
         this.txtStartMth.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
         this.txtStartMth.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
         this.txtStartMth.EditValue = "2018/12";
         this.txtStartMth.EnterMoveNextControl = true;
         this.txtStartMth.Location = new System.Drawing.Point(79, 53);
         this.txtStartMth.MenuManager = this.ribbonControl;
         this.txtStartMth.Name = "txtStartMth";
         this.txtStartMth.Properties.Appearance.Options.UseTextOptions = true;
         this.txtStartMth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
         this.txtStartMth.Properties.Mask.EditMask = "yyyy/MM";
         this.txtStartMth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
         this.txtStartMth.Properties.Mask.UseMaskAsDisplayFormat = true;
         this.txtStartMth.Size = new System.Drawing.Size(118, 26);
         this.txtStartMth.TabIndex = 0;
         this.txtStartMth.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.BackColor = System.Drawing.Color.Transparent;
         this.label2.Location = new System.Drawing.Point(203, 56);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(25, 20);
         this.label2.TabIndex = 20;
         this.label2.Text = "～";
         // 
         // txtEndMth
         // 
         this.txtEndMth.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
         this.txtEndMth.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
         this.txtEndMth.EditValue = "2018/12";
         this.txtEndMth.EnterMoveNextControl = true;
         this.txtEndMth.Location = new System.Drawing.Point(234, 53);
         this.txtEndMth.MenuManager = this.ribbonControl;
         this.txtEndMth.Name = "txtEndMth";
         this.txtEndMth.Properties.Appearance.Options.UseTextOptions = true;
         this.txtEndMth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
         this.txtEndMth.Properties.Mask.EditMask = "yyyy/MM";
         this.txtEndMth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
         this.txtEndMth.Properties.Mask.UseMaskAsDisplayFormat = true;
         this.txtEndMth.Size = new System.Drawing.Size(118, 26);
         this.txtEndMth.TabIndex = 1;
         this.txtEndMth.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.BackColor = System.Drawing.Color.Transparent;
         this.label3.Location = new System.Drawing.Point(16, 56);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(57, 20);
         this.label3.TabIndex = 19;
         this.label3.Text = "月份：";
         // 
         // lblCheck
         // 
         this.lblCheck.AutoSize = true;
         this.lblCheck.BackColor = System.Drawing.Color.Transparent;
         this.lblCheck.Location = new System.Drawing.Point(379, 25);
         this.lblCheck.Name = "lblCheck";
         this.lblCheck.Size = new System.Drawing.Size(89, 20);
         this.lblCheck.TabIndex = 16;
         this.lblCheck.Text = "報表種類：";
         // 
         // txtStartDate
         // 
         this.txtStartDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
         this.txtStartDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
         this.txtStartDate.EditValue = "2018/12";
         this.txtStartDate.EnterMoveNextControl = true;
         this.txtStartDate.Location = new System.Drawing.Point(79, 98);
         this.txtStartDate.MenuManager = this.ribbonControl;
         this.txtStartDate.Name = "txtStartDate";
         this.txtStartDate.Properties.Appearance.Options.UseTextOptions = true;
         this.txtStartDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
         this.txtStartDate.Properties.Mask.EditMask = "yyyy/MM/dd";
         this.txtStartDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
         this.txtStartDate.Properties.Mask.UseMaskAsDisplayFormat = true;
         this.txtStartDate.Size = new System.Drawing.Size(118, 26);
         this.txtStartDate.TabIndex = 2;
         this.txtStartDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.BackColor = System.Drawing.Color.Transparent;
         this.label1.Location = new System.Drawing.Point(203, 101);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(25, 20);
         this.label1.TabIndex = 13;
         this.label1.Text = "～";
         // 
         // txtEndDate
         // 
         this.txtEndDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
         this.txtEndDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
         this.txtEndDate.EditValue = "2018/12";
         this.txtEndDate.EnterMoveNextControl = true;
         this.txtEndDate.Location = new System.Drawing.Point(234, 98);
         this.txtEndDate.MenuManager = this.ribbonControl;
         this.txtEndDate.Name = "txtEndDate";
         this.txtEndDate.Properties.Appearance.Options.UseTextOptions = true;
         this.txtEndDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
         this.txtEndDate.Properties.Mask.EditMask = "yyyy/MM/dd";
         this.txtEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
         this.txtEndDate.Properties.Mask.UseMaskAsDisplayFormat = true;
         this.txtEndDate.Size = new System.Drawing.Size(118, 26);
         this.txtEndDate.TabIndex = 3;
         this.txtEndDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
         // 
         // lblDate
         // 
         this.lblDate.AutoSize = true;
         this.lblDate.BackColor = System.Drawing.Color.Transparent;
         this.lblDate.Location = new System.Drawing.Point(16, 101);
         this.lblDate.Name = "lblDate";
         this.lblDate.Size = new System.Drawing.Size(57, 20);
         this.lblDate.TabIndex = 12;
         this.lblDate.Text = "日期：";
         // 
         // chkMon
         // 
         this.chkMon.Location = new System.Drawing.Point(383, 54);
         this.chkMon.MenuManager = this.ribbonControl;
         this.chkMon.Name = "chkMon";
         this.chkMon.Properties.Caption = "月計總表";
         this.chkMon.Size = new System.Drawing.Size(250, 24);
         this.chkMon.TabIndex = 21;
         // 
         // chkFcm
         // 
         this.chkFcm.Location = new System.Drawing.Point(383, 100);
         this.chkFcm.MenuManager = this.ribbonControl;
         this.chkFcm.Name = "chkFcm";
         this.chkFcm.Properties.Caption = "日計明細表_by期貨商";
         this.chkFcm.Size = new System.Drawing.Size(250, 24);
         this.chkFcm.TabIndex = 22;
         // 
         // chkAcc
         // 
         this.chkAcc.Location = new System.Drawing.Point(383, 145);
         this.chkAcc.MenuManager = this.ribbonControl;
         this.chkAcc.Name = "chkAcc";
         this.chkAcc.Properties.Caption = "日計明細表_by交易人帳號";
         this.chkAcc.Size = new System.Drawing.Size(250, 24);
         this.chkAcc.TabIndex = 23;
         // 
         // W95140
         // 
         this.Appearance.Options.UseFont = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(865, 632);
         this.Name = "W95140";
         this.Text = "W95140";
         this.panParent.ResumeLayout(false);
         this.panParent.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
         this.grpxDescription.ResumeLayout(false);
         this.grpxDescription.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.txtStartMth.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtEndMth.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkMon.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkFcm.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkAcc.Properties)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label lblProcessing;
      private System.Windows.Forms.GroupBox grpxDescription;
      private Widget.TextDateEdit txtStartDate;
      private System.Windows.Forms.Label label1;
      private Widget.TextDateEdit txtEndDate;
      private System.Windows.Forms.Label lblDate;
      private System.Windows.Forms.Label lblCheck;
      private Widget.TextDateEdit txtStartMth;
      private System.Windows.Forms.Label label2;
      private Widget.TextDateEdit txtEndMth;
      private System.Windows.Forms.Label label3;
      private DevExpress.XtraEditors.CheckEdit chkMon;
      private DevExpress.XtraEditors.CheckEdit chkAcc;
      private DevExpress.XtraEditors.CheckEdit chkFcm;
   }
}