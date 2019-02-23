﻿using OnePiece;
using System;
using System.Data;

/// <summary>
/// Winni 2019/01/23
/// </summary>
namespace DataObjects.Dao.Together {
   public class D30630 {
      private Db db;

      public D30630() {
         db = GlobalDaoSetting.DB;
      }

      /// <summary>
      /// Get data by CI.RPT CI.AI2, CI.AM21 (已經固定一些過濾條件)
      /// </summary>
      /// <param name="as_market_code">全部/一般/盤後</param>
      /// <param name="as_prev_sym">前期起始日</param>
      /// <param name="as_prev_eym">前期結束日</param>
      /// <param name="as_sum_subtype">選全部"0"/非全部"3"</param>
      /// <param name="as_param_key">商品名</param>
      /// <param name="as_aft_sym">後期起始日</param>
      /// <param name="as_aft_eym">後期結束日</param>
      /// <returns></returns>
      public DataTable GetData(string as_market_code , string as_prev_sym , string as_prev_eym , string as_sum_subtype ,
                               string as_param_key , string as_aft_sym , string as_aft_eym) {
         object[] parms =
         {
                ":as_market_code", as_market_code,
                ":as_prev_sym", as_prev_sym,
                ":as_prev_eym", as_prev_eym,
                ":as_sum_subtype", as_sum_subtype,
                ":as_param_key", as_param_key,
                ":as_aft_sym", as_aft_sym,
                ":as_aft_eym", as_aft_eym
            };

         string sql = @"
select A.*
FROM
(select nvl(P.AM21_IDFG_TYPE,A.AM21_IDFG_TYPE) as AM21_IDFG_TYPE,   
       nvl(P.AM21_M_QNTY,0) as AM21_M_QNTY_PREV,   
       nvl(P.AM21_OI_QNTY,0) as AM21_OI_QNTY_PREV,  
       P_DAYS.TRADE_DAYS as TRADE_DAYS_PREV,   
       nvl(A.AM21_M_QNTY,0) as AM21_M_QNTY_AFT,   
       nvl(A.AM21_OI_QNTY,0) as AM21_OI_QNTY_AFT,   
       A_DAYS.TRADE_DAYS as TRADE_DAYS_AFT,
       RPT_SEQ_NO
from   ci.RPT,
        --前期
        (select AM21_IDFG_TYPE,   
                SUM(case :as_market_code when '0%' then AM21_M_QNTY - NVL(AM21_AH_M_QNTY,0) 
                                         when '1%' then NVL(AM21_AH_M_QNTY,0) 
                                         else AM21_M_QNTY end) AS AM21_M_QNTY,
                SUM(AM21_OI_QNTY) as AM21_OI_QNTY
        FROM CI.AM21
        where AM21_SUM_TYPE = 'M'
        and AM21_YMD >= :as_prev_sym
        and AM21_YMD <= :as_prev_eym 
        and AM21_SUM_SUBTYPE = :as_sum_subtype
        and AM21_PARAM_KEY    like :as_param_key
        group by AM21_IDFG_TYPE) P,
        --前期交易天數
        (select COUNT(AI2_YMD) as TRADE_DAYS
        from ci.AI2
        where AI2_YMD >= :as_prev_sym||'01'
        and AI2_YMD <= :as_prev_eym||'31'
        and AI2_SUM_TYPE = 'D'
        and AI2_SUM_SUBTYPE = '1'
        and AI2_PROD_TYPE = 'F'
        and (:as_market_code <> '1%' or nvl(AI2_AH_DAY_COUNT,0) > 0)) P_DAYS,
        --後期  
        (select AM21_IDFG_TYPE,   
                SUM(case :as_market_code when '0%' then AM21_M_QNTY - NVL(AM21_AH_M_QNTY,0) 
                                         when '1%' then NVL(AM21_AH_M_QNTY,0) 
                                         else  AM21_M_QNTY end) AS AM21_M_QNTY, 
                SUM(AM21_OI_QNTY) as AM21_OI_QNTY
         FROM CI.AM21
         where AM21_SUM_TYPE = 'M'
         and AM21_YMD >= :as_aft_sym
         and AM21_YMD <= :as_aft_eym 
         and AM21_SUM_SUBTYPE = :as_sum_subtype
         and AM21_PARAM_KEY    like :as_param_key
         group by AM21_IDFG_TYPE) A,
        --前期交易天數
        (select COUNT(AI2_YMD) as TRADE_DAYS
         from ci.AI2
         where AI2_YMD >= :as_aft_sym||'01'
         and AI2_YMD <= :as_aft_eym||'31'
         and AI2_SUM_TYPE = 'D'
         and AI2_SUM_SUBTYPE = '1'
         and AI2_PROD_TYPE = 'F'
         and (:as_market_code <> '1%' or nvl(AI2_AH_DAY_COUNT,0) > 0)) A_DAYS
where RPT_TXD_ID = '30631'
and RPT_VALUE = P.AM21_IDFG_TYPE(+)
and RPT_VALUE = A.AM21_IDFG_TYPE(+) ) A
where am21_m_qnty_prev + am21_oi_qnty_prev + am21_m_qnty_aft + am21_oi_qnty_aft > 0
order by rpt_seq_no
";

         DataTable dtResult = db.GetDataTable(sql , parms);

         return dtResult;
      }

   }
}