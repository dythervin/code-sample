namespace Game
{
    public interface IAISightComponent : IEntityComponentExt
    {
        float Interval { get; set; }

        void Check();
    }
}