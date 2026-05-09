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
        BallController ball = other.GetComponent<BallController>();
        if (ball != null)
        {
            if (gameManager is DoubleGameManager dgm)
            {
                dgm.BallLost(other.gameObject);
            }
            else
            {
                if (other.gameObject.name == "TrappedBall")
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    if (gameManager != null)
                    {
                        gameManager.LoseLife();
                    }
                }
            }
        }
    }
}
