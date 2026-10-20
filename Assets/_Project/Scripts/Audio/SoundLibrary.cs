using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Sort It Right/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip wrong;
    [SerializeField] private AudioClip seriousError;
    [SerializeField] private AudioClip missed;
    [SerializeField] private AudioClip binFull;
    [SerializeField] private AudioClip binRejected;
    [SerializeField] private AudioClip binEmptied;
    [SerializeField] private AudioClip itemPicked;
    [SerializeField] private AudioClip buttonPress;
    [SerializeField] private AudioClip sessionStart;
    [SerializeField] private AudioClip sessionPassed;
    [SerializeField] private AudioClip sessionEnded;

    public AudioClip Correct => correct;
    public AudioClip Wrong => wrong;
    public AudioClip SeriousError => seriousError;
    public AudioClip Missed => missed;
    public AudioClip BinFull => binFull;
    public AudioClip BinRejected => binRejected;
    public AudioClip BinEmptied => binEmptied;
    public AudioClip ItemPicked => itemPicked;
    public AudioClip ButtonPress => buttonPress;
    public AudioClip SessionStart => sessionStart;
    public AudioClip SessionPassed => sessionPassed;
    public AudioClip SessionEnded => sessionEnded;
}
