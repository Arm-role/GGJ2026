using System;

[Serializable]
public struct EmotionTransitionData
{
  public EmotionType from;
  public EmotionType to;
  public float difficulty;   // ความยาก
  public float humanity;     // ผลต่อมนุษย์ (+/-)
}