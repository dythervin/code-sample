using System.Collections.Generic;
using Dythervin.AI.GOAP;
using Dythervin.Game.Framework.Data;

namespace Game.Data
{
    public interface IGAgentData : IEntityComponentDataExt, IModelComponentOwnerData
    {
        new IReadOnlyList<IGAgentActionData> Components { get; }

        IReadOnlyList<StateValueGoal> Goals { get; }
    }
}