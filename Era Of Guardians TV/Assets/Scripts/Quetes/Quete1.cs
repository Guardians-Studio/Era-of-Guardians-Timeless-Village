using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete1 : MonoBehaviour
{
    public int potion = 0;
    public GameObject Panel;
    public Text NomPnj;
    public Text QuetePnj;

    private bool enter = true;

    [SerializeField] WeaponController weaponController;


    public void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Bush" && enter)
        {
            Destroy(collision.gameObject);
            weaponController.bushCount ++;   // probleme ajoute trop de bush dans l'inventaire
            enter = false;
            StartCoroutine(ReinitializeEntryBool());
        }
    }

    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PNJ 1")
        {
            Panel.SetActive(true);
            NomPnj.text = "Inconu";
            if(potion == 1)
            {
                QuetePnj.text = "je n'ai pas d'autres quêtes à te proposer. Reviens une prochaine fois !!";
            }
            else
            {
                QuetePnj.text = "Pourriez vous me trouver 3 buissons SVP";
            }

            if(weaponController.bushCount >= 3 && potion != 1)
            {
                QuetePnj.text = "Merci beaucoup !! \nVoici ta potion !";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
        if(weaponController.bushCount >= 3)
        {
            weaponController.bushCount = 0;
            weaponController.healthPotionCount++;
        }
    }

    IEnumerator ReinitializeEntryBool()
    {
        yield return new WaitForSeconds(2);
        enter = true;
    }
}
