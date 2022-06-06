using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviourPunCallbacks
{
    [Header("Affectation")]
    [SerializeField] string versionName = "0.1";

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
    [SerializeField] GameObject pseudoSolo;
    [SerializeField] GameObject pseudoMenuJoin;

    [SerializeField] InputField userNameInput;
    [SerializeField] InputField userNameInputJoin;
    [SerializeField] InputField createGameInput;
    [SerializeField] InputField joinedGameInput;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject startButtonJoin;
    [SerializeField] GameObject soloStartButton;
    [SerializeField] InputField userNameInputSolo;
    [SerializeField] MainMenuSettings mainMenuSettings;
     [SerializeField] AudioSource ac;

    [SerializeField] Dropdown dropdownMaxPlayer;

    private RoomOptions roomOptions = new RoomOptions();
    
    private void Start()
    {
        if (mainMenuSettings.music)
        {
            ac.Play();
        }

        roomOptions.MaxPlayers = 4;
        mainMenu.SetActive(true);
    }

    public void Update()
    {
        if (!mainMenuSettings.music)
        {
            ac.mute = true;
        }
        else
        {
            ac.mute = false;
        }
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
    }

    public void Create()
    {
        gameCanvas.SetActive(false);
        createMenu.SetActive(true);
    }
    public void Join()
    {
        gameCanvas.SetActive(false);
        joinMenu.SetActive(true);
    }

    public void SettingsBack()
    {
        volumeMenu.SetActive(false);
        resolutionMenu.SetActive(false);
        keyConfigMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void PlayBack()
    {
        playMenu.SetActive(true);
        gameCanvas.SetActive(false);
        PhotonNetwork.Disconnect();
    }

    public void MenuBack()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SelectionBack()
    {
        joinMenu.SetActive(false);
        createMenu.SetActive(false);
        gameCanvas.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void PseudoSolo()
    {
        pseudoSolo.SetActive(true);
    }

    public void PseudoCreate()
    {
        pseudoMenu.SetActive(true);
    }

    public void PseudoJoin()
    {
        pseudoMenuJoin.SetActive(true);
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

    public void ChangeUserNameInputJoin()
    {
        if (userNameInputJoin.text.Length >= 1)
        {
            startButtonJoin.SetActive(true);
        }
        else
        {
            startButtonJoin.SetActive(false);
        }
    }

    public void ChangeUserNameInputSolo()
    {
        if (userNameInputSolo.text.Length >= 1)
        {
            soloStartButton.SetActive(true);
        }
        else
        {
            soloStartButton.SetActive(false);
        }
    }

    public void SoloPlay()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        settingsMenu.SetActive(false);
        volumeMenu.SetActive(false);
        resolutionMenu.SetActive(false);
        keyConfigMenu.SetActive(false);
        gameCanvas.SetActive(false);
        createMenu.SetActive(false);
        joinMenu.SetActive(false);
        pseudoMenu.SetActive(false);
        pseudoSolo.SetActive(false);

        SceneManager.LoadScene("hazeltown");
    }

    public void MultiPlay()
    {
        Connect();
        playMenu.SetActive(false);
        gameCanvas.SetActive(true);
        // pseudoMenu.SetActive(true);
    }

    public void SetUserName()
    {
        PhotonNetwork.NickName = userNameInput.text;
        
        pseudoMenu.SetActive(false);
        CreateGame();
    }

    public void SetUserNameJoin()
    {
        PhotonNetwork.NickName = userNameInputJoin.text;
        pseudoMenu.SetActive(false);
        JoinGame();
    }

    public void SetMaxPlayer()
    {
        if (dropdownMaxPlayer.value == 0)
        {
            roomOptions.MaxPlayers = 4;
        }
        if (dropdownMaxPlayer.value == 1)
        {
            roomOptions.MaxPlayers = 3;
        }
        if (dropdownMaxPlayer.value == 2)
        {
            roomOptions.MaxPlayers = 2;
        }
        if (dropdownMaxPlayer.value == 3)
        {
            roomOptions.MaxPlayers = 1;
        }
        print(roomOptions.MaxPlayers);
    }


    private void Connect()
    {
        print("Connecting to server.");
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected to server.");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason:" + cause.ToString());
    }

    public void CreateGame() // Pas de mess d'erreur si aucun nom d'instance n'est inscrite -- � fix
    {
        PhotonNetwork.CreateRoom(createGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("hazeltown");

        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }

    public void JoinGame()
    {
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(joinedGameInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("hazeltown");

        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }


    public void UpdateList()
    {
        PhotonNetwork.Disconnect();
        Connect();
    }
}