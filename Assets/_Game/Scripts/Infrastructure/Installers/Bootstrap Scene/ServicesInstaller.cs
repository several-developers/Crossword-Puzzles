using Core.Infrastructure.Services.BootstrapScene;
using Zenject;

namespace Core.Infrastructure.Installers.BootstrapScene
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindCrosswordValidationService();

        private void BindCrosswordValidationService()
        {
            Container
                .BindInterfacesTo<CrosswordValidationService>()
                .AsSingle()
                .NonLazy();
        }
    }
}