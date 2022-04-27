using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject spawnpoint;

    private bool firstInstantiate = true;

    private void Update()
    {
        if (!PhotonNetwork.IsConnected && firstInstantiate)
        {
            print("soloPlaying");
            Instantiate(playerPrefab, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z), Quaternion.identity);
            firstInstantiate = false;
        }

        if (PhotonNetwork.CurrentRoom != null && firstInstantiate)
        {
            print("multiPlaying on " + PhotonNetwork.CurrentRoom.Name);
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z), Quaternion.identity);
            firstInstantiate = false;
        }    
    }

}
