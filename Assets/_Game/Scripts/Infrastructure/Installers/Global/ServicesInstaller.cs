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
            BindCrosswordValidationService();
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

        private void BindCrosswordValidationService()
        {
            Container
                .BindInterfacesTo<CrosswordValidationService>()
                .AsSingle()
                .NonLazy();
        }
    }
}