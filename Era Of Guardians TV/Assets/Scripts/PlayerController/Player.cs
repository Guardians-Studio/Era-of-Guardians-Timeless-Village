using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] UIPlayer uiPlayer;

    private string name;
    public float health = 80;
    public float maxHealth = 100;
    private float armor = 10;
    private List<GameObject> items;
    private float xpAmount = 10;
    private int level = 1;
    public int gemmeCount = 0;

    private void Start()
    {
        XP(0);
        Heal(0);
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
}
