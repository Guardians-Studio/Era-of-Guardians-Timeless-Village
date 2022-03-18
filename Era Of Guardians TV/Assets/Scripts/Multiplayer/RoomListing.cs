using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    [SerializeField] Text text;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        text.text = roomInfo.MaxPlayers + " max players in " + roomInfo.Name + " room";
    }
}
