using System;
using Core.Infrastructure.Data.Crossword;
using Core.Infrastructure.Data.Player;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        public GameData()
        {
            playerData = new();
            crosswordData = new();
        }

        public PlayerData playerData;
        public CrosswordData crosswordData;
    }
}