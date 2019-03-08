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
using BusinessObjects.Enums;
using BusinessObjects;
using BaseGround.Report;
using DataObjects.Dao.Together.SpecificDao;
using DevExpress.Spreadsheet;
using BaseGround.Shared;
/// <summary>
/// Lukas, 2018/12/27
/// </summary>
namespace PhoenixCI.FormUI.Prefix5 {
    /// <summary>
    /// 55060 CME美國道瓊及標普500期貨授權費表
    /// 有寫到的功能：Export
    /// </summary>
    public partial class W55060 : FormParent {

        private D55060 dao55060;

        public W55060(string programID, string programName) : base(programID, programName) {
            InitializeComponent();
            dao55060 = new D55060();
            this.Text = _ProgramID + "─" + _ProgramName;
            txtFromMonth.DateTimeValue = GlobalInfo.OCF_DATE;
            txtToMonth.DateTimeValue = GlobalInfo.OCF_DATE;
        }


        public override ResultStatus BeforeOpen() {
            base.BeforeOpen();

            return ResultStatus.Success;
        }

        protected override ResultStatus Open() {
            base.Open();

            return ResultStatus.Success;
        }

        protected override ResultStatus AfterOpen() {
            base.AfterOpen();

            return ResultStatus.Success;
        }

        protected override ResultStatus ActivatedForm() {
            base.ActivatedForm();

            _ToolBtnExport.Enabled = true;

            return ResultStatus.Success;
        }

        protected override ResultStatus Retrieve() {
            base.Retrieve();

            return ResultStatus.Success;
        }

        protected override ResultStatus CheckShield() {
            base.CheckShield();

            return ResultStatus.Success;
        }

        protected override ResultStatus Save(PokeBall pokeBall) {
            base.Save(pokeBall);

            return ResultStatus.Success;
        }

        protected override ResultStatus Run(PokeBall args) {
            base.Run(args);

            return ResultStatus.Success;
        }

        protected override ResultStatus Import() {
            base.Import();

            return ResultStatus.Success;
        }

        protected override ResultStatus Export() {
            if (txtFromMonth.Text.SubStr(0, 4) != txtToMonth.Text.SubStr(0, 4)) {
                MessageBox.Show("不可跨年度查詢!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return ResultStatus.Fail;
            }
            base.Export();
            lblProcessing.Visible = true;
            string excelDestinationPath = CopyExcelTemplateFile(_ProgramID, FileType.XLS);
            string excelDestinationPath_Detail = CopyExcelTemplateFile(_ProgramID + "MM", FileType.XLS);
            ManipulateExcel(excelDestinationPath);
            ManipulateExcel_Detail(excelDestinationPath_Detail);
            /**********************
            轉檔後資訊
            **********************/
            string lsYM;
            lsYM = txtToMonth.Text.Replace("/", "");
            DataTable dt_55060_after_export = dao55060.d_55060_after_export(lsYM);
            if (dt_55060_after_export.Rows[0]["ld_disc_qnty"].AsString() == "0") {
                MessageBox.Show(lsYM + "「結算手續費」的可折抵口數皆為０，"+ Environment.NewLine +"請確認結算手續費作業是否已完成！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ResultStatus.Success;
            }
            lblProcessing.Visible = false;
            return ResultStatus.Success;
        }

        private void ManipulateExcel(string excelDestinationPath) {

           
            try {
                #region wf_55060_1
                string rptName, rptId;
                int i;
                /*************************************
                ls_rpt_name = 報表名稱
                ls_rpt_id = 報表代號
                *************************************/
                rptName = "交易量(單邊)";
                rptId = "55060_1";

                /******************
                讀取資料
                ******************/
                string asSymd = txtFromMonth.Text.Replace("/", "")+"01";
                string asEymd = txtToMonth.Text.Replace("/", "")+"31";
                DataTable dt55060_1 = dao55060.d_55060_1(asSymd, asEymd);
                if (dt55060_1.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                /******************
                切換Sheet
                ******************/
                Workbook workbook = new Workbook();
                workbook.LoadDocument(excelDestinationPath);
                Worksheet worksheet = workbook.Worksheets[1];

                int rowNum = 4;
                for (i = 0; i < dt55060_1.Rows.Count; i++) {
                    DataRow dr55060_1 = dt55060_1.Rows[i];

                    rowNum = rowNum + 1;
                    worksheet.Cells[rowNum, 0].Value = dr55060_1["data_date"].AsString();
                    worksheet.Cells[rowNum, 1].Value = dr55060_1["udf_qnty"].AsDecimal();
                    worksheet.Cells[rowNum, 2].Value = dr55060_1["spf_qnty"].AsDecimal();
                }

                #endregion

                #region wf_55060_2
                //string ls_rpt_name, ls_rpt_id;
                //int i;
                /*************************************
                ls_rpt_name = 報表名稱
                ls_rpt_id = 報表代號
                *************************************/
                rptName = "到期結算OI";
                rptId = "55060_2";

                /******************
                讀取資料
                ******************/
                //計算月底日期
                DateTime date;
                date = Convert.ToDateTime(txtToMonth.Text + "/01").AddDays(31);
                date = date.AddDays(date.Day * -1);
                string asSdate = Convert.ToDateTime(txtFromMonth.Text + "/01").ToString("yyyy/M/d tt hh:mm:ss");
                string asEdate = date.ToString("yyyy/M/d tt hh:mm:ss");

                DataTable dt55060_2 = dao55060.d_55060_2(asSdate, asEdate);
                if (dt55060_2.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                /******************
                切換Sheet
                ******************/
                Worksheet worksheet2 = workbook.Worksheets[2];

                //填資料
                rowNum = 4;
                for (i = 0; i < dt55060_2.Rows.Count; i++) {
                    DataRow dr55060_2 = dt55060_2.Rows[i];

                    rowNum = rowNum + 1;
                    worksheet2.Cells[rowNum, 0].Value = dr55060_2["data_date"].AsString();
                    worksheet2.Cells[rowNum, 1].Value = dr55060_2["spf_oi"].AsDecimal();
                    worksheet2.Cells[rowNum, 2].Value = dr55060_2["udf_oi"].AsDecimal();
                }

                #endregion

                #region wf_55060_3
                string kindId;
                int j, addCol, num;
                /*************************************
                ls_rpt_name = 報表名稱
                ls_rpt_id = 報表代號
                *************************************/
                rptName = "造市折減";
                rptId = "55060_3";

                /******************
                讀取資料
                ******************/
                string asSym = txtFromMonth.Text.Replace("/", "");
                string asEym = txtToMonth.Text.Replace("/", "");
                DataTable dt55060_3 = dao55060.d_55060_3(asSym, asEym);
                if (dt55060_3.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                /******************
                切換Sheet
                ******************/
                Worksheet worksheet3 = workbook.Worksheets[3];

                //填資料
                kindId = "";
                num = 0;
                addCol = 0;
                for (i = 0; i < dt55060_3.Rows.Count; i++) {
                    DataRow dr55060_3 = dt55060_3.Rows[i];
                    if (kindId != dr55060_3["kind_id"].AsString().Trim()) {
                        rowNum = 6;
                        kindId = dr55060_3["kind_id"].AsString().Trim();
                        if (kindId == "UDF") {
                            addCol = 8;
                        }
                        else {
                            addCol = 0;
                        }
                        num = 0;
                    }

                    rowNum = rowNum + 1;
                    num = num + 1;
                    worksheet3.Cells[rowNum, 0 + addCol].Value = dr55060_3["data_ym"].AsString();
                    worksheet3.Cells[rowNum, 1 + addCol].Value = num;
                    worksheet3.Cells[rowNum, 2 + addCol].Value = kindId;
                    worksheet3.Cells[rowNum, 3 + addCol].Value = dr55060_3["trd_ar_amt"].AsDecimal();
                    worksheet3.Cells[rowNum, 4 + addCol].Value = dr55060_3["trd_rec_amt"].AsDecimal();
                    worksheet3.Cells[rowNum, 5 + addCol].Value = dr55060_3["cm_ar_amt"].AsDecimal();
                    worksheet3.Cells[rowNum, 6 + addCol].Value = dr55060_3["cm_rec_amt"].AsDecimal();
                }

                #endregion

                //存檔
                workbook.SaveDocument(excelDestinationPath);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManipulateExcel_Detail(string excelDestinationPath) {

            
            try {
                #region wf_55060_3_trd
                string rptName, rptId, kindId;
                int i, j, addCol;
                //long i;
                /*************************************
                ls_rpt_name = 報表名稱
                ls_rpt_id = 報表代號
                *************************************/
                rptName = "造市折減";
                rptId = "55060_3MM";

                /******************
                讀取資料
                ******************/
                string asSym = txtFromMonth.Text.Replace("/", "");
                string asEym = txtToMonth.Text.Replace("/", "");
                DataTable dt55060_3_trd = dao55060.d_55060_3_trd(asSym, asEym);
                if (dt55060_3_trd.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                //切換Sheet
                Workbook workbook = new Workbook();
                workbook.LoadDocument(excelDestinationPath);
                //造市折減(交易經手費)
                Worksheet worksheet = workbook.Worksheets[1];

                //填資料
                kindId = "";
                int rowNum = 0;
                addCol = 0;
                for (i = 0; i < dt55060_3_trd.Rows.Count; i++) {
                    DataRow dr55060_3_trd = dt55060_3_trd.Rows[i];
                    if (kindId != dr55060_3_trd["feetrd_kind_id"].AsString()) {
                        rowNum = 6;
                        kindId = dr55060_3_trd["feetrd_kind_id"].AsString();
                        if (dr55060_3_trd["feetrd_kind_id"].AsString() == "UDF") {
                            addCol = 14;
                        }
                        else {
                            addCol = 0;
                        }
                    }

                    rowNum = rowNum + 1;
                    worksheet.Cells[rowNum, 0 + addCol].Value = dr55060_3_trd["feetrd_ym"].AsString();
                    worksheet.Cells[rowNum, 1 + addCol].Value = dr55060_3_trd["feetrd_fcm_no"].AsString();
                    worksheet.Cells[rowNum, 2 + addCol].Value = dr55060_3_trd["feetrd_kind_id"].AsString();
                    worksheet.Cells[rowNum, 3 + addCol].Value = dr55060_3_trd["feetrd_disc_qnty"].AsDecimal();
                    worksheet.Cells[rowNum, 4 + addCol].Value = dr55060_3_trd["disc_rate"].AsInt();
                    worksheet.Cells[rowNum, 5 + addCol].Value = dr55060_3_trd["feetrd_ar"].AsDecimal();
                    worksheet.Cells[rowNum, 6 + addCol].Value = dr55060_3_trd["disc_amt"].AsDecimal();
                    worksheet.Cells[rowNum, 7 + addCol].Value = dr55060_3_trd["feetrd_rec_amt"].AsDecimal();
                    worksheet.Cells[rowNum, 8 + addCol].Value = dr55060_3_trd["feetrd_m_qnty"].AsDecimal();
                    worksheet.Cells[rowNum, 9 + addCol].Value = dr55060_3_trd["feetrd_fcm_kind"].AsString();
                    worksheet.Cells[rowNum, 10 + addCol].Value = dr55060_3_trd["feetrd_param_key"].AsString();
                    worksheet.Cells[rowNum, 11 + addCol].Value = dr55060_3_trd["feetrd_acc_no"].AsString();
                    worksheet.Cells[rowNum, 12 + addCol].Value = dr55060_3_trd["feetrd_session"].AsString();
                    //PB的寫法，但打內沒辦法自動分辨型別
                    //for (j = 0; j < 13; j++) {
                    //    worksheet.Cells[ii_ole_row, j + li_add_col].Value = dr55060_3_trd[j].ToString();
                    //}
                }

                #endregion

                #region wf_55060_3_cm
                //讀取資料
                DataTable dt55060_3_cm = dao55060.d_55060_3_cm(asSym, asEym);
                if (dt55060_3_cm.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                //切換Sheet
                //造市折減(結算手續費)
                Worksheet worksheet2 = workbook.Worksheets[2];

                //填資料
                kindId = "";
                for (i = 0; i < dt55060_3_cm.Rows.Count; i++) {
                    DataRow dr55060_3_cm = dt55060_3_cm.Rows[i];
                    if (kindId != dr55060_3_cm["feetdcc_kind_id"].AsString()) {
                        rowNum = 6;
                        kindId = dr55060_3_cm["feetdcc_kind_id"].AsString();
                        if (dr55060_3_cm["feetdcc_kind_id"].AsString() == "UDF") {
                            addCol = 11;
                        }
                        else {
                            addCol = 0;
                        }
                    }

                    rowNum = rowNum + 1;

                    worksheet2.Cells[rowNum, 0 + addCol].Value = dr55060_3_cm["feetdcc_ym"].AsString();
                    worksheet2.Cells[rowNum, 1 + addCol].Value = dr55060_3_cm["feetdcc_fcm_no"].AsString();
                    worksheet2.Cells[rowNum, 2 + addCol].Value = dr55060_3_cm["feetdcc_kind_id"].AsString();
                    worksheet2.Cells[rowNum, 3 + addCol].Value = dr55060_3_cm["feetdcc_disc_qnty"].AsDecimal();
                    worksheet2.Cells[rowNum, 4 + addCol].Value = dr55060_3_cm["disc_rate"].AsDecimal();
                    worksheet2.Cells[rowNum, 5 + addCol].Value = dr55060_3_cm["feetdcc_org_ar"].AsDecimal();
                    worksheet2.Cells[rowNum, 6 + addCol].Value = dr55060_3_cm["feetdcc_disc_amt"].AsDecimal();
                    worksheet2.Cells[rowNum, 7 + addCol].Value = dr55060_3_cm["rec_amt"].AsDecimal();
                    worksheet2.Cells[rowNum, 8 + addCol].Value = dr55060_3_cm["feetdcc_acc_no"].AsString();
                    worksheet2.Cells[rowNum, 9 + addCol].Value = dr55060_3_cm["feetdcc_session"].AsString();
                    //for (j = 0; j < 10; j++) {
                    //    worksheet2.Cells[ii_ole_row, j + li_add_col].Value = dr55060_3_cm[j].ToString();
                    //}
                }

                #endregion

                #region wf_55060_3_all
                //讀取資料
                DataTable dt55060_3_all = dao55060.d_55060_3_all(asSym, asEym);
                if (dt55060_3_all.Rows.Count == 0) {
                    MessageDisplay.Info(string.Format("{0},{1},無任何資料!", txtFromMonth.Text + "-" + txtToMonth.Text, rptName));
                }

                //切換Sheet
                //造市折減(交易+結算)
                Worksheet worksheet3 = workbook.Worksheets[0];

                //填資料
                kindId = "";
                for (i = 0; i < dt55060_3_all.Rows.Count; i++) {
                    DataRow dr55060_3_all = dt55060_3_all.Rows[i];
                    if (kindId != dr55060_3_all["feetrd_kind_id"].AsString()) {
                        rowNum = 6;
                        kindId = dr55060_3_all["feetrd_kind_id"].AsString();
                        if (dr55060_3_all["feetrd_kind_id"].AsString() == "UDF") {
                            addCol = 14;
                        }
                        else {
                            addCol = 0;
                        }
                    }

                    rowNum = rowNum + 1;
                    worksheet3.Cells[rowNum, 0 + addCol].Value = dr55060_3_all["feetrd_feetrd_ym"].AsString();
                    worksheet3.Cells[rowNum, 1 + addCol].Value = dr55060_3_all["feetrd_feetrd_fcm_no"].AsString();
                    worksheet3.Cells[rowNum, 2 + addCol].Value = dr55060_3_all["feetrd_kind_id"].AsString();
                    worksheet3.Cells[rowNum, 3 + addCol].Value = dr55060_3_all["feetrd_feetrd_disc_qnty"].AsDecimal();
                    worksheet3.Cells[rowNum, 4 + addCol].Value = dr55060_3_all["disc_rate"].AsId();
                    worksheet3.Cells[rowNum, 5 + addCol].Value = dr55060_3_all["ar"].AsDecimal();
                    worksheet3.Cells[rowNum, 6 + addCol].Value = dr55060_3_all["disc_amt"].AsDecimal();
                    worksheet3.Cells[rowNum, 7 + addCol].Value = dr55060_3_all["rec_amt"].AsDecimal();
                    worksheet3.Cells[rowNum, 8 + addCol].Value = dr55060_3_all["feetrd_feetrd_m_qnty"].AsDecimal();
                    worksheet3.Cells[rowNum, 9 + addCol].Value = dr55060_3_all["feetrd_feetrd_fcm_kind"].AsString();
                    worksheet3.Cells[rowNum, 10 + addCol].Value = dr55060_3_all["feetrd_feetrd_param_key"].AsString();
                    worksheet3.Cells[rowNum, 11 + addCol].Value = dr55060_3_all["feetrd_feetrd_acc_no"].AsString();
                    worksheet3.Cells[rowNum, 12 + addCol].Value = dr55060_3_all["feetrd_feetrd_session"].AsString();
                    //for (j = 0; j < 13; j++) {
                    //    worksheet3.Cells[ii_ole_row, j + li_add_col].Value = dr55060_3_all[j].ToString();
                    //}
                }

                #endregion

                //存檔
                workbook.SaveDocument(excelDestinationPath);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        protected override ResultStatus Print(ReportHelper reportHelper) {
            base.Print(reportHelper);

            return ResultStatus.Success;
        }

        protected override ResultStatus InsertRow() {
            base.InsertRow();

            return ResultStatus.Success;
        }

        protected override ResultStatus DeleteRow() {
            base.DeleteRow();

            return ResultStatus.Success;
        }

        protected override ResultStatus BeforeClose() {
            return base.BeforeClose();
        }
    }
}