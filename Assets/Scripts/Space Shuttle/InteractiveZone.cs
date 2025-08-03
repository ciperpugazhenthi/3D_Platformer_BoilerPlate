using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public enum ZoneType
{
    Circuitry,
    Navigation,
    Medbay
}

public class InteractiveZone : MonoBehaviour
{
    public ZoneType zoneType;

    public KeyCode interactionKey = KeyCode.G;

    public float holdDuration = 3f;

    public TMP_Text promptText;
    public TMP_Text completionText;

    public Image progressBar;

    private bool playerInZone = false;
    private bool isInteracting = false;
    private float currentHoldTime = 0f;
    private Coroutine interactionCoroutine;

    public bool IsCompleted { get; private set; } = false;
    public string ZoneName => zoneType.ToString();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entered zone: " + zoneType);
        playerInZone = true;
        ShowPrompt(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player exited zone: " + zoneType);
        playerInZone = false;
        ShowPrompt(false);
        ResetInteraction();
    }

    private void Update()
    {
        Debug.Log($"player zone check : {playerInZone}");
        if (!playerInZone) return;
        if (Input.GetKey(interactionKey))
        {
            StartInteraction();
        }
        else if (Input.GetKeyUp(interactionKey))
        {
            StopInteraction();
        }

        if (isInteracting)
        {
            UpdateInteractionProgress();
        }
    }

    private void StartInteraction()
    {
        if (isInteracting) return;
        Debug.Log("Starting interaction in zone: " + zoneType);
        isInteracting = true;
        currentHoldTime = 0f;
        interactionCoroutine = StartCoroutine(InteractionCoroutine());
    }

    private void StopInteraction()
    {
        if (!isInteracting) return;
        ResetInteraction();
    }

    private void UpdateInteractionProgress()
    {
        currentHoldTime += Time.deltaTime;
        float progress = currentHoldTime / holdDuration;
        progressBar.fillAmount = progress;
        int percentage = Mathf.RoundToInt(progress * 100);
        promptText.text = $"Hold {interactionKey} to fix {zoneType} ({percentage}%)";
    }

    private IEnumerator InteractionCoroutine()
    {
        yield return new WaitForSeconds(holdDuration);
        CompleteZone();
    }

    private void CompleteZone()
    {
        IsCompleted = true;
        TriggerZoneEvent();
        UpdateCompletionVisuals();
        UpdateCompletionUI();
        ResetInteraction();
        ShowPrompt(false);
    }

    private void TriggerZoneEvent()
    {
        ShuttleEventActions eventActions = ShuttleEventActions.Instance;
        if (eventActions == null) return;

        switch (zoneType)
        {
            case ZoneType.Circuitry:
                eventActions.TriggerCircuitryDamage();
                break;
            case ZoneType.Navigation:
                eventActions.TriggerNavigationFix();
                break;
            case ZoneType.Medbay:
                eventActions.TriggerMedbayHealing();
                break;
            default:
                Debug.LogWarning("Unknown zone type: " + zoneType);
                break;
        }
    }

    private void UpdateCompletionVisuals()
    {
        // TODO: Add completion visuals if needed
    }

    private void UpdateCompletionUI()
    {
        if (IsCompleted)
        {
            completionText.text = $"{zoneType} repair completed!";
            completionText.gameObject.SetActive(true);
        }
        else
        {
            completionText.gameObject.SetActive(false);
        }
    }

    private void ResetInteraction()
    {
        isInteracting = false;
        currentHoldTime = 0f;
        if (interactionCoroutine != null)
        {
            StopCoroutine(interactionCoroutine);
            interactionCoroutine = null;
        }
        progressBar.fillAmount = 0f;
        promptText.text = $"Hold {interactionKey} to fix {zoneType}";
    }

    private void ShowPrompt(bool show)
    {
        if (IsCompleted)
        {
            show = false;
        }
        promptText.gameObject.SetActive(show);
        if (show)
            promptText.text = $"Hold {interactionKey} to fix {zoneType}";
        progressBar.gameObject.SetActive(show);
        progressBar.fillAmount = 0f;
    }
}