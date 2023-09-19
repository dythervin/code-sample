using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class StunEffectComponent : EntityComponentExt<StunEffectComponentData>, IStunEffectComponent
    {
        public bool IsStunned => false;

        public StunEffectComponent(StunEffectComponentData componentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentData, context, constructorContext)
        {
        }
    }
}