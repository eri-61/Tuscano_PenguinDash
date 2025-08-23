using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button StartGame;
    public Button Settings;
    public Button Exit;

    public GameObject MapSelection;
    public GameObject SettingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StartGame != null) StartGame.onClick.AddListener(ShowMapSelection);
        if (Settings != null) Settings.onClick.AddListener(ShowSettings);
        if (Exit != null) Exit.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowMapSelection()
    {
        Debug.Log("Start Game button clicked"); 
        MapSelection.SetActive(true);
    }
    void ShowSettings()
    {
        Debug.Log("Settings button clicked");
        SettingsMenu.SetActive(true);
    }
    void QuitGame()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();
    }
}
