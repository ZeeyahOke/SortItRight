using System;
using UnityEngine;

public class AudioCueRouter
{
    private readonly SoundLibrary sounds;
    private readonly Action<AudioClip> play;

    public AudioCueRouter(SoundLibrary sounds, Action<AudioClip> play)
    {
        this.sounds = sounds;
        this.play = play;
    }

    public void Subscribe()
    {
        EventBus.Subscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Subscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Subscribe<ItemPickedEvent>(OnItemPicked);
        EventBus.Subscribe<BinFullEvent>(OnBinFull);
        EventBus.Subscribe<BinEmptiedEvent>(OnBinEmptied);
        EventBus.Subscribe<BinRejectedEvent>(OnBinRejected);
        EventBus.Subscribe<SessionStartedEvent>(OnSessionStarted);
        EventBus.Subscribe<SessionEndedEvent>(OnSessionEnded);
    }

    public void Unsubscribe()
    {
        EventBus.Unsubscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Unsubscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Unsubscribe<ItemPickedEvent>(OnItemPicked);
        EventBus.Unsubscribe<BinFullEvent>(OnBinFull);
        EventBus.Unsubscribe<BinEmptiedEvent>(OnBinEmptied);
        EventBus.Unsubscribe<BinRejectedEvent>(OnBinRejected);
        EventBus.Unsubscribe<SessionStartedEvent>(OnSessionStarted);
        EventBus.Unsubscribe<SessionEndedEvent>(OnSessionEnded);
    }

    private void OnItemSorted(ItemSortedEvent e)
    {
        if (e.result.WasRefused)
        {
            return;
        }

        bool serious = e.result.severity == ErrorSeverity.Serious;
        play(e.result.correct ? sounds.Correct : serious ? sounds.SeriousError : sounds.Wrong);
    }

    private void OnItemMissed(ItemMissedEvent e)
    {
        play(e.result.severity == ErrorSeverity.Serious ? sounds.SeriousError : sounds.Missed);
    }

    private void OnItemPicked(ItemPickedEvent e)
    {
        play(sounds.ItemPicked);
    }

    private void OnBinFull(BinFullEvent e)
    {
        play(sounds.BinFull);
    }

    private void OnBinEmptied(BinEmptiedEvent e)
    {
        play(sounds.BinEmptied);
    }

    private void OnBinRejected(BinRejectedEvent e)
    {
        play(sounds.BinRejected);
    }

    private void OnSessionStarted(SessionStartedEvent e)
    {
        play(sounds.SessionStart);
    }

    private void OnSessionEnded(SessionEndedEvent e)
    {
        play(e.outcome == SessionOutcome.Passed ? sounds.SessionPassed : sounds.SessionEnded);
    }
}
