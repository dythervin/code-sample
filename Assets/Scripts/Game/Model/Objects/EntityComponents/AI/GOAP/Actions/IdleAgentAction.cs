using Dythervin.Game.Framework;
using Game.Data;

namespace Game.AI
{
    public class IdleAgentAction : TempGAgentAction<ITempGActionData>
    {
        public IdleAgentAction(ITempGActionData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext)  { }
    }
}