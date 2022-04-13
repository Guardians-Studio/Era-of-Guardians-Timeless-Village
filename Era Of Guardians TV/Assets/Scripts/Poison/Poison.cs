using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] GameObject warning; 
    private Coroutine damage;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        warning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        warning.SetActive(true);
        damage = StartCoroutine(PoisonDmg(other));
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        warning.SetActive(false);
        StopCoroutine(PoisonDmg(other));
        triggered = false;
    }

    IEnumerator PoisonDmg(Collider other)
    {
        while (triggered)
        {
            other.gameObject.GetComponentInParent<Player>().TakeDamage(2);
            yield return new WaitForSeconds(1);
        }
    }
}
