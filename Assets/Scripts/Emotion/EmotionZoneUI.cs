using UnityEngine;

public class EmotionZoneUI : MonoBehaviour
{
  [SerializeField] private RectTransform rect;

  public void Setup(EmotionRuntimeItem item, SliderBar bar)
  {
    rect.anchoredPosition =
      new Vector2(bar.ToAnchoredX(item.Center), 0);

    rect.SetSizeWithCurrentAnchors(
      RectTransform.Axis.Horizontal,
      bar.WidthPx * item.Data.Width
    );
  }
}
