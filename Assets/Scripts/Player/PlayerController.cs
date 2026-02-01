using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float speed = 3;
  [SerializeField] private float detectRadius = 1;
  [SerializeField] private PlayerAnimation playerAnimation;
  [SerializeField] private EmotionController emotionController;

  private Rigidbody2D rb;
  private Vector2 moveInput;
  private ITargetable target;
  private ITargetable previousTarget;
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.interpolation = RigidbodyInterpolation2D.Interpolate;
  }

  private void Update()
  {
    if (GameStateManager.Current != GameState.Gameplay)
    {
      moveInput = Vector2.zero;
      return;
    }

    moveInput = new Vector2(
      Input.GetAxisRaw("Horizontal"),
      Input.GetAxisRaw("Vertical")
    );

    playerAnimation.ApplyFlip(moveInput);

    if (Input.GetKeyDown(KeyCode.E) && target != null)
    {
      target.OnInteraction();
      emotionController.Setup(target.GetEmotionType, target);
    }
  }

  private void FixedUpdate()
  {
    rb.velocity = moveInput * speed;

    var hits = Physics2D.OverlapCircleAll(rb.position, detectRadius);

    ITargetable closestTarget = null;
    float closestDistance = float.MaxValue;

    foreach (var hit in hits)
    {
      if (hit.TryGetComponent(out ITargetable t))
      {
        float distance = Vector2.Distance(rb.position, hit.transform.position);

        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestTarget = t;
        }
      }
    }

    if (previousTarget != closestTarget)
    {
      previousTarget?.OnUnfocus();
      closestTarget?.OnFocus();
      previousTarget = closestTarget;
    }

    target = closestTarget;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, detectRadius);
  }
}