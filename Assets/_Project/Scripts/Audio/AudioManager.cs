using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SoundLibrary sounds;

    private AudioCueRouter cues;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        cues = new AudioCueRouter(sounds, Play);
    }

    private void OnEnable()
    {
        cues?.Subscribe();
    }

    private void OnDisable()
    {
        cues?.Unsubscribe();
    }

    public void PlayButtonPress()
    {
        Play(sounds.ButtonPress);
    }

    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
