using System.IO;
using Core.Infrastructure.Data;
using UnityEngine;

namespace Core.Infrastructure.Services.Global
{
#warning Need rework.
    public class SaveAndLoadService : ISaveAndLoadService
    {
        public SaveAndLoadService(IGameDataService gameDataService)
        {
            _dataPath = "Assets/Resources/GameConfig.json";
            _gameDataService = gameDataService;
            _gameData = gameDataService.GetGameData();

#if UNITY_EDITOR
            CheckGridHelper();
#endif
            LoadGameData();
        }

        private readonly IGameDataService _gameDataService;
        private readonly GameData _gameData;
        private readonly string _dataPath;

        public async void SaveGameData()
        {
#if UNITY_EDITOR
            string gameDataJson = JsonUtility.ToJson(_gameData);
            await File.WriteAllTextAsync(_dataPath, gameDataJson);
#endif
        }

        public void LoadGameData()
        {
#if UNITY_EDITOR
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
#elif UNITY_ANDROID || UNITY_IOS
            TextAsset textAsset = Resources.Load<TextAsset>("GameConfig");
            GameData gameData = JsonUtility.FromJson<GameData>(textAsset.text);
            _gameDataService.SetGameData(gameData);
#endif
        }

        private void CheckGridHelper()
        {
            bool emptyGridHelper = _gameData.crosswordData.Equals(null) ||
                                   _gameData.crosswordData.gridHelper.Length == 0;

            if (!emptyGridHelper)
                return;

            _gameData.crosswordData.gridHelper = GetGridHelper();
        }

        private static string[] GetGridHelper()
        {
            string[] gridHelper =
            {
                " -> Don't edit! <- ",
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