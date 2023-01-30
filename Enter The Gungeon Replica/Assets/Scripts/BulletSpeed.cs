using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeed : MonoBehaviour
{
    public float speed = 20;
    public float damage = 20;
    private float seconds = 1;
    public float afterLife = 6;
    public float impactForce = 30f;


    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        seconds += Time.deltaTime;
       
        if (seconds > afterLife)
        {
            Destroy(gameObject);
        }
    }
}
