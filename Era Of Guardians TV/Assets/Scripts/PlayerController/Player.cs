using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] UIPlayer uiPlayer;

    [SerializeField] GameObject finalPanel;
    [SerializeField] GameObject infoPanel;


    private string name;
    public float health = 80;
    public float maxHealth = 100;
    private float armor = 10;
    private List<GameObject> items;
    private float xpAmount = 10;
    private int level = 1;
    public int gemmeCount = 0;

    private bool inChat = false;

    private void Start()
    {
        XP(0);
        Heal(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) && inChat)
        {
            ResumeGame();
            StartCoroutine(Chat());
        }
        else if (Input.GetKey(KeyCode.Return) && !inChat)
        {
            PauseGame();
            StartCoroutine(Chat());
        }
    }
    public void StartFinal ()
    {
        finalPanel.SetActive(true);
    }

    public void InfoFinal()
    {
        infoPanel.SetActive(true);
    }

    public void CancelInfoFinal()
    {
        infoPanel.SetActive(false);
    }

    public void CancelFinal()
    {
        finalPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        this.health -= amount;
        healthBarExtern.UpdateHealth(this.health / maxHealth);
        uiPlayer.UpdateHealth(this.health / maxHealth, maxHealth);

        if (this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        this.health += amount;
        uiPlayer.UpdateHealth(this.health / maxHealth, maxHealth);
    }

    public void XP(float amount)
    {
        this.xpAmount += amount;
        if (xpAmount >= 100)
        {
            this.level++;
            xpAmount = 0;
            this.maxHealth += 10;
            this.health = maxHealth;
            Heal(0);
            uiPlayer.UpdateLevel(this.level);
        }
        uiPlayer.UpdateXPBar(this.xpAmount / 100);
    }


    private void Die()
    {
        Destroy(gameObject);
        PhotonNetwork.Disconnect();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PhotonNetwork.LoadLevel("MainMenu");
        
    }

    public void ResumeGame()
    {
        GetComponent<PlayerLook>().enabled = true;
        GetComponent<WeaponController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void PauseGame()
    {
        GetComponent<PlayerLook>().enabled = false;
        GetComponent<WeaponController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Chat()
    {
        yield return new WaitForSeconds(1);
        inChat = !inChat;
    }
}
