using System;
using Dythervin.Collections;
using Game.Data;

namespace Game.Common
{
    public interface IResistanceProvider : IIndexerGetter<DamageType, int>
    {
        event Action OnResistanceChanged;
    }
}