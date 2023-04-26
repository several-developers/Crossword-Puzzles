using Core.UI.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Crossword
{
    public class CrosswordViewUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _playerIF;
        
        [SerializeField]
        private Transform _cellsContainer;

        [SerializeField]
        private CellItemUI _cellItemPrefab;

        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;

        private CrosswordBuilder _crosswordBuilder;
        private LayoutFixHelper _layoutFixHelper;
        
        private void Awake()
        {
            _playerIF.onValueChanged.AddListener(OnInputFieldValueChanged);

            _crosswordBuilder = new(_cellItemPrefab, _cellsContainer);
            _layoutFixHelper = new(this, _gridLayoutGroup);
        }

        private void Start() => CreateCrossword();

        private void OnDestroy() =>
            _playerIF.onValueChanged.RemoveListener(OnInputFieldValueChanged);

        private void CreateCrossword()
        {
            _crosswordBuilder.BuildCrossword();
            _layoutFixHelper.FixLayout();
        }
        
        private void OnInputFieldValueChanged(string text)
        {
            
        }
    }
}