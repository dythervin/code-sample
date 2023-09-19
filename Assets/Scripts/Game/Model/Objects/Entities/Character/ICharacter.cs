using Game.Data;

namespace Game
{
    public interface ICharacter : IEntityExt
    {
        new ICharacterROData Data { get; }
    }
}