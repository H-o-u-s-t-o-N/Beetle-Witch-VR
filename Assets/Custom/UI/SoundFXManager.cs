using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        var audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // var clipLength = audioSource.clip.length;
        audioSource.Play();

        Destroy(audioSource.gameObject, 1);
    }

    public void PlayRandomClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
    {
        var random = Random.Range(0, audioClips.Length);

        var audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClips[random];
        audioSource.volume = volume;

        // var clipLength = audioSource.clip.length;
        audioSource.Play();

        Destroy(audioSource.gameObject, 1);
    }

}