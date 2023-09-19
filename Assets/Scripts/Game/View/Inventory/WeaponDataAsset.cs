using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Common.Data;
using Dythervin.Game.Framework.Data;
using Dythervin.ObjectPool.Component;
using Dythervin.ObjectPool.Component.Global;
using Game.Data;
using Game.View.Data;
using UnityEngine;

namespace Game.View
{
    public class WeaponDataAsset<T> : EquipmentDataAsset, IWeaponDataAsset
        where T : MonoBehaviour, IWeaponSkin
    {
        [SerializeField] private FieldReadOnly<float> damage;

        [SerializeField] private FieldReadOnly<float> critChance;

        [SerializeField] private FieldReadOnly<float> critValue;

        [SerializeField] private FieldReadOnly<float> force;

        [SerializeField] private FieldReadOnly<float> cooldown;

        [SerializeField] private PrefabPooled<T> skinPool;

        [SerializeField] private float range;

        [SerializeField]
        private EquipmentSlot[] slots;

        [SerializeField]
        private WeaponType weaponType;

        public float Range => range;

        public override IReadOnlyList<EquipmentSlot> Slots => slots;

        public WeaponType WeaponType => weaponType;

        IComponentPoolOut<IWeaponSkin> IWeaponDataAsset.SkinPool => skinPool.Pool;

        public WeaponInstanceData GetData(IEntityROVars varResolver)
        {
            return new WeaponInstanceData(damage.Evaluate(varResolver),
                cooldown.Evaluate(varResolver),
                critChance.Evaluate(varResolver),
                critValue.Evaluate(varResolver),
                force.Evaluate(varResolver),
                range);
        }

        public PrefabPooled<T> SkinPool => skinPool;
    }
}