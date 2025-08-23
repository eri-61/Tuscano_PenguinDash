using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    public int currentScore { get; private set; } = 0;
    public int[] highScores; // per-map high scores
    public int currentMapIndex; // set when starting map

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize high scores
            int mapCount = 2; // adjust for your maps
            highScores = new int[mapCount];
            for (int i = 0; i < mapCount; i++)
                highScores[i] = PlayerPrefs.GetInt($"HighScore_Map{i}", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    public bool SaveHighScore()
    {
        if (currentScore > highScores[currentMapIndex])
        {
            highScores[currentMapIndex] = currentScore;
            PlayerPrefs.SetInt($"HighScore_Map{currentMapIndex}", currentScore);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}
