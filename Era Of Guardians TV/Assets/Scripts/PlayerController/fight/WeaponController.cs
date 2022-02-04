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

    private int selectedWeapon = 0;
    int previousSelectedWeapon;

    private bool canAttack = true;
    
    private float swordCooldown = 1f;
    private float bowCooldown = 1f;

    private Vector3 rayHit;

    Animator anim;
    AudioSource ac;

    private void Start()
    {
        ac = GetComponentInParent<AudioSource>();

        SelectWeapon();
    }

    private void Update()
    {
        previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
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
                selectedWeapon = transform.childCount - 1;
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
        else if (Input.GetKey(keyConfiguration.threeKey))// modif qd available
        {
            selectedWeapon = 1;
        }
        else if (Input.GetKey(keyConfiguration.fourKey))// modif qd available
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            return;
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
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in weaponHolder.transform)
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
        weaponHolder.transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }

    private void SwordAttack()
    {
        anim = sword.GetComponentInChildren<Animator>();

        canAttack = false;
        
        anim.SetTrigger("Attack");

        ac.PlayOneShot(swordScript.swordAttack);

        StartCoroutine(ResetAttackCooldown(swordCooldown));
    }

    private void BowAttack()
    {
        anim = bow.GetComponentInChildren<Animator>();

        canAttack = false;

        anim.SetTrigger("Attack");

        StartCoroutine(PrepareFire());

        StartCoroutine(ResetAttackCooldown(bowCooldown));
    }

    private void Fire()
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

        InstantiateProjectile(bowScript.projectileSpawnPoint.transform);

    }

    private void InstantiateProjectile(Transform firePoint)
    {
       GameObject bullet = Instantiate(bowScript.projectile, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (rayHit - firePoint.position).normalized * bowScript.projectileSpeed;
    }

    IEnumerator PrepareFire()
    {
        yield return new WaitForSeconds(1f);
        ac.PlayOneShot(bowScript.bowAttack);
        yield return new WaitForSeconds(1f);
        Fire();
        

    }

    IEnumerator ResetAttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
