using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioTrapClips;

    public void OnDoorOpen()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioTrapClips, transform, 1f);
    }
}
