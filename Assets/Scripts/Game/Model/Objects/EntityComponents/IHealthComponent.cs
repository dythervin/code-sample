using System;

namespace Game
{
    public interface IHealthComponent : IEntityComponentExt
    {
        public const float DefaultHealth = 100;

        event Action OnChanged;

        event Action OnDeath;

        bool IsAlive { get; }

        float Max { get; }

        float Value { get; set; }

        event Action OnMaxChanged;

        event Action OnRevive;
    }
}