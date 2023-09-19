using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.Controller.Rules;
using Game.Data;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game
{
    [DefaultExecutionOrder(-1)]
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private GameDataWrappedAsset gameDataAsset;

        [Inject] private IGameControllerExt _gameController;

        [Inject] private IGameRulesContextExt _gameRulesContext;

        [Inject]
        protected virtual void DefineKeys(IAssetKeyResolverService resolver)
        {
            resolver.Register<IGameData>("Game");
            resolver.Register<IEntityROData>("Entity");
            resolver.Register<IEntityComponentData>("EntityComponent");
            resolver.Register<IGAgentActionData>("GAction");
        }

        private void Awake()
        {
            _gameRulesContext.Init(new DefaultGameRules());
            StartGame();
        }

        private void StartGame()
        {
            _gameController.Start(gameDataAsset.WrappedData);
        }
    }
}