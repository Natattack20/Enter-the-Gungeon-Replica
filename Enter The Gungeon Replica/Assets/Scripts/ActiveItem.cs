using UnityEngine;
using TMPro;

public class ActiveItem : MonoBehaviour
{
    public float maxCooldown;
    public float cooldown;
    public bool activation;
    public bool pickedUp;
    public bool itemPickup;
    public string itemName;
    public PlayerController playerController { get; set; }
    public Transform playerTransform { get; set; }
    public GameObject player { get; set; }
    public MeshRenderer playerMeshrenderer;
    public Material playerMaterial;

    private void Start()
    {
        activation = true;
        cooldown = maxCooldown;
        playerMaterial = playerMeshrenderer.material;
        itemName = gameObject.name;
    }

    public virtual void Activate()
    {

    }

    public void UITimer()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (cooldown < 0)
        {
            cooldown = maxCooldown;
            activation = true;
        }
    }

    public void AddItem(GameObject item)
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerTransform = player.transform;
        Transform child = playerTransform.GetChild(1);
        Transform childOfChild = child.GetChild(0);
        Ray ray = new(player.transform.position, -player.transform.forward * 5);
        int layerMask = 1 << 3;
        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out RaycastHit hit, 5, layerMask))
        {
            childOfChild.transform.localPosition = new Vector3(0, 0, 2);
            
        }
        else
        {
            childOfChild.transform.localPosition = new Vector3(0, 0, -2);
        }
        childOfChild.transform.rotation = new Quaternion(0, 0, 0, 0);
        childOfChild.GetChild(0).GetComponent<TextMeshPro>().enabled = true;
        childOfChild.GetComponent<MeshRenderer>().enabled = true;
        childOfChild.GetComponent<BoxCollider>().enabled = true;
        childOfChild.GetComponent<ActiveItem>().pickedUp = false;
        childOfChild.transform.SetParent(null);

        item.GetComponent<MeshRenderer>().enabled = false;
        item.GetComponent<BoxCollider>().enabled = false;
        item.transform.GetChild(0).GetComponent<TextMeshPro>().enabled = false;
        item.GetComponent<ActiveItem>().pickedUp = true;
        item.GetComponent<ActiveItem>().itemPickup = false;
        item.transform.SetParent(child.transform, false);
        item.transform.localPosition = Vector3.zero;
    }

}
