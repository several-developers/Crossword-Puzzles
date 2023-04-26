using UnityEngine;

namespace Core.Crossword
{
    [CreateAssetMenu]
    public class GridMeta : ScriptableObject
    {
        [SerializeField]
        private GridSize _gridSize = new(columns: 10, rows: 10);

        public GridSize GridSize => _gridSize;
    }
}
