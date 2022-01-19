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
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    private void Start()
    {
        userNameMenu.SetActive(true);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
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
}
