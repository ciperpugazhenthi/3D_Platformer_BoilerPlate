using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSliderUI : MonoBehaviour
{
    public Image fuelCooldownSlider;
    private void OnEnable()
    {
        fuelCooldownSlider = GetComponent<Image>();
        HUDGameEvents.OnFuelChanged += UpdateFuelCooldown;
    }
    private void OnDisable()
    {
        HUDGameEvents.OnFuelChanged -= UpdateFuelCooldown;
    }
    private void UpdateFuelCooldown(float value)
    {
        fuelCooldownSlider.fillAmount = value;
    }
}
