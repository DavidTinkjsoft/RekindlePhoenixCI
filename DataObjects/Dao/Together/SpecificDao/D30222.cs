﻿using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Lukas, 2019/3/18
/// </summary>
namespace DataObjects.Dao.Together.SpecificDao {
    public class D30222: DataGate {

        /// <summary>
        /// table: PLS1
        /// data for gcMain
        /// </summary>
        /// <param name="as_ymd">yyyyMMdd</param>
        /// <returns></returns>
        public DataTable d_30222(string as_ymd) {

            object[] parms = {
                ":as_ymd", as_ymd
            };

            string sql =
@"
SELECT PLS1_YMD AS PLS1_EFFECTIVE_YMD,
         PLS1_YMD,   
         NVL(KIND_ID2,PLS1_KIND_ID2) AS PLS1_KIND_ID2,   
         CASE WHEN F_KIND_ID2 IS NULL THEN ' '
              ELSE CASE WHEN KIND_ID2 <> NVL(F_KIND_ID2,' ')  THEN ' ' 
                        ELSE PLS1_FUT END END AS PLS1_FUT,   
         CASE WHEN O_KIND_ID2 IS NULL THEN ' '
              ELSE CASE WHEN KIND_ID2 <> NVL(O_KIND_ID2,' ')  THEN ' ' 
                        ELSE PLS1_OPT END END AS PLS1_OPT,   
         PLS1_SID,  
         PLS1_LEVEL_ADJ, 
         PLS1_CP_LEVEL,   
         PLS1_CP_NATURE,   
         PLS1_CP_LEGAL,   
         PLS1_CP_999,   
         PLS1_CUR_LEVEL,
         PLS1_CUR_NATURE,
         PLS1_CUR_LEGAL,
         PLS1_CUR_999,
         KIND_GRP2 ,
         PLS1_W_TIME,PLS1_W_USER_ID,
         ' ' AS OP_TYPE  ,
         PLS1_QNTY,   
         PLS1_STKOUT,   
         PLS1_CP_LEVEL AS PLS1_LEVEL_ORG,
         PLS1_LEVEL_ADJ AS PLS1_LEVEL_ADJ_ORG,
         CASE WHEN pls1_kind_id2 <> kind_grp2 THEN '小型' ELSE ' ' END as COMPUTE_1
    FROM CI.PLS1,
        --契約基本資料
        (SELECT NVL(F.APDK_KIND_GRP2,O.APDK_KIND_GRP2) AS KIND_GRP2,
                NVL(F.APDK_KIND_ID2,O.APDK_KIND_ID2) AS KIND_ID2,
                NVL(F.APDK_NAME,O.APDK_NAME) AS KIND_NAME,
                F.APDK_KIND_ID2 AS F_KIND_ID2,
                O.APDK_KIND_ID2 AS O_KIND_ID2,
                M_KIND_ID2
           FROM
               --期貨契約基本資料
               (SELECT APDK_KIND_GRP2,APDK_KIND_ID2,MAX(APDK_NAME) AS APDK_NAME,
                       --大型後面帶小型代號
                       MAX( CASE WHEN APDK_KIND_ID2 <> M_KIND_ID2 THEN M_KIND_ID2 ELSE ' ' END) AS M_KIND_ID2 
                  FROM CI.APDK,CI.PLS4,
                     (SELECT APDK_KIND_GRP2 AS M_KIND_GRP2,APDK_KIND_ID2 AS M_KIND_ID2
                        FROM CI.APDK
                       WHERE APDK_REMARK = 'M'
                       GROUP BY APDK_KIND_ID2,APDK_KIND_GRP2) M 
                 WHERE APDK_PROD_TYPE = 'F'
                   AND PLS4_YMD = :AS_YMD
                   AND PLS4_FUT = 'F'
                   AND PLS4_KIND_ID2 = APDK_KIND_ID2
                   AND APDK_KIND_GRP2 = M_KIND_GRP2(+)
                 GROUP BY APDK_KIND_GRP2,APDK_KIND_ID2) F
           FULL OUTER JOIN 
               --選擇權契約基本資料  
               (SELECT APDK_KIND_GRP2,APDK_KIND_ID2,MAX(APDK_NAME) AS APDK_NAME 
                  FROM CI.APDK,CI.PLS4  
                 WHERE APDK_PROD_TYPE = 'O'
                   AND PLS4_YMD = :AS_YMD
                   AND PLS4_OPT = 'O'
                   AND PLS4_KIND_ID2 = APDK_KIND_ID2
                 GROUP BY APDK_KIND_GRP2,APDK_KIND_ID2) O
             ON F.APDK_KIND_GRP2 = O.APDK_KIND_GRP2 AND
                F.APDK_KIND_ID2 = O.APDK_KIND_ID2 )   
   WHERE CI.PLS1.PLS1_YMD = :AS_YMD    
    AND  PLS1_KIND_ID2 = KIND_GRP2(+) 
   ORDER BY KIND_GRP2, PLS1_KIND_ID2
";
            DataTable dtResult = db.GetDataTable(sql, parms);

            return dtResult;
        }

        /// <summary>
        /// Table: PLS2
        /// </summary>
        /// <param name="as_ymd">yyyyMMdd</param>
        /// <returns></returns>
        public DataTable d_30203_pls2(string as_ymd) {

            object[] parms = {
                ":as_ymd", as_ymd
            };

            string sql =
@"
SELECT 
    PLS2_EFFECTIVE_YMD, 
    PLS2_YMD, 
    PLS2_KIND_ID2, 
    PLS2_FUT, 
    PLS2_OPT, 
    PLS2_SID, 
    PLS2_LEVEL_ADJ, 
    PLS2_LEVEL, 
    PLS2_NATURE, 
    PLS2_LEGAL, 
    PLS2_999, 
    PLS2_PREV_LEVEL, 
    PLS2_PREV_NATURE, 
    PLS2_PREV_LEGAL, 
    PLS2_PREV_999, 
    PLS2_KIND_GRP2,
    PLS2_W_TIME, 
    PLS2_W_USER_ID
FROM CI.PLS2
WHERE PLS2_YMD = :AS_YMD
";
            DataTable dtResult = db.GetDataTable(sql, parms);

            return dtResult;
        }

        public DataTable PostDate(string ls_ymd) {

            object[] parms = {
                ":ls_ymd", ls_ymd
            };

            string sql =
@"
select max(case when PLS2_LEVEL_ADJ = '-' then PLS2_EFFECTIVE_YMD else ' ' end) as LOWER_YMD,
           max(case when PLS2_LEVEL_ADJ <> '-' then PLS2_EFFECTIVE_YMD else ' ' end) as RAISE_YMD,
         count(*) as LI_COUNT
  --into :ls_eff_ymd_lower,:ls_eff_ymd,:li_count
  from ci.PLS2
 where PLS2_YMD = :ls_ymd
";
            DataTable dtResult = db.GetDataTable(sql, parms);

            return dtResult;
        }

        /// <summary>
        /// 判斷是否有已確認之資料
        /// </summary>
        /// <param name="ls_eff_ymd">yyyyMMdd</param>
        /// <returns></returns>
        public int checkData(string ls_eff_ymd) {

            object[] parms = {
                ":ls_eff_ymd",ls_eff_ymd
            };

            string sql =
@"
SELECT COUNT(*) AS I 
FROM CI.PLS2 
WHERE PLS2_YMD = :LS_EFF_YMD
";
            DataTable dtResult = db.GetDataTable(sql, parms);
            if (dtResult.Rows.Count > 0)
                return dtResult.Rows[0]["I"].AsInt();
            else
                return 0;
        }
    }
}