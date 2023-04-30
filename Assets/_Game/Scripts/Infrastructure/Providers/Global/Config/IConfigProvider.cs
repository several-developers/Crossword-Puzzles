using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;

namespace Core.Infrastructure.Providers.Global.Config
{
    public interface IConfigProvider
    {
        GameConfig GetGameConfig();
        CrosswordConfig GetCrosswordConfig();
        void LoadConfigs();
    }
}