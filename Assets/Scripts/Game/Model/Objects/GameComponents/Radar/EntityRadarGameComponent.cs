using System.Collections.Generic;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game.GameComponents.Radar
{
    public class EntityRadarGameComponent : RadarGameComponent<EntityRadarGameComponentData, IEntityExt>,
        IEntityRadarGameComponent
    {
        protected override IReadOnlyList<IEntityExt> Targets => Owner.ActiveEntities;

        public EntityRadarGameComponent(EntityRadarGameComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }
}


