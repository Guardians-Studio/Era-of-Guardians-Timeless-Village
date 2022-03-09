using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightDetection : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticle;

    private float damage;
    private string currentWeapon;

    private bool isCollided = true;

    private void Start()
    {
        if (this.tag == "Sword")
        {
            currentWeapon = "Sword";
            damage = GetComponent<Sword>().damage;
        }
        else if (this.tag == "Axe")
        {
            currentWeapon = "Axe";
            damage = GetComponent<Axe>().damage;
        }
        else if (this.tag == "Arrow")
        {
            currentWeapon = "Projectile";
            damage = 30f; // does not do link with Bow Script
        }
        else if(this.tag == "Bullet")
        {
            currentWeapon = "Projectile";
            damage = 50f; // does not do link with Bow Script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentWeapon == "Projectile" && other.gameObject.tag != "Player" && isCollided)
        {
            isCollided = false;
            Destroy(gameObject);
        }

        if (other.tag == "Enemy") // can hit twice +, need this.enable = false;
        {
            // other.GetComponent<Animator>().SetTrigger("Hit");
            ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
            particle.Play();
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
        }
    }
}

