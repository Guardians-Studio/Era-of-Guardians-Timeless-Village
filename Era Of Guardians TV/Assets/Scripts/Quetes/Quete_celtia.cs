using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete_celtia : MonoBehaviour
{
    private bool enter = true;
    private bool gemme = false;
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
            weaponController.healthPotionCount +=2;
            weaponController.xpPotionCount +=2;
            player.gemmeCount += 1;
            enter = false;
            StartCoroutine(ReinitializeEntryBool());
            gemme = true;
        }
    }
    void Update()
    {
        if (gemme)
        {
            if (jsp > 0)
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
        if(other.gameObject.name == "PNJ_celtia")
        {
            Panel.SetActive(true);
            if(gemme)
            {
                QuetePnj.text = "Bravo tu l'as donc trouvé !!\n\n Je t'attendrais à Okeanos pour obtenir la prochaine gemme !";
            }
            else
            {
                QuetePnj.text = "Bonjour cher aventurier ! \n Fais attention, il y a un énorme ravin ! \n Parviendras-tu à trouver la gemme cachée dans un coffre sans périr ?";
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
