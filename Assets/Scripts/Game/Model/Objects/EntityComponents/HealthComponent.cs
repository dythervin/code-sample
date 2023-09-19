using System;
using Dythervin.Common.Data;
using Dythervin.Data.Abstractions;
using Dythervin.Game.Framework;
using Game.Data;
using Unity.Mathematics;

namespace Game
{
    public class HealthComponent : EntityComponentExt<HealthComponentData>, IHealthComponent
    {
        public event Action OnChanged;

        public event Action OnMaxChanged;

        public event Action OnDeath;

        public event Action OnRevive;

        private readonly IVarReadOnly<float> _max;

        private float _value;

        private float _maxValue;

        public bool IsAlive => _value > 0;

        public float Max => _maxValue;

        public float Value
        {
            get => _value;
            set
            {
                float initialValue = _value;
                float newValue = math.clamp(value, 0, _maxValue);
                if (newValue == initialValue)
                    return;

                _value = newValue;
                OnChanged?.Invoke();

                if (initialValue == 0)
                {
                    OnRevive?.Invoke();
                }
                else
                {
                    if (_value == 0)
                        OnDeath?.Invoke();
                }
            }
        }

        public HealthComponent(HealthComponentData componentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentData, context, constructorContext)
        {
            _max = componentData.Max;
        }

        protected override void Init()
        {
            base.Init();
            if (_max is IVar<float> var)
                var.OnChanged += OnMaxValueObjChanged;

            OnMaxValueObjChanged();

            _value = _maxValue;
        }

        protected override void Destroyed()
        {
            if (_max is IVar<float> var)
                var.OnChanged -= OnMaxValueObjChanged;

            base.Destroyed();
        }

        private void OnMaxValueObjChanged()
        {
            _maxValue = _max.Evaluate(Owner.Data.VarResolver);
            OnMaxChanged?.Invoke();
        }
    }
}