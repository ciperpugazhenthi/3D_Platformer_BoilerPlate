using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDamager : EnemyBase
{
    private Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PerformAttack();
    }

    protected override void PerformAttack()
    {
        if (player == null) return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, damageRate * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 1.0f)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(damageRate * Time.deltaTime);
            }
        }
    }
}
