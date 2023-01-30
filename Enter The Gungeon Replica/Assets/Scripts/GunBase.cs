using System.Collections;
using UnityEngine;
using TMPro;

public class GunBase : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public BulletSpeed bulletScript;
    public TextMeshPro objectText;
    public Vector3 spawnLocation;
    public bool allowShooting = true;
    public float pause = 0.6f;
    public float reloadRate = 0.8f;
    
    public int maxAmmo;
    public int currentAmmo;
    public int clipSize;
    public int currentClip;

    private void Start()
    {
        
    }
    void Update()
    {
        spawnLocation = transform.position + transform.right;

        if (Input.GetKeyDown(KeyCode.R) && currentClip < clipSize)
        {
            allowShooting = false;
            StopAllCoroutines();
            StartCoroutine(Reload(reloadRate));
        }

        // Shoots a Projectile based on the Left Mouse Button
        if (allowShooting == true)
        {
            ShootProjectile(0);
        }

    }

    IEnumerator Reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        currentClip = clipSize;
        allowShooting = true;
    }

    public virtual void SpawnProjectile()
    {
        
    }

    private void ShootProjectile(int Fire)
    {
        if (Input.GetMouseButton(Fire))
        {
            SpawnProjectile();
        }
    }

    public IEnumerator Cooldown(float Cool)
    {
        yield return new WaitForSeconds(Cool);
        allowShooting = true;
    }
    private void OnEnable()
    {
        allowShooting = true;
    }
}
