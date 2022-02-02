using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("GameObject Assignement")]
    [SerializeField] GameObject sword;
    [SerializeField] AudioClip swordAttack;
    [SerializeField] AudioClip swordMiss;
    public KeyConfiguration keyConfiguration;

    private bool canAttack = true;
    private bool isAttacking = false;
    private float swordCooldown = 1f;

    Animator anim;
    AudioSource ac;

    private void Start()
    {
        anim = sword.GetComponent<Animator>();
        ac = GetComponent<AudioSource>();
    }

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

        anim.SetTrigger("Attack");
        ac.PlayOneShot(swordAttack);

        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(swordCooldown);
        canAttack = true;
    }
}
