using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    public Dropdown DResolution;
    public AudioSource audiosource;
    public Slider slider;
    public Text TxtVolume;

    public bool fullScreen = true;
    public bool music = true;

    void Start()
    {
        SetVolume();
    }

    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 2:
                Screen.SetResolution(720, 576, true);
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                break;
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void FullScreen()
    {
        fullScreen = !fullScreen;
        Screen.fullScreen = fullScreen;
    }

    public void SetVolume()
    {
        audiosource.volume = slider.value;
        TxtVolume.text = "volume " + (audiosource.volume * 100).ToString("00") + "%";
    }

    public void Music()
    {
        music = !music;
    }
}
