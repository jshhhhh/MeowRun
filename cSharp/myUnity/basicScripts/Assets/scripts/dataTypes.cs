using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataTypes : MonoBehaviour
{
    // Call by value and call by reference in Unity
    // call by value : int, float, double, bool, char, Structs(Vector3, Quaternion)
    // call by reference : Class(Transform, GameObject)
    // Start is called before the first frame update
    void Start()
    {
        // Transform : class, trans : reference type
        Transform trans = transform; 
        trans.position = new Vector3(1.17f,10,-17.73f); // changing trans.position affects transform since it is a reference type.
    }
}
