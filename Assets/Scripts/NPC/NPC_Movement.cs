using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPC_Movement : MonoBehaviour
{
  [Header("Movement")]
  [SerializeField] private float moveSpeed = 2f;
  [SerializeField] private float walkDistance = 3f;

  private Rigidbody2D rb;
  private Vector2 startPosition;
  private int direction = 1; // 1 = ขวา, -1 = ซ้าย
  private SpriteRenderer spriteRenderer;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    startPosition = transform.position;
  }

  private void FixedUpdate()
  {
    Patrol();
  }

  private void Patrol()
  {
    rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

    float distanceFromStart = transform.position.x - startPosition.x;

    if (direction == 1 && distanceFromStart >= walkDistance)
    {
      ChangeDirection();
    }
    else if (direction == -1 && distanceFromStart <= -walkDistance)
    {
      ChangeDirection();
    }
  }

  private void ChangeDirection()
  {
    direction *= -1;

    if (spriteRenderer != null)
      spriteRenderer.flipX = direction < 0;
  }
}