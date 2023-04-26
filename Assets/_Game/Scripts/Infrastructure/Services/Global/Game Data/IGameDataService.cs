using Core.Infrastructure.Data;
using Core.Infrastructure.Data.Crossword;

namespace Core.Infrastructure.Services.Global
{
    public interface IGameDataService
    {
        void SetGameData(GameData gameData);
        GameData GetGameData();
        CrosswordData GetCrosswordData();
    }
}