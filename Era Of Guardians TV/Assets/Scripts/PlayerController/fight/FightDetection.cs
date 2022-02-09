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
        else if(this.tag == "Bullet")
        {
            currentWeapon = "MageParticle";
            damage = 30f; // does not do link with Bow Script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentWeapon == "MageParticle" && other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && isCollided)
        {
            isCollided = false;
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            // other.GetComponent<Animator>().SetTrigger("Hit");

            if (this.enabled)
            {
                ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
                particle.Play();
                other.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
                this.enabled = false;
            }

        }
    }
}

