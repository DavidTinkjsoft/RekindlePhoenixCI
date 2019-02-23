﻿using ActionService.DbDirect;
using ActionServiceW.DbDirect.Prefix;
using BaseGround.Report;
using BaseGround.Shared;
using BusinessObjects;
using BusinessObjects.Enums;
using Common;
using Common.Config;
using Common.Helper;
using DataObjects.Dao;
using DataObjects.Dao.Together;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid;
using Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace BaseGround {
   public partial class FormParent : DevExpress.XtraBars.Ribbon.RibbonForm {
      private string _DefaultFileNamePath;
      private string _ReportID;
      private string _ReportTitle;
      private string _ReportClass;
      private object _PrintableComponent;
      private DataTable _DataSource;

      protected string _ProgramID;
      protected string _ProgramName;

      protected BarButtonItem _ToolBtnInsert;
      protected BarButtonItem _ToolBtnSave;
      protected BarButtonItem _ToolBtnDel;
      protected BarButtonItem _ToolBtnRetrieve;
      protected BarButtonItem _ToolBtnRun;
      protected BarButtonItem _ToolBtnImport;
      protected BarButtonItem _ToolBtnExport;
      protected BarButtonItem _ToolBtnPrintAll;

      /// <summary>
      /// 防止接下來的Print事件觸發
      /// </summary>
      protected bool _IsPreventFlowPrint = false;

      /// <summary>
      /// 防止接下來的Export事件觸發
      /// </summary>
      protected bool _IsPreventFlowExport = false;
      protected ServiceCommon serviceCommon;
      protected ServicePrefix1 servicePrefix1 = new ServicePrefix1();
      /// <summary>
      /// 預設報表物件
      /// </summary>
      protected defReport defReport;
      /// <summary>
      /// 將顯示元件(像是Grid)設定用來列印或匯出用
      /// </summary>
      public object PrintableComponent {
         get {
            return _PrintableComponent;
         }

         set {
            _PrintableComponent = value;
         }
      }

      /// <summary>
      /// 用來檢查資料是否修改過
      /// </summary>
      public DataTable DataSource {
         get {
            return _DataSource;
         }

         set {
            _DataSource = value;
         }
      }

      /// <summary>
      /// 用測試帳號登入,可看到一些隱藏UI,之後要調整
      /// </summary>
      public bool FlagAdmin {
         get {
            //if (GlobalInfo.USER_ID == sqlca.servername)
            //   return true;
            //else
            return false;
         }
      }

      private UserProgInfo _UserProgInfo;

      /// <summary>
      /// 紀錄著目前登錄的使用者和目前使用程式的代碼等資訊
      /// </summary>
      public UserProgInfo UserProgInfo {
         get {
            if (_UserProgInfo == null) {
               _UserProgInfo = new UserProgInfo();
               _UserProgInfo.UserID = GlobalInfo.USER_ID;
               _UserProgInfo.TxnID = _ProgramID;
            }

            return _UserProgInfo;
         }
      }

      public FormParent() {
         InitializeComponent();

         PaintFormBorder();
      }

      public FormParent(string program_id , string program_name) {
         InitializeComponent();

         PaintFormBorder();

         _ProgramID = program_id;
         _ProgramName = program_name;
         _ReportClass = "R" + _ProgramID;

         _DefaultFileNamePath = Path.Combine(GlobalInfo.DEFAULT_REPORT_DIRECTORY_PATH , "CI_" + _ProgramID + "─" + _ProgramName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
         _ReportID = GlobalInfo.SYSTEM_ALIAS + _ProgramID;
         _ReportTitle = _ProgramID + "─" + _ProgramName + GlobalInfo.REPORT_TITLE_MEMO;
      }

      private void FormParent_Load(object sender , EventArgs e) {
         Open();
      }

      private void FormParent_Shown(object sender , EventArgs e) {
         AfterOpen();
      }

      private void FormParent_Activated(object sender , EventArgs e) {
         SetAllToolBtnDisable();

         ActivatedForm();
      }

      private void FormParent_FormClosing(object sender , FormClosingEventArgs e) {
         ResultStatus myResultStatus = ResultStatus.Fail;

         myResultStatus = BeforeClose();

         if (myResultStatus != ResultStatus.Success) {
            e.Cancel = true;
         }
      }

      public ResultStatus ProcessSaveFlow() {
         ResultData resultData = new ResultData();

         serviceCommon = new ServiceCommon();
         try {
            resultData.Status = CheckShield();

            if (resultData.Status != ResultStatus.Success) return resultData.Status;

            resultData = serviceCommon.MultiActionTransaction(Save);

            if (resultData.Status == ResultStatus.Fail) {
               SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , resultData.returnString , "X");

               return resultData.Status;
            }

            if (!_IsPreventFlowPrint) {
               ReportHelper reportHelperForPrint = PrintOrExportSetting();

               resultData.Status = Print(reportHelperForPrint);

               if (resultData.Status != ResultStatus.Success) return resultData.Status;
            }

            if (!_IsPreventFlowExport) {
               ReportHelper reportHelperForExport = PrintOrExportSetting();

               resultData.Status = Export(reportHelperForExport);

               if (resultData.Status != ResultStatus.Success) return resultData.Status;
            }

            if (resultData.Status == ResultStatus.Success) {
               SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , "SAVE COMPLETED" , " ");

               return COMPLETE();
            }

            return resultData.Status;
         } catch (Exception ex) {
            resultData.Status = ResultStatus.Fail;
            SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , ex.Message , "X");
            MessageDisplay.Error("儲存錯誤");

            return resultData.Status;
         }
      }

      public void ProcessInsert() {
         InsertRow();
      }

      public void ProcessDelete() {
         DeleteRow();
      }

      public void ProcessRetrieve() {
         Retrieve();
      }

      public void ProcessRun() {
         PokeBall args = new PokeBall();

         if (RunBefore(args) == ResultStatus.Success) {
            AsyncHelper.DoWorkAsync(() => {
               return Run(args);
            } ,
            (result) => {
               if (result == ResultStatus.Success) {
                  this.BeginInvoke(new MethodInvoker(() => {
                     RunAfter(args);
                  }));
               }
            } ,
            (exception) => {
               MessageBox.Show(exception.Message);
            });
         }
      }

      public void ProcessImport() {
         Import();
      }

      public void ProcessExport() {
         string startTime = DateTime.Now.ToString("HH:mm:ss");

         WriteLog("Export" , "Operation" , "E");
         ResultStatus result = Export();

         if (result == ResultStatus.Success)
            ExportAfter(startTime);
      }

      public virtual void ProcessPrintAll(ReportHelper reportHelper) {
         reportHelper = PrintOrExportSetting();
         reportHelper.IsPrintedFromPrintButton = true;
         Print(reportHelper);
         MessageDisplay.Info(MessageDisplay.MSG_PRINT);

         Export(reportHelper);
      }

      public virtual ResultStatus BeforeOpen() {
         if (DesignMode) return ResultStatus.Success;

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Open() {
         if (DesignMode) return ResultStatus.Success;

         if (MdiParent != null) {
            _ToolBtnInsert = ((FormMain)MdiParent).toolStripButtonInsert;
            _ToolBtnDel = ((FormMain)MdiParent).toolStripButtonDelete;
            _ToolBtnSave = ((FormMain)MdiParent).toolStripButtonSave;
            _ToolBtnRetrieve = ((FormMain)MdiParent).toolStripButtonRetrieve;
            _ToolBtnRun = ((FormMain)MdiParent).toolStripButtonRun;
            _ToolBtnImport = ((FormMain)MdiParent).toolStripButtonImport;
            _ToolBtnExport = ((FormMain)MdiParent).toolStripButtonExport;
            _ToolBtnPrintAll = ((FormMain)MdiParent).toolStripButtonPrintAll;
         }

         SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , "ChildFormOpen" , " ");

         return ResultStatus.Success;
      }

      protected virtual ResultStatus AfterOpen() {
         if (DesignMode) return ResultStatus.Success;

         return ResultStatus.Success;
      }

      protected virtual ResultStatus ActivatedForm() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus CheckShield() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus CheckShield(Control control) {
         GridHelper.AcceptText(control);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Save(PokeBall args) {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus Save(Control control) {
         GridHelper.AcceptText(control);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Save_Override(DataTable dt , string tableName , DBName dBName = DBName.CI) {
         DataGate DG = new DataGate();
         ResultData resultData = DG.Save_Override(dt , tableName , dBName);
         if (resultData.Status == ResultStatus.Fail) {
            throw new Exception(resultData.returnString);
         }
         return resultData.Status;
      }

      protected virtual ResultStatus Retrieve() {
         SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , "Retrieve" , "R");

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Retrieve(Control control) {
         GridHelper.AcceptText(control);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus RunBefore(PokeBall args) {
         string inputDate = args.OcfDate.ToString("yyyy/MM/dd");
         string nowDate = DateTime.Now.ToString("yyyy/MM/dd");

         if (inputDate != nowDate) {
            if (MessageDisplay.Choose("交易日期(" + inputDate + ") 不等於今日(" + nowDate + ")，是否要繼續?") == DialogResult.No) {
               return ResultStatus.Fail;
            }
         }

         if (servicePrefix1.HasLogspDone(Convert.ToDateTime(inputDate) , _ProgramID)) {
            if (MessageDisplay.Choose(_ProgramID + " 作業 " + inputDate + "「曾經」執行過，\n是否要繼續？\n\n★★★建議先執行 [預覽] 確認執行狀態") == DialogResult.No) {
               return ResultStatus.Fail;
            }
         }

         GridHelper.AcceptText(args.GridControlMain);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Run(PokeBall args) {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus Run(GridControl gc) {
         GridHelper.AcceptText(gc);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus RunAsync(PokeBall args) {
         this.Invoke(new MethodInvoker(() => {
            FormWait formWait = new FormWait();
            Point pointWait = new Point();

            int totalLeft = this.Parent.Left + this.Left;
            int totalTop = this.Parent.Top + this.Top;

            pointWait.X = totalLeft + (this.Width / 2) - (formWait.Width / 2);
            pointWait.Y = totalTop + (this.Height / 2) - (formWait.Height / 2);

            SplashScreenManager.ShowForm(this , typeof(FormWait) , true , true , SplashFormStartPosition.Manual , pointWait , ParentFormState.Locked);
         }));


         GridView gv = (GridView)args.GridControlMain.MainView;

         DataTable dtLOGSPForRuned = servicePrefix1.ListLogspForRunned(args.OcfDate , _ProgramID);
         DataView dvLOGSPForRuned = new DataView(dtLOGSPForRuned);

         for (int i = 0 ; i < gv.RowCount ; i++) {
            string TXF_SERVER = gv.GetRowCellValue(i , "TXF_SERVER").AsString();
            string TXF_DB = gv.GetRowCellValue(i , "TXF_DB").AsString();
            string TXF_TXN_ID = gv.GetRowCellValue(i , "TXF_TXN_ID").AsString();
            int TXF_SEQ_NO = gv.GetRowCellValue(i , "TXF_SEQ_NO").AsInt();
            string TXF_TYPE = gv.GetRowCellValue(i , "TXF_TYPE").AsString();
            string TXF_TID = gv.GetRowCellValue(i , "TXF_TID").AsString();
            string TXF_TID_NAME = gv.GetRowCellValue(i , "TXF_TID_NAME").AsString();
            string TXF_DEFAULT = gv.GetRowCellValue(i , "TXF_DEFAULT").AsString();
            string TXF_REDO = gv.GetRowCellValue(i , "TXF_REDO").AsString();
            string TXF_ARG = gv.GetRowCellValue(i , "TXF_ARG").AsString();

            if (TXF_DEFAULT == "1") {
               DateTime LOGSP_DATE = args.OcfDate;
               string LOGSP_TXN_ID = _ProgramID;
               int LOGSP_SEQ_NO = TXF_SEQ_NO;
               string LOGSP_TID = TXF_TID;
               string LOGSP_TID_NAME = TXF_TID_NAME;
               DateTime LOGSP_BEGIN_TIME = new DateTime();
               DateTime LOGSP_END_TIME = new DateTime();
               string LOGSP_MSG = "";

               if (TXF_REDO == "N") {
                  dvLOGSPForRuned.RowFilter = "TXF_TID='" + TXF_TID + "'";
                  if (dvLOGSPForRuned.Count != 0) {
                     if (MessageDisplay.Choose(TXF_TID + " ★★★曾經執行過且不可重覆執行，是否強迫繼續執行 ?") == DialogResult.No) {
                        return ResultStatus.Fail;
                     }
                  }
               }

               #region 開始執行

               LOGSP_BEGIN_TIME = DateTime.Now;

               ResultData resultData = new ResultData();

               switch (TXF_TYPE) {
                  case "I":
                     resultData = serviceCommon.ExecuteInfoWorkFlow(TXF_TID , UserProgInfo);
                     break;
                  case "S":
                     List<DbParameterEx> listParams = null;

                     // 如果這個SP有參數的話
                     if (TXF_ARG == "Y") {
                        DataTable dtTXFPARM = serviceCommon.ListTXFPARM(TXF_SERVER , TXF_DB , TXF_TXN_ID , TXF_TID);

                        if (dtTXFPARM.Rows.Count > 0) {
                           listParams = new List<DbParameterEx>();
                        }

                        foreach (DataRow row in dtTXFPARM.Rows) {
                           string TXFPARM_ARG = row["TXFPARM_ARG"].AsString();
                           string TXFPARM_ARG_TYPE = row["TXFPARM_ARG_TYPE"].AsString();
                           string TXFPARM_DEFAULT = row["TXFPARM_DEFAULT"].AsString();

                           DbParameterEx paramEx;

                           switch (TXFPARM_ARG) {
                              case "":
                                 paramEx = new DbParameterEx("" , TXFPARM_DEFAULT);
                                 listParams.Add(paramEx);
                                 break;
                              case "em_ymd":
                                 paramEx = new DbParameterEx("" , args.OcfDate.ToString("yyyyMMdd"));
                                 listParams.Add(paramEx);
                                 break;
                              case "em_ym":
                                 paramEx = new DbParameterEx("" , args.OcfDate.ToString("yyyyMM"));
                                 listParams.Add(paramEx);
                                 break;
                              case "em_date":
                                 paramEx = new DbParameterEx();
                                 paramEx.DbType = DbTypeEx.Date;
                                 paramEx.Name = "";
                                 paramEx.Value = args.OcfDate;
                                 listParams.Add(paramEx);
                                 break;
                              default:
                                 break;
                           }
                        }
                     }

                     ConnectionInfo connectionInfo = SettingDragons.Instance.GetConnectionInfo(TXF_DB);

                     try {
                        resultData = serviceCommon.ExecuteStoredProcedure(connectionInfo , string.Format("{0}.{1}" , TXF_DB , TXF_TID) , listParams , true);
                     } catch (Exception ex) {
                        resultData.Status = ResultStatus.Fail;
                        MessageDisplay.Error(ex.Message);
                     }

                     break;
                  case "W":
                     resultData.Status = ExecuteForm();
                     break;
                  default:
                     break;
               }

               LOGSP_END_TIME = DateTime.Now;

               if (resultData.Status == ResultStatus.Success) {
                  LOGSP_MSG = "執行正常完成";
               } else {
                  LOGSP_MSG = "作業執行失敗";

                  servicePrefix1.SaveLogsp(LOGSP_DATE , LOGSP_TXN_ID , LOGSP_SEQ_NO , LOGSP_TID , LOGSP_TID_NAME , LOGSP_BEGIN_TIME , LOGSP_END_TIME , LOGSP_MSG);

                  MessageDisplay.Error("序號" + LOGSP_SEQ_NO + "的" + LOGSP_TID + "," + LOGSP_MSG);

                  this.Invoke(new MethodInvoker(() => {
                     SplashScreenManager.CloseForm();
                     gv.SetRowCellValue(i , "ERR_MSG" , LOGSP_MSG);
                  }));

                  return ResultStatus.Fail;
               }

               this.Invoke(new MethodInvoker(() => { gv.SetRowCellValue(i , "ERR_MSG" , LOGSP_MSG); }));

               servicePrefix1.SaveLogsp(LOGSP_DATE , LOGSP_TXN_ID , LOGSP_SEQ_NO , LOGSP_TID , LOGSP_TID_NAME , LOGSP_BEGIN_TIME , LOGSP_END_TIME , LOGSP_MSG);

               #endregion

               #region 執行特別的程式

               args.TXF_TID = TXF_TID;
               this.Invoke(new MethodInvoker(() => { RunAfterEveryItem(args); }));

               #endregion
            } else {
               // 沒勾選項目的話清空狀態
               this.Invoke(new MethodInvoker(() => { gv.SetRowCellValue(i , "ERR_MSG" , ""); }));
            }
         }

         this.Invoke(new MethodInvoker(() => { SplashScreenManager.CloseForm(); }));

         return ResultStatus.Success;
      }

      protected virtual ResultStatus ExecuteForm() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus RunAfterEveryItem(PokeBall args) {
         if (args.TXF_TID == "wf_CI_CIOPF") {
            DataTable dtTxemail = servicePrefix1.ListTxemail(_ProgramID , 1);

            if (dtTxemail.Rows.Count != 0) {
               string TXEMAIL_SENDER = dtTxemail.Rows[0]["TXEMAIL_SENDER"].AsString();
               string TXEMAIL_RECIPIENTS = dtTxemail.Rows[0]["TXEMAIL_RECIPIENTS"].AsString();
               string TXEMAIL_CC = dtTxemail.Rows[0]["TXEMAIL_CC"].AsString();
               string TXEMAIL_TITLE = dtTxemail.Rows[0]["TXEMAIL_TITLE"].AsString();
               string TXEMAIL_TEXT = dtTxemail.Rows[0]["TXEMAIL_TEXT"].AsString();

               string attachmentFilePath = Path.Combine(GlobalInfo.DEFAULT_REPORT_DIRECTORY_PATH , _ProgramID + ".csv");

               this.Invoke(new MethodInvoker(() => {
                  // 先把結果產生出csv檔，再用成附件寄出去
                  Retrieve();
                  args.GridControlSecond.ExportToCsv(attachmentFilePath);
               }));

               MailHelper.SendEmail(TXEMAIL_SENDER , TXEMAIL_RECIPIENTS , TXEMAIL_CC , TXEMAIL_TITLE , TXEMAIL_TEXT , attachmentFilePath);
            }
         }

         return ResultStatus.Success;
      }

      protected virtual ResultStatus RunAfter(PokeBall args) {
         MessageDisplay.Normal("轉檔完畢");

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Import() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus Import(Control control) {
         GridHelper.AcceptText(control);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Export() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus Export(Control control) {
         GridHelper.AcceptText(control);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus Export(ReportHelper reportHelper) {
         reportHelper.Export(reportHelper.FileType , reportHelper.FilePath);

         return ResultStatus.Success;
      }

      protected virtual ResultStatus ExportAfter(string startTime) {
         string finishTime = DateTime.Now.ToString("HH:mm:ss");

         MessageDisplay.Info("轉檔完成!" , "處理結果" + " " + startTime + " ~ " + finishTime);

         return ResultStatus.Success;
      }

      public virtual ReportHelper PrintOrExportSetting() {
         ReportHelper reportHelper = new ReportHelper(PrintableComponent , _ReportID , _ReportTitle);
         reportHelper.FilePath = _DefaultFileNamePath;
         reportHelper.FileType = FileType.PDF;

         return reportHelper;
      }

      protected virtual ResultStatus Print(ReportHelper reportHelper) {
         reportHelper.Print();

         return ResultStatus.Success;
      }

      protected virtual ResultStatus COMPLETE() {
         MessageDisplay.Info(MessageDisplay.MSG_OK);

         Retrieve();
         //this.Close();

         return ResultStatus.Success;
      }

      protected virtual ResultStatus InsertRow() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus InsertRow(object gridObject) {
         if (gridObject != null) {
            if (gridObject is GridView) {
               GridView gv = (GridView)gridObject;

               gv.CloseEditor();
               gv.AddNewRow();
               //gv.UpdateCurrentRow();
            }
         }

         return ResultStatus.Success;
      }

      protected virtual ResultStatus DeleteRow() {
         return ResultStatus.Success;
      }

      protected virtual ResultStatus DeleteRow(object gridObject) {
         if (gridObject != null) {
            if (gridObject is GridView) {
               GridView gv = (GridView)gridObject;

               if (!ConfirmToDelete(gv.FocusedRowHandle + 1)) { return ResultStatus.Fail; }

               gv.DeleteSelectedRows();
            }
         }

         return ResultStatus.Success;
      }

      protected virtual ResultStatus BeforeClose() {
         SingletonLogger.Instance.Info(GlobalInfo.USER_ID , _ProgramID , "ChildFormClosed" , " ");

         if (DataSource == null) {
            return ResultStatus.Success;
         }

         DialogResult myDialogResult = ConfirmToExitWithoutSave(DataSource);

         if (myDialogResult == DialogResult.Yes) { return ProcessSaveFlow(); } else if (myDialogResult == DialogResult.No) { return ResultStatus.Success; } else if (myDialogResult == DialogResult.Cancel) { return ResultStatus.Fail; }

         return ResultStatus.Success;
      }

      protected bool ConfirmToDelete(int rowNum) {
         if (MessageDisplay.Choose("確定刪除第" + rowNum + "筆資料?") == DialogResult.Yes) {
            return true;
         } else {
            return false;
         }
      }

      protected DialogResult ConfirmToExitWithoutSave(DataTable dt) {
         DialogResult myDialogResult = DialogResult.No;

         if (dt.GetChanges() != null && dt.GetChanges().Rows.Count != 0) {
            foreach (DataRow row in dt.GetChanges().Rows) {
               if (row.RowState == DataRowState.Deleted) {
                  myDialogResult = MessageDisplay.Choose("資料有刪除，是否要存檔?");
                  return myDialogResult;
               }

               foreach (object item in row.ItemArray) {
                  if ((item is string && string.IsNullOrEmpty(item.AsString())) || item is DBNull) {
                     // 代表都沒有修改資料
                  } else {
                     myDialogResult = MessageDisplay.Choose("資料有異動，是否要存檔?");
                     return myDialogResult;
                  }
               }
            }
         }

         return myDialogResult;
      }

      protected bool IsDataModify(Object grid) {
         DataTable dt = null;
         if (grid is GridControl) {
            GridControl gridControl = ((GridControl)grid);
            gridControl.MainView.CloseEditor();
            gridControl.MainView.UpdateCurrentRow();
            dt = (DataTable)gridControl.DataSource;
         } else if (grid is VGridControl) {
            VGridControl gridControl = ((VGridControl)grid);
            gridControl.CloseEditor();
            gridControl.UpdateFocusedRecord();
            dt = (DataTable)gridControl.DataSource;
         }

         if (dt == null || dt.GetChanges() == null || dt.GetChanges().Rows.Count == 0) {
            MessageDisplay.Error(MessageDisplay.MSG_NO_DATA_FOR_MODIFY);
            return false;
         }

         return true;
      }

      /// <summary>
      /// 將新增、刪除、變更的紀錄分別都列印或匯出出來
      /// </summary>
      protected void PrintOrExportChanged(GridControl gridControl , ResultData resultData) {
         GridControl gridControlPrint = GridHelper.CloneGrid(gridControl);

         string originReportTitle = _ReportTitle;

         ReportHelper reportHelper;

         if (resultData.ChangedDataViewForAdded.Count != 0) {
            gridControlPrint.DataSource = resultData.ChangedDataViewForAdded;
            PrintableComponent = gridControlPrint;
            _ReportTitle += "─" + "新增";
            reportHelper = PrintOrExportSetting();
            reportHelper.ReportTitle = _ReportTitle;

            Print(reportHelper);
            Export(reportHelper);
            _ReportTitle = originReportTitle;
         }

         if (resultData.ChangedDataViewForDeleted.Count != 0) {
            gridControlPrint.DataSource = resultData.ChangedDataViewForDeleted;
            PrintableComponent = gridControlPrint;
            _ReportTitle += "─" + "刪除";
            reportHelper = PrintOrExportSetting();
            reportHelper.ReportTitle = _ReportTitle;

            Print(reportHelper);
            Export(reportHelper);
            _ReportTitle = originReportTitle;
         }

         if (resultData.ChangedDataViewForModified.Count != 0) {
            gridControlPrint.DataSource = resultData.ChangedDataViewForModified;
            PrintableComponent = gridControlPrint;
            _ReportTitle += "─" + "變更";
            reportHelper = PrintOrExportSetting();
            reportHelper.ReportTitle = _ReportTitle;

            Print(reportHelper);
            Export(reportHelper);
            _ReportTitle = originReportTitle;
         }
      }

      protected string CopyExcelTemplateFile(string fileName , FileType fileType) {
         string originalFilePath = Path.Combine(GlobalInfo.DEFAULT_EXCEL_TEMPLATE_DIRECTORY_PATH , fileName + "." + fileType.ToString().ToLower());

         string destinationFilePath = Path.Combine(GlobalInfo.DEFAULT_REPORT_DIRECTORY_PATH ,
             fileName + "_" + DateTime.Now.ToString("yyyy.MM.dd") + "-" + DateTime.Now.ToString("HH.mm.ss") + "." + fileType.ToString().ToLower());

         File.Copy(originalFilePath , destinationFilePath , true);

         return destinationFilePath;
      }

      private void SetAllToolBtnDisable() {
         _ToolBtnInsert.Enabled = false;
         _ToolBtnDel.Enabled = false;
         _ToolBtnSave.Enabled = false;
         _ToolBtnRetrieve.Enabled = false;
         _ToolBtnRun.Enabled = false;
         _ToolBtnImport.Enabled = false;
         _ToolBtnExport.Enabled = false;
         _ToolBtnPrintAll.Enabled = false;
      }

      private void FormParent_KeyDown(object sender , KeyEventArgs e) {
         if (e.KeyCode == Keys.Escape) {
            this.Close();
         }
      }

      public void PaintFormBorder() {
         SkinElement element = SkinManager.GetSkinElement(SkinProductId.Ribbon , DevExpress.LookAndFeel.UserLookAndFeel.Default , "FormCaptionNoRibbon");
         Image image = element.Image.GetImages().Images[1];
         int counter = element.Image.ImageCount;
         Bitmap bmp = new Bitmap(image.Width , image.Height * 2);

         using (Graphics graphics = Graphics.FromImage(bmp)) {
            int y = 0;
            while (counter-- > 0) {
               graphics.DrawImage(image , new Rectangle(0 , y , image.Width , image.Height));
               graphics.DrawRectangle(Pens.DarkSlateGray , 0 , y , image.Width - 1 , image.Height);
               y += image.Height;
            }
         }
         element.SetActualImage(bmp , true);
         LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
      }

      private void FormParent_SizeChanged(object sender , EventArgs e) {
         if (MdiParent != null) {
            if (this.WindowState == FormWindowState.Maximized) {
               ((FormMain)MdiParent).standaloneBarDockControlMdi.Visible = true;
            } else {
               ((FormMain)MdiParent).standaloneBarDockControlMdi.Visible = false;
            }
         }
      }

      /// <summary>
      /// 發生錯誤時,寫log to db (logType=Error) 順便截圖和顯示錯誤訊息
      /// </summary>
      /// <param name="ex">最後儲存的長度為100字元</param>
      /// <param name="extraMsg">額外的資訊,顯示在最前面</param>
      /// <param name="showMsg"></param>
      public void WriteLog(Exception ex , string extraMsg = "" , bool showMsg = true) {
         //1.截圖
         SaveErrorScreenshot();

         //2.write log to db
         string logType = "Error";
         WriteLog(extraMsg + Environment.NewLine + ex.Message , logType);

         //3.show message to UI (ken,目前只有error才顯示錯誤訊息)
         //if (logType == "Error") {
         if (FlagAdmin) {
            MessageDisplay.Error(extraMsg + Environment.NewLine + ex.ToString());
         } else {
            if (showMsg) {
               MethodBase site = ex.TargetSite;
               string msg = (site == null ? "發生錯誤" : "發生錯誤,Error Function=" + site.Name);
               MessageDisplay.Error(msg);
            }
         }
         //}
      }

      /// <summary>
      /// 寫log to db
      /// </summary>
      /// <param name="msg">最後儲存的長度為100字元</param>
      /// <param name="logType">基本logType可定義為 Info/Operation/Error</param>
      /// <param name="operationType">logType=Info,此參數才有效(I = change data, E = export, R = query, P = print, X = execute)</param>
      public void WriteLog(string msg , string logType = "Info" , string operationType = "") {

         try {
            //ken,先把WriteLog集中,之後可根據不同的logType,存放不同的TABLE或檔案
            //基本logType可定義為 info/operation/error
            //logf_job_type value: I = change data, E = export, R = query, P = print, X = execute
            switch (logType) {
               case ("Error"):
                  operationType = "Z";
                  break;
               case ("Info"):
                  operationType = "A";
                  break;
            }
            new LOGF().Insert(GlobalInfo.USER_ID , _ProgramID , msg.SubStr(0 , 100) , operationType);//取前100字元

         } catch (Exception ex2) {
            //write log to db failed , write file to local
            string filename = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            string filepath = Path.Combine(Application.StartupPath , "Log" , DateTime.Today.ToString("yyyyMM"));
            Directory.CreateDirectory(filepath);
            filepath = Path.Combine(filepath , filename);
            using (StreamWriter sw = File.AppendText(filepath)) {
               sw.WriteLine("");
               sw.WriteLine("");
               sw.WriteLine("==============================");
               sw.WriteLine("datetime=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
               sw.WriteLine("userId=" + GlobalInfo.USER_ID);
               sw.WriteLine("txnId=" + _ProgramID);
               sw.WriteLine("logType=" + logType);
               sw.WriteLine("operationType=" + operationType);
               sw.WriteLine("ex2.msg=" + ex2.Message);
               sw.Write("msg=" + msg);
            }
         }
      }


      /// <summary>
      /// 抓取畫面存檔成png
      /// </summary>
      public void SaveErrorScreenshot() {
         try {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + _ProgramID + ".png";
            string filepath = Path.Combine(Application.StartupPath , "Log" , DateTime.Today.ToString("yyyyMM"));
            Directory.CreateDirectory(filepath);
            filepath = Path.Combine(filepath , filename);

            //ken,只抓此應用程式的顯示範圍,但沒有強制focus此應用程式,所以如果user自己把畫面切走,會抓錯

            //ken,想單純抓該應用程式大小的畫面,實際很難抓,所以抓整個畫面
            ImageFormat format = ImageFormat.Png;
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width , bounds.Height)) {
               using (Graphics g = Graphics.FromImage(bitmap)) {
                  g.CopyFromScreen(new Point(0 , 0) , Point.Empty , bounds.Size);
               }

               bitmap.Save(filepath , format);
            }
         } catch {
            //if error,no response
         }
      }


   }
}