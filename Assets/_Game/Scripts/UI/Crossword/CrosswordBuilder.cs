using System.Collections.Generic;
using UnityEngine;

namespace Core.UI.Crossword
{
    public class CrosswordBuilder
    {
        public CrosswordBuilder(CellItemUI cellItemPrefab, Transform cellsContainer)
        {
            _cellItemPrefab = cellItemPrefab;
            _cellsContainer = cellsContainer;
            _cellItemsList = new(capacity: 100);
        }
        
        private const int Columns = 10;
        private const int Rows = 10;
        
        private readonly CellItemUI _cellItemPrefab;
        private readonly Transform _cellsContainer;
        private readonly List<CellItemUI> _cellItemsList;
        
        public void BuildCrossword()
        {
            RemoveCellItems();
            
            int iterations = Columns * Rows;
            bool isGrayColor = false;

            for (int i = 0; i < iterations; i++)
            {
                CellItemUI cellItemInstance = Object.Instantiate(_cellItemPrefab, _cellsContainer);
                cellItemInstance.SetColor(isGrayColor);
                _cellItemsList.Add(cellItemInstance);
                    
                bool isNewRow = (i + 1) % Columns == 0;
                
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