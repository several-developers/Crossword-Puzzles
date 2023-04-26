using Core.Crossword;
using Core.Infrastructure.Providers.GameScene;
using Core.UI.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.UI.GridView
{
    public class GridViewUI : MonoBehaviour
    {
        [Inject]
        private void Construct(ICrosswordProvider crosswordProvider) =>
            _crosswordProvider = crosswordProvider;

        [SerializeField]
        private GridMeta _gridMeta;

        [SerializeField]
        private Transform _cellContainer;

        [SerializeField]
        private CellItemUI _cellItemPrefab;

        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;

        private ICrosswordProvider _crosswordProvider;
        private GridBuilder _gridBuilder;
        private LayoutFixHelper _layoutFixHelper;

        private void Awake()
        {
            _gridBuilder = new(_gridLayoutGroup, _cellItemPrefab, _cellContainer);
            _layoutFixHelper = new(coroutineRunner: this, _gridLayoutGroup);
            _gridLayoutGroup.enabled = false;
        }

        private void Start() => BuildGrid();

        [ContextMenu("Build Grid")]
        private void BuildGrid()
        {
            _gridBuilder.BuildGrid(_gridMeta);
            _layoutFixHelper.FixLayout();
            //UpdateGridView();
        }

        private void UpdateGridView()
        {
            
        }
    }
}
