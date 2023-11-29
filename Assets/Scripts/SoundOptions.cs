using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    void Start()
    {
        if (PlayerPrefs.GetInt("Default Volume Changed") == 0)
        {
            masterSlider.value = .1f;
            sfxSlider.value = .1f;
            musicSlider.value = .1f;
            PlayerPrefs.SetInt("Default Volume Changed", 1);
        }
        else
        {
            masterSlider.value = PlayerPrefs.GetFloat("Master");
            musicSlider.value = PlayerPrefs.GetFloat("Music");
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        }

    }

    void SetVolume(string name, Slider slider)
    {
        PlayerPrefs.SetFloat(name, slider.value);

        float volume = Mathf.Log10(slider.value) * 20;
        if (slider.value == 0)
        {
            volume = -80;
        }
        mixer.SetFloat(name, volume);
    }

    public void SetMasterVolume()
    {
        SetVolume("Master", masterSlider);
    }
    public void SetSFXVolume()
    {
        SetVolume("SFX", sfxSlider);
    }
    public void SetMusicVolume()
    {
        SetVolume("Music", musicSlider);
    }
}