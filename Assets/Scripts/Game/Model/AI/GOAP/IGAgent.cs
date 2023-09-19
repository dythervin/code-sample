using Dythervin.AI.GOAP;
using Dythervin.Game.Framework;

namespace Game.AI.GOAP
{
    //TODO: Implement IController instead of IModel
    public interface IGAgent : IGAgentBase<GPlanParameters>, IModel
    {
    }
}