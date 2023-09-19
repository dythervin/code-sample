using Dythervin.Game.Framework;
using Game.AI.GOAP;

namespace Game.AI
{
    public interface IGAgentComponent : IEntityComponentExt, IGAgent, IModelComponentOwner
    {
        void TryRequestPlanAndAct(float delay = 0);
    }
}