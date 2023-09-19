using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Data.Structs;
using Game.Data;
using UnityEngine;

namespace Game.View.Data
{
    [CreateAssetMenu(menuName = "Equipment/Armor")]
    public class ArmorDataAsset : EquipmentDataAsset, IArmorDataAsset
    {
        [SerializeField] private SerializedDictionary<DamageType, VarReadOnly<float>> resistances;
        
        [SingleDEnum]
        [SerializeField] private EquipmentSlot[] slots;

        public SerializedDictionary<DamageType, VarReadOnly<float>> Resistances => resistances;

        public override IReadOnlyList<EquipmentSlot> Slots => slots;
    }
}