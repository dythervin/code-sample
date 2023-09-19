using System;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.Core;
using Dythervin.Game.Framework;
using Dythervin.UnsafeUtils.Unsafe;
using Game.Data;
using Game.Radar;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace Game
{
    public class RadarComponent<T> : EntityComponentExt<EntityRadarData>, IRadarComponent<T>
        where T : class, IIdentified, IReadOnlyPositioned
    {
        public event Action OnRefresh;

        [Inject(Id = InjectId.TransformVision)]
        private Transform eyes;

        [Inject(Id = InjectId.TransformRoot)] private Transform transform;

        private PtrPersistent<NativeList<ObjData>> _inRadiusList;

        private float _angle;

        private float _radius;

        public float Angle => _angle;

        public float Radius => _radius;

        private uint _id;

        public LayerMask RaycastMask => Data.LayerMask;

        public Vector3 Forward => eyes.forward;

        public Vector3 Position => transform.position;

        public Vector3 RaycastSource => eyes.position;

        public bool HasTransform => ((object)transform) != null;

        PtrPersistent<NativeList<ObjData>> IRadar<T>.InRadius
        {
            get => _inRadiusList;
            set => _inRadiusList = value;
        }

        uint IIdentified.Id => _id;

        void IIdentified.SetId(uint value) => _id = value;

        public RadarComponent(EntityRadarData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
            SetAngle(data.Angle);
            SetRadius(data.Radius);
        }

        public RadarEnumerator<T> GetEnumerator()
        {
            return new RadarEnumerator<T>(_inRadiusList.Value);
        }

        public void SetAngle(float value)
        {
            DDebug.Assert(value is >= 0 and <= 360);
            _angle = value;
        }

        public void SetRadius(float value)
        {
            _radius = value;
        }

        protected override void Destroyed()
        {
            _inRadiusList = default;
            base.Destroyed();
        }

        protected virtual void RefreshCallback()
        {
        }

        void IRadar<T>.RefreshCallback()
        {
            RefreshCallback();
            OnRefresh?.Invoke();
        }
    }
}