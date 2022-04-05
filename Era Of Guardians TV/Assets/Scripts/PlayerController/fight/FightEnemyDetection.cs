using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyDetection : MonoBehaviour
{

    [SerializeField] ParticleSystem hitParticle;
    private float damage;
    private float damageBow;

    private void Start()
    {
        damage = GetComponent<Sword>().damage;
        damageBow = GetComponent<Bow>().damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // other.GetComponent<Animator>().SetTrigger("Hit");

            if (this.enabled)
            {
                ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
                particle.Play();
                other.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
                other.gameObject.GetComponentInParent<Player>().TakeDamage(damageBow);
                this.enabled = false;
            }
        }
    }
}
