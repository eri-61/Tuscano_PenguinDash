using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject groundObstaclePrefab;
    public GameObject topObstaclePrefab;

    public float spawnInterval = 2f;        // Initial spawn interval
    public float minSpawnInterval = 0.5f;   // Fastest spawn interval
    public float difficultyRamp = 0.05f;    // How much interval decreases per score

    public Transform spawnPoint;            // Right edge of screen
    public float groundY = -2f;             // Y pos for ground obstacle
    public float topY = 2f;                 // Y pos for top obstacle

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (Time.timeScale == 0) return; // Don't spawn while paused

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnObstacle();
            // Reduce interval based on score
            float newInterval = Mathf.Max(minSpawnInterval, spawnInterval - GameManager.Instance.currentScore * difficultyRamp);
            timer = newInterval;
        }
    }

    void SpawnObstacle()
    {
        bool spawnGround = Random.value > 0.5f;
        GameObject prefab = spawnGround ? groundObstaclePrefab : topObstaclePrefab;
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y = spawnGround ? groundY : topY;

        GameObject obs = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Add Rigidbody2D if not present
        Rigidbody2D rb = obs.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obs.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Add ObstacleMovement component
        if (obs.GetComponent<ObstacleMovement>() == null)
        {
            ObstacleMovement move = obs.AddComponent<ObstacleMovement>();
            move.speed = 5f;
        }
    }
}
