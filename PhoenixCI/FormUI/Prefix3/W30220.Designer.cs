﻿namespace PhoenixCI.FormUI.Prefix3 {
    partial class W30220 {
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
            this.txtStkoutDate = new PhoenixCI.Widget.TextDateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDate = new PhoenixCI.Widget.TextDateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEMonth = new PhoenixCI.Widget.TextDateEdit();
            this.txtSMonth = new PhoenixCI.Widget.TextDateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.grpxDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStkoutDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSMonth.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panParent
            // 
            this.panParent.Controls.Add(this.lblProcessing);
            this.panParent.Controls.Add(this.grpxDescription);
            this.panParent.Size = new System.Drawing.Size(936, 612);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(936, 30);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.ForeColor = System.Drawing.Color.Blue;
            this.lblProcessing.Location = new System.Drawing.Point(53, 324);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(85, 20);
            this.lblProcessing.TabIndex = 22;
            this.lblProcessing.Text = "開始轉檔...";
            this.lblProcessing.Visible = false;
            // 
            // grpxDescription
            // 
            this.grpxDescription.AutoSize = true;
            this.grpxDescription.Controls.Add(this.txtStkoutDate);
            this.grpxDescription.Controls.Add(this.label3);
            this.grpxDescription.Controls.Add(this.txtDate);
            this.grpxDescription.Controls.Add(this.label2);
            this.grpxDescription.Controls.Add(this.txtEMonth);
            this.grpxDescription.Controls.Add(this.txtSMonth);
            this.grpxDescription.Controls.Add(this.label1);
            this.grpxDescription.Controls.Add(this.lblDate);
            this.grpxDescription.Location = new System.Drawing.Point(57, 49);
            this.grpxDescription.Name = "grpxDescription";
            this.grpxDescription.Size = new System.Drawing.Size(404, 272);
            this.grpxDescription.TabIndex = 21;
            this.grpxDescription.TabStop = false;
            this.grpxDescription.Text = "請輸入交易日期";
            // 
            // txtStkoutDate
            // 
            this.txtStkoutDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtStkoutDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
            this.txtStkoutDate.EditValue = "2018/12";
            this.txtStkoutDate.EnterMoveNextControl = true;
            this.txtStkoutDate.Location = new System.Drawing.Point(164, 162);
            this.txtStkoutDate.MenuManager = this.ribbonControl;
            this.txtStkoutDate.Name = "txtStkoutDate";
            this.txtStkoutDate.Properties.Appearance.Options.UseTextOptions = true;
            this.txtStkoutDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtStkoutDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.txtStkoutDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtStkoutDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtStkoutDate.Size = new System.Drawing.Size(105, 26);
            this.txtStkoutDate.TabIndex = 4;
            this.txtStkoutDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "流通在外股票：";
            // 
            // txtDate
            // 
            this.txtDate.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtDate.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
            this.txtDate.EditValue = "2018/12";
            this.txtDate.EnterMoveNextControl = true;
            this.txtDate.Location = new System.Drawing.Point(132, 57);
            this.txtDate.MenuManager = this.ribbonControl;
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDate.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.txtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDate.Size = new System.Drawing.Size(105, 26);
            this.txtDate.TabIndex = 1;
            this.txtDate.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "計算日期：";
            // 
            // txtEMonth
            // 
            this.txtEMonth.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtEMonth.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
            this.txtEMonth.EditValue = "2018/12";
            this.txtEMonth.EnterMoveNextControl = true;
            this.txtEMonth.Location = new System.Drawing.Point(283, 112);
            this.txtEMonth.MenuManager = this.ribbonControl;
            this.txtEMonth.Name = "txtEMonth";
            this.txtEMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtEMonth.Properties.Mask.EditMask = "yyyy/MM";
            this.txtEMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtEMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtEMonth.Size = new System.Drawing.Size(82, 26);
            this.txtEMonth.TabIndex = 3;
            this.txtEMonth.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // txtSMonth
            // 
            this.txtSMonth.DateTimeValue = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
            this.txtSMonth.DateType = PhoenixCI.Widget.TextDateEdit.DateTypeItem.Month;
            this.txtSMonth.EditValue = "2018/12";
            this.txtSMonth.EnterMoveNextControl = true;
            this.txtSMonth.Location = new System.Drawing.Point(164, 112);
            this.txtSMonth.MenuManager = this.ribbonControl;
            this.txtSMonth.Name = "txtSMonth";
            this.txtSMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSMonth.Properties.Mask.EditMask = "yyyy/MM";
            this.txtSMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtSMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSMonth.Size = new System.Drawing.Size(82, 26);
            this.txtSMonth.TabIndex = 2;
            this.txtSMonth.TextMaskFormat = PhoenixCI.Widget.TextDateEdit.TextMaskFormatItem.IncludePrompt;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "～";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(37, 115);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(121, 20);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "總交易量月份：";
            // 
            // W30220
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 642);
            this.Name = "W30220";
            this.Text = "W30220";
            this.panParent.ResumeLayout(false);
            this.panParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.grpxDescription.ResumeLayout(false);
            this.grpxDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtStkoutDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSMonth.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.GroupBox grpxDescription;
        private Widget.TextDateEdit txtStkoutDate;
        private System.Windows.Forms.Label label3;
        private Widget.TextDateEdit txtDate;
        private System.Windows.Forms.Label label2;
        private Widget.TextDateEdit txtEMonth;
        private Widget.TextDateEdit txtSMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDate;
    }
}