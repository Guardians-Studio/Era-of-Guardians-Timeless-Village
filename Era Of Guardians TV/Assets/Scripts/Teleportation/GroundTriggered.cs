using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTriggered : MonoBehaviour
{
    public Transform teleportTarget;
    //public GameObject thePlayer;

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportTarget.transform.position; //Can teleport to an another point in the same scene
    }
}
