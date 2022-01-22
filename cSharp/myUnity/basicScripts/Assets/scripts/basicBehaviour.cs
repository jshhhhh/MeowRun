using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicBehaviour : MonoBehaviour
{

    // variables and methods in class considered local 
    // since default access modifier in C# is private.
    // if access modifier is public and variable is in class scope, the value will be
    // overwritten by inspector(Unity editor)
    public int myInt = 5; 
    private useMyFunction test;

    // Start is called before the first frame update
    void Start()
    {
        // if variable declared inside function, it won't be overwritten in inspector.
        float myFloat = 5.0f;
        UnityEngine.Debug.Log(myFloat);
        AddIntegers(3,11);

        int result = new useMyFunction().AccessibleFunction(); // import directly
        test = new useMyFunction(); // create a class instance
        int result2 = test.AccessibleFunction(); // access to the instance's method

        UnityEngine.Debug.Log($"imported value : {result}, {result2}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            GetComponent<Renderer>().material.color = new Color32(127, 17, 224, 1);
            Invoke("ToBlue", 2.0f);
        }
        
    }

    private void ToBlue() 
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    // create an add interger function
    private int AddIntegers(int first, int second)
    {
        int result = first + second;
        UnityEngine.Debug.Log($"result is : {result}");
        return result;
    }

    // code convention 

}
