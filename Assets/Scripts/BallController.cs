using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialForce = 10f;
    private Rigidbody2D rb;
    private bool isLaunched = false;
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
        transform.parent = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * initialForce;
        isLaunched = true;
    }

    public void ResetBall(Transform paddle)
    {
        isLaunched = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = paddle;
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensure ball maintains speed
        rb.linearVelocity = rb.linearVelocity.normalized * initialForce;
    }
}
