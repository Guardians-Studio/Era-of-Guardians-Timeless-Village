using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] Transform statue;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Player player;

    private bool spawned = false;

    public GameObject endscreenPanel;
    public Text endscreenText;

    private void Start()
    {
        endscreenPanel.SetActive(false);
    }

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
        if (GameObject.Find("FinalBoss") == null)
        {
            endscreenPanel.SetActive(true);
            endscreenText.text = "Tidalar est vaincu, bien joué !";
            player.GetComponent<PlayerLook>().enabled = false;
            player.GetComponent<WeaponController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
        PhotonNetwork.Disconnect();
    }
}
