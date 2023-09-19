using Dythervin.Game.Framework.View;
using Zenject;

namespace Game.View
{
    public class GameObjectFactoryExt : GameObjectFactory
    {
        [Inject]
        public GameObjectFactoryExt(IAssetDatabaseService assetDatabaseService, IAssetKeyResolverService assetKeyResolver) : base(
            assetDatabaseService, assetKeyResolver)
        {
        }
    }
}