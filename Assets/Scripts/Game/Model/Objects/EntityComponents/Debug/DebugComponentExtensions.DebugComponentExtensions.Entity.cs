using System;
using System.Diagnostics;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework;
using Game.Data;
using UnityEngine;

namespace Game
{
    public static partial class DebugComponentExtensions
    {
        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this IEntity entity, LogOptions logOptions)
        {
            entity.Components.Get<ILogComponent>().Log(logOptions);
        }
        
        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this IEntity entity, string msg)
        {
            entity.Components.Get<ILogComponent>().Log(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogWarning(this IEntity entity, string msg)
        {
            entity.Components.Get<ILogComponent>().LogWarning(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogError(this IEntity entity, string msg)
        {
            entity.Components.Get<ILogComponent>().LogError(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogError(this IEntity entity, Exception exception, bool @throw = true)
        {
            entity.Components.Get<ILogComponent>().LogError(exception, @throw);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this IEntity entity, string msg, Color color)
        {
            entity.Components.Get<ILogComponent>().Log(msg, color);
        }
    }
}