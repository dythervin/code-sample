using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Game.AI.GOAP;
using Game.AI.PathfindingExt;
using Unity.Mathematics;
using UnityEngine;

namespace Game.AI
{
    public class GoToAgentAction : GAgentAction<GoToActionData>
    {
        private INavMovementComponent _movement;

        public override bool IsValid(GPlanParameters parameters)
        {
            return parameters.destinations.Count > 0;
        }

        protected override void Init()
        {
            base.Init();
            Owner.Owner.GetComponent(out _movement);
            _movement.OnReached += OnReached;
        }

        private void OnReached()
        {
            if (IsActive)
                Complete(_movement.HasReachedDestination);
        }

        protected override void OnEnter()
        {
            base.OnEnter();

            Destination destination = Parameters.destinations.PopLast();
            if (_movement.IsInRange(destination))
                Complete(true);
            else
                _movement.SetDestination(destination);
        }

        protected override void OnExit()
        {
            base.OnExit();
            _movement.Stop();
        }

        protected override float CalculateDynamicCost(GPlanParameters parameters)
        {
            var destinations = parameters.destinations;
            float baseCost = base.CalculateDynamicCost(parameters);

            Destination destination = destinations.GetLast();
            Vector3 from = destinations.Count > 1 ? destinations[destinations.Count - 2].Point : Owner.Owner.Position;
            float distSqr = (from - destination.Point).sqrMagnitude;
            if (distSqr <= destination.RadiusSqr)
                return baseCost;

            return baseCost + math.sqrt(distSqr) - destination.Radius;
        }

        public GoToAgentAction(GoToActionData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext) 
        {
        }
    }
}