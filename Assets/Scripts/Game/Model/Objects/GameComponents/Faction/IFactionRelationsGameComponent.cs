using Game.Data;

namespace Game.GameComponents.Faction
{
    public interface IFactionRelationsGameComponent : IGameComponentExt
    {
        bool IsAlly(Data.Faction a, Data.Faction b);

        bool IsHostile(Data.Faction a, Data.Faction b);

        IFactionMatrix FactionMatrix { get; }
    }
}