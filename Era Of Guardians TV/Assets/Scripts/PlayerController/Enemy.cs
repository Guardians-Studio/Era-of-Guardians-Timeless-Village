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
    [SerializeField] BasicEnemy2 bE2;

    private bool canAttack = true;

    [Header("Sword")]
    [SerializeField] GameObject sword, bow;
    [SerializeField] Sword swordScript;
    [SerializeField] Bow bowScript;
    Animator anim;
    AudioSource ac;

    private void Start()
    {
        ac = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (bE.attack)
        {
            AttackAuto();
        }

        if (bE2.attack)
        {
            BowAttacking();
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

        StartCoroutine(ResetAttackCooldown(swordScript.cooldown));
    }

    private void BowAttacking ()
    {
        bow.GetComponent<FightEnemyDetection>().enabled = true; 
        anim = bow.GetComponentInChildren<Animator>();
        canAttack = false;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown(bowScript.cooldown));
    }

    IEnumerator ResetAttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
