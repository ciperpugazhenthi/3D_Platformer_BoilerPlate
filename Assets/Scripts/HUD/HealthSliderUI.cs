using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderUI : MonoBehaviour
{
    public Image healthBar;
    public Image healthEffect;
    private void OnEnable()
    {
        healthBar = GetComponent<Image>();
        HUDGameEvents.OnPlayerHealthChanged += UpdateHealthBar;
        HUDGameEvents.OnHealthEffectChanged += UpdateHealthWarningEffect;
    }
    private void OnDisable()
    {
        HUDGameEvents.OnPlayerHealthChanged -= UpdateHealthBar;
        HUDGameEvents.OnHealthEffectChanged -= UpdateHealthWarningEffect;
    }
    private void UpdateHealthBar(float value)
    {
        healthBar.fillAmount = value;
    }

    private void UpdateHealthWarningEffect(float value)
    {
        healthEffect.fillAmount = value;
    }

}
