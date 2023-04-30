using Core.Infrastructure.Providers.Global.Config;
using Zenject;

namespace Core.Infrastructure.Installers.Global
{
    public class ProvidersInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindConfigProvider();

        private void BindConfigProvider()
        {
            Container
                .BindInterfacesTo<ConfigProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}