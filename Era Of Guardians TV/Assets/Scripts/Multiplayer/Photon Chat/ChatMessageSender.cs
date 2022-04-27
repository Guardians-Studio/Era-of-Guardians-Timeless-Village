using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatMessageSender : MonoBehaviour
{
    public PhotonChatClient photonChatClient;
    public InputField inputField;
    public TMP_Text userName;
    public TMP_Text PseudoUIOpenWorld;

    private void Start()
    {
        inputField.text = "";
    }

    public void SendMessageToPhotonChat()
    {
        string message = inputField.text;
        if (!string.IsNullOrEmpty(message))
        {
            photonChatClient.SendPublicMessage("MainChannel", message);
        }
    }

    public void SetUserName(string _userName)
    {
        userName.SetText(_userName);
        // PseudoUIOpenWorld.SetText(_userName);
    }
}
