using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBarExtern healthBarExtern;
    [SerializeField] UIPlayer uiPlayer;

    private string name;
    private float health = 100;
    private float armor = 10;
    private List<GameObject> items;
    public float xpAmount = 50;
    private int level = 1;

    private void Start()
    {
        XP(0);
        Heal(0);
    }

    public void TakeDamage(float amount)
    {
        this.health -= amount;
        healthBarExtern.UpdateHealth(this.health / 100);
        uiPlayer.UpdateHealth(this.health / 100);

        if (this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        this.health += amount;
        uiPlayer.UpdateHealth(this.health / 100);
    }

    public void XP(float amount)
    {
        this.xpAmount += amount;
        if (xpAmount >= 100)
        {
            this.level++;
            xpAmount = 0;
            // uiPlayer.UpdateLvl;
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
