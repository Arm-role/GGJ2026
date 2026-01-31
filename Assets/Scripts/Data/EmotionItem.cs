
using UnityEngine;

[CreateAssetMenu(fileName = "new EmotionItem", menuName = "Emotion/EmotionItem")]
public class EmotionItem : SliderItem
{
  [SerializeField] private int value;
  [Range(0f, 1f)]
  [SerializeField] private float width;

  public override int Value => value;
  public override float Width => width;
  public override EmotionDecisionStrategy DecisionStrategy => EmotionDecisionStrategy.PushFurther;
}
