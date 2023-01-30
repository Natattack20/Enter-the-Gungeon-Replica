using System.Collections;
using TMPro;
using UnityEngine;

public class Reflection : ActiveItem
{

    public float timeInvuln = 6f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activation == true && pickedUp == true)
        {
            Activate();
            activation = false;
            playerController.reflection = true;
        }
        if (itemPickup && Input.GetKeyDown(KeyCode.E))
        {
            AddItem(gameObject);
        }
        if (activation == false)
        {
            UITimer();
        }
    }
    public override void Activate()
    {
        StartCoroutine(NoDamage());
        StartCoroutine(MaterialChange());
    }

    IEnumerator MaterialChange()
    {
        playerMaterial.color = new Color(0, 25.5f, 0, 0f);
        yield return new WaitForSeconds(timeInvuln);
        playerMaterial.color = new Color(0, 25.5f, 0, 1f);
    }

    IEnumerator NoDamage()
    {
        yield return new WaitForSeconds(timeInvuln);
        playerController.reflection = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerController = player.GetComponent<PlayerController>();
            playerTransform = player.transform;
            Transform child = playerTransform.GetChild(1);

            if (child.GetComponent<ActiveItemHolder>().itemInSlot == false)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                transform.GetChild(0).GetComponent<TextMeshPro>().enabled = false;

                transform.SetParent(child.transform, false);
                transform.localPosition = Vector3.zero;
                child.GetComponentInParent<ActiveItemHolder>().itemInSlot = true;
                activation = true;
                pickedUp = true;
            }
            else
            {
                itemPickup = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            itemPickup = false;
        }
    }
}
