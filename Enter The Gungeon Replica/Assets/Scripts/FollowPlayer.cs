using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset = new Vector3(0, 12, -3);

    private void LateUpdate()
    {
        CameraOffset();
    }

    private void CameraOffset()
    {
        transform.position = Player.transform.position + offset;
    }
}
