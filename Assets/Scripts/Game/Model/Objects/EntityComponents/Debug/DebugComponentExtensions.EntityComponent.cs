using System.Diagnostics;
using Dythervin.Core.Extensions;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework;
using UnityEngine;

namespace Game
{
    public static partial class DebugComponentExtensions
    {
        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this IEntityComponent entityComponent, string msg)
        {
            string name = entityComponent.GetType().Name.Remove("Component");
            msg = $"{name}: {msg}";
            entityComponent.Owner.Components.Get<ILogComponent>().Log(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogWarning(this IEntityComponent entityComponent, string msg)
        {
            string name = entityComponent.GetType().Name.Remove("Component");
            msg = $"{name}: {msg}";
            entityComponent.Owner.Components.Get<ILogComponent>().LogWarning(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void LogError(this IEntityComponent entityComponent, string msg)
        {
            string name = entityComponent.GetType().Name.Remove("Component");
            msg = $"{name}: {msg}";
            entityComponent.Owner.Components.Get<ILogComponent>().LogError(msg);
        }

        [Conditional(Symbols.DDebug)]
        [Conditional(Symbols.DEVELOPMENT_BUILD)]
        public static void Log(this IEntityComponent entityComponent, string msg, Color color)
        {
            string name = entityComponent.GetType().Name.Remove("Component");
            msg = $"{name}: {msg}";
            entityComponent.Owner.Components.Get<ILogComponent>().Log(msg, color);
        }
    }
}