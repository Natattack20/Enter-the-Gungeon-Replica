using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.enabled = false;
    }
    public void Spawn()
    {
        int index = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[index], transform.position, transform.rotation);

    }
}
