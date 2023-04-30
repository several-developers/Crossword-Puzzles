using System;
using System.Collections;
using Core.Enums;
using Core.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure.Services.Global
{
    public class ScenesLoaderService : IScenesLoaderService
    {
        public ScenesLoaderService(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public event Action OnLoadingStarted;
        public event Action OnLoadingFinished;

        private readonly ICoroutineRunner _coroutineRunner;

        private bool _loadingInProgress;

        public void LoadScene(SceneID sceneID) =>
            LoadScene(sceneID, callback: null);

        public void LoadScene(SceneID sceneID, Action callback)
        {
            if (_loadingInProgress)
                return;

            _coroutineRunner.StartCoroutine(SceneLoaderCO(sceneID, callback));
        }

        private IEnumerator SceneLoaderCO(SceneID sceneID, Action callback = null)
        {
            OnLoadingStarted?.Invoke();
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID.ToString());
            _loadingInProgress = true;

            while (!asyncLoad.isDone)
                yield return null;

            _loadingInProgress = false;
            
            OnLoadingFinished?.Invoke();
            callback?.Invoke();
        }
    }
}