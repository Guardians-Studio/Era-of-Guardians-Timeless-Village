using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListingChat : MonoBehaviour
{
    [SerializeField] Text text;

    public void SetPlayerInfo(Photon.Realtime.Player player, string cause)
    {
        text.text = player.NickName + " " + cause;
    }
}