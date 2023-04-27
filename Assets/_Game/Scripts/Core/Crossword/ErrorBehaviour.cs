using System;

namespace Core.Crossword
{
    [Serializable]
    public struct ErrorBehaviour
    {
        public ErrorBehaviour(bool shakeOnError, bool playSoundOnError)
        {
            this.shakeOnError = shakeOnError;
            this.playSoundOnError = playSoundOnError;
        }

        public bool shakeOnError;
        public bool playSoundOnError;
    }
}