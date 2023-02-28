using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWithUI : Health
{
    public Slider healthBar;
    public Image healthIcon;

    void Start()
    {
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
        }
    }
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthBar)
        {
            healthBar.value = currentHealth;
        }
        else if (healthIcon)
        {
            healthIcon.fillAmount = currentHealth / maxHealth;
        }
    }
}
