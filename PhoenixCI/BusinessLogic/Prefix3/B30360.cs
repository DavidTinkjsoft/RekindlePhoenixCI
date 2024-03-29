﻿using BaseGround.Shared;
using Common;
using DataObjects.Dao.Together;
using DataObjects.Dao.Together.SpecificDao;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 20190304,john,股票選擇權交易概況表
/// </summary>
namespace PhoenixCI.BusinessLogic.Prefix3
{
   /// <summary>
   /// 股票選擇權交易概況表
   /// </summary>
   public class B30360
   {
      private D30360 dao30360;
      private string lsFile;
      private string emMonthText;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="FilePath">Excel_Template</param>
      /// <param name="DatetimeVal">em_month.Text</param>
      public B30360(string FilePath,string DatetimeVal)
      {
         lsFile = FilePath;
         emMonthText = DatetimeVal;
         dao30360 = new D30360();
      }
      /// <summary>
      /// 寫入sheet
      /// </summary>
      /// <param name="SheetName"></param>
      /// <param name="Dt"></param>
      /// <param name="RowIndex"></param>
      private void WriteSheet(string SheetName, DataTable Dt, int RowIndex,int RowTotal)
      {
         try {
            //切換Sheet
            Workbook workbook = new Workbook();
            workbook.LoadDocument(lsFile);
            Worksheet worksheet = workbook.Worksheets[SheetName];
            string lsYMD = "";
            worksheet.Range["A1"].Select();


            int addRowCount = 0;//總計寫入的行數
            foreach (DataRow row in Dt.Rows) {
               if (lsYMD != row["AI2_YMD"].AsString()) {
                  lsYMD = row["AI2_YMD"].AsString();
                  RowIndex = RowIndex + 1;
                  addRowCount++;
                  worksheet.Rows[RowIndex][1 - 1].Value = lsYMD.AsDateTime("yyyyMMdd").ToString("MM/dd");
               }
               if (row["AI2_PC_CODE"].AsString() == "C") {
                  worksheet.Rows[RowIndex][2 - 1].Value = row["AI2_M_QNTY"].AsDecimal();
               }
               else {
                  worksheet.Rows[RowIndex][3 - 1].Value = row["AI2_M_QNTY"].AsDecimal();
               }
               worksheet.Rows[RowIndex][6 - 1].Value = Dt.Compute("sum(AI2_MMK_QNTY)", $@"AI2_YMD='{lsYMD}'").AsDecimal();
               worksheet.Rows[RowIndex][8 - 1].Value = Dt.Compute("sum(AI2_OI)", $@"AI2_YMD='{lsYMD}'").AsDecimal();
            }
            //刪除空白列
            if (RowTotal > addRowCount) {
               //worksheet.Rows.Remove(RowIndex + 1, RowTotal - addRowCount);
               worksheet.Rows.Hide(RowIndex + 1, RowIndex + (RowTotal - addRowCount));
            }
            workbook.SaveDocument(lsFile);
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"B30360-WriteSheet");
         }
      }
      /// <summary>
      /// 寫入30362sheet&30367sheet
      /// </summary>
      /// <param name="RowIndex"></param>
      /// <param name="RowTotal"></param>
      /// <param name="SheetName"></param>
      /// <param name="dt"></param>
      private void WriteSheet2(int RowIndex, int RowTotal, string SheetName, DataTable dt)
      {
         //切換Sheet
         Workbook workbook = new Workbook();
         workbook.LoadDocument(lsFile);
         Worksheet worksheet = workbook.Worksheets[SheetName];
         worksheet.Range["A1"].Select();


         int addRowCount = 0;//總計寫入的行數
         foreach (DataRow row in dt.Rows) {
            string pdkName = row["PDK_NAME"].AsString();
            worksheet.Rows[RowIndex][1 - 1].Value = pdkName + $"({row["KIND_ID_2"].AsString()})";
            worksheet.Rows[RowIndex][2 - 1].Value = row["M_QNTY"].AsDecimal();
            pdkName = pdkName.SubStr(0, pdkName.IndexOf("選擇"));
            worksheet.Rows[RowIndex][4 - 1].Value = pdkName;
            RowIndex = RowIndex + 1;
            addRowCount++;
         }
         RowIndex = RowIndex - 1;
         //刪除空白列
         if (RowTotal > addRowCount) {
            //worksheet.Rows.Remove(RowIndex + 1, RowTotal - addRowCount);
            worksheet.Rows.Hide(RowIndex + 1, RowIndex + (RowTotal - addRowCount));
         }
         workbook.SaveDocument(lsFile);
      }

      /// <summary>
      /// wf_30361() 需確認sheet
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="IsKindID">商品種類</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30361(int RowIndex=1, int RowTotal=33 ,string IsKindID= "STO", string SheetName= "30361", string RptName= "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //當月第1天交易日
            DateTime StartDate = new DateTime(emMonthText.AsDateTime().Year, emMonthText.AsDateTime().Month, 01);
            //抓當月最後交易日
            string EndDate = dao30360.GetMaxLastDay30361(StartDate);
            //讀取資料
            DataTable dt = dao30360.Get30361Data(IsKindID, StartDate.ToString("yyyyMMdd"), EndDate);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{StartDate.ToShortDateString()}～{EndDate.AsDateTime().ToShortDateString()},{SheetName}－{RptName},{IsKindID}無任何資料!");
               return true;
            }
            //儲存寫入sheet
            WriteSheet(SheetName, dt, RowIndex, RowTotal);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30361-{SheetName}");
            return false;
         }
      }

      /// <summary>
      /// wf_30362()
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="IsKindID">商品種類</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30362(int RowIndex = 1, int RowTotal = 50, string IsKindID = "STO", string SheetName = "30362", string RptName = "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //讀取資料
            DataTable dt = dao30360.Get30362Data(emMonthText.AsDateTime(), IsKindID);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{emMonthText.AsDateTime().ToString("yyyyMM")},{SheetName}－{RptName},{IsKindID}無任何資料!");
               return true;
            }
            //儲存寫入sheet
            WriteSheet2(RowIndex, RowTotal, SheetName, dt);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30362-{SheetName}");
            return false;
         }
      }

      

      /// <summary>
      /// wf_30363()
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="IsKindID">商品種類</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30363(int RowIndex = 1, int RowTotal = 32, string IsKindID = "STO", string SheetName = "30363", string RptName = "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //當月第1天交易日
            DateTime StartDate = new DateTime(emMonthText.AsDateTime().Year, emMonthText.AsDateTime().Month, 01);
            //抓當月最後交易日
            string EndDate = dao30360.GetMaxLastDay30361(StartDate);
            //讀取資料
            DataTable dt = dao30360.Get30363Data(StartDate.ToString("yyyyMMdd"), EndDate, IsKindID);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{emMonthText.AsDateTime().ToString("yyyyMM")},{SheetName}－{RptName},{IsKindID}無任何資料!");
               return true;
            }
            //商品
            DataTable dtProd = dao30360.Get30363KindID2Data(StartDate.ToString("yyyyMMdd"), EndDate, IsKindID);
            //切換Sheet
            Workbook workbook = new Workbook();
            workbook.LoadDocument(lsFile);
            Worksheet worksheet = workbook.Worksheets[SheetName];
            worksheet.Range["A1"].Select();

            //表頭
            int columnIndex = 1;//Excel的Column位置
            foreach (DataRow row in dtProd.Rows) {
               string kindID = row["PDK_NAME"].AsString();
               worksheet.Rows[0][columnIndex].Value = kindID.SubStr(0, kindID.IndexOf("選擇權")) + $"({row["KIND_ID_2"].AsString()})";
               worksheet.Rows[1][columnIndex].Value = "買權";
               worksheet.Rows[1][columnIndex + 1].Value = "賣權";
               columnIndex = columnIndex + 2;
            }
            //內容
            int addRowCount = 0;//總計寫入的行數
            string lsYMD = "";
            foreach (DataRow row in dt.Rows) {
               if (lsYMD != row["AI2_YMD"].AsString()) {
                  lsYMD = row["AI2_YMD"].AsString();
                  RowIndex = RowIndex + 1;
                  addRowCount++;
                  worksheet.Rows[RowIndex][1 - 1].Value = lsYMD.AsDateTime("yyyyMMdd").ToString("MM/dd");
               }

               int foundIndex = dtProd.Rows.IndexOf(dtProd.Select($@"KIND_ID_2 ='{ row["AI2_KIND_ID_2"].AsString() }'").FirstOrDefault());
               if (foundIndex > -1) {
                  foundIndex = foundIndex * 2;
                  if (row["AI2_PC_CODE"].AsString() != "C") {
                     foundIndex = foundIndex + 1;
                  }
                  worksheet.Rows[RowIndex][foundIndex+1].Value = row["AI2_M_QNTY"].AsDecimal();//從第二欄開始寫
               }

            }//foreach (DataRow row in dt.Rows)
            //刪除空白列
            if (RowTotal > addRowCount) {
               //worksheet.Rows.Remove(RowIndex + 1, RowTotal - addRowCount);
               worksheet.Rows.Hide(RowIndex + 1, RowIndex + (RowTotal - addRowCount));
            }
            workbook.SaveDocument(lsFile);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30363-{SheetName}");
            return false;
         }
      }

      /// <summary>
      /// wf_30366()
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="IsKindID">商品種類</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30366(int RowIndex = 1, int RowTotal = 33, string SheetName = "30366", string RptName = "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //當月第1天交易日
            DateTime StartDate = new DateTime(emMonthText.AsDateTime().Year, emMonthText.AsDateTime().Month, 01);
            //抓當月最後交易日
            DateTime EndDate = new DateTime(emMonthText.AsDateTime().Year, emMonthText.AsDateTime().Month, 31);
            string lastDate=dao30360.GetMaxLastDay30366(StartDate, EndDate);
            //讀取資料
            DataTable dt = dao30360.Get30366Data("O", StartDate.ToString("yyyyMMdd"), lastDate);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{StartDate.ToShortDateString()}～{lastDate.AsDateTime("yyyyMMdd").ToShortDateString()},{SheetName}－{RptName},無任何資料!");
               return true;
            }
            //儲存寫入sheet
            WriteSheet(SheetName, dt, RowIndex, RowTotal);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30366-{SheetName}");
            return false;
         }
      }
      /// <summary>
      /// wf_30367()
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30367(int RowIndex = 1, int RowTotal = 100, string SheetName = "30367", string RptName = "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //讀取資料
            DataTable dt = dao30360.Get30367Data(emMonthText.AsDateTime());
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{emMonthText.AsDateTime().ToString("yyyyMM")},{SheetName}－{RptName},無任何資料!");
               return true;
            }
            //儲存寫入sheet
            WriteSheet2(RowIndex, RowTotal, SheetName, dt);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30367-{SheetName}");
            return false;
         }
      }

      /// <summary>
      /// wf_30368()
      /// </summary>
      /// <param name="RowIndex">Excel的Row位置</param>
      /// <param name="RowTotal">Excel的Column預留數</param>
      /// <param name="IsKindID">商品種類</param>
      /// <param name="SheetName">工作表名稱</param>
      /// <param name="RptName">作業名稱</param>
      /// <returns></returns>
      public bool Wf30368(int RowIndex = 1, int RowTotal = 32, string IsKindID = "STC", string SheetName = "30368", string RptName = "股票選擇權交易概況表")
      {
         /*************************************
         rowIndex = Excel的Row位置
         columnIndex = Excel的Column位置
         RowTotal = Excel的Column預留數
         lsYMD = 日期
         *************************************/
         try {
            //當月第1天交易日
            DateTime StartDate = new DateTime(emMonthText.AsDateTime().Year, emMonthText.AsDateTime().Month, 01);
            //抓當月最後交易日
            string EndDate = dao30360.GetMaxLastDay30361(StartDate);
            //讀取資料
            DataTable dt = dao30360.Get30368Data(StartDate.ToString("yyyyMMdd"), EndDate);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{emMonthText.AsDateTime().ToString("yyyyMM")},{SheetName}－{RptName},{IsKindID}無任何資料!");
               return true;
            }
            //切換Sheet
            Workbook workbook = new Workbook();
            workbook.LoadDocument(lsFile);
            Worksheet worksheet = workbook.Worksheets[SheetName];
            worksheet.Range["A1"].Select();
            //內容
            int addRowCount = 0;//總計寫入的行數
            string lsYMD = "";
            RowIndex = 2 - 1;
            foreach (DataRow row in dt.Rows) {
               if (lsYMD != row["DATA_YMD"].AsString()) {
                  lsYMD = row["DATA_YMD"].AsString();
                  RowIndex = RowIndex + 1;
                  addRowCount++;
                  worksheet.Rows[RowIndex][1 - 1].Value = lsYMD.AsDateTime("yyyyMMdd").ToString("MM/dd");
               }
               int columnIndex= row["SEQ_NO"].AsInt();
               columnIndex = columnIndex * 2-1;

               //首筆
               if (RowIndex == 2) {
                  string kindID = row["PDK_NAME"].AsString();
                  worksheet.Rows[0][columnIndex].Value = kindID.SubStr(0, kindID.IndexOf("選擇權")) + $"({row["DATA_KIND_ID_2"].AsString()})";
                  worksheet.Rows[1][columnIndex].Value = "買權";
                  worksheet.Rows[1][columnIndex + 1].Value = "賣權";
               }

               //成交量
               if (row["AI2_PC_CODE"].AsString() == "C") {
                  worksheet.Rows[RowIndex][columnIndex].Value = row["AI2_M_QNTY"].AsDecimal();
               }
               else {
                  worksheet.Rows[RowIndex][columnIndex+1].Value = row["AI2_M_QNTY"].AsDecimal();
               }
            }//foreach (DataRow row in dt.Rows)

            //刪除空白列
            if (RowTotal > addRowCount) {
               //worksheet.Rows.Remove(RowIndex + 1, RowTotal - addRowCount);
               worksheet.Rows.Hide(RowIndex + 1, RowIndex + (RowTotal - addRowCount));
            }
            workbook.SaveDocument(lsFile);
            return true;
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, $"Wf30368-{SheetName}");
            return false;
         }
      }

   }
}
