using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemyCount;
    public float damageMultiplier = 1;
    public float enemyBulletSpeed = 1;
    public float enemyBulletHealthMultiplier = 1;
    private bool aliveEnemies = false;
    public GameObject playerProjectile;
    public GameObject[] doors;
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount();        
    }

    private void EnemyCount()
    {
        if (enemyCount == 0)
        {
            while (aliveEnemies == false)
            {

                for (int i = 0; i < doors.Length; i++)
                {
                    Door door = doors[i].GetComponent<Door>();
                    door.OpenDoor();
                    if (i == doors.Length - 1)
                    {
                        aliveEnemies = true;
                        break;
                    }
                }
            }
        }
        else
        {
            aliveEnemies = false;
        }
    }

    public void DestroyBullets()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");

        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i].gameObject);
        }
    }
    
}
