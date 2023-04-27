using System;

namespace Core.Events
{
    public static class DebugEvents
    {
        public static event Action OnCreateCrossword;

        public static void SendCreateCrossword() => OnCreateCrossword?.Invoke();
    }
}