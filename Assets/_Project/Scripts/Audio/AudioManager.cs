using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SoundLibrary sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Subscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Subscribe<BinFullEvent>(OnBinFull);
        EventBus.Subscribe<BinRejectedEvent>(OnBinRejected);
        EventBus.Subscribe<ItemPickedEvent>(OnItemPicked);
        EventBus.Subscribe<SessionEndedEvent>(OnSessionEnded);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Unsubscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Unsubscribe<BinFullEvent>(OnBinFull);
        EventBus.Unsubscribe<BinRejectedEvent>(OnBinRejected);
        EventBus.Unsubscribe<ItemPickedEvent>(OnItemPicked);
        EventBus.Unsubscribe<SessionEndedEvent>(OnSessionEnded);
    }

    public void PlayButtonPress()
    {
        Play(sounds.ButtonPress);
    }

    private void OnItemSorted(ItemSortedEvent e)
    {
        if (e.result.WasRefused)
        {
            return;
        }

        Play(e.result.correct ? sounds.Correct : MistakeClip(e.result));
    }

    private void OnItemMissed(ItemMissedEvent e)
    {
        bool serious = e.result.severity == ErrorSeverity.Serious;
        Play(serious ? sounds.SeriousError : sounds.Missed);
    }

    private void OnBinFull(BinFullEvent e)
    {
        Play(sounds.BinFull);
    }

    private void OnBinRejected(BinRejectedEvent e)
    {
        Play(sounds.BinRejected);
    }

    private void OnItemPicked(ItemPickedEvent e)
    {
        Play(sounds.ItemPicked);
    }

    private void OnSessionEnded(SessionEndedEvent e)
    {
        if (e.outcome == SessionOutcome.Passed)
        {
            Play(sounds.SessionPassed);
        }
    }

    private AudioClip MistakeClip(SortResult result)
    {
        return result.severity == ErrorSeverity.Serious ? sounds.SeriousError : sounds.Wrong;
    }

    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
