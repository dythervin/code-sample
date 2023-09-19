using System.Collections.Generic;
using Dythervin.AssetIdentifier;
using Dythervin.Common;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    public interface IItemStackableData : IItemData, IItemStackableROData
    {
        new int Amount { get; set; }
    }

    public interface IItemStackableROData : IItemROData
    {
        int Amount { get; }
    }

    [System.Serializable]
    public class ItemStackableData : ItemData<IItemStackableDataAsset>, IItemStackableData
    {
        [DSerializedField(20)]
        [SerializeField] private int amount;

        public int Amount => amount;

        int IItemStackableData.Amount
        {
            get => amount;
            set => amount = value;
        }

        public ItemStackableData(int amount = default, AssetId itemAssetId = default, Tag tags = default,
            IReadOnlyList<IEntityComponentDataExt> components = null) : base(itemAssetId, tags, components)
        {
            this.amount = amount;
        }

        public ItemStackableData() : this(default)
        {
        }
    }
}