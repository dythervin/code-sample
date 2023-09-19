using System;
using Game.Common;
using Game.Data;
using Game.Items;

namespace Game.View.Equipment
{
    public abstract class WeaponView<T> : EquipmentView<T>
        where T : class, IWeapon
    {
        private Func<IWeaponData, Damage> _damageFunc;

        public IWeaponSkin WeaponSkin { get; private set; }

        public Damage Damage => _damageFunc.Invoke(Model.Data);

        protected override void Init()
        {
            base.Init();
            WeaponSkin = Model.Data.DataAsset.SkinPool.Get(transform);
            Model.OnHit += Model_OnHit;
        }

        protected override void Destroyed()
        {
            Model.OnHit -= Model_OnHit;
            base.Destroyed();
        }

        protected virtual void Model_OnHit(DamageHitData obj)
        {
        }

        public void Set(Func<IWeaponData, Damage> damageGetter)
        {
            _damageFunc = damageGetter;
        }
    }
}