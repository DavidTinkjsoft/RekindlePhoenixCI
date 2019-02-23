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
using DataObjects.Dao.Together;
using Common;
using DataObjects.Dao.Together.SpecificDao;
using DevExpress.XtraEditors.Controls;
using BaseGround.Shared;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using BusinessObjects.Enums;
using DevExpress.XtraGrid.Views.Grid;
using BusinessObjects;
using static DataObjects.Dao.DataGate;

namespace PhoenixCI.FormUI.PrefixS
{
    public partial class WS0070 : FormParent
    {
        protected DS0070 daoS0070;
        protected COD daoCod;
        protected string is_fm_ymd;
        protected string is_to_ymd;
        protected string oldREQValue;
        protected DateTime is_max_ymd;
        protected DateTime startDateOldValue;
        protected DateTime endDateOldValue;
        protected DataTable periodTable;
        protected DataTable REQTable;

        public WS0070(string programID, string programName) : base(programID, programName) {
            InitializeComponent();
            daoS0070 = new DS0070();
            daoCod = new COD();

            repositoryItemTextEdit3.Leave += repositoryItemTextEdit3_Leave;
        }

        protected override ResultStatus Retrieve() {
            base.Retrieve(gcPresTest);
            GridHelper.SetCommonGrid(gvPresTest);
            GridHelper.SetCommonGrid(gvExAccount);

            #region Set Date Period
            //save 後替換新值
            DataTable dtSPN = daoS0070.GetPeriodByUserId("ST", GlobalInfo.USER_ID);
            if (dtSPN.Rows.Count <= 0) {
                is_fm_ymd = DateTime.Now.AddDays(-60).ToString("yyyyMMdd");
                is_to_ymd = DateTime.Now.ToString("yyyyMMdd");
                is_max_ymd = new AOCF().GetMaxDate(is_fm_ymd, is_to_ymd);

                txtStartDate.DateTimeValue = DateTime.ParseExact(is_max_ymd.ToString("yyyy/MM/dd"), "yyyy/MM/dd", null);
                txtEndDate.DateTimeValue = DateTime.ParseExact(is_max_ymd.ToString("yyyy/MM/dd"), "yyyy/MM/dd", null);
                startDateOldValue = txtStartDate.DateTimeValue;
                endDateOldValue = txtEndDate.DateTimeValue;
            }
            else {
                txtStartDate.DateTimeValue = DateTime.ParseExact(dtSPN.Rows[0]["SPAN_PERIOD_START_DATE"].AsString(), "yyyyMMdd", null);
                txtEndDate.DateTimeValue = DateTime.ParseExact(dtSPN.Rows[0]["SPAN_PERIOD_END_DATE"].AsString(), "yyyyMMdd", null);
                startDateOldValue = txtStartDate.DateTimeValue;
                endDateOldValue = txtEndDate.DateTimeValue;
            }
            #endregion

            #region Set Drop Down Lsit
            //保證金類別
            DataTable cbxSpanReqTypeSource = daoCod.ListByCol2("S0070", "span_req_type");
            SPAN_REQ_TYPE.SetDataTable(cbxSpanReqTypeSource, "COD_ID");

            //設定方式
            RepositoryItemLookUpEdit cbxParamType = new RepositoryItemLookUpEdit();
            DataTable cbxParamTypeSource = daoCod.ListByCol2("S0071", "span_param_type");
            cbxParamType.SetColumnLookUp(cbxParamTypeSource, "COD_ID", "COD_DESC", TextEditStyles.DisableTextEditor, "");
            gcPresTest.RepositoryItems.Add(cbxParamType);
            SPAN_PARAM_TYPE.ColumnEdit = cbxParamType;

            //設定值
            RepositoryItemLookUpEdit cbxParamValue = new RepositoryItemLookUpEdit();
            DataTable cbxParamValueSource = daoCod.ListByCol2("S0071", "span_param_value");
            DataTable dtParamValueData = daoS0070.GetParamData("ST", GlobalInfo.USER_ID);//DB現有資料
            DataTable dtTempParamValue = cbxParamValueSource.Clone();
            for (int i = 0; i < dtParamValueData.Rows.Count; i++) {
                //參數檔案
                dtTempParamValue.Rows.Add();
                dtTempParamValue.Rows[i].SetField("COD_ID", dtParamValueData.Rows[i]["span_param_value"]);
                dtTempParamValue.Rows[i].SetField("COD_DESC", dtParamValueData.Rows[i]["span_param_value"]);
                dtTempParamValue.Rows[i].SetField("CP_DISPLAY", dtParamValueData.Rows[i]["span_param_value"]);
            }
            DataView dtDistinc = new DataView(dtTempParamValue);
            dtTempParamValue = dtDistinc.ToTable(true);
            dtTempParamValue.PrimaryKey = new DataColumn[] { dtTempParamValue.Columns["COD_ID"] };
            cbxParamValueSource.PrimaryKey = new DataColumn[] { cbxParamValueSource.Columns["COD_ID"] };
            cbxParamValueSource.Merge(dtTempParamValue, false);
            cbxParamValue.SetColumnLookUp(cbxParamValueSource, "COD_ID", "COD_DESC", TextEditStyles.Standard, "");
            cbxParamValue.ProcessNewValue += new ProcessNewValueEventHandler(cbxParamValue_ProcessNewValue);
            gcPresTest.RepositoryItems.Add(cbxParamValue);
            SPAN_PARAM_VALUE.ColumnEdit = cbxParamValue;

            //波動度設定
            RepositoryItemLookUpEdit cbxParamVolType = new RepositoryItemLookUpEdit();
            DataTable cbxParamVolTypeSource = daoCod.ListByCol2("S0071", "span_param_vol_type");
            cbxParamVolType.SetColumnLookUp(cbxParamVolTypeSource, "COD_ID", "COD_DESC", TextEditStyles.DisableTextEditor, "");
            gcPresTest.RepositoryItems.Add(cbxParamVolType);
            SPAN_PARAM_VOL_TYPE.ColumnEdit = cbxParamVolType;

            //商品類別
            DataTable dtProdType = daoS0070.dddw_zparm_comb_prod(txtEndDate.DateTimeValue.ToString("yyyyMMdd"));
            RepositoryItemLookUpEdit cbxProdType = new RepositoryItemLookUpEdit();
            cbxProdType.SetColumnLookUp(dtProdType, "PROD_GROUP", "PROD_GROUP", TextEditStyles.DisableTextEditor, "");
            gcPresTest.RepositoryItems.Add(cbxProdType);
            SPAN_PARAM_CLASS.ColumnEdit = cbxProdType;

            //商品組合
            DataTable dtProd = daoS0070.dddw_zparm_comb_prod_by_group(txtEndDate.DateTimeValue.ToString("yyyyMMdd"), "%%", "%%");
            RepositoryItemLookUpEdit cbxProd = new RepositoryItemLookUpEdit();
            cbxProd.SetColumnLookUp(dtProd, "COMB_PROD_VALUE", "COMB_PROD", TextEditStyles.DisableTextEditor, "");
            gcPresTest.RepositoryItems.Add(cbxProd);
            SPAN_PARAM_CC.ColumnEdit = cbxProd;

            //製作連動下拉選單(觸發事件)
            gvPresTest.ShownEditor += GridView_ShownEditor;
            cbxProdType.EditValueChanged += cbxProdType_EditValueChanged;
            #endregion

            #region Set REQ Value
            DataTable dtREQ = daoS0070.GetREQDataByUser("ST", GlobalInfo.USER_ID);
            if (dtREQ.Rows.Count > 0) {
                SPAN_REQ_TYPE.EditValue = dtREQ.Rows[0]["SPAN_REQ_TYPE"].AsString();
                txtREQValue.Text = dtREQ.Rows[0]["SPAN_REQ_VALUE"].AsString();
                oldREQValue = SPAN_REQ_TYPE.EditValue.ToString();
            }
            #endregion

            #region Retrieve grid
            DataTable exAccountTable = daoS0070.GetExAccountData("ST");
            DataTable presTestTable = daoS0070.GetParamData("ST", GlobalInfo.USER_ID);
            if (exAccountTable.Rows.Count == 0) {
                MessageBox.Show("移除帳號無任何資料", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (exAccountTable.Rows.Count == 0) {
                MessageBox.Show("壓力測試設定無任何資料", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            gcExAccount.DataSource = exAccountTable;
            gcPresTest.DataSource = presTestTable;

            #endregion
            return ResultStatus.Success;
        }

        protected override ResultStatus Save(PokeBall args) {
            gvExAccount.CloseEditor();
            gvExAccount.UpdateCurrentRow();
            gvPresTest.CloseEditor();
            gvPresTest.UpdateCurrentRow();
            _IsPreventFlowPrint = true;
            ResultStatus resultStatus = ResultStatus.Fail;

            if (GridHasModified() != "") {
                //更新四部份資料 先更新日期區間
                if (CheckPeriod()) {
                    resultStatus = savePeriod();
                }
                else {
                    return ResultStatus.FailButNext;
                }
                //更新測試保證金設定
                if (CheckREQValue()) {
                    resultStatus = saveREQ();
                }
                else {
                    return ResultStatus.FailButNext;
                }
                if (resultStatus == ResultStatus.Success) {
                    //更新特定帳號排除設定
                    resultStatus = saveExAccount();
                    if (resultStatus == ResultStatus.Success) {
                        //更新測試變化設定
                        return savePresTest();
                    }
                    else {
                        return resultStatus;
                    }
                }
                else {
                    return resultStatus;
                }
            }
            else if (txtStartDate.DateTimeValue == startDateOldValue
                    && txtEndDate.DateTimeValue == endDateOldValue
                    && txtREQValue.EditValue.ToString() == oldREQValue) {
                MessageBox.Show("沒有變更資料,不需要存檔!", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return ResultStatus.FailButNext;
            }
            else //只更新日期區間 & 壓力測試保證金
            {
                if (CheckPeriod()) {
                    resultStatus = savePeriod();
                }
                else {
                    return ResultStatus.FailButNext;
                }
                if (CheckREQValue()) {
                    resultStatus = saveREQ();
                }
                else {
                    return ResultStatus.FailButNext;
                }
            }
            return ResultStatus.Success;
        }

        private ResultStatus savePeriod() {
            periodTable = daoS0070.GetPeriodByUserId("ST", GlobalInfo.USER_ID);
            periodTable.Rows[0].SetField("span_period_start_date", txtStartDate.DateTimeValue.ToString("yyyyMMdd"));
            periodTable.Rows[0].SetField("span_period_end_date", txtEndDate.DateTimeValue.ToString("yyyyMMdd"));
            periodTable.Rows[0].SetField("span_period_w_time", DateTime.Now);

            return base.Save_Override(periodTable, "SPAN_PERIOD", DBName.CFO);
        }

        private ResultStatus saveREQ() {
            REQTable = daoS0070.GetREQDataByUser("ST", GlobalInfo.USER_ID);
            REQTable.Rows[0].SetField("SPAN_REQ_MODULE", "ST");
            REQTable.Rows[0].SetField("SPAN_REQ_TYPE", SPAN_REQ_TYPE.EditValue);
            REQTable.Rows[0].SetField("SPAN_REQ_VALUE", txtREQValue.Text);
            REQTable.Rows[0].SetField("SPAN_REQ_W_TIME", DateTime.Now);

            return base.Save_Override(REQTable, "SPAN_REQ", DBName.CFO);
        }

        private ResultStatus saveExAccount() {
            DataTable dtExAccount = (DataTable)gcExAccount.DataSource;

            for (int i = 0; i < dtExAccount.Rows.Count; i++) {
                if (dtExAccount.Rows[i].RowState != DataRowState.Deleted) {
                    dtExAccount.Rows[i].SetField("span_acct_w_time", DateTime.Now);

                    if (dtExAccount.Rows[i]["SPAN_ACCT_FCM_NO"].AsString().SubStr(4, 3) == "999") {
                        MessageDisplay.Info("期貨商代號欄位必須為7碼，末3碼不為999");
                        return ResultStatus.FailButNext;
                    }
                }
            }
            return base.Save_Override(dtExAccount, "SPAN_ACCT", DBName.CFO);
        }

        private ResultStatus savePresTest() {
            DataTable dtPresTest = (DataTable)gcPresTest.DataSource;

            for (int i = 0; i < dtPresTest.Rows.Count; i++) {
                if (dtPresTest.Rows[i].RowState != DataRowState.Deleted) {
                    dtPresTest.Rows[i].SetField("SPAN_PARAM_W_TIME", DateTime.Now);

                    if (dtPresTest.Rows[i]["span_param_type"].AsString() == "2") {
                        switch (dtPresTest.Rows[i]["span_param_value"].AsString()) {
                            case "1":
                            case "2":
                            case "3":
                            case "4": {//最大漲跌停
                                    break;
                                }
                            default: {
                                    MessageDisplay.Info("設定方式選漲跌停價格, 則設定值請選擇1, 2, 3或最大漲跌停");
                                    return ResultStatus.FailButNext;
                                }
                        }
                    }
                }
            }
            return base.Save_Override(dtPresTest, "SPAN_PARAM", DBName.CFO);
        }

        protected override ResultStatus AfterOpen() {
            base.AfterOpen();
            Retrieve();
            return ResultStatus.Success;
        }

        protected override ResultStatus ActivatedForm() {
            base.ActivatedForm();

            _ToolBtnSave.Enabled = true;
            _ToolBtnRetrieve.Enabled = true;
            _ToolBtnRun.Enabled = true;

            return ResultStatus.Success;
        }

        private void GridView_ShownEditor(object sender, EventArgs e) {
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn.FieldName == "SPAN_PARAM_CC") {
                string prodType = gvPresTest.GetFocusedRowCellValue(SPAN_PARAM_CLASS).ToString();
                LookUpEdit edit = (LookUpEdit)view.ActiveEditor;
                DataTable dtProd = new DataTable();
                RepositoryItemLookUpEdit cbxProd = new RepositoryItemLookUpEdit();
                //修改商品組合下拉清單(重綁data source)
                switch (prodType) {
                    case "EQT-STF":
                    case "EQT-ETF": {
                            string[] prodEQT = prodType.Split('-');
                            dtProd = daoS0070.dddw_zparm_comb_prod_by_group(txtEndDate.DateTimeValue.ToString("yyyyMMdd"), "%" + prodEQT[0] + "%", "%" + prodEQT[1] + "%");
                            break;
                        }
                    default: {
                            dtProd = daoS0070.dddw_zparm_comb_prod_by_group(txtEndDate.DateTimeValue.ToString("yyyyMMdd"), "%" + prodType + "%", "%%");
                            break;
                        }
                }
                cbxProd.SetColumnLookUp(dtProd, "COMB_PROD_VALUE", "COMB_PROD");
                edit.Properties.DataSource = cbxProd.DataSource;
                edit.ShowPopup();
            }
        }

        private void gvPresTest_InitNewRow(object sender, InitNewRowEventArgs e) {
            GridView gv = sender as GridView;
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["SPAN_PARAM_MODULE"], "ST");
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["SPAN_PARAM_USER_ID"], GlobalInfo.USER_ID);
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["SPAN_PARAM_EXPIRY"], 0);
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["IS_NEWROW"], 1);
        }

        private void gvEXAccount_InitNewRow(object sender, InitNewRowEventArgs e) {
            GridView gv = sender as GridView;
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["SPAN_ACCT_MODULE"], "ST");
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["SPAN_ACCT_USER_ID"], GlobalInfo.USER_ID);
            gv.SetRowCellValue(gv.FocusedRowHandle, gv.Columns["EXACCOUNT_IS_NEWROW"], 1);
        }

        private void cbxProdType_EditValueChanged(object sender, EventArgs e) {
            gvPresTest.PostEditor();
            gvPresTest.SetFocusedRowCellValue("SPAN_PARAM_CC", null);
        }

        private void cbxParamValue_ProcessNewValue(object sender, ProcessNewValueEventArgs e) {
            LookUpEdit lookUpEdit = sender as LookUpEdit;
            DataTable table = lookUpEdit.Properties.DataSource as DataTable;
            string value = e.DisplayValue.ToString();
            table.Rows.Add(new object[] { value, value, table.Rows.Count, value }).ToString();
            table.AcceptChanges();
            e.DisplayValue = value;
            e.Handled = true;
        }

        private void btnEXAccountAdd_Click(object sender, EventArgs e) {
            base.InsertRow(gvExAccount);
            gvExAccount.FocusedColumn = gvExAccount.Columns[0];
        }

        private void btnEXAccountDel_Click(object sender, EventArgs e) {
            base.DeleteRow(gvExAccount);
        }

        private void btnPreTestAdd_Click(object sender, EventArgs e) {
            base.InsertRow(gvPresTest);
            gvPresTest.FocusedColumn = gvPresTest.Columns[0];

        }

        private void btnPreTestDel_Click(object sender, EventArgs e) {
            base.DeleteRow(gvPresTest);
        }

        private void btnPresTestClear_Click(object sender, EventArgs e) {
            if (MessageDisplay.Choose("確定刪除所有資料?").AsBool()) {
                while (gvPresTest.DataRowCount != 0)
                    gvPresTest.DeleteRow(0);
            }
        }

        private void txtStartDate_Leave(object sender, EventArgs e) {
            CheckPeriod();
        }

        private void txtEndDate_Leave(object sender, EventArgs e) {
            CheckPeriod();
        }

        private void txtREQValue_Leave(object sender, EventArgs e) {
            CheckREQValue();
        }

        private void repositoryItemTextEdit3_Leave(object sender,EventArgs e) {
            TextEdit editor= sender as TextEdit;
            string check = editor.Text.SubStr(4, 3);
            if (check == "999") {
                MessageDisplay.Info("期貨商代號欄位必須為7碼，末3碼不為999");
                editor.Select();
            }
        }

        private bool CheckPeriod() {
            bool check = true;

            if (txtEndDate.DateTimeValue.Subtract(txtStartDate.DateTimeValue).Days > 31) {
                MessageDisplay.Info("日期區間不可超過31天!");
                txtEndDate.Select();
                check = false;
            }
            else if (txtStartDate.DateTimeValue > txtEndDate.DateTimeValue) {
                MessageDisplay.Info("起始值不可大於迄止值!");
                txtStartDate.Select();
                check = false;
            }
            return check;
        }

        private bool CheckREQValue() {
            if (double.Parse(txtREQValue.Text) < 0.1 || double.Parse(txtREQValue.Text) > 5) {
                MessageDisplay.Info("倍數輸入範圍為 0.10 - 5.00");
                txtREQValue.Select();
                return false;
            }
            return true;
        }

        private string GridHasModified() {
            DataTable dtExAccount = (DataTable)gcExAccount.DataSource;
            DataTable dtPresTest = (DataTable)gcPresTest.DataSource;
            DataTable dtExAccountChange = dtExAccount.GetChanges();
            DataTable dtPresTestChange = dtPresTest.GetChanges();

            string changeGrid = "";

            if (dtExAccountChange != null) {
                if (dtExAccountChange.Rows.Count > 0) {
                    changeGrid = "ExAccount";
                }
            }
            if (dtPresTestChange != null) {
                if (dtPresTestChange.Rows.Count > 0) {
                    changeGrid += " PresTest";
                }
            }
            return changeGrid;
        }
    }
}