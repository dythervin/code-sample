using Dythervin.AI.GOAP;
using Dythervin.Game.Framework;
using Game.AI.GOAP;
using Game.Data;

namespace Game.AI
{
    public class GAgentComponentExt : GAgentComponent<IGAgentData>, IGAgent, IGAgentComponent
    {
        private ISuspiciousLevelComponent _suspiciousLevelComponent;

        private IHealthComponent _health;

        private IStatusEffectComponent _statusEffectComponent;

        protected override SuspiciousLevel SuspiciousLevel => _suspiciousLevelComponent.SuspiciousLevel;

        protected override bool CanAct => _statusEffectComponent.CanAct && base.CanAct;

        public GAgentComponentExt(IGAgentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _suspiciousLevelComponent);
            Owner.GetComponent(out _health);
            Owner.GetComponent(out _statusEffectComponent);
            _suspiciousLevelComponent.OnChanged += SuspiciousLevelComponentOnOnChanged;
            _health.OnDeath += HealthOnOnDeath;
        }

        private void HealthOnOnDeath()
        {
            CancelPlan();
        }

        protected override void Destroyed()
        {
            _health.OnDeath -= HealthOnOnDeath;
            _suspiciousLevelComponent.OnChanged -= SuspiciousLevelComponentOnOnChanged;
            base.Destroyed();
        }

        private void SuspiciousLevelComponentOnOnChanged(SuspiciousLevel obj)
        {
            TryRequestPlanAndAct();
        }
    }
}