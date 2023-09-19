using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;

namespace Game.Data
{
    [System.Serializable]
    [DSerializable]
    public abstract partial class ModelData : IModelData
    {
        public FeatureId FeatureId { get; }

        protected abstract TypeID<IModelData> GroupId { get; }

        protected ModelData()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            FeatureId = new FeatureId(GroupId, Features.GetDataId(this));
        }

        public virtual ushort Version => 0;

        public abstract bool IsReadOnly { get; }
    }
}