using UnityEngine;

public class SliderBarGizmo : MonoBehaviour
{
  [SerializeField] private SliderBar bar;

  private void OnDrawGizmos()
  {
    if (!bar) return;

    var rt = bar.GetComponent<RectTransform>();
    if (!rt) return;

    Vector3 center = rt.position;
    float half = bar.WidthPx * 0.5f * rt.lossyScale.x;

    Vector3 left = center + Vector3.left * half;
    Vector3 right = center + Vector3.right * half;

    // bar
    Gizmos.color = Color.white;
    Gizmos.DrawLine(left, right);

    // center
    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(center, 6f);
  }
}
