﻿using BusinessObjects;
using OnePiece;
using System;
using System.Data;

namespace DataObjects.Dao.Together
{
    public class DPT
    {
        private Db db;

        public DPT()
        {
            db = GlobalDaoSetting.DB;
        }

        public DataTable ListData()
        {
            #region sql

            string sql =
                @"   
                    SELECT  DPT.*,  DPT_ID || '：' || DPT_NAME AS DPT_ID_NAME
                    FROM    ci.DPT
                ";

            #endregion

            DataTable dtResult = db.GetDataTable(sql, null);

            return dtResult;
        }
    }
}
