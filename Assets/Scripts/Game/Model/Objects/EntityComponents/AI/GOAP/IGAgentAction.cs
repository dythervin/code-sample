using Dythervin.AI.GOAP;
using Dythervin.Game.Framework;
using Game.AI.GOAP;

namespace Game.AI
{
    public interface IGAgentAction : IGActionBase<GPlanParameters>, IModelComponent
    {
        new IGAgentComponent Owner { get; }
    }
}