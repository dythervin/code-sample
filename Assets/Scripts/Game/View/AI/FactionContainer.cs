using Dythervin.Collections;
using Dythervin.Core;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.Data;
using UnityEngine;

namespace Game.View.AI
{
    [SingletonAsset]
    public class FactionContainer : DataAsset, IFactionContainer, IFeatureParameter
    {
        [SerializeField] private SerializedDictionary<Faction, FactionData> data;

        public FactionData this[Faction faction] => data[faction];

        public Material Get(Faction faction)
        {
            return data[faction].material;
        }
    }
}