public interface ITargetable
{
  void OnInteraction();

  EmotionType GetEmotionType { get; }

  void SetEmotionType(EmotionType emotionType);
}