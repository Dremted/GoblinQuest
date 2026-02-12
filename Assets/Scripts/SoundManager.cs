using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayRandomSoundFXClip(AudioClip[] clip, Transform positionSound, float volume)
    {
        int randomIndex = Random.Range(0, clip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, positionSound.position, Quaternion.identity);

        audioSource.clip = clip[randomIndex];
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght);
    }
}
