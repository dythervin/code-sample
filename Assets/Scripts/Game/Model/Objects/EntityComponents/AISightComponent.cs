using System;
using Dythervin.AI.GOAP;
using Dythervin.Game.Framework;
using Dythervin.UpdateSystem;
using Game.AI.GOAP;
using Game.Data;
using Game.GameComponents.Faction;
using Game.Radar;
using UnityEngine;

namespace Game
{
    public class AISightComponent : EntityComponentExt<SightControllerData>, IAISightComponent, IUpdatableInterval
    {
        private float _checkInterval;
        private IEntityRadarComponent _entityRadarComponent;
        private ISuspiciousLevelComponent _suspiciousLevelComponent;
        private IFactionRelationsGameComponent _factionRelationsGameComponent;
        private IFactionComponent _factionComponent;
        private IGAgent _agent;
        private IEntityExt _last;

        public float Interval
        {
            get => _checkInterval;
            set
            {
                Debug.Assert(value > 0);
                _checkInterval = value;
            }
        }

        float IUpdatableInterval.UntilUpdate { get; set; }

        public AISightComponent(SightControllerData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
            _checkInterval = data.CheckInterval;
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _entityRadarComponent);
            Owner.GetComponent(out _suspiciousLevelComponent);
            Owner.GetComponent(out _agent);
            Owner.GetComponent(out _factionComponent);
            Owner.Context.Game.GetComponent(out _factionRelationsGameComponent);
            _agent.OnPlanning += AgentOn_Planning;
            this.SetUpdatableInterval(true);
        }

        protected override void Destroyed()
        {
            this.SetUpdatableInterval(false);
            base.Destroyed();
        }

        public void Check()
        {
            Vector3 position = _entityRadarComponent.RaycastSource;
            IEntityExt owner = Owner;
            int enemyInRadius = 0;
            Faction faction = _factionComponent.Faction;

            foreach (RadarEnumerator<IEntityExt>.Data data in _entityRadarComponent)
            {
                IEntityExt target = data.target;
                if (target == owner)
                    continue;


                if (_factionRelationsGameComponent.IsHostile(target.GetFaction(), faction) && IsValid(target))
                {
                    enemyInRadius++;

                    if (!Physics.Linecast(position, target.Center, out RaycastHit hit, _entityRadarComponent.RaycastMask) ||
                        !target.HasCollider(hit.colliderInstanceID))
                        continue;

                    _last = target;
                    _suspiciousLevelComponent.UpdateLevel(SuspiciousLevel.Alert);
                    return;
                }
            }

            switch (_suspiciousLevelComponent.SuspiciousLevel)
            {
                case SuspiciousLevel.Alert:
                    _suspiciousLevelComponent.UpdateLevel(SuspiciousLevel.Suspicious);
                    break;

                case SuspiciousLevel.Suspicious:
                    if (enemyInRadius == 0)
                        _suspiciousLevelComponent.UpdateLevel(SuspiciousLevel.Calm);
                    break;

                case SuspiciousLevel.Calm:
                    if (enemyInRadius > 0)
                        _suspiciousLevelComponent.UpdateLevel(SuspiciousLevel.Suspicious);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsValid(IEntityExt target)
        {
            return !target.TryGetComponent(out IHealthComponent health) || health.IsAlive;
        }

        void IUpdatableInterval.OnUpdate()
        {
            Check();
        }

        private void AgentOn_Planning(GPlanParameters gPlanParameters)
        {
            if (gPlanParameters.target != null && IsValid(gPlanParameters.target))
                return;

            gPlanParameters.target = _last;
        }
    }
}