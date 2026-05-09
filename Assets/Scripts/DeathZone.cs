using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private BreakoutGameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindAnyObjectByType<BreakoutGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") || other.GetComponent<BallController>() != null)
        {
            if (gameManager != null)
            {
                gameManager.LoseLife();
            }
        }
    }
}
