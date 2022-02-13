using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyPlatform : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "SportsCar")
        {
            // transform refers to a Transform component that this sript is applied to
            collision.gameObject.transform.SetParent(transform);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "SportsCar")
        {
            // transform refers to a Transform component that this sript is applied to
            collision.gameObject.transform.SetParent(null);
        }
    }
}
