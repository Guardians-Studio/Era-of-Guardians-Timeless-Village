using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [Header("UI Player Text")]
    [SerializeField] Text aText;
    [SerializeField] Text eText;
    [SerializeField] Text wText;
    [SerializeField] Text xText;
    [SerializeField] Image xpBar;
    [SerializeField] Text xpTxt;
    [SerializeField] Image healthBar;
    [SerializeField] Text healthTxt;
    // [SerializeField] Text levelText;
    [SerializeField] KeyConfiguration keyConfiguration;

    private void Start()
    {
        aText.text = keyConfiguration.aKeyString;
        eText.text = keyConfiguration.eKeyString;
        wText.text = keyConfiguration.wKeyString;
        xText.text = keyConfiguration.xKeyString;
    }

    public void UpdateHealth(float fraction)
    {
        healthBar.fillAmount = fraction;
        healthTxt.text = (fraction * 100).ToString("0") + " / 100";
    }

    public void UpdateXPBar(float fraction)
    {
       xpBar.fillAmount = fraction;
       xpTxt.text = (fraction * 100).ToString("0") + " / 100";
    }

    public void UpdateLevel(int level)
    {
        // levelText.text = (char)level;
    }

}
