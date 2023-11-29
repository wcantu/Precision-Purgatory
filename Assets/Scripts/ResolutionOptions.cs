using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOptions : MonoBehaviour
{
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Toggle toggle;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        bool setDefault = false;
        if (PlayerPrefs.GetInt("set default resolution") == 0)
        {
            setDefault = true;
            PlayerPrefs.GetInt("set default resolution", 1);
        }

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolutionString));
            if (setDefault && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionDropdown.value = i;
            }
        }
        toggle.isOn = PlayerPrefs.GetInt("fullscreen") == 0;
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution selection");

    }

    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, toggle.isOn);
        PlayerPrefs.SetInt("resolution selection", resolutionDropdown.value);


    }
    public void ChangeFullscreen()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, toggle.isOn);
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("f ullscreen", 0);
        }
        else
        {
            Debug.Log("fullscreen off");
            PlayerPrefs.SetInt("fullscreen", 1);
        }
    }
}
