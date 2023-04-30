using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;
using Core.Infrastructure.Providers.Global.Config;
using Core.Infrastructure.Services.Global;
using UnityEngine;
using Zenject;

namespace Core.Crossword
{
    [CreateAssetMenu]
    public class ConfigsEditorMeta : ScriptableObjectInstaller
    {
        [SerializeField]
        private GameConfig _gameConfig;

        [SerializeField]
        private CrosswordConfig _crosswordConfig;

        [SerializeField]
        [Tooltip("Automatically re-create crossword after saving configs (only runtime).")]
        private bool _autoRecreateCrossword;

        public bool AutoRecreateCrossword => _autoRecreateCrossword;

        public override void InstallBindings() { }

        public void SaveConfigs()
        {
            MockConfigProvider mockConfigProvider = new();
            mockConfigProvider.SetGameConfig(_gameConfig);
            mockConfigProvider.SetCrosswordConfig(_crosswordConfig);

            SaveAndLoadConfigService service = new(mockConfigProvider);
            service.Save();
            
            LoadConfigs();
        }

        public void LoadConfigs()
        {
            if (Application.isPlaying)
                LoadRunTimeConfigs();
            else
                LoadInEditorConfigs();
        }

        private void LoadRunTimeConfigs()
        {
            IConfigProvider configProvider = Container.TryResolve<IConfigProvider>();

            if (configProvider == null)
                return;

            configProvider.LoadConfigs();
            _gameConfig = configProvider.GetGameConfig();
            _crosswordConfig = configProvider.GetCrosswordConfig();
        }

        private void LoadInEditorConfigs()
        {
            ConfigProvider configProvider = new();
            _gameConfig = configProvider.GetGameConfig();
            _crosswordConfig = configProvider.GetCrosswordConfig();
        }
    }
}