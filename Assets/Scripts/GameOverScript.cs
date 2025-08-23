using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject gameOverPanel;
    public GameObject gameplayUI;

    public void ShowGameOver()
    {
        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        if (gameOverPanel != null)
        {
            int current = GameManager.Instance.currentScore;
            int high = GameManager.Instance.highScores[GameManager.Instance.currentMapIndex];
            bool newHigh = GameManager.Instance.SaveHighScore();

            currentScoreText.text = current.ToString();
            highScoreText.text = high.ToString();
            if (newHigh)
                highScoreText.text += " (NEW!)";

            gameOverPanel.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
