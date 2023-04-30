using System.IO;
using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;
using Core.Infrastructure.Providers.Global.Config;
using UnityEngine;

namespace Core.Infrastructure.Services.Global
{
    public class SaveAndLoadConfigService : ISaveAndLoadService
    {
        public SaveAndLoadConfigService(IConfigProvider configProvider) =>
            _configProvider = configProvider;

        private readonly IConfigProvider _configProvider;

        public void Save()
        {
            GameConfig gameConfig = _configProvider.GetGameConfig();
            CrosswordConfig crosswordConfig = _configProvider.GetCrosswordConfig();

            CheckGridHelper(crosswordConfig);

            SaveConfig(gameConfig);
            SaveConfig(crosswordConfig);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        public void Load() =>
            _configProvider.LoadConfigs();

        private static async void SaveConfig<T>(T t) where T : IConfig
        {
            string configJson = JsonUtility.ToJson(t);
            await File.WriteAllTextAsync(t.GetFullPath(), configJson);
        }

        private static void CheckGridHelper(CrosswordConfig crosswordConfig)
        {
            bool emptyGridHelper = crosswordConfig.Equals(null) ||
                                   crosswordConfig.gridHelper.Length == 0;

            if (!emptyGridHelper)
                return;

            crosswordConfig.gridHelper = GetGridHelper();
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