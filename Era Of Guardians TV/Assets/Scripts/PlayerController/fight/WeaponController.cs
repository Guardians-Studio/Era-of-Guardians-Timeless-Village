using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera cam;
    public KeyConfiguration keyConfiguration;
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

    [Header("UI Player Cooldown")]
    [SerializeField] Image cooldownImage;
    [SerializeField] Image cdFinishedImage;

    [SerializeField] Image[] weaponsImages;

    private float cooldownTime;
    private float cooldownTimer;
    private bool isCooldown = false;

    private bool canAttack;
  
    private Vector3 rayHit;

    Animator anim;
    AudioSource ac;

    private void Start()
    {
        ac = GetComponentInParent<AudioSource>();
        canAttack = true;
        cooldownImage.fillAmount = 0.0f;

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
                cooldownTime = swordScript.cooldown;
                cooldownTimer = cooldownTime;
                isCooldown = true;
            }
        }

        if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && bow.activeSelf)
        {
            if (canAttack)
            {
                BowAttack();
                cooldownTime = bowScript.cooldown;
                cooldownTimer = cooldownTime;
                isCooldown = true;
            }
        }

        if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && wand.activeSelf)
        {
            if (canAttack)
            {
                WandAttack();
                cooldownTime = wandScript.cooldown;
                cooldownTimer = cooldownTime;
                isCooldown = true;
            }
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
    }
    private void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        cdFinishedImage.gameObject.SetActive(true);
        cooldownImage.gameObject.SetActive(true);

        if (cooldownTimer < 0.0f)
        {
            isCooldown = false;
            cooldownImage.gameObject.SetActive(false);
            cdFinishedImage.gameObject.SetActive(false);
            cooldownImage.fillAmount = 0.0f;
        }
        else
        {
            cooldownImage.fillAmount = cooldownTimer / cooldownTime;
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
        i = 0;
        foreach(Image img in weaponsImages)
        {
            if (i == selectedWeapon)
            {
                img.gameObject.SetActive(true);
            }
            else
            {
                img.gameObject.SetActive(false);
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

        StartCoroutine(PreparebowFire());

        StartCoroutine(ResetAttackCooldown(bowScript.cooldown));
    }

    private void WandAttack()
    {
        anim = wand.GetComponentInChildren<Animator>();

        canAttack = false;

        anim.SetTrigger("Attack");

        StartCoroutine(PrepareWandFire());

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

    private void BowFire()
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

        InstantiateBowProjectile(bowScript.bowProjectileSpawnPoint.transform);

    }


    private void InstantiateBowProjectile(Transform firePoint)
    {
        GameObject bullet = Instantiate(bowScript.bowProjectile, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (rayHit - firePoint.position).normalized * wandScript.projectileSpeed;
    }

    private void InstantiateWandProjectile(Transform firePoint)
    {
       GameObject bullet = Instantiate(wandScript.wandProjectile, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = (rayHit - firePoint.position).normalized * wandScript.projectileSpeed;
    }

    IEnumerator PreparebowFire()
    {
        yield return new WaitForSeconds(1f);
        ac.PlayOneShot(wandScript.wandAttack);
        BowFire();
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
