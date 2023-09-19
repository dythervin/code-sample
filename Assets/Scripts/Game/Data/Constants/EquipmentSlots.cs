using Dythervin.Common;
using UnityEngine;

namespace Game.Data
{
    public struct EquipmentSlots
    {
        public static readonly EquipmentSlot None = EquipmentSlot.None;
        public static readonly EquipmentSlot MainHand = EquipmentSlot.Register(1, nameof(MainHand));
        public static readonly EquipmentSlot OffHand = EquipmentSlot.Register(2, nameof(OffHand));
        public static readonly EquipmentSlot Head = EquipmentSlot.Register(16, nameof(Head));
        public static readonly EquipmentSlot Torso = EquipmentSlot.Register(17, nameof(Torso));
        public static readonly EquipmentSlot Hands = EquipmentSlot.Register(18, nameof(Hands));
        public static readonly EquipmentSlot Legs = EquipmentSlot.Register(19, nameof(Legs));
        public static readonly EquipmentSlot Feet = EquipmentSlot.Register(20, nameof(Feet));
        public static readonly EquipmentSlot Neck = EquipmentSlot.Register(21, nameof(Neck));
        public static readonly EquipmentSlot LeftRing = EquipmentSlot.Register(22, nameof(LeftRing));
        public static readonly EquipmentSlot RightRing = EquipmentSlot.Register(23, nameof(RightRing));

        [RuntimeInitializeOnLoadMethod]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        private static void InitTags()
        {
        }
    }
}