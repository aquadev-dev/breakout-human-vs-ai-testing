using UnityEngine;
using Unity.Properties;
using System.Collections.Generic;

public class CavityGameManager : BreakoutGameManager
{
    public GameObject ballPrefab;
    public Vector2Int cavity1Pos = new Vector2Int(3, 3);
    public Vector2Int cavity2Pos = new Vector2Int(9, 3);
    public Vector2Int cavitySize = new Vector2Int(2, 2);

    private bool ballsSpawned = false;

    private void Awake()
    {
        // Prevent double spawning if editor-spawned ones exist
        if (transform.parent != null)
        {
             GameObject[] existing = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
             foreach(var obj in existing) {
                 if (obj.name == "TrappedBall") ballsSpawned = true;
             }
        }
    }

    public override void SpawnBricks()
    {
        // Classic Super Breakout layout settings
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
                if (IsInCavity(c, r)) continue;

                Vector3 pos = gridOrigin + new Vector3(c * spacingX, -r * spacingY, 0);
                GameObject brick = Instantiate(brickPrefab, pos, Quaternion.identity, transform);
                
                SpriteRenderer sr = brick.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = rowColor;
                }
            }
        }

        if (!ballsSpawned) {
            SpawnTrappedBalls();
            ballsSpawned = true;
        }
    }

    private bool IsInCavity(int c, int r)
    {
        if (c >= cavity1Pos.x && c < cavity1Pos.x + cavitySize.x && r >= cavity1Pos.y && r < cavity1Pos.y + cavitySize.y) return true;
        if (c >= cavity2Pos.x && c < cavity2Pos.x + cavitySize.x && r >= cavity2Pos.y && r < cavity2Pos.y + cavitySize.y) return true;
        return false;
    }

    private Color GetClassicColorForRow(int r)
    {
        // classic Super Breakout colors (top to bottom): 2 Red, 2 Orange, 2 Green, 2 Yellow
        if (r < 2) return Color.red;
        if (r < 4) return new Color(1f, 0.5f, 0f); // Orange
        if (r < 6) return Color.green;
        return Color.yellow;
    }

    private void SpawnTrappedBalls()
    {
        SpawnBallAtCavity(cavity1Pos);
        SpawnBallAtCavity(cavity2Pos);
    }

    private void SpawnBallAtCavity(Vector2Int cavityPos)
    {
        if (ballPrefab == null) return;
        
        Vector3 pos = gridOrigin + new Vector3((cavityPos.x + cavitySize.x/2f - 0.5f) * spacingX, -(cavityPos.y + cavitySize.y/2f - 0.5f) * spacingY, 0);
        GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);
        ball.name = "TrappedBall";
        ball.tag = "Ball";
        
        BallController bc = ball.GetComponent<BallController>();
        if (bc != null)
        {
            bc.canBreakBricks = false;
            bc.isLaunched = true;
            var rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.bodyType = RigidbodyType2D.Dynamic;
                // Give it some initial diagonal velocity
                rb.linearVelocity = new Vector2(Random.value > 0.5f ? 1f : -1f, -1f).normalized * bc.initialForce;
            }
        }
    }
}
