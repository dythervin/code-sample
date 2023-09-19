using Dythervin.Game.Framework;
using Game.Data;
using Game.GameComponents;

namespace Game
{
    public class GameComponentFactory<TGameComponentExt, TGameComponentDataExt>
        : ModelComponentFactory<TGameComponentExt, TGameComponentDataExt>
        where TGameComponentExt : class, IModelInitializable, IGameComponentExt
        where TGameComponentDataExt : class, IGameComponentDataExt
    {
        public GameComponentFactory(IModelContext context) : base(context) { }
    }
}