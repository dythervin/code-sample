using System;
using Dythervin.Game.Framework;
using Game.Common;
using Game.Data;
using UnityEngine;

namespace Game.Items
{
    public abstract class Weapon<TWeaponData> : Equipment<TWeaponData>, IWeapon
        where TWeaponData : class, IWeaponData
    {
        private float _triggerTime = -100;

        public float Cooldown => Mathf.Max(_triggerTime - Time.time + Data.Current.cooldown, 0);

        IWeaponData IWeapon.Data => Data;

        public event Action<DamageHitData> OnHit;

        public event Action<Vector3, Damage> OnUse;

        public float Range => Data.DataAsset.Range;

        public void Use(Vector3 target)
        {
            Damage damage = default;
            //new Attack(Data.Damage, Random.value <= Data.CritChance, DamageType.PhysicalPierce, Data.Force, Owner);

            OnUse?.Invoke(target, damage);
        }

        public Weapon(TWeaponData inventoryComponent, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(inventoryComponent, context, constructorContext)
        {
        }

        protected virtual void Triggered()
        {
            _triggerTime = Time.time;
        }

        protected void AssertIsReady()
        {
            if (!this.IsReady())
                this.LogError(new Exception($"{GetType()} Not yer ready"));
        }

        protected virtual void Hit(DamageHitData obj)
        {
            OnHit?.Invoke(obj);
        }
    }
}