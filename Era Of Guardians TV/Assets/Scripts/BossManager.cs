using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] Transform statue;
    [SerializeField] GameObject bossPrefab;

    private bool spawned = false;

    public GameObject endscreenPanel;
    public Text endscreenText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponentInParent<Player>();
            if (playerScript.gemmeCount > 0)
            {
                Instantiate(bossPrefab, statue);
                Destroy(statue.gameObject);
                spawned = true;
            }
        }  
    }

    private void Update()
    {
        if (GameObject.Find("FinalBoss") == null && spawned)
        {
            endscreenPanel.SetActive(true);
            endscreenText.text = "Tidalar est vaincu, bien joué !";
        }
    }
}
