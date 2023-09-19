using System.Runtime.InteropServices;
using Dythervin.Data.Structs;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [Guid("708689E9-0771-44D4-8BF3-7CE77B237180")]
    public class PatrolGActionData : IdleGActionData
    {
        [DSerializedField(10)]
        [SerializeField] private Range<float> randomPointDistance = new(4, 7);

        public Range<float> RandomPointDistance => randomPointDistance;
    }
}