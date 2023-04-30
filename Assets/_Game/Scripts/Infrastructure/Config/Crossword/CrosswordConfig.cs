using System;

namespace Core.Infrastructure.Config.Crossword
{
    [Serializable]
    public class CrosswordConfig : IConfig
    {
        public CrosswordConfig()
        {
            gridHelper = Array.Empty<string>();
            wordsData = Array.Empty<WordData>();
        }

        public string[] gridHelper;
        public WordData[] wordsData;

        public string GetResourcesPath() =>
            Constants.CrosswordConfigResourcesPath;

        public string GetFullPath() =>
            Constants.CrosswordConfigFullPath;
    }
}