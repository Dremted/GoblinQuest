using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {

    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20f);
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", Mathf.Log10(volume) * 20f);
    }
}
