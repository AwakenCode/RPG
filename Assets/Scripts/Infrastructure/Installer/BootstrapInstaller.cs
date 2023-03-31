using System.Threading.Tasks;
using Infrastructure.Factory;
using Service;
using Service.Asset;
using Service.Data;
using Service.Input;
using Zenject;

namespace Infrastructure.Installer
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindTaskProvider();
            BindCoroutineRunner();
            BindAssetProvider();
            BindDataProvider();
            BindFactory();
            BindInputService();
            BindSceneLoader();
        }
        
        private void BindTaskProvider()
        {
            Container
                .Bind<TaskProvider>()
                .FromInstance(new TaskProvider())
                .AsSingle();
        }
        
        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();
        }
        
        private void BindInputService()
        {
            var playerInputTask = Container
                .Resolve<IGameFactory>()
                .CreateInput();

            Container
                .Bind<Task<PlayerInput>>()
                .FromInstance(playerInputTask)
                .AsSingle();
        }

        private void BindFactory()
        {
            var gameFactory = new GameFactory(
                Container.Resolve<IAssetProvider>(),
                Container.Resolve<IDataProvider>(), 
                Container); 
            
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .FromInstance(gameFactory)
                .AsSingle();
        }
        
        private void BindSceneLoader()
        {
            var curtain = Container
                .Resolve<IGameFactory>()
                .CreateCurtain();
            
            Container
                .BindInstance(new SceneLoader(curtain))
                .AsSingle();
        }

        private void BindAssetProvider()
        {
            var assetProvider = new AssetProvider(); 
            assetProvider.Initialize();
            
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .FromInstance(assetProvider)
                .AsSingle();
        }

        private void BindDataProvider()
        {
            var dataProvider = new DataProvider();
            dataProvider.Load();
            
            Container
                .Bind<IDataProvider>()
                .To<DataProvider>()
                .FromInstance(dataProvider)
                .AsSingle();
        }
    }
}