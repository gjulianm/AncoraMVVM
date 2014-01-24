
using System;
namespace AncoraMVVM.Base.Diagnostics
{
    public enum LogStringDetailLevel { Message, MessageAndLocation, MessageAndFunction, Complete, CompleteWithExceptionBacktrace }

    public class LogEntry
    {
        public string Message { get; set; }
        public LogLevel Level { get; set; }
        public string SourceCodeLocation { get; set; }
        public string OriginatingFunction { get; set; }
        public Exception Exception { get; set; }
        public DateTime Timestamp { get; set; }

        public LogEntry(string message, LogLevel level, string sourceLocation, string origFunction)
            : this(message, level, sourceLocation, origFunction, null)
        {
        }

        public LogEntry(string message, LogLevel level, string sourceLocation, string origFunction, Exception exception)
        {
            Message = message;
            Level = level;
            SourceCodeLocation = sourceLocation;
            OriginatingFunction = origFunction;
            Exception = exception;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return ToString(LogStringDetailLevel.MessageAndFunction);
        }

        public string ToString(LogStringDetailLevel logStringDetailLevel)
        {
            string exType = "";
            string exStackTrace = "";

            if (Exception != null)
            {
                exType = "Ex. type: " + Exception.GetType().Name + ".";
                exStackTrace = "Stacktrace: " + Exception.StackTrace + ".";
            }

            switch (logStringDetailLevel)
            {
                case LogStringDetailLevel.Complete:
                    return String.Format("{0} [{1}]: {2}. Location: {3} at {4}. {5}", Timestamp, Level, Message, OriginatingFunction, SourceCodeLocation, exType);
                case LogStringDetailLevel.CompleteWithExceptionBacktrace:
                    return String.Format("{0} [{1}]: {2}. Location: {3} at {4}. {5} {6}", Timestamp, Level, Message, OriginatingFunction, SourceCodeLocation, exType, exStackTrace);
                case LogStringDetailLevel.Message:
                    return String.Format("{0} [{1}]: {2}.", Timestamp, Level, Message);
                case LogStringDetailLevel.MessageAndFunction:
                    return String.Format("{0} [{1}]: {2}. Method: {3}. {4}", Timestamp, Level, Message, OriginatingFunction, exType);
                case LogStringDetailLevel.MessageAndLocation:
                    return String.Format("{0} [{1}]: {2}. Location: {3}. {4}", Timestamp, Level, Message, SourceCodeLocation, exType);
                default:
                    return ToString();
            }
        }
    }
}
