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
    [SerializeField] private Slider mouseSensitivitySlider;
    private float masterVolume;
    private float musicVolume;
    private float sfxVolume;
    private float dialogueVolume;
    private float mouseSensitivity;
    const string MASTER_VOL_STRING = "Master";
    const string MUSIC_VOL_STRING = "Music";
    const string SFX_VOL_STRING = "SFX";
    const string DIALOGUE_VOL_STRING = "Dialogue";
    const string MOUSE_SENSITIVITY_STRING = "MouseSensitivity";

    private void Start()
    {
        SetVolumeSettings();
    }

    public void SetVolumeSettings()
    {
        initializeVolumeSettings();

        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
        dialogueVolumeSlider.value = dialogueVolume;
        mouseSensitivitySlider.value = mouseSensitivity;
    }

    private void initializeVolumeSettings()
    {
        masterVolume = PlayerPrefs.HasKey(MASTER_VOL_STRING) ? PlayerPrefs.GetFloat(MASTER_VOL_STRING) : 1f;
        musicVolume = PlayerPrefs.HasKey(MUSIC_VOL_STRING) ? PlayerPrefs.GetFloat(MUSIC_VOL_STRING) : 0.1f;
        sfxVolume = PlayerPrefs.HasKey(SFX_VOL_STRING) ? PlayerPrefs.GetFloat(SFX_VOL_STRING) : 1f;
        dialogueVolume = PlayerPrefs.HasKey(DIALOGUE_VOL_STRING) ? PlayerPrefs.GetFloat(DIALOGUE_VOL_STRING) : 1f;
        mouseSensitivity = PlayerPrefs.HasKey(MOUSE_SENSITIVITY_STRING) ? PlayerPrefs.GetFloat(MOUSE_SENSITIVITY_STRING) : 5f;

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
        SetDialogueVolume(dialogueVolume);
        SetMouseSensitivity(mouseSensitivity);
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

    public void SetMouseSensitivity(float sliderValue)
    {
        mouseSensitivity = sliderValue;
        GameManager.Instance.SetMouseSensitivity(sliderValue);
        updateSliderValue(mouseSensitivitySlider, sliderValue, 10f);
    }

    private void setVolume(string channel, float vol)
    {
        masterAudioMixer.SetFloat(channel, Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat(channel, vol);
    }

    private void updateSliderValue(Slider slider, float value, float factor = 100f)
    {
        slider.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = string.Format("{0}", Mathf.Round(value * factor));     
    }
}