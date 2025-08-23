using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;   // All parallax layers
    [SerializeField] private float[] parallaxSpeeds;    // Each layer’s speed (closer = faster)

    private float[] bgWidths;
    private Vector3[] startPositions;

    void Start()
    {
        // Store starting positions & widths
        bgWidths = new float[backgrounds.Length];
        startPositions = new Vector3[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            var sr = backgrounds[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                bgWidths[i] = sr.bounds.size.x;
            }
            else
            {
                Debug.LogWarning($"Background {i} does not have a SpriteRenderer.");
                bgWidths[i] = 10f;
            }

            startPositions[i] = backgrounds[i].position;
        }
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Move layer left
            backgrounds[i].position += Vector3.left * parallaxSpeeds[i] * Time.deltaTime;

            // Loop background if fully off-screen
            if (backgrounds[i].position.x <= startPositions[i].x - bgWidths[i])
            {
                backgrounds[i].position = new Vector3(
                    startPositions[i].x,
                    backgrounds[i].position.y,
                    backgrounds[i].position.z
                );
            }
        }
    }
}
