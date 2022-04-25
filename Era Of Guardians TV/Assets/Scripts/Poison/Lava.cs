using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //[SerializeField] GameObject warning;
    private Coroutine damage;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //warning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //warning.SetActive(true);
        damage = StartCoroutine(LavaDmg(other));
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //warning.SetActive(false);
        StopCoroutine(LavaDmg(other));
        triggered = false;
    }

    IEnumerator LavaDmg(Collider other)
    {
        while (triggered)
        {
            other.gameObject.GetComponentInParent<Player>().TakeDamage(15);
            yield return new WaitForSeconds(1);
        }
    }
}
