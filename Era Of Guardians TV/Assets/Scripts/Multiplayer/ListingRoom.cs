using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ListingRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomListing roomListing;
    [SerializeField] Transform content;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            RoomListing listing = Instantiate(roomListing, content);
            if (listing != null)
            {
                listing.SetRoomInfo(roomInfo);
            }
        }
    }
}
