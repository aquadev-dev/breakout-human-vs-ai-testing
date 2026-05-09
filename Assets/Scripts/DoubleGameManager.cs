using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DoubleGameManager : BreakoutGameManager
{
    public GameObject ballPrefab;
    public Transform topPaddle;
    public Transform bottomPaddle;
    
    [SerializeField]
    private List<GameObject> activeBalls = new List<GameObject>();

    private void Awake()
    {
        // Try to find paddles if they weren't assigned in the inspector
        if (bottomPaddle == null)
        {
            GameObject bottomObj = GameObject.Find("Paddle");
            if (bottomObj != null) bottomPaddle = bottomObj.transform;
        }

        if (topPaddle == null)
        {
            GameObject topObj = GameObject.Find("TopPaddle");
            if (topObj != null) topPaddle = topObj.transform;
        }
        
        // Find initial balls in scene
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach(var b in balls) RegisterBall(b);
    }

    private void Update()
    {
        // Tandem movement: ensure top paddle matches bottom paddle's X
        if (topPaddle != null && bottomPaddle != null)
        {
            Vector3 topPos = topPaddle.position;
            topPos.x = bottomPaddle.position.x;
            topPaddle.position = topPos;
        }

        // Double Launch: Launch all unlaunched balls on Space
        if (InputSystem.actions != null && 
            InputSystem.actions.FindAction("Launch").WasPressedThisFrame())
        {
            // Use a copy to avoid modification while iterating
            var ballsToLaunch = new List<GameObject>(activeBalls);
            foreach(var ball in ballsToLaunch)
            {
                if (ball != null)
                {
                    var bc = ball.GetComponent<BallController>();
                    if (bc != null && !bc.isLaunched)
                    {
                        bc.Launch();
                    }
                }
            }
        }
    }

    public override void SpawnBricks()
    {
        rows = 8;
        columns = 14; 
        spacingX = 1.3f;
        spacingY = 0.4f;
        gridOrigin = new Vector3(-8.45f, 9f, 0f);

        for (int r = 0; r < rows; r++)
        {
            Color rowColor = GetClassicColorForRow(r);
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

    private Color GetClassicColorForRow(int r)
    {
        if (r < 2) return Color.red;
        if (r < 4) return new Color(1f, 0.5f, 0f); // Orange
        if (r < 6) return Color.green;
        return Color.yellow;
    }

    public void RegisterBall(GameObject ball)
    {
        if (ball != null && !activeBalls.Contains(ball))
        {
            activeBalls.Add(ball);
        }
    }

    public void BallLost(GameObject ball)
    {
        if (ball == null) return;

        activeBalls.Remove(ball);
        
        // Cleanup null entries
        activeBalls.RemoveAll(b => b == null);

        if (activeBalls.Count == 0)
        {
            LoseLife();
        }
        else
        {
            Destroy(ball);
        }
    }

    protected override void ResetBall()
    {
        // Destroy any remaining active balls to ensure a clean state
        foreach(var b in activeBalls)
        {
            if (b != null) Destroy(b);
        }
        activeBalls.Clear();

        // Setup two new balls
        if (bottomPaddle != null)
            SetupNewBall("Ball", bottomPaddle, new Vector3(0, 0.5f, 0));
        
        if (topPaddle != null)
            SetupNewBall("SecondBall", topPaddle, new Vector3(0, 0.5f, 0));
    }

    private void SetupNewBall(string name, Transform paddle, Vector3 offset)
    {
        if (ballPrefab == null)
        {
            Debug.LogError($"[DoubleGameManager] Ball prefab is not assigned on {gameObject.name}!");
            return;
        }
        
        GameObject ball = Instantiate(ballPrefab, paddle.position + offset, Quaternion.identity);
        ball.name = name;
        ball.tag = "Ball";
        ball.transform.parent = paddle;
        
        var bc = ball.GetComponent<BallController>();
        if (bc != null)
        {
            bc.ResetBall(paddle);
        }
        
        RegisterBall(ball);
    }
}
