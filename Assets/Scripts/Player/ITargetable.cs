public interface ITargetable
{
  void OnInteraction();
  void OnUnInteraction();
  
  void OnFocus();
  void OnUnfocus();
  EmotionType GetEmotionType { get; }

  void SetEmotionType(EmotionType emotionType);
}