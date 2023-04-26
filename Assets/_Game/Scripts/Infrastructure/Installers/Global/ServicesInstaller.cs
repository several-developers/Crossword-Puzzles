using Core.Infrastructure.Services.Global;
using Zenject;

namespace Core.Infrastructure.Installers.Global
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameDataService();
            BindSaveAndLoadService();
            BindScenesLoaderService();
        }

        private void BindGameDataService()
        {
            Container
                .BindInterfacesTo<GameDataService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindSaveAndLoadService()
        {
            Container
                .BindInterfacesTo<SaveAndLoadService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindScenesLoaderService()
        {
            Container
                .BindInterfacesTo<ScenesLoaderService>()
                .AsSingle()
                .NonLazy();
        }
    }
}