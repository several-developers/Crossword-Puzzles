using Core.Enums;
using Core.Infrastructure.Services.BootstrapScene;
using Core.Infrastructure.Services.Global;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject]
        private void Construct(ICrosswordValidationService crosswordValidationService,
            IScenesLoaderService scenesLoaderService)
        {
            _crosswordValidationService = crosswordValidationService;
            _scenesLoaderService = scenesLoaderService;
        }

        private ICrosswordValidationService _crosswordValidationService;
        private IScenesLoaderService _scenesLoaderService;

        private void Awake() =>
            _crosswordValidationService.OnValidationFinished += OnValidationFinished;

        private void Start() => ValidateCrossword();

        private void OnDestroy() =>
            _crosswordValidationService.OnValidationFinished -= OnValidationFinished;

        private void ValidateCrossword() =>
            _crosswordValidationService.Validate();

        private void LoadGameScene() =>
            _scenesLoaderService.LoadScene(SceneID.Game);

        private void OnValidationFinished(ValidateResult result)
        {
            switch (result)
            {
                case ValidateResult.Success:
                    LoadGameScene();
                    break;
            }
        }
    }
}