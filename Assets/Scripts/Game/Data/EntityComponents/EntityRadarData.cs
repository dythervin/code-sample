using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [System.Runtime.InteropServices.Guid("5E3B2B37-3F6B-4EC1-B685-A490DF72C6A2")]
    [DSerializable]
    public partial class EntityRadarData : EntityComponentDataExt
    {
        [DSerializedField(10)]
        [SerializeField] public float angle = 180;

        [DSerializedField(11)]
        [SerializeField] public LayerMask layerMask;

        public override bool IsReadOnly => true;

        public LayerMask LayerMask => layerMask;

        public float Angle => angle;

        public float Radius => 30;
    }
}