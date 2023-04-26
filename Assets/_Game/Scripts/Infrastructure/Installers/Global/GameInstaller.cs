using Core.Utilities;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Installers.Global
{
    public class GameInstaller : MonoInstaller, ICoroutineRunner
    {
        private const int TargetFramerate = 60;
        
        private void OnDestroy() => StopAllCoroutines();

        public override void InstallBindings()
        {
            SetTargetFramerate();
            BindCoroutineRunner();
        }

        private static void SetTargetFramerate() =>
            Application.targetFrameRate = TargetFramerate;

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}