using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;

namespace Game.Game
{
    public class GameFactoryExt<TGameExt, TGameData> : ModelFactory<TGameExt, TGameData>
        where TGameData : class, IGameData
        where TGameExt : class, IGameExt, IModelInitializable
    {

        public GameFactoryExt(IModelContext context, IFeatureParameter[] featureParameters) : base(context) { }
    }
}