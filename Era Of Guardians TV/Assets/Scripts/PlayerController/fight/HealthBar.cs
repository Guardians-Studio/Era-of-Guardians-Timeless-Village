using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] Text healthTxt;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    public void UpdateHealth(float fraction)
    {
        healthBar.fillAmount = fraction;
        healthTxt.text = (fraction * 100).ToString("0") + " / 100";
    }

}
