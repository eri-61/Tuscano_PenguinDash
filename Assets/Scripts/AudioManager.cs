using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;
    public AudioClip[] musicTracks;

    public Image checkboxImage;
    public Sprite onSprite;
    public Sprite offSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        int trackIndex = PlayerPrefs.GetInt("Track", 0);
        chooseTrack(trackIndex);

        bool on = PlayerPrefs.GetInt("Music", 1) == 1;
        ToggleMusic(on);
    }
    public void ToggleMusic(bool isOn)
    {
        if (!musicSource) return;
        if (isOn)
        {
            if(!musicSource.isPlaying) musicSource.Play();
            checkboxImage.sprite = onSprite;
        }
        else
        {
            musicSource.Pause();
            checkboxImage.sprite = offSprite;
        }
        PlayerPrefs.SetInt("Music", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void chooseTrack(int trackIndex)
    {
        if (musicTracks == null || musicTracks.Length == 0) return;
        if (trackIndex < 0 || trackIndex >= musicTracks.Length) return; 

        musicSource.clip = musicTracks[trackIndex];
        musicSource.Play();
        
        PlayerPrefs.SetInt("Track", trackIndex);
        PlayerPrefs.Save();

    }

}