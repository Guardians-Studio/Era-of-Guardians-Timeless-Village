using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

public class PhotonChatClient : MonoBehaviour, IChatClientListener
{
    [SerializeField] string appID;
    public string userID = "";
    private string appVersion = "0.0.1";

    public ChatMessageDisplayer chatMessageDisplayer;
    public ChatMessageSender chatMessageSender;
    private ChatClient chatClient;

    // Start is called before the first frame update
    void Start()
    {
        chatClient = new ChatClient(this);
        if (PhotonNetwork.CurrentRoom == null)
        {
            userID = "Solo";
        }
        chatMessageSender.SetUserName(userID);

        chatClient.ChatRegion = "EU";
        chatClient.Connect(appID, appVersion, new AuthenticationValues(userID));
    }

    // Update is called once per frame
    void Update()
    {
        chatClient.Service();
    }

    public void SendPublicMessage(string _channel, string message)
    {
        chatClient.PublishMessage(_channel, message);
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log(state);
    }

    public void OnConnected()
    {
        //chatMessageDisplayer.DisplayMessage(userID + " joined the room !");
        chatClient.Subscribe("MainChannel");
        SendPublicMessage("MainChannel", "joined the room !");
    }

    public void OnDisconnected()
    {
        Debug.Log("OnDisconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatMessageDisplayer.DisplayMessage(senders[i] + " : " + messages[i].ToString());
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (var channel in channels)
        {
            // chatMessageDisplayer.DisplayMessage("Subscribed to " + channel);
            // SendPublicMessage(channel, "Bonjour");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
