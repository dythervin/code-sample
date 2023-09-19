using Dythervin.Game.Framework;
using Game.Data;

namespace Game.AI
{
    public class WorkAgentAction : GAgentAction<WorkActionData>
    {
        public WorkAgentAction(WorkActionData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext) 
        {
        }
    }
}