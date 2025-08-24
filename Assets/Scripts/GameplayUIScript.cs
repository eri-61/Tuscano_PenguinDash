using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayUIScript : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    public GameObject pauseMenu;

    public Button Pause;
    private void Start()
    {
        if (Pause != null)
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false); // Hide pause menu initially
            }

            Pause.onClick.AddListener(showPauseMenu);
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = GameManager.Instance.currentScore.ToString();
        }
    }

    void showPauseMenu()
    {
        Debug.Log("Pause button clicked!");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
