using Dythervin.AI.GOAP;

namespace Game.Data
{
    public interface IGAgentActionData : IEntityComponentDataExt
    {
        float BaseCost { get; }

        StateValue[] Conditions { get; }

        StateValueResult[] Result { get; }

        SuspiciousLevel SuspiciousLevel { get; }
    }
}