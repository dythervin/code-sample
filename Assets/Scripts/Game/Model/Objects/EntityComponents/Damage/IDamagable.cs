using System;
using Dythervin.Game.Framework;

namespace Game
{
    public interface IDamagable : IObject
    {
        void Damage(Common.Damage damage);

        event Action<Common.Damage> OnDamaged;
    }
}