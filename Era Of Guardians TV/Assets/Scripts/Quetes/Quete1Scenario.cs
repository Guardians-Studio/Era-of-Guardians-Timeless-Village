using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quete1Scenario : MonoBehaviour
{
    public GameObject Panel;
    private int jsp = 1;
    [SerializeField] WeaponController weaponController;

    void Update()
    {
        if (jsp >0)
            {
            if (weaponController.healthPotionCount >5)
            {
                Panel.SetActive(true);
                StartCoroutine(Wait());
            }
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        Panel.SetActive(false);
        jsp = 0;
    }
}
