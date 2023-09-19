using Dythervin.Common;
using Dythervin.Common.ID;
using Game.Radar;
using UnityEngine;

namespace Game
{
    public interface IRadarComponent<T> : IRadar<T>
        where T : class, IIdentified, IReadOnlyPositioned
    {
        LayerMask RaycastMask { get; }

        Vector3 RaycastSource { get; }
    }
}