using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiate : MonoBehaviour
{
    public Rigidbody BlackSphere;
    public Transform launcherEdge;

    private void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Invoke("Spawn", Time.deltaTime*100);
        }
    }

    private void Spawn()
    {
        Rigidbody instance;
        instance = Instantiate(BlackSphere, launcherEdge.position, launcherEdge.rotation) as Rigidbody;
        instance.AddForce(launcherEdge.up*300f);
    }
}
