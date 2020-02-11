namespace FlappyBird
{
    public static class GlobalGameState
    {
        public static bool IsRetry { get; set; } = false;
        public static Phase CurrentPhase { get; set; }

        public enum Phase
        {
            WaitingForPlayer,
            Playing,
            GameOver
        }
    }
}