using System.Collections.Generic;
using Core.Crossword;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.GridView
{
    public class GridBuilder
    {
        public GridBuilder(GridLayoutGroup gridLayoutGroup, CellItemUI cellItemPrefab, Transform container)
        {
            _gridLayoutGroup = gridLayoutGroup;
            _cellItemPrefab = cellItemPrefab;
            _container = container;
            _cellItemsList = new(capacity: 100);
        }

        private readonly GridLayoutGroup _gridLayoutGroup;
        private readonly CellItemUI _cellItemPrefab;
        private readonly Transform _container;
        private readonly List<CellItemUI> _cellItemsList;

        public void BuildGrid(GridMeta gridMeta)
        {
            RemoveCellItems();
            
            GridSize gridSize = gridMeta.GridSize;
            int columns = gridSize.Columns;
            int rows = gridSize.Rows;
            int iterations = columns * rows;
            bool isGrayColor = false;
            
            _gridLayoutGroup.constraintCount = columns;

            for (int i = 0; i < iterations; i++)
            {
                CellItemUI cellItemInstance = Object.Instantiate(_cellItemPrefab, _container);
                _cellItemsList.Add(cellItemInstance);
                cellItemInstance.SetColor(isGrayColor);

                bool isNewRow = (i + 1) % columns == 0;
                
                if (isNewRow)
                    continue;

                isGrayColor = !isGrayColor;
            }
        }

        private void RemoveCellItems()
        {
            foreach (CellItemUI cellItemInstance in _cellItemsList)
                Object.Destroy(cellItemInstance.gameObject);
            
            _cellItemsList.Clear();
        }
    }
}