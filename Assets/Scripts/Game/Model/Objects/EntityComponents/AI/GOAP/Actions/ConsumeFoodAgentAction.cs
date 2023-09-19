using Dythervin.Game.Framework;
using Game.Data;

namespace Game.AI
{
    public class ConsumeFoodAgentAction : GAgentAction<ConsumeFoodActionData>
    {
        public ConsumeFoodAgentAction(ConsumeFoodActionData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext)  { }
    }
}