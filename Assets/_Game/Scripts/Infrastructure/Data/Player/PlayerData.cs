using System;
using Core.Crossword;

namespace Core.Infrastructure.Data.Player
{
    [Serializable]
    public class PlayerData
    {
        public PlayerData() =>
            errorBehaviour = new(shakeOnError: true, playSoundOnError: false);

        public ErrorBehaviour errorBehaviour;
    }
}