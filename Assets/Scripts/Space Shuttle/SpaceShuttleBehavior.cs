using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SpaceShuttleBehavior : MonoBehaviour
{
    [Header("Rotation Settings")]
    public bool enableRotation = true;
    public float rotationSpeed = 10f;          // Degrees per second

    [Header("Vibration Settings")]
    public float vibrationIntensity = 0.05f;
    public float vibrationFrequency = 10f;
    private Vector3 basePosition;
    private float vibrationTimer;

    [Header("Event System")]
    public float eventCheckInterval = 15f;
    public List<ShuttleEvent> shuttleEvents;

    private Quaternion initialRotation;

    [System.Serializable]
    public class ShuttleEvent
    {
        public string eventName;
        public float triggerChance = 0.3f;       // 0 to 1
        public UnityEvent onEventTriggered;
    }

    private ShuttleEventActions eventActions;

    void Start()
    {
        //TODO: If another shuttle events need to be added in the future, consider using a ScriptableObject or a more dynamic system
        eventActions = FindObjectOfType<ShuttleEventActions>();
        shuttleEvents.Add(new ShuttleEvent
        {
            eventName = "Circuitry Damage",
            triggerChance = 0.3f,
            onEventTriggered = new UnityEngine.Events.UnityEvent()
        });
        shuttleEvents[0].onEventTriggered.AddListener(eventActions.TriggerCircuitryDamage);

        shuttleEvents.Add(new ShuttleEvent
        {
            eventName = "Navigation Fix",
            triggerChance = 0.3f,
            onEventTriggered = new UnityEngine.Events.UnityEvent()
        });
        shuttleEvents[1].onEventTriggered.AddListener(eventActions.TriggerNavigationFix);

        shuttleEvents.Add(new ShuttleEvent
        {
            eventName = "Medbay Healing",
            triggerChance = 0.3f,
            onEventTriggered = new UnityEngine.Events.UnityEvent()
        });
        shuttleEvents[2].onEventTriggered.AddListener(eventActions.TriggerMedbayHealing);
        StartCoroutine(EventLoop());
    }

    void Update()
    {
        if (enableRotation)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
    private void LateUpdate()
    {
        ApplyVibration();
    }

    private void ApplyVibration()
    {
        vibrationTimer += Time.deltaTime * vibrationFrequency;

        float offsetX = Mathf.PerlinNoise(Time.time, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time) - 0.5f;

        Vector3 vibrationOffset = new Vector3(offsetX, offsetY, 0f) * vibrationIntensity;

        transform.position = basePosition + vibrationOffset;
    }

    public void SetBasePosition(Vector3 newPosition)
    {
        basePosition = newPosition;
    }

    IEnumerator EventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(eventCheckInterval);

            foreach (var evt in shuttleEvents)
            {
                if (Random.value <= evt.triggerChance)
                {
                    Debug.Log($"[Shuttle Event Triggered]: {evt.eventName}");
                    evt.onEventTriggered?.Invoke();
                }
            }
        }
    }

    // --- Public API to control shuttle externally ---

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void ToggleRotation(bool state)
    {
        enableRotation = state;
    }

    public void AddEvent(ShuttleEvent newEvent)
    {
        shuttleEvents.Add(newEvent);
    }

    public void ClearEvents()
    {
        shuttleEvents.Clear();
    }
}

/*using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SpaceShuttleBehavior : MonoBehaviour
{
    [Header("Rotation Settings")]
    public bool enableRotation = true;
    public float rotationSpeed = 10f;          // Degrees per second

    [Header("Vibration Settings")]
    public float vibrationIntensity = 0.05f;
    public float vibrationFrequency = 10f;
    private Vector3 basePosition;
    private float vibrationTimer;

    [Header("Event System")]
    public float eventCheckInterval = 15f;
    public List<ShuttleEvent> shuttleEvents;

    private Quaternion initialRotation;

    [System.Serializable]
    public class ShuttleEvent
    {
        public string eventName;
        public float triggerChance = 0.3f;       // 0 to 1
        public UnityEvent onEventTriggered;
    }

    void Start()
    {
        StartCoroutine(EventLoop());
    }

    void Update()
    {
        if (enableRotation)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
    private void LateUpdate()
    {
        ApplyVibration();
    }

    private void ApplyVibration()
    {
        vibrationTimer += Time.deltaTime * vibrationFrequency;

        float offsetX = Mathf.PerlinNoise(Time.time, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, Time.time) - 0.5f;

        Vector3 vibrationOffset = new Vector3(offsetX, offsetY, 0f) * vibrationIntensity;

        transform.position = basePosition + vibrationOffset;
    }

    public void SetBasePosition(Vector3 newPosition)
    {
        basePosition = newPosition;
    }

    IEnumerator EventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(eventCheckInterval);

            foreach (var evt in shuttleEvents)
            {
                if (Random.value <= evt.triggerChance)
                {
                    Debug.Log($"[Shuttle Event Triggered]: {evt.eventName}");
                    evt.onEventTriggered?.Invoke();
                }
            }
        }
    }

    // --- Public API to control shuttle externally ---

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void ToggleRotation(bool state)
    {
        enableRotation = state;
    }

    public void AddEvent(ShuttleEvent newEvent)
    {
        shuttleEvents.Add(newEvent);
    }

    public void ClearEvents()
    {
        shuttleEvents.Clear();
    }
}
*/