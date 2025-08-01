using UnityEngine;

public class FuelCanister : MonoBehaviour
{
    public float fuelAmount = 25f; // How much fuel this gives
    public string poolTag = "Fuel"; // Must match tag used in ObjectPoolManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.RefillFuel(fuelAmount);
            gameObject.SetActive(false); // Return to pool
        }
    }

    void Update()
    {
        // float and rotate the canister for a visual effect
        transform.Rotate(Vector3.up * 50f * Time.deltaTime, Space.World);
        transform.position += Vector3.up * Mathf.Sin(Time.time * 2f) * 0.0015f;
    }
}