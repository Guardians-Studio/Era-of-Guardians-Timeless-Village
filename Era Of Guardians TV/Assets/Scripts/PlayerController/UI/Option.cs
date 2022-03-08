using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] KeyConfiguration keyConfiguration;
    public GameObject Settings;
    bool visible = false;
    
    public Dropdown DResolution;
    public AudioSource audiosource;
    public Slider slider;
    public Text TxtVolume;
    public Player player;


    void Start()
    {
        SetVolume();
    }
    void Update()
    {
        if (Input.GetKeyDown(keyConfiguration.escape))
        {
            visible =! visible;
            if (visible)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PauseGame();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ResumeGame();
            }
        }
    }


   public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(720,576,true);
                break;
            case 1:
                Screen.SetResolution(1280,720,true);
                break;
            case 2:
                Screen.SetResolution(1920,1080,true);
                break;
        }
    }
    public void SetVolume()
    {
        audiosource.volume = slider.value;
        TxtVolume.text = "volume " + (audiosource.volume * 100).ToString("00") + "%";
    }

    public void ResumeGame()
    {
        player.GetComponent<PlayerLook>().enabled = true;
        player.GetComponent<WeaponController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Menu.SetActive(false);
        visible=false;
        Settings.SetActive(false);
    }
    void PauseGame()
    {
        player.GetComponent<PlayerLook>().enabled = false;
        player.GetComponent<WeaponController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Menu.SetActive(true);
        Settings.SetActive(false);
    }
    public void LoadSettings()
    {
        Menu.SetActive(false);
        Settings.SetActive(visible);
        visible = !visible;
    }
    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");  //Pour charger la Scene du menu d'accueil faut modif
    }
}
