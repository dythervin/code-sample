using System;
using System.Runtime.InteropServices;
using Dythervin.AI.GOAP;

namespace Game.Data
{
    [Serializable]
    [Guid("8A34E8A6-DB97-493C-AA6C-9AD250EE5351")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public sealed class GActionDataWrapped : WrappedModelData<IGAgentActionData>, IGAgentActionData
    {
        float IGAgentActionData.BaseCost => WrappedData.BaseCost;

        StateValue[] IGAgentActionData.Conditions => WrappedData.Conditions;

        StateValueResult[] IGAgentActionData.Result => WrappedData.Result;

        SuspiciousLevel IGAgentActionData.SuspiciousLevel => WrappedData.SuspiciousLevel;
    }
}