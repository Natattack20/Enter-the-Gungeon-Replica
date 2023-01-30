using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GundromedaVirus : MonoBehaviour
{
    public float healthChange = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.enemyBulletHealthMultiplier += healthChange;
        }
    }
}
