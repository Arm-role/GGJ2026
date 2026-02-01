public readonly struct EmotionContext
{
  public readonly EmotionTypeResutl Result; 
  public readonly int Score;
  public readonly EmotionType From;
  public readonly EmotionType To;
  public readonly ITargetable Target;
  
  public EmotionContext(
    EmotionTypeResutl result,
    int score, 
    EmotionType from,
    EmotionType to,
    ITargetable target)
  {
    Result = result;  
    Score = score;
    From = from;
    To = to;
    Target = target;
  }
}

public enum EmotionTypeResutl
{
  None,
  Success,
  Failure
}