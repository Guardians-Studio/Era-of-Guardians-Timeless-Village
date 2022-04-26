using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalBossTP : MonoBehaviour
{
    private bool isRdy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponentInParent<Player>();
            if (playerScript.gemmeCount > 0)
            {
                playerScript.StartFinal();
                isRdy = true;
            }
            else
            {
                playerScript.InfoFinal();
            }
        }
    }

    private void Update()
    {
        if (isRdy && Input.GetKey(KeyCode.F))
        {
            SceneManager.LoadScene("finalHazeltown");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponentInParent<Player>();
            if (playerScript.gemmeCount > 0)
            {
                playerScript.CancelFinal();
                isRdy = false;
            }
            else
            {
                playerScript.CancelInfoFinal();
            }
        }
    }
}
