using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Game.Framework;
using Game.Data;
using UnityEngine;

namespace Game
{
    public interface ICameraEntity : IEntity
    {
        Vector3 Forward { get; }
    }

    public class CameraEntity : EntityExt<CameraEntityData>
    {
        public CameraEntity(CameraEntityData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }

    public class CameraEntityData : EntityDataExt
    {
        public CameraEntityData(Tag tags, IReadOnlyList<IEntityComponentDataExt> components) : base(tags, components)
        {
        }

        public CameraEntityData() : base()
        {
        }
    }
}