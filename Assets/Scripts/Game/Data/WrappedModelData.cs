using System;
using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [Guid("68655730-C25B-49B1-9624-27A7EE32D4E0")]
    [DSerializable]
    public partial class WrappedModelData<TModelData> : IModelDataWrapped<TModelData>
        where TModelData : class, IModelData
    {
        [DSerializedField(0)]
        [SerializeField] private AssetIdRef<IModelDataAssetWrapper<TModelData>> value;

        private TModelData _modelData;

        public TModelData WrappedData => _modelData ??= value.Load().WrappedData;

        public FeatureId FeatureId => WrappedData.FeatureId;

        IModelData IModelDataWrapper.WrappedData => WrappedData;

        public Type WrappedType => typeof(TModelData);

        public virtual ushort Version => 0;

        bool IModelData.IsReadOnly => true;
    }
}