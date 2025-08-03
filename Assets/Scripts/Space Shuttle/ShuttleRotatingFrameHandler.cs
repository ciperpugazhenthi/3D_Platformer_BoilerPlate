using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShuttleRotatingFrameHandler : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationTorque = 50f;
    public bool rotateShuttle = true;

    [Header("Vibration Settings")]
    public float vibrationForce = 0.2f;
    public float vibrationFrequency = 10f;
    private float vibrationTimer;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezePositionZ; // if 2.5D
    }

    private void FixedUpdate()
    {
        if (rotateShuttle)
        {
            ApplyRotation();
        }

        ApplyVibration();
    }

    private void ApplyRotation()
    {
        // Applies torque around the Y-axis (or change to your axis)
        rb.AddTorque(Vector3.forward * rotationTorque);
    }

    private void ApplyVibration()
    {
        vibrationTimer += Time.fixedDeltaTime * vibrationFrequency;
        Vector3 vib = new Vector3(
            Mathf.PerlinNoise(vibrationTimer, 0f) - 0.5f,
            Mathf.PerlinNoise(0f, vibrationTimer) - 0.5f,
            0f); // Z=0 for 2.5D
        rb.AddForce(vib * vibrationForce, ForceMode.Force);
    }

    // Call this if you want to enable or disable events or change rotation at runtime
    public void SetRotationActive(bool isActive)
    {
        rotateShuttle = isActive;
    }
}
