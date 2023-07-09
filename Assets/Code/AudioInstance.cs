using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void SetItUp(AudioClip clip, float pitch = 1, float volume = 1)
    {
        _audioSource.volume = volume;
        _audioSource.pitch = pitch;
        _audioSource.clip = clip;

        _audioSource.Play();

        StartCoroutine(WaitThenDie(clip.length));
    }

    private IEnumerator WaitThenDie(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
