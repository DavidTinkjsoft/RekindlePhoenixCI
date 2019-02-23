﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BaseGround;
using Common;
using BaseGround.Report;
using DevExpress.XtraGrid.Views.Grid;
using BusinessObjects.Enums;
using BusinessObjects;
using DevExpress.XtraEditors.Repository;
using DataObjects.Dao.Together.SpecificDao;
using BaseGround.Shared;
using DevExpress.XtraEditors.Controls;

/// <summary>
/// Lukas, 2019/1/18
/// </summary>
namespace PhoenixCI.FormUI.Prefix2 {
    /// <summary>
    /// 20220 部位限制取數標準- 指數類
    /// </summary>
    public partial class W20220 : FormParent {

        private ReportHelper _ReportHelper;
        private D20220 dao20220;
        private RepositoryItemLookUpEdit _RepLookUpEdit_Type;
        private RepositoryItemLookUpEdit _RepLookUpEdit_SubType;

        public W20220(string programID, string programName) : base(programID, programName) {
            InitializeComponent();
            this.Text = _ProgramID + "─" + _ProgramName;

            GridHelper.SetCommonGrid(gvMain);
            PrintableComponent = gcMain;
        }

        protected override ResultStatus Open() {
            base.Open();

            #region 處理下拉選單
            _RepLookUpEdit_Type = new RepositoryItemLookUpEdit();
            DataTable dtProd_Type = new DataTable();
            dtProd_Type.Columns.Add("ID");
            dtProd_Type.Columns.Add("DESC");
            DataRow ptRow1 = dtProd_Type.NewRow();
            ptRow1["ID"] = "F";
            ptRow1["DESC"] = "期貨";
            dtProd_Type.Rows.Add(ptRow1);
            DataRow ptRow2 = dtProd_Type.NewRow();
            ptRow2["ID"] = "O";
            ptRow2["DESC"] = "選擇權";
            dtProd_Type.Rows.Add(ptRow2);
            Extension.SetColumnLookUp(_RepLookUpEdit_Type, dtProd_Type, "ID", "DESC", TextEditStyles.DisableTextEditor, "");
            gcMain.RepositoryItems.Add(_RepLookUpEdit_Type);
            PLT1_PROD_TYPE.ColumnEdit = _RepLookUpEdit_Type;

            _RepLookUpEdit_SubType = new RepositoryItemLookUpEdit();
            DataTable dtProd_SubType = new DataTable();
            dtProd_SubType.Columns.Add("ID");
            dtProd_SubType.Columns.Add("DESC");
            DataRow pstRow1 = dtProd_SubType.NewRow();
            pstRow1["ID"] = "C";
            pstRow1["DESC"] = "黃金類";
            dtProd_SubType.Rows.Add(pstRow1);
            DataRow pstRow2 = dtProd_SubType.NewRow();
            pstRow2["ID"] = "E";
            pstRow2["DESC"] = "匯率類";
            dtProd_SubType.Rows.Add(pstRow2);
            DataRow pstRow3 = dtProd_SubType.NewRow();
            pstRow3["ID"] = "I";
            pstRow3["DESC"] = "指數類";
            dtProd_SubType.Rows.Add(pstRow3);
            Extension.SetColumnLookUp(_RepLookUpEdit_SubType, dtProd_SubType, "ID", "DESC", TextEditStyles.DisableTextEditor, "");
            gcMain.RepositoryItems.Add(_RepLookUpEdit_SubType);
            PLT1_PROD_SUBTYPE.ColumnEdit = _RepLookUpEdit_SubType;
            #endregion

            return ResultStatus.Success;
        }

        protected override ResultStatus AfterOpen() {
            base.AfterOpen();

            //直接讀取資料
            Retrieve();

            //沒有新增資料時,則自動新增內容
            if (gvMain.RowCount == 0) {
                InsertRow();
            }

            return ResultStatus.Success;
        }

        protected override ResultStatus ActivatedForm() {
            base.ActivatedForm();

            _ToolBtnInsert.Enabled = true;//當按下此按鈕時,Grid新增一筆空的(還未存檔都是暫時的)
            _ToolBtnSave.Enabled = true;//儲存(把新增/刪除/修改)多筆的結果一次更新到資料庫
            _ToolBtnDel.Enabled = true;//先選定刪除grid上面的其中一筆,然後按下此刪除按鈕(還未存檔都是暫時的)

            _ToolBtnRetrieve.Enabled = true;//畫面查詢條件選定之後,按下此按鈕,讀取資料 to Grid
            _ToolBtnRun.Enabled = false;//執行,跑job專用按鈕

            _ToolBtnImport.Enabled = false;//匯入
            _ToolBtnExport.Enabled = false;//匯出,格式可以為 pdf/xls/txt/csv, 看功能
            _ToolBtnPrintAll.Enabled = true;//列印

            return ResultStatus.Success;
        }

        protected override ResultStatus Retrieve() {

            dao20220 = new D20220();
            DataTable returnTable = dao20220.ListAll();

            if (returnTable.Rows.Count == 0) {
                MessageBox.Show("無任何資料", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ResultStatus.Fail;
            }

            returnTable.Columns.Add("Is_NewRow", typeof(string));
            gcMain.DataSource = returnTable;
            gcMain.Focus();

            return ResultStatus.Success;
        }

        protected override ResultStatus CheckShield() {
            base.CheckShield(gcMain);
            if (!IsDataModify(gcMain)) { return ResultStatus.Fail; }
            //if (cbxUserId.SelectedItem == null) {
            //    MessageDisplay.Warning("使用者代號不可為空白!");
            //    return ResultStatus.Fail;
            //}

            return ResultStatus.Success;
        }

        protected override ResultStatus Save(PokeBall pokeBall) {
            base.Save(gcMain);

            DataTable dt = (DataTable)gcMain.DataSource;
            DataTable dtChange = dt.GetChanges();

            if (dtChange == null) {
                MessageBox.Show("沒有變更資料,不需要存檔!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ResultStatus.Fail;
            }
            if (dtChange.Rows.Count == 0) {
                MessageBox.Show("沒有變更資料,不需要存檔!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ResultStatus.Fail;
            }
            //更新主要Table
            else {
                ResultStatus status = base.Save_Override(dt, "PLT1");
                if (status == ResultStatus.Fail) {
                    return ResultStatus.Fail;
                }
            }
            return ResultStatus.Success;
        }


        protected override ResultStatus Print(ReportHelper reportHelper) {
            _ReportHelper = reportHelper;
            CommonReportPortraitA4 report = new CommonReportPortraitA4();
            report.printableComponentContainerMain.PrintableComponent = gcMain;
            _ReportHelper.Create(report);

            base.Print(_ReportHelper);
            return ResultStatus.Success;
        }

        protected override ResultStatus InsertRow() {
            base.InsertRow(gvMain);
            gvMain.Focus();
            gvMain.FocusedColumn = gvMain.Columns[0];

            return ResultStatus.Success;
        }

        protected override ResultStatus DeleteRow() {
            base.DeleteRow(gvMain);

            return ResultStatus.Success;
        }

        #region GridControl事件

        /// <summary>
        /// Insert New Row(給Is_NewRow欄位賦值供辨認用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_InitNewRow(object sender, InitNewRowEventArgs e) {
            GridView gv = sender as GridView;
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["Is_NewRow"], 1);
        }

        /// <summary>
        /// 決定哪些欄位無法編輯的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_ShowingEditor(object sender, CancelEventArgs e) {
            GridView gv = sender as GridView;
            string Is_NewRow = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["Is_NewRow"]) == null ? "0" :
                 gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns["Is_NewRow"]).ToString();

            if (gv.IsNewItemRow(gv.FocusedRowHandle) || Is_NewRow == "1") {
                e.Cancel = false;
                gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["Is_NewRow"], 1);
            }
            //編輯狀態時,設定可以編輯的欄位( e.Cancel = false 等於可以編輯)
            else if (gv.FocusedColumn.Name == "PLT1_PROD_TYPE" || gv.FocusedColumn.Name == "PLT1_PROD_SUBTYPE") {
                e.Cancel = true;
            }
            else {
                e.Cancel = false;
            }

        }

        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e) {
            //要用RowHandle不要用FocusedRowHandle
            GridView gv = sender as GridView;
            string Is_NewRow = gv.GetRowCellValue(e.RowHandle, gv.Columns["Is_NewRow"]) == null ? "0" :
                               gv.GetRowCellValue(e.RowHandle, gv.Columns["Is_NewRow"]).ToString();

            //描述每個欄位,在is_newRow時候要顯示的顏色
            //當該欄位不可編輯時,設定為灰色 Color.FromArgb(192,192,192)
            //當該欄位不可編輯時,AllowFocus為false(PB的wf_set_order方法)
            switch (e.Column.FieldName) {
                case ("PLT1_PROD_TYPE"):
                case ("PLT1_PROD_SUBTYPE"):
                    e.Column.OptionsColumn.AllowFocus = Is_NewRow == "1" ? true : false;
                    e.Appearance.BackColor = Is_NewRow == "1" ? Color.White : Color.FromArgb(192, 192, 192);
                    break;
                default:
                    e.Appearance.BackColor = Color.White;
                    break;
            }//switch (e.Column.FieldName) {

        }

        #endregion
    }
}