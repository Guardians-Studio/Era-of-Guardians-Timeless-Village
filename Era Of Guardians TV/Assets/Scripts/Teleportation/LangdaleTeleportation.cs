using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LangdaleTeleportation : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] Transform pos;
    
    void OnTriggerEnter(Collider other)
    {
        //other.transform.position = pos.position; //Can teleport to an another point in the same scene
        SceneManager.LoadScene("cineLangdale");
    }
}
