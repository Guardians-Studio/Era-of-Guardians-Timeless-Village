using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Affectation")]
    [SerializeField] string versionName = "0.1";
    /*[SerializeField] GameObject userName;
    [SerializeField] GameObject connectPannel;*/


    [Header("Canvas")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject playMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject volumeMenu;
    [SerializeField] GameObject resolutionMenu;
    [SerializeField] GameObject keyConfigMenu;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject createMenu;
    [SerializeField] GameObject joinMenu;
    [SerializeField] GameObject pseudoMenu;
    [SerializeField] GameObject pseudoSoloMenu;

    [SerializeField] InputField userNameInput;
    [SerializeField] InputField createGameInput;
    [SerializeField] InputField joindGameInput;

    [SerializeField] GameObject startButton;
    [SerializeField] AudioSource ac;

    private void Start()
    {
        ac.Play();
        mainMenu.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Volume()
    {
        settingsMenu.SetActive(false);
        volumeMenu.SetActive(true);
    }

    public void Resolution()
    {
        settingsMenu.SetActive(false);
        resolutionMenu.SetActive(true);
    }

    public void KeyConfig()
    {
        settingsMenu.SetActive(false);
        keyConfigMenu.SetActive(true);
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        Connect();
    }

    public void Create()
    {
        playMenu.SetActive(false);
        createMenu.SetActive(true);
    }
    public void Join()
    {
        playMenu.SetActive(false);
        joinMenu.SetActive(true);
    }

    public void SettingsBack()
    {
        volumeMenu.SetActive(false);
        resolutionMenu.SetActive(false);
        keyConfigMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void MenuBack()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Connect()
    {
        print("Connecting to server.");
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected to server.");
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason:" + cause.ToString());
    }

    public void Pseudo()
    {
        pseudoMenu.SetActive(true);
    }

    public void ChangeUserNameInput()
    {
        if (userNameInput.text.Length >= 1)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void SoloPlay()
    {
        SceneManager.LoadScene("hazeltown");
    }

    public void MultiPlay()
    {
        playMenu.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void SetUserName()
    {
        PhotonNetwork.playerName = userNameInput.text;
    }
    

    public void CreateGame() // Pas de mess d'erreur si aucun nom d'instance n'est inscrite -- � fix
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("hazeltown");
        
        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(joindGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("hazeltown");

        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }

}
