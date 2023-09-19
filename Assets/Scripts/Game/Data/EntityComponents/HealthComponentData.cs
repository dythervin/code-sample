using System;
using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Data.Abstractions;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    [DSerializable]
    [System.Runtime.InteropServices.Guid("D57535FB-9AC4-4EDB-B831-F55A88DAD0EB")]
    public partial class HealthComponentData : EntityComponentDataExt
    {
        [DSerializedField(10)]
        [SerializeField] private AssetIdRef<IVarReadOnly<float>> max;

        [Range(0, 1)]
        [DSerializedField(11)]
        [SerializeField] private float current;

        public IVarReadOnly<float> Max => max.Load();

        public override bool IsReadOnly => false;

        public HealthComponentData(AssetIdRef<IVarReadOnly<float>> max, float current)
        {
            this.max = max;
            this.current = current;
        }

        public HealthComponentData()
        {
        }
    }
}