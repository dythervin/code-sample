using System;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Dythervin.ObjectPool;
using Game.Data;

namespace Game
{
    public class DamagableComponent : EntityComponentExt<DamagableComponentData>,
        IDamagableComponent,
        IDamagePreprocessorContainerComponent
    {
        public event Action<Common.Damage> OnDamaged;

        private readonly HashList<IDamagePreprocessor> _preprocessors = new HashList<IDamagePreprocessor>();

        private IHealthComponent _health;

        public DamagableComponent(DamagableComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _health);
        }

        protected override void LateInit()
        {
            base.LateInit();
            SortPreprocessors();
        }

        public void Damage(Common.Damage damage)
        {
            using (var tempArray = _preprocessors.ToTempArray())
            {
                foreach (IDamagePreprocessor damagePreprocessor in tempArray)
                {
                    damagePreprocessor.Preprocess(ref damage);
                }
            }

            _health.Value -= damage.amount;
            this.Log($"Damaged {damage}");
            OnDamaged?.Invoke(damage);
        }

        public void Add(IDamagePreprocessor damagePreprocessor)
        {
            _preprocessors.Add(damagePreprocessor);
            if (IsLateInitialized == InitState.Initialized)
                SortPreprocessors();
        }

        private void SortPreprocessors()
        {
            _preprocessors.Sort((a, b) => a.Priority.AsSbyte().CompareTo(b.Priority.AsSbyte()));
        }

        public void Remove(IDamagePreprocessor damagePreprocessor)
        {
            _preprocessors.Remove(damagePreprocessor);
        }
    }
}