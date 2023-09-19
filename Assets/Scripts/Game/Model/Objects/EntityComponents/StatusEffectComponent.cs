using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class StatusEffectComponent : EntityComponentExt<StatusEffectComponentData>, IStatusEffectComponent
    {
        private IHealthComponent _health;
        private IStunEffectComponent _stunEffectComponent;

        private bool IsStunned => _stunEffectComponent?.IsStunned == true;

        public bool CanAct => _health.IsAlive && !IsStunned;

        public bool CanMove => CanAct;

        public StatusEffectComponent(StatusEffectComponentData componentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentData, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _health);
            Owner.TryGetComponent(out _stunEffectComponent);
        }
    }
}