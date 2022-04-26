using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalBossTP : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponentInParent<Player>();
            if (playerScript.gemmeCount > 0)
            {
                playerScript.StartFinal();
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("finalHazeltown");
                    Debug.Log("B has been pressed");
                }
            }
        }
    }
}
