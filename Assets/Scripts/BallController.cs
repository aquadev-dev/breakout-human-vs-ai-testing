using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialForce = 10f;
    public bool canBreakBricks = true;
    public bool isLaunched = false;
    private Rigidbody2D rb;
    private Vector3 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (!isLaunched && UnityEngine.InputSystem.InputSystem.actions.FindAction("Launch").WasPressedThisFrame())
        {
            Launch();
        }
    }

    public void Launch()
    {
        if (isLaunched) return;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * initialForce;
        isLaunched = true;
        canBreakBricks = true; // Enable breaking bricks on launch
    }

    public void ResetBall(Transform paddle)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        isLaunched = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = paddle;
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If ball hits paddle, it can now break bricks (for Cavity mode)
        if (collision.gameObject.GetComponent<PaddleController>() != null || collision.gameObject.CompareTag("Paddle"))
        {
            if (!canBreakBricks)
            {
                Debug.Log($"Ball '{gameObject.name}' activated! Now breaking bricks.");
                canBreakBricks = true;
            }
        }

        // Ensure ball maintains speed
        rb.linearVelocity = rb.linearVelocity.normalized * initialForce;
    }
}
