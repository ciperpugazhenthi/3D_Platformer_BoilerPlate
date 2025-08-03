using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleEventActions : MonoBehaviour
{
    public SpaceShuttleBehavior shuttle;
    // TODO: Need PlayerHealth

    public float reducedVibration = 0.01f;
    public float restoreDelay = 5f;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void TriggerCircuitryDamage()
    {
        Debug.Log("[Shuttle Event Triggered]: Circuitry Damage");
    }

    public void TriggerNavigationFix()
    {
        if (shuttle == null) return;
        Debug.Log("[Shuttle Event Triggered]: Navigation Fix");
        float original = shuttle.vibrationIntensity;
        shuttle.vibrationIntensity = reducedVibration;
    }

    public void TriggerMedbayHealing()
    {
        Debug.Log("[Shuttle Event Triggered]: Medbay Healing");
        if (playerController == null) return;

        playerController.RegainOxygen(50f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
