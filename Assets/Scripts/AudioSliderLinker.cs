using UnityEngine;
using UnityEngine.UI;

public class AudioSliderLinker : MonoBehaviour
{
    public enum SliderType { Music, SFX }
    [SerializeField] private SliderType type;

    private void Start()
    {
        Slider slider = GetComponent<Slider>();

        if (AudioMixerManager.Instance != null)
        {
            if (type == SliderType.Music)
            {
                slider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
                slider.onValueChanged.AddListener(AudioMixerManager.Instance.SetMusicVolume);
            }
            else
            {
                slider.value = PlayerPrefs.GetFloat("FXVol", 1f);
                slider.onValueChanged.AddListener(AudioMixerManager.Instance.SetSoundFXVolume);
            }
        }
    }
}
