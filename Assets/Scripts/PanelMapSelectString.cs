using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PanelMapSelectString : MonoBehaviour
{
    public GameObject Panel;
    public GameObject LockedOverlay;

    public Image Map;

    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI LockedText;

    public Button Left;
    public Button Right;
    public Button Close;
    public Button Select;

    public Sprite[] mapSprites;
    public int[] unlockScore;

    private int index = 0;

    private void Start()
    {
        Left.onClick.AddListener(Prev);
        Right.onClick.AddListener(Next);
        Select.onClick.AddListener(SelectMap);
        Close.onClick.AddListener(ClosePanel);
        UpdateUI();
    }
    void ClosePanel()
    {
        Panel.SetActive(false);
    }

    public void Prev()
    {
        index = (index - 1 + mapSprites.Length) % mapSprites.Length;
        UpdateUI();
    }

    public void Next()
    {
        index = (index + 1) % mapSprites.Length;
        UpdateUI();
    }

    void UpdateUI()
    {
        Map.sprite = mapSprites[index];
        int score = PlayerPrefs.GetInt($"HighScore_Map{index}", 0);
        HighScore.text = $"High Score: {score} ";
        LockedOverlay.SetActive(isLocked(index));
        LockedText.text = isLocked(index) ? " THIS MAP IS LOCKED. GET "+ unlockScore[index] + " ON PREVIOUS MAP TO UNLOCK" : "Unlocked";
    }

    bool isLocked(int index)
    {
        if (index == 0)
            return false; // First map is always unlocked
        int prevHS = PlayerPrefs.GetInt($"HighScore_Map{index - 1}", 0);
        int need = (unlockScore != null && unlockScore.Length > index) ? unlockScore[index] : 0;
        return prevHS < need;
    }

    public void SelectMap()
    {
        if (isLocked(index))
        {
            Debug.Log("Map is locked!");
            return;
        }
        PlayerPrefs.SetInt("SelectedMap", index);

        Debug.Log($"Selected Map: {index}");
        if (index == 0)
        {
            GameManager.Instance.currentMapIndex = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameMap1");
        }
        else if (index == 1)
        {
            GameManager.Instance.currentMapIndex = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameMap2");
        }
        
    }
}