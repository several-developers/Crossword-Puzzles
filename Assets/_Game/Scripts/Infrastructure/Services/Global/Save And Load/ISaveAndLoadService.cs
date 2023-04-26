namespace Core.Infrastructure.Services.Global
{
    public interface ISaveAndLoadService
    {
        void SaveGameData();
        void LoadGameData();
    }
}