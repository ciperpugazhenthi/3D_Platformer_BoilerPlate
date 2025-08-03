using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float maxHealth = 100f;
    protected float currentHealth;

    public float damageRate = 20f;

    // Start is called before the first frame update
    protected virtual void Start() => currentHealth = maxHealth;

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Handle enemy death logic here
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected abstract void PerformAttack();
}
