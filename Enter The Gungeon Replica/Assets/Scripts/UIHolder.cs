using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHolder : MonoBehaviour
{
    private PlayerController playerController;
    public TextMeshProUGUI health;
    public TextMeshProUGUI blanks;
    public TextMeshProUGUI armour;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI activeItem;
    public TextMeshProUGUI cooldown;
    public int currentAmmo;
    public int maxAmmo;
    public float coolDown;
    public string item;
    public static UIHolder instance;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        health.SetText("Health: " + playerController.health);
        blanks.SetText("Blanks: " + playerController.blanks);
        armour.SetText("Armour: " + playerController.armour);
        ammo.SetText(currentAmmo + "/" + maxAmmo);
        activeItem.SetText("Active Item: " + item);
        cooldown.SetText("Cooldown: " + coolDown);
    }
}
