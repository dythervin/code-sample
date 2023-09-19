using System;
using System.Runtime.InteropServices;
using Dythervin.AI.GOAP;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    [Guid("DC2E0F39-A55D-4B3E-A5C9-2889C4A40A84")]
    public abstract class GActionData : ModelData, IGAgentActionData
    {
        [BoxGroup]
        [DSerializedField(10)]
        [SerializeField] protected SuspiciousLevel suspiciousLevel;
#if ODIN_INSPECTOR
        [TableList]
        [BoxGroup]
#endif
        [DSerializedField(11)]
        [SerializeField] protected StateValue[] conditions;
#if ODIN_INSPECTOR
        [TableList]
        [BoxGroup]
#endif
        [DSerializedField(12)]
        [SerializeField] protected StateValueResult[] result;

#if ODIN_INSPECTOR
        [BoxGroup]
#endif
        [DSerializedField(13)]
        [SerializeField] private float baseCost;

        public float BaseCost => baseCost;

        public override bool IsReadOnly => true;

        public StateValue[] Conditions => conditions;

        public StateValueResult[] Result => result;

        public SuspiciousLevel SuspiciousLevel => suspiciousLevel;

        protected override TypeID<IModelData> GroupId => Features.GetDataGroupId<IGAgentActionData>();
    }
}