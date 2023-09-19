using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public static class FactionComponentExtensions
    {
        public static Faction GetFaction(this IEntity entityExt)
        {
            return entityExt.TryGetComponent(out IFactionComponent factionComponent) ?
                factionComponent.Faction :
                Faction.Neutral;
        }
    }
}