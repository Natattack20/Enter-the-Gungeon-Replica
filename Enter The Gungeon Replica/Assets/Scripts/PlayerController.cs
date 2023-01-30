using System.Collections;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed = 100;
    public float health = 100;
    private readonly float invulnTime = 0.6f;
    public int armour { get; set; }
    public int blanks { get; private set; }
    public bool dodging { get; private set; }
    public bool Invuln;
    public bool reflection;
    private Vector3 mousePosition;
    private Vector3 worldPosition;
    
    private Vector3 movement;
    public MeshRenderer meshRenderer;
    private Rigidbody playerRb;
    Plane plane = new(Vector3.up, 0);
    public Material material;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        dodging = false;
        Invuln = false;
        reflection = false;
        blanks = 2;
        armour = 1;
        material = meshRenderer.material;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.forward * 5);

        mousePosition = Input.mousePosition;


        // Handles the Movement
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Initiates the Dodge Roll
        if (Input.GetMouseButtonDown(1) && dodging == false && Invuln == false)
        {
            dodging = true;
            material.color = new Color(0, 25.3f, 0, 0f);
            playerRb.AddForce(playerRb.velocity / 2, ForceMode.Impulse);

            StartCoroutine(DodgeRoll(material));
        }

        // Allows for the destruction of enemy bullets
        Blank();

        // Casts a Ray that allows the rotation of the player
        RayCast();
        
        // Rotates the Player based on where the line hits
        RotatePlayer();
        


        if (health <= 0)
        {
            EditorApplication.ExitPlaymode();
        }
    }

    public void Damage()
    {
        StartCoroutine(Damaged());
    }
    private void FixedUpdate()
    {
        Movement(movement);
    }

    private void RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance) + new Vector3(0,0.5f,0);
        }        
    }

    public void RotatePlayer()
    {
        Vector3 targetdirection = worldPosition - transform.position;

        float singlestep = rotateSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetdirection, singlestep, 0);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator DodgeRoll(Material material)
    {
        yield return new WaitForSeconds(1);

        dodging = false;
        material.color = new Color(0, 25.5f, 0, 1f);
        
    }

    private Vector3 Movement(Vector3 direction)
    {
        if (dodging == false)
        {
            return playerRb.velocity = speed * direction;
        }
        else
        {
            return playerRb.velocity;
        }
    }

    private void Blank()
    {
        if (blanks > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.instance.DestroyBullets();
            blanks--;
        }
    }

    IEnumerator Damaged()
    {
        yield return new WaitForSeconds(invulnTime);
        Invuln = false;
    }
}
