using UnityEngine;
using UnityEngine.UI;

public class GunCooldownSliderUI : MonoBehaviour
{
    public Image gunCooldownSlider;
    private void OnEnable()
    {
        gunCooldownSlider = GetComponent<Image>();
        HUDGameEvents.OnGunCooldownChanged += UpdateGunCooldown;
    }
    private void OnDisable()
    {
        HUDGameEvents.OnGunCooldownChanged -= UpdateGunCooldown;
    }
    private void UpdateGunCooldown(float value)
    {
        gunCooldownSlider.fillAmount = value;
    }
}