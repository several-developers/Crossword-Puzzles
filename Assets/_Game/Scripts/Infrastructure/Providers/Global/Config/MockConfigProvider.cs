using Core.Infrastructure.Config;
using Core.Infrastructure.Config.Crossword;

namespace Core.Infrastructure.Providers.Global.Config
{
    public class MockConfigProvider : IConfigProvider
    {
        private GameConfig _gameConfig;
        private CrosswordConfig _crosswordConfig;
        
        public GameConfig GetGameConfig() => _gameConfig;

        public CrosswordConfig GetCrosswordConfig() => _crosswordConfig;

        public void SetGameConfig(GameConfig gameConfig) =>
            _gameConfig = gameConfig;

        public void SetCrosswordConfig(CrosswordConfig crosswordConfig) =>
            _crosswordConfig = crosswordConfig;

        public void LoadConfigs() { }
    }
}