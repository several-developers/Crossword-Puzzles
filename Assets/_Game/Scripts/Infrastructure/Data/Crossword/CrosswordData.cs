using System;

namespace Core.Infrastructure.Data.Crossword
{
    [Serializable]
    public class CrosswordData
    {
        public CrosswordData()
        {
            gridHelper = Array.Empty<string>();
            wordsData = Array.Empty<WordData>();
        }

        public string crosswordName;
        public string[] gridHelper;
        public WordData[] wordsData;
    }
}