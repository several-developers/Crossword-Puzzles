using System;
using Core.Infrastructure.Data.Crossword;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        public GameData() =>
            crosswordData = new();
        
        public CrosswordData crosswordData;
    }
}