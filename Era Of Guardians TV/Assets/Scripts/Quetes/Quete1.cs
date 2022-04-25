using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete1 : MonoBehaviour
{
    public int Bush = 0;
    public int potion = 0;
    public GameObject Panel;
    public Text NomPnj;
    public Text QuetePnj;

    [SerializeField] WeaponController weaponController;


    public void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Bush")
        {
            Destroy(collision.gameObject);
            Bush = Bush +1;   // probleme ajoute trop de bush dans l'inventaire
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

            if(Bush >= 3 && potion != 1)
            {
                QuetePnj.text = "Merci beaucoup !! \nVoici ta potion !";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
        if(Bush >= 3)
        {
            Bush = 0;
            weaponController.healthPotionCount++;
        }
    }
}
