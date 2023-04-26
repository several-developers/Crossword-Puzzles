using System;
using Core.Enums;

namespace Core.Infrastructure.Services.Global
{
    public interface IScenesLoaderService
    {
        event Action OnLoadingStarted;
        event Action OnLoadingFinished;
        void LoadScene(SceneID sceneID);
        void LoadScene(SceneID sceneID, Action callback);
    }
}