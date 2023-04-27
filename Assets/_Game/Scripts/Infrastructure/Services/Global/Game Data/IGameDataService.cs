using Core.Infrastructure.Data;
using Core.Infrastructure.Data.Crossword;
using Core.Infrastructure.Data.Player;

namespace Core.Infrastructure.Services.Global
{
    public interface IGameDataService
    {
        void SetGameData(GameData gameData);
        GameData GetGameData();
        PlayerData GetPlayerData();
        CrosswordData GetCrosswordData();
    }
}