using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFight : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClipsFight;

    public void OnSoundFight()
    {
        SoundManager.Instance.PlayRandomSoundFXClip(audioClipsFight, transform, 1f);
    }
 }
