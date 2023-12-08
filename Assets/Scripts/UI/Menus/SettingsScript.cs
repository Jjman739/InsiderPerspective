using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private AudioMixer masterAudioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider dialogueVolumeSlider;
    private float masterVolume;
    private float musicVolume;
    private float sfxVolume;
    private float dialogueVolume;
    const string MASTER_VOL_STRING = "Master";
    const string MUSIC_VOL_STRING = "Music";
    const string SFX_VOL_STRING = "SFX";
    const string DIALOGUE_VOL_STRING = "Dialogue";

    private void Start()
    {
        initializeVolumeSettings();

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
        dialogueVolumeSlider.value = dialogueVolume;
    }

    private void initializeVolumeSettings()
    {
        masterVolume = PlayerPrefs.HasKey(MASTER_VOL_STRING) ? PlayerPrefs.GetFloat(MASTER_VOL_STRING) : 1f;
        musicVolume = PlayerPrefs.HasKey(MUSIC_VOL_STRING) ? PlayerPrefs.GetFloat(MUSIC_VOL_STRING) : 0.3f;
        sfxVolume = PlayerPrefs.HasKey(SFX_VOL_STRING) ? PlayerPrefs.GetFloat(SFX_VOL_STRING) : 1f;
        dialogueVolume = PlayerPrefs.HasKey(DIALOGUE_VOL_STRING) ? PlayerPrefs.GetFloat(DIALOGUE_VOL_STRING) : 1f;

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
        SetDialogueVolume(dialogueVolume);
    }

    public void SetMasterVolume(float sliderValue)
    {
        masterVolume = sliderValue;
        setVolume(MASTER_VOL_STRING, sliderValue);
        updateSliderValue(masterVolumeSlider, sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicVolume = sliderValue;
        setVolume(MUSIC_VOL_STRING, sliderValue);
        updateSliderValue(musicVolumeSlider, sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        sfxVolume = sliderValue;
        setVolume(SFX_VOL_STRING, sliderValue);
        updateSliderValue(sfxVolumeSlider, sliderValue);
    }

    public void SetDialogueVolume(float sliderValue)
    {
        dialogueVolume = sliderValue;
        setVolume(DIALOGUE_VOL_STRING, sliderValue);
        updateSliderValue(dialogueVolumeSlider, sliderValue);
    }

    private void setVolume(string channel, float vol)
    {
        masterAudioMixer.SetFloat(channel, Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat(channel, vol);
    }

    private void updateSliderValue(Slider slider, float value)
    {
        slider.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = string.Format("{0}", Mathf.Round(value * 100f));     
    }
}
