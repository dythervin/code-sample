using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;
using UnityEngine;

namespace Game.View
{
    public class CharacterSlotViewComponent : MonoViewComponentExt<CharacterView>
    {
        [SerializeField] private SerializedDictionary<EquipmentSlot, Transform> slots;

        public IReadOnlyDictionary<EquipmentSlot, Transform> Slots => slots;
    }
}