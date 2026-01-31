
using UnityEngine;

public class SliderBar : MonoBehaviour
{
  [SerializeField] private RectTransform bar;

  public float Min => 0f;
  public float Max => 1f;

  public float WidthPx => bar.rect.width;

  public float ToPixel(float t)
  {
    return Mathf.Lerp(
      -WidthPx * 0.5f,
       WidthPx * 0.5f,
       t
    );
  }

  public float ToNormalized(float x)
  {
    return Mathf.InverseLerp(
      -WidthPx * 0.5f,
       WidthPx * 0.5f,
       x
    );
  }

  public float ToAnchoredX(float t)
  {
    return Mathf.Lerp(
      -WidthPx * 0.5f,
       WidthPx * 0.5f,
       t
    );
  }
}
