using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [SerializeField]
    private List<AudioModel> models;

    [Header("CONFIFURATIONS")]
    [SerializeField]
    private MinMax pitch;

    [Header("COMPONENTS")]
    [SerializeField]
    private AudioInstance audioInstance;

    private AudioSource _audioSource;

    private void OnValidate()
    {
        foreach (var model in models)
        {
            if (model == null) continue;

            model.SetName();
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioType audioType)
    {
        PlayInternal(audioType);
    }

    public void PlayRandomPitch(AudioType audioType)
    {
        PlayInternal(audioType, pitch.GetRandom());
    }

    public void PlayInstance(AudioType audioType)
    {
        AudioModel model = models.Where(e => e.AudioType == audioType).FirstOrDefault();

        AudioInstance instance = Instantiate(audioInstance, this.transform);
        instance.transform.parent = null;
        instance.SetItUp(model.AudioClip, 1, model.Volume);
    }

    private void PlayInternal(AudioType audioType, float pitch = 1)
    {
        AudioModel model = models.Where(e => e.AudioType == audioType).FirstOrDefault();
        _audioSource.pitch = pitch;
        _audioSource.clip = model.AudioClip;
        _audioSource.volume = model.Volume;
        _audioSource.Play();
    }

    internal void PlayMusic()
    {
        _audioSource.Play();
    }

    public enum AudioType
    {
        Footstep,
        WeaponAttack,
        PickUPItem,
        ReceiveDamage,
        ProjectileDestoyed,
        Trigger
    }

    [Serializable]
    public class AudioModel
    {
        [HideInInspector]
        public string name;
        [SerializeField]
        private AudioClip audioClip;
        [SerializeField]
        private AudioType audioType;
        [SerializeField]
        private float volume = 1;

        public AudioType AudioType => audioType;
        public AudioClip AudioClip => audioClip;
        public float Volume => volume;

        public void SetName()
        {
            name = audioType.ToString();
        }
    }

    [Serializable]
    private struct MinMax
    {
        public float min;
        public float max;

        public float GetRandom()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}
