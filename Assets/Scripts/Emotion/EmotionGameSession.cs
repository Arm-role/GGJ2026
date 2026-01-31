public class EmotionGameSession
{
  public EmotionDifficulty Difficulty { get; }
  public EmotionType InputEmotion { get; }
  public EmotionType OutputEmotion { get; }

  public int Score { get; private set; }

  public EmotionGameSession(
    EmotionDifficulty difficulty,
    EmotionType input,
    EmotionType output)
  {
    Difficulty = difficulty;
    InputEmotion = input;
    OutputEmotion = output;
    Score = 0;
  }

  public void ApplyResult(bool hit, int gain, int missPenalty)
  {
    Score += hit ? gain : missPenalty;
  }

  public bool IsCompleted => Score >= Difficulty.RequestScore;
  public bool IsFailed => Score <= Difficulty.MissPenaltyScore;
}
