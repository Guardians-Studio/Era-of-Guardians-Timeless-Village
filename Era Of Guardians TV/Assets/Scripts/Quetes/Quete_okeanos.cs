using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quete_okeanos : MonoBehaviour
{
    private bool gemme = false;
    public GameObject Panel;
    public GameObject image_recompense;
    public Text QuetePnj;
    private int jsp = 1;


    [SerializeField] Player player;

    void Update()
    {
        if (GameObject.Find("enemy_quete") == null && !gemme)
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
        if(other.gameObject.name == "PNJ_okeanos")
        {
            Panel.SetActive(true);
            if(gemme)
            {
                QuetePnj.text = "Tu as donc réussi !! \n\n Il te reste alors qu'à retourner sur Hazeltown pour faire apparaître le boss final !!";
            }
            else
            {
                QuetePnj.text = "Combat les Monstres pour obtenir la dernière gemme !!!.";
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Panel.SetActive(false);
    }
}
