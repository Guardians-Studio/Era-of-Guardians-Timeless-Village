using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBar healthBarExtern;
    [SerializeField] HealthBar healthBarUI;

    private string name;
    private float health = 100;
    private float armor = 10;
    private List<GameObject> items;
    private int level = 1;

    public void TakeDamage(float amount)
    {
        this.health -= amount;
        healthBarExtern.UpdateHealth(this.health / 100);
        healthBarUI.UpdateHealth(this.health / 100);

        if (this.health <= 0)
        {
            Die();
        }
    }

    private void Heal(float amount)
    {
        this.health += amount;
    }

    private void Die()
    {
        Destroy(gameObject);
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
