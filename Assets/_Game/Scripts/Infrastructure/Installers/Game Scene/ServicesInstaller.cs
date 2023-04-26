using Core.Infrastructure.Services.GameScene;
using Zenject;

namespace Core.Infrastructure.Providers.GameScene
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindCrosswordService();

        private void BindCrosswordService()
        {
            Container
                .BindInterfacesTo<CrosswordService>()
                .AsSingle()
                .NonLazy();
        }
    }
}