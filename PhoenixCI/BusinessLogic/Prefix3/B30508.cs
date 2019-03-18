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
/// 20190313,john,最佳1檔加權平均委託買賣數量統計表(週) 
/// </summary>
namespace PhoenixCI.BusinessLogic.Prefix3
{
   /// <summary>
   /// 最佳1檔加權平均委託買賣數量統計表(週) 
   /// </summary>
   public class B30508
   {
      private string lsFile;
      private string startDateText;
      private string endDateText;
      /// <summary>
      /// 
      /// </summary>
      /// <param name="FilePath"></param>
      /// <param name="StartDate"></param>
      /// <param name="EndDate"></param>
      public B30508(string FilePath, string StartDate, string EndDate)
      {
         lsFile = FilePath;
         startDateText = StartDate;
         endDateText = EndDate;
      }

      private string CreateCsvFile(string saveFilePath)
      {
         //避免重複寫入
         try {
            if (File.Exists(saveFilePath)) {
               File.Delete(saveFilePath);
            }
            File.Create(saveFilePath).Close();
         }
         catch (Exception ex) {
            throw ex;
         }
         return saveFilePath;
      }

      /// <summary>
      /// 重複寫入文字並換行
      /// </summary>
      /// <param name="openData">檔案路徑</param>
      /// <param name="textToAdd">文字內容</param>
      private void WriteFile(string openData, string textToAdd)
      {
         using (FileStream fs = new FileStream(openData, FileMode.Append)) {
            using (StreamWriter writer = new StreamWriter(fs, Encoding.GetEncoding(950))) {
               writer.WriteLine(textToAdd);
            }
         }
      }

      /// <summary>
      /// 30508(賣)
      /// </summary>
      /// <param name="lsTab"></param>
      /// <param name="lsSellStr"></param>
      /// <param name="newdt"></param>
      /// <returns></returns>
      private static string WriteSellData(string lsTab, string lsSellStr, DataTable newdt)
      {
         //IF(sum(bst1_s_tot_sec for all)=0,0,round(sum(bst1_s_qnty_weight for all)/sum(bst1_s_tot_sec for all),2))
         //decimal sTotSecSUM = newdt.AsEnumerable().Select(q => q.Field<decimal>("BST1_S_TOT_SEC")).Sum();
         decimal sTotSecSUM = newdt.Compute("Sum(BST1_S_TOT_SEC)", "").AsDecimal();
         //decimal sQntyWeightSUM = newdt.AsEnumerable().Select(q => q.Field<decimal>("BST1_S_QNTY_WEIGHT")).Sum();
         decimal sQntyWeightSUM = newdt.Compute("Sum(BST1_S_QNTY_WEIGHT)", "").AsDecimal();
         lsSellStr = $"{lsSellStr}{lsTab}{(sTotSecSUM == 0 ? 0 : (sQntyWeightSUM / sTotSecSUM)).ToString("F2")}";//ls_str2 = ls_str2 + ls_tab + string(ids_1.getitemdecimal(1, "cp_s_qnty"));
         //ToString("F2")比Math.Round(sQntyWeightSUM / sTotSecSUM,2)還符合PB取的小數位
         return lsSellStr;
      }
      /// <summary>
      /// 30508(買)
      /// </summary>
      /// <param name="lsTab"></param>
      /// <param name="lsStr"></param>
      /// <param name="newdt"></param>
      /// <returns></returns>
      private static string WriteBuyData(string lsTab, string lsStr, DataTable newdt)
      {
         //IF(sum(bst1_b_tot_sec for all)=0,0,round(sum(bst1_b_qnty_weight for all)/sum(bst1_b_tot_sec for all),2))
         //decimal bTotSecSUM = newdt.Rows.Count<2? newdt.AsEnumerable().FirstOrDefault()["BST1_B_TOT_SEC"].AsDecimal():newdt.AsEnumerable().Select(q => q.Field<decimal>("BST1_B_TOT_SEC")).Sum();
         decimal bTotSecSUM = newdt.Compute("Sum(BST1_B_TOT_SEC)", "").AsDecimal();
         //decimal bQntyWeightSUM = newdt.Rows.Count < 2 ? newdt.AsEnumerable().FirstOrDefault()["BST1_B_QNTY_WEIGHT"].AsDecimal() : newdt.AsEnumerable().Select(q => q.Field<decimal>("BST1_B_QNTY_WEIGHT")).Sum();
         decimal bQntyWeightSUM = newdt.Compute("Sum(BST1_B_QNTY_WEIGHT)", "").AsDecimal();
         lsStr = $"{lsStr}{lsTab}{(bTotSecSUM == 0 ? 0 : (bQntyWeightSUM / bTotSecSUM)).ToString("F2")}";//ls_str = ls_str + ls_tab + string(ids_1.getitemdecimal(1, "cp_b_qnty"));
         //ToString("F2")比Math.Round(bQntyWeightSUM / bTotSecSUM,2)還符合PB取的小數位
         return lsStr;
      }

      /// <summary>
      /// wf_30508()
      /// </summary>
      /// <returns></returns>
      public bool Wf30508()
      {
         //產生檔案
         CreateCsvFile(lsFile);
         string lsSellFile = lsFile.Replace("30508(買)", "30508(賣)");
         CreateCsvFile(lsSellFile);
         try {
            string lsRptName = "股票期貨最近月份契約最佳1檔加權平均委託買進數量週資料統計表";
            string lsRptId = "30508";
            DateTime startDate = startDateText.AsDateTime();
            DateTime endDate = endDateText.AsDateTime();
            //讀取資料
            DataTable AI2dt = PbFunc.f_week(startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"));
            if (AI2dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{DateTime.Now.ToShortDateString()},30508－年月,無任何資料!");
               return true;
            }
            DataTable dt = new D30508().GetData(startDate, endDate);
            if (dt.Rows.Count <= 0) {
               MessageDisplay.Info($"{DateTime.Now.ToShortDateString()},{lsRptId}－{lsRptName}無任何資料!");
               return true;
            }
            /*統計表*/
            string lsTab = ",";
            //表頭
            string lsStr = lsTab + lsRptId + lsRptName;
            WriteFile(lsFile, lsStr);
            
            lsStr = lsTab + lsRptId + "股票期貨最近月份契約最佳1檔加權平均委託賣出數量週資料統計表";
            WriteFile(lsSellFile, lsStr);
            lsStr = "排序" + lsTab + "商品代碼" + lsTab + "商品名稱";
            foreach (DataRow row in AI2dt.Rows) {
               lsStr = lsStr + lsTab + $"{row["startDate"].AsDateTime().ToString("yyyy/MM/dd")}~{row["endDate"].AsDateTime().ToString("yyyy/MM/dd")}";
            }
            WriteFile(lsFile, lsStr);//FileWrite(li_FileNum, ls_str)
            WriteFile(lsSellFile, lsStr);//FileWrite(li_FileNum2, ls_str)
            lsStr = string.Empty;

            int seqNO = 0;
            for (int k = 0; k < dt.Rows.Count; k++) {
               seqNO = seqNO + 1;
               string lskindID = dt.Rows[k]["BST1_KIND_ID"].AsString();
               lsStr = seqNO.AsString() + lsTab + lskindID + lsTab + dt.Rows[k]["PDK_NAME"].AsString();
               string lsSellStr = lsStr;
               foreach (DataRow AI2row in AI2dt.Rows) {
                  DataTable newdt = dt.Filter($"BST1_KIND_ID = '{lskindID}' and BST1_YMD >= '{AI2row["startDate"].AsDateTime().ToString("yyyyMMdd")}' and BST1_YMD <= '{AI2row["endDate"].AsDateTime().ToString("yyyyMMdd")}'");
                  if (newdt.Rows.Count > 0) {
                     //30508(買)
                     lsStr = WriteBuyData(lsTab, lsStr, newdt);
                     //30508(賣)
                     lsSellStr = WriteSellData(lsTab, lsSellStr, newdt);

                     k = k + newdt.AsEnumerable().Select(q => q.Field<string>("BST1_KIND_ID")).Count();
                  }
                  else {
                     lsStr = lsStr + lsTab + "0";
                     lsSellStr = lsSellStr + lsTab + "0";
                  }
               }//foreach (DataRow AI2row in AI2dt.Rows)
               k = k - 1;
               WriteFile(lsFile, lsStr);//FileWrite(li_FileNum, ls_str)
               WriteFile(lsSellFile, lsSellStr);//FileWrite(li_FileNum2, ls_str2)
            }//for (int k = 0; k < dt.Rows.Count; k++)
         }
         catch (Exception ex) {
            MessageDisplay.Error(ex.Message, "wf_30508");
            return false;
         }
         return true;
      }
      
   }
}