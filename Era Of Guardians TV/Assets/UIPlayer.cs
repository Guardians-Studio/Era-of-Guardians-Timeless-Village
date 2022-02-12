using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [Header("UI Player Text")]
    [SerializeField] Text aText;
    [SerializeField] Text eText;
    [SerializeField] Image xpBar;
    // [SerializeField] Text levelText;
    [SerializeField] KeyConfiguration keyConfiguration;

    private void Start()
    {
        aText.text = keyConfiguration.aKeyString;
        eText.text = keyConfiguration.eKeyString;
    }

   /* public void UpdateXPBar(float fraction)
    {
       xpBar.fillAmount = fraction;
    }*/

    public void UpdateLevel(int level)
    {
        // levelText.text = (char)level;
    }

}
