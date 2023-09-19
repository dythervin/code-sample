using Dythervin.AssetIdentifier;
using Game.Data;
using UnityEngine;

namespace Game.View.Data
{
    public class SharedDataAsset : AssetIdentified, ISharedDataAsset
    {
    }
    
    public abstract class ItemDataAsset : SharedDataAsset, IItemDataAsset
    {
        [SerializeField] private float price;

        [SerializeField] private int weight;

        [SerializeField] private string displayName;

        [SerializeField] private string description;

        public float Price => price;

        public int Weight => weight;

        public string DisplayName => displayName;

        public string Description => description;
    }
}