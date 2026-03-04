using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioDoorClips;

    public void OnDoorOpen()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioDoorClips, transform, 1f);
    }
}
