using System.Collections.Generic;
using Dythervin.Common;
using Game.Data;
using UnityEngine;

namespace Game.View.Data
{
    public abstract class EquipmentDataAsset : ItemDataAsset, IEquipmentDataAsset
    {
        [SerializeField, Min(0)] private int maxLevel;

        public int MaxLevel => maxLevel;

        public abstract IReadOnlyList<EquipmentSlot> Slots { get; }
    }
}