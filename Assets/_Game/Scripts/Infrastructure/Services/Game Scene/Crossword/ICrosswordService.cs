namespace Core.Infrastructure.Services.GameScene
{
    public interface ICrosswordService
    {
        bool TryFindMatchWord();
    }

    public class CrosswordService : ICrosswordService
    {
        public bool TryFindMatchWord()
        {
            return false;
        }
    }
}