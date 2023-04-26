using Zenject;

namespace Core.Infrastructure.Providers.GameScene
{
    public class ProvidersInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindGridProvider();

        private void BindGridProvider()
        {
            Container
                .BindInterfacesTo<CrosswordProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}