using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera cam;
    public KeyConfiguration keyConfiguration;
    [SerializeField] GameObject weaponHolder;
    [Header("Sword")]
    [SerializeField] GameObject sword;
    [SerializeField] Sword swordScript;
    // [SerializeField] AudioClip swordMiss;
    [Header("Bow")]
    [SerializeField] GameObject bow;
    [SerializeField] Bow bowScript;
    [Header("Wand")]
    [SerializeField] GameObject wand;
    [SerializeField] Wand wandScript;

    [SerializeField] GameObject[] weapons;
    private int selectedWeapon = 0;

    private bool canAttack;
  
    private Vector3 rayHit;

    Animator anim;
    AudioSource ac;

    private void Start()
    {
        ac = GetComponentInParent<AudioSource>();
        canAttack = true;


    SelectWeapon();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= weapons.Length - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = weapons.Length - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        else if (Input.GetKey(keyConfiguration.oneKey))
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKey(keyConfiguration.twoKey))
        {
            selectedWeapon = 1;
        }
        else if (Input.GetKey(keyConfiguration.threeKey))
        {
            selectedWeapon = 2;
        }
        else if (Input.GetKey(keyConfiguration.fourKey))// modif qd available
        {
            selectedWeapon = 2;
        }

        SelectWeapon();

        if (Input.GetMouseButtonDown(keyConfiguration.leftMouseKey) && sword.activeSelf)
        {
            if (canAttack)
            {
                SwordAttack();
            }
        }

        if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && bow.activeSelf)
        {
            if (canAttack)
            {
                BowAttack();
            }
        }

        if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && wand.activeSelf)
        {
            if (canAttack)
            {
                WandAttack();
            }
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (GameObject weapon in weapons)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SwordAttack()
    {
        sword.GetComponent<FightDetection>().enabled = true; // to disable attack just by passing on enemy
        
        anim = sword.GetComponentInChildren<Animator>();

        canAttack = false;
        
        anim.SetTrigger("Attack");

        ac.PlayOneShot(swordScript.swordAttack);

        StartCoroutine(ResetAttackCooldown(swordScript.cooldown));
    }

    private void BowAttack()
    {
        anim = bow.GetComponentInChildren<Animator>();

        canAttack = false;

        anim.SetTrigger("Attack");

        StartCoroutine(PrepareWandFire());

        StartCoroutine(ResetAttackCooldown(bowScript.cooldown));
    }

    private void WandAttack()
    {
        anim = wand.GetComponentInChildren<Animator>();

        canAttack = false;

        anim.SetTrigger("Attack");

        StartCoroutine(PrepareWandFire());

        Debug.Log(wandScript.cooldown + "é");
        StartCoroutine(ResetAttackCooldown(wandScript.cooldown));
    }

    private void WandFire()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            rayHit = hit.point;
        }
        else
        {
            rayHit = ray.GetPoint(1000);        
        }

        InstantiateWandProjectile(wandScript.wandProjectileSpawnPoint.transform);

    }

    private void InstantiateWandProjectile(Transform firePoint)
    {
       GameObject bullet = Instantiate(wandScript.wandProjectile, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (rayHit - firePoint.position).normalized * wandScript.projectileSpeed;
    }

    IEnumerator PrepareWandFire()
    {
        yield return new WaitForSeconds(1f);
        ac.PlayOneShot(wandScript.wandAttack);
        WandFire();
    }

    IEnumerator ResetAttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
