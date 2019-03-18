﻿using BaseGround.Shared;
using Common;
using DataObjects.Dao.Together;
using DataObjects.Dao.Together.SpecificDao;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using System;
using System.Data;
using System.Linq;
/// <summary>
/// 20190318,john,國內期貨交易概況表
/// </summary>
namespace PhoenixCI.BusinessLogic.Prefix3
{
   /// <summary>
   /// 國內期貨交易概況表
   /// </summary>
   public class B30520
   {
      private string lsFile;
      private string emMonthText;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="FilePath">Excel_Template</param>
      /// <param name="datetime">em_month.Text</param>
      public B30520(string FilePath,string datetime)
      {
         lsFile = FilePath;
         emMonthText = datetime;
      }

      private static int IDFGtype(DataRow row)
      {
         int columnIndex = 0;
         string codeCol = "AM2_BS_CODE";
         string code = "B";
         if (row["AM2_IDFG_TYPE"].AsString() == "A") {
            /* 期貨自營 */
            columnIndex = (row[codeCol].AsString() == code ? 2 : 3) - 1;
         }
         else {
            /* 期貨經紀 */
            columnIndex = (row[codeCol].AsString() == code ? 4 : 5) - 1;
         }

         return columnIndex;
      }

      /// <summary>
      /// wf_30521
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <returns></returns>
      public string Wf30521(int RowIndex = 4, string RptName = "國內期貨交易概況表")
      {
         string flowStepDesc= "開始轉出資料";
         try {

            //切換Sheet
            flowStepDesc = "切換Sheet";
            Workbook workbook = new Workbook();
            workbook.LoadDocument(lsFile);
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Range["A1"].Select();

            //總列數,隱藏於A3
            int rowTotal = RowIndex + worksheet.Cells["A3"].Value.AsInt();

            //起始年份,隱藏於B3
            string firstYear = worksheet.Cells["B3"].Value.AsString();

            /******************
               讀取資料
               分三段：
               1.年
               2.當年1月至當月合計
               3.當年1月至當月明細
            ******************/
            flowStepDesc = "讀取資料";
            DataTable dt = new D30520().List30521(firstYear, PbFunc.Left(emMonthText, 4), $"{PbFunc.Left(emMonthText, 4)}01", emMonthText.Replace("/", ""));
            if (dt.Rows.Count <= 0) {
               return $"{firstYear}~{PbFunc.Left(emMonthText, 4)},{PbFunc.Left(emMonthText, 4)}01～{emMonthText.Replace("/", "")},30521－{RptName},無任何資料!";
            }
            /* 成交量 & OI */
            flowStepDesc = "讀取成交量資料";
            DataTable dtAI2 = new D30520().List30521AI2(firstYear, PbFunc.Left(emMonthText, 4), $"{PbFunc.Left(emMonthText, 4)}01", emMonthText.Replace("/", ""));
            if (dtAI2.Rows.Count <= 0) {
               return $"{firstYear}~{PbFunc.Left(emMonthText, 4)},{PbFunc.Left(emMonthText, 4)}01～{emMonthText.Replace("/", "")},30521－{RptName},無任何資料!";
            }

            //寫入內容
            flowStepDesc = "寫入內容";
            string lsYMD = "";
            int rowEndIndex = 0;
            string endYMD = dt.AsEnumerable().LastOrDefault()["AM2_YMD"].AsString();
            foreach (DataRow row in dt.Rows) {
               if (lsYMD != row["AM2_YMD"].AsString()) {
                  lsYMD = row["AM2_YMD"].AsString();
                  RowIndex = RowIndex + 1;

                  /* 最後一列時 */
                  if (lsYMD == endYMD) {
                     rowEndIndex = RowIndex;
                     RowIndex=rowTotal;
                  }
                  /* 年度資料 */
                  if (lsYMD.Length==4) {
                     worksheet.Rows[RowIndex][1 - 1].Value = PbFunc.Left(lsYMD, 4).AsInt();
                  }
                  else {
                     worksheet.Rows[RowIndex][1 - 1].Value = PbFunc.f_get_month_eng_name(PbFunc.Right(lsYMD, 2).AsInt(), "1");
                  }//if (lsYMD.Length==4)
                  /* 成交量 & OI */
                  int foundIndex = dtAI2.Rows.IndexOf(dtAI2.Select($@"ai2_ymd ='{lsYMD}'").FirstOrDefault());
                  if (foundIndex > -1) {
                     worksheet.Rows[RowIndex][6 - 1].Value = dtAI2.Rows[foundIndex]["AI2_M_QNTY"].AsDecimal();
                     worksheet.Rows[RowIndex][7 - 1].Value = dtAI2.Rows[foundIndex]["AI2_OI"].AsDecimal();
                  }
               }//if (lsYMD != row["AM2_YMD"].AsString())

               //判斷欄位
               int columnIndex = IDFGtype(row);
               worksheet.Rows[RowIndex][columnIndex].Value = row["AM2_M_QNTY"].AsDecimal();
            }//foreach (DataRow row in dt.Rows)

            //刪除空白列
            flowStepDesc = "刪除空白列";
            if (rowTotal > rowEndIndex) {
               worksheet.Range[$"{rowEndIndex}:{rowTotal + 1 - 1}"].Delete(DeleteMode.EntireRow);
               worksheet.ScrollTo(0, 0);//直接滾動到最上面，不然看起來很像少行數
            }
            workbook.SaveDocument(lsFile);
            return MessageDisplay.MSG_OK;
         }
         catch (Exception ex) {
#if DEBUG
            throw new Exception($"wf30520-{flowStepDesc}:" + ex.Message);
#else
            throw ex;
#endif
         }
      }

      
   }
}
