using System.Runtime.InteropServices;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Guid("7CB03979-1B76-4CFD-97FB-72DBCF0A3A23")]
    [System.Serializable]
    public class TargetAimComponentData : EntityComponentDataExt
    {
        [DSerializedField(10)]
        [SerializeField] private float angularSpeed = 240;

        [DSerializedField(11)]
        [SerializeField]
        private float defaultAngle = 1;

        public override bool IsReadOnly => true;

        public float AngularSpeed => angularSpeed;

        public float DefaultAngle => defaultAngle;
    }
}