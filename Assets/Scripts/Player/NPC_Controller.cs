using UnityEngine;

public class NPC_Controller : MonoBehaviour, ITargetable
{
  [SerializeField] private EmotionType emotionType;

  public void OnInteraction()
  {
  }

  public EmotionType GetEmotionType => emotionType;

  public void SetEmotionType(EmotionType emotionType)
  {
    this.emotionType = emotionType;
  }
}
