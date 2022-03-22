using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
    [SerializeField] Text text;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        text.text = "  " + roomInfo.Name + " | " + "max player : " + roomInfo.MaxPlayers;
    }

    /*public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
        PhotonNetwork.LoadLevel("hazeltown");

        if (PhotonNetwork.IsConnected)
        {
            print("Connected in the room");
        }
    }*/
}
