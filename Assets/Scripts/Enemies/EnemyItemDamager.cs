using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDamager : EnemyBase
{
    // Update is called once per frame
    void Update()
    {
        Movement();
        PerformAttack();
    }

    void Movement()
    {

    }

    protected override void PerformAttack()
    {
        // TODO: implement item damage logic
    }
}
