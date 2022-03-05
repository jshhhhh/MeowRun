using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_alien : MonoBehaviour
{
    public E_PARENT.enemyState states;

    // Start is called before the first frame update
    void Start()
    {
        states = E_PARENT.enemyState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
