using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;

    private string name;
    private float health = 100;
    private float armor = 10;
    private List<GameObject> items;
    private int level = 1;

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBar.UpdateHealth(this.health / 100);

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
    }
}
