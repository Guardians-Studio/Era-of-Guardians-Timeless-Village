using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject Menu;
    public GameObject settingsWindow;
    bool visible = false;
    public Dropdown DResolution;
    public AudioSource audiosource;
    public Slider slider;
    public Text TxtVolume;
    public Player player;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            if (visible)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }


    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(640,360,true);
                break;
            case 1:
                Screen.SetResolution(1920,1080,true);
                break;
        }
    }
    public void SliderChange()
    {
        audiosource.volume = slider.value;
        TxtVolume.text = "volume " + (audiosource.volume * 100).ToString("00") + "%";
    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
        visible=false;
        settingsWindow.SetActive(false);

        player.GetComponent<PlayerLook>().enabled = true;
        player.GetComponent<WeaponController>().enabled = true;
    }
    void PauseGame()
    {
        Menu.SetActive(true);
        settingsWindow.SetActive(false);
        
        player.GetComponent<PlayerLook>().enabled = false;
        player.GetComponent<WeaponController>().enabled = false;
    }
    public void LoadSettings()
    {
        Menu.SetActive(false);
        settingsWindow.SetActive(visible);
        visible = !visible;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");  //Pour charger la Scene du menu d'accueil faut modif
    }
}
