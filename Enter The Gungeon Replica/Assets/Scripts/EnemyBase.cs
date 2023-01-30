using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public float health = 100;
    private float seconds = 1;
    private float max = 3;
    private float healthMultiplier;
    public GameObject projectile;
    private BulletSpeed projectileSpeed;
    private GameObject player;
    private Vector3 previousPlayerPosition;
    private Vector3 spawnLocation;
    private float rotateSpeed = 45;
    private string playerName;
    private bool allowShooting;
    public bool Veteran;

    // Used for AI
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    public float attackRange;
    public bool playerInAttackRange;


    private void Start()
    {
        // Finds the player and uses its position for aiming
        player = GameObject.Find("Player");

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Gets the speed of the projectile for the smart aim
        projectileSpeed = projectile.GetComponent<BulletSpeed>();

        // At the start of the enemy spawning in, add one to enemy count
        GameManager.instance.enemyCount += 1;

        // Gets the GameManager instance and obtains the health multiplier, multiplies the halth by said number
        healthMultiplier = GameManager.instance.enemyBulletHealthMultiplier;
        health *= healthMultiplier;
        Mathf.Round(health);
    }

    private void FixedUpdate()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Gets the spawn location by getting its position, plus it's forward vector, which is local
        spawnLocation = transform.position + transform.forward;

        // Used for timer
        seconds += Time.deltaTime;

        // If the veteran boolean is checked, use smart aim, if not, use regular aim
        if (Veteran == true)
        {
            RotateEnemySmart();
        }
        else
        {
            RotateEnemy();
        }

        // Raycast used for finding if the player is visible
        RayCast();

        // Cooldown function, effectively prevents the enemy from firing all the time
        Cooldown();

        if (!playerInAttackRange)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

        // If health = 0, decrease enemy count and destroy the object
        if (health <= 0)
        {
            GameManager.instance.enemyCount -= 1;
            Destroy(gameObject);
        }

    }

    private void RotateEnemySmart()
    {
        // As the name implies, it is the smart aim version, uses some quadratics and shit, so don't touch
        // Just make sure the speed of the projectile is greater than the speed of the player
        Vector3 velocity = (player.transform.position - previousPlayerPosition) / Time.deltaTime;

        float bulletSpeed = projectileSpeed.speed;

        float Distance = Vector3.Distance(player.transform.position, transform.position);

        float timeToReach = Distance / bulletSpeed;

        Vector3 targetNewPosition = player.transform.position + velocity * timeToReach;

        Vector3 targetDirection = targetNewPosition - transform.position;

        Debug.DrawRay(transform.position, targetDirection);

        float step = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0);

        transform.rotation = Quaternion.LookRotation(newDirection);

        previousPlayerPosition = player.transform.position;
    }
    private void RotateEnemy()
    {
        // This is dumb aim, also don't touch but projectile speed doesn't matter

        Vector3 targetDirection = player.transform.position - transform.position;

        float step = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void RayCast()
    {
        // Used for finding the player and making sure it isn't being blocked by a wall or some such, no touch
        Vector3 Direction = player.transform.position - transform.position;

        Ray ray = new(transform.position, Direction * 10);

        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            playerName = hitinfo.collider.gameObject.name;

            if (playerName == "Player")
            {
                allowShooting = true;
            }
            else
            {
                allowShooting = false;
            }
        }
    }

    public virtual void Shoot()
    {
        // Bang Bang
        Instantiate(projectile, spawnLocation, transform.rotation);
    }

    private void Cooldown()
    {
        // Effectively stops the enemy shooting until it reaches the 'max'
        if (seconds > max && allowShooting == true)
        {
            Shoot();
            seconds = 0;
        }
    }
}

