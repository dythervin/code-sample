using Dythervin.Common;
using Dythervin.Common.ID;
using Game.Radar;

namespace Game.GameComponents.Radar
{
    public interface IRadarGameComponent<T>
        where T : class, IIdentified, IReadOnlyPositioned
    {
        void Add(IRadar<T> radar);

        void Remove(IRadar<T> radar);
    }
}