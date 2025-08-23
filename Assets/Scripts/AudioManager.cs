using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicTracks;

    [SerializeField] private Image musicToggle;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    private int currentTrackIndex;
    private bool isMusicOn;


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

        isMusicOn  = PlayerPrefs.GetInt("Music", 1) == 1;
        currentTrackIndex = PlayerPrefs.GetInt("Track", 0);

        ApplySettings();
    }
    
    private void ApplySettings()
    {
        if(isMusicOn)
        {
            choooseTrack(currentTrackIndex);
            musicSource.Play();
        }
        else
        {
            musicSource.Pause();
        }
    }
    public void ToggleMusic(bool isOn)
    {
        isMusicOn = isOn;
        if (isMusicOn)
        {
            musicToggle.sprite = onSprite;
            musicSource.Play();
        }
        else
        {
            musicToggle.sprite = offSprite;
            musicSource.Pause();
        }
    }
   

    public void choooseTrack(int trackIndex)
    {
        if (trackIndex < 0 || trackIndex >= musicTracks.Length) return;

        currentTrackIndex = trackIndex;
        musicSource.clip = musicTracks[currentTrackIndex];
        if (isMusicOn)
        {
            musicSource.Play();
        }

    }

}