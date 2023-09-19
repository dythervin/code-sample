using System;

namespace Game
{
    public interface ITargetAimComponent : IEntityComponentExt
    {
        event Action OnLost;

        event Action<AimData> OnAimed;

        bool IsTargetInAim { get; }

        void AimTarget(IEntityExt value, float range);
        void AimTarget(IEntityExt value, float range, float angle);

        void Stop();
    }
}