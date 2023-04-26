using System.IO;
using Core.Infrastructure.Data;
using Core.Infrastructure.Providers.GameScene;
using UnityEngine;

namespace Core.Infrastructure.Services.Global
{
    public class SaveAndLoadService : ISaveAndLoadService
    {
        private SaveAndLoadService() =>
            _dataPath = Application.persistentDataPath + "/GameData.json";

        public SaveAndLoadService(GameData gameData) : this()
        {
            _gameData = gameData;
            CheckGridHelper();
        }

        public SaveAndLoadService(IGameDataService gameDataService) : this()
        {
            _gameDataService = gameDataService;
            _gameData = gameDataService.GetGameData();

            CheckGridHelper();
            LoadGameData();
        }

        private readonly IGameDataService _gameDataService;
        private readonly GameData _gameData;
        private readonly string _dataPath;

        public void SaveGameData()
        {
            string gameDataJson = JsonUtility.ToJson(_gameData);
            File.WriteAllText(_dataPath, gameDataJson);
        }

        public void LoadGameData()
        {
            if (!File.Exists(_dataPath))
            {
                SaveGameData();
                return;
            }

            string data = File.ReadAllText(_dataPath);

            if (string.IsNullOrWhiteSpace(data))
                return;

            GameData gameData = JsonUtility.FromJson<GameData>(data);
            _gameDataService.SetGameData(gameData);
        }

        private void CheckGridHelper()
        {
            bool emptyGridHelper = _gameData.crosswordData.Equals(null) ||
                                   _gameData.crosswordData.gridHelper.Length == 0;

            if (!emptyGridHelper)
                return;

            _gameData.crosswordData.gridHelper = GetGridHelper();
        }
        
        private string[] GetGridHelper()
        {
            string[] gridHelper = {
                "0 1 2 3 4 5 6 7 8 9",
                "1 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "2 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "3 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "4 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "5 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "6 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "7 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "8 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
                "9 ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐ ☐",
            };

            return gridHelper;
        }
    }
}