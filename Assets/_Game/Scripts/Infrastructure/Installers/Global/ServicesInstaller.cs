using Core.Infrastructure.Services.Global;
using Zenject;

namespace Core.Infrastructure.Installers.Global
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindScenesLoaderService();

        private void BindScenesLoaderService()
        {
            Container
                .BindInterfacesTo<ScenesLoaderService>()
                .AsSingle()
                .NonLazy();
        }
    }
}