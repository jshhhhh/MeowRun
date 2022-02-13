using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardinalDirection : MonoBehaviour
{
    enum Direction {
        North = 10,  
        East, // 11
        South, // 12
        West // 13
    }
    // Start is called before the first frame update
    void Start()
    {
        Direction myDirection = Direction.North;
    }

    Direction ReverseDirection(Direction dir) 
    {
        switch(dir) {
            case Direction.North : 
                dir = Direction.South;
                return dir;
            case Direction.South : 
                dir = Direction.North;
                return dir;
            case Direction.East : 
                dir = Direction.West;
                return dir;
            case Direction.West : 
                dir = Direction.East;
                return dir;
            default : 
                return dir;
        }
    }
}
