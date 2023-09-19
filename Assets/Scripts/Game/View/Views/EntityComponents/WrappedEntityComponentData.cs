using System.Runtime.InteropServices;
using Game.Data;

namespace Game.View
{
    // [System.Serializable]
    // [Guid("6D29DED8-3586-4260-B379-149A350CFF3A")]
    // [Dythervin.Serialization.SourceGen.DSerializable]
    // public partial class WrappedEntityComponentData<T> : WrappedModelData<T>, IEntityComponentDataExt
    //     where T : class, IEntityComponentDataExt
    // {
    // }

    [System.Serializable]
    [Guid("4ABBF0D4-1163-494C-8959-8229EC65A394")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public sealed class WrappedEntityComponentData : WrappedModelData<IEntityComponentDataExt>,
        IEntityComponentDataExt
    {
    }
}