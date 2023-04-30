using Core.Infrastructure.Data;
using Core.Infrastructure.Data.Player;

namespace Core.Infrastructure.Services.Global
{
    public interface IGameDataService
    {
        void SetGameData(GameData gameConfig);
        GameData GetGameData();
        PlayerData GetPlayerData();
    }
}