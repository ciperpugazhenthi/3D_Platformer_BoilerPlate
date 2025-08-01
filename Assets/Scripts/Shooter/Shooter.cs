using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public float shootCooldown = 0.5f;
    public string bulletTag = "Bullet";

    private float nextShootTime;

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }
    void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }
    void Shoot()
    {

        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(firePoint.position).z;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector3 rayDirection = (mousePosition - firePoint.position).normalized;

            RaycastHit hit;
            bool didHit = Physics.Raycast(firePoint.position, rayDirection, out hit, 300, -1);
            if (didHit)
            {
                Debug.Log("Hit: " + hit.collider.name + " at " + hit.point);
                Debug.DrawLine(firePoint.position, hit.point, Color.red, RayConstants.DebugRayDuration);
            }
            else
            {
                Debug.Log("Missed shot!");
                Debug.DrawLine(firePoint.position, firePoint.position + rayDirection * 300, Color.red, RayConstants.DebugRayDuration);
            }
        }
        //nextShootTime = Time.time + shootCooldown;
        //GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(
        //    bulletTag,
        //    firePoint.position,
        //    firePoint.rotation);
    }
}
