using UnityEngine;

public struct EmotionRuntimeItem
{
  public EmotionItem Data;
  public float Min;
  public float Max;
  public float Center;

  public EmotionRuntimeItem(EmotionItem data, float center)
  {
    Data = data;
    float half = data.Width * 0.5f;
    Min = center - half;
    Max = center + half;
    Center = center;
  }

  public bool Contains(float t)
      => t >= Min && t <= Max;

  public bool Overlaps(EmotionRuntimeItem other)
      => !(Max <= other.Min || Min >= other.Max);

  public bool OverlapsWithPadding(EmotionRuntimeItem other, float padding)
  {
    return !(Max <= other.Min - padding || Min >= other.Max + padding);
  }
}
