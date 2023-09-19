using Game.Data;
using Game.View.Equipment;
using UnityEngine;

namespace Game.View
{
    [CreateAssetMenu]
    public class WeaponRangedDataAsset : WeaponDataAsset<WeaponRangedSkin>, IWeaponRangedDataAsset
    {
        public bool IsRaycast => true;
    }
}