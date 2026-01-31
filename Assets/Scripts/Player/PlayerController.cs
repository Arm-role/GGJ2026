using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float speed = 3;
  [SerializeField] private float detectRadius = 1;
  [SerializeField] private PlayerAnimation playerAnimation;
  [SerializeField] private EmotionController emotionController;

  private Rigidbody2D rb;
  private Vector2 moveInput;
  private ITargetable target = null;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();

    rb.interpolation = RigidbodyInterpolation2D.Interpolate;

    emotionController.OnComplete += OnComplete;
    emotionController.OnFail += OnFail;
  }

  private void OnDestroy()
  {
    emotionController.OnComplete -= OnComplete;
    emotionController.OnFail -= OnFail;
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
      emotionController.Setup(target.GetEmotionType);
    }
  }

  private void FixedUpdate()
  {
    rb.velocity = moveInput * speed;

    var hits = Physics2D.OverlapCircleAll(
      rb.position,
      detectRadius
    );

    target = null;
    foreach (var hit in hits)
    {
      if (hit.TryGetComponent(out ITargetable t))
      {
        target = t;
        target.OnInteraction();
        break;
      }
    }
  }

  private void OnComplete(int score, EmotionType from, EmotionType to)
  {
    if (target == null) return;
    target.SetEmotionType(to);
  }

  private void OnFail()
  {
    Debug.Log("Target Emotion Damage");
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, detectRadius);
  }
}