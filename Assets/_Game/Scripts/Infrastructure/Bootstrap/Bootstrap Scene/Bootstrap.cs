using Core.Enums;
using Core.Infrastructure.Services.Global;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Bootstrap.BootstrapScene
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject]
        private void Construct(IScenesLoaderService scenesLoaderService) =>
            _scenesLoaderService = scenesLoaderService;
        
        private IScenesLoaderService _scenesLoaderService;

        private void Start() => LoadGameScene();

        private void LoadGameScene() =>
            _scenesLoaderService.LoadScene(SceneID.Game);
    }
}