using Dythervin.Common;
using Dythervin.Data.Structs;
using Dythervin.Game.Framework;
using Game.AI.GOAP;
using Game.Data;

namespace Game.AI
{
    public class PatrolAgentAction : TempGAgentAction<PatrolGActionData>
    {
        public override void SetParameters(GPlanParameters parameters)
        {
            base.SetParameters(parameters);
            parameters.destinations.Add(AstarPath.active
                .GetNearest(RandomExt.InsideUnitSphere2d * InitialData.RandomPointDistance.GetRandom()).position);
        }

        public PatrolAgentAction(PatrolGActionData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }
}