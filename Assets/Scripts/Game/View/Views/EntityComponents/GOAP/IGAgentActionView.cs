using Dythervin.Game.Framework.View;
using Game.AI;

namespace Game.View.GOAP
{
    public interface IGAgentActionView : IModelComponentView, IModelView<IGAgentAction>
    {
        new IGAgentAction Model { get; }

        new IGAgentComponentView Owner { get; }
    }
}