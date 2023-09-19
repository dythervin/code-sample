using System;
using System.Collections.Generic;
using Game.Data;

namespace Game
{
    public readonly struct LogData
    {
        public readonly LogOptions value;

        public readonly string stackTrace;

        public readonly DateTime dateTime;

        public LogData(LogOptions value, string stackTrace, DateTime dateTime)
        {
            this.value = value;
            this.stackTrace = stackTrace;
            this.dateTime = dateTime;
        }
    }

    public interface ILogComponent : IEntityComponentExt
    {
        event Action<LogData> OnLog;

        event Action OnClear;

        IReadOnlyList<LogData> Logs { get; }
#if DDebug || DEVELOPMENT_BUILD
        void Log(string msg);

        void LogWarning(string msg);

        void LogError(string msg);

        void LogError(Exception exception, bool @throw = true);

        void Log(in LogOptions options);
#endif
        void Clear();
    }
}