using System;

public static class HUDGameEvents
{
    public static event Action<float> OnFuelChanged;
    public static event Action<float> OnPlayerHealthChanged;
    public static event Action<float> OnShuttleIntegrityChanged;
    public static event Action<float> OnDashCooldownChanged;
    public static event Action<float> OnGunCooldownChanged;
    public static event Action<float> OnHealthEffectChanged;

    public static event Action<bool> OnFuelWarning;
    public static event Action<bool> OnHealthWarning;
    public static event Action<bool> OnShuttleWarning;
    public static event Action<bool> OnSanityWarning;

    public static void FuelChanged(float value) => OnFuelChanged?.Invoke(value);
    public static void PlayerHealthChanged(float value) => OnPlayerHealthChanged?.Invoke(value);
    public static void PlayerHealthEffectChanged(float value) => OnHealthEffectChanged?.Invoke(value);
    public static void ShuttleIntegrityChanged(float value) => OnShuttleIntegrityChanged?.Invoke(value);
    public static void DashCooldownChanged(float value) => OnDashCooldownChanged?.Invoke(value);
    public static void GunCooldownChanged(float value) => OnGunCooldownChanged?.Invoke(value);


    public static void FuelWarning(bool value) => OnFuelWarning?.Invoke(value);
    public static void HealthWarning(bool value) => OnHealthWarning?.Invoke(value);
    public static void ShuttleWarning(bool value) => OnShuttleWarning?.Invoke(value);
    public static void SanityWarning(bool value) => OnSanityWarning?.Invoke(value);

}