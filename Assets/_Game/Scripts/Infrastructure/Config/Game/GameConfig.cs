using System;
using Core.Crossword;

namespace Core.Infrastructure.Config
{
    [Serializable]
    public class GameConfig : IConfig
    {
        public GameConfig() =>
            errorBehaviour = new(shakeOnError: true, playSoundOnError: false);

        public ErrorBehaviour errorBehaviour;

        public string GetResourcesPath() =>
            Constants.GameConfigResourcesPath;

        public string GetFullPath() =>
            Constants.GameConfigFullPath;
    }
}