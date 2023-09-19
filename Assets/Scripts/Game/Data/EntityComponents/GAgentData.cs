using System;
using System.Collections.Generic;
using Dythervin.AI.GOAP;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    [System.Runtime.InteropServices.Guid("D4FF6ECF-A6B1-404C-844E-63ADA65C2D4C")]
    [DSerializable]
    public partial class GAgentData : EntityComponentDataExt, IGAgentData
    {
        [DSerializedField(10)]
        [SerializeReference] private IGAgentActionData[] components;

        [DSerializedField(11)]
        [SerializeField] private StateValueGoal[] goals;

        public GAgentData(IGAgentActionData[] components, StateValueGoal[] goals)
        {
            this.components = components;
            this.goals = goals;
        }

        public GAgentData()
        {
            components = Array.Empty<IGAgentActionData>();
            goals = Array.Empty<StateValueGoal>();
        }

        public override bool IsReadOnly => true;

        public IReadOnlyList<IGAgentActionData> Components => components;

        public IReadOnlyList<StateValueGoal> Goals => goals;

        IReadOnlyList<IModelComponentData> IModelComponentOwnerROData.Components => Components;
    }
}