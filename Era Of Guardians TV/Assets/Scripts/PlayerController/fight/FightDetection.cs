using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightDetection : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticle;

    public bool isAttacking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // modify to Enemy
        {
            isAttacking = true;
            // other.GetComponent<Animator>().SetTrigger("Hit");

            other.gameObject.GetComponentInParent<Player>().TakeDamage(20);

            if (isAttacking)
            {
                ParticleSystem particle = Instantiate(hitParticle, other.gameObject.GetComponentInParent<Transform>());
                particle.Play();
                isAttacking = false;
            }
            
            Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
    }
}
