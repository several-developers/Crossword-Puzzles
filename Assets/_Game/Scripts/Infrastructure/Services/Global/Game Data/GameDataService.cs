using Core.Infrastructure.Data;
using Core.Infrastructure.Data.Player;

namespace Core.Infrastructure.Services.Global
{
    public class GameDataService : IGameDataService
    {
        public GameDataService() =>
            _gameData = new();

        private GameData _gameData;

        public void SetGameData(GameData gameConfig) =>
            _gameData = gameConfig;

        public GameData GetGameData() => _gameData;

        public PlayerData GetPlayerData() =>
            _gameData.playerData;
    }
}