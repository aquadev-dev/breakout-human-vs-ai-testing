using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;

    Rigidbody2D rb;

    int score = 0;
    int lives = 5;

    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage;

    public GameObject gameOverPanel;
    public GameObject youWinPanel;

    public AudioSource ballBounceSource;
    public AudioClip ballBounceClip;
    public AudioClip wallBounceClip;

    int brickCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindAnyObjectByType<LevelGenerator>().transform.childCount + 15;
        rb.linearVelocity = Vector2.down * 5f;
        Scene CurrentScene = SceneManager.GetActiveScene();
        Debug.Log(CurrentScene.name);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY) 
        {
            gameOverPanel.SetActive(false);
            if (lives <= 0)
            {
                GameOver();
            }
            else 
            {
                transform.position = Vector3.zero;
                rb.linearVelocity = Vector2.down * 5f;
                lives--;
                livesImage[lives].SetActive(false);
            }
                
        }

        if (rb.linearVelocity.magnitude > maxVelocity) 
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Brick":
                Destroy(collision.gameObject);
                updateScore();
                updateBrickCount();
                ballBounceSource.PlayOneShot(ballBounceClip);
                if (brickCount <= 0)
                {
                    youWinPanel.SetActive(true);
                    Time.timeScale = 0;
                }
                break;
            case "Wall":
                ballBounceSource.PlayOneShot(wallBounceClip);
                break;
            default:
                break;
        }

    }

    public void updateScore() 
    {
        score += 10;
        scoreTxt.text = score.ToString("00000");
    }

    public void updateBrickCount() 
    {
        brickCount--;
    }

    void GameOver() 
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
