using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject gameOverPanel;
    public GameObject gameplayUI;
    public GameObject trophy;

    public Button Menu;
    public Button Retry;

    public void ShowGameOver()
    {

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        if (gameOverPanel != null)
        {
            int current = GameManager.Instance.currentScore;
            int high = GameManager.Instance.highScores[GameManager.Instance.currentMapIndex];
            bool newHigh = current > high;

            currentScoreText.text = "Score: " + current.ToString() ;
            highScoreText.text = high.ToString();
            if (newHigh == true)
            {
                trophy.SetActive(true);
                highScoreText.text += " (NEW!) High Score: ";
            }
                

            gameOverPanel.SetActive(true);
        }
    }
    public void Reset()
    {
        GameManager.Instance.SaveHighScore();
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        GameManager.Instance.SaveHighScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
