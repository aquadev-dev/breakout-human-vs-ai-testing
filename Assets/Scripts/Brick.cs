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
        if (gameManager != null)
        {
            gameManager.AddScore(points);
        }
        Destroy(gameObject);
    }
}
