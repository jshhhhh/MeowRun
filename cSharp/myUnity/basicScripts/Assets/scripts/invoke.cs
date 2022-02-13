using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invoke : MonoBehaviour
{
    public GameObject target;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", 2, 1);
    }

    void SpawnObject() {
            float x = UnityEngine.Random.Range(target.transform.position.x + -50.0f, target.transform.position.x + 50.0f);
            float z = UnityEngine.Random.Range(target.transform.position.z + -50.0f, target.transform.position.z + 50.0f);
            Instantiate(target, new Vector3(x, 2, z), Quaternion.identity);
            count++;
        if (count > 5)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
