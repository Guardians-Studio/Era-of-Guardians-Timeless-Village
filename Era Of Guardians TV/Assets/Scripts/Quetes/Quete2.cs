using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete2 : MonoBehaviour
{

    private bool Tidalar = false; 
    private bool gemme = false;
    public GameObject Panel;
    public GameObject image_recompense;
    public Text NomPnj;
    public Text QuetePnj;
    private int jsp = 1;

    [SerializeField] Player player;

    void Update()
    {
        if (GameObject.Find("Monstres") == null)
        {
            player.gemmeCount++;
            gemme = true;

        }
        if (gemme)
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
        if(other.gameObject.name == "PNJ 2")
        {
            Panel.SetActive(true);
            NomPnj.text = "Inconu";
            if(gemme)
            {
                QuetePnj.text = "Si tu es revenu ici ça veut dire que tu as réussi !! \n\n Maintenant il te reste une dernière chose a faire : \n Battre Tidalar au centre de Hazeltown !";
            }
            else
            {
                QuetePnj.text = "Combat les Monstres de la forêt orange pour obtenir une gemme.";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
    }
}
