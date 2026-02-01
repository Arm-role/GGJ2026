using UnityEngine;

[CreateAssetMenu(menuName = "Game/Emotion Transition")]
public class EmotionTransitionSO : ScriptableObject
{
  public EmotionType from;
  public EmotionType to;

  [Range(0f, 3f)]
  public float difficulty = 1f;

  [Range(-2f, 2f)]
  public float humanity = 0f;
}

