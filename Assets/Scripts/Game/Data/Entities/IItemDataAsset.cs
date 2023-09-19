namespace Game.Data
{
    public interface IItemDataAsset : ISharedDataAsset
    {
        float Price { get; }

        int Weight { get; }

        string Description { get; }

        string DisplayName { get; }
    }
}