using UnityEngine;
using UnityEngine.UI;

public class GunCooldownSliderUI : MonoBehaviour
{
    public Slider gunCooldownSlider;
    private void OnEnable()
    {
        HUDGameEvents.OnGunCooldownChanged += UpdateGunCooldown;
    }
    private void OnDisable()
    {
        HUDGameEvents.OnGunCooldownChanged -= UpdateGunCooldown;
    }
    private void UpdateGunCooldown(float value)
    {
        gunCooldownSlider.value = value;
    }
}