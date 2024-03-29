﻿using BaseGround;
using BaseGround.Shared;
using BusinessObjects.Enums;
using Common;
using DataObjects.Dao.Together;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors.Controls;
using System;
using System.Data;
using System.IO;

/// <summary>
/// Winni, 2019/02/11
/// </summary>
namespace PhoenixCI.FormUI.Prefix3 {
   /// <summary>
   /// 30592 匯率類期貨交易概況表
   /// 有寫到的功能：Export
   /// </summary>
   public partial class W30592 : FormParent {

      protected D30592 dao30592;
      protected COD daoCod;

      public W30592(string programID , string programName) : base(programID , programName) {
         InitializeComponent();
         dao30592 = new D30592();
         daoCod = new COD();
         this.Text = _ProgramID + "─" + _ProgramName;
         txtStartYMD.Text = GlobalInfo.OCF_DATE.ToString("yyyy/MM/01");
         txtEndYMD.Text = GlobalInfo.OCF_DATE.ToString("yyyy/MM/dd");

         //商品
         DataTable dtProd = daoCod.ListByTxn("30592"); //cod_id/cod_desc/cp_display
         ddlProd.SetDataTable(dtProd , "COD_DESC" , "COD_DESC");

         //Winni test
         //20181001-20181011
      }

      protected override ResultStatus ActivatedForm() {
         base.ActivatedForm();

         _ToolBtnExport.Enabled = true;

         return ResultStatus.Success;
      }

      protected override ResultStatus Export() {

         base.Export();
         lblProcessing.Visible = true;

         /*************************************
            chkGroup.Items[0] = MonQnty
            chkGroup.Items[1] = OI
            chkGroup.Items[2] = MonCnt
            chkGroup.Items[3] = Amt
            chkGroup.Items[4] = Acc
            chkGroup.Items[5] = Id
            chkGroup.Items[6] = Rmb
         *************************************/

         //判斷是否執行各交易所RMB期貨交易量的開啟複製檔案 30592_RMB
         if (chkGroup.Items[6].CheckState.ToString() == "Checked") {
            wf_ExportRmb();
         }

         //先判斷是否有勾選前六項至少其中一項,才執行開啟複製檔案30592
         if (chkGroup.Items[0].CheckState.ToString() == "Checked" || chkGroup.Items[1].CheckState.ToString() == "Checked" ||
            chkGroup.Items[2].CheckState.ToString() == "Checked" || chkGroup.Items[3].CheckState.ToString() == "Checked" ||
            chkGroup.Items[4].CheckState.ToString() == "Checked" || chkGroup.Items[5].CheckState.ToString() == "Checked") {

            string tempMarketCode;
            //RadioButton (rbMarket0 = 一般 / rbMarket1 = 盤後 / rbMarketAll = 全部)
            if (gbMarket.EditValue.ToString() == "rbMarket0") {
               tempMarketCode = "一般";
            } else if (gbMarket.EditValue.ToString() == "rbMarket1") {
               tempMarketCode = "盤後";
            } else {
               tempMarketCode = "全部";
            }

            //1.複製檔案 & 開啟檔案 (因檔案需因MarketCode更動，所以另外寫)
            string originalFilePath = Path.Combine(GlobalInfo.DEFAULT_EXCEL_TEMPLATE_DIRECTORY_PATH , _ProgramID + "." + FileType.XLS.ToString().ToLower());

            string destinationFilePath = Path.Combine(GlobalInfo.DEFAULT_REPORT_DIRECTORY_PATH ,
                _ProgramID + "_" + tempMarketCode + "_" + DateTime.Now.ToString("yyyy.MM.dd") + "-" + DateTime.Now.ToString("HH.mm.ss") + "." + FileType.XLS.ToString().ToLower());

            File.Copy(originalFilePath , destinationFilePath , true);

            Workbook workbook = new Workbook();
            workbook.LoadDocument(destinationFilePath);
            Worksheet worksheet = workbook.Worksheets[0];

            //2.填資料
            bool result = false;
            int ii_ole_row = 1;
            result = wf_Export(workbook , worksheet);  //function 30592

            if (!result) {
               try {
                  workbook = null;
                  System.IO.File.Delete(destinationFilePath);
               } catch (Exception) {
                  //
               }
               return ResultStatus.Fail;
            }

            //3.存檔
            workbook.SaveDocument(destinationFilePath);
            lblProcessing.Visible = false;

            return ResultStatus.Success;
         }

         return ResultStatus.Fail;
      }

      //function wf_30592
      private bool wf_Export(Workbook workbook , Worksheet worksheet) {

         try {
            string ls_kind_id2, ls_pc_code, ls_expiry_type, ls_prod_type, ls_param_key, ls_market_code;
            int li_col, ii_ole_row = 1;
            long ll_found;

            #region 讀取資料(每日)
            ls_prod_type = "F";
            ls_param_key = "%";
            ls_pc_code = "%";
            ls_kind_id2 = "%";
            #endregion

            #region 交易時段
            if (gbMarket.EditValue.ToString() == "rbMarket0") {
               ls_market_code = "0";
            } else if (gbMarket.EditValue.ToString() == "rbMarket1") {
               ls_market_code = "1";
            } else {
               ls_market_code = "%";
            }

            string StartYMD = txtStartYMD.Text.Replace("/" , "");
            string EndYMD = txtEndYMD.Text.Replace("/" , "");

            DataTable dt = new DataTable();
            dt = dao30592.GetData(StartYMD , EndYMD , ls_market_code);
            if (dt.Rows.Count <= 0) {
               lblProcessing.Text = String.Format("{0}:yyyyMMdd" , EndYMD) + "," + _ProgramID + '－' + _ProgramName + ",無任何資料!";
               MessageDisplay.Info(string.Format("{0:yyyyMMdd} ~ {1:yyyyMMdd} , {2}, RHF&RTF無成交量資料!" , StartYMD , EndYMD , this.Text));
               return false;
            }

            #endregion

            #region 表頭      
            ii_ole_row = 1;
            li_col = 2;

            if (chkGroup.Items[0].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "總交易量 [(買+賣)/2]";
            }

            if (chkGroup.Items[1].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "未平倉量[(買+賣)/2]";
            }

            if (chkGroup.Items[2].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "成交筆數(買+賣)";
            }

            if (chkGroup.Items[3].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "成交金額(台幣) [(買+賣)/2]";
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "成交金額(原始幣別) [(買+賣)/2]";
            }

            if (chkGroup.Items[4].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "交易戶數(買+賣)";
            }

            if (chkGroup.Items[5].CheckState.ToString() == "Checked") {
               li_col += 1;
               worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = "交易人數(ID數)(買+賣)";
            }
            #endregion

            #region 內容
            if(ddlProd.Text != "% (全部)") {
               DataView dv = dt.AsDataView();
               dv.RowFilter = "apdk_param_key ='" + ddlProd.Text.Trim() + "'";
               DataTable dtByProd = dv.ToTable();
               for (int i = 0 ; i < dtByProd.Rows.Count ; i++) {
                  ii_ole_row += 1;
                  worksheet.Cells[ii_ole_row - 1 , 0].Value = dtByProd.Rows[i]["APDK_YMD"].AsString();
                  worksheet.Cells[ii_ole_row - 1 , 1].Value = dtByProd.Rows[i]["APDK_PARAM_KEY"].AsString();
                  li_col = 2;

                  if (chkGroup.Items[0].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AI2_M_QNTY"].AsDecimal();
                  }

                  if (chkGroup.Items[1].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AI2_OI"].AsDecimal();
                  }

                  if (chkGroup.Items[2].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dt.Rows[i]["AM10_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AM10_CNT"].AsDecimal();
                  }

                  if (chkGroup.Items[3].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dtByProd.Rows[i]["AA2_AMT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AA2_AMT"].AsDecimal();
                     li_col += 1;
                     if (dtByProd.Rows[i]["AA2_AMT_ORG_CURRENCY"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AA2_AMT_ORG_CURRENCY"].AsDecimal();
                  }

                  if (chkGroup.Items[4].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dtByProd.Rows[i]["AM9_ACC_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AM9_ACC_CNT"].AsDecimal();
                  }

                  if (chkGroup.Items[5].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dtByProd.Rows[i]["AB4_ID_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dtByProd.Rows[i]["AB4_ID_CNT"].AsDecimal();
                  }
               }
            } else {
               for (int i = 0 ; i < dt.Rows.Count ; i++) {
                  ii_ole_row += 1;
                  worksheet.Cells[ii_ole_row - 1 , 0].Value = dt.Rows[i]["APDK_YMD"].AsString();
                  worksheet.Cells[ii_ole_row - 1 , 1].Value = dt.Rows[i]["APDK_PARAM_KEY"].AsString();
                  li_col = 2;

                  if (chkGroup.Items[0].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AI2_M_QNTY"].AsDecimal();
                  }

                  if (chkGroup.Items[1].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AI2_OI"].AsDecimal();
                  }

                  if (chkGroup.Items[2].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dt.Rows[i]["AM10_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AM10_CNT"].AsDecimal();
                  }

                  if (chkGroup.Items[3].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dt.Rows[i]["AA2_AMT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AA2_AMT"].AsDecimal();
                     li_col += 1;
                     if (dt.Rows[i]["AA2_AMT_ORG_CURRENCY"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AA2_AMT_ORG_CURRENCY"].AsDecimal();
                  }

                  if (chkGroup.Items[4].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dt.Rows[i]["AM9_ACC_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AM9_ACC_CNT"].AsDecimal();
                  }

                  if (chkGroup.Items[5].CheckState.ToString() == "Checked") {
                     li_col += 1;
                     if (dt.Rows[i]["AB4_ID_CNT"] != DBNull.Value)
                        worksheet.Cells[ii_ole_row - 1 , li_col - 1].Value = dt.Rows[i]["AB4_ID_CNT"].AsDecimal();
                  }
               }           
            }
            #endregion

            return true;
         } catch (Exception ex) { //失敗寫LOG
            PbFunc.f_write_logf(_ProgramID , "error" , ex.Message);
            lblProcessing.Visible = false;
            return false;
         }
      }

      private bool wf_ExportRmb() {
         try {
            //1.複製檔案 & 開啟檔案
            string excelDestinationPath = CopyExcelTemplateFile(_ProgramID + "_RMB" , FileType.XLS);
            Workbook workbook = new Workbook();
            workbook.LoadDocument(excelDestinationPath);
            Worksheet worksheet = workbook.Worksheets[0];

            //2.讀取資料(每日)
            string StartYMD = txtStartYMD.Text.Replace("/" , "");
            string EndYMD = txtEndYMD.Text.Replace("/" , "");

            DataTable dt = new DataTable();
            dt = dao30592.GetRmbData(StartYMD , EndYMD);
            if (dt.Rows.Count <= 0) {
               lblProcessing.Text = String.Format("{0}:yyyyMMdd" , EndYMD) + "," + _ProgramID + '－' + _ProgramName + ",無任何資料!";
               MessageDisplay.Info(string.Format("{0:yyyyMMdd} ~ {1:yyyyMMdd} , {2}, RHF&RTF無成交量資料!" , StartYMD , EndYMD , this.Text));
               return false;
            }

            int ii_ole_row = 4;
            for (int i = 0 ; i < dt.Rows.Count ; i++) {
               ii_ole_row += 1;              
               for (int j = 0 ; j < 6 ; j++) {
                  if (j == 0) {
                     worksheet.Cells[ii_ole_row - 1 , j].Value = dt.Rows[i][j].AsString().SubStr(0 , 10);
                  } else {
                     if (String.IsNullOrEmpty(dt.Rows[i][j].AsString()))
                        worksheet.Cells[ii_ole_row - 1 , j].Value = "-";
                     else
                        worksheet.Cells[ii_ole_row - 1 , j].Value = dt.Rows[i][j].AsDecimal();
                  }

                  
               }

               //RHF,RTF沒交易
               if (String.IsNullOrEmpty(dt.Rows[i]["rhf_vol"].AsString()))
                  worksheet.Cells[ii_ole_row - 1 , 6].Value = "-";
            }

            //3.刪除空白列
            if (dt.Rows.Count < 100) {
               worksheet.Rows.Remove(ii_ole_row + 1 , 120 - (dt.Rows.Count + 20)); //原dt的row數再留20行
            }
            worksheet.Range["A2"].Select();

            //4.存檔
            workbook.SaveDocument(excelDestinationPath);
            lblProcessing.Visible = false;
            return true;
         } catch (Exception ex) { //失敗寫LOG
            PbFunc.f_write_logf(_ProgramID , "error" , ex.Message);
            lblProcessing.Visible = false;
            return false;
         }

      }

      private void ddlProd_EditValueChanged(object sender , EventArgs e) {
         if (ddlProd.EditValue.ToString() == "RHF" || ddlProd.EditValue.ToString() == "RTF") {
            chkGroup.Items[6].Enabled = true;
         } else {
            chkGroup.Items[6].Enabled = false;
            chkGroup.Items[6].CheckState = System.Windows.Forms.CheckState.Unchecked;
         }
      }
   }
}