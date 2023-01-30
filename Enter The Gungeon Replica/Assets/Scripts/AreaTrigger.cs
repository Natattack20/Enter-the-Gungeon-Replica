using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject[] doors;
    public string player;
    private bool doorSpawnCondition = false;
    private bool spawnerSpawnCondition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Enter();
        }
    }
    
    private void Enter()
    {
        while (doorSpawnCondition == false && spawnerSpawnCondition == false)
        {
            Spawners();

            Doors();
        }

        if (doorSpawnCondition == true && spawnerSpawnCondition == true)
        {

        }
    }

    private void Spawners()
    {
        for (int i = 0; i < spawners.Length; i++)
        {

            Spawner spawner = spawners[i].GetComponent<Spawner>();

            spawner.Spawn();

            if (i == spawners.Length - 1)
            {
                spawnerSpawnCondition = true;
                break;
            }
        }
    }
    private void Doors()
    {
        for (int i = 0; i < doors.Length; i++)
        {

            Door door = doors[i].GetComponent<Door>();

            door.CloseDoor();

            if (i == doors.Length - 1)
            {
                doorSpawnCondition = true;
                break;
            }
        }
    }
}
