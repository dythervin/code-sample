using System.Collections.Generic;
using Dythervin.Core;
using Dythervin.Game.Framework;
using Game.Common;
using Game.Data;
using Unity.Mathematics;

namespace Game
{
    public class ResistanceComponent : EntityComponentExt<ResistanceComponentData>, IResistanceComponent, IDamagePreprocessor
    {
        private readonly HashSet<IResistanceProvider> _resistanceProviders = new();
        private IDamagePreprocessorContainerComponent _damagePreprocessorContainerComponent;

        public Priority Priority => Priority.Default;

        public ResistanceComponent(ResistanceComponentData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _damagePreprocessorContainerComponent);
            _damagePreprocessorContainerComponent.Add(this);
        }

        protected override void Destroyed()
        {
            _damagePreprocessorContainerComponent.Remove(this);
            base.Destroyed();
        }

        public void Add(IResistanceProvider resistanceProvider)
        {
            _resistanceProviders.Add(resistanceProvider);
        }

        public void Remove(IResistanceProvider resistanceProvider)
        {
            _resistanceProviders.Remove(resistanceProvider);
        }

        public float this[DamageType damageType]
        {
            get
            {
                int points = 0;
                foreach (IResistanceProvider provider in _resistanceProviders)
                {
                    points += provider[damageType];
                }

                return ResistanceCalculation.Calculate(damageType, points);
            }
        }

        public void Calculate(DamageType damageType, ref float damage)
        {
            float resistance = this[damageType];
            damage *= resistance switch
            {
                //>100% Damage heals
                >= 1 => -(resistance - 1),
                //0-100% Damage decreases
                < 0 => 1 + math.abs(resistance),
                //<0% Damage increases
                _ => 1 - resistance
            };
        }

        public void Preprocess(ref Damage damage)
        {
            float amount = damage.amount;
            Calculate(damage.type, ref amount);
            damage = new Damage(damage, amount);
        }
    }
}