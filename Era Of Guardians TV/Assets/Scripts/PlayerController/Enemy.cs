using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] HealthBarExtern healthBarExtern;

    private string name;
    private float health = 100;
    private float armor = 10;
    private List<GameObject> items;
    // public float xpValue = 10;
    private int level = 1;
    [SerializeField] BasicEnemy bE;

    private bool canAttack = true;

    [Header("Sword")]
    [SerializeField] GameObject sword;
    [SerializeField] Sword swordScript;
    Animator anim;
    AudioSource ac;

    private void Start()
    {
        ac = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (bE.attack && canAttack)
        {
            AttackAuto();
        }
    }

    public bool TakeDamage(float amount)
    {
        Debug.Log(this.health);
        this.health -= amount;
        healthBarExtern.UpdateHealth(this.health / 100);

        if (this.health <= 0)
        {
            Die();
            return false;
        }

        return true;
    }

    private void Heal(float amount)
    {
        this.health += amount;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void AttackAuto()
    {
        sword.GetComponent<FightEnemyDetection>().enabled = true;

        canAttack = false;

        anim = sword.GetComponentInChildren<Animator>();

        anim.SetTrigger("Attack");

        // ac.PlayOneShot(swordScript.swordAttack);

        StartCoroutine(ResetAttackCooldown(swordScript.cooldown * 1.5f));
    }

    IEnumerator ResetAttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
