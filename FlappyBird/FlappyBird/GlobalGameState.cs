namespace FlappyBird
{
    public static class GlobalGameState
    {
        public static Phase CurrentPhase { get; set; }
        public static bool IsRetry { get; set; }
        public static int Score { get; set; }

        public enum Phase
        {
            WaitingForPlayer,
            Playing,
            GameOver
        }
    }
}