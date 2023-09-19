using Game.Data;
using Game.Items;
using UnityEngine;

namespace Game.View.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = MenuName + nameof(WeaponRanged))]
    public class WeaponRangedDataWrappedAsset : EntityDataWrappedAsset<WeaponRangedData>
    {
    }
}