using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletSpeed
{
    private EnemyBase EnemyBase;
    private float multiplier;

    private void Start()
    {
        multiplier = GameManager.instance.damageMultiplier;
        damage *= multiplier;
        Mathf.Round(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyBase = other.gameObject.GetComponent<EnemyBase>();
            
            EnemyBase.health -= 20;

            Destroy(gameObject);
        }
        else if (other.CompareTag("Building"))
        {
            Destroy(gameObject);
        }
    }
}
