using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Infrastructure
{
    public class EntryPoint : MonoInstaller
    {
        [SerializeField] private SOGameConfig _gameConfig;

        private GameStateMachine _GSM;

        private Queue<IWarmupableSystem> _warmupQueue;
        public override void InstallBindings()
        {
            _warmupQueue = new Queue<IWarmupableSystem>();
            _GSM = new GameStateMachine();

            // Load enemy factories
            // Load cards

            PlayerData playerData = new PlayerData(new Entity(_gameConfig.PlayerEntity));

            // Create game states
            GameState_Map stateMap = new GameState_Map(_GSM, _gameConfig.ConfigMap);
            GameState_Battle stateBattle = new GameState_Battle(_GSM, playerData);

            // Add State instances to the gsm
            _GSM.AddGameState(stateMap);
            _GSM.AddGameState(stateBattle);

            // Add systens to warmup queue
            _warmupQueue.Enqueue(stateMap);

            // State Interface Bindings
            Container.Bind<IMapModel>().To<GameState_Map>().FromInstance(stateMap).AsCached();
            Container.Bind<IBattleModel>().To<GameState_Battle>().FromInstance(stateBattle).AsCached();

            // Misc bindings 
            Container.Bind<INodeProvider>().To<GameState_Map>().FromInstance(stateMap).AsCached();

            Debug.Log("Bindings installed..");
        }

        private new void Start()
        {
            // InstallBindings() is invoked here
            SystemsWarmup();
            LoadGameScene();
        }

        private void SystemsWarmup()
        {
            foreach (var system in _warmupQueue)
            {
                system.WarmUp();
            }
            Debug.Log("Systems warmed up....");
        }

        private void LoadGameScene()
        {
            Debug.Log("Startup......");
            var loaderHandle = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            loaderHandle.completed += OnGameLoad;
        }

        private void OnGameLoad(AsyncOperation async)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
            _GSM.EnterState<GameState_Map>();
        }
    }
}