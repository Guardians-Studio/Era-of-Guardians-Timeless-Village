using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing playerListing;
    [SerializeField] Transform content;

    private List<PlayerListing> _listings = new List<PlayerListing>();

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        PlayerListing listing = Instantiate(playerListing, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(newPlayer);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
