using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyDetection : MonoBehaviour
{

    [SerializeField] ParticleSystem hitParticle;
    private float damage;

    private void Start()
    {
        damage = GetComponent<Sword>().damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            if (!other.GetComponent<Shield>().isDefending)
            {
                if (other.tag == "Player")
                {
                    // other.GetComponent<Animator>().SetTrigger("Hit");

                    if (this.enabled)
                    {
                        ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
                        particle.Play();
                        other.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
                        this.enabled = false;
                    }

                }
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                // other.GetComponent<Animator>().SetTrigger("Hit");

                if (this.enabled)
                {
                    ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
                    particle.Play();
                    other.gameObject.GetComponentInParent<Player>().TakeDamage(damage);
                    this.enabled = false;
                }

            }
        }   
    }
}
