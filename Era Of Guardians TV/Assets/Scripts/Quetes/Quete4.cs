using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete4 : MonoBehaviour
{
    private bool enter = true;
    private bool tresor = false;
    public GameObject Panel;
    public GameObject image_recompense;
    public Text QuetePnj;
    private int jsp = 1;
    [SerializeField] WeaponController weaponController;
    [SerializeField] Player player;

    public void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.name == "Tresors" && enter)
        {
            Destroy(collision.gameObject);
            weaponController.healthPotionCount +=1;
            weaponController.xpPotionCount +=2;
            player.gemmeCount += 1;
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
        if(other.gameObject.name == "PNJ 4")
        {
            Panel.SetActive(true);
            if(tresor)
            {
                QuetePnj.text = "Bravo tu l'as donc trouvé !!\n\n Je t'attendrais à X pour obtenir la prochaine gemme !";
            }
            else
            {
                QuetePnj.text = "Bonjour cher aventurier ! \n Comme tu peux le voir derriere moi, il y a des tranchées de lave. \n Gare à ne pas y tomber ou la vie tu perdras !! \n Pour obtenir la gemme rouge, il te faudra trouver le coffre la contenant";
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
