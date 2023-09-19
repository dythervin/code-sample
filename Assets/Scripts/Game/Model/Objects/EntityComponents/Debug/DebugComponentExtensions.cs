using System;
using System.Diagnostics;
using Dythervin.Core.Utils;
using Game.Data;
using UnityEngine;

namespace Game
{
    public static partial class DebugComponentExtensions
    {
        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this ILogComponent component, string msg, Color color)
        {
            component.Log(new LogOptions(msg, LogType.Log, color));
        }

#if !DDebug && !DEVELOPMENT_BUILD
        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this ILogComponent component, string text)
        {
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogWarning(this ILogComponent component, string msg)
        {
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogError(this ILogComponent component, string msg)
        {
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogError(this ILogComponent component, Exception msg, bool @throw = true)
        {
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this ILogComponent component, LogOptions data)
        {
        }
#endif
    }
}