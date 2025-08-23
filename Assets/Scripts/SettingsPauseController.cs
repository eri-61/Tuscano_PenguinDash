using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPauseController : MonoBehaviour
{
    public GameObject pauseMenu;

    public Button Close;
    public Button Restart;
    public Button MainMenu;

    public Toggle ToggleMusic;
    public TMP_Dropdown Track;

    private void OnEnable()
    {
        int isMusicOn = PlayerPrefs.GetInt("Music", 1);
        int trackIndex = PlayerPrefs.GetInt("Track", 0);

        ToggleMusic.isOn = (isMusicOn == 1);
        Track.value = trackIndex;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic(isMusicOn == 1);
            AudioManager.Instance.choooseTrack(trackIndex);
        }

        ToggleMusic.onValueChanged.AddListener(OnMusicToggleChanged);
        Track.onValueChanged.AddListener(OnDropdownValueChanged);

    }

    private void OnDisable()
    {
        ToggleMusic.onValueChanged.RemoveListener(OnMusicToggleChanged);
        Track.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("Music", isOn ? 1 : 0);
        PlayerPrefs.Save();

        AudioManager.Instance.ToggleMusic(isOn);
    }

    private void OnDropdownValueChanged(int trackIndex)
    {
        PlayerPrefs.SetInt("Track", trackIndex);
        PlayerPrefs.Save();

        AudioManager.Instance.choooseTrack(trackIndex);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MainMenu.onClick.AddListener(Menu);
        Restart.onClick.AddListener(Reset);
        Close.onClick.AddListener(Resume);
    }

    void Menu()
    {
        GameManager.Instance.SaveHighScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void Reset()
    {
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
