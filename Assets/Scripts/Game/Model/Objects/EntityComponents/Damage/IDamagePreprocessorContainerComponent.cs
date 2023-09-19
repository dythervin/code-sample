namespace Game
{
    public interface IDamagePreprocessorContainerComponent : IEntityComponentExt
    {
        void Add(IDamagePreprocessor damagePreprocessor);

        void Remove(IDamagePreprocessor damagePreprocessor);
    }
}