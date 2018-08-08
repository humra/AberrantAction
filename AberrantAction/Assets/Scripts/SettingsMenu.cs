using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider musicSlider;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            if (CheckUniqueString(option, options))
            {
                options.Add(option);
            }
            
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullscreenToggle.isOn = Screen.fullScreen;

        float tempVolumeValue;
        mainMixer.GetFloat("Volume", out tempVolumeValue);
        volumeSlider.value = tempVolumeValue;
        musicMixer.GetFloat("MusicVolume", out tempVolumeValue);
        musicSlider.value = tempVolumeValue;
    }

    private bool CheckUniqueString(string testString, List<string> existingStrings)
    {
        foreach(string existingString in existingStrings)
        {
            if(existingString.Equals(testString))
            {
                return false;
            }
        }

        return true;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("MusicVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
