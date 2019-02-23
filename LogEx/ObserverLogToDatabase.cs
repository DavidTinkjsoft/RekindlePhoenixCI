
using DataObjects.Dao.Together;
using System;

namespace Log
{
    /// <summary>
    /// �NLog�ɼg���Ʈw
    /// </summary>
    public class ObserverLogToDatabase : ILog
    {
        public void Log(object sender, LogEventArgs e)
        {
            LOGF myLOGF = new LOGF();
            myLOGF.Insert(e.LOGF_USER_ID, e.LOGF_TXN_ID, e.LOGF_KEY_DATA, e.LOGF_JOB_TYPE);
        }
    }
}
