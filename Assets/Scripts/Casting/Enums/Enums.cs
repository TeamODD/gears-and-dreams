namespace GearsAndDreams.Casting.Enums
{
    public enum GameState
    {
        Ready,
        Playing,
        Evaluating,
        Finished
    }

    public enum AccuracyLevel
    {
        Perfect,    // ±10% 이하
        Good,       // ±10% ~ ±20%
        Fair,       // ±20% ~ ±30%
        Poor,       // ±30% ~ ±40%
        Miss        // ±40% 초과
    }
}