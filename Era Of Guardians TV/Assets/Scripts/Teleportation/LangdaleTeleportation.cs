using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LangdaleTeleportation : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform teleportTarget;
    public GameObject thePlayer;

    void OnTriggerEnter(Collider other)
    {
        //thePlayer.transform.position = teleportTarget.transform.position; //Can teleport to an another point in the same scene
        SceneManager.LoadScene("langdale");
    }
}
