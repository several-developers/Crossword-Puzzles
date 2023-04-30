using System.IO;
using Core.Infrastructure.Data;
using UnityEngine;

namespace Core.Infrastructure.Services.Global
{
    public class SaveAndLoadDataService : ISaveAndLoadService
    {
        public SaveAndLoadDataService(IGameDataService gameDataService)
        {
            _gameDataPath = Application.persistentDataPath + Constants.GameDataPath;
            _gameDataService = gameDataService;
            _gameData = gameDataService.GetGameData();

            Load();
        }

        private readonly IGameDataService _gameDataService;
        private readonly GameData _gameData;
        private readonly string _gameDataPath;

        public async void Save()
        {
            string gameDataJson = JsonUtility.ToJson(_gameData);
            await File.WriteAllTextAsync(_gameDataPath, gameDataJson);
        }

        public void Load()
        {
            string gameDataJson;
            bool isSaveExists = File.Exists(_gameDataPath); 
            
            if (!isSaveExists)
            {
                gameDataJson = JsonUtility.ToJson(_gameData);
                File.WriteAllText(_gameDataPath, gameDataJson);
            }

            gameDataJson = File.ReadAllText(_gameDataPath);
            GameData gameData = JsonUtility.FromJson<GameData>(gameDataJson);
            _gameDataService.SetGameData(gameData);
        }
    }
}