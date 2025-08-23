using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PanelSettingsScript : MonoBehaviour
{
    public Button Close;
    public Button KeyJump;
    public Button KeySlide;

    public Toggle ToggleMusic;

    public TMP_Dropdown Track;

    public TextMeshProUGUI Jump;
    public TextMeshProUGUI Slide;

    public GameObject Panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Track.onValueChanged.AddListener(val => AudioManager.Instance.choooseTrack(val));

        bool on = PlayerPrefs.GetInt("Music", 1) == 1;
        ToggleMusic.isOn = on;
        AudioManager.Instance.ToggleMusic(on);
        ToggleMusic.onValueChanged.AddListener(val => AudioManager.Instance.ToggleMusic(val));

        Jump.text = KeybindManager.Instance.KeyJump.ToString();
        Slide.text = KeybindManager.Instance.KeySlide.ToString();

        KeyJump.onClick.AddListener(RebindJump);
        KeySlide.onClick.AddListener(RebindSlide);

        Close.onClick.AddListener(OnClickClose);
    }
    public void OnClickClose()
    {
        Panel.SetActive(false);
    }

    public void RebindJump()
    {
        Jump.text = "Press a key...";
        KeybindManager.Instance.BeginRebind((key) =>
        {
            Jump.text = key.ToString();
            KeybindManager.Instance.SetKeyJump(key);
        });
    }

    public void RebindSlide()
    {
        Slide.text = "Press a key...";
        KeybindManager.Instance.BeginRebind((key) =>
        {
            Slide.text = key.ToString();
            KeybindManager.Instance.SetKeySlide(key);
        });
    }

    public void ChooseTrack()
    {
        int selectedIndex = Track.value;
        AudioManager.Instance.choooseTrack(selectedIndex);

    }
}
