using System;
using Dythervin.AI.GOAP;

namespace Game
{
    public interface ISuspiciousLevelComponent : IEntityComponentExt
    {
        event Action<SuspiciousLevel> OnChanged;

        SuspiciousLevel SuspiciousLevel { get; }

        void UpdateLevel(SuspiciousLevel value);
    }
}