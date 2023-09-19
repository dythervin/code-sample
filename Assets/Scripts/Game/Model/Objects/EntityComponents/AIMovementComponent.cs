using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class AIMovementComponent : NavMovementComponent<AIMovementComponentData>
    {
        public AIMovementComponent(AIMovementComponentData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }
}