using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterGun : GunBase
{
    private PlayerController playerController;
    private Transform playerTransform;
    private UIHolder ui;

    void Start()
    {
        ui = GameObject.Find("Player").GetComponent<UIHolder>();
    }

    public override void SpawnProjectile()
    {
        if (allowShooting == true && currentClip != 0 && playerController.dodging == false)
        {
            Instantiate(projectile, spawnLocation, transform.parent.rotation);
            allowShooting = false;

            currentClip--;
            currentAmmo--;
            
            ui.currentAmmo = currentAmmo;

            StartCoroutine(Cooldown(pause));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerTransform = player.transform;
            Transform child = playerTransform.GetChild(0);
            transform.SetParent(child.transform, false);
            transform.localPosition = new Vector3(0, 0, 1f);
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            child.GetComponent<WeaponSwitching>().SelectWeapon();
            GetComponent<BoxCollider>().enabled = false;
            playerController = player.GetComponent<PlayerController>();
            StarterGun starterGun = GetComponent<StarterGun>();
            starterGun.enabled = true;
        }
    }
}
