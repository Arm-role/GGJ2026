
using UnityEngine;

[CreateAssetMenu(fileName = "new AceptItem", menuName = "Emotion/AceptItem")]
public class AceptItem : SliderItem
{
  [Range(0f, 0.5f)]
  [SerializeField] private float width;

  public override int Value => 0;
  public override float Width => width;
  public override EmotionDecisionStrategy DecisionStrategy => EmotionDecisionStrategy.AcceptDeal;
}