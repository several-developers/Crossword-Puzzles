using Core.Events;
using Core.Infrastructure.Services.GameScene;
using Core.Infrastructure.Services.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.Crossword
{
    public class CrosswordViewUI : MonoBehaviour
    {
        [Inject]
        private void Construct(ICrosswordService crosswordService, IGameDataService gameDataService,
            ISaveAndLoadService saveAndLoadService)
        {
            _crosswordService = crosswordService;
            _saveAndLoadService = saveAndLoadService;
            _crosswordViewLogic = new(crosswordService, gameDataService, _cellItemPrefab, _cellsContainer,
                coroutineRunner: this, _gridLayoutGroup, _playerIF);
        }

        [SerializeField]
        private TMP_InputField _playerIF;

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Transform _cellsContainer;

        [SerializeField]
        private CellItemUI _cellItemPrefab;

        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;
        
        private ICrosswordService _crosswordService;
        private ISaveAndLoadService _saveAndLoadService;
        private CrosswordViewLogic _crosswordViewLogic;

        private void Awake()
        {
            _okButton.onClick.AddListener(OnOkClicked);
            DebugEvents.OnCreateCrossword += OnCreateCrossword;
        }

        private void Start() => CreateCrossword();

        private void OnDestroy()
        {
            _okButton.onClick.RemoveListener(OnOkClicked);
            DebugEvents.OnCreateCrossword -= OnCreateCrossword;
        }

        private void CreateCrossword() =>
            _crosswordViewLogic.CreateCrossword();

        private void OnOkClicked() =>
            _crosswordViewLogic.ClickLogic();

        [ContextMenu("Create Crossword")]
        private void DebugCreateCrossword()
        {
            _saveAndLoadService.LoadGameData();
            _crosswordService.UpdateAnswersData();
            CreateCrossword();
        }

        private void OnCreateCrossword() => DebugCreateCrossword();
    }
}