using System;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    [System.Runtime.InteropServices.Guid("C91713B2-33F2-4742-B14C-CBE9CABCC519")]
    [DSerializable]
    public partial class SightControllerData : EntityComponentDataExt
    {
        [DSerializedField(10)]
        [SerializeField] private float checkInterval;

        public float CheckInterval => checkInterval;

        public SightControllerData(float checkInterval)
        {
            this.checkInterval = checkInterval;
        }

        public override bool IsReadOnly => true;
        public SightControllerData() : this(0.2f)
        {
        }
    }
}