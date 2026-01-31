using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  [SerializeField]
  private Animator _animator;

  public void ApplyFlip(Vector2 moveDirection)
  {
    if (moveDirection == Vector2.zero) return;

    Vector3 scale = _animator.transform.localScale;

    if (moveDirection.x > 0)
      _animator.transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
    else if (moveDirection.x < 0)
      _animator.transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
  }
}
