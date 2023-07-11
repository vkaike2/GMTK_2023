using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeConfig : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [Header("UI")]
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectSlide;

    private const float MIN = -80;
    private const float MAX = 20;

    private void Awake()
    {
        musicSlider.value = GetPercentage(GameSave.musicVolume);
        effectSlide.value = GetPercentage(GameSave.effectVolume);

        effectSlide.onValueChanged.AddListener(OnVolumeEffectChange);
        musicSlider.onValueChanged.AddListener(OnVolumeMusicChange);
    }


    private void OnVolumeMusicChange(float value)
    {
        GameSave.musicVolume = CalculateFloatValue(value);

        mixer.SetFloat("MusicVolume", GameSave.musicVolume);
    }

    private void OnVolumeEffectChange(float value)
    {
        GameSave.effectVolume = CalculateFloatValue(value);
        mixer.SetFloat("EffectVolume", GameSave.effectVolume);
    }

    private float GetPercentage(float volume)
    {
        volume = Mathf.Clamp(volume, MIN, MAX);
        return (volume - MIN) / (MAX - MIN);
    }

    private float CalculateFloatValue(float percentage)
    {
        return (percentage * 100) - 80;
    }
}
