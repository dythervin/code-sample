using System;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;

namespace Game.Items
{
    public interface IWeaponRangedAimAmplifier
    {
        
    }
    public interface IWeaponRanged : IWeapon
    {
        event Action<AttackTriggerData> OnTrigger;

        new IWeaponRangedData Data { get; }

        void Trigger(in Vector3 target, IReadOnlyList<IWeaponRangedAimAmplifier> aimAmplifiers = null);
    }
}