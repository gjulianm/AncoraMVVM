using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AncoraMVVM.Base.Diagnostics
{
    public class AncoraLogger
    {
        private static AncoraLogger instance;
        public static AncoraLogger Instance
        {
            get
            {
                instance = instance ?? new AncoraLogger();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public AncoraLogger()
        {
            DefaultEntryDetail = LogStringDetailLevel.MessageAndFunction;
            WriteToStandardDebug = true;
        }

        public LogStringDetailLevel DefaultEntryDetail { get; set; }
        private List<LogEntry> entries = new List<LogEntry>();
        public IEnumerable<LogEntry> LogEntries { get { return entries; } }
        public bool WriteToStandardDebug { get; set; }

        public event EventHandler<LoggedExceptionEventArgs> LoggedException;
        public event EventHandler<LoggedEventEventArgs> LoggedEvent;

        protected void OnLoggedEvent(LogEntry entry)
        {
            var temp = LoggedEvent;
            if (temp != null)
                temp(this, new LoggedEventEventArgs(entry));
        }

        protected void OnLoggedException(LogEntry entry)
        {
            var temp = LoggedException;
            if (temp != null)
                temp(this, new LoggedExceptionEventArgs(entry));
        }

        private string FormatSourceCodeLocation(string path, int line)
        {
            var lastSlash = path.LastIndexOf("\\") + 1;
            path = path.Substring(lastSlash, path.Length - lastSlash);
            return path + ":" + line.ToString();
        }


        public void LogEvent(string message, LogLevel level = LogLevel.Message,
                               [CallerMemberName] string memberName = "",
                               [CallerFilePath] string sourceFilePath = "",
                               [CallerLineNumber] int sourceLineNumber = 0)
        {
            var entry = new LogEntry(message, level, FormatSourceCodeLocation(sourceFilePath, sourceLineNumber), memberName);
            entries.Add(entry);

            if (WriteToStandardDebug)
                Debug.WriteLine(entry.ToString(DefaultEntryDetail));

            OnLoggedEvent(entry);
        }

        public void LogException(string message, Exception exception, LogLevel level = LogLevel.Error,
                               [CallerMemberName] string memberName = "",
                               [CallerFilePath] string sourceFilePath = "",
                               [CallerLineNumber] int sourceLineNumber = 0)
        {
            var entry = new LogEntry(message, level, FormatSourceCodeLocation(sourceFilePath, sourceLineNumber), memberName, exception);
            entries.Add(entry);

            if (WriteToStandardDebug)
                Debug.WriteLine(entry.ToString(DefaultEntryDetail));

            OnLoggedException(entry);
        }

        public IEnumerable<string> GetLogEntriesAsString(LogStringDetailLevel detailLevel = LogStringDetailLevel.MessageAndFunction)
        {
            return entries.Select(x => x.ToString(detailLevel));
        }
    }

    public class LoggedExceptionEventArgs : LoggedEventEventArgs
    {
        public Exception Exception { get; set; }

        public LoggedExceptionEventArgs(LogEntry entry)
            : base(entry)
        {
            Exception = entry.Exception;
        }

        public LoggedExceptionEventArgs(Exception exception)
            : base(null)
        {
            Exception = exception;
        }
    }

    public class LoggedEventEventArgs : EventArgs
    {
        public LogEntry LogEntry { get; set; }

        public LoggedEventEventArgs(LogEntry entry)
        {
            LogEntry = entry;
        }
    }
}
