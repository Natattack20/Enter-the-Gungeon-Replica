using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletSpeed
{
    private PlayerController playerController;
    private EnemyBase enemyBase;

    private void Start()
    {
        speed *= GameManager.instance.enemyBulletSpeed;
        Mathf.Round(speed);
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.dodging == true || playerController.Invuln == true)
            {

            }
            else if (playerController.reflection)
            {
                speed *= -1;
            }
            else if (playerController.armour > 0) 
            {
                GameManager.instance.DestroyBullets();
                playerController.armour--;
            }
            else
            {
                playerController.health -= 20;

                playerController.Invuln = true;

                playerController.Damage();

                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Building"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            enemyBase = other.gameObject.GetComponent<EnemyBase>();
            if (speed < 0)
            {
                enemyBase.health -= 20;
            }
        }
    }
}
