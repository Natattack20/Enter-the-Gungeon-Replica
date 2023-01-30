using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUp : MonoBehaviour
{
    public float multiplier = 0.25f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.damageMultiplier += multiplier;
            Destroy(gameObject);
        }
    }
}
