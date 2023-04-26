using Core.Infrastructure.Services.Global;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Core.UI.LoadingScreen
{
    public class LoadingScreenUI : MonoBehaviour
    {
        [Inject]
        private void Construct(IScenesLoaderService scenesLoaderService)
        {
            _scenesLoaderService = scenesLoaderService;

            _scenesLoaderService.OnLoadingStarted += OnLoadingStarted;
            _scenesLoaderService.OnLoadingFinished += OnLoadingFinished;
        }

        [SerializeField, Min(0)]
        private float _fadeDuration = 0.3f;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private static LoadingScreenUI _instance;
        
        private IScenesLoaderService _scenesLoaderService;
        private Tweener _fadeTN;

        private void Awake() => TryRegisterSingleton();

        private void Start() =>
            FadeOut(instant: true);

        private void OnDestroy()
        {
            _scenesLoaderService.OnLoadingStarted -= OnLoadingStarted;
            _scenesLoaderService.OnLoadingFinished -= OnLoadingFinished;
        }

        private void TryRegisterSingleton()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void FadeIn(bool instant = false) =>
            FadeAnimation(fadeIn: true, instant);

        private void FadeOut(bool instant = false) =>
            FadeAnimation(fadeIn: false, instant);

        private void FadeAnimation(bool fadeIn, bool instant)
        {
            float endValue = fadeIn ? 1 : 0;
            float duration = instant ? 0 : _fadeDuration;

            SetCanvasInteractableState(isInteractable: true);
            
            _fadeTN.Kill();
            _fadeTN = _canvasGroup
                .DOFade(endValue, duration)
                .SetLink(gameObject)
                .OnComplete(() => SetCanvasInteractableState(isInteractable: false));
        }

        private void SetCanvasInteractableState(bool isInteractable)
        {
            _canvasGroup.interactable = isInteractable;
            _canvasGroup.blocksRaycasts = isInteractable;
        }

        private void OnLoadingStarted() => FadeIn();

        private void OnLoadingFinished() => FadeOut();
    }
}
