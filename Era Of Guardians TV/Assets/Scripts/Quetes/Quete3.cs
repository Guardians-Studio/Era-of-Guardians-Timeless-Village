using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete3 : MonoBehaviour
{
    private bool tresor = false;
    public GameObject Panel;
    public GameObject image_recompense;
    public Text QuetePnj;
    private int jsp = 1;
    private bool enter = true;
    [SerializeField] WeaponController weaponController;

    public void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.name == "Tresor" && enter)
        {
            Destroy(collision.gameObject);
            weaponController.healthPotionCount +=2;
            weaponController.xpPotionCount +=2;
            enter = false;
            StartCoroutine(ReinitializeEntryBool());
            tresor = true;
        }
    }
    void Update()
    {
        if (tresor)
        {
            if (jsp >0)
            {
                image_recompense.SetActive(true);
                StartCoroutine(Wait());
            }
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        image_recompense.SetActive(false);
        jsp = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PNJ 3")
        {
            Panel.SetActive(true);
            if(tresor)
            {
                QuetePnj.text = "Bravo tu as réussi à trouver le trésor !!";
            }
            else
            {
                QuetePnj.text = "Plus loin sur ce chemin, il y a un labyrinthe... \n combat le gardien et trouve le tresor pour obtenir des potions ! \n Mais attention à ne pas te perdre et sois attentif !";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
    }
    IEnumerator ReinitializeEntryBool()
    {
        yield return new WaitForSeconds(2);
        enter = true;
    }
}
