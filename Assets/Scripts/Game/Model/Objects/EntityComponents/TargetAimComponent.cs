using System;
using Dythervin.Core.Extensions;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework;
using Dythervin.UpdateSystem;
using Game.Data;
using UnityEngine;

namespace Game
{
    public class TargetAimComponent : EntityComponentExt<TargetAimComponentData>, ITargetAimComponent, IUpdatableDelta
    {
        private IEntityExt _target;

        private float _range;

        private float _angle;

        private bool _isAimed;

        public TargetAimComponent(TargetAimComponentData componentDataInventoryComponentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentDataInventoryComponentData,
            context,
            constructorContext)
        {
        }

        public event Action OnLost;

        public event Action<AimData> OnAimed;

        public bool IsTargetInAim
        {
            get
            {
                GetData(out Vector3 _, out Vector3 _, out Vector3 dir, out float dist);
                return !(dist > _range) && IsInAngle(dir, dist, Owner.Transform.forward);
            }
        }

        public void AimTarget(IEntityExt value, float range)
        {
            AimTarget(value, range, Data.DefaultAngle);
        }

        public void AimTarget(IEntityExt value, float range, float angle)
        {
            _isAimed = false;
            DAssert.IsNotNull(value);
            _angle = angle;
            _range = range;
            _target = value;
            this.SetUpdater(true);
        }

        public void Stop()
        {
            this.SetUpdater(false);
        }

        private bool IsInAngle(in Vector3 dir, float dist, in Vector3 forward)
        {
            return Vector3Ext.IsInAngle(dir / dist, forward, _angle);
        }

        private void GetData(out Vector3 targetPosition, out Vector3 selfPosition, out Vector3 dir, out float dist)
        {
            targetPosition = _target.Position;
            selfPosition = Owner.Position;
            dir = targetPosition - selfPosition;
            dist = dir.magnitude;
        }

        void IUpdatableDelta.OnUpdate(float deltaTime)
        {
            GetData(out Vector3 targetPosition, out Vector3 selfPosition, out Vector3 dir, out float dist);
            if (dist > _range)
            {
                OnLost?.Invoke();
                return;
            }

            Quaternion rotation = Quaternion.RotateTowards(Owner.Transform.rotation,
                Quaternion.LookRotation(dir),
                Data.AngularSpeed * deltaTime);

            Owner.Transform.rotation = rotation;

            if (!_isAimed && IsInAngle(dir, dist, rotation * Vector3.forward))
            {
                _isAimed = true;
                OnAimed?.Invoke(new AimData(_target, targetPosition, selfPosition, dist));
            }
        }
    }
}