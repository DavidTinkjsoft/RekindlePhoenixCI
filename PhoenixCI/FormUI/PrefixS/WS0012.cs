﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseGround;
using BusinessObjects.Enums;
using Common;
using DataObjects.Dao.Together.SpecificDao;
using DevExpress.XtraEditors;
using BusinessObjects;
using static DataObjects.Dao.DataGate;
using BaseGround.Report;

namespace PhoenixCI.FormUI.PrefixS
{
    public partial class WS0012 : FormParent
    {
        private ReportHelper _ReportHelper;
        private DS0012 daoS0012;

        public WS0012(string programID, string programName) : base(programID, programName) {
            InitializeComponent();
            GridHelper.SetCommonGrid(gvMain);

            this.Text = _ProgramID + "─" + _ProgramName;
            _IsPreventFlowPrint = true;
            daoS0012 = new DS0012();
            txtCountDate.DateTimeValue = DateTime.Now;
            txtDate1.DateTimeValue = DateTime.Now;
            txtDate2.DateTimeValue = DateTime.Now;
            txtDate3.DateTimeValue = DateTime.Now;

            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;
            radioGroup3.SelectedIndex = 0;
            adjustmentRadioGroup.SelectedIndex = 0;
        }

        protected override ResultStatus Retrieve() {
            base.Retrieve(gcMain);
            DateTime searchDate1 = txtCountDate.DateTimeValue;
            DateTime searchDate2 = txtCountDate.DateTimeValue;
            DateTime searchDate3 = txtCountDate.DateTimeValue;

            #region Get User Select
            for (int i = 1; i <= 3; i++) {
                string radioSelect = "";
                RadioGroup radios = new RadioGroup();

                switch (i) {
                    case 1: {
                            radioSelect = radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value.AsString().Substring(1);
                            searchDate1 = GetDateByUserSelect(radioSelect);
                            break;
                        }
                    case 2: {
                            radioSelect = radioGroup2.Properties.Items[radioGroup2.SelectedIndex].Value.AsString().Substring(1);
                            searchDate2 = GetDateByUserSelect(radioSelect);
                            break;
                        }
                    case 3: {
                            radioSelect = radioGroup3.Properties.Items[radioGroup3.SelectedIndex].Value.AsString().Substring(1);
                            searchDate3 = GetDateByUserSelect(radioSelect);
                            break;
                        }
                }
            }
            #endregion

            DataTable returnTable1 = daoS0012.GetSP1Data(searchDate1.ToShortDateString(), "1");
            DataTable returnTable2 = daoS0012.GetSP1Data(searchDate2.ToShortDateString(), "2");
            DataTable returnTable3 = daoS0012.GetSP1Data(searchDate3.ToShortDateString(), "3");

            returnTable1.Merge(returnTable2);
            returnTable1.Merge(returnTable3);

            if (returnTable1.Rows.Count == 0) {
                MessageBox.Show("無任何資料", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            gcMain.DataSource = returnTable1;
            gvMain.ExpandAllGroups();

            return ResultStatus.Success;
        }

        protected override ResultStatus Save(PokeBall poke) {
            gvMain.CloseEditor();
            gvMain.UpdateCurrentRow();
            ResultStatus resultStatus = ResultStatus.Fail;

            DataTable dt = (DataTable)gcMain.DataSource;
            DataTable dtChange = dt.GetChanges();

            if (dtChange != null) {
                if (dtChange.Rows.Count == 0) {
                    MessageBox.Show("沒有變更資料,不需要存檔!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return ResultStatus.FailButNext;
                }
                else {
                    if (daoS0012.DeleteSP2S() >= 0) {
                        DataTable insertSP2SData = daoS0012.GetSP2SColumns();
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            insertSP2SData.Rows.Add();
                            insertSP2SData.Rows[i]["SP2S_DATE"] = dt.Rows[i]["SP1_DATE"];
                            insertSP2SData.Rows[i]["SP2S_TYPE"] = dt.Rows[i]["SP1_TYPE"];
                            insertSP2SData.Rows[i]["SP2S_KIND_ID1"] = dt.Rows[i]["SP1_KIND_ID1"];
                            insertSP2SData.Rows[i]["SP2S_KIND_ID2"] = dt.Rows[i]["SP1_KIND_ID2"];
                            insertSP2SData.Rows[i]["SP2S_VALUE_DATE"] = txtCountDate.DateTimeValue;
                            insertSP2SData.Rows[i]["SP2S_OSW_GRP"] = dt.Rows[i]["SP1_OSW_GRP"];
                            insertSP2SData.Rows[i]["SP2S_SPAN_CODE"] = dt.Rows[i]["SP2_SPAN_CODE"];
                            insertSP2SData.Rows[i]["SP2S_ADJ_CODE"] = "Y";
                            insertSP2SData.Rows[i]["SP2S_W_TIME"] = DateTime.Now;
                            insertSP2SData.Rows[i]["SP2S_W_USER_ID"] = GlobalInfo.USER_ID;
                            insertSP2SData.Rows[i]["SP2S_USER_CM"] = dt.Rows[i]["SP1_USER_RATE"].AsDecimal() == 0 ? DBNull.Value : dt.Rows[i]["SP1_USER_RATE"];
                        }
                        resultStatus = base.Save_Override(insertSP2SData, "SP2S", DBName.CFO);
                        if (resultStatus == ResultStatus.Success) {
                            
                            PrintableComponent = gcMain;
                        }
                    }
                }
            }
            return resultStatus;
        }

        protected override ResultStatus ActivatedForm() {
            base.ActivatedForm();

            _ToolBtnSave.Enabled = true;
            _ToolBtnRetrieve.Enabled = true;
            _ToolBtnPrintAll.Enabled = true;

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

        private void adjustmentRadioGroup_SelectedIndexChanged(object sender, EventArgs e) {
            RadioGroup radios = sender as RadioGroup;

            #region Set SPAN CODE 
            switch (radios.Properties.Items[radios.SelectedIndex].Value.ToString()) {
                case "Clear": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                        }
                        break;
                    }
                case "AllSelect": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                        }
                        break;
                    }
                case "1": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            if (gvMain.GetRowCellValue(i, "GROUP_TYPE").AsString() == "1") {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                            }
                            else {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                            }
                        }
                        break;
                    }
                case "2": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            if (gvMain.GetRowCellValue(i, "GROUP_TYPE").AsString() == "2") {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                            }
                            else {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                            }
                        }
                        break;
                    }
                case "3": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            if (gvMain.GetRowCellValue(i, "GROUP_TYPE").AsString() == "3") {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                            }
                            else {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                            }
                        }
                        break;
                    }
                case "ETF": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            if (gvMain.GetRowCellValue(i, "APDK_PROD_SUBTYPE").AsString() == "S") {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                            }
                            else {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                            }
                        }
                        break;
                    }
                case "Index": {
                        for (int i = 0; i < gvMain.DataRowCount; i++) {
                            if (gvMain.GetRowCellValue(i, "APDK_PROD_SUBTYPE").AsString() == "I") {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", "Y");
                            }
                            else {
                                gvMain.SetRowCellValue(i, "SP2_SPAN_CODE", " ");
                            }
                        }
                        break;
                    }
            }
            #endregion
        }

        private DateTime GetDateByUserSelect(string userSelect) {

            DateTime txtDate = new DateTime();

            switch (userSelect) {
                case "1": {
                        txtDate = txtDate1.DateTimeValue;
                        break;
                    }
                case "2": {
                        txtDate = txtDate2.DateTimeValue;
                        break;
                    }
                case "3": {
                        txtDate = txtDate3.DateTimeValue;
                        break;
                    }
            }
            return txtDate;
        }
    }
}