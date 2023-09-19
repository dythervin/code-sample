using Pathfinding;

namespace Game.AI.PathfindingExt
{
    public interface IAstarAICustom : IAstarAI
    {
        float StoppingDistance { get; set; }
        void ResetStoppingDistance();
    }
}