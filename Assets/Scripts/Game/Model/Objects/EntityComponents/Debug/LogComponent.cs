using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Dythervin.Core;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Dythervin.ObjectPool;
using Game.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    public class LogComponent : EntityComponentExt<ILogComponentData>, ILogComponent
    {
        public event Action<LogData> OnLog;

        public event Action OnClear;

        private readonly List<LogData> _logs = new List<LogData>();

        public IReadOnlyList<LogData> Logs => _logs;

        public LogComponent(ILogComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        public void Log(string msg)
        {
            Log(new LogOptions(msg, LogType.Log));
        }

        public void LogWarning(string msg)
        {
            LogInternal(new LogOptions(msg, LogType.Warning));
            DDebug.LogWarning(msg, Owner.Transform);
        }

        public void LogError(string msg)
        {
            LogInternal(new LogOptions(msg, LogType.Error));
            DDebug.LogError(msg, Owner.Transform);
        }

        public void LogError(Exception exception, bool @throw = true)
        {
            LogInternal(new LogOptions(exception.Message, LogType.Error), exception.StackTrace);
            DDebug.LogError(exception, Owner.Transform);
            if (@throw)
                throw exception;
        }

        public void Log(in LogOptions options)
        {
            LogInternal(options);
            DDebug.Log(options.text, Owner.Transform);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LogInternal(in LogOptions options, [CanBeNull] string stackTrace = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(options.text));
            stackTrace ??= AppendStackTrace(stackTrace, 10);
            LogData logData = new LogData(options, stackTrace, DateTime.Now);
            _logs.Add(logData);
            OnLog?.Invoke(logData);
        }

        public void Clear()
        {
            OnClear?.Invoke();
        }

        private static readonly StringBuilder StackTraceBuilder = new StringBuilder();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string AppendStackTrace(string stackTrace, int stackTraceDepth, int intend = 1)
        {
            StackTraceBuilder.Clear();
            if (stackTrace != null)
            {
                StackTraceBuilder.Append(stackTrace);
            }
            else
            {
                stackTrace = StackTraceUtility.ExtractStackTrace();
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    return stackTrace;
                }

                stackTrace = Environment.StackTrace;
                StackTraceBuilder.Append(stackTrace);
                StackTraceBuilder.RemoveFrom(stackTrace.IndexOf("at Microsoft", StringComparison.Ordinal));
                int from = stackTrace.IndexOf('\n');
                StackTraceBuilder.Remove(0, from + 1);
            }

            AppendStackTrace(StackTraceBuilder, stackTraceDepth, intend);
            return StackTraceBuilder.ToStringAndClear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendStackTrace(StringBuilder stackTrace, int stackTraceDepth, int intend = 1)
        {
            using var handler = SharedPools.StringBuilder.Get(out StringBuilder stringBuilder);
            if (intend <= 0 && (stackTraceDepth < 0 || stackTraceDepth == int.MaxValue))
            {
                stringBuilder.Append(stackTrace);
            }
            else
            {
                stringBuilder.EnsureCapacity(stringBuilder.Length + StackTraceBuilder.Length);
                stringBuilder.AppendIntend(intend);
                int depth = -1;
                for (int i = 0; i < StackTraceBuilder.Length; i++)
                {
                    char c = StackTraceBuilder[i];
                    stringBuilder.Append(c);
                    if (c != '\n')
                    {
                        continue;
                    }

                    stringBuilder.AppendIntend(intend);
                    depth++;
                    if (depth >= stackTraceDepth)
                    {
                        break;
                    }
                }
            }

            stringBuilder.AppendLine();
        }
    }
}