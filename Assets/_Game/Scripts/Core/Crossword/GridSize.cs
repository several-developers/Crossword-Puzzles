using System;
using UnityEngine;

namespace Core.Crossword
{
    [Serializable]
    public struct GridSize
    {
        public GridSize(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
        }

        [SerializeField]
        private int _columns;

        [SerializeField]
        private int _rows;

        public int Columns => _columns;
        public int Rows => _rows;
    }
}