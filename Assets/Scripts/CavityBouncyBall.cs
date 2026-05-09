using UnityEngine;

public class CavityBouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;

    public GameObject bouncyBallInstance;
    private bool canBreakBricks = false;

    public AudioSource ballBounceSource;
    public AudioClip ballBounceClip;
    public AudioClip wallBounceClip;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Brick":
                if (canBreakBricks) 
                {
                    Destroy(collision.gameObject);
                    bouncyBallInstance.GetComponent<BouncyBall>().updateScore();
                    bouncyBallInstance.GetComponent<BouncyBall>().updateBrickCount();
                    ballBounceSource.PlayOneShot(ballBounceClip);
                }
                else
                {
                    ballBounceSource.PlayOneShot(wallBounceClip);
                }
                break;
            case "Wall":
                ballBounceSource.PlayOneShot(wallBounceClip);
                break;
            case "Ball":
                canBreakBricks = true;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }

        if (rb.linearVelocity.magnitude > maxVelocity)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
    }
}
