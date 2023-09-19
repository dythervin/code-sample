using Dythervin.Collections;
using Dythervin.Data.Structs;

namespace Game.Data
{
    public interface IArmorDataAsset : IEquipmentDataAsset
    {
        SerializedDictionary<DamageType, VarReadOnly<float>> Resistances { get; }
    }
}