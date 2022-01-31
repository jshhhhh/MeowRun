using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to main camera and set target to follow. 
public class cameraLookAt : MonoBehaviour
{
    // Transform : Position, rotation and scale of an object.
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target); 
    }
}
