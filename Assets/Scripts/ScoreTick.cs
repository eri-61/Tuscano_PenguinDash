using System.Collections;
using UnityEngine;

public class ScoreTick : MonoBehaviour
{
    public float interval = 1f; // seconds per score increment
    private float timer;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore();
            timer = interval;
        }
        else
        {
            StartCoroutine(WaitForGameManager());
        }
    }
    private IEnumerator WaitForGameManager()
    {
        while (GameManager.Instance == null)
            yield return null; // wait a frame

        GameManager.Instance.ResetScore();
        timer = interval;
    }

    private void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GameManager.Instance.AddScore(1); // Increment current score
            timer = interval; // Reset timer
        }
    }
}
