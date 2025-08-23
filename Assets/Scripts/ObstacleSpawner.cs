using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject groundObstaclePrefab;
    public GameObject topObstaclePrefab;

    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 2f;
    public float difficultyRamp = 0.05f; // Decrease spawn interval per score point
    public float airObstacleHeight = 2f;

    private float timer;

    private void Update()
    {
        SpawnObstacle();
    }


    void SpawnObstacle()
    {
        timer += Time.deltaTime;
        if (timer>= obstacleSpawnTime)
        {
            Spawn();
            timer = 0f;

            AdjustDifficulty();

        }
    }

    private void Spawn()
    {
        GameObject obstacle;
        Vector3 spawnPosition = transform.position;

        if (Random.value > 0.5f)
        {
            // Spawn ground obstacle
            obstacle = groundObstaclePrefab;
        }
        else
        {
            // Spawn top obstacle
            spawnPosition.y += airObstacleHeight;
            obstacle = topObstaclePrefab;
        }

        GameObject newObstacle = Instantiate(obstacle, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = newObstacle.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * obstacleSpeed;
    }

    private void AdjustDifficulty()
    {
        int score = GameManager.Instance.currentScore;
        // Decrease spawn time based on score and difficulty ramp
        obstacleSpawnTime = Mathf.Max(0.5f, 2f - (score * difficultyRamp));
        // Optionally increase obstacle speed as well
        obstacleSpeed = Mathf.Min(12f, 3f + score * difficultyRamp);
    }
}
