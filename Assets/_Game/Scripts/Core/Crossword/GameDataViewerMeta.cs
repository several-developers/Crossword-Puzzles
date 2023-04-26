using Core.Infrastructure.Data;
using Core.Infrastructure.Services.Global;
using UnityEngine;

namespace Core.Crossword
{
    [CreateAssetMenu]
    public class GameDataViewerMeta : ScriptableObject
    {
        [SerializeField]
        private GameData _gameData;

        public void SaveGameData()
        {
            SaveAndLoadService saveAndLoadService = new(_gameData);
            saveAndLoadService.SaveGameData();
        }

        public void LoadGameData()
        {
            GameDataService gameDataService = new();
            SaveAndLoadService saveAndLoadService = new(gameDataService);
            saveAndLoadService.LoadGameData();
            _gameData = gameDataService.GetGameData();
        }
    }
}