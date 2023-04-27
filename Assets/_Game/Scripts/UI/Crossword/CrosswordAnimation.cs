using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Crossword;

namespace Core.UI.Crossword
{
    public class CrosswordAnimation
    {
        public CrosswordAnimation(CrosswordBuilder crosswordBuilder) =>
            _crosswordBuilder = crosswordBuilder;

        private const float Delay = 0.11f;
        
        private readonly CrosswordBuilder _crosswordBuilder;

        public async void StartAnimation(List<GridPosition> gridPositionsList)
        {
            int delay = (int)(Delay * 1000);
            
            foreach (GridPosition gridPosition in gridPositionsList)
            {
                if (!_crosswordBuilder.TryGetCellItem(gridPosition, out CellItemUI cellItem))
                    continue;
                
                if (cellItem.HasRotated)
                    continue;
                
                cellItem.StartRotateAnimation();
                await Task.Delay(delay);
            }
        }
    }
}