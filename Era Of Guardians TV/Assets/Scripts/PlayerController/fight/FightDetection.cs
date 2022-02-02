using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightDetection : MonoBehaviour
{
    [SerializeField] GameObject hitParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // modify to Enemy
        {
            Debug.Log("Hit");
            // other.GetComponent<Animator>().SetTrigger("Hit");

            other.gameObject.GetComponentInParent<Player>().TakeDamage(20);
            Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
    }
}
