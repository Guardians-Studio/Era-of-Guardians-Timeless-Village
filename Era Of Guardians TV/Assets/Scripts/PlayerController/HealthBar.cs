using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    public void UpdateHealth(float fraction)
    {
        healthBar.fillAmount = fraction;
    }

}
