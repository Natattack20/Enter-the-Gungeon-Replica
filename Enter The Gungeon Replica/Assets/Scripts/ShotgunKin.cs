using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunKin : EnemyBase
{
    public override void Shoot()
    {
        transform.rotation *= Quaternion.Euler(0, -10, 0);

        for (int i = 0; i < 5; i++)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            transform.rotation *= Quaternion.Euler(0f, 5f, 0f);
            
            if (i == 5)
            {
                break;
            }
        }
    }
}
