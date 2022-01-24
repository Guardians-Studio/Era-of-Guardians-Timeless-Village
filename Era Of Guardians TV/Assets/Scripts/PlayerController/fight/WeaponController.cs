using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("GameObject Assignement")]
    [SerializeField] GameObject sword;
    [SerializeField] bool canAttack = true;
    public bool isAttacking = false;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] AudioClip swordAttack;
    [SerializeField] AudioClip swordMiss;
    public KeyConfiguration keyConfiguration;

    private void Update()
    {
        if (Input.GetMouseButtonDown(keyConfiguration.leftMouseKey))
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }
    }

    private void SwordAttack()
    {
        canAttack = false;
        isAttacking = true;

        Animator anim = sword.GetComponent<Animator>();
        AudioSource ac = GetComponent<AudioSource>();

        anim.SetTrigger("Attack");
        ac.PlayOneShot(swordAttack);


        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
