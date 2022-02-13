using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable : MonoBehaviour
{
    private Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        // Component should have a light component in Unity editor. 
        myLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        // set light intensity : value can be 0 to 8.
        myLight.intensity = 5;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}
