using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera cam;
    public KeyConfiguration keyConfiguration;
    [SerializeField] GameObject capsule;
    [SerializeField] Image[] selectors;
    [Header("Sword")]
    [SerializeField] GameObject sword;
    [SerializeField] Sword swordScript;
    [Header("Axe")]
    [SerializeField] GameObject axe;
    [SerializeField] Axe axeScript;
    [Header("Bow")]
    [SerializeField] GameObject bow;
    [SerializeField] Bow bowScript;
    [Header("Wand")]
    [SerializeField] GameObject wand;
    [SerializeField] Wand wandScript;
    [Header("Shield")]
    [SerializeField] GameObject shield;
    [SerializeField] Shield shieldScript;
    [Header("Shield")]
    [SerializeField] GameObject healthPot;
    [Header("Shield")]
    [SerializeField] GameObject xpPot;

    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] weapons1;
    [SerializeField] GameObject[] weapons2;
    [SerializeField] GameObject[] weapons3;

    private int selectedAbility = 0;
    private int selectedWeapon0 = 0;
    private int selectedWeapon1 = 0;
    private int selectedWeapon2 = 0;
    private int selectedWeapon3 = 0;


    [Header("UI Player Cooldown")]
    [SerializeField] Image cooldownImage;
    [SerializeField] Image cdFinishedImage;

    [SerializeField] Image[] weaponsImages;
    [SerializeField] Image[] weaponsImages1;
    [SerializeField] Image[] weaponsImages2;
    [SerializeField] Image[] weaponsImages3;

    private float cooldownTime;
    private float cooldownTimer;
    private bool isCooldown = false;

    private bool canAttack;
  
    private Vector3 rayHit;

    Animator anim;
    AudioSource ac;
    PhotonView view;

    private Player player;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine || PhotonNetwork.CurrentRoom == null)
        {
            selectors[0].gameObject.SetActive(true);
            SelectWeapon();
            player = GetComponent<Player>();
            ac = GetComponentInParent<AudioSource>();
            canAttack = true;
            cooldownImage.fillAmount = 0.0f;
        }   
    }

    private void Update()
    {
        if (view.IsMine || PhotonNetwork.CurrentRoom == null)
        {

            SelectAbility();

            if (selectedAbility == 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (selectedWeapon0 >= weapons.Length - 1)
                    {
                        selectedWeapon0 = 0;
                    }
                    else
                    {
                        selectedWeapon0++;
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (selectedWeapon0 <= 0)
                    {
                        selectedWeapon0 = weapons.Length - 1;
                    }
                    else
                    {
                        selectedWeapon0--;
                    }
                }
                else if (Input.GetKey(keyConfiguration.oneKey))
                {
                    selectedWeapon0 = 0;
                }
                else if (Input.GetKey(keyConfiguration.twoKey))
                {
                    selectedWeapon0 = 1;
                }
                else if (Input.GetKey(keyConfiguration.threeKey))
                {
                    selectedWeapon0 = 2;
                }
                else if (Input.GetKey(keyConfiguration.fourKey))
                {
                    selectedWeapon0 = 3;
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

                if (Input.GetMouseButtonDown(keyConfiguration.leftMouseKey) && axe.activeSelf)
                {
                    if (canAttack)
                    {
                        AxeAttack();
                        cooldownTime = axeScript.cooldown;
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
            else if (selectedAbility == 1)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (selectedWeapon1 >= weapons1.Length - 1)
                    {
                        selectedWeapon1 = 0;
                    }
                    else
                    {
                        selectedWeapon1++;
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (selectedWeapon1 <= 0)
                    {
                        selectedWeapon1 = weapons1.Length - 1;
                    }
                    else
                    {
                        selectedWeapon1--;
                    }
                }

                SelectWeapon();

                if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && shield.activeSelf)
                {
                    if (!shieldScript.isDefending)
                    {
                        ShieldDefense();
                    }
                    else
                    {
                        ShieldDefenseOff();
                    }
                }
            }
            else if (selectedAbility == 2)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (selectedWeapon2 >= weapons2.Length - 1)
                    {
                        selectedWeapon2 = 0;
                    }
                    else
                    {
                        selectedWeapon2++;
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (selectedWeapon2 <= 0)
                    {
                        selectedWeapon2 = weapons2.Length - 1;
                    }
                    else
                    {
                        selectedWeapon2--;
                    }
                }

                SelectWeapon();

                if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && selectedWeapon2 == 0)
                {
                    player.Heal(20);
                    canAttack = false;
                    StartCoroutine(ResetAttackCooldown(1f));
                }
                if (Input.GetMouseButtonDown(keyConfiguration.rightMouseKey) && selectedWeapon2 == 1)
                {
                    print("xp");
                    player.XP(20);
                    canAttack = false;
                    StartCoroutine(ResetAttackCooldown(1f));
                }
            }
            else if (selectedAbility == 3)
            { 
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (selectedWeapon3 >= weapons3.Length - 1)
                    {
                        selectedWeapon3 = 0;
                    }
                    else
                    {
                        selectedWeapon3++;
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (selectedWeapon3 <= 0)
                    {
                        selectedWeapon3 = weapons3.Length - 1;
                    }
                    else
                    {
                        selectedWeapon3--;
                    }
                }

                SelectWeapon();
            }
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


        private void SelectAbility()
        {
        if (Input.GetKeyDown(keyConfiguration.eKey))
        {
            selectedAbility = 0;
            selectors[0].gameObject.SetActive(true);
            selectors[1].gameObject.SetActive(false);
            selectors[2].gameObject.SetActive(false);
            selectors[3].gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(keyConfiguration.aKey))
        {
            selectedAbility = 1;
            selectors[0].gameObject.SetActive(false);
            selectors[1].gameObject.SetActive(true);
            selectors[2].gameObject.SetActive(false);
            selectors[3].gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(keyConfiguration.wKey))
        {
            selectedAbility = 2;
            selectors[0].gameObject.SetActive(false);
            selectors[1].gameObject.SetActive(false);
            selectors[2].gameObject.SetActive(true);
            selectors[3].gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(keyConfiguration.xKey))
        {
            selectedAbility = 3;
            selectors[0].gameObject.SetActive(false);
            selectors[1].gameObject.SetActive(false);
            selectors[2].gameObject.SetActive(false);
            selectors[3].gameObject.SetActive(true);
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        if (selectedAbility != 0)
        {
            i = 5;
        }
        foreach (GameObject weapon in weapons)
        {
            if (i == selectedWeapon0)
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
        foreach (Image img in weaponsImages)
        {
            if (i == selectedWeapon0)
            {
                img.gameObject.SetActive(true);
            }
            else
            {
                img.gameObject.SetActive(false);
            }
            i++;
        }
        i = 0;
        if (selectedAbility != 1)
        {
            i = 5;
        }
        foreach (GameObject weapon in weapons1)
        {
            if (i == selectedWeapon1)
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
        foreach (Image img in weaponsImages1)
        {
            if (i == selectedWeapon1)
            {
                img.gameObject.SetActive(true);
            }
            else
            {
                img.gameObject.SetActive(false);
            }
            i++;
        }
        i = 0;
        if (selectedAbility != 2)
        {
            i = 5;
        }
        foreach (GameObject weapon in weapons2)
        {
            if (i == selectedWeapon2)
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
        foreach (Image img in weaponsImages2)
        {
            if (i == selectedWeapon2)
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


    private void ShieldDefense()
    {
        anim = shield.GetComponentInChildren<Animator>();

        anim.SetTrigger("Defense");

        shield.GetComponent<BoxCollider>().isTrigger = false;

        shield.GetComponent<Shield>().isDefending = true;

        // ac.PlayOneShot(shieldScript.shieldDefense);
    }

    private void ShieldDefenseOff()
    {
        anim = shield.GetComponentInChildren<Animator>();

        anim.SetTrigger("DefenseOff");

        shield.GetComponent<BoxCollider>().isTrigger = true;

        shield.GetComponent<Shield>().isDefending = false;

        // ac.PlayOneShot(shieldScript.shieldDefense);
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

    private void AxeAttack()
    {
        axe.GetComponent<FightDetection>().enabled = true; // to disable attack just by passing on enemy

        anim = axe.GetComponentInChildren<Animator>();

        canAttack = false;

        anim.SetTrigger("Attack");

        ac.PlayOneShot(axeScript.swordAttack);

        StartCoroutine(ResetAttackCooldown(axeScript.cooldown));
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
        GameObject bullet = Instantiate(bowScript.bowProjectile, firePoint.position, Quaternion.Euler(0, capsule.transform.localRotation.y, 90)) ;
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
