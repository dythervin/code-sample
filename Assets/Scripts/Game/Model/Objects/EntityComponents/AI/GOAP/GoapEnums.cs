using Dythervin.AI.GOAP;

namespace Game.AI
{
    [GoapState(1)]
    public enum TargetAlive : byte
    {
        False,
        True
    }

    [GoapState(2)]
    public enum TargetInReach : byte
    {
        False,
        True
    }

    [GoapState(3)]
    public enum Hungry : byte
    {
        False,
        True
    }

    [GoapState(4)]
    public enum Rest : byte
    {
        None,
        Rest,
        Work,
    }
}