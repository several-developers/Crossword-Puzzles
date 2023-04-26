using System;

namespace Core.Infrastructure.Data.Crossword
{
    [Serializable]
    public class WordData
    {
        public string answer;
        public string direction;
        public int column;
        public int row;
    }
}