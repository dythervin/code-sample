using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class Character : EntityExt<ICharacterData>, ICharacter
    {
        public Character(ICharacterData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        ICharacterROData ICharacter.Data => Data;
    }
}