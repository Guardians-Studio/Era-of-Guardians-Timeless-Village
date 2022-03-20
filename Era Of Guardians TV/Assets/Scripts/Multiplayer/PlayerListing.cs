using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField] Text text;

    public void SetPlayerInfo(Photon.Realtime.Player player, string cause)
    {
        text.text = player.NickName + cause;
    }
}
