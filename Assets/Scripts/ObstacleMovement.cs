using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (Time.timeScale == 0) return; // Stop moving when paused

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy when off left side of screen
        if (Camera.main != null && transform.position.x < Camera.main.transform.position.x - 12f)
        {
            Destroy(gameObject);
        }
    }
}
