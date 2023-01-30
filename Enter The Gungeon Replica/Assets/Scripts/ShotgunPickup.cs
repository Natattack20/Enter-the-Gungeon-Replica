using UnityEngine;
using TMPro;

public class ShotgunPickup : GunBase
{
    private PlayerController playerController;
    private Transform playerTransform;
    private UIHolder ui;

    private void Start()
    {
        ui = GameObject.Find("Player").GetComponent<UIHolder>();
    }

    public override void SpawnProjectile()
    {
        if (allowShooting == true && currentClip != 0 && playerController.dodging == false)
        {
            float leftBounds = 10f;

            for (int i = 0; i < 7; i++)
            {
                Quaternion rotation = transform.parent.rotation * Quaternion.Euler(0, Random.Range(-leftBounds, leftBounds), 0);
                Instantiate(projectile, transform.position, rotation);

                if (i == 7)
                {
                    break;
                }
            }

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
            playerController = player.GetComponent<PlayerController>();
            ShotgunPickup shotgunPickup = GetComponent<ShotgunPickup>();
            Transform childText = transform.GetChild(0);
            TextMeshPro text = childText.GetComponent<TextMeshPro>();
            text.enabled = false;


            transform.SetParent(child.transform, false);
            transform.localPosition = new Vector3(0, 0, 1f);
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            child.GetComponent<WeaponSwitching>().SelectWeapon();
            GetComponent<BoxCollider>().enabled = false;
            shotgunPickup.enabled = true;
        }
    }

}
