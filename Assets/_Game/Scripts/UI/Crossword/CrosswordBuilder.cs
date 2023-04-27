using System.Collections.Generic;
using System.Linq;
using Core.Crossword;
using UnityEngine;

namespace Core.UI.Crossword
{
    public class CrosswordBuilder
    {
        public CrosswordBuilder(CellItemUI cellItemPrefab, Transform cellsContainer)
        {
            _cellItemPrefab = cellItemPrefab;
            _cellsContainer = cellsContainer;
            _cellItemsDictionary = new(capacity: Columns * Rows);
        }
        
        private const int Columns = 10;
        private const int Rows = 10;
        
        private readonly CellItemUI _cellItemPrefab;
        private readonly Transform _cellsContainer;
        private readonly Dictionary<GridPosition, CellItemUI> _cellItemsDictionary;

        public void BuildCrossword()
        {
            RemoveCellItems();
            
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    CellItemUI cellItemInstance = Object.Instantiate(_cellItemPrefab, _cellsContainer);
                    GridPosition gridPosition = new(column, row);
                    cellItemInstance.SetColor(isGray: true);
                    cellItemInstance.HideChar();
                    _cellItemsDictionary.Add(gridPosition, cellItemInstance);
                }
            }
        }

        public bool TryGetCellItem(GridPosition gridPosition, out CellItemUI cellItem) =>
            _cellItemsDictionary.TryGetValue(gridPosition, out cellItem);

        private void RemoveCellItems()
        {
            List<CellItemUI> cellItemsList = _cellItemsDictionary.Values.ToList();
            
            foreach (CellItemUI cellItemInstance in cellItemsList)
                Object.Destroy(cellItemInstance.gameObject);
            
            _cellItemsDictionary.Clear();
        }
    }
}