using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] Transform statue;
    [SerializeField] GameObject bossPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponentInParent<Player>();
            if (playerScript.gemmeCount > 0)
            {
                Instantiate(bossPrefab, statue);
                Destroy(statue.gameObject);
            }
        }  
    }
}
