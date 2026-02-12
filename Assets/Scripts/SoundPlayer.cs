using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipStep;
    [SerializeField] private AudioClip[] audioClipGetItem;
    [SerializeField] private AudioClip[] audioClipsSetTrap;
    [SerializeField] private AudioClip[] audioClipsHide;

    public void OnStepSound()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipStep, transform, 1f);
    }

    public void OnGetItemSound()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipGetItem, transform, 1f);
    }

    public void OnSetTrap()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsSetTrap, transform, 1f);
    }

    public void OnSoundHide()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsHide, transform, 1f);
    }
}
