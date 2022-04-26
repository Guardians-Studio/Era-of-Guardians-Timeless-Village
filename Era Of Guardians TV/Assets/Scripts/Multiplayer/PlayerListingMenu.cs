using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing playerListing;
    [SerializeField] Transform content;
    [SerializeField] PhotonChatClient photonChatClient;

    private List<PlayerListing> _listing = new List<PlayerListing>();

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }

        photonChatClient.userID = PhotonNetwork.CurrentRoom.Players[0].NickName;
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
        // photonChatClient.SendPublicMessage("MainChannel", newPlayer.NickName + "has joined the room");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int i = 0;
        int n = _listing.Count;

        Destroy(_listing[n - 1].gameObject);
        _listing.Remove(_listing[n - 1]);

        foreach (PlayerListing playerListing in _listing)
        {
            playerListing.SetPlayerInfo(PhotonNetwork.PlayerList[i]);
            i++;
        }
    }
}
