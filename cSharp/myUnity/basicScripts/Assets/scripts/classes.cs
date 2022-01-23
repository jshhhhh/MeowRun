using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from Object oriented programming, a class should be responsible for one task. 
public class classes : MonoBehaviour
{
    public class Stuff
    {
        // declare class variables
        public int stuffA; 
        public int stuffB; 
        public int stuffC;
        public string stuffD;
        public string stuffE;

        // create a constructor in C#
        // 1. constructor does not have return type
        // 2. name should be the same with class
        public Stuff(int first, int second, int third)
        {   
            stuffA = first;
            stuffB = second;
            stuffC = third;
        }

        // constructor overloading
        public Stuff(string first, string second)
        {
            stuffD = first;
            stuffE = second;
        }
    }

    public Stuff myStringStuff = new Stuff("singing", "eating");
    public Stuff myIntStuff = new Stuff(2,3,4);
}
