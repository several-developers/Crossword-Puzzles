using System;
using UnityEngine;

namespace Core.GameConfig
{
    [Serializable]
    public class WordConfigData
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