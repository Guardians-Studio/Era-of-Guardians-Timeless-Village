using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CeltiaTeleportation : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("celtia"); //Can teleport to an another point in the same scene
        //SceneManager.LoadScene("celtia");
    }
}
