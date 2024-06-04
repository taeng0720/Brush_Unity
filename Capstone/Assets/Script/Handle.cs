using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public GameObject Wheel;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,Wheel.transform.rotation.y, 0));
        if(transform.rotation.y > 45)
        {
            transform.Rotate(new Vector3(0,44.5f, 0));
        }
        if(transform.rotation.y < -45)
        {
            transform.Rotate(new Vector3(0,-44.5f, 0));
        }
    }
}