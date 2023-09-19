using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Common;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Guid("9A451F52-05DF-46C0-AAA6-B9C3792D347E")]
    [DSerializable]
    [Serializable]
    public abstract partial class ItemData<T> : EntityDataExt, IItemData
        where T : class, IItemDataAsset
    {
        [DSerializedField(15)]
        [SerializeField] private AssetIdRef<T> itemAssetId;

        public AssetIdRef<T> DataAssetId => itemAssetId;

        IItemDataAsset IItemROData.DataAsset => DataAsset;

        public T DataAsset => itemAssetId.Load();

        AssetId IItemROData.DataAssetId => itemAssetId;

        protected ItemData(AssetId itemAssetId = default, Tag tags = default,
            IReadOnlyList<IEntityComponentDataExt> components = null) : base(tags)
        {
            this.itemAssetId = itemAssetId;
        }
    }
}