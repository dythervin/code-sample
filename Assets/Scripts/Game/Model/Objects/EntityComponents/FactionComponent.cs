using System;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class FactionComponent : EntityComponentExt<FactionComponentData>, IFactionComponent
    {
        public event Action OnChanged;

        private readonly IFactionContainer _factionContainer;

        public IFactionContainer FactionContainer => _factionContainer;

        public Faction Faction
        {
            get => Data.Faction;
            set
            {
                if (Faction == value)
                    return;

                Data.Faction = value;
                MarkDataDirty();
                OnChanged?.Invoke();
            }
        }

        public FactionComponent(FactionComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
            Feature.GetParameter(out _factionContainer);
        }
    }
}