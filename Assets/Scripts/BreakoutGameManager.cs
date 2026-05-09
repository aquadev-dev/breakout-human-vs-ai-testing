using UnityEngine;
using Unity.Properties;

public class BreakoutGameManager : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 20;
    public int columns = 24;
    public float spacingX = 0.8f;
    public float spacingY = 0.25f;
    public Vector3 gridOrigin = new Vector3(-9.2f, 10f, 0f);

    [SerializeField, DontCreateProperty]
    private int m_Score = 0;
    
    [CreateProperty]
    public int Score
    {
        get => m_Score;
        protected set => m_Score = value;
    }

    [SerializeField, DontCreateProperty]
    private int m_Lives = 5;

    [CreateProperty]
    public int Lives
    {
        get => m_Lives;
        protected set => m_Lives = value;
    }

    private Color[] rowColors = new Color[]
    {
        Color.red,
        new Color(1f, 0.5f, 0f), // Orange
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        new Color(0.5f, 0f, 1f), // Purple
        Color.magenta
    };

    private void Start()
    {
        if (transform.childCount == 0)
        {
            SpawnBricks();
        }
    }

    public virtual void SpawnBricks()
    {
        for (int r = 0; r < rows; r++)
        {
            Color rowColor = rowColors[r % rowColors.Length];
            for (int c = 0; c < columns; c++)
            {
                Vector3 pos = gridOrigin + new Vector3(c * spacingX, -r * spacingY, 0);
                GameObject brick = Instantiate(brickPrefab, pos, Quaternion.identity, transform);
                
                SpriteRenderer sr = brick.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = rowColor;
                }
            }
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public virtual void LoseLife()
    {
        Lives--;
        if (Lives <= 0)
        {
            GameOver();
        }
        else
        {
            ResetBall();
        }
    }

    protected virtual void ResetBall()
    {
        GameObject ballObj = GameObject.Find("Ball");
        if (ballObj != null)
        {
            BallController ball = ballObj.GetComponent<BallController>();
            PaddleController paddle = Object.FindAnyObjectByType<PaddleController>();
            if (ball != null && paddle != null)
            {
                ball.ResetBall(paddle.transform);
            }
        }
    }

    protected virtual void GameOver()
    {
        Debug.Log("Game Over!");
        GameOverManager gameOverManager = Object.FindAnyObjectByType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }
    }
