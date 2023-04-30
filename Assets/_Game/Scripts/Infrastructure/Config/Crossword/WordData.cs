using System;
using UnityEngine;

namespace Core.Infrastructure.Config.Crossword
{
    [Serializable]
    public class WordData
    {
        public string answer;
        
        [Tooltip("Can only be 'across' or 'down'.")]
        public string direction;
        
        [Range(0, 9)]
        public int column;
        
        [Range(0, 9)]
        public int row;
    }
}