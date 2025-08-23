using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}