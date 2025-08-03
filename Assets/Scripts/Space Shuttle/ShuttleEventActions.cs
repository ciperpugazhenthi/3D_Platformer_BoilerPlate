using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleEventActions : Singleton<ShuttleEventActions>
{
    public SpaceShuttleBehavior shuttle;
    
    public float reducedVibration = 0.01f;
    public float restoreDelay = 5f;

    private PlayerController playerController;

    private bool circuitryFixed = false;
    private bool navigationFixed = false;
    private bool medbayFixed = false;

    private void Awake()
    {
        base.Awake();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void TriggerCircuitryDamage()
    {
        if (circuitryFixed) return;
        Debug.Log("[Shuttle Event Triggered]: Circuitry Damage");
        circuitryFixed = true;
        shuttle.ToggleRotation(false);
        Invoke(nameof(RestoreCircuitry), restoreDelay);
    }

    private void RestoreCircuitry()
    {
        Debug.Log("[Shuttle Event Restored]: Circuitry");
        shuttle.ToggleRotation(true);
    }

    public void TriggerNavigationFix()
    {
        if (navigationFixed) return;
        if (shuttle == null) return;
        Debug.Log("[Shuttle Event Triggered]: Navigation Fix");
        navigationFixed = true;
        float original = shuttle.vibrationIntensity;
        shuttle.vibrationIntensity = reducedVibration;
        Invoke(nameof(RestoreNavigation), restoreDelay);
    }

    private void RestoreNavigation()
    {
        if (shuttle == null) return;
        Debug.Log("[Shuttle Event Restored]: Navigation");
        shuttle.vibrationIntensity = 0.05f;
    }

    public void TriggerMedbayHealing()
    {
        Debug.Log("[Shuttle Event Triggered]: Medbay Healing");
        if (playerController == null) return;
        if (medbayFixed) return;

        playerController.RegainOxygen(50f);
    }


}
