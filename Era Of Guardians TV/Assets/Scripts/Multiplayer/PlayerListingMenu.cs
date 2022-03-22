using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing playerListing;
    [SerializeField] PlayerListingChat playerListingChat;
    [SerializeField] Transform content;
    [SerializeField] Transform contentChat;

    private List<PlayerListing> _listing = new List<PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            print(playerInfo.Value.NickName + " nom ");
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Photon.Realtime.Player player)
    {
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listing.Add(listing);
        }
    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        PlayerListingChat listingChat = Instantiate(playerListingChat, contentChat);
        listingChat.SetPlayerInfo(newPlayer, "has joined the room");
        Destroy(listingChat, 5);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        AddPlayerListing(otherPlayer);
        PlayerListingChat listingChat = Instantiate(playerListingChat, contentChat);
        listingChat.SetPlayerInfo(otherPlayer, "has left the room");
        Destroy(listingChat, 5);
    }
}
