using System;
using Game.Data;

namespace Game
{
    public interface IFactionComponent : IEntityComponentExt
    {
        public event Action OnChanged;

        IFactionContainer FactionContainer { get; }

        Faction Faction { get; set; }
    }
}