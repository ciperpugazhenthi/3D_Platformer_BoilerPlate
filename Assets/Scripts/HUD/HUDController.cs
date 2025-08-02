using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControllerScript : MonoBehaviour
{
    public Slider fuelSlider;
    public Slider healthSlider;
    public Slider dashMeterSlider;
    public Slider gunCooldownSlider;
    public Slider shuttleIntegritySlider;

    public GameObject fuelWarning;
    public GameObject healthWarning;
    public GameObject sanityWarning;
    public GameObject shuttleIntegrityWarning;

    private void OnEnable()
    {
        HUDGameEvents.OnFuelChanged += UpdateFuel;
        HUDGameEvents.OnGunCooldownChanged += UpdateGunCooldown;
        HUDGameEvents.OnPlayerHealthChanged += UpdateHealth;
        HUDGameEvents.OnDashCooldownChanged += UpdateDash;
        HUDGameEvents.OnShuttleIntegrityChanged += UpdateShuttleIntegrity;

        HUDGameEvents.OnFuelWarning += ShowFuelWarning;
        HUDGameEvents.OnHealthWarning += ShowHealthWarning;
        HUDGameEvents.OnSanityWarning += ShowSanityWarning;
        HUDGameEvents.OnShuttleWarning += ShowShuttleIntegrityWarning;
    }

    void UpdateFuel(float value)
    {
        fuelSlider.value = value;
    }

    void UpdateHealth(float value)
    {
        healthSlider.value = value;
    }

    void UpdateDash(float value)
    {
        dashMeterSlider.value = value;
    }

    void UpdateGunCooldown(float value)
    {
        gunCooldownSlider.value = value;
    }

    void UpdateShuttleIntegrity(float value)
    {
        shuttleIntegritySlider.value = value;
    }

    void ShowFuelWarning(bool show)
    {
        fuelWarning.SetActive(show);
    }

    void ShowHealthWarning(bool show)
    {
        healthWarning.SetActive(show);
    }

    void ShowSanityWarning(bool show)
    {
        sanityWarning.SetActive(show);
    }

    void ShowShuttleIntegrityWarning(bool show)
    {
        shuttleIntegrityWarning.SetActive(show);
    }
}
