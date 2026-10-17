using UnityEngine;

public static class VolumeSettings
{
    private const string VolumeKey = "Volume";

    public static float Load()
    {
        return PlayerPrefs.GetFloat(VolumeKey, 1f);
    }

    public static void Apply(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }

    public static void ApplySaved()
    {
        AudioListener.volume = Load();
    }
}
