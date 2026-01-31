
using UnityEngine;

[CreateAssetMenu(fileName = "new EmotionItem", menuName = "Emotion/EmotionItem")]
public class EmotionItem : ScriptableObject
{
  [SerializeField] private int value;
  [Range(0f, 1f)]
  [SerializeField] private float width;

  public  int Value => value;
  public  float Width => width;
  public  EmotionDecisionStrategy DecisionStrategy => EmotionDecisionStrategy.PushFurther;
}
