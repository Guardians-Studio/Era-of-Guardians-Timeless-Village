using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuController : MonoBehaviour
{
    [Header("Affectation")]
    [SerializeField] string versionName = "0.1";
    [SerializeField] GameObject userName;
    [SerializeField] GameObject connectPannel;

    [SerializeField] GameObject gameCanvas;

    [SerializeField] GameObject mainMenu;
    [SerializeField] Button playButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;

    [SerializeField] GameObject settingsMenu;

    [SerializeField] InputField userNameInput;
    [SerializeField] InputField createGameInput;
    [SerializeField] InputField joindGameInput;


    [SerializeField] GameObject startButton;
    [SerializeField] AudioSource ac;

    private void Awake()
    {
        print("Connecting to server.");
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

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

    public void Play()
    {
        mainMenu.SetActive(false);
        userName.SetActive(true);
    }

    public void menu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
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

    public void SetUserName()
    {
        userName.SetActive(false);
        gameCanvas.SetActive(true);
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
