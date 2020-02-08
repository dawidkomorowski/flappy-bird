namespace FlappyBird
{
    public static class GlobalGameState
    {
        public static Phase CurrentPhase { get; set; }
        public enum Phase
        {
            WaitingForPlayer,
            Playing,
            GameOver
        }
    }
}