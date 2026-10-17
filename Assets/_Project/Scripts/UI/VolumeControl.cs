using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        slider.SetValueWithoutNotify(VolumeSettings.Load());
        slider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnVolumeChanged);
        PlayerPrefs.Save();
    }

    private void OnVolumeChanged(float volume)
    {
        VolumeSettings.Apply(volume);
    }
}
