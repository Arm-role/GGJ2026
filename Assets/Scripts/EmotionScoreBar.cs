
using UnityEngine;

public class EmotionScoreBar : MonoBehaviour
{
  [Header("References")]
  [SerializeField] private RectTransform bar;        // ตัว bar
  [SerializeField] private RectTransform triangle;   // สามเหลี่ยม

  [Header("Runtime")]
  [SerializeField] private int currentScore;

  private EmotionDifficulty difficulty;

  private float barWidth;

  private void Awake()
  {
    barWidth = bar.rect.width;
  }

  public void Setup(EmotionDifficulty dif)
  {
    difficulty = dif;
  }

  public void SetScore(int score)
  {
    currentScore = score;
    UpdateTriangle();
  }

  private void UpdateTriangle()
  {
    float normalized = Mathf.InverseLerp(
        difficulty.MissPenaltyScore,
        difficulty.RequestScore,
        currentScore
    );

    float xPos = Mathf.Lerp(
        -barWidth * 0.5f,
        barWidth * 0.5f,
        normalized
    );

    Vector2 pos = triangle.anchoredPosition;
    pos.x = xPos;
    triangle.anchoredPosition = pos;
  }
}
