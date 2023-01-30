using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
    }

    public void CloseDoor()
    {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    public void OpenDoor()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}
