using Dythervin.Game.Framework;
using Game.Data;
using UnityEngine;

namespace Game
{
    public interface IEntityExt : IEntity<IEntityComponentExt>
    {
        new IEntityDataExt Data { get; }

        Vector3 Center { get; }
    }
}