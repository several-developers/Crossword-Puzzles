using Core.Enums;

namespace Core.Crossword
{
    public struct AnswerData
    {
        public AnswerData(Direction direction, int column, int row, int length)
        {
            Direction = direction;
            Column = column;
            Row = row;
            Length = length;
        }
        
        public Direction Direction { get; }
        public int Column { get; }
        public int Row { get; }
        public int Length { get; }
    }
}