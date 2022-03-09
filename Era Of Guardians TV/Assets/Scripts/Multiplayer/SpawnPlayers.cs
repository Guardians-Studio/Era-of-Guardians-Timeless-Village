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
        if (PhotonNetwork.CurrentRoom != null && firstInstantiate)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z), Quaternion.identity);
            firstInstantiate = false;
        }    
    }

}
