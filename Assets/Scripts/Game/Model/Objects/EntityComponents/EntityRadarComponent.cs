using Dythervin.Game.Framework;
using Game.Data;
using Game.GameComponents.Radar;

namespace Game
{
    public class EntityRadarComponent : RadarComponent<IEntityExt>, IEntityRadarComponent
    {
        private IEntityRadarGameComponent _radarGameComponent;

        public EntityRadarComponent(EntityRadarData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext) { }

        protected override void Init()
        {
            base.Init();
            Context.Game.GetComponent(out _radarGameComponent);
            _radarGameComponent.Add(this);
        }

        protected override void Destroyed()
        {
            _radarGameComponent.Remove(this);
            base.Destroyed();
        }
    }
}