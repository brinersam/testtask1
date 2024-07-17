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

            BindAndWarmupService<IMapNodeModel, MapData>(_warmupQueue, new MapData(_gameConfig.ConfigMap));


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

            Container.Bind<TInterface>().To<TService>().FromInstance(service).AsSingle();
        }


    }
}