using System;
using Game.Data;

namespace Game.Items
{
    public interface IEquipment : IItem
    {
        event Action OnEquipChanged;
        new IEquipmentROData Data { get; }

        int Level { get; set; }

        bool IsEquipped { get; set; }
    }
}