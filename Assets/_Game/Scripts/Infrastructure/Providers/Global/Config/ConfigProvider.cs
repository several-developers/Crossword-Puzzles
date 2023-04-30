using System;
using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;
using Core.Utilities;
using UnityEngine;

namespace Core.Infrastructure.Providers.Global.Config
{
    public class ConfigProvider : IConfigProvider
    {
        public ConfigProvider() => LoadConfigs();
        
        private GameConfig _gameConfig;
        private CrosswordConfig _crosswordConfig;

        public GameConfig GetGameConfig() => _gameConfig;

        public CrosswordConfig GetCrosswordConfig() => _crosswordConfig;

        public void LoadConfigs()
        {
            LoadConfig(ref _gameConfig);
            LoadConfig(ref _crosswordConfig);
        }

        private static void LoadConfig<T>(ref T t) where T : IConfig
        {
            t ??= (T)Activator.CreateInstance(typeof(T));
            TextAsset textAsset = Resources.Load<TextAsset>(t.GetResourcesPath());

            if (textAsset == null)
            {
                LogConfigLoadError(typeof(T).Name);
                return;
            }
            
            t = JsonUtility.FromJson<T>(textAsset.text);
        }

        private static void LogConfigLoadError(string configName)
        {
            string errorLog = Log.Print($"<gb>{configName}.json</gb> <rb>not found</rb>!");
            Debug.LogError(errorLog);
        }
    }
}