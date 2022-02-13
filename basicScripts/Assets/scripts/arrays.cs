using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrays : MonoBehaviour
{
    List<GameObject> players;
    List<int> intList = new List<int>{1};
    int[] myLimitedArray = new int[4];
    int[] myUnlimitedArray = {1,2,3} ;
    // Start is called before the first frame update
    void Start()
    {
        myLimitedArray[0] = 1; 
        // myUnlimitedArray[3] = 5;

        // Using list as an alternative of array in C#
        for (int i=0; i<3; i++)
        {
            intList.Add(i);
        }
        UnityEngine.Debug.Log($"total count of list is : {intList.Count}");

        // find game objects with player tag
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        for (int j=0; j< players.Count; j++) 
        {
            UnityEngine.Debug.Log($"{j}th's player name : {players[j].name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
