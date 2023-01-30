using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemHolder : MonoBehaviour
{
    public bool itemInSlot { get; set; }
    private UIHolder ui;
    private ActiveItem Item;
    void Start()
    {
        if (GameManager.instance != null)
        {

        }
        else
        {
            itemInSlot = false;
        }
        
        ui = GetComponentInParent<UIHolder>();
    }

    private void Update()
    {
        if (transform.childCount == 1)
        {
            Item = GetComponentInChildren<ActiveItem>();
            ItemCheck();
            ui.coolDown = Mathf.RoundToInt(Item.cooldown);
        }
    }
    
    private void ItemCheck()
    {
        ui.item = Item.itemName;
    }
}
