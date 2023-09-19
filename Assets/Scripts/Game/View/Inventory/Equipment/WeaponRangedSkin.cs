using UnityEngine;

namespace Game.View.Equipment
{
    public class WeaponRangedSkin : WeaponSkin, IWeaponRangedSkin
    {
        [SerializeField] private Transform muzzle;

        public Transform Muzzle => muzzle;
    }
}