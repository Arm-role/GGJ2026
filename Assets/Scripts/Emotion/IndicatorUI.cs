using System;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorUI : MonoBehaviour
{
  [SerializeField] private RectTransform indicator;
  [SerializeField] private SliderBar bar;
  [SerializeField] private float speed = 1f;

  public float Direction { get; private set; } = 1f;
  public Action<float> OnPress;

  private float t;
  private float delay;
  private bool isRunning = false;
  public bool IsRunning => isRunning;
  
  [SerializeField] private Button pressButton;

  private void Start()
  {
    pressButton.onClick.AddListener(OnPressed);
  }

  void Update()
  {
    if (GameStateManager.Current != GameState.HugglingUI)
      return;
    
    if (Input.GetKeyDown(KeyCode.Space))
      OnPressed();

    if (!isRunning)
      return;

    // ---------- RUNNING ----------
    if (delay > 0)
    {
      delay -= Time.deltaTime;
      return;
    }

    t += Direction * speed * Time.deltaTime;
    t = Mathf.Clamp01(t);

    if (t == 1f) Direction = -1f;
    else if (t == 0f) Direction = 1f;

    indicator.anchoredPosition =
      new Vector2(bar.ToAnchoredX(t), 0);
  }

  private void OnPressed()
  {
    if (!isRunning)
    {
      isRunning = true;
      delay = 0.2f;
      return;
    }

    delay = 0.4f;
    Debug.Log("Pointer : " + t);
    OnPress?.Invoke(t);
  }

  public void ResetIndicator()
  {
    isRunning = false;
    t = 0f;
    Direction = 1f;
    delay = 0f;

    indicator.anchoredPosition = new Vector2(-400f, 0);
  }

  public (float min, float max) GetBarRange01()
  {
    return (bar.Min, bar.Max);
  }

  private void OnDrawGizmos()
  {
    if (!bar) return;

    RectTransform barRect = bar.GetComponent<RectTransform>();
    if (!barRect) return;

    // ใช้ local space ของ bar
    Gizmos.matrix = barRect.localToWorldMatrix;

    float x = bar.ToAnchoredX(t);

    Gizmos.color = Color.magenta;
    Gizmos.DrawLine(
      new Vector3(x, -30f, 0),
      new Vector3(x, 30f, 0)
    );

    // ---------- center reference ----------
    Gizmos.color = Color.green;
    Gizmos.DrawLine(
      Vector3.zero + Vector3.down * 20f,
      Vector3.zero + Vector3.up * 20f
    );
  }
}