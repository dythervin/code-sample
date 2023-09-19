using System;
using Dythervin.Game.Framework;
using Game.AI.PathfindingExt;
using Game.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class NavMovementComponent<TData> : EntityComponentExt<TData>, INavMovementComponent
        where TData : class, IEntityComponentDataExt
    {
        public event Action OnReached;

        public event Action OnMovementStart;

        private Movement _movement;

        [CanBeNull] private IHealthComponent _healthComponent;

        public bool HasReachedDestination => _movement.reachedDestination;

        public bool IsMoving => _movement.hasPath;

        public Vector3 Velocity => GetVelocity();

        protected virtual bool CanMove => _healthComponent == null || _healthComponent.IsAlive;

        protected virtual Vector3 GetVelocity()
        {
            return _movement.velocity;
        }

        public NavMovementComponent(TData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.TryGetComponent(out _healthComponent);
            if (_healthComponent != null)
            {
                _healthComponent.OnDeath += HealthComponentOnOnDeath;
            }
        }

        protected override void Destroyed()
        {
            if (_healthComponent != null)
                _healthComponent.OnDeath -= HealthComponentOnOnDeath;

            base.Destroyed();
        }

        private void HealthComponentOnOnDeath()
        {
            _movement.canMove = false;
            _movement.Stop();
            _healthComponent.OnRevive += HealthComponentOnOnRevive;
        }

        private void HealthComponentOnOnRevive()
        {
            _healthComponent.OnRevive -= HealthComponentOnOnRevive;
            _movement.canMove = true;
        }

        [Inject]
        private void Init(Movement movement)
        {
            _movement = movement;
            _movement.OnDestinationSet += MovementOnOnDestinationSet;
            _movement.OnReached += MovementOnOnReached;
        }

        private void MovementOnOnDestinationSet(in Destination destination)
        {
            OnMovementStart?.Invoke();
        }

        private void MovementOnOnReached()
        {
            OnReached?.Invoke();
        }

        public void TurnTo(in Destination target)
        {
            Stop();
        }

        public void SetDestination(in Destination destination)
        {
            _movement.SetDestination(destination);
        }

        public bool IsInRange(in Destination destination)
        {
            return _movement.IsInRange(destination);
        }

        public void Stop()
        {
            _movement.Stop();
        }
    }
}