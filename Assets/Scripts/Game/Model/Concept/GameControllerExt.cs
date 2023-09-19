using Dythervin.Common;
using Dythervin.Game.Framework;

namespace Game
{
    public class GameControllerExt : GameController, IModelContextExt, IGameControllerExt
    {
        public GameControllerExt(IAnyFactory anyFactory, IDataAssetRepository dataAssetRepository,
            IServiceContainer serviceContainer) : base(anyFactory, dataAssetRepository, serviceContainer)
        {
        }
    }
}