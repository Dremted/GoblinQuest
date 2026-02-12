using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipStep;
    [SerializeField] private AudioClip[] audioClipFall;
    [SerializeField] private AudioClip[] audioClipsRage;
    [SerializeField] private AudioClip[] audioClipsFallSlink;
    [SerializeField] private AudioClip[] audioClipsDeleteTrap;

    public void OnStepSound()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipStep, transform, 1f);
    }

    public void OnFallSound()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipFall, transform, 1f);
    }

    public void OnRageSound()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsRage, transform, 1f);
    }

    public void OnFallSlink()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsFallSlink, transform, 1f);
    }

    public void DeleteTrap()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsDeleteTrap, transform, 1f);
    }
}
