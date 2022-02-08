using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBar healthBarExtern;
    [SerializeField] HealthBar healthBarIntern;

    PhotonView view;

    private string name;
    private float health = 100;
    private float armor = 10;
    private List<GameObject> items;
    private int level = 1;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBarExtern.UpdateHealth(this.health / 100);
        healthBarIntern.UpdateHealth(this.health / 100);

        if (this.health <= 0)
        {
            Die();
        }
    }

    private void Heal(float heal)
    {
        this.health += heal;
    }

    private void Die()
    {
        Destroy(gameObject);
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
