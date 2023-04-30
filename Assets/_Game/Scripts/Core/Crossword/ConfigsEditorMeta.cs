using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;
using Core.Infrastructure.Providers.Global.Config;
using Core.Infrastructure.Services.Global;
using UnityEngine;

namespace Core.Crossword
{
    [CreateAssetMenu]
    public class ConfigsEditorMeta : ScriptableObject
    {
        [SerializeField]
        private GameConfig _gameConfig;

        [SerializeField]
        private CrosswordConfig _crosswordConfig;

        public void SaveConfigs()
        {
            MockConfigProvider mockConfigProvider = new();
            mockConfigProvider.SetGameConfig(_gameConfig);
            mockConfigProvider.SetCrosswordConfig(_crosswordConfig);
            
            SaveAndLoadConfigService service = new(mockConfigProvider);
            service.Save();
        }

        public void LoadConfigs()
        {
            ConfigProvider configProvider = new();
            configProvider.LoadConfigs();
            
            _gameConfig = configProvider.GetGameConfig();
            _crosswordConfig = configProvider.GetCrosswordConfig();
        }
    }
}