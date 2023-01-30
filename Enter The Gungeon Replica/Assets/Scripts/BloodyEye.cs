using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyEye : MonoBehaviour
{

    public float slow = 0.15f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.enemyBulletSpeed -= slow;
            Destroy(gameObject);
        }
    }
}
