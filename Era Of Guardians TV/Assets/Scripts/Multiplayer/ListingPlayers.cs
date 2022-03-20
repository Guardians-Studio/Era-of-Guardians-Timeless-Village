using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ListingPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing playerListing;
    [SerializeField] Transform content;

    private List<PlayerListing> _listing = new List<PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayer();
    }

    private void GetCurrentRoomPlayer()
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo  in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Photon.Realtime.Player player)
    {
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            _listing.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        print("entered");
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(newPlayer, " join the room");
            _listing.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        print("left");
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(otherPlayer, " left the room");
            _listing.Remove(listing);
        }
    }
}
