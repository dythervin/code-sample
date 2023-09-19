using Game.Data;
using Game.Items;
using UnityEngine;
using Zenject;

namespace Game.View.Equipment
{
    public class WeaponViewRanged : WeaponView<IWeaponRanged>
    {
        private Transform _muzzle;

        private IWeaponRangedSkin RangedSkin => (IWeaponRangedSkin)WeaponSkin;

        protected override void Init()
        {
            base.Init();
            Model.OnTrigger += ModelOnOnTrigger;
            _muzzle.SetParent(RangedSkin.Muzzle, false);
        }

        protected override void Destroyed()
        {
            Model.OnTrigger -= ModelOnOnTrigger;
            base.Destroyed();
        }

        private void ModelOnOnTrigger(AttackTriggerData obj)
        {
            FireVFX();
        }

        private void FireVFX()
        {
        }

        public override void InstallBindings(DiContainer container)
        {
            base.InstallBindings(container);
            _muzzle = new GameObject("Muzzle") { transform = { parent = transform } }.transform;
            container.BindInstance(_muzzle).WithId(InjectId.TransformMuzzle).AsCached();
        }
    }
}