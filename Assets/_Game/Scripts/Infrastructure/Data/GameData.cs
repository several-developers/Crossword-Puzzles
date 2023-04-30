using System;
using Core.Infrastructure.Data.Player;

namespace Core.Infrastructure.Data
{
    [Serializable]
    public class GameData
    {
        public GameData() =>
            playerData = new();

        public PlayerData playerData;
    }
}