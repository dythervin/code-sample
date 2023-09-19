using System.Collections.Generic;
using Dythervin.Common;

namespace Game.Data
{
    public interface IEquipmentDataAsset : IItemDataAsset
    {
        int MaxLevel { get; }

        IReadOnlyList<EquipmentSlot> Slots { get; }
    }
}