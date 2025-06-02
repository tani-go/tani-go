using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Toggle windowedToggle;
    private bool isChanging = false;
    [SerializeField] Slider volumeSlider; 

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }

        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            windowedToggle.isOn = true;
            fullscreenToggle.isOn = false;
        }
        else
        {
            fullscreenToggle.isOn = true;
            windowedToggle.isOn = false;
        }

        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        windowedToggle.onValueChanged.AddListener(OnWindowedToggleChanged);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void BackButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    private void OnFullscreenToggleChanged(bool isOn)
    {
        if (isChanging) return;

        if (isOn)
        {
            isChanging = true;
            windowedToggle.isOn = false;
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isChanging = false;
        }
    }

    private void OnWindowedToggleChanged(bool isOn)
    {
        if (isChanging) return;

        if (isOn)
        {
            isChanging = true;
            fullscreenToggle.isOn = false;
            Screen.fullScreenMode = FullScreenMode.Windowed;
            isChanging = false;
        }
    }
}
