using Dythervin.Game.Framework;
using Game.AI;
using Game.Data;

namespace Game
{
    public class GActionFactory<TGActionExt, TGActionData> : ModelComponentFactory<TGActionExt, TGActionData>
        where TGActionExt : class, IGAgentAction, IModelInitializable
        where TGActionData : class, IGAgentActionData
    {
        public GActionFactory(IModelContext context) : base(context)
        {
        }
    }
}