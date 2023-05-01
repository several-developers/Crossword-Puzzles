using Core.Crossword;
using Core.Enums;

namespace Core.Infrastructure.Services.GameScene
{
    public interface ICrosswordService
    {
        bool TryFindMatchWord(int hashCode);
        AnswerData GetAnswerData(int hashCode);
        AnswerData GetAnswerData(int column, int row, Direction direction);
        void UpdateAnswersData();
    }
}