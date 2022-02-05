using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuController : MonoBehaviour
{
    [Header("GameObject Attribution")]
    [SerializeField] string versionName = "0.1";
    [SerializeField] GameObject userNameMenu;
    [SerializeField] GameObject connectPannel;

    [SerializeField] InputField userNameInput;
    [SerializeField] InputField createGameInput;
    [SerializeField] InputField joindGameInput;

    [SerializeField] GameObject startButton;

    private void Awake()
    {
        print("Connecting to server.");
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    private void Start()
    {
        userNameMenu.SetActive(true);
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
        userNameMenu.SetActive(false);
        PhotonNetwork.playerName = userNameInput.text;
    }
    

    public void CreateGame() // Pas de mess d'erreur si aucun nom d'instance n'est inscrite -- à fix
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("testScene");
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
        PhotonNetwork.LoadLevel("testScene");

        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }

}
