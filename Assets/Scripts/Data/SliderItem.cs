
using UnityEngine;

public abstract class SliderItem : ScriptableObject
{
  public virtual int Value { get; set; }
  public virtual float Width { get; set; }
  public virtual EmotionDecisionStrategy DecisionStrategy { get; set; }
}
public enum EmotionDecisionStrategy
{
  AcceptDeal,     // ยอมรับ → ได้คะแนนตามที่สะสม
  PushFurther,    // ดันต่อ → เสี่ยง (อาจ + หรือ -)
}
