using System.Collections.Generic;
using UnityEngine;

public class EmotionTurnGizmo : MonoBehaviour
{
  [SerializeField] private SliderBar bar;
  [SerializeField] private EmotionController controller;

  private void OnDrawGizmos()
  {
    if (controller == null || bar == null) return;

    var turnField = typeof(EmotionController)
      .GetField("turn", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

    if (turnField == null) return;

    var turn = turnField.GetValue(controller) as EmotionTurn;
    if (turn == null) return;

    // ---------- DRAW BAR ----------
    DrawBar();

    // ---------- DRAW SPAWN SPACES ----------
#if UNITY_EDITOR
    foreach (var space in turn.Debug_GetEmptySpaces())
    {
      DrawSpawnSpace(space.min, space.max);
    }
#endif

    // ---------- DRAW ITEMS ----------
    foreach (var item in turn.AllItems)
    {
      DrawItem(item);
    }
  }


  private void DrawItem(EmotionRuntimeItem item)
  {
    float xMin = bar.ToPixel(item.Min);
    float xMax = bar.ToPixel(item.Max);
    float xCenter = bar.ToPixel(item.Center);

    Vector3 basePos = bar.transform.position;

    Vector3 min = basePos + Vector3.right * xMin;
    Vector3 max = basePos + Vector3.right * xMax;
    Vector3 center = basePos + Vector3.right * xCenter;

    Gizmos.color = item.Data.DecisionStrategy == EmotionDecisionStrategy.AcceptDeal
      ? Color.green
      : Color.cyan;

    Gizmos.DrawLine(min, max);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(center, 4f);
  }

  private void DrawBar()
  {
    float half = bar.WidthPx * 0.5f;
    Vector3 basePos = bar.transform.position;

    Gizmos.color = Color.white;
    Gizmos.DrawLine(
      basePos + Vector3.right * -half,
      basePos + Vector3.right * half
    );
  }

  private void DrawSpawnSpace(float min01, float max01)
  {
    float xMin = bar.ToPixel(min01);
    float xMax = bar.ToPixel(max01);

    Vector3 basePos = bar.transform.position;
    Vector3 left = basePos + Vector3.right * xMin;
    Vector3 right = basePos + Vector3.right * xMax;

    Gizmos.color = new Color(0f, 1f, 1f, 0.25f);

    Gizmos.DrawLine(left, right);

    // ความหนา
    Gizmos.DrawLine(left + Vector3.up * 6f, right + Vector3.up * 6f);
    Gizmos.DrawLine(left - Vector3.up * 6f, right - Vector3.up * 6f);
  }

}