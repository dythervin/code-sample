using System.Runtime.InteropServices;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [Guid("A8761357-10C9-43D5-B9EA-2C58C544EF08")]
    public class IdleGActionData : GActionData, ITempGActionData
    {
        [DSerializedField(10)]
        [SerializeField] private float duration = 2;

        public float Duration => duration;
    }
}