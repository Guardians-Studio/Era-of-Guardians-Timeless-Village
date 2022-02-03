using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("GameObject Assignement")]
    public KeyConfiguration keyConfiguration;
    [Header("Sword")]
    [SerializeField] GameObject sword;
    [SerializeField] AudioClip swordAttack;
    // [SerializeField] AudioClip swordMiss;
    [Header("Bow")]
    [SerializeField] GameObject bow;
    [SerializeField] GameObject arrow;
    [SerializeField] AudioClip bowAttack;
    
    private bool canAttack = true;
    
    private float swordCooldown = 1f;
    private float bowCooldown = 2.5f;

    private GameObject swordObject;
    private GameObject bowObject;

    Animator anim;
    AudioSource ac;

    private void Start()
    {
        anim = sword.GetComponent<Animator>();
        ac = GetComponent<AudioSource>();

        swordObject = GetComponentInParent<Transform>().gameObject;
        bowObject = GetComponentInParent<Transform>().gameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(keyConfiguration.leftMouseKey) && swordObject.activeSelf)
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }

        if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && bowObject.activeSelf)
        {
            if (canAttack)
            {
                BowAttack();
            }
        }
    }

    private void SwordAttack()
    {
        canAttack = false;
        
        anim.SetTrigger("Attack");

        ac.PlayOneShot(swordAttack);

        StartCoroutine(ResetAttackCooldown(swordCooldown));
    }

    private void BowAttack()
    {
        canAttack = false;

        anim.SetTrigger("Attack");

        ac.PlayOneShot(bowAttack);
        StartCoroutine(PrepareFire());

        Fire();

        StartCoroutine(ResetAttackCooldown(bowCooldown));
    }

    private void Fire()
    {
        Instantiate(arrow, this.transform);
    }

    IEnumerator PrepareFire()
    {
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator ResetAttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
