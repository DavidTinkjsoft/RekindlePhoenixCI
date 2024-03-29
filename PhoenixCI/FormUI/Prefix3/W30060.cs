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
using BusinessObjects.Enums;
using BaseGround.Shared;
using DevExpress.Spreadsheet;
using DataObjects.Dao.Together.SpecificDao;
using Common;

/// <summary>
/// Lukas, 2019/3/6
/// </summary>
namespace PhoenixCI.FormUI.Prefix3 {

    /// <summary>
    /// 30060 各商品每日成交紀錄
    /// </summary>
    public partial class W30060 : FormParent {

        private D30060 dao30060;

        public W30060(string programID, string programName) : base(programID, programName) {
            InitializeComponent();
            this.Text = _ProgramID + "─" + _ProgramName;
        }

        protected override ResultStatus Open() {
            base.Open();
            txtEDate.EditValue = PbFunc.f_ocf_date(0);
            txtSDate.EditValue = txtEDate.Text.SubStr(0, 8) + "01";
            txtSDate.Focus();
            return ResultStatus.Success;
        }

        protected override ResultStatus ActivatedForm() {
            base.ActivatedForm();

            _ToolBtnInsert.Enabled = false;//當按下此按鈕時,Grid新增一筆空的(還未存檔都是暫時的)
            _ToolBtnSave.Enabled = false;//儲存(把新增/刪除/修改)多筆的結果一次更新到資料庫
            _ToolBtnDel.Enabled = false;//先選定刪除grid上面的其中一筆,然後按下此刪除按鈕(還未存檔都是暫時的)

            _ToolBtnRetrieve.Enabled = false;//畫面查詢條件選定之後,按下此按鈕,讀取資料 to Grid
            _ToolBtnRun.Enabled = false;//執行,跑job專用按鈕

            _ToolBtnImport.Enabled = false;//匯入
            _ToolBtnExport.Enabled = true;//匯出,格式可以為 pdf/xls/txt/csv, 看功能
            _ToolBtnPrintAll.Enabled = false;//列印

            return ResultStatus.Success;
        }

        protected override ResultStatus Export() {

            dao30060 = new D30060();
            string rptId = "30060", file;
            string symd = txtSDate.Text.Replace("/", "");
            string eymd = txtEDate.Text.Replace("/", "");

            // 1. 複製檔案
            file = PbFunc.wf_copy_file(rptId, rptId);
            if (file == "") {
                return ResultStatus.Fail;
            }

            // 2. 開啟檔案
            Workbook workbook = new Workbook();
            workbook.LoadDocument(file);

            // 3. 匯出資料
            int rowNum = 0;

            #region wf_30060
            string rptName, ymd;
            int colNum;

            rptName = "各商品每日成交紀錄";

            // 切換Sheet
            Worksheet ws30060 = workbook.Worksheets[0];

            /******************
            讀取資料 (每日)
            ******************/
            DataTable dt30060 = dao30060.d_30060(symd, eymd);
            if (dt30060.Rows.Count == 0) {
                lblProcessing.Text = PbFunc.f_ocf_date(1).SubStr(0, 6) + "," + rptId + '－' + rptName + ",無任何資料!";
            }
            ymd = "";
            foreach (DataRow dr in dt30060.Rows) {
                if (ymd != dr["AI2_YMD"].AsString()) {
                    ymd = dr["AI2_YMD"].AsString();
                    rowNum = rowNum + 1;
                    ws30060.Cells[rowNum, 0].Value = ymd.SubStr(0, 4) + "/" + ymd.SubStr(4, 2) + "/" + ymd.SubStr(6, 2);
                }
                // 交易量
                colNum = dr["M_COL_SEQ"].AsInt() -1;
                ws30060.Cells[rowNum, colNum].Value = dr["AI2_M_QNTY"].AsDecimal();
                // OI
                colNum = dr["OI_COL_SEQ"].AsInt() - 1;
                ws30060.Cells[rowNum, colNum].Value = dr["AI2_OI"].AsDecimal();
            }
            #endregion

            // 4. 存檔
            ws30060.ScrollToRow(0);
            workbook.SaveDocument(file);
            return ResultStatus.Success;
        }
    }
}