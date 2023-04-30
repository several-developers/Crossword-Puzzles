using Core.Events;
using Core.Infrastructure.Providers.Global.Config;
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
        private void Construct(ICrosswordService crosswordService, IConfigProvider configProvider,
            ISaveAndLoadService saveAndLoadService)
        {
            _crosswordService = crosswordService;
            _saveAndLoadService = saveAndLoadService;
            _crosswordViewLogic = new(crosswordService, configProvider, _cellItemPrefab, _cellsContainer,
                coroutineRunner: this, _gridLayoutGroup, _playerIF, _errorSoundAs, _crosswordViewShakeAnimation);
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

        [SerializeField]
        private AudioSource _errorSoundAs;

        [SerializeField]
        private CrosswordViewShakeAnimation _crosswordViewShakeAnimation;
        
        private ICrosswordService _crosswordService;
        private ISaveAndLoadService _saveAndLoadService;
        private CrosswordViewLogic _crosswordViewLogic;

        private void Awake()
        {
            _okButton.onClick.AddListener(OnOkClicked);
            DebugEvents.OnCreateCrossword += OnCreateCrossword;
        }

        private void Start() =>
            _crosswordViewLogic.CreateCrossword();

        private void OnDestroy()
        {
            _okButton.onClick.RemoveListener(OnOkClicked);
            DebugEvents.OnCreateCrossword -= OnCreateCrossword;
        }

        private void OnOkClicked() =>
            _crosswordViewLogic.ClickLogic();

        private void OnCreateCrossword() => DebugCreateCrossword();
        
        [ContextMenu("Create Crossword")]
        private void DebugCreateCrossword()
        {
            _saveAndLoadService.Load();
            _crosswordService.UpdateAnswersData();
            _crosswordViewLogic.CreateCrossword();
        }

        [ContextMenu("Start Shake Animation")]
        private void DebugStartShakeAnimation() =>
            _crosswordViewShakeAnimation.StartAnimation();
    }
}