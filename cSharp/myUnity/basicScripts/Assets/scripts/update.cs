using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class update : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // created with VS code Extension
    void LateUpdate()
    {
        UnityEngine.Debug.Log("called from Extension");
    }

    void FixedUpdate()
    {
        // regular intervals between updates
        // physics update should be done here => e.g Rigidbody
        UnityEngine.Debug.Log($"FixedUpdateTime : {Time.deltaTime}");
    }

    // Update is called once per frame => almost every changes needed happen here.
    void Update()
    {
        UnityEngine.Debug.Log($"Update time : {Time.deltaTime}"); 
    }
}
