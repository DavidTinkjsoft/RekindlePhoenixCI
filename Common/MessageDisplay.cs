﻿using System.Windows.Forms;
using static Common.MessageBoxTimeOut;

namespace Common
{
   public class MessageDisplay
   {
      public const string MSG_NO_PERMISSION = "時點不允許執行此交易";
      public const string MSG_NO_DATA_FOR_MODIFY = "無資料可以異動";
      public const string MSG_NO_DATA = "無資料";
      public const string MSG_OK = "處理完成";
      public const string MSG_PRINT = "列印完成";
      public const string MSG_IMPORT = "轉檔完成";

      public static DialogResult Normal(string content, string caption = "訊息")
      {
#if DEBUG
         TimerThread tt = new TimerThread(2000);
         try {
            return MessageBox.Show(content, caption);
         }
         finally {
            tt.Terminate();
         }
#else
            return MessageBox.Show(content, caption);
#endif
      }

      public static DialogResult Error(string content, string caption = "注意")
      {
#if DEBUG
         TimerThread tt = new TimerThread(2000);
         try {
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         finally {
            tt.Terminate();
         }
#else
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
      }

      public static DialogResult Warning(string content, string caption = "注意")
      {

#if DEBUG
         TimerThread tt = new TimerThread(2000);
         try {
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
         }
         finally {
            tt.Terminate();
         }
#else
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
      }

      public static DialogResult Info(string content, string caption = "注意")
      {
#if DEBUG
         TimerThread tt = new TimerThread(2000);
         try {
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
         finally {
            tt.Terminate();
         }
#else
            return MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
      }

      public static DialogResult Choose(string content, string caption = "注意")
      {
#if DEBUG
         TimerThread tt = new TimerThread(2000);
         try {
            return MessageBox.Show(content, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
         }
         finally {
            tt.Terminate();
         }
#else
            return MessageBox.Show(content, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
#endif
      }
   }
}