using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMetalSliderUI : MonoBehaviour
{
    public Image dashCooldownSlider;
    private void OnEnable()
    {
        dashCooldownSlider = GetComponent<Image>();
        HUDGameEvents.OnDashCooldownChanged += UpdateDashCooldown;
    }
    private void OnDisable()
    {
        HUDGameEvents.OnDashCooldownChanged -= UpdateDashCooldown;
    }
    private void UpdateDashCooldown(float value)
    {
        dashCooldownSlider.fillAmount = value;
    }
}
