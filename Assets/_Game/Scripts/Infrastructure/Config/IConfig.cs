namespace Core.Infrastructure.Config
{
    public interface IConfig
    {
        string GetResourcesPath();
        string GetFullPath();
    }
}