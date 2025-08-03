using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudControllerScript : MonoBehaviour
{
    public Image fuelSlider;
    public Image healthSlider;
    public Image dashMeterSlider;
    public Image gunCooldownSlider;
    public Image shuttleIntegritySlider;

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
        fuelSlider.fillAmount = value;
    }

    void UpdateHealth(float value)
    {
        healthSlider.fillAmount = value;
    }

    void UpdateDash(float value)
    {
        dashMeterSlider.fillAmount = value;
    }

    void UpdateGunCooldown(float value)
    {
        gunCooldownSlider.fillAmount = value;
    }

    void UpdateShuttleIntegrity(float value)
    {
        shuttleIntegritySlider.fillAmount = value;
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
