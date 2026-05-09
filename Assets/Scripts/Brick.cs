using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 10;
    private BreakoutGameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<BreakoutGameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();
        if (ball != null && !ball.canBreakBricks)
        {
            // Ball is trapped, just bounce
            return;
        }

        if (ball == null) 
        {
            Debug.LogWarning($"Brick hit by non-ball object: {collision.gameObject.name}");
        }

        if (gameManager != null)
        {
            gameManager.AddScore(points);
        }
        Destroy(gameObject);
    }
}
