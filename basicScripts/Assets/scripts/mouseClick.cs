using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseClick : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown() {
        UnityEngine.Debug.Log("door clicked");
        // Rigidbody.AddForce : Adds a force to the Rigidbody.
        rb.AddForce(Vector3.up * 500f);

        // Rigidbody.useGravity : Controls whether gravity affects this rigidbody.
        rb.useGravity = true;
    }
}
