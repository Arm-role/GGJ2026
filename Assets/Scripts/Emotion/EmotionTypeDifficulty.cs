
using System;
using UnityEngine;

[CreateAssetMenu(
  fileName = "EmotionTypeDifficulty",
  menuName = "Emotion/EmotionTypeDifficulty"
)]
public class EmotionTypeDifficulty : ScriptableObject
{
  [Header("Spawn Items")]
  [SerializeField] private EmotionType emotionType;
  [SerializeField] private ETD easy;
  [SerializeField] private ETD normal;
  [SerializeField] private ETD hard;
  [SerializeField] private ETD chaos;

  public EmotionType Type => emotionType;

  public ETD Easy => easy;
  public ETD Normal => normal;
  public ETD Hard => hard;
  public ETD Chaos => chaos;
}


[Serializable]
public class ETD
{
  public EmotionType EmotionType;
  public EmotionDifficulty Difficulty;
}