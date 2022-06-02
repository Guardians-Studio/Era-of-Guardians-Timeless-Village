using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OkeanosTeleportation : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("okeanos"); //Can teleport to an another point in the same scene
        //SceneManager.LoadScene("okeanos");
    }
}