using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Infrastructure
{
    public class EntryPoint : MonoInstaller
    {
        [SerializeField] private SOGameConfig _gameConfig;
        private Queue<IWarmupableSystem> _warmupQueue;
        public override void InstallBindings()
        {
            _warmupQueue = new Queue<IWarmupableSystem>();
            // Load enemy types
            // Load cards


            PlayerData playerData = new PlayerData();
            GameStateManager GSM = new GameStateManager();

            MapModel mapData = new MapModel(_gameConfig.ConfigMap, GSM);


            _warmupQueue.Enqueue(mapData);
            Container.Bind<INodeProvider>().To<MapModel>().FromInstance(mapData).AsCached();
            Container.Bind<IMapModel>().To<MapModel>().FromInstance(mapData).AsCached();


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
            loaderHandle.completed += (x) => SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }

        private void BindAndWarmupService<TInterface, TService>(Queue<IWarmupableSystem> warmupList, TService service)
            where TService : TInterface, IWarmupableSystem
        {
            if (!warmupList.Contains(service))
                warmupList.Enqueue(service);

            Container.Bind<TInterface>().To<TService>().FromInstance(service).AsCached();
        }


    }
}