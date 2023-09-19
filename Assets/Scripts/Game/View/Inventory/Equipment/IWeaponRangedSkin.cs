using Game.Data;
using UnityEngine;

namespace Game.View.Equipment
{
    internal interface IWeaponRangedSkin : IWeaponSkin
    {
        Transform Muzzle { get; }
    }
}