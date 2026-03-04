using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsBellSound;
    public void OnSoundDoorBell()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsBellSound, transform, 1f);
    }
}
