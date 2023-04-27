using System;

namespace Core.GameConfig
{
    [Serializable]
    public class CrosswordConfigData
    {
        public CrosswordConfigData()
        {
            gridHelper = Array.Empty<string>();
            wordsData = Array.Empty<WordConfigData>();
        }

        public string[] gridHelper;
        public WordConfigData[] wordsData;
    }
}