using UnityEngine;
using System.Collections.Generic;

public class OxygenCanisterSpawner : MonoBehaviour
{
    [Header("Torus Spawn Area")]
    public Transform centerReference;
    public float majorRadius = 15f;
    public float minorRadius = 3f;

    [Header("Spawning Logic")]
    public string OxigenCanisterTag = "Fuel";
    public float spawnInterval = 5f;
    public int maxActiveCanisters = 5;
    public float playerBiasRadius = 8f;

    [Header("References")]
    public Transform player;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        int activeCount = CountActiveCanisters();
        if (activeCount >= maxActiveCanisters) return;

        Vector3 spawnPos = GetBiasedSpawnPosition();
        GameObject obj = ObjectPoolManager.Instance.SpawnFromPool(OxigenCanisterTag, spawnPos, Quaternion.identity);
        if (obj != null) obj.SetActive(true);
    }

    int CountActiveCanisters()
    {
        Queue<GameObject> poolQueue = ObjectPoolManager.Instance.GetPoolQueue(OxigenCanisterTag);
        int count = 0;
        foreach (var obj in poolQueue)
        {
            if (obj.activeInHierarchy) count++;
        }
        return count;
    }

    Vector3 GetBiasedSpawnPosition()
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 candidate = GetRandomPointInsideTorus(centerReference.position, majorRadius, minorRadius);
            if (Vector3.Distance(candidate, player.position) < playerBiasRadius)
                return candidate;
        }

        // If all biased attempts fail, return a truly random position
        return GetRandomPointInsideTorus(centerReference.position, majorRadius, minorRadius);
    }

    Vector3 GetRandomPointInsideTorus(Vector3 center, float R, float r)
    {
        float u = Random.Range(0f, 2f * Mathf.PI);
        float v = Random.Range(0f, 2f * Mathf.PI);

        /*        float x = (R + r * Mathf.Cos(v)) * Mathf.Cos(u);
                float y = r * Mathf.Sin(v);
                //float z = (R + r * Mathf.Cos(v)) * Mathf.Sin(u);
                Debug.Log($"center {center}");
                return center + new Vector3(x, y, 0f);*/
        // Calculate circular position around YZ plane (vertical torus)
        float y = Mathf.Cos(u) * majorRadius;
        float x = Mathf.Sin(u) * majorRadius;

        // Add inner circle offset
        float offsetZ = Mathf.Cos(v) * minorRadius;
        float offsetR = Mathf.Sin(v) * minorRadius;

        // Final position in space
        Vector3 spawnPoint = center + new Vector3(x + offsetR * Mathf.Sin(u), y + offsetR * Mathf.Cos(u), 0f/*offsetZ*/);

        return spawnPoint;
    }

    void OnDrawGizmosSelected()
    {
        if (centerReference == null) return;

        Gizmos.color = Color.cyan;
        int samples = 100;

        for (int i = 0; i < samples; i++)
        {
            Vector3 p = GetRandomPointInsideTorus(centerReference.position, majorRadius, minorRadius);
            Gizmos.DrawSphere(p, 1f);
        }

        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, playerBiasRadius);
        }
    }
}
