using Core.Infrastructure.Data;
using Core.Infrastructure.Services.Global;
using UnityEngine;
using Zenject;

namespace Core.Crossword
{
    public class CrosswordEditor : MonoBehaviour
    {
        [Inject]
        private void Construct(IGameDataService gameDataService, ISaveAndLoadService saveAndLoadService)
        {
            _gameDataService = gameDataService;
            _saveAndLoadService = saveAndLoadService;
        }

        private IGameDataService _gameDataService;
        private ISaveAndLoadService _saveAndLoadService;

        [SerializeField]
        private GameData _gameData;

        private void Start() => LoadGameData();

        [ContextMenu("Save Game Data")]
        private void SaveGameData()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                SaveAndLoadService saveAndLoadService = new(_gameData);
                saveAndLoadService.SaveGameData();
                return;
            }
#endif
            
            _saveAndLoadService.SaveGameData();
        }

        [ContextMenu("Load Game Data")]
        private void LoadGameData()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                GameDataService gameDataService = new();
                SaveAndLoadService saveAndLoadService = new(gameDataService);
                saveAndLoadService.LoadGameData();
                _gameData = gameDataService.GetGameData();
                return;
            }
#endif

            _gameData = _gameDataService.GetGameData();
        }
    }
}