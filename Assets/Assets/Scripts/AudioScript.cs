using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
public class AudioScript : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // saved volumes 
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

     
        InitializeSounds(musicSounds, "Music");
        InitializeSounds(sfxSounds, "SFX");
    }

    private void InitializeSounds(Sound[] sounds, string outputMixerGroup)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixer.FindMatchingGroups(outputMixerGroup)[0];
        }
    }

    public void SetVolume(string name, Slider slider)
    {
        PlayerPrefs.SetFloat(name + "Volume", slider.value);

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

    public void SetMusicVolume()
    {
        SetVolume("Music", musicSlider);
    }

    public void SetSFXVolume()
    {
        SetVolume("SFX", sfxSlider);
    }
    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);
        if (s != null && s.source != null)
        {
            s.source.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s != null && s.source != null)
        {
            s.source.Play();
        }
    }
}
